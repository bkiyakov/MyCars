using MyCars.API.Models;
using MyCars.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyCars.API.Controllers
{
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
    }
}
