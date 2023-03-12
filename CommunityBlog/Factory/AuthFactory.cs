using System;
using CommunityBlog.Models.User;
using CommunityBlog.Services;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http.Features;

namespace CommunityBlog.Factory
{
    public class AuthFactory:IAuthFactory
    {
        private readonly IAuthService _context;
        private readonly ITokenHandlerFactory _token;
        public AuthFactory(IAuthService context,ITokenHandlerFactory token)
        {
            _context = context;
            _token = token;
        }
         
        public int IsAuthenticUser(string username, string password,string type,int remember_me,int checkType)
        {
            if (username.Length>0 && password.Length > 0 && type.Length > 0)
            {
                UserModel user=_context.IsUserAuthentic(username);
                if (user != null)
                {
                    if (Hash.DecryptHash(password, user.Password))
                    {
                        if (checkType == 0)
                        {
                            return 1;
                        }
                        else
                        {
                            if (user.Type == type)
                            {
                                return 1;// all the credentials matched
                            }
                            else
                                return 2;//type not matched        
                        }
                    }
                    else
                        return 3;//user/password not correct
                }
                else
                    return 3;
            }
            else
                return 4;//credentials null
        }
        public int GetUserIDFromUsername(string username)
        {
            return _context.GetUserIDFromUsername(username);
        }
        public UserModel GetSingleUserData(string username)
        {
            return _context.GetSingleUserData(username);
        }
        public bool ChangePassword(string token, string password)
        {
            if(token!=null && password!="")
            {
                int id = _token.GetIDFromToken(token, 0);
                if (id!=-1)
                {
                    string hash = Hash.CreateHash(password);
                    _context.ChangePassword(id, hash);
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        public string GetTypeOfUser(int id)
        {
            string type= _context.GetTypeOfUser(id);
            return type;
        }
        public int AddUser(UserModel user)
        {
            user.Type = "user";
            user.Password = Hash.CreateHash(user.Password);
            return _context.AddUser(user);
        }
        public bool AddUserToGroups(List<int> groups,int user_id)
        {
            if(groups.Count>0 && groups.Count<=5)
            {
                _context.AddUserToGroups(groups, user_id);  
            }
            
            return true;
        }
        public List<UserModel> GetAllUsers()
        {
            return _context.GetAllUsers();
        }
        public UserModel GetUserById(int id)
        {
            return _context.GetUserById(id);
        }
    }
}

