// Decompiled with JetBrains decompiler
// Type: LoungeAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class LoungeAnnounce : UIBehaviour
{
  protected UIWidget widget;
  protected UITweenCtrl tweenCtrl;

  public void Play(LoungeAnnounce.AnnounceData data, System.Action onComplete)
  {
    this.Play(data.type, data.name, onComplete);
  }

  public virtual void Play(LoungeAnnounce.ANNOUNCE_TYPE type, string userName, System.Action onComplete)
  {
    this.SetActive((Enum) LoungeAnnounce.UI.WGT_ANCHOR_POINT, true);
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
          this.SetLabelText((Enum) LoungeAnnounce.UI.LBL_ANNOUNCE, StringTable.Get(STRING_CATEGORY.LOUNGE, 0U));
          break;
        case LoungeAnnounce.ANNOUNCE_TYPE.JOIN_LOUNGE:
          this.SetLabelText((Enum) LoungeAnnounce.UI.LBL_ANNOUNCE, StringTable.Get(STRING_CATEGORY.LOUNGE, 1U));
          break;
        case LoungeAnnounce.ANNOUNCE_TYPE.LEAVED_LOUNGE:
          this.SetLabelText((Enum) LoungeAnnounce.UI.LBL_ANNOUNCE, StringTable.Get(STRING_CATEGORY.LOUNGE, 2U));
          break;
      }
      this.SetLabelText((Enum) LoungeAnnounce.UI.LBL_USER_NAME, userName);
      this.SetFontStyle((Enum) LoungeAnnounce.UI.LBL_ANNOUNCE, (FontStyle) 2);
      this.SetFontStyle((Enum) LoungeAnnounce.UI.LBL_USER_NAME, (FontStyle) 2);
      this.tweenCtrl.Reset();
      this.tweenCtrl.Play(onFinished: (EventDelegate.Callback) (() =>
      {
        if (onComplete == null)
          return;
        onComplete();
      }));
    }
  }

  private void Start()
  {
    Transform ctrl = this.GetCtrl((Enum) LoungeAnnounce.UI.OBJ_EFFECT);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
      ctrl.localScale = Vector3.zero;
    this.widget = this.GetComponent<UIWidget>((Enum) LoungeAnnounce.UI.WGT_ANCHOR_POINT);
    this.tweenCtrl = this.GetComponent<UITweenCtrl>((Enum) LoungeAnnounce.UI.OBJ_TWEENCTRL);
    this.SetActive((Enum) LoungeAnnounce.UI.WGT_ANCHOR_POINT, false);
  }

  public enum UI
  {
    WGT_ANCHOR_POINT,
    OBJ_TWEENCTRL,
    OBJ_EFFECT,
    LBL_ANNOUNCE,
    LBL_USER_NAME,
  }

  public enum ANNOUNCE_TYPE
  {
    CREATED_PARTY,
    JOIN_LOUNGE,
    LEAVED_LOUNGE,
  }

  public class AnnounceData
  {
    public LoungeAnnounce.ANNOUNCE_TYPE type { get; private set; }

    public string name { get; private set; }

    public AnnounceData(LoungeAnnounce.ANNOUNCE_TYPE setType, string setName)
    {
      this.type = setType;
      this.name = setName;
    }
  }
}
