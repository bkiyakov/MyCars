using MyCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Services.Interfaces
{
    public interface IEventService
    {
        IEnumerable<Event> GetAllByUserId(int userId);
        Event GetById(int eventId, int userId);
        Event Add(Event eventModel, int userId);
        bool DeleteById(int eventId, int userId);
        Event Update(Event eventModel, int userId);
    }
}
