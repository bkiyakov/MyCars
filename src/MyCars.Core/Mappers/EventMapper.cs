using MyCars.Core.Entities;
using MyCars.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Mappers
{
    public static class EventMapper
    {
        public static Event ToDomain(this EventEntity eventEntity)
        {
            return new Event()
            {
                EventId = eventEntity.EventId,
                EventDate = eventEntity.EventDate,
                EventType = eventEntity.EventType.ToDomain(),
                Car = eventEntity.Car.ToDomain(),
                Milieage = eventEntity.Milieage,
                Text = eventEntity.Text,
                UserId = eventEntity.UserId
            };
        }
    }
}
