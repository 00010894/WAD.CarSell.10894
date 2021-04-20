using CarSell.Blazor.Pagination;
using CarSell.DAL.Parameteres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSell.Blazor.HTTPRepo
{
    public interface ICarsHttpRepo<T> where T : class
    {
        Task<PageResponse<T>> GetAsync(ModelParameters modelParam);
        Task CreateAsync(T entity);
        Task<T> GetById(string id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
