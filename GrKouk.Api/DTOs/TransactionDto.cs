using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrKouk.Api.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        
        public DateTime TransactionDate { get; set; }
        public string ReferenceCode { get; set; }
        public int TransactorId { get; set; }
        public string TransactorName { get; set; }
        public string Description { get; set; }
        public decimal AmountFpa { get; set; }
        public decimal AmountNet { get; set; }
        public decimal AmountTotal { get; set; }
        
    }
}
