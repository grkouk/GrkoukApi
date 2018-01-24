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
using Microsoft.AspNetCore.Cors;

namespace GrKouk.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/RoomQuoteRequests")]
    public class RoomQuoteRequestsController : Controller
    {
        private readonly ApiDbContext _context;
        private readonly IEmailSender _emailSender;

        public RoomQuoteRequestsController(ApiDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: api/RoomQuoteRequests
        [HttpGet]
        [DisableCors]
        public IEnumerable<RoomQuoteRequest> GetRoomQuoteRequests()
        {
            return _context.RoomQuoteRequests;
        }

        // GET: api/RoomQuoteRequests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomQuoteRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomQuoteRequest = await _context.RoomQuoteRequests.SingleOrDefaultAsync(m => m.Id == id);

            if (roomQuoteRequest == null)
            {
                return NotFound();
            }

            return Ok(roomQuoteRequest);
        }

        // PUT: api/RoomQuoteRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomQuoteRequest([FromRoute] int id, [FromBody] RoomQuoteRequest roomQuoteRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomQuoteRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomQuoteRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomQuoteRequestExists(id))
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

        // POST: api/RoomQuoteRequests
        [HttpPost]
        public async Task<IActionResult> PostRoomQuoteRequest([FromBody] RoomQuoteRequest roomQuoteRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            roomQuoteRequest.RequestDate=DateTime.Today;
            _context.RoomQuoteRequests.Add(roomQuoteRequest);
            await _context.SaveChangesAsync();
            var _emailTo = "info@villakoukoudis.com";

            var _subject = $"Quote Request from {roomQuoteRequest.RequesterName}";
            var _message1 = $"Requester name {roomQuoteRequest.RequesterName} from {roomQuoteRequest.Country}";
            var _message2 = $"Quote for {roomQuoteRequest.RoomType} from {roomQuoteRequest.DateFrom} to {roomQuoteRequest.DateTo}";
            var _message3 = $"Adults = {roomQuoteRequest.Adults} children = {roomQuoteRequest.Children}";
            var _message = _message1 + System.Environment.NewLine + _message2 + System.Environment.NewLine + _message3;

            await _emailSender.SendEmailAsync(roomQuoteRequest.RequesterEmail, _emailTo,_subject,_message);

            return CreatedAtAction("GetRoomQuoteRequest", new { id = roomQuoteRequest.Id }, roomQuoteRequest);
        }

        // DELETE: api/RoomQuoteRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomQuoteRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomQuoteRequest = await _context.RoomQuoteRequests.SingleOrDefaultAsync(m => m.Id == id);
            if (roomQuoteRequest == null)
            {
                return NotFound();
            }

            _context.RoomQuoteRequests.Remove(roomQuoteRequest);
            await _context.SaveChangesAsync();

            return Ok(roomQuoteRequest);
        }

        private bool RoomQuoteRequestExists(int id)
        {
            return _context.RoomQuoteRequests.Any(e => e.Id == id);
        }
    }
}