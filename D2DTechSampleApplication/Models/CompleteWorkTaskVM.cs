using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D2DTechSampleApplication.Models
{
    public class CompleteWorkTaskVM
    {
        [Key]
        public int WorkTaskId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int WorkStatus { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int TechnicianId { get; set; }
    }
}