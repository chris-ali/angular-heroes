using System.Net;
using System.Threading;
using System.Threading.Tasks;
using angular_heroes.Infrastructure;
using angular_heroes.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Requests.Heroes
{
  public class Update
    {
        public record Command(Hero hero)  : IRequest<Hero>;

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
                var data = await context.FindAsync<Hero>(request.hero.Id);

                if (data == null) 
                {
                    var message = $"No hero found for id: {request.hero.Id}";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.NotFound, new { Message = message});
                }
                
                // logger.LogDebug($"Found hero: {data.Id} - {data.Name} to update...");

                data.Name = request.hero.Name;
                data.Power = request.hero.Power;
                
                context.Update<Hero>(data);
                await context.SaveChangesAsync();

                // logger.LogDebug($"...updated successfully!");

                return data;
            }
        }
    }
}