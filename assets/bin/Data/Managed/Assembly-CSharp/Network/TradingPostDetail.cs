using System;

namespace Network
{
	[Serializable]
	public class TradingPostDetail
	{
		public int transactionId;

		public string from;

		public int itemId;

		public int quantity;

		public int price;
	}
}
