using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QoutesApi.Data;
using QoutesApi.Models;

namespace QoutesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }
        // GET: api/Quotes
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_quotesDbContext.Quotes);
        }

        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var quoteFromDb = _quotesDbContext.Quotes.FirstOrDefault(q => q.Id == id);
            if( quoteFromDb == null)
            {
                return NotFound("No quote found against this id...");
            }
            return Ok(quoteFromDb);
        }

        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quotesDbContext.Add<Quote>(quote);
            _quotesDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            var quoteFromDb = _quotesDbContext.Quotes.FirstOrDefault(q => q.Id == id);
            if (quoteFromDb == null)
            {
                return NotFound("No quote found against this id...");
            }
            quoteFromDb.Title = quote.Title;
            quoteFromDb.Author = quote.Author;
            quoteFromDb.Description = quote.Description;
            quoteFromDb.Type = quote.Type;
            _quotesDbContext.SaveChanges();
            return Ok("Quote update successfully");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quoteFromDb = _quotesDbContext.Quotes.FirstOrDefault(q => q.Id == id);
            if (quoteFromDb == null)
            {
                return NotFound("No quote found against this id...");
            }
            _quotesDbContext.Quotes.Remove(quoteFromDb);
            _quotesDbContext.SaveChanges();
            return Ok("Quote deleted...");
        }
    }
}
