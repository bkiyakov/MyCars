using MyCars.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCars.API.Models
{
    public class GetAllResponseModel
    {
        public List<GetResponseModel> CarList { get; private set; }
        public GetAllResponseModel(IEnumerable<Car> cars)
        {
            CarList = new List<GetResponseModel>();

            if (cars != null && cars.Count() > 0)
            {
                foreach (var car in cars)
                {
                    CarList.Add(new GetResponseModel(car));
                }
            }
        }
    }

    public class GetResponseModel
    {
        public int CarId { get; private set; }
        public string CarName { get; private set; }
        public string Brand { get; private set; }
        public DateTime IssueYear { get; private set; }
        public string VIN { get; private set; }
        public string Numberplate { get; private set; }
        //public int UserId { get; set; }
        //public DateTimeOffset Created { get; set; }
        //public DateTimeOffset Modified { get; set; }

        public GetResponseModel(Car car)
        {
            if (car != null)
            {
                CarId = car.CarId;
                CarName = car.CarName;
                Brand = car.Brand;
                IssueYear = car.IssueYear;
                VIN = car.VIN;
                Numberplate = car.Numberplate;
            }
        }

    }
}