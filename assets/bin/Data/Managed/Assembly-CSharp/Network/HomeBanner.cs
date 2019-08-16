using System;

namespace Network
{
	[Serializable]
	public class HomeBanner
	{
		public int id;

		public int bannerId;

		public int homeType;

		public string targetString;

		public EndDate endDate = new EndDate();

		public HOME_TYPE HomeType => (HOME_TYPE)homeType;
	}
}
