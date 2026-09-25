// Decompiled with JetBrains decompiler
// Type: UIScreenRotationSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class UIScreenRotationSize : UIScreenRotationHandler
{
  [SerializeField]
  private UIWidget target;
  [SerializeField]
  private UIScreenRotationSize.Point portrait = UIScreenRotationSize.Point.zero;
  [SerializeField]
  private UIScreenRotationSize.Point landscape = UIScreenRotationSize.Point.zero;

  protected override void OnScreenRotate(bool is_portrait)
  {
    if (is_portrait)
    {
      this.target.width = this.portrait.x;
      this.target.height = this.portrait.y;
    }
    else
    {
      this.target.width = this.landscape.x;
      this.target.height = this.landscape.y;
    }
  }

  [Serializable]
  private struct Point
  {
    public int x;
    public int y;

    public static UIScreenRotationSize.Point zero
    {
      get
      {
        UIScreenRotationSize.Point zero;
        zero.x = 0;
        zero.y = 0;
        return zero;
      }
    }
  }
}
