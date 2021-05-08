using System;
using System.ComponentModel.DataAnnotations;

namespace Faregosoft.NewApi.Data.Entities
{
    public class Providers
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(15)]
        public string Code { get; set; }
        [MaxLength(15)]
        public string Category { get; set; }
        [MaxLength(15)]
        public string Type { get; set; }
        [MaxLength(120)]
        public string Contact { get; set; }
        [MaxLength(120)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(50)]
        public string City { get; set; }

        public Double CreditLimit { get; set; }
        public int PaymentCodition { get; set; }
        [MaxLength(250)]
        public string Observation { get; set; }
        public bool IsActive { get; set; }

        public User User { get; set; }
    }
}
