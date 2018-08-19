using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;  

namespace Mvc4Crud.Models
{
    public class ApplicationDbContext : DbContext  
    {
        public ApplicationDbContext()
            : base("tdlConnection")
        {
        }        
        public DbSet<Tasks> Events { get; set; }  
    }
}