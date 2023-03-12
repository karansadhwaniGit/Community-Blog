using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityBlog.Models
{
    public class CommentsPostModel
    {
        public int ID { get; set; }
        public int  CID { get; set; }
        public int PID { get; set; }
    }
}
