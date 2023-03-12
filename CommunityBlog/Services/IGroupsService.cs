using CommunityBlog.Models.Group;

namespace CommunityBlog.Services
{
    public interface IGroupsService
    {
        dynamic GetAllGroups();
        public int CreateGroup(GroupModel group, int id);
        public dynamic GetAllGroupsBySpecificAdmin(int userId);
        public dynamic GetAllUserInNotInSpecificGroup(int groupId,int userId);
        public dynamic GetAllUsersInSpecificGroup(int groupId);
        public bool deleteMemberFromGroup(int participantId, int groupId);
        public bool addMemberToGroup(int participantId, int groupId);
        public dynamic getAllGroupsOfSpecificUser(int userId);
        public dynamic getAllGroupsWhereUsersIsNotParticipant(int userId);
        public bool AddToGroup(GroupParticiantsModel groupParticiant);
        public bool RequestToJoin(JoinRequestModel group);
        public dynamic LoadAllRequestsOfGroup(int groupId);
        public dynamic loadMyRequests(int userId, int groupId);
        public bool RejectJoinRequest(int userId, int groupId);
        public dynamic GetAllUsers(int id);
        public bool ControlAdmin(int id);

    }
}
