using System.ComponentModel.DataAnnotations;

namespace Parkinson_Models.Dto
{
    public class DoctorRegisterRequestDto
    {
        [MaxLength(250)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

    }
}
