using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace D2DTechSampleApplication.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public SkillType SkillType { get; set; }

        public List<Technician> Technicians { get; set; }
    }

    public enum SkillType
    {
        Installation,
        Maintenance
    }
}