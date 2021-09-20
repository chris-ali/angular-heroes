using System;

namespace angular_heroes.Models
{
    public class BaseEntity
    {
        public string id { get; set; }

        public string createdBy { get; set; }

        public DateTime createdDate { get; set; }
    }
}