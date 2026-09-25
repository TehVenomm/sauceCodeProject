// Decompiled with JetBrains decompiler
// Type: UIScreenRotationPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIScreenRotationPosition : UIScreenRotationHandler
{
  [SerializeField]
  private Transform target;
  [SerializeField]
  private Vector3 portrait = Vector3.zero;
  [SerializeField]
  private Vector3 landscape = Vector3.zero;

  protected override void OnScreenRotate(bool is_portrait)
  {
    if (is_portrait)
      this.target.localPosition = this.portrait;
    else
      this.target.localPosition = this.landscape;
  }
}
