using Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.DAL
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var categories = new List<Category>
            {
                new Category {ID = 0, description = "All Categories" },
                new Category {ID = 1, description = "Books & Audible" },
                new Category {ID = 2, description = "Movies, Music & Games" },
                new Category {ID = 3, description = "Electronics & Computers" },
                new Category {ID = 4, description = "Home, Garden & Tools" },
                new Category {ID = 5, description = "Beauty, Health & Grocery" },
                new Category {ID = 6, description = "Toys, Kids & Baby" },
                new Category {ID = 7, description = "Clothing, Shoes & Jewelry" },
                new Category {ID = 8, description = "Sport & Outdoors" },
                new Category {ID = 9, description = "Automative & Industrial" }
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product { ID = 1, name = "Lego Minecraft 38900", description = "Lorem ipsum blablabla", quantity = 18, price = 199, category = context.Categories.Where(x => x.ID == 6).SingleOrDefault()},
                new Product { ID = 2, name = "Nivea Deodorant 30ml", description = "Lorem ipsum blablabla", quantity = 8, price = 39, category = context.Categories.Where(x => x.ID == 5).SingleOrDefault()}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

        }

    }
}