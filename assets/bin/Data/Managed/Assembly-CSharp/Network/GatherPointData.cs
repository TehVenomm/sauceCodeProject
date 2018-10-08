using System;

namespace Network
{
	[Serializable]
	public class GatherPointData
	{
		public int gatherPointId;

		public int gatherObjectId;

		public int gatherCount;

		public int status;

		public int rest;

		public int attackTime;

		public EndDate appearAt;

		public EndDate disappearAt;

		public EndDate gatherEndAt;

		public GATHER_POINT_STATUS pointStatus => (GATHER_POINT_STATUS)status;
	}
}
