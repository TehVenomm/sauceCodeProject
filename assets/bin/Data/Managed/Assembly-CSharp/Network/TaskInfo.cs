using System;

namespace Network
{
	[Serializable]
	public class TaskInfo
	{
		public enum STATUS
		{
			NONE,
			NOT_ACHIEVED,
			NOT_RECIEVED,
			RECEIVED
		}

		public int taskId;

		public int status;

		public int newFlg;

		public int progress;
	}
}
