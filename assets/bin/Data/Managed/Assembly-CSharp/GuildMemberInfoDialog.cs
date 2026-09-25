// Decompiled with JetBrains decompiler
// Type: GuildMemberInfoDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class GuildMemberInfoDialog : GameSection
{
  private FriendCharaInfo data;

  public override void Initialize()
  {
    this.data = GameSection.GetEventData() as FriendCharaInfo;
    base.Initialize();
  }

  private void OnQuery_PROFILE() => GameSection.SetEventData((object) this.data);

  private void OnQuery_MESSAGE() => MonoBehaviourSingleton<GuildManager>.I.SetTalkUser(this.data);

  private void OnQuery_JUMP()
  {
  }

  private void OnQuery_BAN()
  {
  }
}
