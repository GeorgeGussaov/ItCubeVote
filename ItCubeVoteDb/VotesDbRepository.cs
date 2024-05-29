using ItCubeVoteDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItCubeVoteDb
{
	public class VotesDbRepository : IVotes
	{
		private DatabaseContext _dbContext;
		public VotesDbRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void Add(Vote vote)
		{
			_dbContext.Add(vote);
			_dbContext.SaveChanges();
		}
		public List<Vote> GetVotes()
		{
			return _dbContext.Votes.ToList();
		}
	}

	public interface IVotes
	{
		public void Add(Vote vote);
		public List<Vote> GetVotes();
	}
}
