using Microsoft.AspNetCore.Http;
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
		[Required(ErrorMessage ="Необходиме заполнить хотябы поле с первым автором!")]
		public string FirsAuthor { get; set; }
		public string SecondAuthor { get; set; }
		[Required(ErrorMessage ="Укажите ссылку на Git!")]
		public string GitLink { get; set; }
		[Required(ErrorMessage = "Добавьте скриншот своего проекта")]
		public IFormFile Img { get; set; }
		public bool IsRemovable { get; set; } = true;
		public string ImgPath { get; set; }
	}
}
