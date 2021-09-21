using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angular_heroes.Data;
using angular_heroes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Controllers
{
    public class HeroesController : BaseAngularHeroesController
    {
        public HeroesController(ILogger<HeroesController> logger) : base(logger)
        {}

        [HttpGet]
        public ActionResult<IEnumerable<Hero>> GetAll()
        {
            var data = MockDataBase.heroes;
            logger.LogDebug($"Found {data.Count} heroes to return...");

            return data;
        }

        /* [HttpGet("{createdBy}")]
        public ActionResult<IEnumerable<Hero>> GetManyByCreatedBy(string createdBy)
        {
            var data = MockDataBase.heroes.FindAll(x => x.createdBy == createdBy);
            logger.LogDebug($"Found {data.Count} heroes to return for user {createdBy}...");

            return data;
        } */

        [HttpGet("{id}")]
        public ActionResult<Hero> GetOne(int id)
        {
            var data = MockDataBase.heroes.Find(x => x.id == id);
            
            if (data != null)
                logger.LogDebug($"Found hero: {data.id} - {data.name} to return...");
            else 
                logger.LogWarning($"No hero found for id: {id}");

            return data;
        }

        [HttpPut]
        public ActionResult<Hero> Update(Hero hero)
        {
            var data = MockDataBase.heroes.Find(x => x.id == hero.id);

            if (data != null) 
            {
                logger.LogDebug($"Found hero: {data.id} - {data.name} to update...");
                MockDataBase.heroes.Remove(data);
                MockDataBase.heroes.Add(hero);
                logger.LogDebug($"...updated successfully!");

                return NoContent();
            }
            else 
            {
                logger.LogWarning($"No hero found for id: {hero.id}");

                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<Hero> Create(Hero hero)
        {
            hero.id = MockDataBase.getNextHeroId();
            logger.LogDebug($"New hero id is: {hero.id}");

            MockDataBase.heroes.Add(hero);
            logger.LogDebug($"Added new hero: {hero.id} - {hero.name}!");

            return CreatedAtAction(nameof(GetOne), hero.id, hero);
        }

        [HttpDelete("{id}")]
        public ActionResult<Hero> Delete(int id)
        {
            var data = MockDataBase.heroes.Find(x => x.id == id);

            if (data != null) 
            {
                logger.LogDebug($"Found hero: {data.id} - {data.name} to delete...");
                MockDataBase.heroes.Remove(data);
                logger.LogDebug($"...deleted successfully!");

                return NoContent();
            }
            else 
            {
                logger.LogWarning($"No hero found to delete for id: {id}!");

                return NotFound();
            }
        }
    }
}
