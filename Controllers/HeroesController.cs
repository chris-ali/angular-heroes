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
    public class HeroesController : BaseAngularHeroesController
    {
        private readonly HeroContext context;

        public HeroesController(ILogger<HeroesController> logger, HeroContext context) : base(logger)
        {
            this.context = context;
            context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetAll()
        {
            var data = await context.Heroes.ToListAsync();
            logger.LogDebug($"Found {data.Count} heroes to return...");

            return data;
        }

        /* [HttpGet("{createdBy}")]
        public async Task<ActionResult<IEnumerable<Hero>>> GetManyByCreatedBy(string createdBy)
        {
            var data = context.Heros.Where(x => x.createdBy == createdBy).ToListAsync();
            logger.LogDebug($"Found {data.Count} heroes to return for user {createdBy}...");

            return data;
        } */

        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetOne(int id)
        {
            var data = await context.FindAsync<Hero>(id);
            
            if (data != null)
                logger.LogDebug($"Found hero: {data.id} - {data.name} to return...");
            else 
                logger.LogWarning($"No hero found for id: {id}");

            return data;
        }

        [HttpPut]
        public async Task<ActionResult<Hero>> Update(Hero hero)
        {
            var data = await context.FindAsync<Hero>(hero.id);

            if (data == null) 
            {
                logger.LogWarning($"No hero found for id: {hero.id}");

                return NotFound();
            }
            
            logger.LogDebug($"Found hero: {data.id} - {data.name} to update...");
            
            context.Update<Hero>(hero);
            await context.SaveChangesAsync();

            logger.LogDebug($"...updated successfully!");

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Hero>> Create(Hero hero)
        {
            await context.AddAsync<Hero>(hero);
            await context.SaveChangesAsync();

            logger.LogDebug($"Added new hero: {hero.id} - {hero.name}!");

            return CreatedAtAction(nameof(GetOne), hero.id, hero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Hero>> Delete(int id)
        {
            var data = await context.FindAsync<Hero>(id);

            if (data == null) 
            {
                logger.LogWarning($"No hero found to delete for id: {id}!");

                return NotFound();
            }
            
            logger.LogDebug($"Found hero: {data.id} - {data.name} to delete...");

            context.Remove(data);
            await context.SaveChangesAsync();
            
            logger.LogDebug($"...deleted successfully!");

            return NoContent();
        }
    }
}
