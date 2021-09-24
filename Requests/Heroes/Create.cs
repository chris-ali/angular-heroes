using System.Net;
using System.Threading;
using System.Threading.Tasks;
using angular_heroes.Infrastructure;
using angular_heroes.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Requests.Heroes
{
    public class Create
    {
        public record Command(Hero hero) : IRequest<Hero>;

        public class CommandHandler : IRequestHandler<Command, Hero>
        {
            private readonly HeroesDbContext context;

            public CommandHandler(HeroesDbContext context)
            {
                this.context = context;
                context.Database.EnsureCreated();
            }

            public async Task<Hero> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.hero == null) 
                {
                    var message = $"No hero data found in request";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.BadRequest, new { Message = message});
                }

                // Add server-side validation here?

                await context.AddAsync<Hero>(request.hero);
                await context.SaveChangesAsync();

                // logger.LogDebug($"Added new hero: {request.hero.Id} - {request.hero.Name}!");

                return request.hero;
            }
        }
    }
}