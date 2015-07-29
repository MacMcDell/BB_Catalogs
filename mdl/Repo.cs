using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl
{
    public class Repo : IRepo
    {
        private Context ctx;
        public Repo(Context context)
        {
            ctx = context;
        }

        public bool AddCatalog(Catalog catalog)
        {
            ctx.Catalogs.Add(catalog);
            return true;
        }

        public bool DeleteCatalog(int CatalogId)
        {

            var collectionToDelete = new List<Catalog>();
            var catalog = ctx.Catalogs.Where(x => x.Id == CatalogId).First(); //get the root item wherever it is.
            collectionToDelete.Add(catalog);
            DeleteRecursive(CatalogId, ref collectionToDelete);

            for (int i = collectionToDelete.Count-1; i >= 0; i--)
            {
                ctx.Catalogs.Remove(collectionToDelete[i]);
                Save();
            }



          

            //if(catalog.ChildCatalog != null)
            //ctx.Catalogs.Remove(catalog);
            return true;
        }

        private void DeleteRecursive(int id, ref List<Catalog> coll)
        {

            var itemstoDelete = ctx.Catalogs.Where(x => x.ParentId == id);
            if (itemstoDelete != null)
            {
                coll.AddRange(itemstoDelete);
                foreach (var item in itemstoDelete)
                {
                    DeleteRecursive(item.Id, ref coll);
                }
            }


            //if (itemtoDelete != null)
            //    coll.Add(itemtoDelete);
            //foreach (var item in ctx.Catalogs)
            //{
            //   coll.add()

            //}


            //var currentCatalog = ctx.Catalogs.Where(x => x.Id == id).Single();
            //var children = ctx.Catalogs.Where(x => x.ParentId == currentCatalog.id)

        }

        public IQueryable<Catalog> GetCatalogByParent(int parentId)
        {
            return ctx.Catalogs.Where(x => x.ParentId == parentId);
        }

        public IQueryable<Catalog> GetCatalogs()
        {
            return ctx.Catalogs;
        }

        public bool UpdateCatalog(Catalog catalog)
        {
            var oldCatalog = ctx.Catalogs.Where(x => x.Id == catalog.Id).FirstOrDefault();
            oldCatalog.Name = catalog.Name;
            oldCatalog.ParentId = catalog.ParentId;
            ctx.Entry(oldCatalog).State = System.Data.Entity.EntityState.Modified;
            return true;
        }

        public bool Save()
        {
            try
            {
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<Catalog> GetCatalogAndProducts()
        {
            Dictionary<int, Catalog> dict = new Dictionary<int, Catalog>();

            var catalogItem = ctx.Catalogs.OrderBy(x => x.ParentId);

            foreach (var node in catalogItem)
            {
                dict.Add(node.Id, node);
                node.ChildCatalog = new List<Catalog>();
                if (node.ParentId.HasValue)
                {
                    node.Catalog1 = new Catalog();
                    node.Catalog1.Id = node.ParentId.Value;
                }
            }

            var rootnodes = new List<Catalog>();

            foreach (var node in catalogItem)
            {
                if (node.Catalog1 == null)
                {
                    rootnodes.Add(node);
                }
                else
                {
                    node.Catalog1 = dict[node.Catalog1.Id];
                    node.Catalog1.ChildCatalog.Add(node);

                }
            }

            return rootnodes.AsQueryable();

        }

        public Product GetProdutById(int id)
        {
            var product = ctx.Products.Where(x => x.Id == id).Single();

            return product != null ? product : null;

        }

        public bool UpdateProduct(Product product)
        {
            var oldProduct = ctx.Products.Where(x => x.Id == product.Id).FirstOrDefault();
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Description = product.Description;
            ctx.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var product = ctx.Products.Where(x => x.Id == id).First();
            ctx.Products.Remove(product);
            return true;

        }

        public bool AddProduct(Product product)
        {
            ctx.Products.Add(product);
            return true;
        }

        public Catalog GetCatalogById(int? id)
        {
            return ctx.Catalogs.Where(x => x.Id == id.Value).Single();

        }
    }
}
