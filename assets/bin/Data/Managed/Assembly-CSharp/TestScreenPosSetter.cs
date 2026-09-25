// Decompiled with JetBrains decompiler
// Type: TestScreenPosSetter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TestScreenPosSetter : MonoBehaviour
{
  public PuniController punicon;

  private void Start()
  {
  }

  private void Update()
  {
    if (this.IsTouchOn())
      this.punicon.SetStartPosition(this.GetTouchScreenPos());
    if (this.IsTouchOff())
      this.punicon.Reset();
    if (!this.IsTouch())
      return;
    this.punicon.SetEndPosition(this.GetTouchScreenPos());
  }

  private bool IsTouchOn() => false;

  private bool IsTouchOff() => false;

  private bool IsTouch() => false;

  private Vector3 GetTouchScreenPos() => Vector3.zero;
}
