using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using angular_heroes.Infrastructure;
using angular_heroes.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Requests.Heroes 
{
    public class GetAll
    {
        public record Query() : IRequest<IEnumerable<Hero>>;

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Hero>>
        {
            private readonly HeroesDbContext context;

            public QueryHandler(HeroesDbContext context)
            {
                this.context = context;
                context.Database.EnsureCreated();
            }

            public async Task<IEnumerable<Hero>> Handle(Query request, CancellationToken cancellationToken)
            {
                var data = await context.Heroes.ToListAsync();
                // logger.LogDebug($"Found {data.Count} heroes to return...");

                return data;
            }
        }
    }
}