using Loan.Core;
using Loan.Core.Entities;
using Loan.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.Data
{
    public class LoanRepository : Repository<LoanApplication>, ILoanRepository
    {
        public LoanRepository(LoanContext context) : base(context)
        {

        }

        public async Task<IReadOnlyList<LoanApplication>> GetAllWithCriteria(LoanApplicationFilter filter)
        {
            return await FindByCondition(x => x.IsActive == true 
            && (!filter.LoanId.HasValue || x.Id == filter.LoanId)
            && (string.IsNullOrEmpty(filter.FirstName) || x.FirstName.ToLower().Contains(filter.FirstName.ToLower()))
            && (string.IsNullOrEmpty(filter.LastName) || x.LastName.ToLower().Contains(filter.LastName.ToLower())))
                .Include(x => x.LoanType)
                .Include(x => x.LoanStatus)
                .ToListAsync();
        }

        public async Task<LoanApplication> GetByIdWithCriteria(int id)
        {
            return await FindByCondition(x => x.IsActive == true && x.Id == id)
                .Include(x => x.LoanType).Include(x => x.LoanStatus).FirstOrDefaultAsync();
        }
    }
}
