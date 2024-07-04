namespace JwtAuthAspNet7WebAPI.Core.Dtos
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string AsignedBy { get; set; }
        public string AsignedTo { get; set; }
        public Boolean IsDone { get; set; }
        public Boolean CurrierDelivered { get; set; }
        public string EditedBy { get; set; }
        public DateTime AsignedOn { get; set; }
        public byte[] Image { get; set; }
    }


}
