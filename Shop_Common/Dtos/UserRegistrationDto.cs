using System.ComponentModel.DataAnnotations;

namespace Shop_Common.Dtos
{
    public class UserRegistrationDto
    {
        public UserRegistrationDto(string name, string surname, string patronymic, string password, string login)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Password = password;
            Login = login;
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}