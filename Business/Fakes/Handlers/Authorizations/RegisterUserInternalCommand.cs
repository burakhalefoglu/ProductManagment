using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.Authorizations.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using MediatR;

namespace Business.Fakes.Handlers.Authorizations
{
    public class RegisterUserInternalCommand : IRequest<IResult>
    {
        public string FullName { get; set; } // Adı - Soyadı
        public string Email { get; set; } // E-Posta
        public string MobilePhones { get; set; } // Telefon
        public string Password { get; set; } // Kullanıcı Şifresi


        public class RegisterUserInternalCommandHandler : IRequestHandler<RegisterUserInternalCommand, IResult>
        {
            private readonly IUserRepository _userRepository;


            public RegisterUserInternalCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }


            [ValidationAspect(typeof(RegisterUserValidator), Priority = 2)]
            [CacheRemoveAspect()]
            public async Task<IResult> Handle(RegisterUserInternalCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.Email);

                if (isThereAnyUser != null)
                {
                    return new ErrorResult(Messages.NameAlreadyExist);
                }

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
                _userRepository.Add(user); 
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}