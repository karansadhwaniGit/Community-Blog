using CommunityBlog.Factory;
using CommunityBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace CommunityBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenHandlerFactory _token;
        public HomeController(ILogger<HomeController> logger, ITokenHandlerFactory token)
        {
            _logger = logger;
            _token = token;
        }
        public IActionResult Index()
        {
            string token = Request.Cookies["token"];
            if (token != null)
            {
                if (_token.IsTokenValid(token, 1))
                {
                    int id=_token.GetIDFromToken(token,1);
                    @HttpContext.Session.SetString("user_id", id.ToString());
                    return View();
                }
                else
                {
                    Response.Cookies.Delete("token");
                    return RedirectToAction("Login", new { area = "superadmin" });
                }
            }
            return RedirectToAction("SignUp","Auth");
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}