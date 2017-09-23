namespace Business_Management.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false),
                        MiddleNames = c.String(),
                        LastName = c.String(maxLength: 50),
                        Gender = c.Byte(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        Title_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Titles", t => t.Title_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Title_Id);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.ClientAddresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressLine1 = c.String(nullable: false),
                        AddressLine2 = c.String(),
                        AddressLine3 = c.String(),
                        AddressLine4 = c.String(),
                        AddressLine5 = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Client_Id = c.Guid(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                        Type_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.ClientAddressTypes", t => t.Type_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        MiddleNames = c.String(),
                        LastName = c.String(),
                        Gender = c.Byte(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                        Title_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.Titles", t => t.Title_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Title_Id);
            
            CreateTable(
                "dbo.ClientAddressTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.ClientCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.ClientCategoryLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Category_Id = c.Guid(nullable: false),
                        Client_Id = c.Guid(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientCategories", t => t.Category_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.ClientContacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Client_Id = c.Guid(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                        Type_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.ClientContactTypes", t => t.Type_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ClientContactTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.ClientTransactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Bank_Id = c.Guid(),
                        Client_Id = c.Guid(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                        Type_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banks", t => t.Bank_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.ClientTransactionTypes", t => t.Type_Id)
                .Index(t => t.Bank_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ClientTransactionTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Paid = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Client_Id = c.Guid(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                        Transaction_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.ClientTransactions", t => t.Transaction_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Transaction_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                        Type_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.ProductTypes", t => t.Type_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.ProductSales",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Client_Id = c.Guid(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        Invoice_Id = c.Guid(),
                        ModifiedBy_Id = c.Guid(),
                        Product_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Invoice_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ServiceIntervals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Interval = c.Time(nullable: false, precision: 7),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        Interval_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                        Type_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.ServiceIntervals", t => t.Interval_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.ServiceTypes", t => t.Type_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Interval_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.ServiceTimeLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Started = c.DateTime(nullable: false),
                        Ended = c.DateTime(),
                        Current = c.Boolean(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Client_Id = c.Guid(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        Invoice_Id = c.Guid(),
                        ModifiedBy_Id = c.Guid(),
                        Service_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Invoice_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Service_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.UserRoleLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                        Role_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.UserRoles", t => t.Role_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                        ModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoleLinks", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoleLinks", "Role_Id", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoles", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoleLinks", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoleLinks", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Settings", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Settings", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ServiceTimeLogs", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ServiceTimeLogs", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceTimeLogs", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ServiceTimeLogs", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.ServiceTimeLogs", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ServiceTimeLogs", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Services", "Type_Id", "dbo.ServiceTypes");
            DropForeignKey("dbo.ServiceTypes", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ServiceTypes", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Services", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Services", "Interval_Id", "dbo.ServiceIntervals");
            DropForeignKey("dbo.Services", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ServiceIntervals", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ServiceIntervals", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductSales", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ProductSales", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductSales", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductSales", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.ProductSales", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductSales", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Products", "Type_Id", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductTypes", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductTypes", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Invoices", "Transaction_Id", "dbo.ClientTransactions");
            DropForeignKey("dbo.Invoices", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Invoices", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Invoices", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ClientTransactions", "Type_Id", "dbo.ClientTransactionTypes");
            DropForeignKey("dbo.ClientTransactionTypes", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientTransactionTypes", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientTransactions", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientTransactions", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientTransactions", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ClientTransactions", "Bank_Id", "dbo.Banks");
            DropForeignKey("dbo.ClientContacts", "Type_Id", "dbo.ClientContactTypes");
            DropForeignKey("dbo.ClientContactTypes", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientContactTypes", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientContacts", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientContacts", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientContacts", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ClientCategoryLinks", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientCategoryLinks", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientCategoryLinks", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ClientCategoryLinks", "Category_Id", "dbo.ClientCategories");
            DropForeignKey("dbo.ClientCategories", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientCategories", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientAddresses", "Type_Id", "dbo.ClientAddressTypes");
            DropForeignKey("dbo.ClientAddressTypes", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientAddressTypes", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientAddresses", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientAddresses", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ClientAddresses", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Clients", "Title_Id", "dbo.Titles");
            DropForeignKey("dbo.Clients", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Clients", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Banks", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Banks", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Title_Id", "dbo.Titles");
            DropForeignKey("dbo.Titles", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Titles", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "CreatedBy_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.UserRoles", new[] { "CreatedBy_Id" });
            DropIndex("dbo.UserRoleLinks", new[] { "User_Id" });
            DropIndex("dbo.UserRoleLinks", new[] { "Role_Id" });
            DropIndex("dbo.UserRoleLinks", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.UserRoleLinks", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Settings", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Settings", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ServiceTimeLogs", new[] { "User_Id" });
            DropIndex("dbo.ServiceTimeLogs", new[] { "Service_Id" });
            DropIndex("dbo.ServiceTimeLogs", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ServiceTimeLogs", new[] { "Invoice_Id" });
            DropIndex("dbo.ServiceTimeLogs", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ServiceTimeLogs", new[] { "Client_Id" });
            DropIndex("dbo.ServiceTypes", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ServiceTypes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Services", new[] { "Type_Id" });
            DropIndex("dbo.Services", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Services", new[] { "Interval_Id" });
            DropIndex("dbo.Services", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ServiceIntervals", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ServiceIntervals", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ProductSales", new[] { "User_Id" });
            DropIndex("dbo.ProductSales", new[] { "Product_Id" });
            DropIndex("dbo.ProductSales", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ProductSales", new[] { "Invoice_Id" });
            DropIndex("dbo.ProductSales", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ProductSales", new[] { "Client_Id" });
            DropIndex("dbo.ProductTypes", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ProductTypes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Products", new[] { "Type_Id" });
            DropIndex("dbo.Products", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Products", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Invoices", new[] { "Transaction_Id" });
            DropIndex("dbo.Invoices", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Invoices", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Invoices", new[] { "Client_Id" });
            DropIndex("dbo.ClientTransactionTypes", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ClientTransactionTypes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ClientTransactions", new[] { "Type_Id" });
            DropIndex("dbo.ClientTransactions", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ClientTransactions", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ClientTransactions", new[] { "Client_Id" });
            DropIndex("dbo.ClientTransactions", new[] { "Bank_Id" });
            DropIndex("dbo.ClientContactTypes", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ClientContactTypes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ClientContacts", new[] { "Type_Id" });
            DropIndex("dbo.ClientContacts", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ClientContacts", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ClientContacts", new[] { "Client_Id" });
            DropIndex("dbo.ClientCategoryLinks", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ClientCategoryLinks", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ClientCategoryLinks", new[] { "Client_Id" });
            DropIndex("dbo.ClientCategoryLinks", new[] { "Category_Id" });
            DropIndex("dbo.ClientCategories", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ClientCategories", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ClientAddressTypes", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ClientAddressTypes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Clients", new[] { "Title_Id" });
            DropIndex("dbo.Clients", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Clients", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ClientAddresses", new[] { "Type_Id" });
            DropIndex("dbo.ClientAddresses", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.ClientAddresses", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ClientAddresses", new[] { "Client_Id" });
            DropIndex("dbo.Titles", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Titles", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Users", new[] { "Title_Id" });
            DropIndex("dbo.Users", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Banks", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Banks", new[] { "CreatedBy_Id" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserRoleLinks");
            DropTable("dbo.Settings");
            DropTable("dbo.ServiceTimeLogs");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceIntervals");
            DropTable("dbo.ProductSales");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.Invoices");
            DropTable("dbo.ClientTransactionTypes");
            DropTable("dbo.ClientTransactions");
            DropTable("dbo.ClientContactTypes");
            DropTable("dbo.ClientContacts");
            DropTable("dbo.ClientCategoryLinks");
            DropTable("dbo.ClientCategories");
            DropTable("dbo.ClientAddressTypes");
            DropTable("dbo.Clients");
            DropTable("dbo.ClientAddresses");
            DropTable("dbo.Titles");
            DropTable("dbo.Users");
            DropTable("dbo.Banks");
        }
    }
}
