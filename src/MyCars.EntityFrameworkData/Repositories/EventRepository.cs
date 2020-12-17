using MyCars.Core.Entities;
using MyCars.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCars.EntityFrameworkData.Repositories
{
    public class EventRepository : IEventRepository
    {
        public Event Add(Event model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAllByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Event GetById(int modelId)
        {
            throw new NotImplementedException();
        }

        public Event Update(Event model)
        {
            throw new NotImplementedException();
        }
    }
}
