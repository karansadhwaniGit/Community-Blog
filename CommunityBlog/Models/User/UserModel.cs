using CommunityBlog.Models.Group;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityBlog.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? Type { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public int choicecheck { get; set; }
    }
}
