using Loan.Core.Entities;
using Loan.Core.Interfaces;
using Loan.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly LoanContext _loanContext;

        public GenericRepository(LoanContext loanContext)
        {
            _loanContext = loanContext;
        }
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> GetByFilterAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _loanContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetListByFilterAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _loanContext.Set<T>().ToListAsync();
        }

        public async Task PostAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException();
            }

            await _loanContext.Set<T>().AddAsync(entity);
            await _loanContext.SaveChangesAsync();

        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecEvaluator<T>.GetQuery(_loanContext.Set<T>(),spec);
        }
    }
}
