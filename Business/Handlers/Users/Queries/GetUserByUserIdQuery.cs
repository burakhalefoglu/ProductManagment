
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
namespace Business.Handlers.Users.Queries 
{

    public class GetUserByUserIdQuery : IRequest<IDataResult<User>>
    {

        public class GetUserByUserIdQueryHandler : IRequestHandler<GetUserByUserIdQuery, IDataResult<User>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetUserByUserIdQueryHandler(IUserRepository userRepository, IMediator mediator)
            {
                _userRepository = userRepository;
                _mediator = mediator;
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<User>> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
            {
                var userId = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var user = await _userRepository.GetAsync(u => u.UserId == int.Parse(userId));
                return new SuccessDataResult<User>(user);
            }
        }
    }
}
