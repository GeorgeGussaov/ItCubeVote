using ItCubeVoteDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItCubeVoteDb
{
	public class UsersDbRepository : IUsers
	{
		private readonly DatabaseContext _databaseContext;
		public UsersDbRepository(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public void Add(User user)
		{
			_databaseContext.Users.Add(user);
			_databaseContext.SaveChanges();
		}

		public List<User> GetUsers()
		{
			return _databaseContext.Users.ToList();
		}
	}

	public interface IUsers
	{
		public void Add(User user);
		public List<User> GetUsers();
	}
}
