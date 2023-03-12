using CommunityBlog.Models.Group;
using CommunityBlog.Models.User;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommunityBlog.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly DataContext _context;
        public GroupsService(DataContext dataContext)
        {
            _context = dataContext;
        }
        public dynamic GetAllGroups()
        {
            var groups = _context.Groups.Where(x=>x.Type==1).ToList();
            return groups;
        }
        public int CreateGroup(GroupModel _group,int id)
        {
            _context.Groups.Add(_group);
            _context.SaveChanges();
            GroupAdminsModel admin = new GroupAdminsModel();
            admin.UserId = id;
            admin.GroupId = _group.Id;
            var participant = new GroupParticiantsModel();
            participant.GroupId = _group.Id;
            participant.UserId = id;
            _context.GroupsParticipants.Add(participant);
            _context.SaveChanges();
            _context.AdminsGroups.Add(admin);
            _context.SaveChanges();
           
            return _group.Id;
        }
        public dynamic GetAllGroupsBySpecificAdmin(int userId)
        {
            var groups = _context.Groups.ToList().Join(_context.AdminsGroups.ToList().Where(x => x.UserId == userId), g => g.Id, a => a.GroupId, (group, admin) => new
            {
                group = group,
            }).ToList().OrderByDescending(x => x.group.Created_At);
            return groups;
        }
        public dynamic GetAllUserInNotInSpecificGroup(int groupId,int user_id)
        {
            var users = _context.GroupsParticipants.Where(x => x.GroupId == groupId).Select(y => y.UserId).ToArray();
            var notInGroup = _context.Users.Where(x => x.Id!=user_id && !users.Contains(x.Id));
            return notInGroup;
        }
        public dynamic GetAllUsersInSpecificGroup(int groupId)
        {
            var users = _context.GroupsParticipants.Where(x => x.GroupId == groupId).ToList().Join(_context.Users.ToList(), x => x.UserId, y => y.Id, (g, p) => new
            {
                users = p
            }).ToList();
            return users;
        }
        public bool deleteMemberFromGroup(int participantId,int groupId)
        {
            var participant = _context.GroupsParticipants.Where(x => x.UserId == participantId && x.GroupId==groupId).FirstOrDefault();
            if (participant != null)
            {
                _context.GroupsParticipants.Remove(participant);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool addMemberToGroup(int participantId,int groupId)
        {
            var user = _context.Users.Where(x => x.Id==participantId).FirstOrDefault();
            if (user != null)
            {
                var participantModel = new GroupParticiantsModel();
                participantModel.UserId = participantId;
                participantModel.GroupId=groupId;
                _context.GroupsParticipants.Add(participantModel);
                _context.SaveChanges();
                JoinRequestModel isprivate = _context.JoinRequests.Where(x => x.GroupId == groupId && x.UserId == participantId && x.Status == "Requested").FirstOrDefault();
                if (isprivate != null)
                {
                    isprivate.Status = "Approved";
                    _context.SaveChanges();
                }
                return true;
            }
            else
                return false;
        }
        public dynamic getAllGroupsOfSpecificUser(int userId)
        {
            var groupIds = _context.GroupsParticipants.Where(x => x.UserId == userId).Select(y => y.GroupId).ToArray();
            var groups = _context.Groups.Where(x => groupIds.Contains(x.Id)).ToList();
            return groups;
        }
        public dynamic getAllGroupsWhereUsersIsNotParticipant(int userId)
        {
            var groupsArray = _context.GroupsParticipants.Where(x => x.UserId == userId).Select(y=>y.GroupId).ToArray();
            var groups = _context.Groups.Where(x => !groupsArray.Contains(x.Id));
            return groups;
        }
        public bool AddToGroup(GroupParticiantsModel groupParticiant)
        {
            int count = _context.GroupsParticipants.Where(x => x.UserId == groupParticiant.UserId && x.GroupId == groupParticiant.GroupId).Count();
            if (count > 0)
            {
                return false;
            }
        
            _context.GroupsParticipants.Add(groupParticiant);
            _context.SaveChanges();
            return true;
        }
        public bool RequestToJoin(JoinRequestModel group)
        {
            int count = _context.GroupsParticipants.Where(x => x.UserId == group.UserId && x.GroupId == group.GroupId).Count();
            if (count > 0)
            {
                return false;
            }
            else
            {
                 count = _context.JoinRequests.Where(x => x.GroupId == group.GroupId && x.UserId == group.UserId && x.Status == "Requested").Count();
                if(count>0)
                {
                    return false;
                }
            }
            group.Status = "Requested";
            group.CreatedAt = System.DateTime.Now.ToString();
            group.UpdatedAt = System.DateTime.Now.ToString();
            _context.JoinRequests.Add(group);
            _context.SaveChanges();
            return true;
        }
        public dynamic LoadAllRequestsOfGroup(int groupId)
        {
             var requests=_context.JoinRequests.Where(x => x.GroupId == groupId && x.Status=="Requested").Select(y => y.UserId).ToArray();
            var users = _context.Users.Where(x => requests.Contains(x.Id)).ToList();
                return users;
        }
        public dynamic loadMyRequests(int userId, int groupId)
        {
            List<JoinRequestModel> requests = _context.JoinRequests.Where(x => x.UserId == userId).ToList();
            List<GroupJoinRequestModel> list = new List<GroupJoinRequestModel>();
            if (requests != null)
            {
                foreach (var request in requests)
                {
                    var myrequests = new GroupJoinRequestModel();
                    myrequests.group = _context.Groups.Where(x => x.Id == request.GroupId).FirstOrDefault();
                    myrequests.status = request.Status;
                    list.Add(myrequests);
                }
            }
            return list;
        }
        public bool RejectJoinRequest(int userId,int groupId)
        {
            JoinRequestModel request = _context.JoinRequests.Where(x => x.GroupId == groupId && x.Status == "Requested" && x.UserId == userId).FirstOrDefault();
            if (request != null) {
                request.Status = "Rejected";
                _context.SaveChanges();
                return true;
             }
            return false;
        }
        public dynamic GetAllUsers(int id)
        {
            var users = _context.Users.Where(x => x.Id != id).ToList();
            return users;
        }
        public bool ControlAdmin(int id)
        {
            var user= _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                if (user.Type == "superadmin")
                    user.Type = "user";
                else
                    user.Type = "superadmin";
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}   
