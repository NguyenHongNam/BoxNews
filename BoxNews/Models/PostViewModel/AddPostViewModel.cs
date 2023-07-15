using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BoxNews.Models.Domain;
namespace BoxNews.Models.PostViewModel
{
    [Table("tblPosts")]
    public class AddPostViewModel
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        public int CategoryID { get; set; }
        public int AccountID { get; set; }
        public string Content { get; set; }
        public string ImgSrc { get; set; }
        public Boolean Status { get; set; }
        public List<Category> Categories { get; set; }
    }
}