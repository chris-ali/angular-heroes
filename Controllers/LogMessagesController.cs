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
            var data = await context.Messages.Where(x => x.createdBy == createdBy).ToListAsync();
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

        [HttpPut]
        public async Task<ActionResult<LogMessage>> Update(LogMessage message)
        {
            var data = await context.FindAsync<LogMessage>(message.id);

            if (data == null)
            {
                logger.LogWarning($"No message found for id: {message.id}");

                return NotFound();
            }
             
            logger.LogDebug($"Found message id: {data.id} to update...");
            
            data.contents = message.contents;

            context.Update(data);
            await context.SaveChangesAsync();

            logger.LogDebug($"...updated successfully!");

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<LogMessage>> Create(LogMessage message)
        {
            await context.AddAsync<LogMessage>(message);
            await context.SaveChangesAsync();

            logger.LogDebug($"Added new message: {message.id} - {message.contents}!");

            return CreatedAtAction(nameof(GetManyByCreatedBy), message.id, message);
        }

        /*[HttpDelete("{id}")]
        public async Task<ActionResult<LogMessage>> DeleteOne(int id)
        {
            var data = MockDataBase.messages.Find(x => x.id == message.id);

            if (data == null) 
            {
                logger.LogWarning($"No message found to delete for id: {id}!");

                return NotFound();
            }

            logger.LogDebug($"Found message: {data.id} - {data.contents} to delete...");
            
            context.Remove(data);
            await context.SaveChangesAsync();

            logger.LogDebug($"...deleted successfully!");

            return NoContent();            
        } */

        [HttpDelete("{createdBy}")]
        public async Task<ActionResult<IEnumerable<LogMessage>>> DeleteManyByCreatedBy(string createdBy)
        {
            var data = context.Messages.Where(x => x.createdBy == createdBy);

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
