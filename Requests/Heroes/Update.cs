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
        public record Command(Hero hero) : IRequest<Hero>;

        public class CommandHandler : BaseRequest, IRequestHandler<Command, Hero>
        {
            public CommandHandler(HeroesDbContext context) : base(context)
            {
            }

            public async Task<Hero> Handle(Command request, CancellationToken cancellationToken)
            {
                var data = await context.FindAsync<Hero>(new object[] { request.hero.Id }, cancellationToken);

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
                await context.SaveChangesAsync(cancellationToken);

                // logger.LogDebug($"...updated successfully!");

                return data;
            }
        }
    }
}