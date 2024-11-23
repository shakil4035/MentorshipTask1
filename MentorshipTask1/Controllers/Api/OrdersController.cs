using MentorshipTask1.Manager;
using MentorshipTask1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace MentorshipTask1.Controllers.Api
{
    public class OrdersController : ApiController
    {
        public OrderManager _manager = new OrderManager();


       
        // GET: api/Orders
        public IEnumerable<Order> Get()
        {
            return _manager.GetAll();
        }

        // GET: api/Orders/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Orders
        public IHttpActionResult Post([FromBody] Order vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ret = _manager.Add(vm);
                    if (ret == 0)
                    {
                        return BadRequest("insufficient stock");
                    }
                    else
                    {
                        return Ok();
                    }
                    
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

        // PUT: api/Orders/5
        public IHttpActionResult Put(int id, [FromBody] Order vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var up = _manager.Update(id, vm);
                    if(up==0)
                    {
                        return BadRequest("doesn't available stock");
                    }
                    else
                    {
                        return Ok();
                    }
                    
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

        // DELETE: api/Orders/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var up = _manager.Remove(id);
                return Ok();
            }

            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }

        // API 04
        [Route("api/Orders/GetData")]
        [HttpGet]
        public IEnumerable<OrderViewModel> GetData()
        {
            return _manager.GetData();
        }


        // API 05
        [Route("api/Orders/GetDataFor5")]
        [HttpGet]
        public IEnumerable<ViewModelApi5> GetDataFor5()
        {
            return _manager.GetDataApi5();
        }

        // API 05
        [Route("api/Orders/GetDataFor7")]
        [HttpGet]
        public IEnumerable<CustomerSummary> GetDataFor7()
        {
            return _manager.GetApi7();
        }

        [Route("api/Orders/GetApi9")]
        [HttpPost]
        public IHttpActionResult GetApi9([FromBody] List<Order> vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ret = _manager.GetApi9(vm);
                    if (ret == 0)
                    {
                        return BadRequest("insufficient stock");
                    }
                    else
                    {
                        return Ok();
                    }

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


    }
}
