namespace Chess.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClubMembers", "SystemClubId", "dbo.Clubs");
            DropIndex("dbo.ClubMembers", new[] { "SystemClubId" });
            CreateIndex("dbo.ClubMembers", "SystemClubId");
            AddForeignKey("dbo.ClubMembers", "SystemClubId", "dbo.Clubs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClubMembers", "SystemClubId", "dbo.Clubs");
            DropIndex("dbo.ClubMembers", new[] { "SystemClubId" });
            CreateIndex("dbo.ClubMembers", "SystemClubId");
            AddForeignKey("dbo.ClubMembers", "SystemClubId", "dbo.Clubs", "Id");
        }
    }
}
