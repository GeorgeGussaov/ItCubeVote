using System.ComponentModel.DataAnnotations;
using System;

namespace ItCubeVoteDb.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
	}
}
