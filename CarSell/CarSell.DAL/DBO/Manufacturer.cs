using System;
using System.Collections.Generic;
using System.Text;

namespace CarSell.DAL.DBO
{
    public class Manufacturer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
