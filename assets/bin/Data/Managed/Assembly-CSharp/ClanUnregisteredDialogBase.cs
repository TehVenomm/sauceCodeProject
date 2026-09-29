// Decompiled with JetBrains decompiler
// Type: ClanUnregisteredDialogBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ClanUnregisteredDialogBase : GameSection
{
  private void OnQuery_CREATE()
  {
  }

  private void OnQuery_SEARCH()
  {
  }

  private void OnQuery_FOLLOWER_LIST()
  {
  }

  private void OnQuery_INVITED_LIST()
  {
  }

  private void OnQuery_HELP() => GameSection.SetEventData((object) WebViewManager.Clan);
}
