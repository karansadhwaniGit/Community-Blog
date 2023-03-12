using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityBlog.Models
{
    public class CommentModel
    {
        public int ID { get; set; }
        public int UID { get; set; }
        public  string Content { get; set; }
        public DateTime Created_At { get; set; }
    }
}
