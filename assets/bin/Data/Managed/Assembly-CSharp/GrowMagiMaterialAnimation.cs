// Decompiled with JetBrains decompiler
// Type: GrowMagiMaterialAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class GrowMagiMaterialAnimation : MonoBehaviour
{
  [SerializeField]
  public float rotate = 5f;

  private void Start()
  {
  }

  private void Anim()
  {
    ((Component) this).GetComponentInChildren<Animation>().Play("MaterialAnim_1");
  }

  private void Update() => ((Component) this).transform.Rotate(0.0f, this.rotate, 0.0f);
}
