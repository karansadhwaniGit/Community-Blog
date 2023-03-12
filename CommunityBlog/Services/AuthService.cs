using CommunityBlog.Models.Group;
using CommunityBlog.Models.User;

namespace CommunityBlog.Services
{
    public class AuthService:IAuthService
    {
        private readonly DataContext context;
        public AuthService(DataContext dataContext)
        {
            context = dataContext;
        }
        public void CreateNormalUser(UserModel user)
        {
            if (user != null)
            {
                if (user.Type != "superadmin")
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }
        public UserModel IsUserAuthentic(string username)
        {
            //checks weather the user is an authentic user!
            var user=context.Users
                .Where(x => x.UserName == username).FirstOrDefault();
            return user!=null?user:null;
        }
        public int GetUserIDFromUsername(string username)
        {
            var user= context.Users.Where(x => x.UserName ==
            username).FirstOrDefault();
            if (user!=null)
            {
                return user.Id;
            }
            return -1;
        }
        public UserModel GetSingleUserData(string username)
        {
            var user = context.Users.Where(x => x.UserName ==
            username).FirstOrDefault();
            return user;
        }
        public int ChangePassword(int user_id,string password)
        {
            var user = context.Users.Where(x => x.Id == user_id).FirstOrDefault();
            if (user != null)
            {
                user.Password = password;
                context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public string GetTypeOfUser(int id)
        {
            var userType = context.Users.Where(x => x.Id == id).FirstOrDefault().Type;
            if(userType!=null)
            {
                return userType.ToString();
            }
            return null;
        }
        public int AddUser(UserModel user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user.Id;
        }
        public bool AddUserToGroups(List<int> groups,int user_id)
        {
            GroupParticiantsModel participant;
            foreach (var item in groups)
            {
                participant = new GroupParticiantsModel();
                participant.UserId = user_id;
                participant.GroupId = item;
                context.GroupsParticipants.Add(participant);
                context.SaveChanges();
            }
            var user = context.Users.Where(x => x.Id == user_id).FirstOrDefault();
            if (user != null)
            {
                user.choicecheck = 1;
                context.SaveChanges();
            }
           
            return true;
        }
        public List<UserModel> GetAllUsers()
        {
            var users = context.Users.ToList();
            return users;
        }
        public UserModel GetUserById(int id)
        {
            return context.Users.Where(x => x.Id==id).FirstOrDefault();
        }
    }
}
