using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly pubsContext context;

        public StoreController(pubsContext context)
        {
            this.context = context;
        }

        //GET: api/stores
        [HttpGet]

        public ActionResult<IEnumerable<Stores>> Get()
        {
            return context.Stores.ToList();
        }

        //GET BY ID
        //GET: api/stores/5
        [HttpGet("{id}")]
        public ActionResult<Stores> GetById(string id)
        {
            Stores stores = (from a in context.Stores
                                   where a.StorId == id
                                   select a).SingleOrDefault();
            return stores;
        }

        //GET BY NAME
        [HttpGet("name/{name}")]

        public ActionResult<Stores> GetByName(string name)
        {
            Stores stores = (from a in context.Stores
                             where a.StorName ==name
                             select a).SingleOrDefault();
            return stores;
        }

        //GetByZip

        [HttpGet("zip/{zip}")]

        public ActionResult<Stores> GetByZip(string zip)
        {
            Stores stores = (from a in context.Stores
                             where a.Zip == zip
                             select a).SingleOrDefault();
            return stores;
        }


        //GetByCityState

        [HttpGet("city/{city}")]
        public ActionResult<IEnumerable<Stores>> GetByCity(string city)
        {
            var stores = (from a in context.Stores
                          where a.City == city
                          select a).ToList();
            return stores;

        }



        //Post api/stores
        [HttpPost]

        public ActionResult Post(Stores stores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(stores);
            }
            context.Stores.Add(stores);
            context.SaveChanges();
            return Ok();
        }

        //Put
        //Put api/stores/2

        [HttpPut("{id}")]

        public ActionResult Put(string id, [FromBody] Stores stores)
        {
            if (id != stores.StorId)
            {
                return BadRequest();
            }
            context.Entry(stores).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        //Delete  api/store/3

        [HttpDelete("{id}")]

        public ActionResult<Stores> Delete(string id)
        {
            var stores = (from a in context.Stores
                             where a.StorId == id
                             select a).SingleOrDefault();
            if (stores == null)
            {
                return NotFound();
            }
            context.Stores.Remove(stores);
            context.SaveChanges();
            return stores;


        }
    }
}
