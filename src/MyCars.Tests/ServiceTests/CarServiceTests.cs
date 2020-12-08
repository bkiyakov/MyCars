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
using MyCars.Core.Mappers;

namespace MyCars.Tests.ServiceTests
{
    [TestClass]
    public class CarServiceTests
    {
        private readonly ICarRepository _mockCarRepository;
        private readonly ICarService carService;
        private readonly IList<CarEntity> carList;

        public CarServiceTests()
        {
            // create some mock cars to play with
            carList = new List<CarEntity>
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

            // Delete car by id
            mockCarRepository.Setup(mr => mr.DeleteById(It.IsAny<int>()))
                .Returns((int carId) =>
                {
                    var car = carList.Where(carFromList => carFromList.CarId == carId).FirstOrDefault();

                    if (car != null)
                    {
                        return carList.Remove(car);
                    }
                    else
                    {
                        return false; // Нужно выбросить исключение NotFound, но это Moq
                    }
                });

            // Update car
            mockCarRepository.Setup(mr => mr.Update(It.IsAny<Car>()))
                .Returns((Car newCar) =>
                {
                    carList.Where(car => car.CarId == newCar.CarId)
                        .Select(c =>
                        {
                            c.CarName = newCar.CarName;
                            c.Brand = newCar.Brand;
                            c.IssueYear = newCar.IssueYear;
                            c.VIN = newCar.VIN;
                            c.Numberplate = newCar.Numberplate;
                            c.UserId = newCar.UserId;
                            c.Modified = DateTime.Now;

                            return c;
                        }).ToList();

                    return carList.FirstOrDefault(c => c.CarId == newCar.CarId)?.ToDomain();
                });

            _mockCarRepository = mockCarRepository.Object;
            carService = new CarService(_mockCarRepository);
        }

        // Проверка на получение всех автомобилей пользователя
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

        /// <summary>
        /// Проверка на получение автомобиля пользователя
        /// </summary>
        [TestMethod]
        public void CanReturnCarByIdWithAllowedUser()
        {
            Car car = carService.GetById(1, 1);

            Assert.IsNotNull(car);
            Assert.IsInstanceOfType(car, typeof(Car));
            Assert.AreEqual(1, car.CarId);
        }

        /// <summary>
        /// Проверка на ошибку при попытке получения чужого автомобиля
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CanNotReturnCarByIdWithNotAllowedUser()
        {
            Car car = carService.GetById(3, 1);
        }

        /// <summary>
        /// Проверка на удаление автомобиля пользоваетелем
        /// </summary>
        [TestMethod]
        public void CanDeleteCarByIdWithAllowedUser()
        {
            bool success = carService.DeleteById(1, 1);
            var deletedCar = carList.Where(car => car.CarId == 1).FirstOrDefault();

            Assert.IsTrue(success);
            Assert.IsNull(deletedCar);
        }

        /// <summary>
        /// Проверка на ошибку при попытке удаления чужого автомобиля
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CanNotDeleteCarByIdWithNotAllowedUser()
        {
            bool success = carService.DeleteById(3, 1);
        }

        /// <summary>
        /// Проверка на обновление автомобиля
        /// </summary>
        [TestMethod]
        public void CanUpdateCarWithAllowedUser()
        {
            Car newCar = new Car()
            {
                CarId = 3,
                CarName = "Honda Super",
                Brand = "Honda Civic Restyle",
                IssueYear = new DateTime(2011, 1, 1),
                VIN = "VGDD12ATNYP3",
                Numberplate = "ТВ411Р"
            };

            Car updatedCar = carService.Update(newCar, 2);

            Assert.IsNotNull(updatedCar);
            Assert.AreEqual("Honda Super", updatedCar.CarName);
            Assert.AreEqual("Honda Civic Restyle", updatedCar.Brand);
            Assert.AreEqual(new DateTime(2011, 1, 1), updatedCar.IssueYear);
            Assert.AreEqual("VGDD12ATNYP3", updatedCar.VIN);
            Assert.AreEqual("ТВ411Р", updatedCar.Numberplate);
        }

        /// <summary>
        /// Проверка на ошибку при попытке обновления чужого автомобиля
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CanNotUpdateWithNotAllowedUser()
        {
            Car newCar = new Car()
            {
                CarId = 3,
                CarName = "Honda Super",
                Brand = "Honda Civic Restyle",
                IssueYear = new DateTime(2011, 1, 1),
                VIN = "VGDD12ATNYP3",
                Numberplate = "ТВ411Р"
            };

            Car updatedCar = carService.Update(newCar, 1);
        }
    }
}
