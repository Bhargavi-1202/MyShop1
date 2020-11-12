using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCatagoryManagerController : Controller
    {
        InMemoryRepository<ProductCatagory> context;

        public ProductCatagoryManagerController()
        {
            context = new InMemoryRepository<ProductCatagory>();
        }
        // GET: ProductCatagoryManager
        public ActionResult Index()
        {
            List<ProductCatagory> productcatagories = context.Collection().ToList();
            return View(productcatagories);
        }

        public ActionResult Create()
        {
            ProductCatagory productcatagory = new ProductCatagory();
            return View(productcatagory);
        }

        [HttpPost]
        public ActionResult Create(ProductCatagory productcatagory)
        {
            if (!ModelState.IsValid)
            {
                return View(productcatagory);
            }
            else
            {
                context.Insert(productcatagory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCatagory productcatagory = context.Find(Id);
            if (productcatagory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcatagory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCatagory productcatagory, string Id)
        {
            ProductCatagory productCatagoryToEdit = context.Find(Id);
            if (productCatagoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productcatagory);
                }

                productCatagoryToEdit.Catagory = productcatagory.Catagory;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCatagory productCatagoryToDelete = context.Find(Id);
            if (productCatagoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCatagoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCatagory productCatagoryToDelete = context.Find(Id);
            if (productCatagoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}