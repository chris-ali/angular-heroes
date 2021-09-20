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
    public class LogMessagesController : BaseAngularHeroesController
    {
        private static LogMessage[] messages = new[]
        {};

        public LogMessagesController(ILogger<LogMessagesController> logger) : base(logger)
        {}

        [HttpGet]
        public IEnumerable<LogMessage> GetManyByCreatedBy(string createdBy)
        {

        }

        [HttpGet]
        public IEnumerable<LogMessage> GetOne(int id)
        {

        }

        [HttpPut]
        public IEnumerable<LogMessage> Update(LogMessage hero)
        {

        }

        [HttpPost]
        public IEnumerable<LogMessage> Create()
        {

        }

        [HttpDelete]
        public IEnumerable<LogMessage> DeleteOne(int id)
        {

        }

        [HttpDelete]
        public IEnumerable<LogMessage> DeleteManyByCreatedBy(string createdBy)
        {

        }
    }
}
