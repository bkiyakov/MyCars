using MyCars.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Repositories.Interfaces
{
    public interface IEventRepository : IRepository <Event, int, int>
    {
    }
}
