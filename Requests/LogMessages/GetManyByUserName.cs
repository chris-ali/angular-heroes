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
    public class GetManyByUserName
    {
        public record Query(string userName) : IRequest<IEnumerable<LogMessage>>;

        public class QueryHandler : IRequestHandler<Query, IEnumerable<LogMessage>>
        {
            private readonly HeroesDbContext context;

            public QueryHandler(HeroesDbContext context)
            {
                this.context = context;
                context.Database.EnsureCreated();
            }

            public async Task<IEnumerable<LogMessage>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.userName))
                {
                    var message = $"Must provide userName";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.BadRequest, new { Message = message});
                }
                
                var data = await context.Messages
                    .Include(x => x.Owner)
                    .Where(x => x.Owner.UserName == request.userName)
                    .ToListAsync();

                // logger.LogDebug($"Found {data.Count} messages to return for user {request.userName}...");

                return data;
            }
        }
    }
}