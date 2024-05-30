using ItCubeVoteDb.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ItCubeVote.Models
{
	public class DateViewModel
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage ="Вы не указали дату!")]
		public DateTime DateTime { get; set; }
		public List<VoteViewModel> Votes { get; set; }
		public List<ProjectViewModel> Projects { get; set; }
	}
}
