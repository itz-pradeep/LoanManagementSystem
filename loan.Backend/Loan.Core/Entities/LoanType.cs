using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Core.Entities
{
    public class LoanType : BaseEntity
    {
        public string Type { get; set; }
        public bool? IsActive { get; set; }
    }
}
