// Decompiled with JetBrains decompiler
// Type: UIAnimationGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIAnimationGauge : UIHGauge
{
  private Material _mat;

  private void Awake()
  {
    this._mat = ((Renderer) ((Component) this).GetComponent<MeshRenderer>()).material;
  }

  protected override void UpdateGauge()
  {
    if (Object.op_Equality((Object) this._mat, (Object) null))
      return;
    this._mat.SetFloat("_Ratio", this.nowPercent);
  }
}
