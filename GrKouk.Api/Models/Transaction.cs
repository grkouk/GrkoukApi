using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrKouk.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        /// <summary>
        /// Ημερομηνία Κίνησης
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
        /// <summary>
        /// Αριθμός Παραστατικού
        /// </summary>
        public string ReferenceCode { get; set; }
        [Required]
        public int TransactorId { get; set; }
        public Transactor Transactor { get; set; }

        /// <summary>
        /// Αιτιολογία Κίνησης
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }
        /// <summary>
        /// Ποσό ΦΠΑ
        /// </summary>
        public decimal AmountFpa { get; set; }
        /// <summary>
        /// Καθαρό Ποσό
        /// </summary>
        public decimal AmountNet { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
