using System;

using System.Collections.Generic;
using System.Text;

namespace GrKouk.Api.Dtos
{
    public class TransactionCreateDto
    {
        public int Id { get; set; }
        
        public DateTime TransactionDate { get; set; }
        public string ReferenceCode { get; set; }
        public int TransactorId { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public int CostCentreId { get; set; }
        public int RevenueCentreId { get; set; }
        public string Description { get; set; }
        public int Kind { get; set; }
        public decimal AmountFpa { get; set; }
        public decimal AmountNet { get; set; }
    }
}
