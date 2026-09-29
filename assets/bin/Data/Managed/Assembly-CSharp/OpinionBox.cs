// Decompiled with JetBrains decompiler
// Type: OpinionBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class OpinionBox : GameSection
{
  public override void UpdateUI()
  {
    this.SetInput((Enum) OpinionBox.UI.IPT_TEXT, string.Empty, 511 /*0x01FF*/);
  }

  protected virtual void OnQuery_SEND()
  {
    GameSection.StayEvent();
    string str = this.GetInputValue((Enum) OpinionBox.UI.IPT_TEXT);
    if (str.IsNullOrWhiteSpace())
      str = str.Trim();
    if (!string.IsNullOrEmpty(str))
      str = str.Replace("\n", "\\n");
    MonoBehaviourSingleton<UserInfoManager>.I.SendOpinionMessage(str, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected enum UI
  {
    IPT_TEXT,
    CLOSE,
  }
}
