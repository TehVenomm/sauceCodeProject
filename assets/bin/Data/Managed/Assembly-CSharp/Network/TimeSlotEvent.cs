using System;

namespace Network
{
	[Serializable]
	public class TimeSlotEvent
	{
		public int timeSlotType;

		public string description;

		public string endDate = "";

		public string startData = "";
	}
}
