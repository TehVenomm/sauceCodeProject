using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class DeliveryBattleInfo
	{
		[Serializable]
		public class SkillCount
		{
			public int skillId;

			public int totalCount;

			public SkillCount(int skillId, int count)
			{
				this.skillId = skillId;
				totalCount = count;
			}
		}

		[Serializable]
		public class DamageByWeapon
		{
			public int equipmentType = 9999;

			public int spAttackType;

			public int damage;
		}

		[Serializable]
		public class PlayerActionInfo
		{
			public int actionType;

			public int totalDamage;

			public int totalCount;

			public PlayerActionInfo(PLAYER_ACTION_TYPE type, int damage, int count)
			{
				actionType = (int)type;
				totalDamage = damage;
				totalCount = count;
			}
		}

		public int maxDamageSelf;

		public int totalAttackCount;

		public List<SkillCount> totalSkillCountList = new List<SkillCount>();

		public List<DamageByWeapon> damageByWeaponList = new List<DamageByWeapon>();

		public List<PlayerActionInfo> playerActionInfoList = new List<PlayerActionInfo>();
	}
}
