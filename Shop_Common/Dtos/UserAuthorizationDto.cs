namespace Shop_Common.Dtos
{
    public class UserAuthorizationDto
    {
        public UserAuthorizationDto(string? login, string? password)
        {
            Login = login;
            Password = password;
        }

        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
