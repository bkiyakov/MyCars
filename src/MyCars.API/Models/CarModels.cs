using MyCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCars.API.Models
{
    public class CarGetAllResponseModel
    {
        public List<CarGetResponseModel> CarList { get; private set; }
        public CarGetAllResponseModel(IEnumerable<Car> cars)
        {
            CarList = new List<CarGetResponseModel>();

            if (cars != null && cars.Count() > 0)
            {
                foreach (var car in cars)
                {
                    CarList.Add(new CarGetResponseModel(car));
                }
            }
        }
    }

    public class CarGetResponseModel
    {
        public int CarId { get; private set; }
        public string CarName { get; private set; }
        public string Brand { get; private set; }
        public DateTime IssueYear { get; private set; }
        public string VIN { get; private set; }
        public string Numberplate { get; private set; }
        //public int UserId { get; set; }
        //public DateTime Created { get; set; }
        //public DateTime Modified { get; set; }

        public CarGetResponseModel(Car car)
        {
            if (car != null)
            {
                CarId = car.CarId;
                CarName = car.CarName;
                Brand = car.Brand;
                IssueYear = car.IssueYear;
                VIN = car.VIN;
                Numberplate = car.Numberplate;
            } else
            {
                throw new ArgumentNullException();
            }
        }

    }
}