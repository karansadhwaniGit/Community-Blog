namespace CommunityBlog.Models.Post
{
    public class PostDetailModel
    {
        public PostModel post { get; set; }
        public bool isLikedByUser {get;set;}
        public int likesCount { get; set; }
        public int commentsCount { get; set; }
    }
}
