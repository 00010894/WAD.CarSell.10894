using CarSell.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace CarSell.DAL.RepoExtentions
{
    public static class CarExtention
    {
        public static IQueryable<Car> Search(this IQueryable<Car> cars, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return cars;
            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();
            return cars.Where(p => p.CarName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Car> Sort(this IQueryable<Car> cars, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return cars.OrderBy(e => e.Region);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Car).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split()[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return cars.OrderBy(e => e.Region);

           return cars.OrderBy(orderQuery);
        }

    }
}
