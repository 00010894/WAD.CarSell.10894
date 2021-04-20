using System;
using System.Collections.Generic;
using System.Text;

namespace CarSell.DAL.Repositories
{
    public abstract class BaseRepo
    {
        protected readonly CarsDbContext _context;

        protected BaseRepo(CarsDbContext context)
        {
            _context = context;
        }
    }
}
