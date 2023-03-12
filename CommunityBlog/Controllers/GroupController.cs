using CommunityBlog.Factory;
using CommunityBlog.Models;
using CommunityBlog.Models.Group;
using CommunityBlog.Models.Post;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CommunityBlog.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupsFactory _groupsFactory;
        private readonly IPostFactory _postFactory;
        public GroupController(IGroupsFactory groupsFactory, IPostFactory postFactory)
        {
            _groupsFactory = groupsFactory;
            _postFactory = postFactory;
        }
        public IActionResult CreateGroup(GroupModel group)
        {
            var id = HttpContext.Session.GetString("user_id");
            if (id != null)

            {
                var _groupId = _groupsFactory.CreateGroup(group, int.Parse(id));
                group.Id = _groupId;
                return Redirect("../superadmin");
            }
            return View("Index");
        }
        public IActionResult GetAllGroupsBySpecificAdmin()
        {
            var id = HttpContext.Session.GetString("user_id");
            if (id != null)
            {
                var groups = _groupsFactory.GetAllGroupsBySpecificAdmin(int.Parse(id));
                return Json(groups);
            }
            return Json(0);
        }
        [Microsoft.AspNetCore.Mvc.Route("/Group/GetAllUsersNotInGroup/{id}")]
        public IActionResult GetAllUsersNotInGroup(int id)
        {
            string user_id = HttpContext.Session.GetString("user_id");
            if (user_id != null)
            {
                var users = _groupsFactory.GetAllUserInNotInSpecificGroup(id, int.Parse(user_id));
                return Json(users);
            }
            return Json("ok");
        }
        [Microsoft.AspNetCore.Mvc.Route("/Group/GetAllUsersInSpecificGroup/{id}")]
        public IActionResult GetAllUsersInSpecificGroup(int id)
        {
            var user_id = HttpContext.Session.GetString("user_id");
            if (user_id != null)
            {
                var users = _groupsFactory.GetAllUsersInSpecificGroup(id);
                return Json(users);
            }
            return Json("ok");
        }
        [Microsoft.AspNetCore.Mvc.Route("/Group/DeleteParticipant/{id}/{groupid}")]
        [HttpPost]
        public IActionResult DeleteParticipant(int id, int groupid)
        {
            var user_id = HttpContext.Session.GetString("user_id");
            if (id > 0 && user_id != null)
            {
                if (id == int.Parse(user_id))
                {
                    return Json("Cannot Delete The Group Admin");
                }
                var res = _groupsFactory.deleteMemberFromGroup(id, groupid);
                return Json(res);
            }
            return Json(false);
        }
        [Microsoft.AspNetCore.Mvc.Route("/Group/AddParticipant/{id}/{groupid}")]
        [HttpPost]
        public IActionResult AddParticipant(int id, int groupid)
        {
            if (id > 0)
            {
                var res = _groupsFactory.addMemberToGroup(id, groupid);
                return Json(res);
            }
            return Json(false);
        }

        [Microsoft.AspNetCore.Mvc.Route("/Group/GetAllGroupsOfSpecificUser")]
        [HttpGet]
        public IActionResult GetAllGroupsOfSpecificUser()
        {
            var id = HttpContext.Session.GetString("user_id");
            if (id != null)
            {
                var res = _groupsFactory.getAllGroupsOfSpecificUser(int.Parse(id));
                return Json(res);
            }
            return Json(false);
        }
        [Microsoft.AspNetCore.Mvc.Route("/Group/CreatePost")]
        [HttpPost]
        public IActionResult CreatePost(PostModel post)
        {
            var uid = HttpContext.Session.GetString("user_id");
            if (uid != null)
            {
                post.UserId = int.Parse(uid);
                post.Type = 1;
                post.CreatedAt = System.DateTime.Now;
                _postFactory.CreatePost(post);
            }
            ModelState.Clear();
            return Json("ok");
        }
        [Microsoft.AspNetCore.Mvc.Route("/Group/DisplayAllPostsOfSpecificGroup/{groupId}")]
        [HttpGet]
        public IActionResult GetAllPosts(int groupId)
        {
            var uid = HttpContext.Session.GetString("user_id");
            if (uid != null)
            {
                var posts = _postFactory.GetAllPostsOfGroup(groupId, int.Parse(uid));
                return Json(posts);
            }
            return Json("ok");
        }
        [Microsoft.AspNetCore.Mvc.Route("/Group/LikeUnlikePost/{postId}/{flagLikeUnlike}")]
        public IActionResult LikeUnlikePost(int postId, int flagLikeUnlike)
        {
            var uid = HttpContext.Session.GetString("user_id");
            if (uid != null)
            {
                bool res;
                if (flagLikeUnlike == 0)
                {
                    res = _postFactory.UnlikePost(postId, int.Parse(uid));
                }
                else
                {
                    res = _postFactory.LikePost(postId, int.Parse(uid));
                }
                return Json(res);
            }
            return Json(false);
        }
        [Microsoft.AspNetCore.Mvc.Route("/Group/CreateComment/{postId}")]
        public IActionResult CreateComment(CommentModel comment, int postId)
        {
            var uid = HttpContext.Session.GetString("user_id");
            if (uid != null)
            {
                comment.UID = int.Parse(uid);
                comment.Created_At = DateTime.Now;
                _postFactory.CreateComment(comment, postId);
                return Json("ok");
            }
            return Json("false");
        }


        [Microsoft.AspNetCore.Mvc.Route("/Group/ShowAllCommentsOnPost/{postId}")]
        public IActionResult ShowAllCommentsOnPost(int postId)
        {
            var comments = _postFactory.GetAllCommentsOnPost(postId);
            if (comments != null)
                return Json(comments);
            else
                return Json(null);
        }

        [Microsoft.AspNetCore.Mvc.Route("/Group/ShowAllLikesOnPost/{postId}")]
        public IActionResult GetAllLikesOnPost(int postId)
        {
            var likes = _postFactory.GetAllLikesOnPost(postId);
            if (likes != null)
                return Json(likes);
            else
                return Json(null);
        }

        [Microsoft.AspNetCore.Mvc.Route("/Group/GetSinglePostData/{postId}")]
        public IActionResult GetSinglePostData(int postId)
        {
            var post = _postFactory.GetSinglePostData(postId);
            if (post != null)
                return Json(post);
            else
                return Json(null);
        }
        [Microsoft.AspNetCore.Mvc.Route("/Group/NotInGroups")]
        public IActionResult NotInGroups()
        {
            var user_id = HttpContext.Session.GetString("user_id");
            if (user_id != null)
            {
                var groups = _groupsFactory.getAllGroupsWhereUsersIsNotParticipant(int.Parse(user_id));
                return Json(groups);
            }
            return Json("false");
        }
        [Microsoft.AspNetCore.Mvc.Route("Group/AddToGroup/{groupId}")]
        public IActionResult AddToGroup(int groupId)
        {
            var user_id = HttpContext.Session.GetString("user_id");
            if (user_id != null)
            {
                var addParticipant = _groupsFactory.addMemberToGroup(int.Parse(user_id), groupId);
                return Json(true);
            }
            return Json(false);
        }
        [Microsoft.AspNetCore.Mvc.Route("Group/RequestToJoin/{groupId}")]
        public IActionResult RequestToJoin(int groupId)
        {
            var user_id = HttpContext.Session.GetString("user_id");
            if (user_id != null)
            {
                var request = _groupsFactory.RequestToJoin(int.Parse(user_id), groupId);
                if (request)
                {
                    return Json(true);
                }

            }
            return Json(false);
        }

        [Microsoft.AspNetCore.Mvc.Route("Group/LoadAllRequestsOfGroup/{groupId}")]
        [HttpGet]
        public IActionResult LoadAllRequestsOfGroup(int groupId)
        {
            var users = _groupsFactory.LoadAllRequestsOfGroup(groupId);
            return Json(users);
        }

        [Microsoft.AspNetCore.Mvc.Route("Group/LoadMyRequests/{groupId}")]
        [HttpGet]
        public IActionResult LoadMyRequests(int groupId)
        {
            var user_id = HttpContext.Session.GetString("user_id");
            if (user_id != null)
            {
                var users = _groupsFactory.loadMyRequests(int.Parse(user_id), groupId);
                return Json(users);
            }
            return Json(null);
        }

        [Microsoft.AspNetCore.Mvc.Route("Group/RejectJoinRequest/{userId}/{groupId}")]
        [HttpPost]
        public IActionResult RejectJoinRequest(int userId, int groupId)
        {
            var res = _groupsFactory.RejectJoinRequest(userId, groupId);
            return Json(res);
        }

        [Microsoft.AspNetCore.Mvc.Route("Group/AllUsers")]
        public IActionResult GetAllUsers()
        {
            var user_id = HttpContext.Session.GetString("user_id");
            if (user_id != null)
            {
                var users = _groupsFactory.GetAllUsers(int.Parse(user_id));
                return Json(users);
            }
            return Json(false);
        }
        [Microsoft.AspNetCore.Mvc.Route("Group/ControlAdmin/{id}")]
        public IActionResult ControlAdmin(int id)
        {
            var res = _groupsFactory.ControlAdmin(id);
            return Json(res);
        }
        [Microsoft.AspNetCore.Mvc.Route("Group/GetSessionValue")]
        public IActionResult GetSessionValue()
        {
            var user_id=HttpContext.Session.GetString("user_id"); 
            return Json(user_id);
        }
    }
}
