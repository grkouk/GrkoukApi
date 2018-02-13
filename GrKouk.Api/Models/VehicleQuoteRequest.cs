using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrKouk.Api.Models
{
    public class VehicleQuoteRequest
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [MaxLength(100), Required]
        public string RequesterEmail { get; set; }
        [MaxLength(100), Required]
        public string RequesterName { get; set; }

        public string City { get; set; }
        [MaxLength(10), Required]
        public string Country { get; set; }
        [MaxLength(10), Required]
        public string VehicleType { get; set; }

        [DataType(DataType.Date), Required]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date), Required]
        public DateTime DateTo { get; set; }
    }
}
