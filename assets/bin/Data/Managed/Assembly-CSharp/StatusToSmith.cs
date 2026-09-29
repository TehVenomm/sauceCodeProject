// Decompiled with JetBrains decompiler
// Type: StatusToSmith
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class StatusToSmith : GameSection
{
  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_SMITH_BADGE;
  }

  public override void Initialize()
  {
    if (!TutorialStep.HasAllTutorialCompleted() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.npcMessage, (Object) null))
      MonoBehaviourSingleton<UIManager>.I.npcMessage.HideMessage();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetBadge((Enum) StatusToSmith.UI.BTN_CREATE_WEAPON, MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetAllWeaponBadgeNum(), (SpriteAlignment) 1, 7, -9, true);
    this.SetBadge((Enum) StatusToSmith.UI.BTN_CREATE_DEFENSE, MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetAllDefenseBadgeNum(), (SpriteAlignment) 1, 7, -9, true);
    base.UpdateUI();
  }

  protected override void OnOpen()
  {
    MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.NONE);
    base.OnOpen();
  }

  private void OnQuery_CREATE_WEAPON()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
    {
      if (MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ARROW) > 0)
        GameSection.ChangeEvent("CREATE", (object) EQUIPMENT_TYPE.ARROW);
      else if (MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.TWO_HAND_SWORD) > 0)
        GameSection.ChangeEvent("CREATE", (object) EQUIPMENT_TYPE.TWO_HAND_SWORD);
      else if (MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.SPEAR) > 0)
        GameSection.ChangeEvent("CREATE", (object) EQUIPMENT_TYPE.SPEAR);
      else if (MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.PAIR_SWORDS) > 0)
        GameSection.ChangeEvent("CREATE", (object) EQUIPMENT_TYPE.PAIR_SWORDS);
      else
        GameSection.ChangeEvent("CREATE", (object) EQUIPMENT_TYPE.ONE_HAND_SWORD);
    }
    else
      GameSection.ChangeEvent("CREATE", (object) EQUIPMENT_TYPE.ONE_HAND_SWORD);
  }

  private void OnQuery_CREATE_DEFENSE()
  {
    GameSection.ChangeEvent("CREATE", (object) EQUIPMENT_TYPE.HELM);
  }

  private void OnQuery_GROW_WEAPON()
  {
    GameSection.ChangeEvent("GROW", (object) EQUIPMENT_TYPE.ONE_HAND_SWORD);
    this.ToGrow();
  }

  private void OnQuery_GROW_DEFENSE()
  {
    GameSection.ChangeEvent("GROW", (object) EQUIPMENT_TYPE.HELM);
    this.ToGrow();
  }

  private void ToGrow()
  {
    equipmentType = EQUIPMENT_TYPE.ONE_HAND_SWORD;
    if (!(GameSection.GetEventData() is EQUIPMENT_TYPE equipmentType))
      ;
    GameSection.SetEventData((object) new object[2]
    {
      (object) SmithEquipBase.SmithType.GROW,
      (object) equipmentType
    });
  }

  private void OnQuery_SKILL_GROW()
  {
    if (MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetCount() != 0)
      return;
    GameSection.ChangeEvent("NOT_HAVE_SKILL_ITEM");
  }

  private void OnQuery_EXCHANGE()
  {
    Transform ctrl = this.GetCtrl((Enum) StatusToSmith.UI.BTN_EXCHANGE);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    int num = ((Component) ctrl).gameObject.activeSelf ? 1 : 0;
  }

  private enum UI
  {
    BTN_EXCHANGE,
    BTN_CREATE_WEAPON,
    BTN_CREATE_DEFENSE,
  }
}
