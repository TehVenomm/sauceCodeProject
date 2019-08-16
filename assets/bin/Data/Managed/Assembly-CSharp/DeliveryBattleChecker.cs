using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DeliveryBattleChecker : BattleCheckerBase
{
	private DeliveryBattleInfo deliveryBattleInfo = new DeliveryBattleInfo();

	private Dictionary<int, int> weaponListIndex = new Dictionary<int, int>();

	public DeliveryBattleInfo GetInfo()
	{
		return deliveryBattleInfo;
	}

	public void ClearInfo()
	{
		deliveryBattleInfo = new DeliveryBattleInfo();
	}

	public void AddSkillCount(int skillId, int count = 1)
	{
		if (deliveryBattleInfo.totalSkillCountList == null)
		{
			deliveryBattleInfo.totalSkillCountList = new List<DeliveryBattleInfo.SkillCount>();
		}
		DeliveryBattleInfo.SkillCount skillCount = deliveryBattleInfo.totalSkillCountList.FirstOrDefault((DeliveryBattleInfo.SkillCount skill) => skill.skillId == skillId);
		if (skillCount == null || skillCount.skillId <= 0)
		{
			deliveryBattleInfo.totalSkillCountList.Add(new DeliveryBattleInfo.SkillCount(skillId, count));
		}
		else
		{
			skillCount.totalCount += count;
		}
	}

	public void AddMySkillCount(int skillId, int count = 1)
	{
		if (deliveryBattleInfo.mySkillCountList == null)
		{
			deliveryBattleInfo.mySkillCountList = new List<DeliveryBattleInfo.SkillCount>();
		}
		DeliveryBattleInfo.SkillCount skillCount = deliveryBattleInfo.mySkillCountList.FirstOrDefault((DeliveryBattleInfo.SkillCount skill) => skill.skillId == skillId);
		if (skillCount == null || skillCount.skillId <= 0)
		{
			deliveryBattleInfo.mySkillCountList.Add(new DeliveryBattleInfo.SkillCount(skillId, count));
		}
		else
		{
			skillCount.totalCount += count;
		}
	}

	public void SetMaxDamageSelf(int damage)
	{
		deliveryBattleInfo.maxDamageSelf = Mathf.Max(deliveryBattleInfo.maxDamageSelf, damage);
	}

	public void AddTotalAttackCount(int count = 1)
	{
		deliveryBattleInfo.totalAttackCount += count;
	}

	public void AddAttackCount(int count = 1)
	{
		deliveryBattleInfo.attackCount += count;
	}

	public void ClearDamageByWeapon()
	{
		deliveryBattleInfo.damageByWeaponList.Clear();
		deliveryBattleInfo.currentDamageByWeaponList.Clear();
		weaponListIndex.Clear();
	}

	public void ResetItemDamageByWeapon(List<EquipItemTable.EquipItemData> weaponEquipItemDataList)
	{
		if (weaponEquipItemDataList.IsNullOrEmpty())
		{
			return;
		}
		deliveryBattleInfo.currentDamageByWeaponList.Clear();
		weaponListIndex.Clear();
		for (int i = 0; i < weaponEquipItemDataList.Count; i++)
		{
			if (weaponEquipItemDataList[i] != null)
			{
				weaponListIndex.Add(i, deliveryBattleInfo.currentDamageByWeaponList.Count);
				DeliveryBattleInfo.DamageByWeapon damageByWeapon = new DeliveryBattleInfo.DamageByWeapon();
				damageByWeapon.equipmentType = (int)weaponEquipItemDataList[i].type;
				damageByWeapon.spAttackType = (int)weaponEquipItemDataList[i].spAttackType;
				deliveryBattleInfo.currentDamageByWeaponList.Add(damageByWeapon);
				deliveryBattleInfo.damageByWeaponList.Add(damageByWeapon);
			}
		}
	}

	public void AddDamageByWeapon(int weaponIndex, int damage)
	{
		if (weaponListIndex.ContainsKey(weaponIndex))
		{
			int num = weaponListIndex[weaponIndex];
			if (num < deliveryBattleInfo.currentDamageByWeaponList.Count)
			{
				deliveryBattleInfo.currentDamageByWeaponList[num].damage += damage;
			}
		}
	}

	protected override void OnNormalWeakHit(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.WEAK, damage);
	}

	protected override void OnWeaponWeakHit(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.WEAPON_WEAK, damage);
	}

	protected override void OnCounter(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.COUNTER, damage);
	}

	protected override void OnSpearSpecialAttack(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.RUSH_LANCE, damage);
	}

	protected override void OnSpearExChargeAttack(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.EX_CHARGE_LANCE, damage);
	}

	protected override void OnPairSwordsCombo(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.COMBO_PAIR_SWORDS, damage);
		base.isEnableAttackCount = false;
	}

	public void OnHeatPairSwords()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.HEAT_PAIR_SWORDS, 0);
	}

	public void OnSoulPairSwords()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_PAIR_SWORDS, 0);
	}

	protected override void OnTwoHandSwordChargeAttack(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.CHARGE_SWORD, damage);
	}

	public void OnSoulTwoHandSword()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_TWOHAND_SWORD, 0);
	}

	protected override void OnTwoHandSwordExChargeAttack(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.EX_CHARGE_TOW_HAND_SWORD, damage);
	}

	protected override void OnArrowChargeAttack(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.CHARGE_BOW, damage);
	}

	public void OnSoulArrow()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_ARROW, 0);
	}

	protected override void OnHeatTwoHandSword(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.HEAT_TWOHAND_SWORD, damage);
	}

	protected override void OnRevengeBurst(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.REVENGE_BURST, damage);
	}

	public void OnJustGuard()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.JUST_GUARD, 0);
	}

	public void OnSoulOneHandSword()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_ONE_HAND_SWORD, 0);
	}

	public void OnShadowSealing()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SHADOW_SEALING, 0);
	}

	public void OnJump(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.JUMP, damage);
	}

	public void OnSoulSpear()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_SPEAR, 0);
	}

	protected override void OnBurstOneHandSword(int damage)
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_ONE_HAND_SWORD, damage);
	}

	public void OnBurstTwoHandSword()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_TWO_HAND_SWORD, 0);
	}

	public void OnBurstPairSwords()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_PAIR_SWORDS, 0);
	}

	public void OnBurstSpear()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_SPEAR, 0);
	}

	public void OnBurstArrow()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_ARROW, 0);
	}

	public void OnConcussion()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.CONCUSSION, 0);
	}

	public void OnOracleOneHandSword()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.ORACLE_ONE_HAND_SWORD, 0);
	}

	public void OnOracleSpear()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.ORACLE_SPEAR, 0);
	}

	public void OnOraclePairSwords()
	{
		UpdateHitTypeInfo(PLAYER_ACTION_TYPE.ORACLE_PAIR_SWORDS, 0);
	}

	private void UpdateHitTypeInfo(PLAYER_ACTION_TYPE type, int damage)
	{
		DeliveryBattleInfo.PlayerActionInfo playerActionInfo = null;
		int i = 0;
		for (int count = deliveryBattleInfo.playerActionInfoList.Count; i < count; i++)
		{
			DeliveryBattleInfo.PlayerActionInfo playerActionInfo2 = deliveryBattleInfo.playerActionInfoList[i];
			if (playerActionInfo2.actionType == (int)type)
			{
				playerActionInfo = playerActionInfo2;
				break;
			}
		}
		if (playerActionInfo != null)
		{
			playerActionInfo.totalCount++;
			playerActionInfo.totalDamage += damage;
		}
		else
		{
			DeliveryBattleInfo.PlayerActionInfo item = new DeliveryBattleInfo.PlayerActionInfo(type, damage, 1);
			deliveryBattleInfo.playerActionInfoList.Add(item);
		}
	}
}
