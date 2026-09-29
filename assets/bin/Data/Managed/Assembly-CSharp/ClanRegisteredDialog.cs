// Decompiled with JetBrains decompiler
// Type: ClanRegisteredDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ClanRegisteredDialog : GameSection
{
  public override void UpdateUI()
  {
    this.SetLabelText((Enum) ClanRegisteredDialog.UI.LBL_CLAN_NAME, MonoBehaviourSingleton<UserInfoManager>.I.userClan.name);
    this.SetActive((Enum) ClanRegisteredDialog.UI.BTN_SETTING, MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader());
  }

  private void OnQuery_CLAN_DETAIL() => GameSection.SetEventData((object) "0");

  private void OnQuery_TO_CLAN()
  {
  }

  private enum UI
  {
    LBL_CLAN_NAME,
    BTN_SETTING,
  }
}
