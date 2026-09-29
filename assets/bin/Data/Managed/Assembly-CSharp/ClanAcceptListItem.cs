// Decompiled with JetBrains decompiler
// Type: ClanAcceptListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class ClanAcceptListItem : UIBehaviour
{
  public void Setup(Transform t, int index, FriendCharaInfo info)
  {
    this.SetEvent(t, "DETAIL", index);
    this.SetEvent(t, (Enum) ClanAcceptListItem.UI.BTN_ACCEPT, "ACCEPT", index);
    this.SetEvent(t, (Enum) ClanAcceptListItem.UI.BTN_REJECT, "REJECT", index);
    this.SetLabelText(t, (Enum) ClanAcceptListItem.UI.LBL_NAME, info.name);
    this.SetLabelText(t, (Enum) ClanAcceptListItem.UI.LBL_LEVEL, $"Lv.{info.level,3:D}");
    this.SetLabelText(t, (Enum) ClanAcceptListItem.UI.LBL_COMMENT, info.comment);
    this.SetLabelText(t, (Enum) ClanAcceptListItem.UI.LBL_HP, info.hp.ToString());
    this.SetLabelText(t, (Enum) ClanAcceptListItem.UI.LBL_ATK, info.atk.ToString());
    this.SetLabelText(t, (Enum) ClanAcceptListItem.UI.LBL_DEF, info.def.ToString());
  }

  private enum UI
  {
    LBL_NAME,
    LBL_LEVEL,
    LBL_COMMENT,
    LBL_HP,
    LBL_ATK,
    LBL_DEF,
    BTN_ACCEPT,
    BTN_REJECT,
  }
}
