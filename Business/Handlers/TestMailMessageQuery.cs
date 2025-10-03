
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Entities.Dtos;
using Core.Utilities.Mail;
using System.Collections.Generic;
using Business.Services;

namespace Business.Handlers.Users.Queries
{

    public class TestMailMessageQuery : IRequest<IDataResult<TestDto>>
    {
        public string Email { get; set; }
        public class TestMailMessageQueryHandler : IRequestHandler<TestMailMessageQuery, IDataResult<TestDto>>
        {

            private readonly Core.Utilities.Mail.IMailService _mailService;
            private readonly IFileService _fileService;

            public TestMailMessageQueryHandler(IMailService mailService, IFileService fileService)
            {
                _mailService = mailService;
                _fileService = fileService;
            }
            public async Task<IDataResult<TestDto>> Handle(TestMailMessageQuery request, CancellationToken cancellationToken)
            {
                var content = await _fileService.ReadFileContentAsync("FilePaths:EmailTemplatePath", "order.txt");

                _mailService.Send(new EmailMessage
                {
                    ToAddresses = new List<EmailAddress>
                    {
                        new EmailAddress { Name = "Bu Bir Test Mailidir.", Address = request.Email}
                    },
                    FromAddresses = new List<EmailAddress>
                    {
                        new EmailAddress { Name = "Işık Medya", Address = "destek@isik.media" }
                    },
                    Subject = "Mail Testi Başarılı!",
                    Content = content,
                });
                return new SuccessDataResult<TestDto>();
            }
        }
    }
}