using angular_heroes.Models;
using angular_heroes.Requests.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace angular_heroes.Controllers
{
  public class UsersController : BaseAngularHeroesController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<User>> Get(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCurrent.Query(), cancellationToken);
        }

        [HttpPut]
        public async Task<ActionResult<User>> Update(User user, CancellationToken cancellationToken)
        {
            return await mediator.Send(new Update.Command(user), cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user, CancellationToken cancellationToken)
        {
            return await mediator.Send(new Create.Command(user), cancellationToken);
        }
    }
}
