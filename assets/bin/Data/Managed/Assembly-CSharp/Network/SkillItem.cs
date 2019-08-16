using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class SkillItem
	{
		public class EquipSetSlot
		{
			public int setNo;

			public string euid;

			public int slotNo;

			public EquipSetSlot()
			{
			}

			public EquipSetSlot(int sNo, string eId, int slNo)
			{
				setNo = sNo;
				euid = eId;
				slotNo = slNo;
			}
		}

		public class UniqueEquipSetSlot
		{
			public string euid;

			public int slotNo;

			public UniqueEquipSetSlot()
			{
			}

			public UniqueEquipSetSlot(string eId, int slNo)
			{
				euid = eId;
				slotNo = slNo;
			}
		}

		public class DiffEquipSetSlot : EquipSetSlot
		{
			public string uniqId;

			public DiffEquipSetSlot()
			{
			}

			public DiffEquipSetSlot(int sNo, string eId, int slNo, string uId)
				: base(sNo, eId, slNo)
			{
				uniqId = uId;
			}
		}

		public string uniqId;

		public int skillItemId;

		public XorInt level = 0;

		public int exceed;

		public int exceedExp;

		public int is_locked;

		public int exp;

		public int expPrev;

		public int expNext;

		public float growCost;

		public int price;

		public List<EquipSetSlot> equipSlots = new List<EquipSetSlot>();

		public UniqueEquipSetSlot uniqueEquipSlots = new UniqueEquipSetSlot();

		public int RelativeExp => exp - expPrev;

		public int RelativeExpNext => expNext - expPrev;

		public float ExpProgress01 => (RelativeExpNext > 0) ? ((float)RelativeExp / (float)RelativeExpNext) : 1f;
	}
}
