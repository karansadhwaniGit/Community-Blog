namespace CommunityBlog.Models.Group
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Type { get; set; }
        public string? Description { get; set; }
        public string? Created_At { get; set; }
    }
}
