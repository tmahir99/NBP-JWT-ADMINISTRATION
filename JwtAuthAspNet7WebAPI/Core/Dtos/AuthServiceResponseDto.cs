namespace JwtAuthAspNet7WebAPI.Core.Dtos
{
    public class AuthServiceResponseDto
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
