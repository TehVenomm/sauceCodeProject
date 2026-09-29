// Decompiled with JetBrains decompiler
// Type: MissionCheckEqpip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class MissionCheckEqpip : MissionCheckBase
{
  private bool isClear;

  protected override void Initialize(MISSION_REQUIRE require, int param)
  {
    EquipItemInfo baseWeapon = (EquipItemInfo) null;
    if (QuestManager.IsValidInGameSeriesArena())
    {
      for (int order = 1; order <= MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum(); ++order)
      {
        EquipSetInfo orderUniqueEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetOrderUniqueEquipSet(order);
        if (orderUniqueEquipSet != null)
        {
          for (int index = 0; index < 3; ++index)
          {
            EquipItemInfo checkWeapon = orderUniqueEquipSet.item[index];
            if (checkWeapon != null)
            {
              if (baseWeapon == null)
                baseWeapon = checkWeapon;
              if (this.Check(require, checkWeapon, baseWeapon))
                return;
            }
          }
        }
      }
    }
    else
    {
      for (int equip_slot = 0; equip_slot < 3; ++equip_slot)
      {
        EquipItemInfo equipmentWeaponInfo = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentWeaponInfo(equip_slot);
        if (equipmentWeaponInfo != null)
        {
          if (baseWeapon == null)
            baseWeapon = equipmentWeaponInfo;
          if (this.Check(require, equipmentWeaponInfo, baseWeapon))
            return;
        }
      }
    }
    if (require == MISSION_REQUIRE.MULTI_EQUIP)
      return;
    this.isClear = true;
  }

  private bool Check(MISSION_REQUIRE require, EquipItemInfo checkWeapon, EquipItemInfo baseWeapon)
  {
    switch (require)
    {
      case MISSION_REQUIRE.ONLY_ONE_HAND_SWORD:
        return checkWeapon.tableData.type != 0;
      case MISSION_REQUIRE.ONLY_TWO_HAND_SWORD:
        return checkWeapon.tableData.type != EQUIPMENT_TYPE.TWO_HAND_SWORD;
      case MISSION_REQUIRE.ONLY_SPEAR:
        return checkWeapon.tableData.type != EQUIPMENT_TYPE.SPEAR;
      case MISSION_REQUIRE.ONLY_PAIR_SWORDS:
        return checkWeapon.tableData.type != EQUIPMENT_TYPE.PAIR_SWORDS;
      case MISSION_REQUIRE.ONLY_ARROW:
        return checkWeapon.tableData.type != EQUIPMENT_TYPE.ARROW;
      case MISSION_REQUIRE.MULTI_EQUIP:
        int num = checkWeapon.tableData.type != baseWeapon.tableData.type ? 1 : 0;
        if (num == 0)
          return num != 0;
        this.isClear = true;
        return num != 0;
      default:
        return false;
    }
  }

  public override bool IsMissionClear() => this.isClear;
}
