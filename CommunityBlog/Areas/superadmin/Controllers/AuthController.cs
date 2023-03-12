using CommunityBlog.Factory;
using CommunityBlog.Models;
using CommunityBlog.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;

namespace CommunityBlog.Areas.superadmin.Controllers
{
    [Area("superadmin")]
    [Route("superadmin")]
    public class AuthController : Controller
    {
        private readonly IAuthFactory _auth;
        private readonly ITokenHandlerFactory _token;
        private readonly IGroupsFactory _groups;
        MailService? mailService;
        public string id;

        public AuthController(IAuthFactory auth, ITokenHandlerFactory token,IGroupsFactory groups)
        {
            _auth = auth;
            _token = token;
            _groups = groups;
        }
        [Route("")]
        public IActionResult Index()
        {
            id = @HttpContext.Session.GetString("user_id");
            string token = Request.Cookies["token"];
            ViewData["Users"] = _auth.GetAllUsers();
            if (token != null)
            {
                if (_token.IsTokenValid(token, 1))
                {
                    if (id != null)
                    {
                        string type = _auth.GetTypeOfUser(int.Parse(id));
                        if (type != null && type == "superadmin")
                        {
                            
                            return View();
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                if (id != null)
                {
                    string type = _auth.GetTypeOfUser(int.Parse(id));
                    if (type != null && type == "superadmin")
                    {
                        
                        return View();
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Auth");
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            string token = Request.Cookies["token"];
            if (token != null)
            {
                if (_token.IsTokenValid(token, 1))
                {
                    int id = _token.GetIDFromToken(token, 1);
                    @HttpContext.Session.SetString("user_id", id.ToString());
                    return RedirectToAction("Index", new { area = "superadmin" });
                }
                else
                {
                    Response.Cookies.Delete("token");
                    return RedirectToAction("Login");
                }
            }
            return View("Login");
        }

        [Route("CheckCredentials")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckCredentials(IFormCollection keyValuePairs)
        {
            var user = keyValuePairs;
            int result = user["remember_me"] == "on" ? _auth.IsAuthenticUser(user["username"], user["password"], user["type"], 1,1) : _auth.IsAuthenticUser(user["username"], user["password"], user["type"], 0,1);
            string msg = "";
            switch (result)
            {
                case 1:
                    if (user["remember_me"] == "on")
                    {
                        int id = _auth.GetUserIDFromUsername(keyValuePairs["username"]);
                        CookieOptions cookie = new CookieOptions();
                        cookie.Expires = DateTime.Now.AddHours(1);
                        cookie.Secure = true;
                        @HttpContext.Session.SetString("user_id", id.ToString());
                        TokenModel newToken = _token.CreateToken(id, 1);
                        Response.Cookies.Append("token", newToken.Token, cookie);
                    }
                    else
                    {
                        int id = _auth.GetUserIDFromUsername(keyValuePairs["username"]);
                        @HttpContext.Session.SetString("user_id", id.ToString());
                        return RedirectToAction("Index","Auth");
                    }
                    break;
                case 2:
                    msg = "Only SuperAdmins can Access this!";
                    break;
                case 3:
                    msg = "Credentials Not matched, Please Check Username and Password!";
                    break;
                case 4:
                    msg = "Something went wrong username or password cannot be empty!";
                    break;
            }
            ViewData.Add("msg", msg);

            if (msg != "")
                return View("Login");
            return RedirectToAction("Login");
        }

        [Route("ForgotPassword")]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var id = HttpContext.Session.GetString("user_id");
            if (id != null)
            {
                if (_token.GetValidExistingToken(int.Parse(id), 1) == null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }
        [Route("ForgotPassword")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ForgotPassword(IFormCollection keyValuePairs)
        {

            string username = keyValuePairs["username"];
            if (username != null)
            {
                int user_id = _auth.GetUserIDFromUsername(username);
                if (user_id != -1)
                {
                    ViewData["msg"] = "The Link to change the password has been sent on registered Email-ID the link will expire in 10 mins";
                    var token = _token.CreateToken(user_id, 0);
                    UserModel user = _auth.GetSingleUserData(username);
                    var mailModel = new MailMessage("Chatify@gmail.com", user.Email);
                    mailModel.Subject = "Request for change of password!";
                    mailModel.IsBodyHtml = true;

                    mailModel.Body = "The Link for changing your password is " + String.Format("<a href=\"https://localhost:7154/{0}/ResetPassword/{1}\">Reset Link</a>", user.Type, token.Token) + " this link will expire in 10 minutes ";
                    mailService = new MailService();
                    mailService.sendMail(mailModel);
                }
                else
                {
                    ViewData["msg"] = "Cannot find the account with given username";
                }

            }
            return View();
        }

        [Route("ResetPassword/{token?}")]
        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            string id = HttpContext.Session.GetString("user_id");
            if (id == null)
            {
                if (_token.GetValidExistingToken(int.Parse(id), 1) == null)
                {
                    if (_token.IsTokenValid(token, 0))
                    {
                        ViewData["isValid"] = true;
                        return View("ResetPassword", "Already Logged In? How Can You");
                    }
                }
            }
            else
            {
                ViewData["isValid"] = false;
                return View();
            }
            return RedirectToAction("Index");
        }
        [Route("ResetPassword/{token?}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ResetPassword(IFormCollection keyValuePairs, string token)
        {
            var password = keyValuePairs["password"];
            _auth.ChangePassword(token, password.ToString());
            return RedirectToAction("Login");
        }

       
    }
}
