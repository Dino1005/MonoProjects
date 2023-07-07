
namespace Projects.WebApi.Models
{
    public class AccountView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AccountView(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}