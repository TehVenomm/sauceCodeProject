// Decompiled with JetBrains decompiler
// Type: QuestRoomSettingsOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class QuestRoomSettingsOption : MonoBehaviour
{
  public Color texEnableColor;
  public Color disableColor;
  [Header("[0]通常時 : [1]グレー表示時")]
  public string[] buttonSpriteName;
  [Header("[0]通常時 : [1]グレー表示時")]
  public string[] frameSpriteName;
  public UILabel[] lbls;
  public UIButton btn;
  public UISprite spr;
  public UITexture tex;

  public void SetShowOption(bool is_enable)
  {
    int length = this.lbls != null ? this.lbls.Length : 0;
    if (is_enable)
    {
      int index1 = 0;
      for (int index2 = length; index1 < index2; ++index1)
      {
        if (Object.op_Inequality((Object) this.lbls[index1], (Object) null))
          this.lbls[index1].color = Color.white;
      }
      ((Collider) ((Component) this.btn).GetComponent<BoxCollider>()).enabled = true;
      this.btn.normalSprite = this.btn.pressedSprite = this.buttonSpriteName[0];
      this.spr.spriteName = this.frameSpriteName[0];
      this.tex.color = this.texEnableColor;
    }
    else
    {
      int index3 = 0;
      for (int index4 = length; index3 < index4; ++index3)
      {
        if (Object.op_Inequality((Object) this.lbls[index3], (Object) null))
          this.lbls[index3].color = this.disableColor;
      }
      ((Collider) ((Component) this.btn).GetComponent<BoxCollider>()).enabled = false;
      this.btn.normalSprite = this.btn.pressedSprite = this.buttonSpriteName[1];
      this.spr.spriteName = this.frameSpriteName[1];
      this.tex.color = this.disableColor;
    }
    this.btn.UpdateColor(true);
  }
}
