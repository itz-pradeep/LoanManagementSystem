namespace Loan.API.Dtos.Loan
{
    public class LoanApplicationResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PropertyAddress { get; set; }
        public Decimal LoanAmount { get; set; }
        public int LoanTenure { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }
        public bool IsActive { get; set; }
    }
}
