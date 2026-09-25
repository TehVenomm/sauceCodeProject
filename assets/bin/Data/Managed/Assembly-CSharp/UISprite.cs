// Decompiled with JetBrains decompiler
// Type: UISprite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Sprite")]
public class UISprite : UIBasicSprite
{
  [HideInInspector]
  [SerializeField]
  private UIAtlas mAtlas;
  [HideInInspector]
  [SerializeField]
  private string mSpriteName;
  [HideInInspector]
  [SerializeField]
  private bool mFillCenter = true;
  [NonSerialized]
  protected UISpriteData mSprite;
  [NonSerialized]
  private bool mSpriteSet;

  public override Material material
  {
    get
    {
      return !Object.op_Inequality((Object) this.mAtlas, (Object) null) ? (Material) null : this.mAtlas.spriteMaterial;
    }
  }

  public UIAtlas atlas
  {
    get => this.mAtlas;
    set
    {
      if (!Object.op_Inequality((Object) this.mAtlas, (Object) value))
        return;
      this.RemoveFromPanel();
      this.mAtlas = value;
      this.mSpriteSet = false;
      this.mSprite = (UISpriteData) null;
      if (string.IsNullOrEmpty(this.mSpriteName) && Object.op_Inequality((Object) this.mAtlas, (Object) null) && this.mAtlas.spriteList.Count > 0)
      {
        this.SetAtlasSprite(this.mAtlas.spriteList[0]);
        this.mSpriteName = this.mSprite.name;
      }
      if (string.IsNullOrEmpty(this.mSpriteName))
        return;
      string mSpriteName = this.mSpriteName;
      this.mSpriteName = "";
      this.spriteName = mSpriteName;
      this.MarkAsChanged();
    }
  }

  public string spriteName
  {
    get => this.mSpriteName;
    set
    {
      if (string.IsNullOrEmpty(value))
      {
        if (string.IsNullOrEmpty(this.mSpriteName))
          return;
        this.mSpriteName = "";
        this.mSprite = (UISpriteData) null;
        this.mChanged = true;
        this.mSpriteSet = false;
      }
      else
      {
        if (!(this.mSpriteName != value))
          return;
        this.mSpriteName = value;
        this.mSprite = (UISpriteData) null;
        this.mChanged = true;
        this.mSpriteSet = false;
      }
    }
  }

  public bool isValid => this.GetAtlasSprite() != null;

  [Obsolete("Use 'centerType' instead")]
  public bool fillCenter
  {
    get => this.centerType != 0;
    set
    {
      if (value == (this.centerType != 0))
        return;
      this.centerType = value ? UIBasicSprite.AdvancedType.Sliced : UIBasicSprite.AdvancedType.Invisible;
      this.MarkAsChanged();
    }
  }

  public override Vector4 border
  {
    get
    {
      UISpriteData atlasSprite = this.GetAtlasSprite();
      return atlasSprite == null ? base.border : new Vector4((float) atlasSprite.borderLeft, (float) atlasSprite.borderBottom, (float) atlasSprite.borderRight, (float) atlasSprite.borderTop);
    }
  }

  public override float pixelSize
  {
    get => !Object.op_Inequality((Object) this.mAtlas, (Object) null) ? 1f : this.mAtlas.pixelSize;
  }

  public override int minWidth
  {
    get
    {
      if (this.type != UIBasicSprite.Type.Sliced && this.type != UIBasicSprite.Type.Advanced)
        return base.minWidth;
      float pixelSize = this.pixelSize;
      Vector4 vector4 = Vector4.op_Multiply(this.border, this.pixelSize);
      int num = Mathf.RoundToInt(vector4.x + vector4.z);
      UISpriteData atlasSprite = this.GetAtlasSprite();
      if (atlasSprite != null)
        num += Mathf.RoundToInt(pixelSize * (float) (atlasSprite.paddingLeft + atlasSprite.paddingRight));
      return Mathf.Max(base.minWidth, (num & 1) == 1 ? num + 1 : num);
    }
  }

  public override int minHeight
  {
    get
    {
      if (this.type != UIBasicSprite.Type.Sliced && this.type != UIBasicSprite.Type.Advanced)
        return base.minHeight;
      Vector4 vector4 = Vector4.op_Multiply(this.border, this.pixelSize);
      int num = Mathf.RoundToInt(vector4.y + vector4.w);
      UISpriteData atlasSprite = this.GetAtlasSprite();
      if (atlasSprite != null)
        num += atlasSprite.paddingTop + atlasSprite.paddingBottom;
      return Mathf.Max(base.minHeight, (num & 1) == 1 ? num + 1 : num);
    }
  }

  public override Vector4 drawingDimensions
  {
    get
    {
      Vector2 pivotOffset = this.pivotOffset;
      float num1 = -pivotOffset.x * (float) this.mWidth;
      float num2 = -pivotOffset.y * (float) this.mHeight;
      float num3 = num1 + (float) this.mWidth;
      float num4 = num2 + (float) this.mHeight;
      if (this.GetAtlasSprite() != null && this.mType != UIBasicSprite.Type.Tiled)
      {
        int paddingLeft = this.mSprite.paddingLeft;
        int paddingBottom = this.mSprite.paddingBottom;
        int paddingRight = this.mSprite.paddingRight;
        int paddingTop = this.mSprite.paddingTop;
        float pixelSize = this.pixelSize;
        if ((double) pixelSize != 1.0)
        {
          paddingLeft = Mathf.RoundToInt(pixelSize * (float) paddingLeft);
          paddingBottom = Mathf.RoundToInt(pixelSize * (float) paddingBottom);
          paddingRight = Mathf.RoundToInt(pixelSize * (float) paddingRight);
          paddingTop = Mathf.RoundToInt(pixelSize * (float) paddingTop);
        }
        int num5 = this.mSprite.width + paddingLeft + paddingRight;
        int num6 = this.mSprite.height + paddingBottom + paddingTop;
        float num7 = 1f;
        float num8 = 1f;
        if (num5 > 0 && num6 > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
        {
          if ((num5 & 1) != 0)
            ++paddingRight;
          if ((num6 & 1) != 0)
            ++paddingTop;
          num7 = 1f / (float) num5 * (float) this.mWidth;
          num8 = 1f / (float) num6 * (float) this.mHeight;
        }
        if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
        {
          num1 += (float) paddingRight * num7;
          num3 -= (float) paddingLeft * num7;
        }
        else
        {
          num1 += (float) paddingLeft * num7;
          num3 -= (float) paddingRight * num7;
        }
        if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
        {
          num2 += (float) paddingTop * num8;
          num4 -= (float) paddingBottom * num8;
        }
        else
        {
          num2 += (float) paddingBottom * num8;
          num4 -= (float) paddingTop * num8;
        }
      }
      Vector4 vector4 = Object.op_Inequality((Object) this.mAtlas, (Object) null) ? Vector4.op_Multiply(this.border, this.pixelSize) : Vector4.zero;
      float num9 = vector4.x + vector4.z;
      float num10 = vector4.y + vector4.w;
      double num11 = (double) Mathf.Lerp(num1, num3 - num9, this.mDrawRegion.x);
      float num12 = Mathf.Lerp(num2, num4 - num10, this.mDrawRegion.y);
      float num13 = Mathf.Lerp(num1 + num9, num3, this.mDrawRegion.z);
      float num14 = Mathf.Lerp(num2 + num10, num4, this.mDrawRegion.w);
      double num15 = (double) num12;
      double num16 = (double) num13;
      double num17 = (double) num14;
      return new Vector4((float) num11, (float) num15, (float) num16, (float) num17);
    }
  }

  public override bool premultipliedAlpha
  {
    get
    {
      return Object.op_Inequality((Object) this.mAtlas, (Object) null) && this.mAtlas.premultipliedAlpha;
    }
  }

  public UISpriteData GetAtlasSprite()
  {
    if (!this.mSpriteSet)
      this.mSprite = (UISpriteData) null;
    if (this.mSprite == null && Object.op_Inequality((Object) this.mAtlas, (Object) null))
    {
      if (!string.IsNullOrEmpty(this.mSpriteName))
      {
        UISpriteData sprite = this.mAtlas.GetSprite(this.mSpriteName);
        if (sprite == null)
          return (UISpriteData) null;
        this.SetAtlasSprite(sprite);
      }
      if (this.mSprite == null && this.mAtlas.spriteList.Count > 0)
      {
        UISpriteData sprite = this.mAtlas.spriteList[0];
        if (sprite == null)
          return (UISpriteData) null;
        this.SetAtlasSprite(sprite);
        if (this.mSprite == null)
        {
          Debug.LogError((object) (((Object) this.mAtlas).name + " seems to have a null sprite!"));
          return (UISpriteData) null;
        }
        this.mSpriteName = this.mSprite.name;
      }
    }
    return this.mSprite;
  }

  protected void SetAtlasSprite(UISpriteData sp)
  {
    this.mChanged = true;
    this.mSpriteSet = true;
    if (sp != null)
    {
      this.mSprite = sp;
      this.mSpriteName = this.mSprite.name;
    }
    else
    {
      this.mSpriteName = this.mSprite != null ? this.mSprite.name : "";
      this.mSprite = sp;
    }
  }

  public override void MakePixelPerfect()
  {
    if (!this.isValid)
      return;
    base.MakePixelPerfect();
    if (this.mType == UIBasicSprite.Type.Tiled)
      return;
    UISpriteData atlasSprite = this.GetAtlasSprite();
    if (atlasSprite == null)
      return;
    Texture mainTexture = this.mainTexture;
    if (Object.op_Equality((Object) mainTexture, (Object) null) || this.mType != UIBasicSprite.Type.Simple && this.mType != UIBasicSprite.Type.Filled && atlasSprite.hasBorder || !Object.op_Inequality((Object) mainTexture, (Object) null))
      return;
    int num1 = Mathf.RoundToInt(this.pixelSize * (float) (atlasSprite.width + atlasSprite.paddingLeft + atlasSprite.paddingRight));
    int num2 = Mathf.RoundToInt(this.pixelSize * (float) (atlasSprite.height + atlasSprite.paddingTop + atlasSprite.paddingBottom));
    if ((num1 & 1) == 1)
      ++num1;
    if ((num2 & 1) == 1)
      ++num2;
    this.width = num1;
    this.height = num2;
  }

  protected override void OnInit()
  {
    if (!this.mFillCenter)
    {
      this.mFillCenter = true;
      this.centerType = UIBasicSprite.AdvancedType.Invisible;
    }
    base.OnInit();
  }

  protected override void OnUpdate()
  {
    base.OnUpdate();
    if (!this.mChanged && this.mSpriteSet)
      return;
    this.mSpriteSet = true;
    this.mSprite = (UISpriteData) null;
    this.mChanged = true;
  }

  public override void OnFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    Texture mainTexture = this.mainTexture;
    if (Object.op_Equality((Object) mainTexture, (Object) null))
      return;
    if (this.mSprite == null)
      this.mSprite = this.atlas.GetSprite(this.spriteName);
    if (this.mSprite == null)
      return;
    Rect rect1;
    // ISSUE: explicit constructor call
    ((Rect) ref rect1).\u002Ector((float) this.mSprite.x, (float) this.mSprite.y, (float) this.mSprite.width, (float) this.mSprite.height);
    Rect rect2;
    // ISSUE: explicit constructor call
    ((Rect) ref rect2).\u002Ector((float) (this.mSprite.x + this.mSprite.borderLeft), (float) (this.mSprite.y + this.mSprite.borderTop), (float) (this.mSprite.width - this.mSprite.borderLeft - this.mSprite.borderRight), (float) (this.mSprite.height - this.mSprite.borderBottom - this.mSprite.borderTop));
    Rect texCoords1 = NGUIMath.ConvertToTexCoords(rect1, mainTexture.width, mainTexture.height);
    Rect texCoords2 = NGUIMath.ConvertToTexCoords(rect2, mainTexture.width, mainTexture.height);
    int size = verts.size;
    this.Fill(verts, uvs, cols, texCoords1, texCoords2);
    if (this.onPostFill == null)
      return;
    this.onPostFill((UIWidget) this, size, verts, uvs, cols);
  }
}
