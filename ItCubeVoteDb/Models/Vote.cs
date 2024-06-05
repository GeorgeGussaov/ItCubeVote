using System;

namespace ItCubeVoteDb.Models
{
	public class Vote
	{
		public Guid Id { get; set; }
		public User User { get; set; }
		public DateTime Time { get; set; }
		public Project MostDificult { get; set; }
		public Project MostBeautiful { get; set; }
		public Project Coolest { get; set; }
		public Vote()
		{
			Time = DateTime.Now;
		}
	}
}
