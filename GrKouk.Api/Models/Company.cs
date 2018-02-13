using System.ComponentModel.DataAnnotations;

namespace GrKouk.Api.Models
{
    public class Company
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