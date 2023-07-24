using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BoxNews.Models.CategoryViewModel
{
    [Table("tblCategory")]
    public class AddAccountViewModel
    {
        [Required(ErrorMessage ="Tên danh mục không được để trống")]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}