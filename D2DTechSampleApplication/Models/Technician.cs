using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D2DTechSampleApplication.Models
{
    public class Technician
    {
        [Key]
        public int TechnicianId { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location CurrentLocation { get; set; }

        public List<WorkTask> WorkTasks { get; set; }

        public List<Skill> Skills { get; set; }
    }
}