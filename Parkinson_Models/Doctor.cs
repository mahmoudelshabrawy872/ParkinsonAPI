using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Parkinson_Models
{
    public class Doctor
    {
        [Required]
        public int ID { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}
