using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using angular_heroes.Infrastructure;
using angular_heroes.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Requests.LogMessages
{
  public class DeleteManyByUserName
    {
        public record Command(string userName)  : IRequest<IEnumerable<LogMessage>>;

        public class CommandHandler : BaseRequest, IRequestHandler<Command, IEnumerable<LogMessage>>
        {
            public CommandHandler(HeroesDbContext context) : base(context)
            {
            }

            public async Task<IEnumerable<LogMessage>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.userName))
                {
                    var message = $"Must provide userName";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.BadRequest, new { Message = message});
                }

                var data = context.Messages.Include(x => x.Owner).Where(x => x.Owner.UserName == request.userName);

                if (data == null) 
                {
                    var message = $"No messages found to delete for user: {request.userName}";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.NotFound, new { Message = message});
                }
                
                // logger.LogDebug($"Found {data.Count()} messages from {request.userName} to delete...");

                context.RemoveRange(data);
                await context.SaveChangesAsync(cancellationToken);
                
                // logger.LogDebug($"...deleted successfully!");

                return data;    
            }
        }
    }
}