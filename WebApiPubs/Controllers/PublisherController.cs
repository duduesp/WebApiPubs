using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly  pubsContext context;


        public PublisherController(pubsContext context)
        {
            this.context = context;
        }
        //GET: api/publisher
        [HttpGet]

        public ActionResult<IEnumerable<Publisher>> Get()
        {
            return context.Publishers.ToList();
        }

        //GET BY ID
        //GET: api/publisher/5
        [HttpGet("{id}")]
            //Publisher publisher = (from a in context.Publishers
            //                   where a.PubId == id
            //                   select a).SingleOrDefault();
            //return publisher;
        public ActionResult<Publisher> GetById(string id)
        {
            Publisher publisher = context.Publishers.Include(x => x.Titles).SingleOrDefault(x => x. PubId== id);
            return publisher;
        }

        //Post api/publisher
        [HttpPost]

        public ActionResult Post(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(publisher);
            }
            context.Publishers.Add(publisher);
            context.SaveChanges();
            return Ok();
        }

        //Put
        //Put api/publisher/2

        [HttpPut("{id}")]

        public ActionResult Put(string id, [FromBody] Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }
            context.Entry(publisher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        //Delete  api/clinica/3

        [HttpDelete("{id}")]

        public ActionResult<Publisher> Delete(string id)
        {
            var publisher = (from a in context.Publishers
                           where a.PubId == id
                           select a).SingleOrDefault();
            if (publisher == null)
            {
                return NotFound();
            }
            context.Publishers.Remove(publisher);
            context.SaveChanges();
            return publisher;


        }

    }
}
