// Decompiled with JetBrains decompiler
// Type: AbilityItemSellConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
public class AbilityItemSellConfirm : ItemSellConfirm
{
  private List<string> uniqs = new List<string>();

  protected override bool isShowIconBG() => false;

  public override string overrideBackKeyEvent => "NO";

  public override void Initialize()
  {
    this.sellData = (GameSection.GetEventData() as List<AbilityItemSortData>).Select<AbilityItemSortData, SortCompareData>((Func<AbilityItemSortData, SortCompareData>) (x => (SortCompareData) x)).ToList<SortCompareData>();
    this.isRareConfirm = false;
    int index = 0;
    for (int count = this.sellData.Count; index < count; ++index)
    {
      if (!this.isRareConfirm && this.sellData[index] != null && GameDefine.IsRequiredAlertByRarity(this.sellData[index].GetRarity()))
        this.isRareConfirm = true;
    }
    base.Initialize();
  }

  protected override void DrawIcon()
  {
    base.DrawIcon();
    this.SetActive((Enum) AbilityItemSellConfirm.UI.STR_NON_REWARD, true);
  }

  private void OnQuery_NO() => GameSection.SetEventData((object) this.sellData);

  private void OnQuery_YES()
  {
    GameSection.SetEventData((object) null);
    this.uniqs.Clear();
    this.sellData.ForEach((Action<SortCompareData>) (sort_data => this.uniqs.Add(sort_data.GetUniqID().ToString())));
    if (this.isRareConfirm || this.isEquipConfirm || this.isExceedConfirm || this.isExceedEquipmentConfirm)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(this.sectionData.GetText("TEXT_CONFIRM"));
      if (this.isRareConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_RARE"));
      if (this.isEquipConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_EQUIP"));
      if (this.isExceedConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_EXCEED"));
      if (this.isExceedEquipmentConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_EXCEED_EQUIP"));
      stringBuilder.AppendLine("");
      stringBuilder.Append(this.sectionData.GetText("TEXT_GROW"));
      GameSection.ChangeEvent("INCLUDE_RARE_CONFIRM", (object) stringBuilder.ToString());
    }
    else
    {
      GameSection.SetEventData((object) null);
      this.SendSell();
    }
  }

  private void SendSell()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellAbilityItem(this.uniqs, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  public void OnQuery_AbilityItemSellIncludeRareConfirm_YES()
  {
    GameSection.SetEventData((object) null);
    this.SendSell();
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
