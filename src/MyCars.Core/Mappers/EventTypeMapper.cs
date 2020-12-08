using MyCars.Core.Entities;
using MyCars.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Mappers
{
    public static class EventTypeMapper
    {
        public static EventType ToDomain(this EventTypeEntity eventTypeEntity)
        {
            return new EventType()
            {
                EventTypeId = eventTypeEntity.EventTypeId,
                EventTypeName =  eventTypeEntity.EventTypeName
            };
        }
    }
}
