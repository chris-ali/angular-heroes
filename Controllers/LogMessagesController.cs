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
    public class LogMessagesController : BaseAngularHeroesController
    {
        public LogMessagesController(ILogger<LogMessagesController> logger, HeroesDbContext context) : base(logger, context)
        {
        }

        [HttpGet("{createdBy}")]
        public async Task<ActionResult<IEnumerable<LogMessage>>> GetManyByCreatedBy(string createdBy)
        {
            var data = await context.Messages.Where(x => x.CreatedBy == createdBy).ToListAsync();
            logger.LogDebug($"Found {data.Count} messages to return for user {createdBy}...");

            return data;
        }

        /* [HttpGet("{id}")]
        public async Task<ActionResult<LogMessage>> GetOne(int id)
        {
            var data = await context.FindAsync<LogMessage>(id);
            
            if (data != null)
                logger.LogDebug($"Found message id: {data.id} to return...");
            else 
                logger.LogWarning($"No message found for id: {id}");

            return data;
        } */

        [HttpPost]
        public async Task<ActionResult<LogMessage>> Create(LogMessage message)
        {
            await context.AddAsync<LogMessage>(message);
            await context.SaveChangesAsync();

            logger.LogDebug($"Added new message: {message.Id} - {message.Contents}!");

            return CreatedAtAction(nameof(GetManyByCreatedBy), message.Id, message);
        }

        [HttpDelete("{createdBy}")]
        public async Task<ActionResult<IEnumerable<LogMessage>>> DeleteManyByCreatedBy(string createdBy)
        {
            var data = context.Messages.Where(x => x.CreatedBy == createdBy);

            if (data == null) 
            {
                logger.LogWarning($"No messages found to delete for user: {createdBy}!");

                return NotFound();
            }

            logger.LogDebug($"Found {data.Count()} messages from {createdBy} to delete...");
            
            context.RemoveRange(data);
            await context.SaveChangesAsync();

            logger.LogDebug($"...deleted successfully!");

            return NoContent();
        }
    }
}
