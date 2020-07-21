namespace BoniStreaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMIgrationHIstory : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM [dbo].[__MigrationHistory]");
        }

        public override void Down()
        {
        }
    }
}
