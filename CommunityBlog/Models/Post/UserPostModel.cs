using CommunityBlog.Models.Post;
using CommunityBlog.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;  
using System.Threading.Tasks;

namespace CommunityBlog.Models
{
    public class UserPostModel
    {
        public UserModel user { get;set; }
        public PostModel post { get; set; }
        public int likeCount { get; set; }      
        public int CommentCount { get; set; }
        public bool HasUserLiked { get; set; }

    }
}
