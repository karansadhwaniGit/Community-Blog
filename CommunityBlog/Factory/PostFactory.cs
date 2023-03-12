using CommunityBlog.Models;
using CommunityBlog.Models.Post;
using CommunityBlog.Services;

namespace CommunityBlog.Factory
{
    public class PostFactory : IPostFactory
    {
        private readonly IPostService _post;
        private readonly IAuthFactory _authFactory;
        public PostFactory(IPostService post, IAuthFactory auth)
        {
            _post = post;
            _authFactory = auth;
        }
        public void CreatePost(PostModel post)
        {
            _post.CreatePost(post);
        }
        public dynamic GetAllPostsOfGroup(int groupId, int userId)
        {
            var posts = _post.GetAllPostsOfGroup(groupId, userId);
            var UserPosts = new List<UserPostModel>();
            foreach (var post in (dynamic)posts)
            {
                var UserPost = new UserPostModel();
                UserPost.user = _authFactory.GetUserById(post.UserId);
                UserPost.post = post;
                UserPost.likeCount = _post.GetLikesCountOnPost(post.ID);
                UserPost.CommentCount = _post.GetCommentsCountOnPost(post.ID);
                UserPost.HasUserLiked = _post.HasUserLikedPost(post.ID, userId);
                UserPosts.Add(UserPost);
            }
            return UserPosts;
        }
        public bool LikePost(int postId, int userId)
        {
            LikePostModel like = new LikePostModel();
            like.UID = userId;
            like.PID = postId;
            return _post.LikePost(like);
        }
        public bool UnlikePost(int postId, int userId)
        {
            LikePostModel like = new LikePostModel();
            like.UID = userId;
            like.PID = postId;
            return _post.UnlikePost(like);
        }
        public void CreateComment(CommentModel comment, int postId)
        {
            _post.CreateComment(comment, postId);
        }
        public dynamic GetAllCommentsOnPost(int postId)
        {
            var posts = _post.GetAllCommentsOnPost(postId);
            var commentUsers = new List<CommentUserModel>();
            foreach (var post in (dynamic)posts)
            {
                var commentUser = new CommentUserModel();
                var user = _authFactory.GetUserById(post.compo.UID);
                commentUser.Username = user.UserName;
                commentUser.Comment = post.compo.Content;
                commentUser.Created_At = post.compo.Created_At.ToString();
                commentUsers.Add(commentUser);
            }
            return commentUsers;
        }
        public dynamic GetSinglePostData(int postId)
        {
            var post = _post.GetSinglePostData(postId);
            var userPost = new UserPostModel();
            userPost.user = _authFactory.GetUserById(post.UserId);
            userPost.post = post;
            return userPost;
        }
        public dynamic GetAllLikesOnPost(int postId)
        {
            return _post.GetAllLikesOnPost(postId);
        }
    }
}
