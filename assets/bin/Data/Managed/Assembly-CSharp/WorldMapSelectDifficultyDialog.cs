// Decompiled with JetBrains decompiler
// Type: WorldMapSelectDifficultyDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class WorldMapSelectDifficultyDialog : GameSection
{
  private bool isRegion;
  private bool isOpenedHard;
  private bool enableHardLevel;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.isRegion = (bool) eventData[0];
    this.isOpenedHard = (bool) eventData[1];
    this.enableHardLevel = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 150;
    bool is_visible = this.enableHardLevel;
    if (is_visible && this.isRegion)
      is_visible = this.isOpenedHard;
    this.SetActive((Enum) WorldMapSelectDifficultyDialog.UI.BTN_HARD, is_visible);
    this.SetActive((Enum) WorldMapSelectDifficultyDialog.UI.BTN_HARD_GRAY, !is_visible);
    this.SetText();
    base.Initialize();
  }

  private void SetText()
  {
    this.SetLabelText((Enum) WorldMapSelectDifficultyDialog.UI.STR_NORMAL, this.sectionData.GetText("NORMAL"));
    this.SetLabelText((Enum) WorldMapSelectDifficultyDialog.UI.STR_HARD, this.sectionData.GetText("HARD"));
    string text = string.Format(this.sectionData.GetText("HARD_LV"), (object) 150);
    ((Component) this.GetCtrl((Enum) WorldMapSelectDifficultyDialog.UI.STR_HARD_LV)).GetComponent<UILabel>().text = text;
    ((Component) this.GetCtrl((Enum) WorldMapSelectDifficultyDialog.UI.STR_HARD_LV)).GetComponent<UILabel>().supportEncoding = true;
    this.SetLabelText((Enum) WorldMapSelectDifficultyDialog.UI.STR_HARD_LV, text);
  }

  private void OnQuery_HARD() => GameSection.SetEventData((object) REGION_DIFFICULTY_TYPE.HARD);

  private void OnQuery_NORMAL() => GameSection.SetEventData((object) REGION_DIFFICULTY_TYPE.NORMAL);

  private void OnQuery_HARD_GRAY()
  {
    if (!this.isRegion || !this.enableHardLevel)
      GameSection.SetEventData((object) new object[1]
      {
        (object) 150.ToString()
      });
    else
      GameSection.ChangeEvent("HARD_NOT_OPEN");
  }

  protected enum UI
  {
    BTN_HARD,
    BTN_HARD_GRAY,
    STR_NORMAL,
    STR_HARD,
    STR_HARD_LV,
  }
}
