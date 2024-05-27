using System;
using System.ComponentModel.DataAnnotations;

namespace ItCubeVote.Models
{
	public class UserViewModel
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage ="Введите логин")]
		public string Login { get; set; }
		[Required(ErrorMessage = "Введите пароль")]
		public string Password { get; set; }

	}
}
