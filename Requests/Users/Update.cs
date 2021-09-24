using System.Net;
using System.Threading;
using System.Threading.Tasks;
using angular_heroes.Infrastructure;
using angular_heroes.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Requests.Users
{
  public class Update
    {
        // TODO make user wrapper here that has password; Models.User should just have JWT token 

        public record Command(User user)  : IRequest<User>;

        public class CommandHandler
        {
            private readonly HeroesDbContext context;

            public CommandHandler(HeroesDbContext context)
            {
                this.context = context;
                context.Database.EnsureCreated();
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                var data = await context.FindAsync<User>(request.user.Id, cancellationToken);

                if (data == null) 
                {
                    var message = $"No user found for id: {request.user.Id}";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.NotFound, new { Message = message});
                }
                
                // logger.LogDebug($"Found user id: {data.Id} to update...");
                
                data.FirstName = request.user.FirstName;
                data.LastName = request.user.LastName;
                data.Email = request.user.Email;
                data.UserName = request.user.UserName;
                // TODO Handle password hashing here if password edited

                context.Update(data);
                await context.SaveChangesAsync(cancellationToken);

                // logger.LogDebug($"...updated successfully!");

                return data;
            }
        }
    }
}