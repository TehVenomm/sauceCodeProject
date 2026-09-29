// Decompiled with JetBrains decompiler
// Type: UIBasicSprite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public abstract class UIBasicSprite : UIWidget
{
  [HideInInspector]
  [SerializeField]
  protected UIBasicSprite.Type mType;
  [HideInInspector]
  [SerializeField]
  protected UIBasicSprite.FillDirection mFillDirection = UIBasicSprite.FillDirection.Radial360;
  [Range(0.0f, 1f)]
  [HideInInspector]
  [SerializeField]
  protected float mFillAmount = 1f;
  [HideInInspector]
  [SerializeField]
  protected bool mInvert;
  [HideInInspector]
  [SerializeField]
  protected UIBasicSprite.Flip mFlip;
  [NonSerialized]
  private Rect mInnerUV;
  [NonSerialized]
  private Rect mOuterUV;
  public UIBasicSprite.AdvancedType centerType = UIBasicSprite.AdvancedType.Sliced;
  public UIBasicSprite.AdvancedType leftType = UIBasicSprite.AdvancedType.Sliced;
  public UIBasicSprite.AdvancedType rightType = UIBasicSprite.AdvancedType.Sliced;
  public UIBasicSprite.AdvancedType bottomType = UIBasicSprite.AdvancedType.Sliced;
  public UIBasicSprite.AdvancedType topType = UIBasicSprite.AdvancedType.Sliced;
  protected static Vector2[] mTempPos = new Vector2[4];
  protected static Vector2[] mTempUVs = new Vector2[4];

  public virtual UIBasicSprite.Type type
  {
    get => this.mType;
    set
    {
      if (this.mType == value)
        return;
      this.mType = value;
      this.MarkAsChanged();
    }
  }

  public UIBasicSprite.Flip flip
  {
    get => this.mFlip;
    set
    {
      if (this.mFlip == value)
        return;
      this.mFlip = value;
      this.MarkAsChanged();
    }
  }

  public UIBasicSprite.FillDirection fillDirection
  {
    get => this.mFillDirection;
    set
    {
      if (this.mFillDirection == value)
        return;
      this.mFillDirection = value;
      this.mChanged = true;
    }
  }

  public float fillAmount
  {
    get => this.mFillAmount;
    set
    {
      float num = Mathf.Clamp01(value);
      if ((double) this.mFillAmount == (double) num)
        return;
      this.mFillAmount = num;
      this.mChanged = true;
    }
  }

  public override int minWidth
  {
    get
    {
      if (this.type != UIBasicSprite.Type.Sliced && this.type != UIBasicSprite.Type.Advanced)
        return base.minWidth;
      Vector4 vector4 = Vector4.op_Multiply(this.border, this.pixelSize);
      int num = Mathf.RoundToInt(vector4.x + vector4.z);
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
      return Mathf.Max(base.minHeight, (num & 1) == 1 ? num + 1 : num);
    }
  }

  public bool invert
  {
    get => this.mInvert;
    set
    {
      if (this.mInvert == value)
        return;
      this.mInvert = value;
      this.mChanged = true;
    }
  }

  public bool hasBorder
  {
    get
    {
      Vector4 border = this.border;
      return (double) border.x != 0.0 || (double) border.y != 0.0 || (double) border.z != 0.0 || (double) border.w != 0.0;
    }
  }

  public virtual bool premultipliedAlpha => false;

  public virtual float pixelSize => 1f;

  private Vector4 drawingUVs
  {
    get
    {
      switch (this.mFlip)
      {
        case UIBasicSprite.Flip.Horizontally:
          return new Vector4(((Rect) ref this.mOuterUV).xMax, ((Rect) ref this.mOuterUV).yMin, ((Rect) ref this.mOuterUV).xMin, ((Rect) ref this.mOuterUV).yMax);
        case UIBasicSprite.Flip.Vertically:
          return new Vector4(((Rect) ref this.mOuterUV).xMin, ((Rect) ref this.mOuterUV).yMax, ((Rect) ref this.mOuterUV).xMax, ((Rect) ref this.mOuterUV).yMin);
        case UIBasicSprite.Flip.Both:
          return new Vector4(((Rect) ref this.mOuterUV).xMax, ((Rect) ref this.mOuterUV).yMax, ((Rect) ref this.mOuterUV).xMin, ((Rect) ref this.mOuterUV).yMin);
        default:
          return new Vector4(((Rect) ref this.mOuterUV).xMin, ((Rect) ref this.mOuterUV).yMin, ((Rect) ref this.mOuterUV).xMax, ((Rect) ref this.mOuterUV).yMax);
      }
    }
  }

  private Color32 drawingColor
  {
    get
    {
      Color c = this.color;
      c.a = this.finalAlpha;
      if (this.premultipliedAlpha)
        c = NGUITools.ApplyPMA(c);
      if (QualitySettings.activeColorSpace == 1)
      {
        c.r = Mathf.GammaToLinearSpace(c.r);
        c.g = Mathf.GammaToLinearSpace(c.g);
        c.b = Mathf.GammaToLinearSpace(c.b);
      }
      return Color32.op_Implicit(c);
    }
  }

  protected void Fill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols,
    Rect outer,
    Rect inner)
  {
    this.mOuterUV = outer;
    this.mInnerUV = inner;
    switch (this.type)
    {
      case UIBasicSprite.Type.Simple:
        this.SimpleFill(verts, uvs, cols);
        break;
      case UIBasicSprite.Type.Sliced:
        this.SlicedFill(verts, uvs, cols);
        break;
      case UIBasicSprite.Type.Tiled:
        this.TiledFill(verts, uvs, cols);
        break;
      case UIBasicSprite.Type.Filled:
        this.FilledFill(verts, uvs, cols);
        break;
      case UIBasicSprite.Type.Advanced:
        this.AdvancedFill(verts, uvs, cols);
        break;
    }
  }

  private void SimpleFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    Vector4 drawingDimensions = this.drawingDimensions;
    Vector4 drawingUvs = this.drawingUVs;
    Color32 drawingColor = this.drawingColor;
    verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.y));
    verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.w));
    verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.w));
    verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.y));
    uvs.Add(new Vector2(drawingUvs.x, drawingUvs.y));
    uvs.Add(new Vector2(drawingUvs.x, drawingUvs.w));
    uvs.Add(new Vector2(drawingUvs.z, drawingUvs.w));
    uvs.Add(new Vector2(drawingUvs.z, drawingUvs.y));
    cols.Add(drawingColor);
    cols.Add(drawingColor);
    cols.Add(drawingColor);
    cols.Add(drawingColor);
  }

  private void SlicedFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    Vector4 vector4 = Vector4.op_Multiply(this.border, this.pixelSize);
    if ((double) vector4.x == 0.0 && (double) vector4.y == 0.0 && (double) vector4.z == 0.0 && (double) vector4.w == 0.0)
    {
      this.SimpleFill(verts, uvs, cols);
    }
    else
    {
      Color32 drawingColor = this.drawingColor;
      Vector4 drawingDimensions = this.drawingDimensions;
      UIBasicSprite.mTempPos[0].x = drawingDimensions.x;
      UIBasicSprite.mTempPos[0].y = drawingDimensions.y;
      UIBasicSprite.mTempPos[3].x = drawingDimensions.z;
      UIBasicSprite.mTempPos[3].y = drawingDimensions.w;
      if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
      {
        UIBasicSprite.mTempPos[1].x = UIBasicSprite.mTempPos[0].x + vector4.z;
        UIBasicSprite.mTempPos[2].x = UIBasicSprite.mTempPos[3].x - vector4.x;
        UIBasicSprite.mTempUVs[3].x = ((Rect) ref this.mOuterUV).xMin;
        UIBasicSprite.mTempUVs[2].x = ((Rect) ref this.mInnerUV).xMin;
        UIBasicSprite.mTempUVs[1].x = ((Rect) ref this.mInnerUV).xMax;
        UIBasicSprite.mTempUVs[0].x = ((Rect) ref this.mOuterUV).xMax;
      }
      else
      {
        UIBasicSprite.mTempPos[1].x = UIBasicSprite.mTempPos[0].x + vector4.x;
        UIBasicSprite.mTempPos[2].x = UIBasicSprite.mTempPos[3].x - vector4.z;
        UIBasicSprite.mTempUVs[0].x = ((Rect) ref this.mOuterUV).xMin;
        UIBasicSprite.mTempUVs[1].x = ((Rect) ref this.mInnerUV).xMin;
        UIBasicSprite.mTempUVs[2].x = ((Rect) ref this.mInnerUV).xMax;
        UIBasicSprite.mTempUVs[3].x = ((Rect) ref this.mOuterUV).xMax;
      }
      if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
      {
        UIBasicSprite.mTempPos[1].y = UIBasicSprite.mTempPos[0].y + vector4.w;
        UIBasicSprite.mTempPos[2].y = UIBasicSprite.mTempPos[3].y - vector4.y;
        UIBasicSprite.mTempUVs[3].y = ((Rect) ref this.mOuterUV).yMin;
        UIBasicSprite.mTempUVs[2].y = ((Rect) ref this.mInnerUV).yMin;
        UIBasicSprite.mTempUVs[1].y = ((Rect) ref this.mInnerUV).yMax;
        UIBasicSprite.mTempUVs[0].y = ((Rect) ref this.mOuterUV).yMax;
      }
      else
      {
        UIBasicSprite.mTempPos[1].y = UIBasicSprite.mTempPos[0].y + vector4.y;
        UIBasicSprite.mTempPos[2].y = UIBasicSprite.mTempPos[3].y - vector4.w;
        UIBasicSprite.mTempUVs[0].y = ((Rect) ref this.mOuterUV).yMin;
        UIBasicSprite.mTempUVs[1].y = ((Rect) ref this.mInnerUV).yMin;
        UIBasicSprite.mTempUVs[2].y = ((Rect) ref this.mInnerUV).yMax;
        UIBasicSprite.mTempUVs[3].y = ((Rect) ref this.mOuterUV).yMax;
      }
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = index1 + 1;
        for (int index3 = 0; index3 < 3; ++index3)
        {
          if (this.centerType != UIBasicSprite.AdvancedType.Invisible || index1 != 1 || index3 != 1)
          {
            int index4 = index3 + 1;
            verts.Add(new Vector3(UIBasicSprite.mTempPos[index1].x, UIBasicSprite.mTempPos[index3].y));
            verts.Add(new Vector3(UIBasicSprite.mTempPos[index1].x, UIBasicSprite.mTempPos[index4].y));
            verts.Add(new Vector3(UIBasicSprite.mTempPos[index2].x, UIBasicSprite.mTempPos[index4].y));
            verts.Add(new Vector3(UIBasicSprite.mTempPos[index2].x, UIBasicSprite.mTempPos[index3].y));
            uvs.Add(new Vector2(UIBasicSprite.mTempUVs[index1].x, UIBasicSprite.mTempUVs[index3].y));
            uvs.Add(new Vector2(UIBasicSprite.mTempUVs[index1].x, UIBasicSprite.mTempUVs[index4].y));
            uvs.Add(new Vector2(UIBasicSprite.mTempUVs[index2].x, UIBasicSprite.mTempUVs[index4].y));
            uvs.Add(new Vector2(UIBasicSprite.mTempUVs[index2].x, UIBasicSprite.mTempUVs[index3].y));
            cols.Add(drawingColor);
            cols.Add(drawingColor);
            cols.Add(drawingColor);
            cols.Add(drawingColor);
          }
        }
      }
    }
  }

  private void TiledFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    Texture mainTexture = this.mainTexture;
    if (Object.op_Equality((Object) mainTexture, (Object) null))
      return;
    Vector2 vector2_1;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_1).\u002Ector(((Rect) ref this.mInnerUV).width * (float) mainTexture.width, ((Rect) ref this.mInnerUV).height * (float) mainTexture.height);
    Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, this.pixelSize);
    if (Object.op_Equality((Object) mainTexture, (Object) null) || (double) vector2_2.x < 2.0 || (double) vector2_2.y < 2.0)
      return;
    Color32 drawingColor = this.drawingColor;
    Vector4 drawingDimensions = this.drawingDimensions;
    Vector4 vector4;
    if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
    {
      vector4.x = ((Rect) ref this.mInnerUV).xMax;
      vector4.z = ((Rect) ref this.mInnerUV).xMin;
    }
    else
    {
      vector4.x = ((Rect) ref this.mInnerUV).xMin;
      vector4.z = ((Rect) ref this.mInnerUV).xMax;
    }
    if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
    {
      vector4.y = ((Rect) ref this.mInnerUV).yMax;
      vector4.w = ((Rect) ref this.mInnerUV).yMin;
    }
    else
    {
      vector4.y = ((Rect) ref this.mInnerUV).yMin;
      vector4.w = ((Rect) ref this.mInnerUV).yMax;
    }
    float x1 = drawingDimensions.x;
    float y1 = drawingDimensions.y;
    float x2 = vector4.x;
    float y2 = vector4.y;
    for (; (double) y1 < (double) drawingDimensions.w; y1 += vector2_2.y)
    {
      float x3 = drawingDimensions.x;
      float num1 = y1 + vector2_2.y;
      float num2 = vector4.w;
      if ((double) num1 > (double) drawingDimensions.w)
      {
        num2 = Mathf.Lerp(vector4.y, vector4.w, (drawingDimensions.w - y1) / vector2_2.y);
        num1 = drawingDimensions.w;
      }
      for (; (double) x3 < (double) drawingDimensions.z; x3 += vector2_2.x)
      {
        float num3 = x3 + vector2_2.x;
        float num4 = vector4.z;
        if ((double) num3 > (double) drawingDimensions.z)
        {
          num4 = Mathf.Lerp(vector4.x, vector4.z, (drawingDimensions.z - x3) / vector2_2.x);
          num3 = drawingDimensions.z;
        }
        verts.Add(new Vector3(x3, y1));
        verts.Add(new Vector3(x3, num1));
        verts.Add(new Vector3(num3, num1));
        verts.Add(new Vector3(num3, y1));
        uvs.Add(new Vector2(x2, y2));
        uvs.Add(new Vector2(x2, num2));
        uvs.Add(new Vector2(num4, num2));
        uvs.Add(new Vector2(num4, y2));
        cols.Add(drawingColor);
        cols.Add(drawingColor);
        cols.Add(drawingColor);
        cols.Add(drawingColor);
      }
    }
  }

  private void FilledFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    if ((double) this.mFillAmount < 1.0 / 1000.0)
      return;
    Vector4 drawingDimensions = this.drawingDimensions;
    Vector4 drawingUvs = this.drawingUVs;
    Color32 drawingColor = this.drawingColor;
    if (this.mFillDirection == UIBasicSprite.FillDirection.Horizontal || this.mFillDirection == UIBasicSprite.FillDirection.Vertical)
    {
      if (this.mFillDirection == UIBasicSprite.FillDirection.Horizontal)
      {
        float num = (drawingUvs.z - drawingUvs.x) * this.mFillAmount;
        if (this.mInvert)
        {
          drawingDimensions.x = drawingDimensions.z - (drawingDimensions.z - drawingDimensions.x) * this.mFillAmount;
          drawingUvs.x = drawingUvs.z - num;
        }
        else
        {
          drawingDimensions.z = drawingDimensions.x + (drawingDimensions.z - drawingDimensions.x) * this.mFillAmount;
          drawingUvs.z = drawingUvs.x + num;
        }
      }
      else if (this.mFillDirection == UIBasicSprite.FillDirection.Vertical)
      {
        float num = (drawingUvs.w - drawingUvs.y) * this.mFillAmount;
        if (this.mInvert)
        {
          drawingDimensions.y = drawingDimensions.w - (drawingDimensions.w - drawingDimensions.y) * this.mFillAmount;
          drawingUvs.y = drawingUvs.w - num;
        }
        else
        {
          drawingDimensions.w = drawingDimensions.y + (drawingDimensions.w - drawingDimensions.y) * this.mFillAmount;
          drawingUvs.w = drawingUvs.y + num;
        }
      }
    }
    UIBasicSprite.mTempPos[0] = new Vector2(drawingDimensions.x, drawingDimensions.y);
    UIBasicSprite.mTempPos[1] = new Vector2(drawingDimensions.x, drawingDimensions.w);
    UIBasicSprite.mTempPos[2] = new Vector2(drawingDimensions.z, drawingDimensions.w);
    UIBasicSprite.mTempPos[3] = new Vector2(drawingDimensions.z, drawingDimensions.y);
    UIBasicSprite.mTempUVs[0] = new Vector2(drawingUvs.x, drawingUvs.y);
    UIBasicSprite.mTempUVs[1] = new Vector2(drawingUvs.x, drawingUvs.w);
    UIBasicSprite.mTempUVs[2] = new Vector2(drawingUvs.z, drawingUvs.w);
    UIBasicSprite.mTempUVs[3] = new Vector2(drawingUvs.z, drawingUvs.y);
    if ((double) this.mFillAmount < 1.0)
    {
      if (this.mFillDirection == UIBasicSprite.FillDirection.Radial90)
      {
        if (!UIBasicSprite.RadialCut(UIBasicSprite.mTempPos, UIBasicSprite.mTempUVs, this.mFillAmount, this.mInvert, 0))
          return;
        for (int index = 0; index < 4; ++index)
        {
          verts.Add(Vector2.op_Implicit(UIBasicSprite.mTempPos[index]));
          uvs.Add(UIBasicSprite.mTempUVs[index]);
          cols.Add(drawingColor);
        }
        return;
      }
      if (this.mFillDirection == UIBasicSprite.FillDirection.Radial180)
      {
        for (int index1 = 0; index1 < 2; ++index1)
        {
          float num1 = 0.0f;
          float num2 = 1f;
          float num3;
          float num4;
          if (index1 == 0)
          {
            num3 = 0.0f;
            num4 = 0.5f;
          }
          else
          {
            num3 = 0.5f;
            num4 = 1f;
          }
          UIBasicSprite.mTempPos[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num3);
          UIBasicSprite.mTempPos[1].x = UIBasicSprite.mTempPos[0].x;
          UIBasicSprite.mTempPos[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num4);
          UIBasicSprite.mTempPos[3].x = UIBasicSprite.mTempPos[2].x;
          UIBasicSprite.mTempPos[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num1);
          UIBasicSprite.mTempPos[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num2);
          UIBasicSprite.mTempPos[2].y = UIBasicSprite.mTempPos[1].y;
          UIBasicSprite.mTempPos[3].y = UIBasicSprite.mTempPos[0].y;
          UIBasicSprite.mTempUVs[0].x = Mathf.Lerp(drawingUvs.x, drawingUvs.z, num3);
          UIBasicSprite.mTempUVs[1].x = UIBasicSprite.mTempUVs[0].x;
          UIBasicSprite.mTempUVs[2].x = Mathf.Lerp(drawingUvs.x, drawingUvs.z, num4);
          UIBasicSprite.mTempUVs[3].x = UIBasicSprite.mTempUVs[2].x;
          UIBasicSprite.mTempUVs[0].y = Mathf.Lerp(drawingUvs.y, drawingUvs.w, num1);
          UIBasicSprite.mTempUVs[1].y = Mathf.Lerp(drawingUvs.y, drawingUvs.w, num2);
          UIBasicSprite.mTempUVs[2].y = UIBasicSprite.mTempUVs[1].y;
          UIBasicSprite.mTempUVs[3].y = UIBasicSprite.mTempUVs[0].y;
          float num5 = !this.mInvert ? this.fillAmount * 2f - (float) index1 : this.mFillAmount * 2f - (float) (1 - index1);
          if (UIBasicSprite.RadialCut(UIBasicSprite.mTempPos, UIBasicSprite.mTempUVs, Mathf.Clamp01(num5), !this.mInvert, NGUIMath.RepeatIndex(index1 + 3, 4)))
          {
            for (int index2 = 0; index2 < 4; ++index2)
            {
              verts.Add(Vector2.op_Implicit(UIBasicSprite.mTempPos[index2]));
              uvs.Add(UIBasicSprite.mTempUVs[index2]);
              cols.Add(drawingColor);
            }
          }
        }
        return;
      }
      if (this.mFillDirection == UIBasicSprite.FillDirection.Radial360)
      {
        for (int index3 = 0; index3 < 4; ++index3)
        {
          float num6;
          float num7;
          if (index3 < 2)
          {
            num6 = 0.0f;
            num7 = 0.5f;
          }
          else
          {
            num6 = 0.5f;
            num7 = 1f;
          }
          float num8;
          float num9;
          if (index3 == 0 || index3 == 3)
          {
            num8 = 0.0f;
            num9 = 0.5f;
          }
          else
          {
            num8 = 0.5f;
            num9 = 1f;
          }
          UIBasicSprite.mTempPos[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num6);
          UIBasicSprite.mTempPos[1].x = UIBasicSprite.mTempPos[0].x;
          UIBasicSprite.mTempPos[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num7);
          UIBasicSprite.mTempPos[3].x = UIBasicSprite.mTempPos[2].x;
          UIBasicSprite.mTempPos[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num8);
          UIBasicSprite.mTempPos[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num9);
          UIBasicSprite.mTempPos[2].y = UIBasicSprite.mTempPos[1].y;
          UIBasicSprite.mTempPos[3].y = UIBasicSprite.mTempPos[0].y;
          UIBasicSprite.mTempUVs[0].x = Mathf.Lerp(drawingUvs.x, drawingUvs.z, num6);
          UIBasicSprite.mTempUVs[1].x = UIBasicSprite.mTempUVs[0].x;
          UIBasicSprite.mTempUVs[2].x = Mathf.Lerp(drawingUvs.x, drawingUvs.z, num7);
          UIBasicSprite.mTempUVs[3].x = UIBasicSprite.mTempUVs[2].x;
          UIBasicSprite.mTempUVs[0].y = Mathf.Lerp(drawingUvs.y, drawingUvs.w, num8);
          UIBasicSprite.mTempUVs[1].y = Mathf.Lerp(drawingUvs.y, drawingUvs.w, num9);
          UIBasicSprite.mTempUVs[2].y = UIBasicSprite.mTempUVs[1].y;
          UIBasicSprite.mTempUVs[3].y = UIBasicSprite.mTempUVs[0].y;
          float num10 = this.mInvert ? this.mFillAmount * 4f - (float) NGUIMath.RepeatIndex(index3 + 2, 4) : this.mFillAmount * 4f - (float) (3 - NGUIMath.RepeatIndex(index3 + 2, 4));
          if (UIBasicSprite.RadialCut(UIBasicSprite.mTempPos, UIBasicSprite.mTempUVs, Mathf.Clamp01(num10), this.mInvert, NGUIMath.RepeatIndex(index3 + 2, 4)))
          {
            for (int index4 = 0; index4 < 4; ++index4)
            {
              verts.Add(Vector2.op_Implicit(UIBasicSprite.mTempPos[index4]));
              uvs.Add(UIBasicSprite.mTempUVs[index4]);
              cols.Add(drawingColor);
            }
          }
        }
        return;
      }
    }
    for (int index = 0; index < 4; ++index)
    {
      verts.Add(Vector2.op_Implicit(UIBasicSprite.mTempPos[index]));
      uvs.Add(UIBasicSprite.mTempUVs[index]);
      cols.Add(drawingColor);
    }
  }

  private void AdvancedFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    Texture mainTexture = this.mainTexture;
    if (Object.op_Equality((Object) mainTexture, (Object) null))
      return;
    Vector4 vector4 = Vector4.op_Multiply(this.border, this.pixelSize);
    if ((double) vector4.x == 0.0 && (double) vector4.y == 0.0 && (double) vector4.z == 0.0 && (double) vector4.w == 0.0)
    {
      this.SimpleFill(verts, uvs, cols);
    }
    else
    {
      Color32 drawingColor = this.drawingColor;
      Vector4 drawingDimensions = this.drawingDimensions;
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(((Rect) ref this.mInnerUV).width * (float) mainTexture.width, ((Rect) ref this.mInnerUV).height * (float) mainTexture.height);
      vector2 = Vector2.op_Multiply(vector2, this.pixelSize);
      if ((double) vector2.x < 1.0)
        vector2.x = 1f;
      if ((double) vector2.y < 1.0)
        vector2.y = 1f;
      UIBasicSprite.mTempPos[0].x = drawingDimensions.x;
      UIBasicSprite.mTempPos[0].y = drawingDimensions.y;
      UIBasicSprite.mTempPos[3].x = drawingDimensions.z;
      UIBasicSprite.mTempPos[3].y = drawingDimensions.w;
      if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
      {
        UIBasicSprite.mTempPos[1].x = UIBasicSprite.mTempPos[0].x + vector4.z;
        UIBasicSprite.mTempPos[2].x = UIBasicSprite.mTempPos[3].x - vector4.x;
        UIBasicSprite.mTempUVs[3].x = ((Rect) ref this.mOuterUV).xMin;
        UIBasicSprite.mTempUVs[2].x = ((Rect) ref this.mInnerUV).xMin;
        UIBasicSprite.mTempUVs[1].x = ((Rect) ref this.mInnerUV).xMax;
        UIBasicSprite.mTempUVs[0].x = ((Rect) ref this.mOuterUV).xMax;
      }
      else
      {
        UIBasicSprite.mTempPos[1].x = UIBasicSprite.mTempPos[0].x + vector4.x;
        UIBasicSprite.mTempPos[2].x = UIBasicSprite.mTempPos[3].x - vector4.z;
        UIBasicSprite.mTempUVs[0].x = ((Rect) ref this.mOuterUV).xMin;
        UIBasicSprite.mTempUVs[1].x = ((Rect) ref this.mInnerUV).xMin;
        UIBasicSprite.mTempUVs[2].x = ((Rect) ref this.mInnerUV).xMax;
        UIBasicSprite.mTempUVs[3].x = ((Rect) ref this.mOuterUV).xMax;
      }
      if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
      {
        UIBasicSprite.mTempPos[1].y = UIBasicSprite.mTempPos[0].y + vector4.w;
        UIBasicSprite.mTempPos[2].y = UIBasicSprite.mTempPos[3].y - vector4.y;
        UIBasicSprite.mTempUVs[3].y = ((Rect) ref this.mOuterUV).yMin;
        UIBasicSprite.mTempUVs[2].y = ((Rect) ref this.mInnerUV).yMin;
        UIBasicSprite.mTempUVs[1].y = ((Rect) ref this.mInnerUV).yMax;
        UIBasicSprite.mTempUVs[0].y = ((Rect) ref this.mOuterUV).yMax;
      }
      else
      {
        UIBasicSprite.mTempPos[1].y = UIBasicSprite.mTempPos[0].y + vector4.y;
        UIBasicSprite.mTempPos[2].y = UIBasicSprite.mTempPos[3].y - vector4.w;
        UIBasicSprite.mTempUVs[0].y = ((Rect) ref this.mOuterUV).yMin;
        UIBasicSprite.mTempUVs[1].y = ((Rect) ref this.mInnerUV).yMin;
        UIBasicSprite.mTempUVs[2].y = ((Rect) ref this.mInnerUV).yMax;
        UIBasicSprite.mTempUVs[3].y = ((Rect) ref this.mOuterUV).yMax;
      }
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = index1 + 1;
        for (int index3 = 0; index3 < 3; ++index3)
        {
          if (this.centerType != UIBasicSprite.AdvancedType.Invisible || index1 != 1 || index3 != 1)
          {
            int index4 = index3 + 1;
            if (index1 == 1 && index3 == 1)
            {
              if (this.centerType == UIBasicSprite.AdvancedType.Tiled)
              {
                float x1 = UIBasicSprite.mTempPos[index1].x;
                float x2 = UIBasicSprite.mTempPos[index2].x;
                double y1 = (double) UIBasicSprite.mTempPos[index3].y;
                float y2 = UIBasicSprite.mTempPos[index4].y;
                float x3 = UIBasicSprite.mTempUVs[index1].x;
                float y3 = UIBasicSprite.mTempUVs[index3].y;
                for (float v0y = (float) y1; (double) v0y < (double) y2; v0y += vector2.y)
                {
                  float v0x = x1;
                  float u1y = UIBasicSprite.mTempUVs[index4].y;
                  float v1y = v0y + vector2.y;
                  if ((double) v1y > (double) y2)
                  {
                    u1y = Mathf.Lerp(y3, u1y, (y2 - v0y) / vector2.y);
                    v1y = y2;
                  }
                  for (; (double) v0x < (double) x2; v0x += vector2.x)
                  {
                    float v1x = v0x + vector2.x;
                    float u1x = UIBasicSprite.mTempUVs[index2].x;
                    if ((double) v1x > (double) x2)
                    {
                      u1x = Mathf.Lerp(x3, u1x, (x2 - v0x) / vector2.x);
                      v1x = x2;
                    }
                    UIBasicSprite.Fill(verts, uvs, cols, v0x, v1x, v0y, v1y, x3, u1x, y3, u1y, Color32.op_Implicit(drawingColor));
                  }
                }
              }
              else if (this.centerType == UIBasicSprite.AdvancedType.Sliced)
                UIBasicSprite.Fill(verts, uvs, cols, UIBasicSprite.mTempPos[index1].x, UIBasicSprite.mTempPos[index2].x, UIBasicSprite.mTempPos[index3].y, UIBasicSprite.mTempPos[index4].y, UIBasicSprite.mTempUVs[index1].x, UIBasicSprite.mTempUVs[index2].x, UIBasicSprite.mTempUVs[index3].y, UIBasicSprite.mTempUVs[index4].y, Color32.op_Implicit(drawingColor));
            }
            else if (index1 == 1)
            {
              if (index3 == 0 && this.bottomType == UIBasicSprite.AdvancedType.Tiled || index3 == 2 && this.topType == UIBasicSprite.AdvancedType.Tiled)
              {
                double x4 = (double) UIBasicSprite.mTempPos[index1].x;
                float x5 = UIBasicSprite.mTempPos[index2].x;
                float y4 = UIBasicSprite.mTempPos[index3].y;
                float y5 = UIBasicSprite.mTempPos[index4].y;
                float x6 = UIBasicSprite.mTempUVs[index1].x;
                float y6 = UIBasicSprite.mTempUVs[index3].y;
                float y7 = UIBasicSprite.mTempUVs[index4].y;
                for (float v0x = (float) x4; (double) v0x < (double) x5; v0x += vector2.x)
                {
                  float v1x = v0x + vector2.x;
                  float u1x = UIBasicSprite.mTempUVs[index2].x;
                  if ((double) v1x > (double) x5)
                  {
                    u1x = Mathf.Lerp(x6, u1x, (x5 - v0x) / vector2.x);
                    v1x = x5;
                  }
                  UIBasicSprite.Fill(verts, uvs, cols, v0x, v1x, y4, y5, x6, u1x, y6, y7, Color32.op_Implicit(drawingColor));
                }
              }
              else if (index3 == 0 && this.bottomType != UIBasicSprite.AdvancedType.Invisible || index3 == 2 && this.topType != UIBasicSprite.AdvancedType.Invisible)
                UIBasicSprite.Fill(verts, uvs, cols, UIBasicSprite.mTempPos[index1].x, UIBasicSprite.mTempPos[index2].x, UIBasicSprite.mTempPos[index3].y, UIBasicSprite.mTempPos[index4].y, UIBasicSprite.mTempUVs[index1].x, UIBasicSprite.mTempUVs[index2].x, UIBasicSprite.mTempUVs[index3].y, UIBasicSprite.mTempUVs[index4].y, Color32.op_Implicit(drawingColor));
            }
            else if (index3 == 1)
            {
              if (index1 == 0 && this.leftType == UIBasicSprite.AdvancedType.Tiled || index1 == 2 && this.rightType == UIBasicSprite.AdvancedType.Tiled)
              {
                float x7 = UIBasicSprite.mTempPos[index1].x;
                float x8 = UIBasicSprite.mTempPos[index2].x;
                double y8 = (double) UIBasicSprite.mTempPos[index3].y;
                float y9 = UIBasicSprite.mTempPos[index4].y;
                float x9 = UIBasicSprite.mTempUVs[index1].x;
                float x10 = UIBasicSprite.mTempUVs[index2].x;
                float y10 = UIBasicSprite.mTempUVs[index3].y;
                for (float v0y = (float) y8; (double) v0y < (double) y9; v0y += vector2.y)
                {
                  float u1y = UIBasicSprite.mTempUVs[index4].y;
                  float v1y = v0y + vector2.y;
                  if ((double) v1y > (double) y9)
                  {
                    u1y = Mathf.Lerp(y10, u1y, (y9 - v0y) / vector2.y);
                    v1y = y9;
                  }
                  UIBasicSprite.Fill(verts, uvs, cols, x7, x8, v0y, v1y, x9, x10, y10, u1y, Color32.op_Implicit(drawingColor));
                }
              }
              else if (index1 == 0 && this.leftType != UIBasicSprite.AdvancedType.Invisible || index1 == 2 && this.rightType != UIBasicSprite.AdvancedType.Invisible)
                UIBasicSprite.Fill(verts, uvs, cols, UIBasicSprite.mTempPos[index1].x, UIBasicSprite.mTempPos[index2].x, UIBasicSprite.mTempPos[index3].y, UIBasicSprite.mTempPos[index4].y, UIBasicSprite.mTempUVs[index1].x, UIBasicSprite.mTempUVs[index2].x, UIBasicSprite.mTempUVs[index3].y, UIBasicSprite.mTempUVs[index4].y, Color32.op_Implicit(drawingColor));
            }
            else if (index3 == 0 && this.bottomType != UIBasicSprite.AdvancedType.Invisible || index3 == 2 && this.topType != UIBasicSprite.AdvancedType.Invisible || index1 == 0 && this.leftType != UIBasicSprite.AdvancedType.Invisible || index1 == 2 && this.rightType != UIBasicSprite.AdvancedType.Invisible)
              UIBasicSprite.Fill(verts, uvs, cols, UIBasicSprite.mTempPos[index1].x, UIBasicSprite.mTempPos[index2].x, UIBasicSprite.mTempPos[index3].y, UIBasicSprite.mTempPos[index4].y, UIBasicSprite.mTempUVs[index1].x, UIBasicSprite.mTempUVs[index2].x, UIBasicSprite.mTempUVs[index3].y, UIBasicSprite.mTempUVs[index4].y, Color32.op_Implicit(drawingColor));
          }
        }
      }
    }
  }

  private static bool RadialCut(Vector2[] xy, Vector2[] uv, float fill, bool invert, int corner)
  {
    if ((double) fill < 1.0 / 1000.0)
      return false;
    if ((corner & 1) == 1)
      invert = !invert;
    if (!invert && (double) fill > 0.99900001287460327)
      return true;
    float num1 = Mathf.Clamp01(fill);
    if (invert)
      num1 = 1f - num1;
    float num2 = num1 * 1.57079637f;
    float cos = Mathf.Cos(num2);
    float sin = Mathf.Sin(num2);
    UIBasicSprite.RadialCut(xy, cos, sin, invert, corner);
    UIBasicSprite.RadialCut(uv, cos, sin, invert, corner);
    return true;
  }

  private static void RadialCut(Vector2[] xy, float cos, float sin, bool invert, int corner)
  {
    int index1 = corner;
    int index2 = NGUIMath.RepeatIndex(corner + 1, 4);
    int index3 = NGUIMath.RepeatIndex(corner + 2, 4);
    int index4 = NGUIMath.RepeatIndex(corner + 3, 4);
    if ((corner & 1) == 1)
    {
      if ((double) sin > (double) cos)
      {
        cos /= sin;
        sin = 1f;
        if (invert)
        {
          xy[index2].x = Mathf.Lerp(xy[index1].x, xy[index3].x, cos);
          xy[index3].x = xy[index2].x;
        }
      }
      else if ((double) cos > (double) sin)
      {
        sin /= cos;
        cos = 1f;
        if (!invert)
        {
          xy[index3].y = Mathf.Lerp(xy[index1].y, xy[index3].y, sin);
          xy[index4].y = xy[index3].y;
        }
      }
      else
      {
        cos = 1f;
        sin = 1f;
      }
      if (!invert)
        xy[index4].x = Mathf.Lerp(xy[index1].x, xy[index3].x, cos);
      else
        xy[index2].y = Mathf.Lerp(xy[index1].y, xy[index3].y, sin);
    }
    else
    {
      if ((double) cos > (double) sin)
      {
        sin /= cos;
        cos = 1f;
        if (!invert)
        {
          xy[index2].y = Mathf.Lerp(xy[index1].y, xy[index3].y, sin);
          xy[index3].y = xy[index2].y;
        }
      }
      else if ((double) sin > (double) cos)
      {
        cos /= sin;
        sin = 1f;
        if (invert)
        {
          xy[index3].x = Mathf.Lerp(xy[index1].x, xy[index3].x, cos);
          xy[index4].x = xy[index3].x;
        }
      }
      else
      {
        cos = 1f;
        sin = 1f;
      }
      if (invert)
        xy[index4].y = Mathf.Lerp(xy[index1].y, xy[index3].y, sin);
      else
        xy[index2].x = Mathf.Lerp(xy[index1].x, xy[index3].x, cos);
    }
  }

  private static void Fill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols,
    float v0x,
    float v1x,
    float v0y,
    float v1y,
    float u0x,
    float u1x,
    float u0y,
    float u1y,
    Color col)
  {
    verts.Add(new Vector3(v0x, v0y));
    verts.Add(new Vector3(v0x, v1y));
    verts.Add(new Vector3(v1x, v1y));
    verts.Add(new Vector3(v1x, v0y));
    uvs.Add(new Vector2(u0x, u0y));
    uvs.Add(new Vector2(u0x, u1y));
    uvs.Add(new Vector2(u1x, u1y));
    uvs.Add(new Vector2(u1x, u0y));
    cols.Add(Color32.op_Implicit(col));
    cols.Add(Color32.op_Implicit(col));
    cols.Add(Color32.op_Implicit(col));
    cols.Add(Color32.op_Implicit(col));
  }

  public enum Type
  {
    Simple,
    Sliced,
    Tiled,
    Filled,
    Advanced,
  }

  public enum FillDirection
  {
    Horizontal,
    Vertical,
    Radial90,
    Radial180,
    Radial360,
  }

  public enum AdvancedType
  {
    Invisible,
    Sliced,
    Tiled,
  }

  public enum Flip
  {
    Nothing,
    Horizontally,
    Vertically,
    Both,
  }
}
