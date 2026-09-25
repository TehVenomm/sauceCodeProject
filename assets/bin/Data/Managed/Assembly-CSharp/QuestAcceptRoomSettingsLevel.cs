// Decompiled with JetBrains decompiler
// Type: QuestAcceptRoomSettingsLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class QuestAcceptRoomSettingsLevel : QuestAcceptEntryPassRoom
{
  private const int LEVEL_MAX_DIGIT = 3;
  private QuestAcceptRoomSettingsLevel.UI[] lblAry = new QuestAcceptRoomSettingsLevel.UI[5]
  {
    QuestAcceptRoomSettingsLevel.UI.LBL_INPUT_PASS_1,
    QuestAcceptRoomSettingsLevel.UI.LBL_INPUT_PASS_2,
    QuestAcceptRoomSettingsLevel.UI.LBL_INPUT_PASS_3,
    QuestAcceptRoomSettingsLevel.UI.LBL_INPUT_PASS_4,
    QuestAcceptRoomSettingsLevel.UI.LBL_INPUT_PASS_5
  };

  public override void Initialize()
  {
    int index1 = 0;
    for (int length = this.passCode.Length; index1 < length; ++index1)
      this.passCode[index1] = string.Empty;
    if (MonoBehaviourSingleton<PartyManager>.I.partySetting != null && MonoBehaviourSingleton<PartyManager>.I.partySetting.level > 0)
    {
      string str = MonoBehaviourSingleton<PartyManager>.I.partySetting.level.ToString();
      int length = str.Length;
      if (length > 0)
      {
        int index2 = 0;
        for (int index3 = length; index2 < index3; ++index2)
          this.passCode[index2] = str[index2].ToString();
        this.passCodeIndex = length;
      }
    }
    this.digit = 3;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) QuestAcceptRoomSettingsLevel.UI.STR_NON_SETTINGS, false);
    int index = 0;
    for (int length = this.passCode.Length; index < length; ++index)
      this.SetLabelText((Enum) this.lblAry[index], string.Empty);
    string text = string.Join("", this.passCode);
    if (text.Length == 0)
      this.SetActive((Enum) QuestAcceptRoomSettingsLevel.UI.STR_NON_SETTINGS, true);
    else
      this.SetLabelText((Enum) QuestAcceptRoomSettingsLevel.UI.LBL_INPUT_PASS_3, text);
  }

  protected override void OnOpen()
  {
  }

  protected override string GetResetString() => string.Empty;

  protected override void OnQuery_0()
  {
    if (this.passCodeIndex == 0)
      GameSection.StopEvent();
    else
      base.OnQuery_0();
  }

  protected override void OnQuery_ROOM()
  {
    int result = 0;
    int.TryParse(string.Join("", this.passCode).Replace("-", ""), out result);
    MonoBehaviourSingleton<PartyManager>.I.partySetting.level = result;
    GameSection.ChangeEvent("[BACK]");
  }

  private void OnQuery_SECTION_BACK()
  {
    MonoBehaviourSingleton<PartyManager>.I.SetPartySetting((PartyManager.PartySetting) null);
  }

  protected new enum UI
  {
    LBL_INPUT_PASS_1,
    LBL_INPUT_PASS_2,
    LBL_INPUT_PASS_3,
    LBL_INPUT_PASS_4,
    LBL_INPUT_PASS_5,
    STR_NON_SETTINGS,
  }
}
