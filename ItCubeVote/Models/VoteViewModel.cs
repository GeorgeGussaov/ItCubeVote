using ItCubeVoteDb.Models;
using System;

namespace ItCubeVote.Models
{
	public class VoteViewModel
	{
		public Guid Id { get; set; }
		public User User { get; set; }
		public DateTime Time { get; set; }
		public ProjectViewModel MostDificult { get; set; }
		public ProjectViewModel MostBeautiful { get; set; }
		public ProjectViewModel Coolest { get; set; }
	}
}
