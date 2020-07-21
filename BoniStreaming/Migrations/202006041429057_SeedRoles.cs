namespace BoniStreaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'969bbff6-6fbe-4c41-92fe-cd9fbe15aa39', N'CanManageMovies')");
        }
        
        public override void Down()
        {
        }
    }
}
