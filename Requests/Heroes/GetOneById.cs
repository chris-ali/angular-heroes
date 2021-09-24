using System.Threading;
using System.Threading.Tasks;
using angular_heroes.Infrastructure;
using angular_heroes.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Requests.Heroes
{
  public class GetOneById
    {
        public record Query(int id) : IRequest<Hero>;

        public class QueryHandler : IRequestHandler<Query, Hero>
        {
            private readonly HeroesDbContext context;

            public QueryHandler(HeroesDbContext context)
            {
                this.context = context;
                context.Database.EnsureCreated();
            }

            public async Task<Hero> Handle(Query request, CancellationToken cancellationToken)
            {
                var data = await context.FindAsync<Hero>(request.id);
            
                // if (data != null)
                //     logger.LogDebug($"Found hero: {data.Id} - {data.Name} to return...");
                // else 
                //     logger.LogWarning($"No hero found for id: {request.id}");

                return data;
            }
        }
    }
}