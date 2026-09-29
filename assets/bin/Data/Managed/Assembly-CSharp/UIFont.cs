// Decompiled with JetBrains decompiler
// Type: UIFont
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Font")]
public class UIFont : MonoBehaviour
{
  [HideInInspector]
  [SerializeField]
  private Material mMat;
  [HideInInspector]
  [SerializeField]
  private Rect mUVRect = new Rect(0.0f, 0.0f, 1f, 1f);
  [HideInInspector]
  [SerializeField]
  private BMFont mFont = new BMFont();
  [HideInInspector]
  [SerializeField]
  private UIAtlas mAtlas;
  [HideInInspector]
  [SerializeField]
  private UIFont mReplacement;
  [HideInInspector]
  [SerializeField]
  private List<BMSymbol> mSymbols = new List<BMSymbol>();
  [HideInInspector]
  [SerializeField]
  private Font mDynamicFont;
  [HideInInspector]
  [SerializeField]
  private int mDynamicFontSize = 16 /*0x10*/;
  [HideInInspector]
  [SerializeField]
  private FontStyle mDynamicFontStyle;
  [NonSerialized]
  private UISpriteData mSprite;
  private int mPMA = -1;
  private int mPacked = -1;

  public BMFont bmFont
  {
    get
    {
      return !Object.op_Inequality((Object) this.mReplacement, (Object) null) ? this.mFont : this.mReplacement.bmFont;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        this.mReplacement.bmFont = value;
      else
        this.mFont = value;
    }
  }

  public int texWidth
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.texWidth;
      return this.mFont == null ? 1 : this.mFont.texWidth;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
      {
        this.mReplacement.texWidth = value;
      }
      else
      {
        if (this.mFont == null)
          return;
        this.mFont.texWidth = value;
      }
    }
  }

  public int texHeight
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.texHeight;
      return this.mFont == null ? 1 : this.mFont.texHeight;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
      {
        this.mReplacement.texHeight = value;
      }
      else
      {
        if (this.mFont == null)
          return;
        this.mFont.texHeight = value;
      }
    }
  }

  public bool hasSymbols
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.hasSymbols;
      return this.mSymbols != null && this.mSymbols.Count != 0;
    }
  }

  public List<BMSymbol> symbols
  {
    get
    {
      return !Object.op_Inequality((Object) this.mReplacement, (Object) null) ? this.mSymbols : this.mReplacement.symbols;
    }
  }

  public UIAtlas atlas
  {
    get
    {
      return !Object.op_Inequality((Object) this.mReplacement, (Object) null) ? this.mAtlas : this.mReplacement.atlas;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
      {
        this.mReplacement.atlas = value;
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mAtlas, (Object) value))
          return;
        this.mPMA = -1;
        this.mAtlas = value;
        if (Object.op_Inequality((Object) this.mAtlas, (Object) null))
        {
          this.mMat = this.mAtlas.spriteMaterial;
          if (this.sprite != null)
            this.mUVRect = this.uvRect;
        }
        this.MarkAsChanged();
      }
    }
  }

  public Material material
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.material;
      if (Object.op_Inequality((Object) this.mAtlas, (Object) null))
        return this.mAtlas.spriteMaterial;
      if (Object.op_Inequality((Object) this.mMat, (Object) null))
      {
        if (Object.op_Inequality((Object) this.mDynamicFont, (Object) null) && Object.op_Inequality((Object) this.mMat, (Object) this.mDynamicFont.material))
          this.mMat.mainTexture = this.mDynamicFont.material.mainTexture;
        return this.mMat;
      }
      return Object.op_Inequality((Object) this.mDynamicFont, (Object) null) ? this.mDynamicFont.material : (Material) null;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
      {
        this.mReplacement.material = value;
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mMat, (Object) value))
          return;
        this.mPMA = -1;
        this.mMat = value;
        this.MarkAsChanged();
      }
    }
  }

  [Obsolete("Use UIFont.premultipliedAlphaShader instead")]
  public bool premultipliedAlpha => this.premultipliedAlphaShader;

  public bool premultipliedAlphaShader
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.premultipliedAlphaShader;
      if (Object.op_Inequality((Object) this.mAtlas, (Object) null))
        return this.mAtlas.premultipliedAlpha;
      if (this.mPMA == -1)
      {
        Material material = this.material;
        this.mPMA = !Object.op_Inequality((Object) material, (Object) null) || !Object.op_Inequality((Object) material.shader, (Object) null) || !((Object) material.shader).name.Contains("Premultiplied") ? 0 : 1;
      }
      return this.mPMA == 1;
    }
  }

  public bool packedFontShader
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.packedFontShader;
      if (Object.op_Inequality((Object) this.mAtlas, (Object) null))
        return false;
      if (this.mPacked == -1)
      {
        Material material = this.material;
        this.mPacked = !Object.op_Inequality((Object) material, (Object) null) || !Object.op_Inequality((Object) material.shader, (Object) null) || !((Object) material.shader).name.Contains("Packed") ? 0 : 1;
      }
      return this.mPacked == 1;
    }
  }

  public Texture2D texture
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.texture;
      Material material = this.material;
      return !Object.op_Inequality((Object) material, (Object) null) ? (Texture2D) null : material.mainTexture as Texture2D;
    }
  }

  public Rect uvRect
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.uvRect;
      return !Object.op_Inequality((Object) this.mAtlas, (Object) null) || this.sprite == null ? new Rect(0.0f, 0.0f, 1f, 1f) : this.mUVRect;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
      {
        this.mReplacement.uvRect = value;
      }
      else
      {
        if (this.sprite != null || !Rect.op_Inequality(this.mUVRect, value))
          return;
        this.mUVRect = value;
        this.MarkAsChanged();
      }
    }
  }

  public string spriteName
  {
    get
    {
      return !Object.op_Inequality((Object) this.mReplacement, (Object) null) ? this.mFont.spriteName : this.mReplacement.spriteName;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
      {
        this.mReplacement.spriteName = value;
      }
      else
      {
        if (!(this.mFont.spriteName != value))
          return;
        this.mFont.spriteName = value;
        this.MarkAsChanged();
      }
    }
  }

  public bool isValid
  {
    get => Object.op_Inequality((Object) this.mDynamicFont, (Object) null) || this.mFont.isValid;
  }

  [Obsolete("Use UIFont.defaultSize instead")]
  public int size
  {
    get => this.defaultSize;
    set => this.defaultSize = value;
  }

  public int defaultSize
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.defaultSize;
      return this.isDynamic || this.mFont == null ? this.mDynamicFontSize : this.mFont.charSize;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        this.mReplacement.defaultSize = value;
      else
        this.mDynamicFontSize = value;
    }
  }

  public UISpriteData sprite
  {
    get
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        return this.mReplacement.sprite;
      if (this.mSprite == null && Object.op_Inequality((Object) this.mAtlas, (Object) null) && !string.IsNullOrEmpty(this.mFont.spriteName))
      {
        this.mSprite = this.mAtlas.GetSprite(this.mFont.spriteName);
        if (this.mSprite == null)
          this.mSprite = this.mAtlas.GetSprite(((Object) this).name);
        if (this.mSprite == null)
          this.mFont.spriteName = (string) null;
        else
          this.UpdateUVRect();
        int index = 0;
        for (int count = this.mSymbols.Count; index < count; ++index)
          this.symbols[index].MarkAsChanged();
      }
      return this.mSprite;
    }
  }

  public UIFont replacement
  {
    get => this.mReplacement;
    set
    {
      UIFont uiFont = value;
      if (Object.op_Equality((Object) uiFont, (Object) this))
        uiFont = (UIFont) null;
      if (!Object.op_Inequality((Object) this.mReplacement, (Object) uiFont))
        return;
      if (Object.op_Inequality((Object) uiFont, (Object) null) && Object.op_Equality((Object) uiFont.replacement, (Object) this))
        uiFont.replacement = (UIFont) null;
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
        this.MarkAsChanged();
      this.mReplacement = uiFont;
      if (Object.op_Inequality((Object) uiFont, (Object) null))
      {
        this.mPMA = -1;
        this.mMat = (Material) null;
        this.mFont = (BMFont) null;
        this.mDynamicFont = (Font) null;
      }
      this.MarkAsChanged();
    }
  }

  public bool isDynamic
  {
    get
    {
      return !Object.op_Inequality((Object) this.mReplacement, (Object) null) ? Object.op_Inequality((Object) this.mDynamicFont, (Object) null) : this.mReplacement.isDynamic;
    }
  }

  public Font dynamicFont
  {
    get
    {
      return !Object.op_Inequality((Object) this.mReplacement, (Object) null) ? this.mDynamicFont : this.mReplacement.dynamicFont;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
      {
        this.mReplacement.dynamicFont = value;
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mDynamicFont, (Object) value))
          return;
        if (Object.op_Inequality((Object) this.mDynamicFont, (Object) null))
          this.material = (Material) null;
        this.mDynamicFont = value;
        this.MarkAsChanged();
      }
    }
  }

  public FontStyle dynamicFontStyle
  {
    get
    {
      return !Object.op_Inequality((Object) this.mReplacement, (Object) null) ? this.mDynamicFontStyle : this.mReplacement.dynamicFontStyle;
    }
    set
    {
      if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
      {
        this.mReplacement.dynamicFontStyle = value;
      }
      else
      {
        if (this.mDynamicFontStyle == value)
          return;
        this.mDynamicFontStyle = value;
        this.MarkAsChanged();
      }
    }
  }

  private void Trim()
  {
    if (!Object.op_Inequality((Object) this.mAtlas.texture, (Object) null) || this.mSprite == null)
      return;
    Rect pixels = NGUIMath.ConvertToPixels(this.mUVRect, ((Texture) this.texture).width, ((Texture) this.texture).height, true);
    Rect rect;
    // ISSUE: explicit constructor call
    ((Rect) ref rect).\u002Ector((float) this.mSprite.x, (float) this.mSprite.y, (float) this.mSprite.width, (float) this.mSprite.height);
    this.mFont.Trim(Mathf.RoundToInt(((Rect) ref rect).xMin - ((Rect) ref pixels).xMin), Mathf.RoundToInt(((Rect) ref rect).yMin - ((Rect) ref pixels).yMin), Mathf.RoundToInt(((Rect) ref rect).xMax - ((Rect) ref pixels).xMin), Mathf.RoundToInt(((Rect) ref rect).yMax - ((Rect) ref pixels).yMin));
  }

  private bool References(UIFont font)
  {
    if (Object.op_Equality((Object) font, (Object) null))
      return false;
    if (Object.op_Equality((Object) font, (Object) this))
      return true;
    return Object.op_Inequality((Object) this.mReplacement, (Object) null) && this.mReplacement.References(font);
  }

  public static bool CheckIfRelated(UIFont a, UIFont b)
  {
    if (Object.op_Equality((Object) a, (Object) null) || Object.op_Equality((Object) b, (Object) null))
      return false;
    return a.isDynamic && b.isDynamic && a.dynamicFont.fontNames[0] == b.dynamicFont.fontNames[0] || Object.op_Equality((Object) a, (Object) b) || a.References(b) || b.References(a);
  }

  private Texture dynamicTexture
  {
    get
    {
      if (Object.op_Implicit((Object) this.mReplacement))
        return this.mReplacement.dynamicTexture;
      return this.isDynamic ? this.mDynamicFont.material.mainTexture : (Texture) null;
    }
  }

  public void MarkAsChanged()
  {
    if (Object.op_Inequality((Object) this.mReplacement, (Object) null))
      this.mReplacement.MarkAsChanged();
    this.mSprite = (UISpriteData) null;
    UILabel[] active = NGUITools.FindActive<UILabel>();
    int index1 = 0;
    for (int length = active.Length; index1 < length; ++index1)
    {
      UILabel uiLabel = active[index1];
      if (((Behaviour) uiLabel).enabled && NGUITools.GetActive(((Component) uiLabel).gameObject) && UIFont.CheckIfRelated(this, uiLabel.bitmapFont))
      {
        UIFont bitmapFont = uiLabel.bitmapFont;
        uiLabel.bitmapFont = (UIFont) null;
        uiLabel.bitmapFont = bitmapFont;
      }
    }
    int index2 = 0;
    for (int count = this.symbols.Count; index2 < count; ++index2)
      this.symbols[index2].MarkAsChanged();
  }

  public void UpdateUVRect()
  {
    if (Object.op_Equality((Object) this.mAtlas, (Object) null))
      return;
    Texture texture = this.mAtlas.texture;
    if (!Object.op_Inequality((Object) texture, (Object) null))
      return;
    this.mUVRect = new Rect((float) (this.mSprite.x - this.mSprite.paddingLeft), (float) (this.mSprite.y - this.mSprite.paddingTop), (float) (this.mSprite.width + this.mSprite.paddingLeft + this.mSprite.paddingRight), (float) (this.mSprite.height + this.mSprite.paddingTop + this.mSprite.paddingBottom));
    this.mUVRect = NGUIMath.ConvertToTexCoords(this.mUVRect, texture.width, texture.height);
    if (!this.mSprite.hasPadding)
      return;
    this.Trim();
  }

  private BMSymbol GetSymbol(string sequence, bool createIfMissing)
  {
    int index = 0;
    for (int count = this.mSymbols.Count; index < count; ++index)
    {
      BMSymbol mSymbol = this.mSymbols[index];
      if (mSymbol.sequence == sequence)
        return mSymbol;
    }
    if (!createIfMissing)
      return (BMSymbol) null;
    BMSymbol symbol = new BMSymbol();
    symbol.sequence = sequence;
    this.mSymbols.Add(symbol);
    return symbol;
  }

  public BMSymbol MatchSymbol(string text, int offset, int textLength)
  {
    int count = this.mSymbols.Count;
    if (count == 0)
      return (BMSymbol) null;
    textLength -= offset;
    for (int index1 = 0; index1 < count; ++index1)
    {
      BMSymbol mSymbol = this.mSymbols[index1];
      int length = mSymbol.length;
      if (length != 0 && textLength >= length)
      {
        bool flag = true;
        for (int index2 = 0; index2 < length; ++index2)
        {
          if ((int) text[offset + index2] != (int) mSymbol.sequence[index2])
          {
            flag = false;
            break;
          }
        }
        if (flag && mSymbol.Validate(this.atlas))
          return mSymbol;
      }
    }
    return (BMSymbol) null;
  }

  public void AddSymbol(string sequence, string spriteName)
  {
    this.GetSymbol(sequence, true).spriteName = spriteName;
    this.MarkAsChanged();
  }

  public void RemoveSymbol(string sequence)
  {
    BMSymbol symbol = this.GetSymbol(sequence, false);
    if (symbol != null)
      this.symbols.Remove(symbol);
    this.MarkAsChanged();
  }

  public void RenameSymbol(string before, string after)
  {
    BMSymbol symbol = this.GetSymbol(before, false);
    if (symbol != null)
      symbol.sequence = after;
    this.MarkAsChanged();
  }

  public bool UsesSprite(string s)
  {
    if (!string.IsNullOrEmpty(s))
    {
      if (s.Equals(this.spriteName))
        return true;
      int index = 0;
      for (int count = this.symbols.Count; index < count; ++index)
      {
        BMSymbol symbol = this.symbols[index];
        if (s.Equals(symbol.spriteName))
          return true;
      }
    }
    return false;
  }
}
