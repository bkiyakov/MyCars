using MyCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Repositories.Interfaces
{
    public interface ICarRepository : IRepository<Car, int, int>
    {
    }
}
