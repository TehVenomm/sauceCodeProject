// Decompiled with JetBrains decompiler
// Type: ResolutionIphoneXResource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class ResolutionIphoneXResource
{
  public string Name;
  public GameObject PanelFix;
  public bool LockRatioPosition;
  public string Path = "";
  public bool IsEditMain;

  public bool IsAdd
  {
    get
    {
      return Object.op_Inequality((Object) this.PanelFix, (Object) null) && Object.op_Implicit((Object) this.PanelFix.GetComponent<FixedNGUIThrowIphoneX>());
    }
  }
}
