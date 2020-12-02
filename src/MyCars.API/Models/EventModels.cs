using MyCars.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCars.API.Models
{
    public class EventGetAllResponseModel
    {
        public List<EventGetResponseModel> EventList { get; private set; }
        public EventGetAllResponseModel(IEnumerable<Event> events)
        {
            EventList = new List<EventGetResponseModel>();

            if (events != null && events.Count() > 0)
            {
                foreach (var eventModel in events)
                {
                    EventList.Add(new EventGetResponseModel(eventModel));
                }
            }
        }
    }

    public class EventGetResponseModel
    {
        public EventGetResponseModel(Event eventModel)
        {
            if (eventModel != null)
            {
                EventId = eventModel.EventId;
                EventDate = eventModel.EventDate;
                EventTypeId = eventModel.EventType.EventTypeId;
                CarId = eventModel.Car.CarId;
                Milieage = eventModel.Milieage;
                Text = eventModel.Text;
            }
        }
        public int EventId { get; private set; }
        public DateTime EventDate { get; private set; }
        public int EventTypeId { get; private set; }
        public int CarId { get; private set; }
        public int Milieage { get; private set; }
        public string Text { get; private set; }
    }
}