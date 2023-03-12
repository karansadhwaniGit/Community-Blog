using CommunityBlog.Models;
using CommunityBlog.Models.Post;

namespace CommunityBlog.Services
{
    public interface IPostService
    {
        public void CreatePost(PostModel post);
        public dynamic GetAllPostsOfGroup(int groupId, int userId);
        public int GetLikesCountOnPost(int postId);
        public int GetCommentsCountOnPost(int postId);
        public bool HasUserLikedPost(int postId, int userId);
        public bool LikePost(LikePostModel like);
        public bool UnlikePost(LikePostModel like);
        public void CreateComment(CommentModel comment, int postId);
        public dynamic GetAllCommentsOnPost(int postId);
        public dynamic GetSinglePostData(int postId);
        public dynamic GetAllLikesOnPost(int postId);
    }
}
