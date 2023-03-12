using CommunityBlog.Models.Group;
using CommunityBlog.Services;

namespace CommunityBlog.Factory
{
    public class GroupsFactory : IGroupsFactory
    {
        private readonly IGroupsService _groupsService;
        public GroupsFactory(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }
        public List<GroupModel>? GetAllGroups()
        {
            var groups = _groupsService.GetAllGroups();
            return groups != null ? groups : null;
        }

        public int CreateGroup(GroupModel group, int id)
        {
            group.Created_At = DateTime.Now.ToString();
            return _groupsService.CreateGroup(group, id);
        }
        public dynamic GetAllGroupsBySpecificAdmin(int user_id)
        {
            return _groupsService.GetAllGroupsBySpecificAdmin(user_id);
        }
        public dynamic GetAllUserInNotInSpecificGroup(int groupId, int userId)
        {
            return _groupsService.GetAllUserInNotInSpecificGroup(groupId, userId);
        }
        public dynamic GetAllUsersInSpecificGroup(int groupId)
        {
            return _groupsService.GetAllUsersInSpecificGroup(groupId);
        }
        public bool deleteMemberFromGroup(int participantId, int groupId)
        {
            return _groupsService.deleteMemberFromGroup(participantId, groupId);
        }
        public bool addMemberToGroup(int participantId, int groupId)
        {
            return _groupsService.addMemberToGroup(participantId, groupId);
        }
        public dynamic getAllGroupsOfSpecificUser(int userId)
        {
            return _groupsService.getAllGroupsOfSpecificUser(userId);
        }
        public dynamic getAllGroupsWhereUsersIsNotParticipant(int userId)
        {
            return _groupsService.getAllGroupsWhereUsersIsNotParticipant(userId);
        }
        public bool AddToGroup(int user_id, int groupid)
        {
            var groupParticipant = new GroupParticiantsModel();
            groupParticipant.UserId = user_id;
            groupParticipant.GroupId = groupid;
            return _groupsService.AddToGroup(groupParticipant);
        }
        public bool RequestToJoin(int user_id, int group_id)
        {
            var joinModel = new JoinRequestModel();
            joinModel.UserId = user_id;
            joinModel.GroupId = group_id;
            return _groupsService.RequestToJoin(joinModel);

        }
        public dynamic LoadAllRequestsOfGroup(int groupId)
        {
            return _groupsService.LoadAllRequestsOfGroup(groupId);
        }
        public dynamic loadMyRequests(int userId, int groupId)
        {
            return _groupsService.loadMyRequests(userId, groupId);

        }
        public bool RejectJoinRequest(int userId, int groupId)
        {
            return _groupsService.RejectJoinRequest(userId, groupId);
        }
        public dynamic GetAllUsers(int id)
        { 
            return _groupsService.GetAllUsers(id); 
        }
        public bool ControlAdmin(int id)
        {
            return _groupsService.ControlAdmin(id);
        }

    }
}
