using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrKouk.Api.Models
{
    public class CostCentre
    {
        public int Id { get; set; }

        [MaxLength(15)]
        [Required]
        public string Code { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
    }
}
