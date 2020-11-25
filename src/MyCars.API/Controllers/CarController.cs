using MyCars.API.Models;
using MyCars.Core.Models;
using MyCars.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyCars.API.Controllers
{
    [RoutePrefix("api/Car")]
    public class CarController : ApiController
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var userId = 1; // TODO take a real id

            var cars = _carService.GetAllByUserId(userId);

            return Json(new GetAllResponseModel(cars));
        }

        [HttpGet]
        [Route("Get/{id}")]
        public IHttpActionResult Get(int id)
        {
            var userId = 1; // TODO take a real id

            var car = _carService.GetById(id, userId);

            return Json(new GetResponseModel(car));
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody] AddRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = 1;  // TODO take a real id

            Car car = new Car
            {
                CarName = model.CarName,
                IssueYear = model.IssueYear,
                VIN = model.VIN,
                Brand = model.Brand,
                Numberplate = model.Numberplate
            };

            try
            {
                var addedCar = _carService.Add(car, userId);

                return Created($"api/Car/Get/{addedCar.CarId}",new GetResponseModel(addedCar));
            } catch (Exception ex)
            {
                return new ExceptionResult(ex.Message);
            }
        }
    }
}
