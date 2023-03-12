using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityBlog.Models
{
    public class CommentUserModel
    {
        public int ID { get; set; }
        public string Comment { get;set; }
        public string Username { get; set; }
        public string Created_At { get; set; }
    }
}
