using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace D2DTechSampleApplication.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }

        public string Name { get; set; }

        public SkillType SkillType { get; set; }

        public List<Technician> Technicians { get; set; }
    }

    public enum SkillType
    {
        Installation,
        Maintenance
    }
}