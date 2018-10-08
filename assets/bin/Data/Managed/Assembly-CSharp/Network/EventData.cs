using System;

namespace Network
{
	public class EventData
	{
		public int eventId;

		public string name;

		public int bannerId;

		public string linkName;

		public string minVersion;

		public EndDate endDate = new EndDate();

		public int rest;

		public int prologueStoryId;

		public string prologueTitle;

		public bool readPrologueStory;

		public int eventType;

		public int hostCountLimit;

		private DateTime receiveDateTime;

		private Version requiredVersion;

		public bool IsPlayableWith(Version version)
		{
			if (string.IsNullOrEmpty(minVersion))
			{
				return true;
			}
			return version >= requiredVersion;
		}

		public bool HasEndDate()
		{
			return !string.IsNullOrEmpty(endDate.date);
		}

		public int GetRest()
		{
			int num = (int)(DateTime.UtcNow - receiveDateTime).TotalSeconds;
			if (num < 0)
			{
				num = 0;
			}
			return rest - num;
		}

		public void OnRecv()
		{
			receiveDateTime = DateTime.UtcNow;
			if (!string.IsNullOrEmpty(minVersion))
			{
				requiredVersion = new Version(minVersion);
			}
		}
	}
}
