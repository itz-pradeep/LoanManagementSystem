using Loan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Core.Specifications
{
    public class LoanWithTypeAndStatus : BaseSpecification<LoanApplication>
    {
        public LoanWithTypeAndStatus(int loanId) : base(
            x => x.Id == loanId
        )
        {
            AddIncludes(x => x.LoanType);
            AddIncludes(x => x.LoanStatus);
        }
        public LoanWithTypeAndStatus(LoanApplicationFilter filter) : base(
            x => x.IsActive == true &&
            (!filter.LoanId.HasValue || x.Id == filter.LoanId)
            && (string.IsNullOrEmpty(filter.FirstName) || x.FirstName.ToLower().Contains(filter.FirstName.ToLower()))
            && (string.IsNullOrEmpty(filter.LastName) || x.LastName.ToLower().Contains(filter.LastName.ToLower())))
        {
            AddIncludes(x => x.LoanType);
            AddIncludes(x => x.LoanStatus);
            AddOrderBy(x => x.CreatedDate);
        }
    }
}
