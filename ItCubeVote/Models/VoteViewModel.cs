using System;

namespace ItCubeVote.Models
{
	public class VoteViewModel
	{
		public int Id { get; set; }
		public DateTime Time { get; set; } = DateTime.Now;
		public ProjectViewModel MostDificult { get; set; }
		public ProjectViewModel MostBeautiful { get; set; }
		public ProjectViewModel Coolest { get; set; }
	}
}
