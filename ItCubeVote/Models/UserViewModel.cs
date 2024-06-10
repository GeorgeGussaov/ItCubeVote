using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ItCubeVote.Models
{
	public class UserViewModel
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage ="Введите Имя и Фамилию")]
		public string Name { get; set; }
		public string School { get; set; }
		[Required(ErrorMessage = "Введите класс(если вы не школьник то выберите другое)")]
		public string Class { get; set; }

	}
}
