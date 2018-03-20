using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrKouk.Api.Data;

using GrKouk.Api.Models;
using GrKouk.Api.Dtos;

namespace GrKouk.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private readonly ApiDbContext _context;

        public TransactionsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public IEnumerable<TransactionDto> GetTransactions()
        {
            var transactions = Mapper.Map<IEnumerable<TransactionDto>>(
                _context.Transactions
                    .Include(s => s.Transactor)
                    .Include(t=>t.Category)
                    .Include(t=>t.Company)
                    .Include(t=>t.CostCentre)
                    .Include(t=>t.RevenueCentre)
                    .ToList());
            return transactions;
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // PUT: api/Transactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction([FromRoute] int id, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] TransactionCreateDto transactionCreateDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entityToMap = Mapper.Map<Transaction>(transactionCreateDto);
                _context.Transactions.Add(entityToMap);
                await _context.SaveChangesAsync();
                if (entityToMap.Transactor==null)
                {
                    _context.Entry(entityToMap).Reference(p => p.Transactor).Load();
                }
                if (entityToMap.Category == null)
                {
                    _context.Entry(entityToMap).Reference(p => p.Category).Load();
                }
                if (entityToMap.Company == null)
                {
                    _context.Entry(entityToMap).Reference(p => p.Company).Load();
                }
                if (entityToMap.CostCentre == null)
                {
                    _context.Entry(entityToMap).Reference(p => p.CostCentre).Load();
                }
                if (entityToMap.RevenueCentre == null)
                {
                    _context.Entry(entityToMap).Reference(p => p.RevenueCentre).Load();
                }
                var entityToReturn = Mapper.Map<TransactionDto>(entityToMap);
                return CreatedAtAction("GetTransaction", new { id = entityToMap.Id },entityToReturn );
            }
            catch (Exception e)
            {
                throw e;
            }

            
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction);
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }

        // GET: api/Transactions/GetTransactionsInPeriod?dateFrom=2018-12-01&dateTo=2018-12-31
        //[Route("api/TransactionsInPeriod")]
        [HttpGet("TransactionsInPeriod")]
        public async Task<IActionResult> GetTransactionsInPeriod(DateTime fromDate, DateTime toDate)
        {
            if (fromDate==null)
            {
                throw new ArgumentNullException();
            }
            if (toDate == null)
            {
                throw new ArgumentNullException();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


           // DateTime tDate=

            var transactions = Mapper.Map<IEnumerable<TransactionDto> >( await _context.Transactions.Where(m=>m.TransactionDate>=fromDate && m.TransactionDate<=toDate )
                .Include(s => s.Transactor)
                .Include(t => t.Category)
                .Include(t => t.Company)
                .Include(t => t.CostCentre)
                .Include(t => t.RevenueCentre)
                .ToListAsync());

            if (transactions == null)
            {
                return NoContent();
            }

            return Ok(transactions);
        }
    }
}