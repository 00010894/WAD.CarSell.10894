using System;
using System.Collections.Generic;
using System.Text;

namespace CarSell.DAL.Parameteres
{
    public class ModelParameters
    {
        const int maxPageSize = 40;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 3;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string SearchTerm { get; set; }
        public string OrderBy { get; set; } = "status";
    }
}
