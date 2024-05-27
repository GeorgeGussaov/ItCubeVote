using System;

namespace ItCubeVoteDb.Models
{
	public class Vote
	{
		public Guid Id { get; set; }
		public Project MostDificult { get; set; }
		public Project MostBeautiful { get; set; }
		public Project Coolest { get; set; }
	}
}
