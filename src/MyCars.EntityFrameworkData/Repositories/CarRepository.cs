using MyCars.Core.Exceptions;
using MyCars.Core.Entities;
using MyCars.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCars.TestRepositories.Repositories
{
    public class CarRepository : ICarRepository
    {
        private List<Car> carList = new List<Car>
        {
            new Car
                {
                    CarId = 1,
                    CarName = "Моя первая машина",
                    Brand = "Nissan Qashqai",
                    IssueYear = new DateTime(2009, 1, 1),
                    VIN = "AHDN29ADXGP2",
                    Numberplate = "М329ОР",
                    UserId = 1,
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                },
                new Car
                {
                    CarId = 2,
                    CarName = "Жигуль",
                    Brand = "LADA 2105",
                    IssueYear = new DateTime(2001, 1, 1),
                    VIN = "XGDW31ATIYP1",
                    Numberplate = "РО450Т",
                    UserId = 1,
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                },
                new Car
                {
                    CarId = 3,
                    CarName = "Honda",
                    Brand = "Honda Civic",
                    IssueYear = new DateTime(2010, 1, 1),
                    VIN = "VGDD12ATNYP4",
                    Numberplate = "ТВ421Р",
                    UserId = 2,
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                }
        };

        public Car GetById(int carId)
        {
            var carDb = carList.Where<Car>(car => car.CarId == carId).FirstOrDefault();

            if (carDb != null)
            {
                return carDb;
            } else
            {
                throw new NotFoundException();
            }
        }

        public IEnumerable<Car> GetAllByUserId(int userId)
        {
            return carList.Where<Car>(car => car.UserId == userId).ToList();
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
