using Network;
using System;
using System.Collections.Generic;

public class OnceStatusInfoModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public UserInfo user = new UserInfo();

		public UserStatus userStatus = new UserStatus();

		public List<EquipSetSimple> equipSets = new List<EquipSetSimple>();

		public List<BoostStatus> boost = new List<BoostStatus>();

		public int followNum;

		public int followerNum;

		public GlobalSettingsManager.HasVisuals hasVisuals;

		public List<int> unlockStamps = new List<int>();

		public List<int> selectedDegrees = new List<int>();

		public List<int> unlockDegrees = new List<int>();

		public List<AccessorySet> accessorySets = new List<AccessorySet>();
	}

	public static string URL = "ajax/once/statusinfo";

	public Param result = new Param();
}
