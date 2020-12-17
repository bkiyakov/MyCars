using MyCars.Core.Exceptions;
using MyCars.Core.Entities;
using MyCars.Core.Repositories.Interfaces;
using MyCars.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public Event Add(Event eventModel, int userId)
        {
            eventModel.UserId = userId;
            eventModel.Created = DateTime.UtcNow;
            eventModel.Modified = DateTime.UtcNow;

            return _eventRepository.Add(eventModel);
        }

        public bool DeleteById(int eventId, int userId)
        {
            var eventModel = _eventRepository.GetById(eventId);

            if (eventModel.UserId == userId)
            {
                return _eventRepository.DeleteById(eventId);
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public IEnumerable<Event> GetAllByUserId(int userId)
        {
            return _eventRepository.GetAllByUserId(userId);
        }

        public Event GetById(int eventId, int userId)
        {
            var eventModel = _eventRepository.GetById(eventId);

            if (eventModel.UserId == userId)
            {
                return eventModel;
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public Event Update(Event eventModel, int userId)
        {
            var eventFromRepo = _eventRepository.GetById(eventModel.EventId);

            if (eventFromRepo != null && eventFromRepo.UserId == userId)
            {
                eventModel.Modified = DateTime.UtcNow;

                return _eventRepository.Update(eventModel);
            }
            else
            {
                throw new NotFoundException();
            }
        }
    }
}
