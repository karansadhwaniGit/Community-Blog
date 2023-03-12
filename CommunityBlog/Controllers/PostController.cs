using CommunityBlog.Factory;
using CommunityBlog.Models.Post;
using Microsoft.AspNetCore.Mvc;

namespace CommunityBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostFactory _postFactory;
        public PostController(IPostFactory postFactory)
        {
            _postFactory = postFactory;
        }

    }
}
