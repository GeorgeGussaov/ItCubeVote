using ItCubeVote.Models;


namespace ItCubeVote.Areas.Admin.Models
{
	public class ProjectResult
	{
		public ProjectViewModel Project { get; set; }
		public int CntMostBeautifulVotes { get; set; }
		public int CntMostDificultVotes { get; set; }
		public int CntCoolestVotes { get; set; }
		public int CntAll { get; set; }
		public void CountAll()
		{
			CntAll = CntCoolestVotes + CntMostBeautifulVotes + CntMostDificultVotes;
		}
	}
}
