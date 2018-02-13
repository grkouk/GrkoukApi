using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrKouk.Api.Data;
using GrKouk.Api.Models;
using GrKouk.Api.Services;

namespace GrKouk.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/VehicleQuoteRequests")]
    public class VehicleQuoteRequestsController : Controller
    {
        private readonly ApiDbContext _context;
        private readonly IEmailSender _emailSender;

        public VehicleQuoteRequestsController(ApiDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: api/VehicleQuoteRequests
        [HttpGet]
        public IEnumerable<VehicleQuoteRequest> GetVehicleQuoteRequests()
        {
            return _context.VehicleQuoteRequests;
        }

        // GET: api/VehicleQuoteRequests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleQuoteRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleQuoteRequest = await _context.VehicleQuoteRequests.SingleOrDefaultAsync(m => m.Id == id);

            if (vehicleQuoteRequest == null)
            {
                return NotFound();
            }

            return Ok(vehicleQuoteRequest);
        }

        // PUT: api/VehicleQuoteRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleQuoteRequest([FromRoute] int id, [FromBody] VehicleQuoteRequest vehicleQuoteRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleQuoteRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleQuoteRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleQuoteRequestExists(id))
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

        // POST: api/VehicleQuoteRequests
        [HttpPost]
        public async Task<IActionResult> PostVehicleQuoteRequest([FromBody] VehicleQuoteRequest vehicleQuoteRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            vehicleQuoteRequest.RequestDate = DateTime.Today;
            _context.VehicleQuoteRequests.Add(vehicleQuoteRequest);
            await _context.SaveChangesAsync();

            var _emailTo = "info@thassos-rent-a-bike.com";

            var _subject = $"Quote Request from {vehicleQuoteRequest.RequesterName}";
            var _message1 = $"Requester name {vehicleQuoteRequest.RequesterName} from {vehicleQuoteRequest.Country}";
            var _message2 = $"Quote for {vehicleQuoteRequest.VehicleType} from {vehicleQuoteRequest.DateFrom} to {vehicleQuoteRequest.DateTo}";
            //var _message3 = $"Adults = {roomQuoteRequest.Adults} children = {roomQuoteRequest.Children}";
            var _message = _message1 + "\r\n" + _message2 + "\r\n" ;

            await _emailSender.SendEmailAsync(vehicleQuoteRequest.RequesterEmail, _emailTo, _subject, _message);

            return CreatedAtAction("GetVehicleQuoteRequest", new { id = vehicleQuoteRequest.Id }, vehicleQuoteRequest);
        }

        // DELETE: api/VehicleQuoteRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleQuoteRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleQuoteRequest = await _context.VehicleQuoteRequests.SingleOrDefaultAsync(m => m.Id == id);
            if (vehicleQuoteRequest == null)
            {
                return NotFound();
            }

            _context.VehicleQuoteRequests.Remove(vehicleQuoteRequest);
            await _context.SaveChangesAsync();

            return Ok(vehicleQuoteRequest);
        }

        private bool VehicleQuoteRequestExists(int id)
        {
            return _context.VehicleQuoteRequests.Any(e => e.Id == id);
        }
    }
}