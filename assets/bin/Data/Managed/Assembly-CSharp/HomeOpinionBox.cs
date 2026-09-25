// Decompiled with JetBrains decompiler
// Type: HomeOpinionBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class HomeOpinionBox : OpinionBox
{
  private bool isFromRieviewAppeal;
  private HomeAppReviewAppealDialogBase.Info infoDataOnStart;

  public override void Initialize()
  {
    object eventData = GameSection.GetEventData();
    this.isFromRieviewAppeal = eventData != null && eventData is HomeAppReviewAppealDialogBase.Info;
    if (this.isFromRieviewAppeal)
      this.infoDataOnStart = (HomeAppReviewAppealDialogBase.Info) eventData;
    base.Initialize();
    this.SetEvent((Enum) OpinionBox.UI.CLOSE, "CLOSE", 0);
  }

  protected override void OnQuery_SEND()
  {
    GameSection.StayEvent();
    string msg = this.GetInputValue((Enum) OpinionBox.UI.IPT_TEXT);
    if (!string.IsNullOrEmpty(msg))
      msg = msg.Replace("\n", "\\n");
    MonoBehaviourSingleton<UserInfoManager>.I.SendOpinionMessage(msg, (Action<bool>) (is_success =>
    {
      if (this.isFromRieviewAppeal & is_success)
        this.SendInfo(this.infoDataOnStart);
      else
        GameSection.ResumeEvent(is_success);
    }));
  }

  protected void OnQuery_CLOSE()
  {
    if (!this.isFromRieviewAppeal)
      return;
    GameSection.StayEvent();
    this.SendInfo(this.infoDataOnStart.starValue, 2);
  }

  private void SendInfo(HomeAppReviewAppealDialogBase.Info info)
  {
    this.SendInfo(info.starValue, info.replyAction);
  }

  private void SendInfo(int starValue, int replyAction)
  {
    MonoBehaviourSingleton<UserInfoManager>.I.SendAppReviewInfo(starValue, replyAction, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }
}
