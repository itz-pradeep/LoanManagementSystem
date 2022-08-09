using System.ComponentModel.DataAnnotations;

namespace Loan.API.Dtos.Loan
{
    public class CreateLoanRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PropertyAddress { get; set; }
        [Required]
        public Decimal LoanAmount { get; set; }
        [Required]
        public int LoanTenure { get; set; }
        [Required]
        public int LoanTypeId { get; set; }
    }
}
