namespace Chess.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClubMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClubId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        PlatformId = c.String(),
                        ClubMembersState = c.Int(nullable: false),
                        IsClubManger = c.Boolean(nullable: false),
                        IsOwner = c.Boolean(nullable: false),
                        SystemClubId = c.Int(),
                        SystemUserId = c.Int(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clubs", t => t.SystemClubId)
                .ForeignKey("dbo.SystemUsers", t => t.SystemUserId)
                .Index(t => t.SystemClubId)
                .Index(t => t.SystemUserId);
            
            CreateTable(
                "dbo.Clubs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClubId = c.Int(nullable: false),
                        ClubName = c.String(),
                        ShortLink = c.String(),
                        MangerShortLink = c.String(),
                        PlatformName = c.String(),
                        PayMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Double(nullable: false),
                        PayRemark = c.String(),
                        SystemUserId = c.Int(),
                        PlatformId = c.Int(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Platforms", t => t.PlatformId)
                .Index(t => t.PlatformId);
            
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlatformId = c.String(),
                        PlatformName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecordLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecId = c.Int(nullable: false),
                        ClubId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        ReplayId = c.String(),
                        Score = c.Int(nullable: false),
                        MemberOpenid = c.String(),
                        PayMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayState = c.Int(nullable: false),
                        PlatformId = c.Int(),
                        SystemUserId = c.Int(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SystemUsers", t => t.SystemUserId)
                .ForeignKey("dbo.Platforms", t => t.PlatformId)
                .Index(t => t.SystemUserId)
                .Index(t => t.PlatformId);
            
            CreateTable(
                "dbo.SystemUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        UserName = c.String(),
                        GameId = c.Int(),
                        Sex = c.Int(nullable: false),
                        NickName = c.String(),
                        HeadImgUrl = c.String(),
                        HeadImg = c.Binary(),
                        PayCodeImg = c.Binary(),
                        PayCodeUrl = c.String(),
                        UserIdentity = c.Int(nullable: false),
                        Openid = c.String(),
                        Unionid = c.String(),
                        AgentId = c.String(),
                        AgentPassWord = c.String(),
                        CheckKey = c.String(),
                        PlatformId = c.Int(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Platforms", t => t.PlatformId)
                .Index(t => t.PlatformId);
            
            CreateTable(
                "dbo.OperationRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Content = c.String(),
                        ActionUrl = c.String(),
                        SystemUserId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SystemUsers", t => t.SystemUserId, cascadeDelete: true)
                .Index(t => t.SystemUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClubMembers", "SystemUserId", "dbo.SystemUsers");
            DropForeignKey("dbo.ClubMembers", "SystemClubId", "dbo.Clubs");
            DropForeignKey("dbo.Clubs", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.RecordLogs", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.RecordLogs", "SystemUserId", "dbo.SystemUsers");
            DropForeignKey("dbo.SystemUsers", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.OperationRecords", "SystemUserId", "dbo.SystemUsers");
            DropIndex("dbo.ClubMembers", new[] { "SystemUserId" });
            DropIndex("dbo.ClubMembers", new[] { "SystemClubId" });
            DropIndex("dbo.Clubs", new[] { "PlatformId" });
            DropIndex("dbo.RecordLogs", new[] { "PlatformId" });
            DropIndex("dbo.RecordLogs", new[] { "SystemUserId" });
            DropIndex("dbo.SystemUsers", new[] { "PlatformId" });
            DropIndex("dbo.OperationRecords", new[] { "SystemUserId" });
            DropTable("dbo.OperationRecords");
            DropTable("dbo.SystemUsers");
            DropTable("dbo.RecordLogs");
            DropTable("dbo.Platforms");
            DropTable("dbo.Clubs");
            DropTable("dbo.ClubMembers");
        }
    }
}
