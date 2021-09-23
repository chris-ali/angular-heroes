using angular_heroes.Data;
using angular_heroes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angular_heroes.Controllers
{
    public class UsersController : BaseAngularHeroesController
    {
        public UsersController(ILogger<UsersController> logger, HeroesDbContext context) : base(logger, context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<User>> Get(string userName)
        {
            var data = await context.Users.Include(x => x.Heroes).Where<User>(x => x.UserName == userName)
                            .FirstOrDefaultAsync();
            
            if (data != null)
                logger.LogDebug($"Found user: {data.UserName} to return...");
            else 
                logger.LogWarning($"No message found for userName: {data.UserName}");

            return data;
        }

        [HttpPut]
        public async Task<ActionResult<User>> Update(User user)
        {
            var data = await context.FindAsync<User>(user.Id);

            if (data == null)
            {
                logger.LogWarning($"No message found for id: {user.Id}");

                return NotFound();
            }
             
            logger.LogDebug($"Found message id: {data.Id} to update...");
            
            data.FirstName = user.FirstName;
            data.LastName = user.LastName;
            data.Email = user.Email;
            data.UserName = user.UserName;

            context.Update(data);
            await context.SaveChangesAsync();

            logger.LogDebug($"...updated successfully!");

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await context.AddAsync<User>(user);
            await context.SaveChangesAsync();

            logger.LogDebug($"Added new user: {user.Id} - {user.UserName}!");

            return CreatedAtAction(nameof(Get), user.Id, user);
        }
    }
}
