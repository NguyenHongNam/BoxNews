using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BoxNews.Models.CategoryViewModel
{
    [Table("tblCategory")]
    public class AddCategoryViewModel
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}