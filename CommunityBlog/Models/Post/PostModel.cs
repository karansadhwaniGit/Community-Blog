namespace CommunityBlog.Models.Post
{
    public class PostModel
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int Type { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

