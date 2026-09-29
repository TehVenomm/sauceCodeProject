// Decompiled with JetBrains decompiler
// Type: UVScroller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UVScroller : MonoBehaviour
{
  private Material mat;
  private Vector2 offset = Vector2.zero;
  [SerializeField]
  private Vector2 speed = Vector2.zero;

  private void Awake() => this.mat = ((Component) this).GetComponent<Renderer>().material;

  private void Update()
  {
    this.offset = Vector2.op_Addition(this.offset, Vector2.op_Multiply(this.speed, Time.deltaTime));
    this.offset = new Vector2(Mathf.Repeat(this.offset.x, 1f), Mathf.Repeat(this.offset.y, 1f));
    this.mat.SetTextureOffset("_MainTex", this.offset);
  }
}
