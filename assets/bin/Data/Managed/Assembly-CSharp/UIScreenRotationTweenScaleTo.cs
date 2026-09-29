// Decompiled with JetBrains decompiler
// Type: UIScreenRotationTweenScaleTo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIScreenRotationTweenScaleTo : UIScreenRotationHandler
{
  [SerializeField]
  private TweenScale target;
  [SerializeField]
  private float portrait;
  [SerializeField]
  private float landscape;

  protected override void OnScreenRotate(bool is_portrait)
  {
    if (is_portrait)
      this.target.to = new Vector3(this.portrait, this.portrait, this.portrait);
    else
      this.target.to = new Vector3(this.landscape, this.landscape, this.landscape);
  }
}
