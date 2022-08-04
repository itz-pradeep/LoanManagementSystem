namespace Loan.API.Dtos.Loan
{
    public class UpdateLoanRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PropertyAddress { get; set; }
        public Decimal LoanAmount { get; set; }
        public int LoanTenure { get; set; }
        public int LoanTypeId { get; set; }
    }
}
