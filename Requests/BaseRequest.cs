using angular_heroes.Infrastructure;

namespace angular_heroes.Requests
{
    public class BaseRequest
    {
        protected readonly HeroesDbContext context;

        public BaseRequest(HeroesDbContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
        }
    }
}