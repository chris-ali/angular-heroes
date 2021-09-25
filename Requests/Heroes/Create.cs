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

        public class CommandHandler : BaseRequest, IRequestHandler<Command, Hero>
        {
            public CommandHandler(HeroesDbContext context) : base(context)
            {
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

                await context.AddAsync<Hero>(request.hero, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                // logger.LogDebug($"Added new hero: {request.hero.Id} - {request.hero.Name}!");

                return request.hero;
            }
        }
    }
}