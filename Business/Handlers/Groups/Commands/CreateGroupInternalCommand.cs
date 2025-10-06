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

namespace Business.Handlers.Groups.Commands
{
    public class CreateGroupInternalCommand : IRequest<IResult>
    {
        public string GroupName { get; set; }

        public class CreateGroupInternalCommandHandler : IRequestHandler<CreateGroupInternalCommand, IResult>
        {
            private readonly IGroupRepository _groupRepository;


            public CreateGroupInternalCommandHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            public async Task<IResult> Handle(CreateGroupInternalCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var group = new Group
                    {
                        GroupName = request.GroupName
                    };
                    _groupRepository.Add(group);
                    await _groupRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Added);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}