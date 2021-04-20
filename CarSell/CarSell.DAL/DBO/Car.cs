using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarSell.DAL.DBO
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000, MinimumLength =2)]
        public string CarName { get; set; }

        [Required]
        public double Price { get; set; }

        [StringLength(100, MinimumLength =2)]
        public string Region { get; set; }

        [Required]
        [Range(1980, int.MaxValue)]
        public int YearOfRelease { get; set; }

        public string Image { get; set; }

        public int? ManufacturerId { get; set; }
        
        public virtual Manufacturer Manufacturer { get; set; }


    }
}
