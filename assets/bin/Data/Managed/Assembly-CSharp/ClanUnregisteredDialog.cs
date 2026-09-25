// Decompiled with JetBrains decompiler
// Type: ClanUnregisteredDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ClanUnregisteredDialog : ClanUnregisteredDialogBase
{
  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetBadge((Enum) ClanUnregisteredDialog.UI.BTN_SCOUTED, MonoBehaviourSingleton<UserInfoManager>.I.clanInviteNum, (SpriteAlignment) 3, -15, -10);
  }

  private enum UI
  {
    BTN_SCOUTED,
  }
}
