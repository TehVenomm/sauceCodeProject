// Decompiled with JetBrains decompiler
// Type: UI2DSprite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Unity2D Sprite")]
public class UI2DSprite : UIBasicSprite
{
  [HideInInspector]
  [SerializeField]
  private Sprite mSprite;
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
  [HideInInspector]
  [SerializeField]
  private float mPixelSize = 1f;
  public Sprite nextSprite;
  [NonSerialized]
  private int mPMA = -1;

  public Sprite sprite2D
  {
    get => this.mSprite;
    set
    {
      if (!Object.op_Inequality((Object) this.mSprite, (Object) value))
        return;
      this.RemoveFromPanel();
      this.mSprite = value;
      this.nextSprite = (Sprite) null;
      this.CreatePanel();
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
      this.RemoveFromPanel();
      this.mShader = value;
      if (!Object.op_Equality((Object) this.mMat, (Object) null))
        return;
      this.mPMA = -1;
      this.MarkAsChanged();
    }
  }

  public override Texture mainTexture
  {
    get
    {
      if (Object.op_Inequality((Object) this.mSprite, (Object) null))
        return (Texture) this.mSprite.texture;
      return Object.op_Inequality((Object) this.mMat, (Object) null) ? this.mMat.mainTexture : (Texture) null;
    }
  }

  public override bool premultipliedAlpha
  {
    get
    {
      if (this.mPMA == -1)
      {
        Shader shader = this.shader;
        this.mPMA = !Object.op_Inequality((Object) shader, (Object) null) || !((Object) shader).name.Contains("Premultiplied") ? 0 : 1;
      }
      return this.mPMA == 1;
    }
  }

  public override float pixelSize => this.mPixelSize;

  public override Vector4 drawingDimensions
  {
    get
    {
      Vector2 pivotOffset = this.pivotOffset;
      float num1 = -pivotOffset.x * (float) this.mWidth;
      float num2 = -pivotOffset.y * (float) this.mHeight;
      float num3 = num1 + (float) this.mWidth;
      float num4 = num2 + (float) this.mHeight;
      if (Object.op_Inequality((Object) this.mSprite, (Object) null) && this.mType != UIBasicSprite.Type.Tiled)
      {
        Rect rect1 = this.mSprite.rect;
        int num5 = Mathf.RoundToInt(((Rect) ref rect1).width);
        Rect rect2 = this.mSprite.rect;
        int num6 = Mathf.RoundToInt(((Rect) ref rect2).height);
        int num7 = Mathf.RoundToInt(this.mSprite.textureRectOffset.x);
        int num8 = Mathf.RoundToInt(this.mSprite.textureRectOffset.y);
        Rect rect3 = this.mSprite.rect;
        double width1 = (double) ((Rect) ref rect3).width;
        Rect textureRect1 = this.mSprite.textureRect;
        double width2 = (double) ((Rect) ref textureRect1).width;
        int num9 = Mathf.RoundToInt((float) (width1 - width2) - this.mSprite.textureRectOffset.x);
        Rect rect4 = this.mSprite.rect;
        double height1 = (double) ((Rect) ref rect4).height;
        Rect textureRect2 = this.mSprite.textureRect;
        double height2 = (double) ((Rect) ref textureRect2).height;
        int num10 = Mathf.RoundToInt((float) (height1 - height2) - this.mSprite.textureRectOffset.y);
        float num11 = 1f;
        float num12 = 1f;
        if (num5 > 0 && num6 > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
        {
          if ((num5 & 1) != 0)
            ++num9;
          if ((num6 & 1) != 0)
            ++num10;
          num11 = 1f / (float) num5 * (float) this.mWidth;
          num12 = 1f / (float) num6 * (float) this.mHeight;
        }
        if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
        {
          num1 += (float) num9 * num11;
          num3 -= (float) num7 * num11;
        }
        else
        {
          num1 += (float) num7 * num11;
          num3 -= (float) num9 * num11;
        }
        if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
        {
          num2 += (float) num10 * num12;
          num4 -= (float) num8 * num12;
        }
        else
        {
          num2 += (float) num8 * num12;
          num4 -= (float) num10 * num12;
        }
      }
      float num13;
      float num14;
      if (this.mFixedAspect)
      {
        num13 = 0.0f;
        num14 = 0.0f;
      }
      else
      {
        Vector4 vector4 = Vector4.op_Multiply(this.border, this.pixelSize);
        num13 = vector4.x + vector4.z;
        num14 = vector4.y + vector4.w;
      }
      double num15 = (double) Mathf.Lerp(num1, num3 - num13, this.mDrawRegion.x);
      float num16 = Mathf.Lerp(num2, num4 - num14, this.mDrawRegion.y);
      float num17 = Mathf.Lerp(num1 + num13, num3, this.mDrawRegion.z);
      float num18 = Mathf.Lerp(num2 + num14, num4, this.mDrawRegion.w);
      double num19 = (double) num16;
      double num20 = (double) num17;
      double num21 = (double) num18;
      return new Vector4((float) num15, (float) num19, (float) num20, (float) num21);
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

  protected override void OnUpdate()
  {
    if (Object.op_Inequality((Object) this.nextSprite, (Object) null))
    {
      if (Object.op_Inequality((Object) this.nextSprite, (Object) this.mSprite))
        this.sprite2D = this.nextSprite;
      this.nextSprite = (Sprite) null;
    }
    base.OnUpdate();
    if (!this.mFixedAspect || !Object.op_Inequality((Object) this.mainTexture, (Object) null))
      return;
    Rect rect = this.mSprite.rect;
    int num1 = Mathf.RoundToInt(((Rect) ref rect).width);
    rect = this.mSprite.rect;
    int num2 = Mathf.RoundToInt(((Rect) ref rect).height);
    int num3 = Mathf.RoundToInt(this.mSprite.textureRectOffset.x);
    int num4 = Mathf.RoundToInt(this.mSprite.textureRectOffset.y);
    rect = this.mSprite.rect;
    double width1 = (double) ((Rect) ref rect).width;
    rect = this.mSprite.textureRect;
    double width2 = (double) ((Rect) ref rect).width;
    int num5 = Mathf.RoundToInt((float) (width1 - width2) - this.mSprite.textureRectOffset.x);
    rect = this.mSprite.rect;
    double height1 = (double) ((Rect) ref rect).height;
    rect = this.mSprite.textureRect;
    double height2 = (double) ((Rect) ref rect).height;
    int num6 = Mathf.RoundToInt((float) (height1 - height2) - this.mSprite.textureRectOffset.y);
    int num7 = num3 + num5;
    int num8 = num1 + num7;
    int num9 = num2 + (num6 + num4);
    float mWidth = (float) this.mWidth;
    float mHeight = (float) this.mHeight;
    float num10 = mWidth / mHeight;
    float num11 = (float) num8 / (float) num9;
    if ((double) num11 < (double) num10)
    {
      float num12 = (float) (((double) mWidth - (double) mHeight * (double) num11) / (double) mWidth * 0.5);
      this.drawRegion = new Vector4(num12, 0.0f, 1f - num12, 1f);
    }
    else
    {
      float num13 = (float) (((double) mHeight - (double) mWidth / (double) num11) / (double) mHeight * 0.5);
      this.drawRegion = new Vector4(0.0f, num13, 1f, 1f - num13);
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
    Rect rect = this.mSprite.rect;
    int num1 = Mathf.RoundToInt(((Rect) ref rect).width);
    int num2 = Mathf.RoundToInt(((Rect) ref rect).height);
    if ((num1 & 1) == 1)
      ++num1;
    if ((num2 & 1) == 1)
      ++num2;
    this.width = num1;
    this.height = num2;
  }

  public override void OnFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    Texture mainTexture = this.mainTexture;
    if (Object.op_Equality((Object) mainTexture, (Object) null))
      return;
    Rect outer = Object.op_Inequality((Object) this.mSprite, (Object) null) ? this.mSprite.textureRect : new Rect(0.0f, 0.0f, (float) mainTexture.width, (float) mainTexture.height);
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
