namespace TimeTracking.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientOrProjects",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.WorkItems",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(),
                        Description = c.String(nullable: false),
                        Reference = c.String(),
                        ExtraReference = c.String(),
                        IsLogged = c.Boolean(nullable: false),
                        ClientOrProjectGuid = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.ClientOrProjects", t => t.ClientOrProjectGuid)
                .Index(t => t.ClientOrProjectGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItems", "ClientOrProjectGuid", "dbo.ClientOrProjects");
            DropIndex("dbo.WorkItems", new[] { "ClientOrProjectGuid" });
            DropTable("dbo.WorkItems");
            DropTable("dbo.ClientOrProjects");
        }
    }
}
