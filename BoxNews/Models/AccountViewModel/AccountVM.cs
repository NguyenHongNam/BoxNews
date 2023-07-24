using BoxNews.Models.Domain;
namespace BoxNews.Models.AccountViewModel
{
    public class AccountVM
    {
        public List<Account> Accounts { get; set; }
        public List<Role> Roles { get; set; }
    }
}
