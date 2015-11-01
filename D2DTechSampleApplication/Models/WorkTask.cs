using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace D2DTechSampleApplication.Models
{
    public class WorkTask
    {
        public int WorkTaskId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int WorkStatus { get; set; }

        public int CustomerId { get; set; }

        public int TechId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public Technician Technician { get; set; }
    }
}