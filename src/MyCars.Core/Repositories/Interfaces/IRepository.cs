using System;
using System.Collections.Generic;
using System.Text;

namespace MyCars.Core.Repositories.Interfaces
{
    public interface IRepository<Model, T, U>
    {
        IEnumerable<Model> GetAllByUserId(U userId);
        Model GetById(T modelId);
        Model Add(Model model);
        bool DeleteById(T modelId);
        Model Update(Model model);
    }
}
