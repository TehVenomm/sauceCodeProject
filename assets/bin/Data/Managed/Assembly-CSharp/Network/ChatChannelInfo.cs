using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class ChatChannelInfo
	{
		public List<int> channels = new List<int>();

		public int recommend;

		public int hot;

		public int cold;
	}
}
