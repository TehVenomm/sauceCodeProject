// Decompiled with JetBrains decompiler
// Type: LoungeInviteDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class LoungeInviteDialog : QuestRoomInviteDialog
{
  public override void Initialize()
  {
    string inviteMessage = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetInviteMessage();
    this.helpLinkURL = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetInviteHelpURL();
    this.inviteMessage = inviteMessage.Replace("<BR>", "\n").Replace("<br>", "\n");
    this.InitializeBase();
  }

  protected new enum UI
  {
    LBL_SNS,
    OBJ_LINE_ROOT,
    OBJ_TWITTER_ROOT,
    LBL_SERVICE,
    LBL_INVITE,
  }
}
