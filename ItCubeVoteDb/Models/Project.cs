﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItCubeVoteDb.Models
{
	public class Project
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string FirsAuthor { get; set; }
		public string SecondAuthor { get; set; }
		public string GitLink { get; set; }
		public string ImgPath { get; set; }
	}
}
