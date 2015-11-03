using System.ComponentModel.DataAnnotations;

namespace D2DTechSampleApplication.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }

        [Required]
        public string Area { get; set; }

        public override string ToString()
        {
            return Area + " (Lat: " + Latitude + ", Long: " + Longitude + ")";
        }
    }
}