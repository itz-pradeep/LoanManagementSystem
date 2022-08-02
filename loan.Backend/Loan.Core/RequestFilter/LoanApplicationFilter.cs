using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Core
{
    public class LoanApplicationFilter
    {
        public int? LoanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
