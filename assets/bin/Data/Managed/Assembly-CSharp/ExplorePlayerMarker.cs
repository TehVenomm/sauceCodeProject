// Decompiled with JetBrains decompiler
// Type: ExplorePlayerMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ExplorePlayerMarker : MonoBehaviour
{
  [SerializeField]
  private Vector3[] offsets = new Vector3[4];
  private UISprite sprite_;

  private void Awake() => this.sprite_ = ((Component) this).GetComponent<UISprite>();

  public void SetIndex(int idx)
  {
    if (Object.op_Equality((Object) null, (Object) this.sprite_) || 0 > idx || 4 <= idx)
      return;
    this.sprite_.spriteName = "PlayerMarker_skin_2d_" + idx.ToString("D2");
    ((Component) this).transform.localPosition = this.offsets[idx];
  }
}
