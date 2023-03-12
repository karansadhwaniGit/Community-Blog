using CommunityBlog.Models;
using CommunityBlog.Models.Post;

namespace CommunityBlog.Factory
{
    public interface IPostFactory
    {
        public void CreatePost(PostModel post);
        public dynamic GetAllPostsOfGroup(int groupId, int userId);
        public bool LikePost(int postId, int userId);
        public bool UnlikePost(int postId, int userId);
        public void CreateComment(CommentModel comment, int postId);
        public dynamic GetAllCommentsOnPost(int postId);
        public dynamic GetSinglePostData(int postId);
        public dynamic GetAllLikesOnPost(int postId);



    }
}
