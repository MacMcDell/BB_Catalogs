using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mdl;
using WebApplication1.Models;
using System.Web.Mvc.Html;

namespace WebApplication1.Controllers
{


    public class HomeController : Controller
    {
        IRepo _repo;

        public HomeController(IRepo repo)
        {
            _repo = repo;
        }


        public ActionResult Index()
        {
            var m = new HomeModel(_repo);

            return View(m);
        }

        public ActionResult Edit(int id)
        {
            var m = _repo.GetProdutById(id);
            return View(m);

        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {

            if (_repo.UpdateProduct(product) && _repo.Save())
            {
                return RedirectToAction("Index");
            }
            return View(product);

        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            if (_repo.DeleteProduct(id) && _repo.Save())
                return RedirectToAction("Index");

            //if something goes wrong the delete
            var p = _repo.GetProdutById(id);
            return View(p);

        }

        [HttpPost]
        public ActionResult InsertProduct(Product product)
        {
            if (_repo.AddProduct(product) && _repo.Save())
                return RedirectToAction("Index");

            return View(product);

        }

        [HttpGet]
        public ActionResult Insert(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            ViewBag.CatalogId = id;
            //var m = new Product();
            return View();
        }

        [HttpGet]
        public ActionResult InsertCatalog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertCatalog(Catalog catalog)
        {
            if (_repo.AddCatalog(catalog) && _repo.Save())
                return RedirectToAction("Index");

            return View(catalog);
        }


        public ActionResult AddSubCatalog(int? id)
        {
            var c = _repo.GetCatalogById(id);

            return View(c);
        }

        [HttpPost]
        public ActionResult AddSubCatalog(Catalog catalog)
        {
            catalog.ParentId = catalog.Id;
       
            if (_repo.AddCatalog(catalog) && _repo.Save())
                return RedirectToAction("Index");

            return View(catalog);
        }


        public ActionResult EditCatalog(int? id)
        {
            var catalog = _repo.GetCatalogById(id);
            return View(catalog);
        }

        [HttpPost]
        public ActionResult EditCatalog(Catalog catalog, string action)
        {
            var result = false;
            switch (action)
            {
                case "Delete":
                    _repo.DeleteCatalog(catalog.Id);
                    break;
                default:
                   result =  (_repo.UpdateCatalog(catalog) && _repo.Save());
                    break;
            }

            return RedirectToAction("Index");
        }
        

    }

}