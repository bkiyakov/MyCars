using MyCars.Core.Models;
using MyCars.Core.Repositories.Interfaces;
using MyCars.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public Car Add(Car car, int userId)
        {
            car.UserId = userId;

            try
            {
                return _carRepository.Add(car);
            } catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public bool DeleteById(int carId, int userId)
        {
            var car = _carRepository.GetById(carId);

            return car.UserId == userId ? _carRepository.DeleteById(carId) : false;
        }

        public Car GetById(int carId, int userId)
        {
            var car = _carRepository.GetById(carId);

            return car.UserId == userId ? car : null;
        }

        public IEnumerable<Car> GetAllByUserId(int userId)
        {
            return _carRepository.GetAllByUserId(userId);
        }

        public Car Update(Car car, int userId)
        {
            var carFromRepo = _carRepository.GetById(car.CarId);

            return carFromRepo.UserId == userId ? _carRepository.Update(car) : null;
        }
    }
}
