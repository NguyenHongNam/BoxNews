namespace BoxNews.Models.Domain
{
    public class Rating
    {
        public int RatingId { get; set; }
        public string PostID { get; set; }
        public string UserName { get; set;}
        public string Comments { get; set;}
        public DateTime CreateAt { get; set; }
    }
}
