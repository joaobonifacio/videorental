namespace BoniStreaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createAspNetRolesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
"dbo.AspNetRoles",
c => new
{
    Id = c.String(nullable: false, maxLength: 128),
    Name = c.String(nullable: false, maxLength: 256),
})
.PrimaryKey(t => t.Id)
.Index(t => t.Name, unique: true, name: "RoleNameIndex");
        }
        
        public override void Down()
        {
        }
    }
}
