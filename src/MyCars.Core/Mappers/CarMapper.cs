using MyCars.Core.Entities;
using MyCars.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Mappers
{
    public static class CarMapper
    {
        public static Car ToDomain(this CarEntity carEntity)
        {
            return new Car()
            {
                CarId = carEntity.CarId,
                CarName = carEntity.CarName,
                Brand = carEntity.Brand,
                IssueYear = carEntity.IssueYear,
                VIN = carEntity.VIN,
                Numberplate = carEntity.Numberplate,
                UserId = carEntity.UserId
            };
        }
    }
}
