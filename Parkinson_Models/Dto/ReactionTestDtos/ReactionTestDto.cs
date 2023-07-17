namespace Parkinson_Models.Dto.MemoryTestDtos
{
    public class ReactionTestDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public decimal Result { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
