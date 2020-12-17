using MyCars.Core.Exceptions;
using MyCars.Core.Entities;
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
            car.Created = DateTime.UtcNow;
            car.Modified = DateTime.UtcNow;

            return _carRepository.Add(car);
        }

        public bool DeleteById(int carId, int userId)
        {
            var car = _carRepository.GetById(carId);

            if (car.UserId == userId)
            {
                return _carRepository.DeleteById(carId);
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public Car GetById(int carId, int userId)
        {
            var car = _carRepository.GetById(carId);

            if (car.UserId == userId)
            {
                return car;
            } else
            {
                throw new NotFoundException();
            }
        }

        public IEnumerable<Car> GetAllByUserId(int userId)
        {
            return _carRepository.GetAllByUserId(userId);
        }

        public Car Update(Car car, int userId)
        {
            var carFromRepo = _carRepository.GetById(car.CarId);

            if (carFromRepo != null && carFromRepo.UserId == userId)
            {
                car.Modified = DateTime.UtcNow;

                return _carRepository.Update(car);
            }
            else
            {
                throw new NotFoundException();
            }
        }
    }
}
