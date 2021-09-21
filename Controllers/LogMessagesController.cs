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
    public class LogMessagesController : BaseAngularHeroesController
    {
        public LogMessagesController(ILogger<LogMessagesController> logger) : base(logger)
        {}

        [HttpGet("{createdBy}")]
        public ActionResult<IEnumerable<LogMessage>> GetManyByCreatedBy(string createdBy)
        {
            var data = MockDataBase.messages.FindAll(x => x.createdBy == createdBy);
            logger.LogDebug($"Found {data.Count} messages to return for user {createdBy}...");

            return data;
        }

        /* [HttpGet("{id}")]
        public ActionResult<LogMessage> GetOne(int id)
        {
            var data = MockDataBase.messages.Find(x => x.id == id);
            
            if (data != null)
                logger.LogDebug($"Found message id: {data.id} to return...");
            else 
                logger.LogWarning($"No message found for id: {id}");

            return data;
        } */

        [HttpPut]
        public ActionResult<LogMessage> Update(LogMessage message)
        {
            var data = MockDataBase.messages.Find(x => x.id == message.id);

            if (data != null) 
            {
                logger.LogDebug($"Found message id: {data.id} to update...");
                MockDataBase.messages.Remove(data);
                MockDataBase.messages.Add(message);
                logger.LogDebug($"...updated successfully!");

                return NoContent();
            }
            else 
            {
                logger.LogWarning($"No message found for id: {message.id}");

                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<LogMessage> Create(LogMessage message)
        {
            message.id = MockDataBase.getNextHeroId();
            logger.LogDebug($"New message id is: {message.id}");

            MockDataBase.messages.Add(message);
            logger.LogDebug($"Added new message: {message.id} - {message.contents}!");

            return CreatedAtAction(nameof(GetManyByCreatedBy), message.id, message);
        }

        /*[HttpDelete("{id}")]
        public ActionResult<LogMessage> DeleteOne(int id)
        {
            var data = MockDataBase.messages.Find(x => x.id == id);

            if (data != null) 
            {
                logger.LogDebug($"Found message: {data.id} - {data.contents} to delete...");
                MockDataBase.messages.Remove(data);
                logger.LogDebug($"...deleted successfully!");

                return NoContent();
            }
            else 
            {
                logger.LogWarning($"No message found to delete for id: {id}!");

                return NotFound();
            }
        } */

        [HttpDelete("{createdBy}")]
        public ActionResult<IEnumerable<LogMessage>> DeleteManyByCreatedBy(string createdBy)
        {
            var data = MockDataBase.messages.FindAll(x => x.createdBy == createdBy);

            if (data != null) 
            {
                logger.LogDebug($"Found {data.Count} messages from {createdBy} to delete...");
                MockDataBase.messages.RemoveAll(x => x.createdBy == createdBy);
                logger.LogDebug($"...deleted successfully!");

                return NoContent();
            }
            else 
            {
                logger.LogWarning($"No messages found to delete for user: {createdBy}!");

                return NotFound();
            }
        }
    }
}
