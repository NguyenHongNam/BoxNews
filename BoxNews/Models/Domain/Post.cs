using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BoxNews.Models.Domain
{
    [Table("tblPosts")]
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int AccountID { get; set; }
        public string Content { get; set; }
        public string ImgSrc { get; set; }
        public bool Status { get; set; }
        public List<Rating> Comments { get; set; }
        public Category Category { get; set; }
    }
}