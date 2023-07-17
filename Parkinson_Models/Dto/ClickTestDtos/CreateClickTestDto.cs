namespace Parkinson_Models.Dto.ClickTestDtos
{
    public class CreateClickTestDto
    {
        public int PatientId { get; set; }
        public decimal LeftHand { get; set; }
        public decimal RightHand { get; set; }
    }
}
