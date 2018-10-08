using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class EquipItem
	{
		public class Ability
		{
			public int id;

			public int pt;

			public bool vr;
		}

		public string uniqId;

		public int equipItemId;

		public XorInt level = 0;

		public int exceed;

		public int is_locked;

		public int price;

		public List<Ability> ability = new List<Ability>();

		public AbilityItem abilityItem;
	}
}
