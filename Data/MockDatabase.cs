using System;
using System.Collections.Generic;
using System.Linq;
using angular_heroes.Models;

namespace angular_heroes.Data
{
    public static class MockDataBase
    {
        public static List<Hero> heroes = new List<Hero>
        {
            new Hero { id = 11, name = "Dr Nice", createdBy = "chris", createdDate = DateTime.Now },
            new Hero { id = 12, name = "Narco", createdBy = "chris", createdDate = DateTime.Now },
            new Hero { id = 13, name = "Bombasto", createdBy = "chris", createdDate = DateTime.Now },
            new Hero { id = 14, name = "Celeritas", createdBy = "chris", createdDate = DateTime.Now },
            new Hero { id = 15, name = "Magneta", createdBy = "chris", createdDate = DateTime.Now },
            new Hero { id = 16, name = "RubberMan", createdBy = "chris", createdDate = DateTime.Now },
            new Hero { id = 17, name = "Dynama", createdBy = "chris", createdDate = DateTime.Now },
            new Hero { id = 18, name = "Dr IQ", createdBy = "chris", createdDate = DateTime.Now },
            new Hero { id = 19, name = "Magma", createdBy = "chris", createdDate = DateTime.Now },
            new Hero { id = 20, name = "Tornado", createdBy = "chris", createdDate = DateTime.Now }
        };

        public static List<LogMessage> messages = new List<LogMessage> {
            new LogMessage { id = 5, contents = "Test Log Message", createdBy = "chris", createdDate = DateTime.Now }
        };

        public static int getNextHeroId() {
            return heroes.Count > 0 ? (heroes.Max(x => x.id) + 1) : 11;
        }

        public static int getNextMessageId() {
            return messages.Count > 0 ? (messages.Max(x => x.id) + 1) : 11;
        }
    }
}