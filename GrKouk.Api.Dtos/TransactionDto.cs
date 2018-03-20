﻿using System;

namespace GrKouk.Api.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }
        public string ReferenceCode { get; set; }
        public int TransactorId { get; set; }
        public string TransactorName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int CostCentreId { get; set; }
        public string CostCentreName { get; set; }
        public int RevenueCentreId { get; set; }
        public string RevenueCentreName { get; set; }

        public string Description { get; set; }
        public int Kind { get; set; }
        public decimal AmountFpa { get; set; }
        public decimal AmountNet { get; set; }
        public decimal AmountTotal { get; set; }
    }
}
