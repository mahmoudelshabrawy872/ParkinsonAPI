using Microsoft.AspNetCore.Identity;

namespace Parkinson_Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        public Doctor Doctor { get; set; }
        public int? DoctorId { get; set; }
        //  public List<Test> Tests { get; } = new();
    }
}
