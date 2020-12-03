using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Entities
{
    public abstract class BaseEntity
    {
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
