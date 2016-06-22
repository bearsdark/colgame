namespace ColGameServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColGame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharacterLogs",
                c => new
                    {
                        CharacterLogID = c.Int(nullable: false, identity: true),
                        CharID = c.Int(nullable: false),
                        CharPosition = c.String(nullable: false, maxLength: 20),
                        CharHp = c.Int(nullable: false),
                        CharMana = c.Int(nullable: false),
                        Character_CharacterID = c.Int(),
                    })
                .PrimaryKey(t => t.CharacterLogID)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterID)
                .Index(t => t.Character_CharacterID);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        CharacterID = c.Int(nullable: false, identity: true),
                        CharacterName = c.String(nullable: false, maxLength: 20),
                        CharacterTexture = c.String(nullable: false, maxLength: 50),
                        CharacterHp = c.Int(nullable: false),
                        CharacterMana = c.Int(nullable: false),
                        CharacterClass = c.String(nullable: false, maxLength: 20),
                        CharacterMoney = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharacterID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ItemChars",
                c => new
                    {
                        ItemCharID = c.Int(nullable: false, identity: true),
                        IC_CharID = c.Int(nullable: false),
                        IC_ItemID = c.Int(nullable: false),
                        IC_Quantity = c.Int(nullable: false),
                        Character_CharacterID = c.Int(),
                    })
                .PrimaryKey(t => t.ItemCharID)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterID)
                .Index(t => t.Character_CharacterID);
            
            CreateTable(
                "dbo.SkillChars",
                c => new
                    {
                        SkillCharacterID = c.Int(nullable: false, identity: true),
                        SC_CharID = c.Int(nullable: false),
                        SC_SkillID = c.Int(nullable: false),
                        SC_Level = c.Int(nullable: false),
                        Character_CharacterID = c.Int(),
                    })
                .PrimaryKey(t => t.SkillCharacterID)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterID)
                .Index(t => t.Character_CharacterID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 24),
                        Password = c.String(nullable: false, maxLength: 32),
                        Email = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "UserID", "dbo.Users");
            DropForeignKey("dbo.SkillChars", "Character_CharacterID", "dbo.Characters");
            DropForeignKey("dbo.ItemChars", "Character_CharacterID", "dbo.Characters");
            DropForeignKey("dbo.CharacterLogs", "Character_CharacterID", "dbo.Characters");
            DropIndex("dbo.SkillChars", new[] { "Character_CharacterID" });
            DropIndex("dbo.ItemChars", new[] { "Character_CharacterID" });
            DropIndex("dbo.Characters", new[] { "UserID" });
            DropIndex("dbo.CharacterLogs", new[] { "Character_CharacterID" });
            DropTable("dbo.Users");
            DropTable("dbo.SkillChars");
            DropTable("dbo.ItemChars");
            DropTable("dbo.Characters");
            DropTable("dbo.CharacterLogs");
        }
    }
}
