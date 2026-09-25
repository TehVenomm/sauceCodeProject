// Decompiled with JetBrains decompiler
// Type: UIColorPicker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (UITexture))]
public class UIColorPicker : MonoBehaviour
{
  public static UIColorPicker current;
  public Color value = Color.white;
  public UIWidget selectionWidget;
  public List<EventDelegate> onChange = new List<EventDelegate>();
  [NonSerialized]
  private Transform mTrans;
  [NonSerialized]
  private UITexture mUITex;
  [NonSerialized]
  private Texture2D mTex;
  [NonSerialized]
  private UICamera mCam;
  [NonSerialized]
  private Vector2 mPos;
  [NonSerialized]
  private int mWidth;
  [NonSerialized]
  private int mHeight;
  private static AnimationCurve mRed;
  private static AnimationCurve mGreen;
  private static AnimationCurve mBlue;

  private void Start()
  {
    this.mTrans = ((Component) this).transform;
    this.mUITex = ((Component) this).GetComponent<UITexture>();
    this.mCam = UICamera.FindCameraForLayer(((Component) this).gameObject.layer);
    this.mWidth = this.mUITex.width;
    this.mHeight = this.mUITex.height;
    Color[] colorArray = new Color[this.mWidth * this.mHeight];
    for (int index1 = 0; index1 < this.mHeight; ++index1)
    {
      float y = ((float) index1 - 1f) / (float) this.mHeight;
      for (int index2 = 0; index2 < this.mWidth; ++index2)
      {
        float x = ((float) index2 - 1f) / (float) this.mWidth;
        int index3 = index2 + index1 * this.mWidth;
        colorArray[index3] = UIColorPicker.Sample(x, y);
      }
    }
    this.mTex = new Texture2D(this.mWidth, this.mHeight, (TextureFormat) 3, false);
    this.mTex.SetPixels(colorArray);
    ((Texture) this.mTex).filterMode = (FilterMode) 2;
    ((Texture) this.mTex).wrapMode = (TextureWrapMode) 1;
    this.mTex.Apply();
    this.mUITex.mainTexture = (Texture) this.mTex;
    this.Select(this.value);
  }

  private void OnDestroy()
  {
    Object.Destroy((Object) this.mTex);
    this.mTex = (Texture2D) null;
  }

  private void OnPress(bool pressed)
  {
    if (!(((Behaviour) this).enabled & pressed) || UICamera.currentScheme == UICamera.ControlScheme.Controller)
      return;
    this.Sample();
  }

  private void OnDrag(Vector2 delta)
  {
    if (!((Behaviour) this).enabled)
      return;
    this.Sample();
  }

  private void OnPan(Vector2 delta)
  {
    if (!((Behaviour) this).enabled)
      return;
    this.mPos.x = Mathf.Clamp01(this.mPos.x + delta.x);
    this.mPos.y = Mathf.Clamp01(this.mPos.y + delta.y);
    this.Select(this.mPos);
  }

  private void Sample()
  {
    Vector3 vector3 = this.mTrans.InverseTransformPoint(this.mCam.cachedCamera.ScreenToWorldPoint(Vector2.op_Implicit(UICamera.lastEventPosition)));
    Vector3[] localCorners = this.mUITex.localCorners;
    this.mPos.x = Mathf.Clamp01((float) (((double) vector3.x - (double) localCorners[0].x) / ((double) localCorners[2].x - (double) localCorners[0].x)));
    this.mPos.y = Mathf.Clamp01((float) (((double) vector3.y - (double) localCorners[0].y) / ((double) localCorners[2].y - (double) localCorners[0].y)));
    if (Object.op_Inequality((Object) this.selectionWidget, (Object) null))
    {
      vector3.x = Mathf.Lerp(localCorners[0].x, localCorners[2].x, this.mPos.x);
      vector3.y = Mathf.Lerp(localCorners[0].y, localCorners[2].y, this.mPos.y);
      ((Component) this.selectionWidget).transform.OverlayPosition(this.mTrans.TransformPoint(vector3), this.mCam.cachedCamera);
    }
    this.value = UIColorPicker.Sample(this.mPos.x, this.mPos.y);
    UIColorPicker.current = this;
    EventDelegate.Execute(this.onChange);
    UIColorPicker.current = (UIColorPicker) null;
  }

  public void Select(Vector2 v)
  {
    v.x = Mathf.Clamp01(v.x);
    v.y = Mathf.Clamp01(v.y);
    this.mPos = v;
    if (Object.op_Inequality((Object) this.selectionWidget, (Object) null))
    {
      Vector3[] localCorners = this.mUITex.localCorners;
      v.x = Mathf.Lerp(localCorners[0].x, localCorners[2].x, this.mPos.x);
      v.y = Mathf.Lerp(localCorners[0].y, localCorners[2].y, this.mPos.y);
      v = Vector2.op_Implicit(this.mTrans.TransformPoint(Vector2.op_Implicit(v)));
      ((Component) this.selectionWidget).transform.OverlayPosition(Vector2.op_Implicit(v), this.mCam.cachedCamera);
    }
    this.value = UIColorPicker.Sample(this.mPos.x, this.mPos.y);
    UIColorPicker.current = this;
    EventDelegate.Execute(this.onChange);
    UIColorPicker.current = (UIColorPicker) null;
  }

  public Vector2 Select(Color c)
  {
    if (Object.op_Equality((Object) this.mUITex, (Object) null))
    {
      this.value = c;
      return this.mPos;
    }
    float num1 = float.MaxValue;
    for (int index1 = 0; index1 < this.mHeight; ++index1)
    {
      float y = ((float) index1 - 1f) / (float) this.mHeight;
      for (int index2 = 0; index2 < this.mWidth; ++index2)
      {
        float x = ((float) index2 - 1f) / (float) this.mWidth;
        Color color = UIColorPicker.Sample(x, y);
        color.r -= c.r;
        color.g -= c.g;
        color.b -= c.b;
        float num2 = (float) ((double) color.r * (double) color.r + (double) color.g * (double) color.g + (double) color.b * (double) color.b);
        if ((double) num2 < (double) num1)
        {
          num1 = num2;
          this.mPos.x = x;
          this.mPos.y = y;
        }
      }
    }
    if (Object.op_Inequality((Object) this.selectionWidget, (Object) null))
    {
      Vector3[] localCorners = this.mUITex.localCorners;
      Vector3 worldPos;
      worldPos.x = Mathf.Lerp(localCorners[0].x, localCorners[2].x, this.mPos.x);
      worldPos.y = Mathf.Lerp(localCorners[0].y, localCorners[2].y, this.mPos.y);
      worldPos.z = 0.0f;
      worldPos = this.mTrans.TransformPoint(worldPos);
      ((Component) this.selectionWidget).transform.OverlayPosition(worldPos, this.mCam.cachedCamera);
    }
    this.value = c;
    UIColorPicker.current = this;
    EventDelegate.Execute(this.onChange);
    UIColorPicker.current = (UIColorPicker) null;
    return this.mPos;
  }

  public static Color Sample(float x, float y)
  {
    if (UIColorPicker.mRed == null)
    {
      UIColorPicker.mRed = new AnimationCurve(new Keyframe[8]
      {
        new Keyframe(0.0f, 1f),
        new Keyframe(0.142857149f, 1f),
        new Keyframe(0.2857143f, 0.0f),
        new Keyframe(0.428571433f, 0.0f),
        new Keyframe(0.5714286f, 0.0f),
        new Keyframe(0.714285731f, 1f),
        new Keyframe(0.857142866f, 1f),
        new Keyframe(1f, 0.5f)
      });
      UIColorPicker.mGreen = new AnimationCurve(new Keyframe[8]
      {
        new Keyframe(0.0f, 0.0f),
        new Keyframe(0.142857149f, 1f),
        new Keyframe(0.2857143f, 1f),
        new Keyframe(0.428571433f, 1f),
        new Keyframe(0.5714286f, 0.0f),
        new Keyframe(0.714285731f, 0.0f),
        new Keyframe(0.857142866f, 0.0f),
        new Keyframe(1f, 0.5f)
      });
      UIColorPicker.mBlue = new AnimationCurve(new Keyframe[8]
      {
        new Keyframe(0.0f, 0.0f),
        new Keyframe(0.142857149f, 0.0f),
        new Keyframe(0.2857143f, 0.0f),
        new Keyframe(0.428571433f, 1f),
        new Keyframe(0.5714286f, 1f),
        new Keyframe(0.714285731f, 1f),
        new Keyframe(0.857142866f, 0.0f),
        new Keyframe(1f, 0.5f)
      });
    }
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(UIColorPicker.mRed.Evaluate(x), UIColorPicker.mGreen.Evaluate(x), UIColorPicker.mBlue.Evaluate(x));
    if ((double) y < 0.5)
    {
      y *= 2f;
      vector3.x *= y;
      vector3.y *= y;
      vector3.z *= y;
    }
    else
      vector3 = Vector3.Lerp(vector3, Vector3.one, (float) ((double) y * 2.0 - 1.0));
    return new Color(vector3.x, vector3.y, vector3.z, 1f);
  }
}
