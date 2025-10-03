using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
namespace Business.Fakes.Handlers.Users
{

    public class GetUserByUserIdInternalQuery : IRequest<IDataResult<User>>
    {
        public int UserId { get; set; }
        public class GetUserByUserIdInternalQueryHandler : IRequestHandler<GetUserByUserIdInternalQuery, IDataResult<User>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public GetUserByUserIdInternalQueryHandler(IUserRepository userRepository, 
                IMediator mediator)
            {
                _userRepository = userRepository;
                _mediator = mediator;

            }
            public async Task<IDataResult<User>> Handle(GetUserByUserIdInternalQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.UserId == request.UserId);
                return new SuccessDataResult<User>(user);
            }
        }
    }
}

