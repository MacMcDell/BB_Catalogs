using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mdl; 


namespace WebApplication1.Models
{
    public class HomeModel
    {

        public List<Catalog> catalogs { get; set; }
        public List<Product> products { get; set; }

       
        public HomeModel(IRepo repo)
        {
            
         //   catalogs = repo.GetCatalogAndProducts().ToList(); 
            using (var c = new Context())
            {
                catalogs = c.Catalogs.ToList();
                products = c.Products.ToList(); 
                


                           
                           
                           
            }


        }

    }
}