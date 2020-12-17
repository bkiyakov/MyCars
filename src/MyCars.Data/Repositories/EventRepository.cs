using MyCars.Core.Entities;
using MyCars.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCars.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext context;
        public EventRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Event Add(Event model)
        {
            Event addedEvent = context.Events.Add(model);

            if (context.SaveChanges() != 0)
            {
                Event eventFromDb = context.Events.Where(e => e.EventId == addedEvent.EventId)
                    .Include(e => e.EventType)
                    .Include(e => e.Car)
                    .FirstOrDefault();

                return eventFromDb;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteById(int modelId)
        {
            Event eventToRemove = context.Events.Where(e => e.EventId == modelId).FirstOrDefault();

            if (eventToRemove != null)
            {
                context.Events.Remove(eventToRemove);

                return (context.SaveChanges() != 0);
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Event> GetAllByUserId(int userId)
        {
            return context.Events
                .Where(e => e.UserId == userId)
                .Include(e => e.EventType)
                .Include(e => e.Car)
                .ToList();
        }

        public Event GetById(int modelId)
        {
            return context.Events
                .Where(e => e.EventId == modelId)
                .Include(e => e.EventType)
                .Include(e => e.Car)
                .FirstOrDefault();
        }

        public Event Update(Event model)
        {
            Event eventFromDb = context.Events
                .Where(e => e.EventId == model.EventId)
                .Include(e => e.EventType)
                .Include(e => e.Car)
                .FirstOrDefault();

            if (eventFromDb != null)
            {
                eventFromDb.CarId = model.CarId;
                eventFromDb.EventDate = model.EventDate;
                eventFromDb.EventTypeId = model.EventTypeId;
                eventFromDb.Milieage = model.Milieage;
                eventFromDb.Modified = DateTime.UtcNow;
                eventFromDb.Text = model.Text;

                context.SaveChanges();

                return eventFromDb;
            }
            else
            {
                return null;
            }
        }
    }
}
