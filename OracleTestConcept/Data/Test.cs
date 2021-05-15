using System.ComponentModel.DataAnnotations;

namespace OracleTestConcept.Data
{
    public class Test
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
