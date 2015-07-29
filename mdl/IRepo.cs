using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl
{
    public interface IRepo
    {
        bool AddCatalog(Catalog catalog);
        bool DeleteCatalog(int CatalogId);
        bool UpdateCatalog(Catalog catalog);
        IQueryable<Catalog> GetCatalogs();
        IQueryable<Catalog> GetCatalogByParent(int parentId);

        IQueryable<Catalog> GetCatalogAndProducts(); 
        bool Save();
        Product GetProdutById(int id);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int id);
        bool AddProduct(Product product);
        Catalog GetCatalogById(int? id);
    }
}
