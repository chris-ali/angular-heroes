using angular_heroes.Models;
using angular_heroes.Requests.LogMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace angular_heroes.Controllers
{
  public class LogMessagesController : BaseAngularHeroesController
    {
        public LogMessagesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{createdBy}")]
        public async Task<IEnumerable<LogMessage>> GetManyByUserName(string createdBy, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetManyByUserName.Query(createdBy), cancellationToken);
        }

        [HttpPost]
        public async Task<LogMessage> Create(LogMessage message, CancellationToken cancellationToken)
        {
            return await mediator.Send(new Create.Command(message), cancellationToken);
        }

        [HttpDelete("{createdBy}")]
        public async Task<IEnumerable<LogMessage>> DeleteManyByUserName(string createdBy, CancellationToken cancellationToken)
        {
            return await mediator.Send(new DeleteManyByUserName.Command(createdBy), cancellationToken);
        }
    }
}
