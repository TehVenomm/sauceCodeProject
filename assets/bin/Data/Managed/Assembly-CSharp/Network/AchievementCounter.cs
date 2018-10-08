using System;

namespace Network
{
	[Serializable]
	public class AchievementCounter
	{
		public int type;

		public int subType;

		public string count;

		public ACHIEVEMENT_TYPE Type => (ACHIEVEMENT_TYPE)type;

		public long Count => long.Parse(count);
	}
}
