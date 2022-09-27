using System.ComponentModel.DataAnnotations;

namespace UserDB.Models
{
    public class UserViewModel
    {
        [Required (ErrorMessage = "Введите непустое имя")]
        [MaxLength(50, ErrorMessage = "Слишком длинное имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите непустой пароль")]
        [MaxLength(255, ErrorMessage = "Слишком длинный пароль")]
        [MinLength(8 , ErrorMessage = "Слишком короткий пароль")]
        public string Password { get; set; }

        public int Id { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(int id, string name, string password)
        {
            Name = name;
            Password = password;
            Id = id;
        }
    }
}
