using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ItCubeVoteDb.Models
{
	public class Date
	{
		public Guid Id { get; set; }
		public DateTime DateTime { get; set; }
		public List<Vote> Votes { get; set; }
		public List<Project> Projects { get; set; }
		public Date()
		{
			Projects = new List<Project>();
			Votes = new List<Vote>();
		}
	}
}
