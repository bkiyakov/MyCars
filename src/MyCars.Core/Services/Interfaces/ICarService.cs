using MyCars.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Services.Interfaces
{
    public interface ICarService
    {
        IEnumerable<Car> GetAll();
        Car Get(int id);
    }
}
