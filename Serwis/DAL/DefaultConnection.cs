using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serwis.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Serwis.DAL
{
    public class DefaultConnection : DbContext
    {
        public DefaultConnection() : base("DefaultConnection")
        {
        }
       

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Repair_status> Repair_statuses { get; set; }
        public DbSet<Request> Requests { get; set; }


    }
}