using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrKouk.Api.Data;
using GrKouk.Api.Models;

namespace GrKouk.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactors")]
    public class TransactorsController : Controller
    {
        private readonly ApiDbContext _context;

        public TransactorsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactors
        [HttpGet]
        public IEnumerable<Transactor> GetTransactors()
        {
            return _context.Transactors;
        }

        // GET: api/Transactors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactor = await _context.Transactors.SingleOrDefaultAsync(m => m.Id == id);

            if (transactor == null)
            {
                return NotFound();
            }

            return Ok(transactor);
        }

        // PUT: api/Transactors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactor([FromRoute] int id, [FromBody] Transactor transactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transactor.Id)
            {
                return BadRequest();
            }

            _context.Entry(transactor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactors
        [HttpPost]
        public async Task<IActionResult> PostTransactor([FromBody] Transactor transactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Transactors.Add(transactor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactor", new { id = transactor.Id }, transactor);
        }

        // DELETE: api/Transactors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactor = await _context.Transactors.SingleOrDefaultAsync(m => m.Id == id);
            if (transactor == null)
            {
                return NotFound();
            }

            _context.Transactors.Remove(transactor);
            await _context.SaveChangesAsync();

            return Ok(transactor);
        }

        private bool TransactorExists(int id)
        {
            return _context.Transactors.Any(e => e.Id == id);
        }

        // GET: api/Transactors/Name/George
        [HttpGet("name/{TransactorName}")]
        public async Task<IActionResult> GetTransactorByName([FromRoute] string transactorName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactors = await _context.Transactors.Where(m => m.Name.StartsWith(transactorName,StringComparison.CurrentCultureIgnoreCase)).ToListAsync();

            if (transactors == null || transactors.Count==0)
            {
                return NotFound();
            }

            return Ok(transactors);
        }
    }
}