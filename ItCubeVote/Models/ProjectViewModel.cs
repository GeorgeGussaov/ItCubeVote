using System;
using System.ComponentModel.DataAnnotations;

namespace ItCubeVote.Models
{
	public class ProjectViewModel
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage ="Заполните поле с названием проекта!")]
		public string Name { get; set; }
	}
}
