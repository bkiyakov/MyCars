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
    public class CarController : ApiController
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var cars = _carService.GetAll();

            return Json(new GetAllResponseModel(cars));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Json(_carService.Get(id));
        }
    }
}
