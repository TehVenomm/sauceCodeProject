using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class TradingPostTransactionLog
	{
		[Serializable]
		public class ActiveLog
		{
			public int transactionId;

			public string from;

			public int itemId;

			public int quantity;

			public int price;

			public string expiredTime;

			public string createdAt;

			public int status;
		}

		public List<ActiveLog> activeList;

		public List<ActiveLog> historyList;
	}
}
