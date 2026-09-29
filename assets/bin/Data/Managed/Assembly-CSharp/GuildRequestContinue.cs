// Decompiled with JetBrains decompiler
// Type: GuildRequestContinue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class GuildRequestContinue : GameSection
{
  public override void UpdateUI()
  {
    this.SetLabelText((Enum) GuildRequestContinue.UI.MESSAGE, GameSection.GetEventData() as string);
    base.UpdateUI();
  }

  private void OnQuery_YES()
  {
    if (!GameSection.CheckCrystal(MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem().crystalNum))
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestExtend((Action<bool>) (questCompleteData =>
    {
      GameSection.ResumeEvent(questCompleteData);
      GameSection.SetEventData((object) questCompleteData);
    }));
  }

  private void OnQuery_NO()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestRetire((Action<bool>) (questCompleteData =>
    {
      GameSection.ResumeEvent(questCompleteData);
      GameSection.SetEventData((object) questCompleteData);
    }));
  }

  private enum UI
  {
    MESSAGE,
  }
}
