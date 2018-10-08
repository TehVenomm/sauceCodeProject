using System;

namespace Network
{
	[Serializable]
	public class TimeSlotEvent
	{
		public int timeSlotType;

		public string description;

		public string endDate = string.Empty;

		public string startData = string.Empty;
	}
}
