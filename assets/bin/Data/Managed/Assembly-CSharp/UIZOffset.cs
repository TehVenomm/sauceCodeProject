// Decompiled with JetBrains decompiler
// Type: UIZOffset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIZOffset : MonoBehaviour
{
  private Material _mat;
  [SerializeField]
  private int zOffset;
  private int sourceQueue;

  private void Awake()
  {
    MeshRenderer component = ((Component) this).GetComponent<MeshRenderer>();
    this._mat = new Material(((Renderer) component).sharedMaterial);
    this.sourceQueue = this._mat.renderQueue;
    this._mat.renderQueue = this.sourceQueue + this.zOffset;
    ((Renderer) component).material = this._mat;
  }

  private void Update()
  {
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this._mat, (Object) null))
      return;
    Object.Destroy((Object) this._mat);
  }
}
