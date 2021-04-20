using CarSell.DAL.DBO;
using CarSell.DAL.Parameteres;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarSell.DAL.Repositories
{
    public class ManufacturerRepo : BaseRepo, IRepository<Manufacturer>
    {
        public ManufacturerRepo(CarsDbContext context)
            :base(context)
        {

        }


        public async Task<PageList<Manufacturer>> GetAllAsync(ModelParameters modelParameters)
        {
            var manufacturers = await _context.Manufacturers.ToListAsync();

            return PageList<Manufacturer>
                .ToPagedList(manufacturers, modelParameters.PageNumber, modelParameters.PageSize);
        }

        public async Task<Manufacturer> GetByIdAsync(int id)
        {
            return await _context.Manufacturers
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateAsync(Manufacturer entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Manufacturer entity, Manufacturer entityupd)
        {
            entityupd.Name = entity.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
        }

       

       
    }
}
