using System.ComponentModel.DataAnnotations.Schema;

namespace BoxNews.Models.Domain
{
    [Table("tblRatings")]
    public class Rating
    {
        public int RatingId { get; set; }
        public int PostID { get; set; }
        public string UserName { get; set;}
        public string Comments { get; set;}
        public DateTime CreateAt { get; set; }
    }
}
