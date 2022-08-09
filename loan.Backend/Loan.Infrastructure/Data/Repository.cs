using Loan.Core.Entities;
using Loan.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly LoanContext _context;

        public Repository(LoanContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll() => _context.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _context.Set<T>().Where(expression).AsNoTracking();
        public async Task CreateAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
    }
}
