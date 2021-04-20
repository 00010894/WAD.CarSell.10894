using CarSell.DAL.DBO;
using CarSell.DAL.Parameteres;
using CarSell.DAL.RepoExtentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarSell.DAL.Repositories
{
    public class CarRepo : BaseRepo, IRepository<Car>
    {
        public CarRepo(CarsDbContext context)
            :base(context)
        {

        }

        public async Task CreateAsync(Car entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<PageList<Car>> GetAllAsync(ModelParameters modelParameters)
        {
            var cars = await _context.Cars
               .Include(a => a.Manufacturer)
              .Search(modelParameters.SearchTerm)
              .Sort(modelParameters.OrderBy)
              .ToListAsync();

            

            return PageList<Car>
                .ToPagedList(cars, modelParameters.PageNumber, modelParameters.PageSize);
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Car entity, Car entityupd)
        {

            entityupd.CarName = entity.CarName;
            entityupd.Image = entity.Image;
            entityupd.Price = entity.Price;
            entityupd.Region = entity.Region;
            entityupd.ManufacturerId = entity.ManufacturerId;
            entityupd.YearOfRelease = entity.YearOfRelease;


            await _context.SaveChangesAsync();
        }
    }
}
