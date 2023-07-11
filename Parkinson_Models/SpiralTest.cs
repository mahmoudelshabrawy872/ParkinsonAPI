namespace Parkinson_Models
{
    public class SpiralTest
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public decimal Result { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
