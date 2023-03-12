using CommunityBlog.Models.User;

namespace CommunityBlog.Services
{
    public interface IAuthService
    {
        UserModel IsUserAuthentic(string username);
        public int GetUserIDFromUsername(string username);
        public UserModel GetSingleUserData(string username);
        public int ChangePassword(int user_id, string password);
        public string GetTypeOfUser(int id);
        public int AddUser(UserModel user);
        public bool AddUserToGroups(List<int> groups,int user_id);

        public List<UserModel> GetAllUsers();
        public UserModel GetUserById(int id);
    }
}
