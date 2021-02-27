using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T newEntity);
        Task<T> Update(T updatedEntity);
        Task<T> Delete(int id);
        Task<IEnumerable<T>> Search(string searchKey);

    }
}