using DemoProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoProjectMVC
{
    public class DemoContext:DbContext
    {
        public DemoContext()
            :base("DemoContext")
        {

        }
       public DbSet<Category> Category { get; set; }
       public DbSet<Product> Product{ get; set; }

        //public System.Data.Entity.DbSet<DemoProjectMVC.Models.Category> Categories { get; set; }
    }
}