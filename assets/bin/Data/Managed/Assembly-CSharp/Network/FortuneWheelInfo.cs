using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class FortuneWheelInfo
	{
		public int curTicket;

		public int requiredTicket;

		public int requiredTicketx10;

		public int jackpot;

		public List<FortuneWheelItem> itemList;

		public int ticketPrice;

		public bool freeSpin;
	}
}
