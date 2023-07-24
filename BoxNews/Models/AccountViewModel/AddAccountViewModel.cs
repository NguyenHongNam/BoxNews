using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BoxNews.Models.AccountViewModel
{
    [Table("tblAccounts")]
    public class AddAccountViewModel
    {
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        public string UserName { get; set; }

        public string UserPassword { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
    }
}