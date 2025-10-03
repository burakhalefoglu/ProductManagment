using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Mail;
using System.Collections.Generic;
using Business.Handlers.UserGroups.Commands;
using Business.Services;

namespace Business.Handlers.Users.Commands
{
    public class CreateUserCommand : IRequest<IResult>
    {
        public string FullName { get; set; } // Adı - Soyadı
        public string Email { get; set; } // E-Posta
        public string MobilePhones { get; set; } // Telefon

        public string Password { get; set; } // Kullanıcı Şifresi

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;
            private readonly IMailService _mailService;
            private readonly IFileService _fileService;

            public CreateUserCommandHandler(IUserRepository userRepository,
                IMailService mailService, IMediator mediator,
                IFileService fileService)
            {
                _userRepository = userRepository;
                _mailService = mailService;
                _mediator = mediator;
                _fileService = fileService;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.Email);

                if (isThereAnyUser != null)
                {
                    return new ErrorResult(Messages.NameAlreadyExist);
                }
                // Şifreleme işlemi
                HashingHelper.CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);
                var user = new User
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    MobilePhones = request.MobilePhones,
                    Status = true,
                    RecordDate = DateTime.Now,
                    UpdateContactDate = DateTime.Now,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                var result = _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();

                await _mediator.Send(new CreateUserGroupCommand
                {
                    UserId = result.UserId,
                    GroupId = 1
                });
                return new SuccessResult(Messages.Added);
            }
        }
    }
}