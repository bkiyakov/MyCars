using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCars.Core.Repositories.Interfaces;
using MyCars.Core.Models;
using MyCars.Core.Entities;
using MyCars.Core.Services;
using MyCars.Core.Services.Interfaces;
using MyCars.Core.Exceptions;
using Moq;

namespace MyCars.Tests
{
    [TestClass]
    public class CarServiceTest
    {
        private readonly ICarRepository MockCarRepository;
        private readonly ICarService carService;

        public CarServiceTest()
        {
            // create some mock cars to play with
            IList<CarEntity> carList = new List<CarEntity>
            {
                new CarEntity
                    {
                        CarId = 1,
                        CarName = "Моя первая машина",
                        Brand = "Nissan Qashqai",
                        IssueYear = new DateTime(2009, 1, 1),
                        VIN = "AHDN29ADXGP2",
                        Numberplate = "М329ОР",
                        UserId = 1,
                        Created = new DateTime(2020, 10, 6, 12, 0, 35),
                        Modified = new DateTime(2020, 10, 6, 12, 0, 35)
                    },
                    new CarEntity
                    {
                        CarId = 2,
                        CarName = "Жигуль",
                        Brand = "LADA 2105",
                        IssueYear = new DateTime(2001, 1, 1),
                        VIN = "XGDW31ATIYP1",
                        Numberplate = "РО450Т",
                        UserId = 1,
                        Created = new DateTime(2020, 11, 14, 10, 12, 23),
                        Modified = new DateTime(2020, 11, 14, 12, 53, 14)
                    },
                    new CarEntity
                    {
                        CarId = 3,
                        CarName = "Honda",
                        Brand = "Honda Civic",
                        IssueYear = new DateTime(2010, 1, 1),
                        VIN = "VGDD12ATNYP4",
                        Numberplate = "ТВ421Р",
                        UserId = 2,
                        Created = new DateTime(2020, 11, 23, 22, 10, 5),
                        Modified = new DateTime(2020, 11, 28, 16, 40, 24)
                    }
            };

            // Mock the Cars Repository using Moq
            Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();

            // Return all the cars
            mockCarRepository.Setup(mr => mr.GetAllByUserId(It.IsAny<int>()))
                .Returns((int userId) => from CarEntity carEntity in carList
                                         where carEntity.UserId == userId
                                         select carEntity.ToDomain());

            // Return car by id
            mockCarRepository.Setup(mr => mr.GetById(It.IsAny<int>()))
                .Returns((int carId) => carList.Where(c => c.CarId == carId)
                .FirstOrDefault()?
                .ToDomain());

            // Add car
            mockCarRepository.Setup(mr => mr.Add(It.IsAny<Car>()))
                .Returns((Car car) =>
                {
                    int newId = carList.Count + 1;

                    CarEntity newCarEntity = new CarEntity
                    {
                        CarId = newId,
                        CarName = car.CarName,
                        Brand = car.Brand,
                        IssueYear = car.IssueYear,
                        VIN = car.VIN,
                        Numberplate = car.Numberplate,
                        UserId = car.UserId,
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    };

                    carList.Add(newCarEntity);

                    return carList.Where(c => c.CarId == newId).FirstOrDefault().ToDomain();
                });

            // TODO Delete car by id
            // TODO Update car

            MockCarRepository = mockCarRepository.Object;
            carService = new CarService(MockCarRepository);
        }

        [TestMethod]
        public void CanReturnAllUsersCars()
        {
            IList<Car> carsFirstUser = carService.GetAllByUserId(1).ToList();
            IList<Car> carsSecondUser = carService.GetAllByUserId(2).ToList();

            Assert.IsNotNull(carsFirstUser);
            Assert.AreEqual(2, carsFirstUser.Count);

            Assert.IsNotNull(carsSecondUser);
            Assert.AreEqual(1, carsSecondUser.Count);
        }

        [TestMethod]
        public void CanReturnCarByIdWithAllowedUser()
        {
            Car car = carService.GetById(1, 1);

            Assert.IsNotNull(car);
            Assert.IsInstanceOfType(car, typeof(Car));
            Assert.AreEqual(1, car.CarId);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CanNotReturnCarByIdWithNotAllowedUser()
        {
            Car car = carService.GetById(3, 1);
        }
    }
}
