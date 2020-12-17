using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Entities
{
    public class Car : BaseEntity
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string Brand { get; set; }
        public DateTime IssueYear { get; set; }
        public string VIN { get; set; }
        public string Numberplate { get; set; }
    }
}
