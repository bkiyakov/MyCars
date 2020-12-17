using MyCars.Core.Entities;
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
                EventTypeName = eventModel.EventType.EventTypeName;
                CarGetResponseModel = new CarGetResponseModel(eventModel.Car);
                Milieage = eventModel.Milieage;
                Text = eventModel.Text;
            } else
            {
                throw new ArgumentNullException();
            }
        }
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTypeName { get; set; }
        public CarGetResponseModel CarGetResponseModel { get; set; }
        public int Milieage { get; set; }
        public string Text { get; set; }
    }

    public class EventAddRequestModel
    {
        public DateTime EventDate { get; set; }
        public int EventTypeId { get; set; }
        public int CarId { get; set; }
        public int Milieage { get; set; }
        public string Text { get; set; }
    }

}