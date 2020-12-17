using MyCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCars.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DBConnection")
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Event> Events { get; set; }
        public EventType EventTypes { get; set; }
    }
}
