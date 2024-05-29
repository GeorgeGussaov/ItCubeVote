using System.ComponentModel.DataAnnotations;
using System;

namespace ItCubeVote.Areas.Admin.Models
{
    public class AdminCheck //класс чисто для проверки админа
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
}
