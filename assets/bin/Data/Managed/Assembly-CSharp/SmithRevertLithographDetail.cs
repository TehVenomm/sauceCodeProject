// Decompiled with JetBrains decompiler
// Type: SmithRevertLithographDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithRevertLithographDetail : ItemSellConfirm
{
  private List<string> uniqs = new List<string>();
  private EquipItemSortData equipData;
  private ItemTable.ItemData[] lithographArr;

  protected override bool isShowIconBG() => false;

  public override string overrideBackKeyEvent => "NO";

  public override void Initialize()
  {
    this.equipData = GameSection.GetEventData() as EquipItemSortData;
    this.sellData = new List<SortCompareData>()
    {
      (SortCompareData) this.equipData
    };
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (this.sellData == null)
      return;
    this.DrawIcon();
    this.SetActive((Enum) SmithRevertLithographDetail.UI.BTN_CENTER, false);
    this.SetActive((Enum) SmithRevertLithographDetail.UI.BTN_0, true);
    this.SetActive((Enum) SmithRevertLithographDetail.UI.BTN_1, true);
  }

  protected override void DrawIcon()
  {
    base.DrawIcon();
    this.lithographArr = new ItemTable.ItemData[1]
    {
      Singleton<EquipItemTable>.I.GetEquipItemData(this.equipData.GetTableID()).GetRootLithograph()
    };
    this.SetGrid((Enum) SmithRevertLithographDetail.UI.GRD_REWARD_ICON, (string) null, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (i < this.lithographArr.Length)
      {
        ItemTable.ItemData itemData = this.lithographArr[i];
        ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.ITEM, itemData.id, t, 1, "NONE");
        rewardItemIcon.SetRewardBG(true);
        Transform ctrl = this.GetCtrl((Enum) SmithRevertLithographDetail.UI.GRD_REWARD_ICON);
        this.SetMaterialInfo(rewardItemIcon.transform, REWARD_TYPE.ITEM, itemData.id, ctrl);
      }
      else
        this.SetActive(t, false);
    }));
    this.SetActive((Enum) SmithRevertLithographDetail.UI.STR_NON_REWARD, this.lithographArr.Length == 0);
  }

  public void OnQuery_YES()
  {
    GameSection.SetEventData((object) string.Format(StringTable.Get(STRING_CATEGORY.SMITH, 12U), (object) MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SMITH_RESTORE_USE_CRYSTAL));
  }

  private void OnQuery_SmithRevertLithographConfirm_YES()
  {
    if (!GameSection.CheckCrystal(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SMITH_RESTORE_USE_CRYSTAL))
      return;
    GameSection.SetEventData((object) StringTable.Format(STRING_CATEGORY.SMITH, 13U, (object) this.lithographArr[0].name));
    GameSection.StayEvent();
    MonoBehaviourSingleton<SmithManager>.I.SendRevertLithograph(this.equipData.GetUniqID(), (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  public new enum UI
  {
    STR_INCLUDE_RARE,
    STR_MAIN_TEXT,
    STR_TITLE_R,
    GRD_ICON,
    LBL_TOTAL,
    OBJ_GOLD,
    BTN_0,
    BTN_1,
    BTN_CENTER,
    SCR_ICON,
    GRD_REWARD_ICON,
    STR_NON_REWARD,
  }
}
