using System;
using System.ComponentModel.DataAnnotations;

namespace Faregosoft.NewApi.Data.Entities
{
    public class Seller
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
        [Required]
        public float Comision { get; set; }

        public bool IsActive { get; set; }

        public User User { get; set; }

    }
}
