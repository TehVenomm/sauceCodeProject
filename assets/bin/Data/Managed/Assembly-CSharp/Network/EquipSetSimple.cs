using System;

namespace Network
{
	[Serializable]
	public class EquipSetSimple
	{
		public int setNo;

		public string weapon_0 = string.Empty;

		public string weapon_1 = string.Empty;

		public string weapon_2 = string.Empty;

		public string armor = string.Empty;

		public string arm = string.Empty;

		public string leg = string.Empty;

		public string helm = string.Empty;

		public string setName = string.Empty;

		public int showHelm;

		public AccessoryPlaceInfo acc = new AccessoryPlaceInfo();
	}
}
