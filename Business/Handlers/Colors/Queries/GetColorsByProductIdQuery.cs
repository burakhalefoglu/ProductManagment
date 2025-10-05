
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Colors.Queries
{

    public class GetColorsByProductIdQuery : IRequest<IDataResult<IEnumerable<Color>>>
    {
        public int ProductId { get; set; }
        public class GetColorsByProductIdQueryHandler : IRequestHandler<GetColorsByProductIdQuery, IDataResult<IEnumerable<Color>>>
        {
            private readonly IColorRepository _colorRepository;
            private readonly IMediator _mediator;

            public GetColorsByProductIdQueryHandler(IColorRepository colorRepository, IMediator mediator)
            {
                _colorRepository = colorRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Color>>> Handle(GetColorsByProductIdQuery request, CancellationToken cancellationToken)
            {
                var colors = await _colorRepository.GetListAsync(x => x.ProductId == request.ProductId); 
                return new SuccessDataResult<IEnumerable<Color>>(colors);
            }
        }
    }
}