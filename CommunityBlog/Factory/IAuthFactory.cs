using CommunityBlog.Models.User;

namespace CommunityBlog.Factory
{
    public interface IAuthFactory
    {
        int IsAuthenticUser(string username, string password,string type,int remember_me,int check);
        public int GetUserIDFromUsername(string username);
        public UserModel GetSingleUserData(string username);
        public bool ChangePassword(string token, string password);
        public string GetTypeOfUser(int id);
        public int AddUser(UserModel user);
        public bool AddUserToGroups(List<int> groups,int user_id);
        public List<UserModel> GetAllUsers();
        public UserModel GetUserById(int id);

    }
}
