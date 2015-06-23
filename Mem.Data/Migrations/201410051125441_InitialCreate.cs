namespace Mem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        TwoName = c.String(unicode: false),
                        TestName = c.String(unicode: false),
                        ThreeName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SettingsAuthorityClassify",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SettingsAuthority",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        AuthorityClassifyId = c.Long(nullable: false),
                        Controller = c.String(unicode: false),
                        Action = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SettingsRoleAuthorities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SettingsRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SettingsSiteMenu",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        Content = c.String(maxLength: 500, unicode: false, storeType: "nvarchar"),
                        ImageUrl = c.String(maxLength: 500, unicode: false, storeType: "nvarchar"),
                        ParentId = c.Long(nullable: false),
                        SrcUrl = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SettingsUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SettingsUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SettingsUserRoles");
            DropTable("dbo.SettingsUsers");
            DropTable("dbo.SettingsSiteMenu");
            DropTable("dbo.SettingsRoles");
            DropTable("dbo.SettingsRoleAuthorities");
            DropTable("dbo.SettingsAuthority");
            DropTable("dbo.SettingsAuthorityClassify");
            DropTable("dbo.Category");
        }
    }
}
