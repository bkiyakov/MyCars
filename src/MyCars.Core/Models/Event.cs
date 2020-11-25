using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public EventType EventType { get; set; }
        public Car Car { get; set; }
        public int Milieage { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
    }
}
