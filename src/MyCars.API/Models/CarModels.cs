using MyCars.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCars.API.Models
{
    public class GetAllResponseModel
    {
        public List<GetResponseModel> CarList { get; set; }
        public GetAllResponseModel()
        {

        }
        public GetAllResponseModel(IEnumerable<Car> cars)
        {
            CarList = new List<GetResponseModel>();

            foreach (var car in cars)
            {
                CarList.Add(new GetResponseModel(car));
            }
        }
    }

    public class GetResponseModel
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string Brand { get; set; }
        public DateTime IssueYear { get; set; }
        public string VIN { get; set; }
        public string Numberplate { get; set; }
        public GetResponseModel()
        {

        }

        public GetResponseModel(Car car)
        {
            CarId = car.CarId;
            CarName = car.CarName;
            Brand = car.Brand;
            IssueYear = car.IssueYear;
            VIN = car.VIN;
            Numberplate = car.Numberplate;
        }
        //public int UserId { get; set; }
        //public DateTimeOffset Created { get; set; }
        //public DateTimeOffset Modified { get; set; }

    }
}