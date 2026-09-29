// Decompiled with JetBrains decompiler
// Type: UITexture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Texture")]
public class UITexture : UIBasicSprite
{
  [HideInInspector]
  [SerializeField]
  private Rect mRect = new Rect(0.0f, 0.0f, 1f, 1f);
  [HideInInspector]
  [SerializeField]
  private Texture mTexture;
  [HideInInspector]
  [SerializeField]
  private Material mMat;
  [HideInInspector]
  [SerializeField]
  private Shader mShader;
  [HideInInspector]
  [SerializeField]
  private Vector4 mBorder = Vector4.zero;
  [HideInInspector]
  [SerializeField]
  private bool mFixedAspect;
  [NonSerialized]
  private int mPMA = -1;

  public override Texture mainTexture
  {
    get
    {
      if (Object.op_Inequality((Object) this.mTexture, (Object) null))
        return this.mTexture;
      return Object.op_Inequality((Object) this.mMat, (Object) null) ? this.mMat.mainTexture : (Texture) null;
    }
    set
    {
      if (!Object.op_Inequality((Object) this.mTexture, (Object) value))
        return;
      if (Object.op_Inequality((Object) this.drawCall, (Object) null) && this.drawCall.widgetCount == 1 && Object.op_Equality((Object) this.mMat, (Object) null))
      {
        this.mTexture = value;
        this.drawCall.mainTexture = value;
      }
      else
      {
        this.RemoveFromPanel();
        this.mTexture = value;
        this.mPMA = -1;
        this.MarkAsChanged();
      }
    }
  }

  public override Material material
  {
    get => this.mMat;
    set
    {
      if (!Object.op_Inequality((Object) this.mMat, (Object) value))
        return;
      this.RemoveFromPanel();
      this.mShader = (Shader) null;
      this.mMat = value;
      this.mPMA = -1;
      this.MarkAsChanged();
    }
  }

  public override Shader shader
  {
    get
    {
      if (Object.op_Inequality((Object) this.mMat, (Object) null))
        return this.mMat.shader;
      if (Object.op_Equality((Object) this.mShader, (Object) null))
        this.mShader = Shader.Find("Unlit/Transparent Colored");
      return this.mShader;
    }
    set
    {
      if (!Object.op_Inequality((Object) this.mShader, (Object) value))
        return;
      if (Object.op_Inequality((Object) this.drawCall, (Object) null) && this.drawCall.widgetCount == 1 && Object.op_Equality((Object) this.mMat, (Object) null))
      {
        this.mShader = value;
        this.drawCall.shader = value;
      }
      else
      {
        this.RemoveFromPanel();
        this.mShader = value;
        this.mPMA = -1;
        this.mMat = (Material) null;
        this.MarkAsChanged();
      }
    }
  }

  public override bool premultipliedAlpha
  {
    get
    {
      if (this.mPMA == -1)
      {
        Material material = this.material;
        this.mPMA = !Object.op_Inequality((Object) material, (Object) null) || !Object.op_Inequality((Object) material.shader, (Object) null) || !((Object) material.shader).name.Contains("Premultiplied") ? 0 : 1;
      }
      return this.mPMA == 1;
    }
  }

  public override Vector4 border
  {
    get => this.mBorder;
    set
    {
      if (!Vector4.op_Inequality(this.mBorder, value))
        return;
      this.mBorder = value;
      this.MarkAsChanged();
    }
  }

  public Rect uvRect
  {
    get => this.mRect;
    set
    {
      if (!Rect.op_Inequality(this.mRect, value))
        return;
      this.mRect = value;
      this.MarkAsChanged();
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
      if (Object.op_Inequality((Object) this.mTexture, (Object) null) && this.mType != UIBasicSprite.Type.Tiled)
      {
        int width = this.mTexture.width;
        int height = this.mTexture.height;
        int num5 = 0;
        int num6 = 0;
        float num7 = 1f;
        float num8 = 1f;
        if (width > 0 && height > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
        {
          if ((width & 1) != 0)
            ++num5;
          if ((height & 1) != 0)
            ++num6;
          num7 = 1f / (float) width * (float) this.mWidth;
          num8 = 1f / (float) height * (float) this.mHeight;
        }
        if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
          num1 += (float) num5 * num7;
        else
          num3 -= (float) num5 * num7;
        if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
          num2 += (float) num6 * num8;
        else
          num4 -= (float) num6 * num8;
      }
      float num9;
      float num10;
      if (this.mFixedAspect)
      {
        num9 = 0.0f;
        num10 = 0.0f;
      }
      else
      {
        Vector4 border = this.border;
        num9 = border.x + border.z;
        num10 = border.y + border.w;
      }
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

  public bool fixedAspect
  {
    get => this.mFixedAspect;
    set
    {
      if (this.mFixedAspect == value)
        return;
      this.mFixedAspect = value;
      this.mDrawRegion = new Vector4(0.0f, 0.0f, 1f, 1f);
      this.MarkAsChanged();
    }
  }

  public override void MakePixelPerfect()
  {
    base.MakePixelPerfect();
    if (this.mType == UIBasicSprite.Type.Tiled)
      return;
    Texture mainTexture = this.mainTexture;
    if (Object.op_Equality((Object) mainTexture, (Object) null) || this.mType != UIBasicSprite.Type.Simple && this.mType != UIBasicSprite.Type.Filled && this.hasBorder || !Object.op_Inequality((Object) mainTexture, (Object) null))
      return;
    int width = mainTexture.width;
    int height = mainTexture.height;
    if ((width & 1) == 1)
      ++width;
    if ((height & 1) == 1)
      ++height;
    this.width = width;
    this.height = height;
  }

  protected override void OnUpdate()
  {
    base.OnUpdate();
    if (!this.mFixedAspect)
      return;
    Texture mainTexture = this.mainTexture;
    if (!Object.op_Inequality((Object) mainTexture, (Object) null))
      return;
    int width = mainTexture.width;
    int height = mainTexture.height;
    if ((width & 1) == 1)
      ++width;
    if ((height & 1) == 1)
      ++height;
    float mWidth = (float) this.mWidth;
    float mHeight = (float) this.mHeight;
    float num1 = mWidth / mHeight;
    float num2 = (float) width / (float) height;
    if ((double) num2 < (double) num1)
    {
      float num3 = (float) (((double) mWidth - (double) mHeight * (double) num2) / (double) mWidth * 0.5);
      this.drawRegion = new Vector4(num3, 0.0f, 1f - num3, 1f);
    }
    else
    {
      float num4 = (float) (((double) mHeight - (double) mWidth / (double) num2) / (double) mHeight * 0.5);
      this.drawRegion = new Vector4(0.0f, num4, 1f, 1f - num4);
    }
  }

  public override void OnFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    Texture mainTexture = this.mainTexture;
    if (Object.op_Equality((Object) mainTexture, (Object) null))
      return;
    Rect outer;
    // ISSUE: explicit constructor call
    ((Rect) ref outer).\u002Ector(((Rect) ref this.mRect).x * (float) mainTexture.width, ((Rect) ref this.mRect).y * (float) mainTexture.height, (float) mainTexture.width * ((Rect) ref this.mRect).width, (float) mainTexture.height * ((Rect) ref this.mRect).height);
    Rect inner = outer;
    Vector4 border = this.border;
    ref Rect local1 = ref inner;
    ((Rect) ref local1).xMin = ((Rect) ref local1).xMin + border.x;
    ref Rect local2 = ref inner;
    ((Rect) ref local2).yMin = ((Rect) ref local2).yMin + border.y;
    ref Rect local3 = ref inner;
    ((Rect) ref local3).xMax = ((Rect) ref local3).xMax - border.z;
    ref Rect local4 = ref inner;
    ((Rect) ref local4).yMax = ((Rect) ref local4).yMax - border.w;
    float num1 = 1f / (float) mainTexture.width;
    float num2 = 1f / (float) mainTexture.height;
    ref Rect local5 = ref outer;
    ((Rect) ref local5).xMin = ((Rect) ref local5).xMin * num1;
    ref Rect local6 = ref outer;
    ((Rect) ref local6).xMax = ((Rect) ref local6).xMax * num1;
    ref Rect local7 = ref outer;
    ((Rect) ref local7).yMin = ((Rect) ref local7).yMin * num2;
    ref Rect local8 = ref outer;
    ((Rect) ref local8).yMax = ((Rect) ref local8).yMax * num2;
    ref Rect local9 = ref inner;
    ((Rect) ref local9).xMin = ((Rect) ref local9).xMin * num1;
    ref Rect local10 = ref inner;
    ((Rect) ref local10).xMax = ((Rect) ref local10).xMax * num1;
    ref Rect local11 = ref inner;
    ((Rect) ref local11).yMin = ((Rect) ref local11).yMin * num2;
    ref Rect local12 = ref inner;
    ((Rect) ref local12).yMax = ((Rect) ref local12).yMax * num2;
    int size = verts.size;
    this.Fill(verts, uvs, cols, outer, inner);
    if (this.onPostFill == null)
      return;
    this.onPostFill((UIWidget) this, size, verts, uvs, cols);
  }
}
