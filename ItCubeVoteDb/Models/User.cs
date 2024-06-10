﻿using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace ItCubeVoteDb.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string School { get; set; }
		public string Class { get; set; }
	}
}
