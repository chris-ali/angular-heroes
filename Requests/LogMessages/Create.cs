using System.Net;
using System.Threading;
using System.Threading.Tasks;
using angular_heroes.Infrastructure;
using angular_heroes.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Requests.LogMessages
{
    public class Create
    {
        public record Command(LogMessage message) : IRequest<LogMessage>;

        public class CommandHandler : IRequestHandler<Command, LogMessage>
        {
            private readonly HeroesDbContext context;

            public CommandHandler(HeroesDbContext context)
            {
                this.context = context;
                context.Database.EnsureCreated();
            }

            public async Task<LogMessage> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.message == null) 
                {
                    var message = $"No message data found in request";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.BadRequest, new { Message = message});
                }

                // Add server-side validation here?

                // Set current user as ownner 
                // or use user accessor here instead
                // var currentUser = context.Users.FindAsync(x => x.UserName == request.message.createdBy);

                // request.message.Owner = currentUser;
                await context.AddAsync<LogMessage>(request.message);
                await context.SaveChangesAsync();

                // logger.LogDebug($"Added new message: {request.message.Id} - {request.message.Contents}!");

                return request.message;
            }
        }
    }
}