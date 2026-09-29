// Decompiled with JetBrains decompiler
// Type: RectInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public struct RectInt
{
  public int top;
  public int bottom;
  public int left;
  public int right;

  public void Set(int left, int right, int bottom, int top)
  {
    this.left = left;
    this.right = right;
    this.bottom = bottom;
    this.top = top;
  }

  public void Setf(float left, float right, float bottom, float top)
  {
    this.left = Mathf.CeilToInt(left);
    this.right = Mathf.CeilToInt(right);
    this.bottom = Mathf.CeilToInt(bottom);
    this.top = Mathf.CeilToInt(top);
  }

  public void Scale(float x, float y)
  {
    this.top = Mathf.CeilToInt((float) this.top * y);
    this.bottom = Mathf.CeilToInt((float) this.bottom * y);
    this.left = Mathf.CeilToInt((float) this.left * x);
    this.right = Mathf.CeilToInt((float) this.right * x);
  }
}
