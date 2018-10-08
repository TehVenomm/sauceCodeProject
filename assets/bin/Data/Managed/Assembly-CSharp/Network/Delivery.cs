using System;

namespace Network
{
	[Serializable]
	public class Delivery
	{
		public string uId;

		public int dId;

		public int type;

		public int mode;

		public string limit;

		public int order;

		public DIFFICULTY_MODE fieldMode => (DIFFICULTY_MODE)mode;
	}
}
