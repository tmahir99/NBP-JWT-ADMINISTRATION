namespace JwtAuthAspNet7WebAPI.Core.Dtos
{
    public class Request
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsResolved { get; set; }
        public int EmployeeId { get; set; } 
        public ApplicationUserDto Employee { get; set; }
    }

}
