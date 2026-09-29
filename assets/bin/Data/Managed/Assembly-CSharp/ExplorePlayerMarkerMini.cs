// Decompiled with JetBrains decompiler
// Type: ExplorePlayerMarkerMini
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ExplorePlayerMarkerMini : MonoBehaviour
{
  [SerializeField]
  public Color32[] colors = new Color32[4];
  public ExplorePlayerMarkerMini.Size playerSize;
  public ExplorePlayerMarkerMini.Size friendSize;
  [SerializeField]
  private Vector3[] offsets = new Vector3[4];
  private UISprite sprite_;

  private void Awake() => this.sprite_ = ((Component) this).GetComponent<UISprite>();

  public void SetIndex(int idx)
  {
    if (Object.op_Equality((Object) null, (Object) this.sprite_) || 0 > idx || 4 <= idx)
      return;
    if (idx == 0)
    {
      this.sprite_.spriteName = "dp_radar_player";
      this.sprite_.color = Color.white;
      this.sprite_.width = this.playerSize.width;
      this.sprite_.height = this.playerSize.height;
      ++this.sprite_.depth;
      ((Component) this).transform.localPosition = this.offsets[idx];
    }
    else
    {
      this.sprite_.spriteName = "dp_radar_another";
      this.sprite_.color = Color32.op_Implicit(this.colors[idx]);
      this.sprite_.width = this.friendSize.width;
      this.sprite_.height = this.friendSize.height;
      ((Component) this).transform.localPosition = this.offsets[idx];
    }
  }

  [Serializable]
  public class Size
  {
    public int width;
    public int height;
  }
}
