using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Core.Entities
{
    public class LoanStatus : BaseEntity
    {
        public string Status { get; set; }
        public bool? IsActive { get; set; }
    }
}
