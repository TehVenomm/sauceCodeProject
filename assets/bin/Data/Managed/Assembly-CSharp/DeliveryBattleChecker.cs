// Decompiled with JetBrains decompiler
// Type: DeliveryBattleChecker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
[Serializable]
public class DeliveryBattleChecker : BattleCheckerBase
{
  private DeliveryBattleInfo deliveryBattleInfo = new DeliveryBattleInfo();
  private Dictionary<int, int> weaponListIndex = new Dictionary<int, int>();

  public DeliveryBattleInfo GetInfo() => this.deliveryBattleInfo;

  public void ClearInfo() => this.deliveryBattleInfo = new DeliveryBattleInfo();

  public void AddSkillCount(int skillId, int count = 1)
  {
    if (this.deliveryBattleInfo.totalSkillCountList == null)
      this.deliveryBattleInfo.totalSkillCountList = new List<DeliveryBattleInfo.SkillCount>();
    DeliveryBattleInfo.SkillCount skillCount = this.deliveryBattleInfo.totalSkillCountList.FirstOrDefault<DeliveryBattleInfo.SkillCount>((Func<DeliveryBattleInfo.SkillCount, bool>) (skill => skill.skillId == skillId));
    if (skillCount == null || skillCount.skillId <= 0)
      this.deliveryBattleInfo.totalSkillCountList.Add(new DeliveryBattleInfo.SkillCount(skillId, count));
    else
      skillCount.totalCount += count;
  }

  public void AddMySkillCount(int skillId, int count = 1)
  {
    if (this.deliveryBattleInfo.mySkillCountList == null)
      this.deliveryBattleInfo.mySkillCountList = new List<DeliveryBattleInfo.SkillCount>();
    DeliveryBattleInfo.SkillCount skillCount = this.deliveryBattleInfo.mySkillCountList.FirstOrDefault<DeliveryBattleInfo.SkillCount>((Func<DeliveryBattleInfo.SkillCount, bool>) (skill => skill.skillId == skillId));
    if (skillCount == null || skillCount.skillId <= 0)
      this.deliveryBattleInfo.mySkillCountList.Add(new DeliveryBattleInfo.SkillCount(skillId, count));
    else
      skillCount.totalCount += count;
  }

  public void SetMaxDamageSelf(int damage)
  {
    this.deliveryBattleInfo.maxDamageSelf = Mathf.Max(this.deliveryBattleInfo.maxDamageSelf, damage);
  }

  public void AddTotalAttackCount(int count = 1)
  {
    this.deliveryBattleInfo.totalAttackCount += count;
  }

  public void AddAttackCount(int count = 1) => this.deliveryBattleInfo.attackCount += count;

  public void ClearDamageByWeapon()
  {
    this.deliveryBattleInfo.damageByWeaponList.Clear();
    this.deliveryBattleInfo.currentDamageByWeaponList.Clear();
    this.weaponListIndex.Clear();
  }

  public void ResetItemDamageByWeapon(
    List<EquipItemTable.EquipItemData> weaponEquipItemDataList)
  {
    if (weaponEquipItemDataList.IsNullOrEmpty<EquipItemTable.EquipItemData>())
      return;
    this.deliveryBattleInfo.currentDamageByWeaponList.Clear();
    this.weaponListIndex.Clear();
    for (int index = 0; index < weaponEquipItemDataList.Count; ++index)
    {
      if (weaponEquipItemDataList[index] != null)
      {
        this.weaponListIndex.Add(index, this.deliveryBattleInfo.currentDamageByWeaponList.Count);
        DeliveryBattleInfo.DamageByWeapon damageByWeapon = new DeliveryBattleInfo.DamageByWeapon();
        damageByWeapon.equipmentType = (int) weaponEquipItemDataList[index].type;
        damageByWeapon.spAttackType = (int) weaponEquipItemDataList[index].spAttackType;
        this.deliveryBattleInfo.currentDamageByWeaponList.Add(damageByWeapon);
        this.deliveryBattleInfo.damageByWeaponList.Add(damageByWeapon);
      }
    }
  }

  public void AddDamageByWeapon(int weaponIndex, int damage)
  {
    if (!this.weaponListIndex.ContainsKey(weaponIndex))
      return;
    int index = this.weaponListIndex[weaponIndex];
    if (index >= this.deliveryBattleInfo.currentDamageByWeaponList.Count)
      return;
    this.deliveryBattleInfo.currentDamageByWeaponList[index].damage += damage;
  }

  protected override void OnNormalWeakHit(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.WEAK, damage);
  }

  protected override void OnWeaponWeakHit(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.WEAPON_WEAK, damage);
  }

  protected override void OnCounter(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.COUNTER, damage);
  }

  protected override void OnSpearSpecialAttack(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.RUSH_LANCE, damage);
  }

  protected override void OnSpearExChargeAttack(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.EX_CHARGE_LANCE, damage);
  }

  protected override void OnPairSwordsCombo(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.COMBO_PAIR_SWORDS, damage);
    this.isEnableAttackCount = false;
  }

  public void OnHeatPairSwords() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.HEAT_PAIR_SWORDS, 0);

  public void OnSoulPairSwords() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_PAIR_SWORDS, 0);

  protected override void OnTwoHandSwordChargeAttack(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.CHARGE_SWORD, damage);
  }

  public void OnSoulTwoHandSword()
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_TWOHAND_SWORD, 0);
  }

  protected override void OnTwoHandSwordExChargeAttack(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.EX_CHARGE_TOW_HAND_SWORD, damage);
  }

  protected override void OnArrowChargeAttack(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.CHARGE_BOW, damage);
  }

  public void OnSoulArrow() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_ARROW, 0);

  protected override void OnHeatTwoHandSword(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.HEAT_TWOHAND_SWORD, damage);
  }

  protected override void OnRevengeBurst(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.REVENGE_BURST, damage);
  }

  public void OnJustGuard() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.JUST_GUARD, 0);

  public void OnSoulOneHandSword()
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_ONE_HAND_SWORD, 0);
  }

  public void OnShadowSealing() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SHADOW_SEALING, 0);

  public void OnJump(int damage) => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.JUMP, damage);

  public void OnSoulSpear() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.SOUL_SPEAR, 0);

  protected override void OnBurstOneHandSword(int damage)
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_ONE_HAND_SWORD, damage);
  }

  public void OnBurstTwoHandSword()
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_TWO_HAND_SWORD, 0);
  }

  public void OnBurstPairSwords()
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_PAIR_SWORDS, 0);
  }

  public void OnBurstSpear() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_SPEAR, 0);

  public void OnBurstArrow() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.BURST_ARROW, 0);

  public void OnConcussion() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.CONCUSSION, 0);

  public void OnOracleOneHandSword()
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.ORACLE_ONE_HAND_SWORD, 0);
  }

  public void OnOracleSpear() => this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.ORACLE_SPEAR, 0);

  public void OnOraclePairSwords()
  {
    this.UpdateHitTypeInfo(PLAYER_ACTION_TYPE.ORACLE_PAIR_SWORDS, 0);
  }

  private void UpdateHitTypeInfo(PLAYER_ACTION_TYPE type, int damage)
  {
    int num = (int) type;
    DeliveryBattleInfo.PlayerActionInfo playerActionInfo1 = (DeliveryBattleInfo.PlayerActionInfo) null;
    int index = 0;
    for (int count = this.deliveryBattleInfo.playerActionInfoList.Count; index < count; ++index)
    {
      DeliveryBattleInfo.PlayerActionInfo playerActionInfo2 = this.deliveryBattleInfo.playerActionInfoList[index];
      if (playerActionInfo2.actionType == num)
      {
        playerActionInfo1 = playerActionInfo2;
        break;
      }
    }
    if (playerActionInfo1 != null)
    {
      ++playerActionInfo1.totalCount;
      playerActionInfo1.totalDamage += damage;
    }
    else
      this.deliveryBattleInfo.playerActionInfoList.Add(new DeliveryBattleInfo.PlayerActionInfo(type, damage, 1));
  }
}
