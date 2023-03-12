using CommunityBlog.Models;
using CommunityBlog.Models.Post;

namespace CommunityBlog.Services
{
    public class PostService:IPostService
    {
        private readonly DataContext _context;
        public PostService(DataContext context)
        {
            _context = context;
        }
        public void CreatePost(PostModel post)
        {
            _context.Post.Add(post);
            _context.SaveChanges();
        }
        public dynamic GetAllPostsOfGroup(int groupId,int userId)
        {
            var posts=_context.Post.Where(x => x.GroupId == groupId).ToList().OrderByDescending(x => x.CreatedAt).ToList();
            return posts;
        }
        public int GetLikesCountOnPost(int postId)
        {
            return _context.Likes_Post.Where(x => x.PID == postId).Count();
        }
        public int GetCommentsCountOnPost(int postId)
        {
            return _context.Comments_Post.Where(x => x.PID == postId).Count();
        }
        public bool HasUserLikedPost(int postId,int userId)
        {
            return _context.Likes_Post.Where(x => x.UID == userId && x.PID == postId).Count()==1?true:false;
        }
        public bool UnlikePost(LikePostModel like)
        {
            var singleLike=_context.Likes_Post.Where(x => x.UID == like.UID && x.PID == like.PID).FirstOrDefault();
            if (singleLike != null)
            {
                _context.Likes_Post.Remove(singleLike);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool LikePost(LikePostModel like)
        {
            _context.Likes_Post.Add(like);
            _context.SaveChanges();
            return true;
        }
        public void CreateComment(CommentModel comment,int postId)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            CommentsPostModel commentpost = new CommentsPostModel();
            commentpost.CID = comment.ID;
            commentpost.PID = postId;
            _context.Comments_Post.Add(commentpost);
            _context.SaveChanges();
        }
        public dynamic GetAllCommentsOnPost(int postId)
        {
            var comments = _context.Comments_Post.Where(x => x.PID == postId).ToList().Join(_context.Comments.ToList(),c=>c.CID,c1=>c1.ID,(com,compo)=>new{
                com,
                compo
            }).ToList().OrderByDescending(x=>x.compo.Created_At);
            return comments;    
        }

        public dynamic GetAllLikesOnPost(int postId)
        {
            var likes = _context.Likes_Post.Where(x => x.PID == postId).ToList().Join(_context.Users.ToList(), l => l.UID, u => u.Id, (like, user) => new
            {
                user = user
            }).ToList();
            return likes;
        }
        public dynamic GetSinglePostData(int postId)
        {
            return _context.Post.Where(x => x.ID == postId).FirstOrDefault();
        }
    }
}
