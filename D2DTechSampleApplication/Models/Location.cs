using System.ComponentModel.DataAnnotations;

namespace D2DTechSampleApplication.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        public float Latitute { get; set; }

        public float Longitute { get; set; }

        public string Area { get; set; }

        public override string ToString()
        {
            return Area + " (Lat: " + Latitute + ", Long: " + Longitute + ")";
        }
    }
}