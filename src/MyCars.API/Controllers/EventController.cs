using MyCars.API.Models;
using MyCars.Core.Exceptions;
using MyCars.Core.Entities;
using MyCars.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyCars.API.Controllers
{
    [RoutePrefix("api/Event")]
    public class EventController : ApiController
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var userId = 1; // TODO take a real id

            var events = _eventService.GetAllByUserId(userId);

            return Json(new EventGetAllResponseModel(events));
        }

        [HttpGet]
        [Route("Get/{id}")]
        public IHttpActionResult Get(int id)
        {
            var userId = 1; // TODO take a real id

            var eventModel = _eventService.GetById(id, userId);

            return Json(new EventGetResponseModel(eventModel));
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody] EventAddRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = 1;  // TODO take a real id

            Event eventModel = new Event
            {
                EventDate = model.EventDate,
                EventTypeId = model.EventTypeId,
                CarId = model.CarId,
                Milieage = model.Milieage,
                Text = model.Text
            };

            var addedEvent = _eventService.Add(eventModel, userId);

            return Created($"api/Event/Get/{addedEvent.EventId}", new EventGetResponseModel(addedEvent));
        }

        [HttpPost]
        [Route("Update/{id}")]
        public IHttpActionResult Update(int id, [FromBody] EventAddRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = 1;  // TODO take a real id

            Event eventModel = new Event
            {
                EventId = id,
                EventDate = model.EventDate,
                EventTypeId = model.EventTypeId,
                CarId = model.CarId,
                Milieage = model.Milieage,
                Text = model.Text
            };

            return Ok(_eventService.Update(eventModel, userId));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var userId = 1; //  TODO take a real id

            return _eventService.DeleteById(id, userId) ? Ok() : throw new CommonException();
        }
    }
}
