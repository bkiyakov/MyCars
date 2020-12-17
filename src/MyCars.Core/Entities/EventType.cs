using System;

namespace MyCars.Core.Entities
{
    public class EventType
    {
        public int EventTypeId { get; set; }
        public string EventTypeName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}