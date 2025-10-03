﻿using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Users.Commands
{
    public class UpdateUserCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public string FullName { get; set; } // Adı - Soyadı
        public string Email { get; set; } // E-Posta
        public string MobilePhones { get; set; } // Telefon
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;

            public UpdateUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }


            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.UserId == request.UserId);

                isThereAnyUser.FullName = request.FullName;
                isThereAnyUser.Email = request.Email;
                isThereAnyUser.MobilePhones = request.MobilePhones;

                _userRepository.Update(isThereAnyUser);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}