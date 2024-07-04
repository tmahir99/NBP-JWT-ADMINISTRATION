using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthAspNet7WebAPI.Core.Dtos
{
    public class Document
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DateCreated { get; set; }
            public string FilePath { get; set; }
            //This should be a forgine key, but since i'm working alone i will leave it as it is...
            public string EmployeeUserName { get; set; } 

    }
}
