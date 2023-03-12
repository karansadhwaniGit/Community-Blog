using CommunityBlog.Models.Group;

namespace CommunityBlog.Factory
{
    public interface IGroupsFactory
    {
        public List<GroupModel> GetAllGroups();
        public int CreateGroup(GroupModel group,int id);
        public dynamic GetAllGroupsBySpecificAdmin(int userId);
        public dynamic GetAllUserInNotInSpecificGroup(int groupId,int userId);
        public dynamic GetAllUsersInSpecificGroup(int groupId);
        public bool deleteMemberFromGroup(int participantId, int groupId);
        public bool addMemberToGroup(int participantId, int groupId);
        public dynamic getAllGroupsOfSpecificUser(int userId);
        public dynamic getAllGroupsWhereUsersIsNotParticipant(int userId);
        public bool AddToGroup(int user_id,int group_id);
        public bool RequestToJoin(int user_id, int group_id);
        public dynamic LoadAllRequestsOfGroup(int groupId);
        public dynamic loadMyRequests(int userId, int groupId);
        public bool RejectJoinRequest(int userId, int groupId);
        public dynamic GetAllUsers(int id);
        public bool ControlAdmin(int id);


    }
}
