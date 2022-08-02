using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Core.Entities
{
    public class LoanApplication : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string PropertyAddress { get; set; }
        public Decimal LoanAmount { get; set; }
        public int LoanTenure { get; set; }
        public int LoanTypeId { get; set; }
        public LoanType LoanType { get; set; }
        public int LoanStatusId { get; set; }
        public LoanStatus LoanStatus { get; set; }
        public bool? IsActive { get; set; }

    }
}
