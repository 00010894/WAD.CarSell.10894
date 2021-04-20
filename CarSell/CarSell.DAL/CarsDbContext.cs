using CarSell.DAL.DBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarSell.DAL
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData(new Car
            {
                Id = 1,
                CarName = "Tayota Land Crusier Prado",
                Image = "https://kluz-photos.kcdn.online/webp/27/27ea59a8-982f-4951-9598-de7b0dd2c86c/1-160x120.jpg",
                Price = 24000,
                YearOfRelease = 2007,
                Region = "Tashkent",
                ManufacturerId = 1
            }, new Car
            {
                Id = 2,
                CarName = "Lacetti, 3rd position",
                Image = "https://kluz-photos.kcdn.online/webp/90/9097f693-c587-49da-8baf-2d0c0b10af0c/1-160x120.jpg",
                Price = 11000,
                YearOfRelease = 2018,
                Region = "Tashkent",
                ManufacturerId = 2,
            }
            );
            modelBuilder.Entity<Manufacturer>().HasData(new Manufacturer
            {
                Id = 1,
                Name = "Tayota"
            }, new Manufacturer
            {
                Id = 2,
                Name = "UzAM"
            }
            );
        }


        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    }
}
