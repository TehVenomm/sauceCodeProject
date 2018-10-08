using System;

namespace Network
{
	[Serializable]
	public class TheaterModePostData
	{
		public int theaterId;

		public int deliveryId;

		public int storyId;

		public TheaterModePostData(int id, int delivery, int story)
		{
			theaterId = id;
			deliveryId = delivery;
			storyId = story;
		}
	}
}
