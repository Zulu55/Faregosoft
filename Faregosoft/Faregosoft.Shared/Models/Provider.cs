using System;

namespace Faregosoft.Models
{
    public class Provider
    {
        public int Id { get; set; }
               
        public string FirstName { get; set; }
     
        public string LastName { get; set; }
       
        public string Code { get; set; }
       
        public string Category { get; set; }
      
        public string Type { get; set; }
      
        public string Contact { get; set; }
    
        public string Address { get; set; }
        
        public string Country { get; set; }
       
        public string City { get; set; }

        public Double CreditLimit { get; set; }

        public int PaymentCodition { get; set; }
     
        public string Observation { get; set; }
        public bool IsActive { get; set; }

        public User User { get; set; }

        public bool WasSaved { get; set; }

        public bool IsEdit { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
