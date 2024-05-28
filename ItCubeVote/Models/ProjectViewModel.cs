using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItCubeVote.Models
{
	public class ProjectViewModel
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage ="Заполните поле с названием проекта!")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Заполните поле с описанием проекта!")]
		public string Description { get; set; }
		public string FirsAuthor { get; set; }
		public string SecondAuthor { get; set; }
	}
}
