namespace CommunityBlog.Models
{
    public class TokenModel
    { 
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string? Token { get; set; }
        public string? Expires_At { get; set; }
        public int Is_Remember { get; set; }
    }
}
