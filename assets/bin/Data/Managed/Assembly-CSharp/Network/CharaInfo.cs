using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class CharaInfo
	{
		[Serializable]
		public class EquipItem
		{
			public int eId;

			public int lv;

			public int exceed;

			public List<int> sIds = new List<int>();

			public List<int> sLvs = new List<int>();

			public List<int> sExs = new List<int>();

			public List<int> aIds = new List<int>();

			public List<int> aPts = new List<int>();

			public AbilityItem ai = new AbilityItem();

			public override string ToString()
			{
				string empty = string.Empty;
				empty = empty + eId + ",";
				empty = empty + lv + ",";
				int i = 0;
				for (int count = sIds.Count; i < count; i++)
				{
					empty += "s(";
					empty = empty + sIds[i] + ",";
					empty = empty + sLvs[i] + ",";
					int num = 0;
					if (i < sExs.Count)
					{
						num = sExs[i];
					}
					empty = empty + num.ToString() + ",";
					empty += "),";
				}
				int j = 0;
				for (int count2 = aIds.Count; j < count2; j++)
				{
					empty += "a(";
					empty = empty + aIds[j] + ",";
					empty = empty + aPts[j] + ",";
					empty += "),";
				}
				string text = empty;
				empty = text + "ai(" + ai + ")";
				return base.ToString() + empty;
			}

			public int GetSkillExceed(int idx)
			{
				if (sExs == null || sExs.Count <= idx)
				{
					return 0;
				}
				return sExs[idx];
			}
		}

		[Serializable]
		public class ClanInfo
		{
			public int clanId;

			public int[] emblem;

			public string tag;
		}

		[Serializable]
		public class UserAccessory
		{
			public string uniqId;

			public int accessoryId;

			public int place;
		}

		public int userId;

		public string name;

		public string comment;

		public string lastLogin;

		public int lastLoginTm;

		public string code;

		public XorInt hp = 0;

		public XorInt atk = 0;

		public XorInt def = 0;

		public XorInt level = 0;

		public int sex;

		public int faceId;

		public int hairId;

		public int hairColorId;

		public int skinId;

		public int voiceId;

		public int aId;

		public int hId;

		public int rId;

		public int lId;

		public int showHelm;

		public string equipSetName;

		public List<EquipItem> equipSet = new List<EquipItem>();

		public List<int> selectedDegrees;

		public ClanInfo clanInfo;

		public UserClanData userClanData;

		public bool isInviteToClan;

		public List<UserAccessory> accessory = new List<UserAccessory>();

		public bool isEqualAccessory(List<UserAccessory> src)
		{
			if (accessory.IsNullOrEmpty() && src.IsNullOrEmpty())
			{
				return true;
			}
			if (accessory.IsNullOrEmpty() || src.IsNullOrEmpty())
			{
				return false;
			}
			if (accessory.Count != src.Count)
			{
				return false;
			}
			for (int i = 0; i < accessory.Count; i++)
			{
				UserAccessory userAccessory = accessory[i];
				for (int j = 0; j < src.Count; j++)
				{
					UserAccessory userAccessory2 = src[j];
					if (userAccessory.uniqId == userAccessory2.uniqId && userAccessory.accessoryId == userAccessory2.accessoryId && userAccessory.place == userAccessory2.place)
					{
						return true;
					}
				}
			}
			return false;
		}

		public override string ToString()
		{
			string str = string.Empty;
			str = str + userId + ",";
			str = str + name + ",";
			str = str + comment + ",";
			str = str + lastLogin + ",";
			str = str + lastLoginTm + ",";
			str = str + code + ",";
			str = str + hp + ",";
			str = str + atk + ",";
			str = str + def + ",";
			str = str + level + ",";
			str = str + sex + ",";
			str = str + faceId + ",";
			str = str + hairId + ",";
			str = str + hairColorId + ",";
			str = str + skinId + ",";
			str = str + voiceId + ",";
			str = str + aId + ",";
			str = str + hId + ",";
			str = str + rId + ",";
			str = str + lId + ",";
			str = str + showHelm + ",";
			if (equipSet != null)
			{
				equipSet.ForEach(delegate(EquipItem e)
				{
					string text2 = str;
					str = text2 + "[" + e + "],";
				});
			}
			if (!accessory.IsNullOrEmpty())
			{
				accessory.ForEach(delegate(UserAccessory a)
				{
					string text = str;
					str = text + "[" + a + "],";
				});
			}
			return base.ToString() + str;
		}
	}
}
