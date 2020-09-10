using Products.DAL;
using Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Products.Models
{
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Products()
        {
            return RedirectToAction("Index");
        }

        public ActionResult ProductsBySearch(string str)
        {
            IEnumerable<Product> products = db.Products.Where(prod => prod.name.ToLower().Contains(str.ToLower()) ||
                                                                      prod.description.ToLower().Contains(str.ToLower()));

            return View("Index", products);
        }

        public ActionResult ProductsByCategory(int? id)
        {
            if(id == 0)
            {
                return RedirectToAction("Index");
            }

            Category category = db.Categories.Where(x => x.ID == id).SingleOrDefault();

            IEnumerable<Product> products = (from prod in db.Products
                                         orderby prod.name ascending
                                         join cat in db.Categories on prod.category.ID equals cat.ID
                                         where cat.description == category.description
                                         select prod).ToList();
            return View("Index", products);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        { 
            if(id.HasValue)
            {
                Product p = db.Products.Where(x => x.ID == id.Value).SingleOrDefault();
                if (p != null)
                {
                    ProductViewModel model = new ProductViewModel();
                    model.ID = p.ID;
                    model.Name = p.name;
                    model.Description = p.description;
                    model.Stock = p.quantity;
                    model.Price = p.price;
                    model.categoryID = p.category.ID;
                    model.availableCategories = PopulateDropDownData();

                    return View(model);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                Product p = db.Products.Where(x => x.ID == model.ID).SingleOrDefault();
                if(p != null)
                {
                    p.name = model.Name;
                    p.description = model.Description;
                    p.quantity = model.Stock;
                    p.price = model.Price;
                    p.category = db.Categories.Where(x => x.ID == (model.categoryID - 1)).SingleOrDefault();
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            model.availableCategories = PopulateDropDownData();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel model = new ProductViewModel();
            model.availableCategories = PopulateDropDownData();

            return View(model);
        }

        [HttpPost] 
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product p = new Product();

                p.name = model.Name;
                p.description = model.Description;
                p.quantity = model.Stock;
                p.price = model.Price;
                p.category = db.Categories.Where(x => x.ID == (model.categoryID - 1)).SingleOrDefault();

            
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            model.availableCategories = PopulateDropDownData();
            return View(model);
        }

        public List<SelectListItem> PopulateDropDownData()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem() { Value = "", Text = "- Choose a category -" });

            db.Categories.ToList().ForEach((x) => {categories.Add(new SelectListItem() { Value = x.ID.ToString(), Text = x.description });});
            return categories;
        }
    }
}