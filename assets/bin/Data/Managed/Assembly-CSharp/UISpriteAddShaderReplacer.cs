// Decompiled with JetBrains decompiler
// Type: UISpriteAddShaderReplacer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UISpriteAddShaderReplacer : MonoBehaviour
{
  private UISprite sprite;
  private UIManager.AtlasEntry entry;

  private void Awake() => this.sprite = ((Component) this).GetComponent<UISprite>();

  public void Replace(string shaderName)
  {
    if (!Object.op_Implicit((Object) this.sprite))
    {
      this.Awake();
      if (!Object.op_Implicit((Object) this.sprite))
        return;
    }
    if (this.entry != null)
    {
      MonoBehaviourSingleton<UIManager>.I.ReleaseAtlas(this.sprite);
      this.entry = (UIManager.AtlasEntry) null;
    }
    this.entry = MonoBehaviourSingleton<UIManager>.I.ReplaceAtlas(this.sprite, shaderName);
  }

  private void OnDestroy()
  {
    if (AppMain.isApplicationQuit || !MonoBehaviourSingleton<UIManager>.IsValid() || this.entry == null)
      return;
    MonoBehaviourSingleton<UIManager>.I.ReleaseAtlas(this.sprite);
  }
}
