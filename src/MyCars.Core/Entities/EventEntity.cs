using MyCars.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Entities
{
    public class EventEntity : BaseEntity
    {
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public int EventTypeId { get; set; }
        public virtual EventTypeEntity EventType { get; set; }
        public int CarId { get; set; }
        public virtual CarEntity Car { get; set; }
        public int Milieage { get; set; }
        public string Text { get; set; }
    }
}
