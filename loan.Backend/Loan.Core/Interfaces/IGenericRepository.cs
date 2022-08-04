using Loan.Core.Entities;
using Loan.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetByFilterAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetListByFilterAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task PostAsync(T entity);
        Task PutAsync(T entity);
    }
}
