using FluentMigrator;
using System.Data.SqlServerCe;

namespace CommunityBlog.Migrations
{
    [Migration(130920221510)]
    public class Migrations : Migration
    {
        public override void Down()
        {
            Delete.Table("Comments_Post");
            Delete.Table("Comments");
            Delete.Table("Likes_Post");
            Delete.Table("Post");
            Delete.Table("JoinRequests");
            Delete.Table("AdminsGroups");
            Delete.Table("GroupsParticipants");
            Delete.Table("Groups");
            Delete.Table("Token");
            Delete.Table("Users");
            
        }

        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("ID").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("UserName").AsString().NotNullable()
                .WithColumn("Phone").AsString().NotNullable()
                .WithColumn("Type").AsString().NotNullable()
                .WithColumn("Country").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("Gender").AsString()
                .WithColumn("Created_At").AsString().WithDefaultValue(DateTime.UtcNow)
                .WithColumn("choicecheck").AsInt32().NotNullable().WithDefaultValue(0);

            Create.Table("Token")
                .WithColumn("ID").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("User_Id").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("Token").AsString().NotNullable()
                .WithColumn("Expires_At").AsString().NotNullable()
                .WithColumn("Is_Remember").AsInt32().NotNullable();

            Create.Table("Groups")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Created_At").AsString().NotNullable();

            Create.Table("GroupsParticipants")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("Users", "Id")
                .WithColumn("GroupId").AsInt32().NotNullable().ForeignKey("Groups", "Id")
                    .OnDeleteOrUpdate(System.Data.Rule.Cascade);

            Create.Table("AdminsGroups")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("Users", "Id")
                .WithColumn("GroupId").AsInt32().NotNullable().ForeignKey("Groups", "Id")
                    .OnDeleteOrUpdate(System.Data.Rule.Cascade);

            Create.Table("JoinRequests")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("GroupId").AsInt32().NotNullable().ForeignKey("Groups", "Id")
                    .OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("Users", "Id")
                    .OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("Status").AsString().NotNullable()
                .WithColumn("CreatedAt").AsString().NotNullable()
                    .WithDefaultValue(DateTime.Now)
                .WithColumn("UpdatedAt").AsString().NotNullable()
                    .WithDefaultValue(DateTime.Now);

            Create.Table("Post")
                 .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                 .WithColumn("GroupId").AsInt32().NotNullable().ForeignKey("Groups", "Id")
                     .OnDeleteOrUpdate(System.Data.Rule.Cascade)
                 .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("Users", "Id")
                     .OnDeleteOrUpdate(System.Data.Rule.Cascade)
                 .WithColumn("Type").AsInt32().NotNullable()
                 .WithColumn("Content").AsString().NotNullable()
                 .WithColumn("CreatedAt").AsDateTime().NotNullable()
                     .WithDefaultValue(System.DateTime.Now);

            Create.Table("Likes_Post")
                 .WithColumn("ID").AsInt32().PrimaryKey().NotNullable().Identity()
                 .WithColumn("PID").AsInt32().ForeignKey("Post", "ID").NotNullable()
                 .WithColumn("UID").AsInt32().ForeignKey("Users", "ID").NotNullable();

            Create.Table("Comments")
                .WithColumn("ID").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("UID").AsInt32().ForeignKey("Users", "ID").NotNullable()
                .WithColumn("Content").AsString().NotNullable()
                .WithColumn("Created_At").AsDateTime2().Nullable().WithDefaultValue(System.DateTime.UtcNow);

            Create.Table("Comments_Post")
                .WithColumn("ID").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("PID").AsInt32().ForeignKey("Post", "ID").NotNullable()
                .WithColumn("CID").AsInt32().ForeignKey("Comments", "ID").NotNullable();
        }
    }
}
