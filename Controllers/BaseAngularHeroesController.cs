using angular_heroes.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseAngularHeroesController : ControllerBase
    {
        protected readonly HeroesDbContext context;
        protected readonly ILogger<BaseAngularHeroesController> logger;

        public BaseAngularHeroesController(ILogger<BaseAngularHeroesController> logger, HeroesDbContext context)
        {
            this.logger = logger;
            this.context = context;

            context.Database.EnsureCreated();
        }
    }
}
