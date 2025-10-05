using System;
using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Authorizations.ValidationRules;
using Business.Services.Authentication;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations.Commands
{
    public class RegisterUserCommand : IRequest<IDataResult<AccessToken>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }


        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IDataResult<AccessToken>>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IMediator _mediator;
            private readonly ICacheManager _cacheManager;

            public RegisterUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IMediator mediator, ICacheManager cacheManager)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _mediator = mediator;
                _cacheManager = cacheManager;
            }


            public async Task<IDataResult<AccessToken>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.Email == request.Email);

                if (user == null)
                {
                    HashingHelper.CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);
                    var usr = new User
                    {
                        Email = request.Email,
                        FullName = request.FullName,
                        MobilePhones = "05555555555",
                        RecordDate = DateTime.Now,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Status = true
                    };

                    _userRepository.Add(usr);
                    await _userRepository.SaveChangesAsync();
                    user = await _userRepository.GetAsync(u => u.Email == request.Email);
                }

                var claims = _userRepository.GetClaims(user.UserId);

                var accessToken = _tokenHelper.CreateToken<DArchToken>(new UserJwtDto
                {
                    AuthenticationProviderType = user.AuthenticationProviderType,
                    UserId = user.UserId,
                    FullName = user.FullName,
                });
                accessToken.Claims = claims.Select(x => x.Name).ToList();

                user.RefreshToken = accessToken.RefreshToken;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                _cacheManager.Add($"{CacheKeys.UserIdForClaim}={user.UserId}", claims.Select(x => x.Name));
                return new SuccessDataResult<AccessToken>(accessToken, Messages.SuccessfulLogin);
            }
        }
    }
}