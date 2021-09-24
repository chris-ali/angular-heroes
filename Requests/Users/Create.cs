using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using angular_heroes.Infrastructure;
using angular_heroes.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Requests.Users
{
    public class Create
    {
        // TODO make user wrapper here that has reistration details; use as input to endpoint; Models.User should just have JWT token

        public record Command(User user) : IRequest<User>;

        public class CommandHandler : IRequestHandler<Command, User>
        {
            private readonly HeroesDbContext context;

            public CommandHandler(HeroesDbContext context)
            {
                this.context = context;

                context.Database.EnsureCreated();
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.user == null) 
                {
                    var message = $"No user data found in request";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.BadRequest, new { Message = message});
                }

                if(await context.Users.Where(x => x.UserName == request.user.UserName).AnyAsync(cancellationToken))
                {
                    var message = $"Username already exists in database";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.BadRequest, new { Message = message});
                }

                if(await context.Users.Where(x => x.Email == request.user.Email).AnyAsync(cancellationToken))
                {
                    var message = $"Email already exists in database";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.BadRequest, new { Message = message});
                }

                // Add server-side validation here?

                // TODO Create token hash and salt here

                await context.AddAsync<User>(request.user);
                await context.SaveChangesAsync(cancellationToken);

                // logger.LogDebug($"Added new user: {request.user.Id} - {request.user.UserName}!");

                return request.user;
            }
        }
    }
}