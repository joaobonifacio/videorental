namespace BoniStreaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@" INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'28553497-952c-489f-9075-e27c29d26402', N'admin@mail.com', 0, N'AKDjaqRPsHcupa8GYL6v97+eTjLnvWUn6TiDqlADyhy3N1rzzgiCIj4nUOFtCI0OiA==', N'507135ab-756b-427e-9dec-dea8567d174d', NULL, 0, 0, NULL, 1, 0, N'admin@mail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7e107bd7-d1e1-445e-a882-53f6115f38a9', N'guest@mail.com', 0, N'AMYJ87W60cVW0WsF4FdPHpyNHpQ/90n/bZ+Zb6Q6CBJOJkqa+NzIetDl/5J244clUA==', N'cfa7703a-23a4-4b9a-a7f9-d094d7a5d74c', NULL, 0, 0, NULL, 1, 0, N'guest@mail.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'969bbff6-6fbe-4c41-92fe-cd9fbe15aa39', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'28553497-952c-489f-9075-e27c29d26402', N'969bbff6-6fbe-4c41-92fe-cd9fbe15aa39')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7e107bd7-d1e1-445e-a882-53f6115f38a9', N'969bbff6-6fbe-4c41-92fe-cd9fbe15aa39')

");
        }
        
        public override void Down()
        {
        }
    }
}
