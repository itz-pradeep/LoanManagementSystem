namespace Loan.API.Dtos.Account
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
    }
}
