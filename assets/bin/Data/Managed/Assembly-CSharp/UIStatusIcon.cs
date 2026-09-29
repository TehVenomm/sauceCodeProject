// Decompiled with JetBrains decompiler
// Type: UIStatusIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class UIStatusIcon
{
  public static readonly UIStatusIcon.STATUS_TYPE[] NON_BUFF_STATUS = new UIStatusIcon.STATUS_TYPE[6]
  {
    UIStatusIcon.STATUS_TYPE.PARALYZE,
    UIStatusIcon.STATUS_TYPE.FREEZE,
    UIStatusIcon.STATUS_TYPE.SHADOWSEALING,
    UIStatusIcon.STATUS_TYPE.LIGHT_RING,
    UIStatusIcon.STATUS_TYPE.CONCUSSION,
    UIStatusIcon.STATUS_TYPE.DAMAGE_MOTION_STOP
  };
  private readonly Color defaultTintColor = Color.white;
  private readonly Color fieldBuffTintColor = Color32.op_Implicit(new Color32((byte) 159, (byte) 104, (byte) 104, byte.MaxValue));
  [SerializeField]
  protected UIStatusIcon.IconInfo[] statusIcons;
  [SerializeField]
  protected string[] spriteNames = new string[88];
  public Character target;

  public void UpDateStatusIcon()
  {
    if (Object.op_Equality((Object) this.target, (Object) null))
      return;
    int length = this.statusIcons.Length;
    int index1 = 0;
    int type1 = 0;
    while (type1 < 88 && (!this.CheckStatus((UIStatusIcon.STATUS_TYPE) type1, true) || !this._SetStatusIcon((UIStatusIcon.STATUS_TYPE) type1, index1, true) || ++index1 < length))
      ++type1;
    int type2 = 0;
    while (type2 < 88 && (!this.CheckStatus((UIStatusIcon.STATUS_TYPE) type2) || !this._SetStatusIcon((UIStatusIcon.STATUS_TYPE) type2, index1, false) || ++index1 < length))
      ++type2;
    for (int index2 = index1; index2 < length; ++index2)
    {
      this.statusIcons[index2].isFieldBuff = false;
      this.statusIcons[index2].SetTweenEnable(false);
      this.statusIcons[index2].icon.color = this.defaultTintColor;
      ((Component) this.statusIcons[index2].icon).gameObject.SetActive(false);
    }
  }

  private bool _SetStatusIcon(UIStatusIcon.STATUS_TYPE type, int index, bool isFieldBuff)
  {
    string nameByStatusType = this.GetIconSpriteNameByStatusType(type);
    if (this.statusIcons[index].icon.atlas.GetSprite(nameByStatusType) == null)
      return false;
    this.statusIcons[index].icon.spriteName = nameByStatusType;
    this.statusIcons[index].icon.color = this.defaultTintColor;
    this.statusIcons[index].SetTweenEnable(isFieldBuff);
    ((Component) this.statusIcons[index].icon).gameObject.SetActive(true);
    return true;
  }

  public int RotatedUpdateStatusIcon(int checkFirstStatus, BuffParam buffParam, List<int> nonBuff)
  {
    int num = checkFirstStatus;
    int length = this.statusIcons.Length;
    int index1 = 0;
    for (int index2 = 0; index2 < 88 && index1 < length; ++index2)
    {
      int type = index2 + checkFirstStatus;
      if (type >= 88)
        type %= 88;
      if (UIStatusIcon.CheckStatus((UIStatusIcon.STATUS_TYPE) type, buffParam, nonBuff))
      {
        UIStatusIcon.IconInfo statusIcon = this.statusIcons[index1];
        statusIcon.icon.spriteName = this.GetIconSpriteNameByStatusType((UIStatusIcon.STATUS_TYPE) type);
        ((Component) statusIcon.icon).gameObject.SetActive(true);
        ++index1;
        num = type;
      }
    }
    for (int index3 = index1; index3 < length; ++index3)
      ((Component) this.statusIcons[index3].icon).gameObject.SetActive(false);
    return num;
  }

  public bool HasActiveMultipleBuffIcon(BuffParam buffParam, List<int> nonBuff)
  {
    HashSet<string> stringSet = new HashSet<string>();
    int num = 0;
    for (int type = 0; type < 88; ++type)
    {
      if (UIStatusIcon.CheckStatus((UIStatusIcon.STATUS_TYPE) type, buffParam, nonBuff))
      {
        string nameByStatusType = this.GetIconSpriteNameByStatusType((UIStatusIcon.STATUS_TYPE) type);
        if (!string.IsNullOrEmpty(nameByStatusType) && !stringSet.Contains(nameByStatusType))
        {
          stringSet.Add(nameByStatusType);
          ++num;
          if (num > 1)
            return true;
        }
      }
    }
    return false;
  }

  private bool CheckStatus(UIStatusIcon.STATUS_TYPE type, bool isFieldBuff = false)
  {
    return UIStatusIcon.CheckStatus(type, this.target, isFieldBuff);
  }

  public static bool CheckStatus(
    UIStatusIcon.STATUS_TYPE type,
    Character character,
    bool isFieldBuff)
  {
    switch (type)
    {
      case UIStatusIcon.STATUS_TYPE.PARALYZE:
        return character.IsParalyze() && !isFieldBuff;
      case UIStatusIcon.STATUS_TYPE.FREEZE:
        return character.IsFreeze() && !isFieldBuff;
      case UIStatusIcon.STATUS_TYPE.SHADOWSEALING:
        return character.IsDebuffShadowSealing() && !isFieldBuff;
      case UIStatusIcon.STATUS_TYPE.LIGHT_RING:
        return character.IsLightRing() && !isFieldBuff;
      case UIStatusIcon.STATUS_TYPE.CONCUSSION:
        return character.IsConcussion() && !isFieldBuff;
      default:
        return UIStatusIcon.CheckStatus(type, character.buffParam, isFieldBuff);
    }
  }

  public static bool CheckStatus(
    UIStatusIcon.STATUS_TYPE type,
    BuffParam buffParam,
    List<int> nonBuffStatus)
  {
    if (UIStatusIcon.CheckStatus(type, buffParam))
      return true;
    return nonBuffStatus != null && UIStatusIcon.CheckStatus(type, nonBuffStatus);
  }

  private static bool CheckStatus(UIStatusIcon.STATUS_TYPE type, List<int> nonBuffStatus)
  {
    return nonBuffStatus.Contains((int) type);
  }

  private static bool CheckStatus(
    UIStatusIcon.STATUS_TYPE type,
    BuffParam buffParam,
    bool isFieldBuff = false)
  {
    switch (type)
    {
      case UIStatusIcon.STATUS_TYPE.POISON:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.POISON, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.BURNING:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BURNING, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.MOVE_SPEED_DOWN:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEADLY_POISON:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEADLY_POISON, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLECOUNT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLECOUNT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_SPEED_UP:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_SPEED_UP, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.MOVE_SPEED_UP:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.MOVE_SPEED_UP, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_NORMAL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_NORMAL, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_NORMAL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_FIRE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_FIRE, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_FIRE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_WATER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_WATER, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_WATER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_THUNDER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_THUNDER, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_THUNDER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_SOIL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_SOIL, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_SOIL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_LIGHT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_LIGHT, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_LIGHT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_DARK:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_DARK, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_DARK, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_ALLELEMENT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEFENCE_NORMAL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_NORMAL, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_NORMAL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEFENCE_FIRE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_FIRE, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_FIRE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEFENCE_WATER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_WATER, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_WATER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEFENCE_THUNDER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_THUNDER, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_THUNDER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEFENCE_SOIL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_SOIL, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_SOIL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEFENCE_LIGHT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_LIGHT, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_LIGHT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEFENCE_DARK:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_DARK, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_DARK, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEFENCE_ALLELEMENT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_ALLELEMENT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.REGENERATE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.REGENERATE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ELECTRIC_SHOCK:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ELECTRIC_SHOCK, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INK_SPLASH:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INK_SPLASH, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.POISON_DAMAGE_DOWN:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.POISON_DAMAGE_DOWN, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.POISON_GUARD:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.POISON_GUARD, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.BURNING_DAMAGE_DOWN:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BURN_DAMAGE_DOWN, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.BURNING_GUARD:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BURN_GUARD, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.SUPER_ARMOR:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SUPER_ARMOR, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEFENCE_DOWN:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.SHIELD:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SHIELD, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.SLIDE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SLIDE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.PARALYZE_GUARD:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.PARALYZE_GUARD, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.SILENCE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SILENCE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.REGENERATE_PROPORTION:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.REGENERATE_PROPORTION, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ABSORB_NORMAL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_NORMAL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ABSORB_FIRE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_FIRE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ABSORB_WATER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_WATER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ABSORB_THUNDER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_THUNDER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ABSORB_SOIL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_SOIL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ABSORB_LIGHT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_LIGHT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ABSORB_DARK:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_DARK, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ABSORB_ALL_ELEMENT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_ALL_ELEMENT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_FIRE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_WATER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_THUNDER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_SOIL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_LIGHT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_DARK:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_ALLELEMENT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_SPEED_DOWN:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.AUTO_REVIVE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.AUTO_REVIVE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.WARP_BY_AVOID:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.WARP_BY_AVOID, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_FIRE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_FIRE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_WATER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_WATER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_THUNDER:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_THUNDER, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_SOIL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_SOIL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DAMAGE_UP_NORMAL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DAMAGE_UP_NORMAL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.DAMAGE_UP_FROM_AVOID:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DAMAGE_UP_FROM_AVOID, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.SKILL_HEAL_SPEEDUP:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SKILL_HEAL_SPEEDUP, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.GAUGE_INCREASE_UP:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.HEAT_GAUGE_INCREASE_UP, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SOUL_GAUGE_INCREASE_UP, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.SKILL_CHARGE_WHEN_DAMAGED:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_LIGHT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_LIGHT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_DARK:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_DARK, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.CANT_HEAL_HP:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.BLIND:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BLIND, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_BADSTATUS:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.SLIDE_ICE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.EROSION:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.EROSION, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.STONE:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.STONE, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.SOIL_SHOCK:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SOIL_SHOCK, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ATTACK_ALL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_ALL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.BLEEDING:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BLEEDING, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_NORMAL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_NORMAL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_ALLELEMENT:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_ALL_ELEMENT, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_ALL:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_ALL, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.ACID:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ACID, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.CORRUPTION:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.CORRUPTION, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.STIGMATA:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.STIGMATA, isFieldBuff);
      case UIStatusIcon.STATUS_TYPE.CYCLONIC_THUNDERSTORM:
        return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM, isFieldBuff);
      default:
        return false;
    }
  }

  private string GetIconSpriteNameByStatusType(UIStatusIcon.STATUS_TYPE statusType)
  {
    switch (statusType)
    {
      case UIStatusIcon.STATUS_TYPE.PARALYZE:
        return "Pala";
      case UIStatusIcon.STATUS_TYPE.POISON:
        return "Poizon";
      case UIStatusIcon.STATUS_TYPE.BURNING:
        return "Burn";
      case UIStatusIcon.STATUS_TYPE.MOVE_SPEED_DOWN:
        return "SpeedDown";
      case UIStatusIcon.STATUS_TYPE.DEADLY_POISON:
        return "DeadlyPoison";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLECOUNT:
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_BADSTATUS:
        return string.Empty;
      case UIStatusIcon.STATUS_TYPE.ATTACK_SPEED_UP:
        return "AtkSpeedUp";
      case UIStatusIcon.STATUS_TYPE.MOVE_SPEED_UP:
        return "SpeedUp";
      case UIStatusIcon.STATUS_TYPE.ATTACK_NORMAL:
      case UIStatusIcon.STATUS_TYPE.ATTACK_FIRE:
      case UIStatusIcon.STATUS_TYPE.ATTACK_WATER:
      case UIStatusIcon.STATUS_TYPE.ATTACK_THUNDER:
      case UIStatusIcon.STATUS_TYPE.ATTACK_SOIL:
      case UIStatusIcon.STATUS_TYPE.ATTACK_LIGHT:
      case UIStatusIcon.STATUS_TYPE.ATTACK_DARK:
      case UIStatusIcon.STATUS_TYPE.ATTACK_ALLELEMENT:
      case UIStatusIcon.STATUS_TYPE.DAMAGE_UP_NORMAL:
      case UIStatusIcon.STATUS_TYPE.DAMAGE_UP_FROM_AVOID:
      case UIStatusIcon.STATUS_TYPE.ATTACK_ALL:
        return "AtkUp";
      case UIStatusIcon.STATUS_TYPE.DEFENCE_NORMAL:
      case UIStatusIcon.STATUS_TYPE.DEFENCE_FIRE:
      case UIStatusIcon.STATUS_TYPE.DEFENCE_WATER:
      case UIStatusIcon.STATUS_TYPE.DEFENCE_THUNDER:
      case UIStatusIcon.STATUS_TYPE.DEFENCE_SOIL:
      case UIStatusIcon.STATUS_TYPE.DEFENCE_LIGHT:
      case UIStatusIcon.STATUS_TYPE.DEFENCE_DARK:
      case UIStatusIcon.STATUS_TYPE.DEFENCE_ALLELEMENT:
        return "DefUp";
      case UIStatusIcon.STATUS_TYPE.REGENERATE:
      case UIStatusIcon.STATUS_TYPE.REGENERATE_PROPORTION:
        return "Recovery";
      case UIStatusIcon.STATUS_TYPE.FREEZE:
        return "Frozen";
      case UIStatusIcon.STATUS_TYPE.ELECTRIC_SHOCK:
        return "ElectricShock";
      case UIStatusIcon.STATUS_TYPE.INK_SPLASH:
        return "InkSplash";
      case UIStatusIcon.STATUS_TYPE.POISON_DAMAGE_DOWN:
        return "PoisonDamageDown";
      case UIStatusIcon.STATUS_TYPE.POISON_GUARD:
        return "PoisonGuard";
      case UIStatusIcon.STATUS_TYPE.BURNING_DAMAGE_DOWN:
        return "BurningDamageDown";
      case UIStatusIcon.STATUS_TYPE.BURNING_GUARD:
        return "BurningGuard";
      case UIStatusIcon.STATUS_TYPE.SUPER_ARMOR:
        return "SuperArmor";
      case UIStatusIcon.STATUS_TYPE.DEFENCE_DOWN:
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_ALLELEMENT:
        return "DefDown";
      case UIStatusIcon.STATUS_TYPE.SHIELD:
        return "Shield";
      case UIStatusIcon.STATUS_TYPE.SLIDE:
        return "Slide";
      case UIStatusIcon.STATUS_TYPE.PARALYZE_GUARD:
        return "ParalyzeGuard";
      case UIStatusIcon.STATUS_TYPE.SILENCE:
        return "Silence";
      case UIStatusIcon.STATUS_TYPE.SHADOWSEALING:
        return "ShadowSealing";
      case UIStatusIcon.STATUS_TYPE.ABSORB_FIRE:
        return "AbsorbFire";
      case UIStatusIcon.STATUS_TYPE.ABSORB_WATER:
        return "AbsorbWater";
      case UIStatusIcon.STATUS_TYPE.ABSORB_THUNDER:
        return "AbsorbThunder";
      case UIStatusIcon.STATUS_TYPE.ABSORB_SOIL:
        return "AbsorbSoil";
      case UIStatusIcon.STATUS_TYPE.ABSORB_LIGHT:
        return "AbsorbLight";
      case UIStatusIcon.STATUS_TYPE.ABSORB_DARK:
        return "AbsorbDark";
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_FIRE:
        return "DefDownFire";
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_WATER:
        return "DefDownWater";
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_THUNDER:
        return "DefDownThunder";
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_SOIL:
        return "DefDownSoil";
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_LIGHT:
        return "DefDownLight";
      case UIStatusIcon.STATUS_TYPE.DEF_DOWN_DARK:
        return "DefDownDark";
      case UIStatusIcon.STATUS_TYPE.ATTACK_SPEED_DOWN:
        return "AtkSpeedDown";
      case UIStatusIcon.STATUS_TYPE.AUTO_REVIVE:
        return "AutoRevive";
      case UIStatusIcon.STATUS_TYPE.WARP_BY_AVOID:
        return "Warp";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_FIRE:
        return "dmgOffFire";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_WATER:
        return "dmgOffWater";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_THUNDER:
        return "dmgOffThunder";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_SOIL:
        return "dmgOffSoil";
      case UIStatusIcon.STATUS_TYPE.SKILL_HEAL_SPEEDUP:
      case UIStatusIcon.STATUS_TYPE.GAUGE_INCREASE_UP:
      case UIStatusIcon.STATUS_TYPE.SKILL_CHARGE_WHEN_DAMAGED:
        return "GaugeIncreaseUp";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_LIGHT:
        return "dmgOffLight";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_DARK:
        return "dmgOffDark";
      case UIStatusIcon.STATUS_TYPE.CANT_HEAL_HP:
        return "CantHealHp";
      case UIStatusIcon.STATUS_TYPE.BLIND:
        return "Blind";
      case UIStatusIcon.STATUS_TYPE.SLIDE_ICE:
        return "Slide";
      case UIStatusIcon.STATUS_TYPE.LIGHT_RING:
        return "LightRing";
      case UIStatusIcon.STATUS_TYPE.EROSION:
        return "Erosion";
      case UIStatusIcon.STATUS_TYPE.STONE:
        return "Stone";
      case UIStatusIcon.STATUS_TYPE.SOIL_SHOCK:
        return "SoilShock";
      case UIStatusIcon.STATUS_TYPE.CONCUSSION:
        return "Enemy_Stan";
      case UIStatusIcon.STATUS_TYPE.BLEEDING:
        return "Bleeding";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_NORMAL:
        return "dmgOffNone";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_ALLELEMENT:
        return "dmgOffElementall";
      case UIStatusIcon.STATUS_TYPE.INVINCIBLE_ALL:
        return "dmgOffAll";
      case UIStatusIcon.STATUS_TYPE.ACID:
        return "Acid";
      case UIStatusIcon.STATUS_TYPE.CORRUPTION:
        return "Corruption";
      case UIStatusIcon.STATUS_TYPE.STIGMATA:
        return "Stigmata";
      case UIStatusIcon.STATUS_TYPE.CYCLONIC_THUNDERSTORM:
        return "CyclonicThunderstorm";
      default:
        return string.Empty;
    }
  }

  public enum STATUS_TYPE
  {
    PARALYZE,
    POISON,
    BURNING,
    MOVE_SPEED_DOWN,
    DEADLY_POISON,
    INVINCIBLECOUNT,
    ATTACK_SPEED_UP,
    MOVE_SPEED_UP,
    ATTACK_NORMAL,
    ATTACK_FIRE,
    ATTACK_WATER,
    ATTACK_THUNDER,
    ATTACK_SOIL,
    ATTACK_LIGHT,
    ATTACK_DARK,
    ATTACK_ALLELEMENT,
    DEFENCE_NORMAL,
    DEFENCE_FIRE,
    DEFENCE_WATER,
    DEFENCE_THUNDER,
    DEFENCE_SOIL,
    DEFENCE_LIGHT,
    DEFENCE_DARK,
    DEFENCE_ALLELEMENT,
    REGENERATE,
    FREEZE,
    ELECTRIC_SHOCK,
    INK_SPLASH,
    POISON_DAMAGE_DOWN,
    POISON_GUARD,
    BURNING_DAMAGE_DOWN,
    BURNING_GUARD,
    SUPER_ARMOR,
    DEFENCE_DOWN,
    SHIELD,
    SLIDE,
    PARALYZE_GUARD,
    SILENCE,
    SHADOWSEALING,
    REGENERATE_PROPORTION,
    ABSORB_NORMAL,
    ABSORB_FIRE,
    ABSORB_WATER,
    ABSORB_THUNDER,
    ABSORB_SOIL,
    ABSORB_LIGHT,
    ABSORB_DARK,
    ABSORB_ALL_ELEMENT,
    DEF_DOWN_FIRE,
    DEF_DOWN_WATER,
    DEF_DOWN_THUNDER,
    DEF_DOWN_SOIL,
    DEF_DOWN_LIGHT,
    DEF_DOWN_DARK,
    DEF_DOWN_ALLELEMENT,
    ATTACK_SPEED_DOWN,
    AUTO_REVIVE,
    WARP_BY_AVOID,
    INVINCIBLE_FIRE,
    INVINCIBLE_WATER,
    INVINCIBLE_THUNDER,
    INVINCIBLE_SOIL,
    DAMAGE_UP_NORMAL,
    DAMAGE_UP_FROM_AVOID,
    SKILL_HEAL_SPEEDUP,
    GAUGE_INCREASE_UP,
    SKILL_CHARGE_WHEN_DAMAGED,
    INVINCIBLE_LIGHT,
    INVINCIBLE_DARK,
    CANT_HEAL_HP,
    BLIND,
    INVINCIBLE_BADSTATUS,
    SLIDE_ICE,
    LIGHT_RING,
    EROSION,
    STONE,
    SOIL_SHOCK,
    CONCUSSION,
    ATTACK_ALL,
    BLEEDING,
    INVINCIBLE_NORMAL,
    INVINCIBLE_ALLELEMENT,
    INVINCIBLE_ALL,
    ACID,
    DAMAGE_MOTION_STOP,
    CORRUPTION,
    STIGMATA,
    CYCLONIC_THUNDERSTORM,
    MAX,
  }

  [Serializable]
  public class IconInfo
  {
    public UISprite icon;
    [SerializeField]
    private TweenColor tween;
    [HideInInspector]
    public bool isFieldBuff;

    public void SetTweenEnable(bool enable)
    {
      if (this.tween == null)
        return;
      ((Behaviour) this.tween).enabled = enable;
    }
  }
}
