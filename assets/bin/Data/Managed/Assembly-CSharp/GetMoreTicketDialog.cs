// Decompiled with JetBrains decompiler
// Type: GetMoreTicketDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class GetMoreTicketDialog : GameSection
{
  public override void Initialize()
  {
    ((Behaviour) ((Component) this.GetCtrl((Enum) GetMoreTicketDialog.UI.SPR_BTN_FB)).GetComponent<UIButton>()).enabled = true;
    ((Behaviour) ((Component) this.GetCtrl((Enum) GetMoreTicketDialog.UI.SPR_BTN_Tweter)).GetComponent<UIButton>()).enabled = true;
    base.Initialize();
  }

  private void OnQuery_FACEBOOK() => Native.OpenURL("https://www.facebook.com/DragonProject/");

  private void OnQuery_TWITTER() => Native.OpenURL("https://twitter.com/dragonprojectgl");

  private void OnQuery_OK() => GameSection.BackSection();

  private enum UI
  {
    SPR_BTN_FB,
    SPR_BTN_Tweter,
  }
}
