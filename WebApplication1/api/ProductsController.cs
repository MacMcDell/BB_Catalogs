using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mdl;
using System.Web.Http.Description;

namespace WebApplication1.api
{
    public class ProductsController : ApiController
    {

        IRepo _repo;

        public ProductsController(IRepo repo)
        {
            _repo = repo;
        }


        // GET api/<controller>
        [ResponseType(typeof(Product))]
        public IEnumerable<Product> Get()
        {
            return _repo.GetProducts();
        }

        // GET api/<controller>/5
        public Product Get(int id)
        {
            return _repo.GetProdutById(id);
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Product product)
        {
            if (_repo.AddProduct(product) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, product);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put( [FromBody]Product updatedProduct)
        {
            if (_repo.UpdateProduct(updatedProduct) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, updatedProduct);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            if (_repo.DeleteProduct(id) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, id);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }
    }
}