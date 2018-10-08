using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class ClearStatusQuest
	{
		public int questId;

		public int questStatus;

		public List<int> missionStatus = new List<int>();

		public List<int> story = new List<int>();
	}
}
