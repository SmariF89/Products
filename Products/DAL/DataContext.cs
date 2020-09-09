using Products.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Products.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base ("ProductsLive")
        {
            Database.SetInitializer<DataContext> (new DropCreateDatabaseAlways<DataContext>());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}