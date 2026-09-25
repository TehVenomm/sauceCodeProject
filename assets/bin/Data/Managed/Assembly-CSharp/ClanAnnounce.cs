// Decompiled with JetBrains decompiler
// Type: ClanAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ClanAnnounce : LoungeAnnounce
{
  public override void Play(LoungeAnnounce.ANNOUNCE_TYPE type, string userName, System.Action onComplete)
  {
    this.SetActive((Enum) ClanAnnounce.UI.WGT_ANCHOR_POINT, true);
    if (Object.op_Equality((Object) this.widget, (Object) null) || Object.op_Equality((Object) this.tweenCtrl, (Object) null))
    {
      if (onComplete == null)
        return;
      onComplete();
    }
    else
    {
      switch (type)
      {
        case LoungeAnnounce.ANNOUNCE_TYPE.CREATED_PARTY:
          this.SetLabelText((Enum) ClanAnnounce.UI.LBL_ANNOUNCE, StringTable.Get(STRING_CATEGORY.CLAN, 0U));
          break;
        case LoungeAnnounce.ANNOUNCE_TYPE.JOIN_LOUNGE:
          this.SetLabelText((Enum) ClanAnnounce.UI.LBL_ANNOUNCE, StringTable.Get(STRING_CATEGORY.CLAN, 1U));
          break;
        case LoungeAnnounce.ANNOUNCE_TYPE.LEAVED_LOUNGE:
          this.SetLabelText((Enum) ClanAnnounce.UI.LBL_ANNOUNCE, StringTable.Get(STRING_CATEGORY.CLAN, 2U));
          break;
      }
      this.SetLabelText((Enum) ClanAnnounce.UI.LBL_USER_NAME, userName);
      this.SetFontStyle((Enum) ClanAnnounce.UI.LBL_ANNOUNCE, (FontStyle) 2);
      this.SetFontStyle((Enum) ClanAnnounce.UI.LBL_USER_NAME, (FontStyle) 2);
      this.tweenCtrl.Reset();
      this.tweenCtrl.Play(onFinished: (EventDelegate.Callback) (() =>
      {
        if (onComplete == null)
          return;
        onComplete();
      }));
    }
  }

  public new enum UI
  {
    WGT_ANCHOR_POINT,
    OBJ_TWEENCTRL,
    OBJ_EFFECT,
    LBL_ANNOUNCE,
    LBL_USER_NAME,
  }
}
