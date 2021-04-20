using CarSell.DAL.Parameteres;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarSell.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity, T entityupd);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<PageList<T>> GetAllAsync(ModelParameters modelParameters);
    }
}
