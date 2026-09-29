// Decompiled with JetBrains decompiler
// Type: UISpriteShaderReplacer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UISpriteShaderReplacer : MonoBehaviour
{
  private UISprite sprite;
  private UISpriteShaderReplacer.AtlasEntry entry;
  private static Dictionary<UIAtlas, UISpriteShaderReplacer.AtlasEntry> atlases = new Dictionary<UIAtlas, UISpriteShaderReplacer.AtlasEntry>();

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
      --this.entry.refCount;
      this.entry = (UISpriteShaderReplacer.AtlasEntry) null;
    }
    if (UISpriteShaderReplacer.atlases.TryGetValue(this.sprite.atlas, out this.entry) && !Object.op_Implicit((Object) this.entry.atlas))
    {
      UISpriteShaderReplacer.atlases.Remove(this.sprite.atlas);
      this.entry = (UISpriteShaderReplacer.AtlasEntry) null;
    }
    if (this.entry == null)
    {
      UIAtlas atlas = ResourceUtility.Instantiate<UIAtlas>(this.sprite.atlas);
      atlas.spriteMaterial = new Material(atlas.spriteMaterial);
      atlas.spriteMaterial.shader = ResourceUtility.FindShader(shaderName);
      this.entry = new UISpriteShaderReplacer.AtlasEntry(atlas);
      UISpriteShaderReplacer.atlases.Add(this.sprite.atlas, this.entry);
      if (MonoBehaviourSingleton<AppMain>.IsValid())
        ((Component) atlas).transform.parent = MonoBehaviourSingleton<AppMain>.I._transform;
    }
    ++this.entry.refCount;
    this.sprite.atlas = this.entry.atlas;
  }

  private void OnDestroy()
  {
    if (AppMain.isApplicationQuit || this.entry == null)
      return;
    --this.entry.refCount;
    if (this.entry.refCount > 0)
      return;
    UIAtlas key = (UIAtlas) null;
    foreach (KeyValuePair<UIAtlas, UISpriteShaderReplacer.AtlasEntry> atlase in UISpriteShaderReplacer.atlases)
    {
      if (atlase.Value == this.entry)
        key = atlase.Key;
    }
    if (Object.op_Inequality((Object) null, (Object) key))
      UISpriteShaderReplacer.atlases.Remove(key);
    if (!Object.op_Implicit((Object) this.entry.atlas))
      return;
    Object.Destroy((Object) this.entry.atlas.spriteMaterial);
    Object.Destroy((Object) ((Component) this.entry.atlas).gameObject);
  }

  private class AtlasEntry
  {
    public UIAtlas atlas;
    public int refCount;

    public AtlasEntry(UIAtlas atlas) => this.atlas = atlas;
  }
}
