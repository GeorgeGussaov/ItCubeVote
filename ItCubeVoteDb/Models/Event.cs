using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ItCubeVoteDb.Models
{
	public class Event
	{
		public Guid Id { get; set; }
		public DateTime DateTime { get; set; }
		public List<Vote> Votes { get; set; }
		public List<Project> Projects { get; set; }
		public Event()
		{
			Projects = new List<Project>();
			Votes = new List<Vote>();
		}
	}
}
