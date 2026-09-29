// Decompiled with JetBrains decompiler
// Type: BaseBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class BaseBanner : GameSection
{
  public override string overrideBackKeyEvent => "CLOSE";

  public override void UpdateUI()
  {
    ResourceLoad.LoadCommonTexture(((Component) this.FindCtrl(this._transform, (Enum) BaseBanner.UI.BANNER)).GetComponent<UITexture>(), MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
  }

  private enum UI
  {
    BANNER,
  }
}
