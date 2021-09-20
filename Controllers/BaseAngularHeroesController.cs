using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Controllers
{
    public class BaseAngularHeroesController : ControllerBase
    {
        private readonly ILogger<BaseAngularHeroesController> _logger;

        public BaseAngularHeroesController(ILogger<BaseAngularHeroesController> logger)
        {
            _logger = logger;
        }
    }
}
