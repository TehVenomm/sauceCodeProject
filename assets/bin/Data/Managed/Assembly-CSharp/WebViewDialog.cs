// Decompiled with JetBrains decompiler
// Type: WebViewDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class WebViewDialog : GameSection
{
  public override string overrideBackKeyEvent
  {
    get
    {
      return ((Object) ((Component) this).gameObject).name == nameof (WebViewDialog) ? "[BACK]" : "CLOSE";
    }
  }

  public override void Initialize()
  {
    if (!(GameSceneEvent.current.userData is string str))
    {
      List<GameSceneTables.TextData> currentSectionTextList = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList();
      if (currentSectionTextList.Count > 0)
        str = currentSectionTextList[0].text;
    }
    if (MonoBehaviourSingleton<WebViewManager>.IsValid())
      MonoBehaviourSingleton<WebViewManager>.I.Open(NetworkManager.APP_HOST + str, (Action<string>) (result => this.DispatchEvent("CLOSE")));
    base.Initialize();
  }
}
