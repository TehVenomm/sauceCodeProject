using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class PresentList
	{
		public List<Present> presents = new List<Present>();

		public int totalCount;

		public int page;

		public int pageNumMax;
	}
}
