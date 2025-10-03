using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Business.Services;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Mail;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Users.Commands
{
    public class UserChangePasswordCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public string Password { get; set; }

        public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;
            private readonly IMailService _mailService;
            private readonly IFileService _fileService;
            public UserChangePasswordCommandHandler(IUserRepository userRepository,
                IMediator mediator,
                IMailService mailService, IFileService fileService)
            {
                _userRepository = userRepository;
                _mediator = mediator;
                _mailService = mailService;
                _fileService = fileService;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.UserId == request.UserId);
                if (isThereAnyUser == null)
                {
                    return new ErrorResult(Messages.UserNotFound);
                }

                HashingHelper.CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);

                isThereAnyUser.PasswordHash = passwordHash;
                isThereAnyUser.PasswordSalt = passwordSalt;

                _userRepository.Update(isThereAnyUser);
                await _userRepository.SaveChangesAsync();
                var content = await _fileService
                   .ReadFileContentAsync("FilePaths:EmailTemplatePath", "username-or-password-change.txt");
                content = content.Replace("#KULLANICIADI", isThereAnyUser.FullName);
                content = content.Replace("#SIFRE", request.Password);
                _mailService.Send(new EmailMessage
                {
                    ToAddresses = new List<EmailAddress>
                    {
                        new EmailAddress { Name = isThereAnyUser.FullName.ToUpper(), Address = isThereAnyUser.Email}
                    },
                    FromAddresses = new List<EmailAddress>
                    {
                        new EmailAddress { Name = "Işık Medya", Address = "destek@isik.media" }
                    },
                    Subject = $"Sayın {isThereAnyUser.FullName.ToUpper()} - Kullanıcı Adınız Yada Şifreniz Güncellendi!",
                    Content = content,
                });
                
                                   
                var contentAdmin = await _fileService
                    .ReadFileContentAsync("FilePaths:EmailTemplatePath", "admin-bilgilendirme.txt");
                contentAdmin = contentAdmin.Replace("#KULLANICIADI", isThereAnyUser.FullName.ToUpper());
                contentAdmin = contentAdmin.Replace("#EPOSTA", isThereAnyUser.Email);
                contentAdmin = contentAdmin.Replace("#KONU", "Kullanıcı Şifresi Başarılı Bir Şekilde Güncellendi.");
                contentAdmin = contentAdmin.Replace("#ACIKLAMA", $"Kullanıcı Adı: {isThereAnyUser.Email}" +
                                                                 $"Şifre: {request.Password}" );
                
                _mailService.Send(new EmailMessage
                {
                    ToAddresses = new List<EmailAddress>
                    {
                        new EmailAddress { Name = "Işık Medya", Address = "info@isik.media"}
                    },
                    FromAddresses = new List<EmailAddress>
                    {
                        new EmailAddress { Name = "Işık Medya", Address = "destek@isik.media" }
                    },
                    Subject = $"{isThereAnyUser.FullName.ToUpper()} Adlı Kullanıcının Şifresi Başarılı Bir Şekilde Güncellendi.",
                    Content = contentAdmin
                });

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}