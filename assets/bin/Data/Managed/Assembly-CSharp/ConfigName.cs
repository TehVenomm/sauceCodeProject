// Decompiled with JetBrains decompiler
// Type: ConfigName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Text.RegularExpressions;

#nullable disable
public class ConfigName : GameSection
{
  protected ConfigName.SECTION_TYPE sectionType;
  protected string before_text;
  protected int inputMaxLength;

  protected virtual void SetBeforeText()
  {
    this.before_text = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
    this.inputMaxLength = 14;
  }

  public override void Initialize()
  {
    this.SetBeforeText();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) ConfigName.UI.SPR_TITLE_CHANGE_NAME, this.sectionType == ConfigName.SECTION_TYPE.CHANGE_NAME);
    this.SetActive((Enum) ConfigName.UI.SPR_TITLE_CHANGE_COMMENT, this.sectionType == ConfigName.SECTION_TYPE.CHANGE_COMMENT);
    this.SetActive((Enum) ConfigName.UI.SPR_TITLE_SEARCH_NAME, this.sectionType == ConfigName.SECTION_TYPE.SEARCH_NAME);
    this.SetActive((Enum) ConfigName.UI.SPR_TITLE_SEARCH_ID, this.sectionType == ConfigName.SECTION_TYPE.SEARCH_ID);
    this.SetInput((Enum) ConfigName.UI.IPT_TEXT, this.before_text, this.inputMaxLength, new EventDelegate.Callback(this.UpdateButton));
  }

  private void UpdateButton()
  {
    string str = this.CheckString(this.GetInputValue((Enum) ConfigName.UI.IPT_TEXT));
    this.SetInputValue((Enum) ConfigName.UI.IPT_TEXT, str);
    this.SetButtonEnabled((Enum) ConfigName.UI.BTN_OK, str != this.before_text && str.Length > 0);
  }

  private void OnQuery_OK()
  {
    string inputValue = this.GetInputValue((Enum) ConfigName.UI.IPT_TEXT);
    GameSection.StayEvent();
    MonoBehaviourSingleton<UserInfoManager>.I.SendChangeName(inputValue, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private string CheckString(string s) => Regex.Replace(s, "\\p{Cs}", "");

  private enum UI
  {
    IPT_TEXT,
    BTN_OK,
    SPR_TITLE_CHANGE_NAME,
    SPR_TITLE_CHANGE_COMMENT,
    SPR_TITLE_SEARCH_NAME,
    SPR_TITLE_SEARCH_ID,
  }

  protected enum SECTION_TYPE
  {
    CHANGE_NAME,
    CHANGE_COMMENT,
    SEARCH_NAME,
    SEARCH_ID,
  }
}
