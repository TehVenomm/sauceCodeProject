using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class FortuneWheelHistory
	{
		public List<FortuneWheelServerLog> server;

		public List<FortuneWheelRewardLog> user;
	}
}
