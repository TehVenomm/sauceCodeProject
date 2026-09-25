// Decompiled with JetBrains decompiler
// Type: QuestResultDropSellConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class QuestResultDropSellConfirm : ItemSellConfirm
{
  private int totalSell;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.sellData = eventData[0] as List<SortCompareData>;
    this.totalSell = (int) eventData[1];
    this.isRareConfirm = true;
    this.isHideMainText = true;
    this.isButtonSingle = true;
    base.Initialize();
  }

  private void OnQuery_OK() => GameSection.BackSection();

  protected override int GetSellGold() => this.totalSell;

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
  }
}
