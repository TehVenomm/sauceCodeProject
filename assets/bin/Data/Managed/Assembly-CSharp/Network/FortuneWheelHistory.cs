using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class FortuneWheelHistory
	{
		public int jackpot;

		public List<FortuneWheelServerLog> server;

		public List<FortuneWheelUserLog> user;
	}
}
