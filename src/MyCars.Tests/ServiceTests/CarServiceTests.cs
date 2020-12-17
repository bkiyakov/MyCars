using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCars.Core.Repositories.Interfaces;
using MyCars.Core.Entities;
using MyCars.Core.Services;
using MyCars.Core.Services.Interfaces;
using MyCars.Core.Exceptions;
using Moq;

namespace MyCars.Tests.ServiceTests
{
    [TestClass]
    public class CarServiceTests
    {
        private readonly ICarRepository _mockCarRepository;
        private readonly ICarService carService;
        private readonly IList<Core.Entities.Car> carList;

        public CarServiceTests()
        {
            // create some mock cars to play with
            carList = new List<Core.Entities.Car>
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
                    Created = new DateTime(2020, 10, 6, 12, 0, 35),
                    Modified = new DateTime(2020, 10, 6, 12, 0, 35)
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
                    Created = new DateTime(2020, 11, 14, 10, 12, 23),
                    Modified = new DateTime(2020, 11, 14, 12, 53, 14)
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
                    Created = new DateTime(2020, 11, 23, 22, 10, 5),
                    Modified = new DateTime(2020, 11, 28, 16, 40, 24)
                }
            };

            // Mock the Cars Repository using Moq
            Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();

            // Return all the cars
            mockCarRepository.Setup(mr => mr.GetAllByUserId(It.IsAny<int>()))
                .Returns((int userId) => (carList.Where(c => c.UserId == userId)));

            // Return car by id
            mockCarRepository.Setup(mr => mr.GetById(It.IsAny<int>()))
                .Returns((int carId) => carList.Where(c => c.CarId == carId)
                .FirstOrDefault());

            // Add car
            mockCarRepository.Setup(mr => mr.Add(It.IsAny<Car>()))
                .Returns((Car car) =>
                {
                    int newId = carList.Count + 1;

                    car.CarId = newId;

                    carList.Add(car);

                    return carList.Where(c => c.CarId == newId).FirstOrDefault();
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
                            c.Created = newCar.Created;
                            c.Modified = newCar.Modified;

                            return c;
                        }).ToList();

                    return carList.FirstOrDefault(c => c.CarId == newCar.CarId);
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
