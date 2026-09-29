// Decompiled with JetBrains decompiler
// Type: SmithGrowSkillSelectMaterialItemNum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class SmithGrowSkillSelectMaterialItemNum : GameSection
{
  protected SortCompareData m_data;
  private int m_nowEmpty;
  private int m_maxNum;
  private int m_nowSelect;

  public override bool useOnPressBackKey => true;

  public override void OnPressBackKey() => this.DispatchEvent("[BACK]");

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.m_data = eventData[0] as SortCompareData;
    this.m_nowEmpty = (int) eventData[1];
    this.m_nowSelect = (int) eventData[2];
    this.m_maxNum = this.m_nowEmpty + this.m_nowSelect < this.m_data.GetNum() ? this.m_nowEmpty + this.m_nowSelect : this.m_data.GetNum();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    string key = "TEXT_SELECT";
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterialItemNum.UI.LBL_CAPTION, this.sectionData.GetText(key));
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterialItemNum.UI.STR_TITLE_U, this.sectionData.GetText(key));
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterialItemNum.UI.STR_TITLE_D, this.sectionData.GetText(key));
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterialItemNum.UI.STR_SELECT_NUM, this.sectionData.GetText("TEXT_SELECT_NUM"));
    this.SetProgressInt((Enum) SmithGrowSkillSelectMaterialItemNum.UI.SLD_SELECT_NUM, this.m_nowSelect, 0, this.m_maxNum, new EventDelegate.Callback(this.OnChagenSlider));
  }

  private void OnChagenSlider()
  {
    this.SetLabelText((Enum) SmithGrowSkillSelectMaterialItemNum.UI.LBL_SELECT_NUM, string.Format("{0,8:#,0}", (object) this.GetProgressInt((Enum) SmithGrowSkillSelectMaterialItemNum.UI.SLD_SELECT_NUM)));
  }

  private void OnQuery_SELECT_NUM_MINUS()
  {
    this.SetProgressInt((Enum) SmithGrowSkillSelectMaterialItemNum.UI.SLD_SELECT_NUM, this.GetProgressInt((Enum) SmithGrowSkillSelectMaterialItemNum.UI.SLD_SELECT_NUM) - 1);
  }

  private void OnQuery_SELECT_NUM_PLUS()
  {
    this.SetProgressInt((Enum) SmithGrowSkillSelectMaterialItemNum.UI.SLD_SELECT_NUM, this.GetProgressInt((Enum) SmithGrowSkillSelectMaterialItemNum.UI.SLD_SELECT_NUM) + 1);
  }

  protected int GetSliderNum()
  {
    return this.GetProgressInt((Enum) SmithGrowSkillSelectMaterialItemNum.UI.SLD_SELECT_NUM);
  }

  private void OnQuery_SELECT()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.m_data,
      (object) this.GetSliderNum()
    });
    GameSection.BackSection();
  }

  protected enum UI
  {
    LBL_SELECT_NUM,
    LBL_SELECT_PRICE,
    BTN_SELECT_NUM_MINUS,
    BTN_SELECT_NUM_PLUS,
    SLD_SELECT_NUM,
    SPR_SELECT_FRAME,
    STR_TITLE_U,
    STR_TITLE_D,
    STR_SELECT_NUM,
    LBL_CAPTION,
  }
}
