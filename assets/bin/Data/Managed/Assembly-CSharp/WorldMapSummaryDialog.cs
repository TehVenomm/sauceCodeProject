// Decompiled with JetBrains decompiler
// Type: WorldMapSummaryDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class WorldMapSummaryDialog : GameSection
{
  private int maxPage;
  private int currentPage;
  private string[] splitedSummary;

  public override void Initialize()
  {
    int id = (int) GameSection.GetEventData();
    for (int index = id - 1; index < 1; --index)
    {
      RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint) id);
      if (data.difficulty == REGION_DIFFICULTY_TYPE.NORMAL)
      {
        id = (int) data.regionId;
        break;
      }
    }
    this.splitedSummary = StringTable.Get(STRING_CATEGORY.SUMMARY, (uint) id).Split('@');
    this.currentPage = 1;
    this.maxPage = this.splitedSummary.Length;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.UpdatePageUI();
    this.UpdateSummary();
  }

  private void UpdateSummary()
  {
    this.SetLabelText((Enum) WorldMapSummaryDialog.UI.LBL_SUMMARY, this.splitedSummary[this.currentPage - 1].Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
  }

  private void UpdatePageUI()
  {
    this.SetLabelText((Enum) WorldMapSummaryDialog.UI.LBL_NOW, this.currentPage.ToString());
    this.SetLabelText((Enum) WorldMapSummaryDialog.UI.LBL_MAX, this.maxPage.ToString());
    this.UpdatePageArrows();
  }

  private void UpdatePageArrows()
  {
    if (this.currentPage == 1)
    {
      bool is_visible = this.currentPage < this.maxPage;
      this.SetActive((Enum) WorldMapSummaryDialog.UI.OBJ_ACTIVE_ARROW_L, false);
      this.SetActive((Enum) WorldMapSummaryDialog.UI.OBJ_ACTIVE_ARROW_R, is_visible);
      this.SetActive((Enum) WorldMapSummaryDialog.UI.SPR_INACTIVE_ARROW_L, true);
      this.SetActive((Enum) WorldMapSummaryDialog.UI.SPR_INACTIVE_ARROW_R, !is_visible);
    }
    else if (this.currentPage >= this.maxPage)
    {
      this.SetActive((Enum) WorldMapSummaryDialog.UI.OBJ_ACTIVE_ARROW_L, true);
      this.SetActive((Enum) WorldMapSummaryDialog.UI.OBJ_ACTIVE_ARROW_R, false);
      this.SetActive((Enum) WorldMapSummaryDialog.UI.SPR_INACTIVE_ARROW_L, false);
      this.SetActive((Enum) WorldMapSummaryDialog.UI.SPR_INACTIVE_ARROW_R, true);
    }
    else
    {
      this.SetActive((Enum) WorldMapSummaryDialog.UI.OBJ_ACTIVE_ARROW_L, true);
      this.SetActive((Enum) WorldMapSummaryDialog.UI.OBJ_ACTIVE_ARROW_R, true);
      this.SetActive((Enum) WorldMapSummaryDialog.UI.SPR_INACTIVE_ARROW_L, false);
      this.SetActive((Enum) WorldMapSummaryDialog.UI.SPR_INACTIVE_ARROW_R, false);
    }
  }

  private void OnQuery_PAGE_NEXT()
  {
    ++this.currentPage;
    this.RefreshUI();
  }

  private void OnQuery_PAGE_PREV()
  {
    --this.currentPage;
    this.RefreshUI();
  }

  protected enum UI
  {
    LBL_SUMMARY,
    LBL_NOW,
    LBL_MAX,
    OBJ_ACTIVE_ARROW_R,
    OBJ_ACTIVE_ARROW_L,
    SPR_INACTIVE_ARROW_R,
    SPR_INACTIVE_ARROW_L,
  }
}
