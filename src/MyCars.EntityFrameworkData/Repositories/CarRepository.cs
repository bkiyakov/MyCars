using MyCars.Core.Models;
using MyCars.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCars.EntityFrameworkData.Repositories
{
    public class CarRepository : ICarRepository
    {
        public Car GetById(int carId)
        {
            return new Car
            {
                CarId = carId,
                CarName = "Моя первая машина",
                Brand = "Nissan Qashqai",
                IssueYear = new DateTime(2009, 1, 1),
                VIN = "AHDN29ADXGP2",
                Numberplate = "М329ОР",
                UserId = 1
            };
        }

        public IEnumerable<Car> GetAllByUserId(int userId)
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
                    Numberplate = "М329ОР",
                    UserId = 1
                },
                new Car
                {
                    CarId = 2,
                    CarName = "Жигуль",
                    Brand = "LADA 2105",
                    IssueYear = new DateTime(2001, 1, 1),
                    VIN = "XGDW31ATIYP1",
                    Numberplate = "РО450Т",
                    UserId = 1
                }
            };

            return cars;
        }

        public Car Add(Car model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int modelId)
        {
            throw new NotImplementedException();
        }

        public Car Update(Car model)
        {
            throw new NotImplementedException();
        }
    }
}
