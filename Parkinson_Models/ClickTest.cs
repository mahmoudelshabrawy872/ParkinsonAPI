namespace Parkinson_Models
{
    public class ClickTest
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public decimal LeftHand { get; set; }
        public decimal RightHand { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
