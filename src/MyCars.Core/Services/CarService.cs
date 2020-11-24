using MyCars.Core.Models;
using MyCars.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Services
{
    public class CarService : ICarService
    {
        public Car Get(int id)
        {
            return new Car
            {
                CarId = 1,
                CarName = "Моя первая машина",
                Brand = "Nissan Qashqai",
                IssueYear = new DateTime(2009, 1, 1),
                VIN = "AHDN29ADXGP2",
                Numberplate = "М329ОР"
            };
        }

        public IEnumerable<Car> GetAll()
        {
            var cars = new List<Car>
            {
                new Car
                {
                    CarId = 1,
                    CarName = "Моя первая машина",
                    Brand = "Nissan Qashqai",
                    IssueYear = new DateTime(2009, 1, 1),
                    VIN = "AHDN29ADXGP2",
                    Numberplate = "М329ОР"
                },
                new Car
                {
                    CarId = 2,
                    CarName = "Жигуль",
                    Brand = "LADA 2105",
                    IssueYear = new DateTime(2001, 1, 1),
                    VIN = "XGDW31ATIYP1",
                    Numberplate = "РО450Т"
                }
            };

            return cars;
        }
    }
}
