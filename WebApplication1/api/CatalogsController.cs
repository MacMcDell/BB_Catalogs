using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mdl;

namespace WebApplication1.api
{
    public class CatalogsController : ApiController
    {
        private IRepo repo;
        public CatalogsController(IRepo repository)
        {
            repo = repository;
        }

        // GET api/<controller>
        public IEnumerable<Catalog> Get()
        {
            return repo.GetCatalogAndProducts();

        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Catalog catalog)
        {
            if (repo.AddCatalog(catalog) && repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, catalog);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}