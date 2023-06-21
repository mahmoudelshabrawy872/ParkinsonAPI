namespace Parkinson_Models
{
    public class PatientTest
    {
        public int Id { get; set; }
        public Patient Patient { get; set; } = null!;
        public Test Test { get; set; } = null!;
        public DateTime TestedOn { get; set; } = DateTime.Now;
    }
}
