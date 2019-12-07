using System;

namespace Network
{
	[Serializable]
	public class EquipSetSimple
	{
		public int setNo;

		public string weapon_0 = "";

		public string weapon_1 = "";

		public string weapon_2 = "";

		public string armor = "";

		public string arm = "";

		public string leg = "";

		public string helm = "";

		public string setName = "";

		public int showHelm;

		public AccessoryPlaceInfo acc = new AccessoryPlaceInfo();

		public int order;
	}
}
