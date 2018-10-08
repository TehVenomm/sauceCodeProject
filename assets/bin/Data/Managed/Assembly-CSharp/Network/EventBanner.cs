using System;

namespace Network
{
	[Serializable]
	public class EventBanner
	{
		public int bannerId;

		public int linkType;

		public int param;

		public int orderNo;

		public EndDate endDate = new EndDate();

		public LINK_TYPE LinkType => (LINK_TYPE)linkType;
	}
}
