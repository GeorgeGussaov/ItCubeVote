namespace ItCubeVote.Models
{
	public class VoteViewModel
	{
		public int Id { get; set; }
		public ProjectViewModel MostDificult { get; set; }
		public ProjectViewModel MostBeautiful { get; set; }
		public ProjectViewModel Coolest { get; set; }
	}
}
