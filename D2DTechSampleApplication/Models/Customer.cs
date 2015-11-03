using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D2DTechSampleApplication.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location CustomerLocation { get; set; }
    }
}