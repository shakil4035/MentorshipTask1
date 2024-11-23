using MentorshipTask1.Manager;
using MentorshipTask1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;

namespace MentorshipTask1.Controllers.Api
{
    public class ProductsController : ApiController
    {
        public ProductManager _manager = new ProductManager();
        // GET: api/Products
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody] Product vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_manager.Add(vm));
                }
                else
                {
                    return BadRequest("Input Data is valid");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }

        // API 04
        [Route("api/Products/GetDataFor6")]
        [HttpGet]
        public IEnumerable<Product> GetDataFor6()
        {
            return _manager.GetDataApi6();
        }

        [Route("api/Products/GetDataFor8")]
        [HttpGet]
        public IEnumerable<Product> GetDataFor8()
        {
            return _manager.GetDataApi8();
        }
    }
}
