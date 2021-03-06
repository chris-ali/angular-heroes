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

namespace angular_heroes.Requests.Heroes 
{
    public class GetManyByUserName
    {
        public record Query(string userName) : IRequest<IList<Hero>>;

        public class QueryHandler : BaseRequest, IRequestHandler<Query, IList<Hero>>
        {
            public QueryHandler(HeroesDbContext context) : base(context)
            {
            }

            public async Task<IList<Hero>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.userName))
                {
                    var message = $"Must provide userName";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.BadRequest, new { Message = message});
                }
                
                var data = await context.Heroes
                    .Include(x => x.Users)
                    .Where(x => x.Users
                    .Any(x => x.UserName == request.userName))
                    .ToListAsync(cancellationToken);
                
                if (data.Count == 0)
                {
                    var message = $"No heroes found to delete for user: {request.userName}";
                    // logger.LogWarning(message);
                    throw new RestException(HttpStatusCode.NotFound, new { Message = message});
                }
                
                // logger.LogDebug($"Found {data.Count} heroes to return for user {request.userName}...");

                return data;
            }
        }
    }
}