namespace Serwis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Imię = c.String(nullable: false, maxLength: 50),
                        Nazwisko = c.String(nullable: false, maxLength: 50),
                        Adres = c.String(),
                        Miasto = c.String(),
                        Nr_telefonu = c.String(),
                        Adres_email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Repair_status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequestID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        Data_rozpoczęcia = c.DateTime(nullable: false),
                        Data_zakończenia = c.DateTime(nullable: false),
                        Status = c.String(),
                        Opis_naprawy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        Typ_urządzenia = c.Int(),
                        Model = c.String(),
                        Opis_usterki = c.String(),
                        Uwagi = c.String(),
                        Czy_gwarancja = c.Boolean(nullable: false),
                        Repair_status_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Repair_status", t => t.Repair_status_ID)
                .Index(t => t.ClientID)
                .Index(t => t.EmployeeID)
                .Index(t => t.Repair_status_ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Imię = c.String(maxLength: 50),
                        Nazwisko = c.String(maxLength: 50),
                        Stanowisko = c.Int(),
                        Adres_email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "Repair_status_ID", "dbo.Repair_status");
            DropForeignKey("dbo.Requests", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Requests", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.Repair_status", "ClientID", "dbo.Clients");
            DropIndex("dbo.Requests", new[] { "Repair_status_ID" });
            DropIndex("dbo.Requests", new[] { "EmployeeID" });
            DropIndex("dbo.Requests", new[] { "ClientID" });
            DropIndex("dbo.Repair_status", new[] { "ClientID" });
            DropTable("dbo.Employees");
            DropTable("dbo.Requests");
            DropTable("dbo.Repair_status");
            DropTable("dbo.Clients");
        }
    }
}
