using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityBlog.Models
{
    public class LikePostModel
    {
        public int ID { get; set; }
        public int PID { get; set; }
        public int UID { get; set; }
    }
}
