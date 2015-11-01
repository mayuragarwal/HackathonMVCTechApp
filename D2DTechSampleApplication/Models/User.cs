using System.ComponentModel.DataAnnotations;

namespace D2DTechSampleApplication.Models
{
    public class User
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}