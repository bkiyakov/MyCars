using MyCars.Core.Entities;
using MyCars.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCars.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext context;

        public CarRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Car Add(Car model)
        {
            Car addedCar =  context.Cars.Add(model);

            if (context.SaveChanges() != 0)
            {
                return addedCar;
            } else
            {
                return null;
            }
        }

        public bool DeleteById(int modelId)
        {
            Car carToRemove = context.Cars.Where(c => c.CarId == modelId).FirstOrDefault();

            if (carToRemove != null)
            {
                context.Cars.Remove(carToRemove);

                return (context.SaveChanges() != 0);
            } else
            {
                return false;
            }
            
        }

        public IEnumerable<Car> GetAllByUserId(int userId)
        {
            return context.Cars.Where(c => c.UserId == userId).ToList();
        }

        public Car GetById(int modelId)
        {
            return context.Cars.Where(c => c.CarId == modelId).FirstOrDefault();
        }

        public Car Update(Car model)
        {
            Car carFromDb = context.Cars.Find(model.CarId);

            if(carFromDb != null)
            {
                carFromDb.CarName = model.CarName;
                carFromDb.Brand = model.Brand;
                carFromDb.IssueYear = model.IssueYear;
                carFromDb.Numberplate = model.Numberplate;
                carFromDb.VIN = model.VIN;
                carFromDb.Modified = DateTime.UtcNow;

                context.SaveChanges();
                return context.Cars.Find(model.CarId);
            } else
            {
                return null;
            }
        }
    }
}
