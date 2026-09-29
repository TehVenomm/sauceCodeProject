// Decompiled with JetBrains decompiler
// Type: ClanNoticeBoardEditor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ClanNoticeBoardEditor : GameSection
{
  public override void UpdateUI()
  {
    this.SetInput((Enum) ClanNoticeBoardEditor.UI.IPT_TEXT, MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.body, 256 /*0x0100*/);
  }

  protected virtual void OnQuery_SEND()
  {
    GameSection.StayEvent();
    string msg = this.GetInputValue((Enum) ClanNoticeBoardEditor.UI.IPT_TEXT);
    if (!string.IsNullOrEmpty(msg))
      msg = msg.Replace("\n", "\\n");
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestEditNoticeBoard(msg, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected enum UI
  {
    IPT_TEXT,
  }
}
