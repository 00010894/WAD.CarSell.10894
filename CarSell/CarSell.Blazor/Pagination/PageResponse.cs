using CarSell.DAL.Parameteres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSell.Blazor.Pagination
{
    public class PageResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public PageParam PageParameters { get; set; }
    }
}
