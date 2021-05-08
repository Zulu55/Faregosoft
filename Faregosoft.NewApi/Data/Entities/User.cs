using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Faregosoft.NewApi.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }

        [JsonIgnore]
        public ICollection<Customer> Customers { get; set; }
       
        [JsonIgnore]
        public ICollection<Seller> Sellers { get; set; }
       
    }
}
