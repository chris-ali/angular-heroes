using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace angular_heroes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseAngularHeroesController : ControllerBase
    {
        protected readonly IMediator mediator;

        public BaseAngularHeroesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
