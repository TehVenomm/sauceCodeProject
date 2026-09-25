// Decompiled with JetBrains decompiler
// Type: ShopItemSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class ShopItemSelect : GameSection
{
  protected object[] selectEventData;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetTable((Enum) ShopItemSelect.UI.TBL_LIST, "ShopItemListItem", MonoBehaviourSingleton<ShopManager>.I.shopData.lineups.Count, false, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      ShopList.ShopLineup lineup = MonoBehaviourSingleton<ShopManager>.I.shopData.lineups[i];
      this.SetLabelText(t, (Enum) ShopItemSelect.UI.LBL_NAME, lineup.name);
      this.SetLabelText(t, (Enum) ShopItemSelect.UI.LBL_DESCRIPTION, lineup.description);
      this.SetLabelText(t, (Enum) ShopItemSelect.UI.LBL_CRYSTAL_NUM, lineup.crystalNum.ToString());
      this.SetEvent(t, "SELECT", lineup.shopLineupId);
      ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.ITEM, (uint) lineup.itemIds[0], this.FindCtrl(t, (Enum) ShopItemSelect.UI.OBJ_ICON_ROOT));
      if (!Object.op_Inequality((Object) rewardItemIcon, (Object) null))
        return;
      rewardItemIcon.SetEnableCollider(false);
    }));
  }

  private void OnQuery_SELECT()
  {
    int eventData = (int) GameSection.GetEventData();
    ShopList.ShopLineup lineup = MonoBehaviourSingleton<ShopManager>.I.GetLineup(eventData);
    if (lineup == null)
    {
      Log.Error(LOG.OUTGAME, $"lineup_id={(object) eventData} is not found.");
      GameSection.StopEvent();
    }
    else
    {
      this.selectEventData = new object[7]
      {
        (object) eventData,
        (object) lineup,
        (object) lineup.name,
        (object) lineup.description,
        (object) lineup.crystalNum,
        (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal,
        (object) (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal - lineup.crystalNum)
      };
      GameSection.SetEventData((object) this.selectEventData);
    }
  }

  protected void OnQuery_ShopItemConfirm_YES()
  {
    GameSection.SetEventData((object) this.selectEventData);
    GameSection.StayEvent();
    MonoBehaviourSingleton<ShopManager>.I.SendBuy((int) this.selectEventData[0], (Action<Error>) (error =>
    {
      if (error != Error.None)
      {
        if (error == Error.ERR_CRYSTAL_NOT_ENOUGH)
        {
          GameSection.ChangeStayEvent("NOT_ENOUGTH");
          GameSection.ResumeEvent(true);
        }
        else
          GameSection.ResumeEvent(false);
      }
      else
      {
        this.selectEventData[6] = (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
        GameSection.ResumeEvent(true);
      }
    }));
  }

  protected enum UI
  {
    OBJ_FRAME,
    SCR_LIST,
    TBL_LIST,
    LBL_NAME,
    LBL_DESCRIPTION,
    LBL_CRYSTAL_NUM,
    OBJ_ICON_ROOT,
  }
}
