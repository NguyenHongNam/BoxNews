using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BoxNews.Models.Domain;
namespace BoxNews.Models.PostViewModel
{
    [Table("tblPosts")]
    public class AddPostViewModel
    {
        [Required(ErrorMessage ="Tên bài viết không được để trống")]
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        [Required(ErrorMessage ="Vui lòng chọn danh mục")]
        public int CategoryID { get; set; }
        public int AccountID { get; set; }
        [Required(ErrorMessage ="Nhập nội dung bài viết")]
        public string Content { get; set; }
        [Required(ErrorMessage ="Vui lòng tải ảnh bài viết")]
        public string ImgSrc { get; set; }
        public Boolean Status { get; set; }
        public List<Category> Categories { get; set; }
    }
}