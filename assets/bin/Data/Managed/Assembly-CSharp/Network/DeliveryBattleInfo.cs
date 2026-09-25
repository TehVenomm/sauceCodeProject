// Decompiled with JetBrains decompiler
// Type: Network.DeliveryBattleInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class DeliveryBattleInfo
{
  public int maxDamageSelf;
  public int totalAttackCount;
  public int attackCount;
  public List<DeliveryBattleInfo.SkillCount> totalSkillCountList = new List<DeliveryBattleInfo.SkillCount>();
  public List<DeliveryBattleInfo.SkillCount> mySkillCountList = new List<DeliveryBattleInfo.SkillCount>();
  public List<DeliveryBattleInfo.DamageByWeapon> damageByWeaponList = new List<DeliveryBattleInfo.DamageByWeapon>();
  [NonSerialized]
  public List<DeliveryBattleInfo.DamageByWeapon> currentDamageByWeaponList = new List<DeliveryBattleInfo.DamageByWeapon>();
  public List<DeliveryBattleInfo.PlayerActionInfo> playerActionInfoList = new List<DeliveryBattleInfo.PlayerActionInfo>();

  [Serializable]
  public class SkillCount
  {
    public int skillId;
    public int totalCount;

    public SkillCount(int skillId, int count)
    {
      this.skillId = skillId;
      this.totalCount = count;
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
      this.actionType = (int) type;
      this.totalDamage = damage;
      this.totalCount = count;
    }
  }
}
