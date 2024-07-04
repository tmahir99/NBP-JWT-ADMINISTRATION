namespace JwtAuthAspNet7WebAPI.Core.Dtos
{
    public class JobRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string AsignedBy { get; set; }
        public string AsignedTo { get; set; }
        public DateTime AsignedOn { get; set; }
    }

}
