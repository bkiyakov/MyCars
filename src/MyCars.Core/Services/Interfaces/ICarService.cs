using MyCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Services.Interfaces
{
    public interface ICarService
    {
        IEnumerable<Car> GetAllByUserId(int userId);
        Car GetById(int carId, int userId);
        Car Add(Car car, int userId);
        bool DeleteById(int carId, int userId);
        Car Update(Car car, int userId);
    }
}
