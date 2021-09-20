using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angular_heroes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace angular_heroes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroesController : BaseAngularHeroesController
    {
        private static Hero[] heroes = new[]
        {};

        public HeroesController(ILogger<HeroesController> logger) : base(logger)
        {}

        [HttpGet]
        public IEnumerable<Hero> GetAll()
        {

        }

        [HttpGet]
        public IEnumerable<Hero> GetManyByCreatedBy(string createdBy)
        {

        }

        [HttpGet]
        public IEnumerable<Hero> GetOne(int id)
        {

        }

        [HttpPut]
        public IEnumerable<Hero> Update(Hero hero)
        {

        }

        [HttpPost]
        public IEnumerable<Hero> Create()
        {

        }

        [HttpDelete]
        public IEnumerable<Hero> Delete(int id)
        {

        }
    }
}
