using CommunityBlog.Factory;
using Microsoft.AspNetCore.Mvc;

namespace CommunityBlog.Controllers
{
    public class ManageGroupController : Controller
    {
        private readonly IGroupsFactory _groupsFactory;
        public ManageGroupController(IGroupsFactory groupsFactory)
        {
            _groupsFactory = groupsFactory;
        }


    }
}
