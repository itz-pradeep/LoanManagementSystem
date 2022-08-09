using Loan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Core.Interfaces
{
    public interface ILoanRepository : IRepository<LoanApplication>
    {
        Task<IReadOnlyList<LoanApplication>> GetAllWithCriteria(LoanApplicationFilter filter);
        Task<LoanApplication> GetByIdWithCriteria(int id);
    }
}
