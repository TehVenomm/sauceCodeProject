// Decompiled with JetBrains decompiler
// Type: CommunityInClanDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class CommunityInClanDialog : CommunityDialogBase
{
  private void OnQuery_CLAN_DETAIL() => GameSection.SetEventData((object) "0");

  private new enum UI
  {
    SPR_FRAME,
    BTN_CLAN_ON,
    BTN_CLAN_OFF,
    BTN_LOUNGE,
    BTN_LOUNGE_OFF,
    BTN_EXIT,
    SPR_NEW_ICON,
  }
}
