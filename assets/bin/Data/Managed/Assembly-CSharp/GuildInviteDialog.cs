// Decompiled with JetBrains decompiler
// Type: GuildInviteDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GuildInviteDialog : QuestRoomInviteDialog
{
  public override void Initialize()
  {
    string inviteMessage = MonoBehaviourSingleton<GuildManager>.I.GetInviteMessage();
    this.helpLinkURL = MonoBehaviourSingleton<GuildManager>.I.GetInviteHelpURL();
    this.inviteMessage = inviteMessage.Replace("<BR>", "\n").Replace("<br>", "\n");
    this.InitializeBase();
  }
}
