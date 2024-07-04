using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthAspNet7WebAPI.Core.Dtos
{
    public class ApplicationUserDto
    {
        [Key]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }
    }
}
