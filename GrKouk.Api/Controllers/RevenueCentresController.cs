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
    [Route("api/RevenueCentres")]
    public class RevenueCentresController : Controller
    {
        private readonly ApiDbContext _context;

        public RevenueCentresController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/RevenueCentres
        [HttpGet]
        public IEnumerable<RevenueCentre> GetRevenueCentres()
        {
            return _context.RevenueCentres;
        }

        // GET: api/RevenueCentres/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRevenueCentre([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var revenueCentre = await _context.RevenueCentres.SingleOrDefaultAsync(m => m.Id == id);

            if (revenueCentre == null)
            {
                return NotFound();
            }

            return Ok(revenueCentre);
        }

        // PUT: api/RevenueCentres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRevenueCentre([FromRoute] int id, [FromBody] RevenueCentre revenueCentre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != revenueCentre.Id)
            {
                return BadRequest();
            }

            _context.Entry(revenueCentre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevenueCentreExists(id))
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

        // POST: api/RevenueCentres
        [HttpPost]
        public async Task<IActionResult> PostRevenueCentre([FromBody] RevenueCentre revenueCentre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RevenueCentres.Add(revenueCentre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRevenueCentre", new { id = revenueCentre.Id }, revenueCentre);
        }

        // DELETE: api/RevenueCentres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRevenueCentre([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var revenueCentre = await _context.RevenueCentres.SingleOrDefaultAsync(m => m.Id == id);
            if (revenueCentre == null)
            {
                return NotFound();
            }

            _context.RevenueCentres.Remove(revenueCentre);
            await _context.SaveChangesAsync();

            return Ok(revenueCentre);
        }

        private bool RevenueCentreExists(int id)
        {
            return _context.RevenueCentres.Any(e => e.Id == id);
        }
        // GET: api/RevenueCentreSearchList
        [HttpGet("RevenueCentreSearchList")]
        public async Task<IActionResult> RevenueCentreSearchList()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var revenueCentreList = await _context.RevenueCentres.OrderBy(p => p.Name)
                .Select(s => new
                {
                    Name = s.Name,
                    Key = s.Id
                })
                .ToListAsync();

            if (revenueCentreList == null || revenueCentreList.Count == 0)
            {
                return NotFound();
            }

            return Ok(revenueCentreList);
        }
    }
}