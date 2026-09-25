// Decompiled with JetBrains decompiler
// Type: EquipSetCalculator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class EquipSetCalculator
{
  private EquipValue[] equipValues = new EquipValue[7];
  private StatusFactor cachedFactor = new StatusFactor();
  private SimpleStatus cachedFinal = new SimpleStatus();
  private int cachedWeaponIndex = -1;
  private int cachedHp;
  private int cachedAtk;
  private int cachedDef;
  private EquipSetCalculator.eDirtyState dirtyFlag;
  private int setWeaponIndex;

  private void _Reset()
  {
    for (int index = 0; index < 7; ++index)
      this.equipValues[index] = (EquipValue) null;
    this.cachedFinal.Reset();
    this.cachedFactor.Reset();
    this.cachedWeaponIndex = -1;
    this.cachedHp = 0;
    this.cachedAtk = 0;
    this.cachedDef = 0;
    this.dirtyFlag = EquipSetCalculator.eDirtyState.Dirty;
    this.setWeaponIndex = 0;
  }

  public void SetEquipSet(EquipSetInfo equipSet, int setNo, bool isUnique = false)
  {
    if (equipSet == null)
      return;
    this.SetEquipSet(!isUnique ? equipSet.ConvertSelfEquipSetItemList(setNo, true) : equipSet.ConvertSelfUniqueEquipSetItemList(setNo, true), true);
  }

  public void SetEquipSet(List<CharaInfo.EquipItem> equips, bool isAddNull = false)
  {
    if (equips == null)
      return;
    this._Reset();
    for (int index = 0; index < equips.Count; ++index)
      this.SetEquipItem(equips[index], isAddNull ? index : -1);
  }

  public void SetEquipItem(CharaInfo.EquipItem item, int index)
  {
    if (item == null)
    {
      if (index == -1)
        return;
      this.equipValues[index] = (EquipValue) null;
      this.dirtyFlag = EquipSetCalculator.eDirtyState.Dirty;
    }
    else
    {
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) item.eId);
      if (equipItemData == null)
        return;
      if (index == -1)
      {
        switch (equipItemData.type)
        {
          case EQUIPMENT_TYPE.ONE_HAND_SWORD:
          case EQUIPMENT_TYPE.TWO_HAND_SWORD:
          case EQUIPMENT_TYPE.SPEAR:
          case EQUIPMENT_TYPE.PAIR_SWORDS:
          case EQUIPMENT_TYPE.ARROW:
            index = this.setWeaponIndex;
            ++this.setWeaponIndex;
            break;
          case EQUIPMENT_TYPE.ARMOR:
            index = 3;
            break;
          case EQUIPMENT_TYPE.HELM:
            index = 4;
            break;
          case EQUIPMENT_TYPE.ARM:
            index = 5;
            break;
          case EQUIPMENT_TYPE.LEG:
            index = 6;
            break;
          default:
            return;
        }
      }
      EquipValue equipValue = new EquipValue();
      equipValue.Parse(item, equipItemData);
      this.equipValues[index] = equipValue;
      this.dirtyFlag = EquipSetCalculator.eDirtyState.Dirty;
    }
  }

  public void SwapWeapon(int swapIndex, int nowIndex)
  {
    EquipValue equipValue = this.equipValues[nowIndex];
    this.equipValues[nowIndex] = this.equipValues[swapIndex];
    this.equipValues[swapIndex] = equipValue;
    this.dirtyFlag = EquipSetCalculator.eDirtyState.Dirty;
  }

  public StatusFactor GetStatusFactor(int weaponIndex)
  {
    if (weaponIndex < 0 || weaponIndex >= 3)
      return (StatusFactor) null;
    if (weaponIndex == this.cachedWeaponIndex && this.dirtyFlag >= EquipSetCalculator.eDirtyState.CalcFactor)
      return this.cachedFactor;
    this.cachedFactor.Reset();
    EQUIPMENT_TYPE weaponType = EQUIPMENT_TYPE.NONE;
    SP_ATTACK_TYPE spAttackType = SP_ATTACK_TYPE.NONE;
    for (int index1 = 0; index1 < 7; ++index1)
    {
      if (this.equipValues[index1] != null)
      {
        EquipValue equipValue = this.equipValues[index1];
        if (index1 < 3)
        {
          if (index1 == weaponIndex)
          {
            weaponType = equipValue.type;
            spAttackType = equipValue.spAttackType;
          }
          else
            continue;
        }
        this.cachedFactor.baseStatus.hp += equipValue.baseStatus.hp;
        this.cachedFactor.constHp += equipValue.constHp;
        for (int index2 = 0; index2 < 7; ++index2)
        {
          this.cachedFactor.baseStatus.attacks[index2] += equipValue.baseStatus.attacks[index2];
          this.cachedFactor.constAtks[index2] += equipValue.constAtks[index2];
        }
        this.cachedFactor.baseStatus.defences[0] += equipValue.baseStatus.defences[0];
        this.cachedFactor.constDefs[0] += equipValue.constDefs[0];
        for (int index3 = 0; index3 < 6; ++index3)
        {
          this.cachedFactor.baseStatus.tolerances[index3] += equipValue.baseStatus.tolerances[index3];
          this.cachedFactor.constTols[index3] += equipValue.constTols[index3];
        }
        this._GetEnableSkillSupport(weaponType, spAttackType, equipValue);
      }
    }
    for (int index = 0; index < 6; ++index)
      this.cachedFactor.baseStatus.defences[index + 1] += this.cachedFactor.baseStatus.defences[0];
    this.cachedFactor.CheckMinusRate();
    this.cachedWeaponIndex = weaponIndex;
    this.dirtyFlag = EquipSetCalculator.eDirtyState.CalcFactor;
    return this.cachedFactor;
  }

  public SimpleStatus GetFinalStatus(int weaponIndex, UserStatus status)
  {
    return this.GetFinalStatus(weaponIndex, (int) status.hp, (int) status.atk, (int) status.def);
  }

  public SimpleStatus GetFinalStatus(int weaponIndex, int userHp, int userAtk, int userDef)
  {
    if (weaponIndex == this.cachedWeaponIndex && userHp == this.cachedHp && userAtk == this.cachedAtk && userDef == this.cachedDef && this.dirtyFlag == EquipSetCalculator.eDirtyState.CalcFinal)
      return this.cachedFinal;
    this.GetStatusFactor(weaponIndex);
    this.cachedFinal.Reset();
    this.cachedFinal.hp = (int) ((double) (userHp + this.cachedFactor.baseStatus.hp) * (double) this.cachedFactor.hpRate + (double) this.cachedFactor.constHp);
    if (this.cachedFinal.hp < 1)
      this.cachedFinal.hp = 1;
    for (int index = 0; index < 7; ++index)
    {
      this.cachedFinal.attacks[index] = index != 0 ? (int) ((double) this.cachedFactor.baseStatus.attacks[index] * (double) this.cachedFactor.atkRate[index] + (double) this.cachedFactor.constAtks[index]) : (int) ((double) (userAtk + this.cachedFactor.baseStatus.attacks[index]) * (double) this.cachedFactor.atkRate[index] + (double) this.cachedFactor.constAtks[index]);
      if (this.cachedFinal.attacks[index] < 0)
        this.cachedFinal.attacks[index] = 0;
      this.cachedFinal.defences[index] = (int) ((double) (userDef + this.cachedFactor.baseStatus.defences[index]) * (double) this.cachedFactor.defRate[index] + (double) this.cachedFactor.constDefs[index]);
      if (this.cachedFinal.defences[index] < 0)
        this.cachedFinal.defences[index] = 0;
    }
    for (int index = 0; index < 6; ++index)
      this.cachedFinal.tolerances[index] = (int) ((double) this.cachedFactor.baseStatus.tolerances[index] * (double) this.cachedFactor.tolRate[index] + (double) this.cachedFactor.constTols[index]);
    this.cachedHp = userHp;
    this.cachedAtk = userAtk;
    this.cachedDef = userDef;
    this.dirtyFlag = EquipSetCalculator.eDirtyState.CalcFinal;
    return this.cachedFinal;
  }

  private void _GetEnableSkillSupport(
    EQUIPMENT_TYPE weaponType,
    SP_ATTACK_TYPE spAttackType,
    EquipValue equip)
  {
    int index1 = 0;
    for (int count = equip.skillSupport.Count; index1 < count; ++index1)
    {
      EquipValue.SkillSupport skillSupport = equip.skillSupport[index1];
      if (Utility.IsEnableEquip(weaponType, skillSupport.targetEquip) && Utility.IsEnableSpAttackType(skillSupport.targetSpAttackType, spAttackType))
      {
        switch (skillSupport.type)
        {
          case BuffParam.BUFFTYPE.ATTACK_NORMAL:
            this.cachedFactor.constAtks[0] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_FIRE:
            this.cachedFactor.constAtks[1] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_WATER:
            this.cachedFactor.constAtks[2] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_THUNDER:
            this.cachedFactor.constAtks[3] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_SOIL:
            this.cachedFactor.constAtks[4] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_LIGHT:
            this.cachedFactor.constAtks[5] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_DARK:
            this.cachedFactor.constAtks[6] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_ALLELEMENT:
            for (int index2 = 0; index2 < 6; ++index2)
              this.cachedFactor.constAtks[index2 + 1] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_NORMAL:
            this.cachedFactor.constDefs[0] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_FIRE:
            this.cachedFactor.constDefs[1] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_WATER:
            this.cachedFactor.constDefs[2] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_THUNDER:
            this.cachedFactor.constDefs[3] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_SOIL:
            this.cachedFactor.constDefs[4] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_LIGHT:
            this.cachedFactor.constDefs[5] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_DARK:
            this.cachedFactor.constDefs[6] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT:
            for (int index3 = 0; index3 < 6; ++index3)
              this.cachedFactor.constDefs[index3 + 1] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATKUP_RATE_NORMAL:
            this.cachedFactor.atkRate[0] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKUP_RATE_FIRE:
            this.cachedFactor.atkRate[1] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKUP_RATE_WATER:
            this.cachedFactor.atkRate[2] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKUP_RATE_THUNDER:
            this.cachedFactor.atkRate[3] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKUP_RATE_SOIL:
            this.cachedFactor.atkRate[4] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKUP_RATE_LIGHT:
            this.cachedFactor.atkRate[5] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKUP_RATE_DARK:
            this.cachedFactor.atkRate[6] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT:
            for (int index4 = 0; index4 < 6; ++index4)
              this.cachedFactor.atkRate[index4 + 1] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFUP_RATE_NORMAL:
            this.cachedFactor.defRate[0] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFUP_RATE_FIRE:
            this.cachedFactor.defRate[1] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFUP_RATE_WATER:
            this.cachedFactor.defRate[2] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFUP_RATE_THUNDER:
            this.cachedFactor.defRate[3] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFUP_RATE_SOIL:
            this.cachedFactor.defRate[4] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFUP_RATE_LIGHT:
            this.cachedFactor.defRate[5] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFUP_RATE_DARK:
            this.cachedFactor.defRate[6] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFUP_RATE_ALLELEMENT:
            for (int index5 = 0; index5 < 6; ++index5)
              this.cachedFactor.defRate[index5 + 1] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL:
            this.cachedFactor.defRate[0] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT:
            for (int index6 = 0; index6 < 6; ++index6)
              this.cachedFactor.defRate[index6 + 1] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.HP_UP:
            this.cachedFactor.constHp += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.HP_DOWN:
            this.cachedFactor.constHp -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.HPUP_RATE:
            this.cachedFactor.hpRate += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.HPDOWN_RATE:
            this.cachedFactor.hpRate -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_DOWN_NORMAL:
            this.cachedFactor.constAtks[0] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_DOWN_FIRE:
            this.cachedFactor.constAtks[1] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_DOWN_WATER:
            this.cachedFactor.constAtks[2] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_DOWN_THUNDER:
            this.cachedFactor.constAtks[3] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_DOWN_SOIL:
            this.cachedFactor.constAtks[4] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_DOWN_LIGHT:
            this.cachedFactor.constAtks[5] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_DOWN_DARK:
            this.cachedFactor.constAtks[6] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATTACK_DOWN_ALLELEMENT:
            for (int index7 = 0; index7 < 6; ++index7)
              this.cachedFactor.constAtks[index7 + 1] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.ATKDOWN_RATE_NORMAL:
            this.cachedFactor.atkRate[0] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKDOWN_RATE_FIRE:
            this.cachedFactor.atkRate[1] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKDOWN_RATE_WATER:
            this.cachedFactor.atkRate[2] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKDOWN_RATE_THUNDER:
            this.cachedFactor.atkRate[3] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKDOWN_RATE_SOIL:
            this.cachedFactor.atkRate[4] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKDOWN_RATE_LIGHT:
            this.cachedFactor.atkRate[5] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKDOWN_RATE_DARK:
            this.cachedFactor.atkRate[6] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKDOWN_RATE_ALLELEMENT:
            for (int index8 = 0; index8 < 6; ++index8)
              this.cachedFactor.atkRate[index8 + 1] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_DOWN_NORMAL:
            this.cachedFactor.constDefs[0] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_DOWN_FIRE:
            this.cachedFactor.constDefs[1] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_DOWN_WATER:
            this.cachedFactor.constDefs[2] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_DOWN_THUNDER:
            this.cachedFactor.constDefs[3] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_DOWN_SOIL:
            this.cachedFactor.constDefs[4] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_DOWN_LIGHT:
            this.cachedFactor.constDefs[5] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_DOWN_DARK:
            this.cachedFactor.constDefs[6] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFENCE_DOWN_ALLELEMENT:
            for (int index9 = 0; index9 < 6; ++index9)
              this.cachedFactor.constDefs[index9 + 1] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE:
            this.cachedFactor.defRate[1] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER:
            this.cachedFactor.defRate[2] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER:
            this.cachedFactor.defRate[3] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL:
            this.cachedFactor.defRate[4] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT:
            this.cachedFactor.defRate[5] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK:
            this.cachedFactor.defRate[6] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_FIRE:
            this.cachedFactor.constTols[0] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_WATER:
            this.cachedFactor.constTols[1] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_THUNDER:
            this.cachedFactor.constTols[2] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_SOIL:
            this.cachedFactor.constTols[3] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_LIGHT:
            this.cachedFactor.constTols[4] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_DARK:
            this.cachedFactor.constTols[5] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_ALLELEMENT:
            for (int index10 = 0; index10 < 6; ++index10)
              this.cachedFactor.constTols[index10] += skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_DOWN_FIRE:
            this.cachedFactor.constTols[0] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_DOWN_WATER:
            this.cachedFactor.constTols[1] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_DOWN_THUNDER:
            this.cachedFactor.constTols[2] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_DOWN_SOIL:
            this.cachedFactor.constTols[3] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_DOWN_LIGHT:
            this.cachedFactor.constTols[4] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_DOWN_DARK:
            this.cachedFactor.constTols[5] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLERANCE_DOWN_ALLELEMENT:
            for (int index11 = 0; index11 < 6; ++index11)
              this.cachedFactor.constTols[index11] -= skillSupport.value;
            continue;
          case BuffParam.BUFFTYPE.TOLUP_RATE_FIRE:
            this.cachedFactor.tolRate[0] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLUP_RATE_WATER:
            this.cachedFactor.tolRate[1] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLUP_RATE_THUNDER:
            this.cachedFactor.tolRate[2] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLUP_RATE_SOIL:
            this.cachedFactor.tolRate[3] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLUP_RATE_LIGHT:
            this.cachedFactor.tolRate[4] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLUP_RATE_DARK:
            this.cachedFactor.tolRate[5] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLUP_RATE_ALLELEMENT:
            for (int index12 = 0; index12 < 6; ++index12)
              this.cachedFactor.tolRate[index12] += (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLDOWN_RATE_FIRE:
            this.cachedFactor.tolRate[0] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLDOWN_RATE_WATER:
            this.cachedFactor.tolRate[1] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLDOWN_RATE_THUNDER:
            this.cachedFactor.tolRate[2] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLDOWN_RATE_SOIL:
            this.cachedFactor.tolRate[3] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLDOWN_RATE_LIGHT:
            this.cachedFactor.tolRate[4] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLDOWN_RATE_DARK:
            this.cachedFactor.tolRate[5] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.TOLDOWN_RATE_ALLELEMENT:
            for (int index13 = 0; index13 < 6; ++index13)
              this.cachedFactor.tolRate[index13] -= (float) skillSupport.value * 0.01f;
            continue;
          case BuffParam.BUFFTYPE.ATKUP_RATE_ALL:
            this.cachedFactor.atkRate[0] += (float) skillSupport.value * 0.01f;
            goto case BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT;
          default:
            continue;
        }
      }
    }
  }

  private enum eDirtyState
  {
    Dirty,
    CalcFactor,
    CalcFinal,
  }
}
