using System;

namespace Network
{
	[Serializable]
	public class EquipSet
	{
		public int setNo;

		public EquipItem weapon_0 = new EquipItem();

		public EquipItem weapon_1 = new EquipItem();

		public EquipItem weapon_2 = new EquipItem();

		public EquipItem armor = new EquipItem();

		public EquipItem arm = new EquipItem();

		public EquipItem leg = new EquipItem();

		public EquipItem helm = new EquipItem();

		public string setName = "";

		public int showHelm;

		public AccessoryPlaceInfo acc = new AccessoryPlaceInfo();
	}
}
