namespace Parkinson_Models.Dto.ClickTestDtos
{
    public class ClickTestDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public decimal LeftHand { get; set; }
        public decimal RightHand { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
