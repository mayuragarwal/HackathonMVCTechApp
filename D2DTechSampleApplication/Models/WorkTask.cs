using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D2DTechSampleApplication.Models
{
    public class WorkTask
    {
        [Key]
        public int WorkTaskId { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int WorkStatus { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int TechId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public Technician Technician { get; set; }
    }
}