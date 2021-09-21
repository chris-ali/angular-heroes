using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseAngularHeroesController : ControllerBase
    {
        protected readonly ILogger<BaseAngularHeroesController> logger;

        public BaseAngularHeroesController(ILogger<BaseAngularHeroesController> logger)
        {
            this.logger = logger;
        }
    }
}
