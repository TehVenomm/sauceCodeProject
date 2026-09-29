// Decompiled with JetBrains decompiler
// Type: EdgeInsets
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
public class EdgeInsets
{
  public float top;
  public float bottom;
  public float left;
  public float right;
  public float width;
  public float height;
  public static EdgeInsets zero = new EdgeInsets(0.0f, 0.0f, 0.0f, 0.0f);

  public EdgeInsets(float top, float left, float bottom, float right, float width = 0.0f, float height = 0.0f)
  {
    this.Set(top, left, bottom, right, width, height);
  }

  public void Set(float top, float left, float bottom, float right, float width = 0.0f, float height = 0.0f)
  {
    this.top = top;
    this.left = left;
    this.bottom = bottom;
    this.right = right;
    this.width = width;
    this.height = height;
  }

  public override string ToString()
  {
    return $"[EdgeInsets: ({this.top},{this.left},{this.bottom},{this.right})]";
  }

  public bool IsZero()
  {
    return (double) this.top == 0.0 && (double) this.bottom == 0.0 && (double) this.left == 0.0 && (double) this.right == 0.0;
  }

  public float ScreenWidth => (double) this.width == 0.0 ? (float) Screen.width : this.width;

  public float ScreenHeight => (double) this.height == 0.0 ? (float) Screen.height : this.height;

  public float SafeHeightMax => this.ScreenHeight - Mathf.Max(this.top, this.bottom) * 2f;

  public float SafeHeight => this.ScreenHeight - this.top - this.bottom;

  public float SafeWidthMax => this.ScreenWidth - Mathf.Max(this.left, this.right) * 2f;

  public float SafeWidth => this.ScreenWidth - this.left - this.right;
}
