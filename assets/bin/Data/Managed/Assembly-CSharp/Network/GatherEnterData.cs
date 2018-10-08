using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class GatherEnterData
	{
		public class Fairy
		{
			public int lost;

			public int add;
		}

		public class Gather
		{
			public int gatherPointId;

			public int gatherObjectId;
		}

		public Fairy fairy = new Fairy();

		public List<Gather> appear = new List<Gather>();

		public List<Gather> open = new List<Gather>();

		public List<Gather> disappear = new List<Gather>();
	}
}
