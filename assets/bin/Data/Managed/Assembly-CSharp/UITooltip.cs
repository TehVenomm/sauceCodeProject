// Decompiled with JetBrains decompiler
// Type: UITooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
  protected static UITooltip mInstance;
  public Camera uiCamera;
  public UILabel text;
  public UISprite background;
  public float appearSpeed = 10f;
  public bool scalingTransitions = true;
  protected GameObject mTooltip;
  protected Transform mTrans;
  protected float mTarget;
  protected float mCurrent;
  protected Vector3 mPos;
  protected Vector3 mSize = Vector3.zero;
  protected UIWidget[] mWidgets;

  public static bool isVisible
  {
    get
    {
      return Object.op_Inequality((Object) UITooltip.mInstance, (Object) null) && (double) UITooltip.mInstance.mTarget == 1.0;
    }
  }

  private void Awake() => UITooltip.mInstance = this;

  private void OnDestroy() => UITooltip.mInstance = (UITooltip) null;

  protected virtual void Start()
  {
    this.mTrans = ((Component) this).transform;
    this.mWidgets = ((Component) this).GetComponentsInChildren<UIWidget>();
    this.mPos = this.mTrans.localPosition;
    if (Object.op_Equality((Object) this.uiCamera, (Object) null))
      this.uiCamera = NGUITools.FindCameraForLayer(((Component) this).gameObject.layer);
    this.SetAlpha(0.0f);
  }

  protected virtual void Update()
  {
    if (Object.op_Inequality((Object) this.mTooltip, (Object) UICamera.tooltipObject))
    {
      this.mTooltip = (GameObject) null;
      this.mTarget = 0.0f;
    }
    if ((double) this.mCurrent == (double) this.mTarget)
      return;
    this.mCurrent = Mathf.Lerp(this.mCurrent, this.mTarget, RealTime.deltaTime * this.appearSpeed);
    if ((double) Mathf.Abs(this.mCurrent - this.mTarget) < 1.0 / 1000.0)
      this.mCurrent = this.mTarget;
    this.SetAlpha(this.mCurrent * this.mCurrent);
    if (!this.scalingTransitions)
      return;
    Vector3 vector3_1 = Vector3.op_Multiply(this.mSize, 0.25f);
    vector3_1.y = -vector3_1.y;
    Vector3 vector3_2 = Vector3.op_Multiply(Vector3.one, (float) (1.5 - (double) this.mCurrent * 0.5));
    this.mTrans.localPosition = Vector3.Lerp(Vector3.op_Subtraction(this.mPos, vector3_1), this.mPos, this.mCurrent);
    this.mTrans.localScale = vector3_2;
  }

  protected virtual void SetAlpha(float val)
  {
    int index = 0;
    for (int length = this.mWidgets.Length; index < length; ++index)
    {
      UIWidget mWidget = this.mWidgets[index];
      Color color = mWidget.color;
      color.a = val;
      mWidget.color = color;
    }
  }

  protected virtual void SetText(string tooltipText)
  {
    if (Object.op_Inequality((Object) this.text, (Object) null) && !string.IsNullOrEmpty(tooltipText))
    {
      this.mTarget = 1f;
      this.mTooltip = UICamera.tooltipObject;
      this.text.text = tooltipText;
      this.mPos = Vector2.op_Implicit(UICamera.lastEventPosition);
      Transform transform = ((Component) this.text).transform;
      Vector3 localPosition = transform.localPosition;
      Vector3 localScale = transform.localScale;
      this.mSize = Vector2.op_Implicit(this.text.printedSize);
      this.mSize.x *= localScale.x;
      this.mSize.y *= localScale.y;
      if (Object.op_Inequality((Object) this.background, (Object) null))
      {
        Vector4 border = this.background.border;
        this.mSize.x += (float) ((double) border.x + (double) border.z + ((double) localPosition.x - (double) border.x) * 2.0);
        this.mSize.y += (float) ((double) border.y + (double) border.w + (-(double) localPosition.y - (double) border.y) * 2.0);
        this.background.width = Mathf.RoundToInt(this.mSize.x);
        this.background.height = Mathf.RoundToInt(this.mSize.y);
      }
      if (Object.op_Inequality((Object) this.uiCamera, (Object) null))
      {
        this.mPos.x = Mathf.Clamp01(this.mPos.x / (float) Screen.width);
        this.mPos.y = Mathf.Clamp01(this.mPos.y / (float) Screen.height);
        float num = (float) Screen.height * 0.5f / (this.uiCamera.orthographicSize / this.mTrans.parent.lossyScale.y);
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(num * this.mSize.x / (float) Screen.width, num * this.mSize.y / (float) Screen.height);
        this.mPos.x = Mathf.Min(this.mPos.x, 1f - vector2.x);
        this.mPos.y = Mathf.Max(this.mPos.y, vector2.y);
        this.mTrans.position = this.uiCamera.ViewportToWorldPoint(this.mPos);
        this.mPos = this.mTrans.localPosition;
        this.mPos.x = Mathf.Round(this.mPos.x);
        this.mPos.y = Mathf.Round(this.mPos.y);
        this.mTrans.localPosition = this.mPos;
      }
      else
      {
        if ((double) this.mPos.x + (double) this.mSize.x > (double) Screen.width)
          this.mPos.x = (float) Screen.width - this.mSize.x;
        if ((double) this.mPos.y - (double) this.mSize.y < 0.0)
          this.mPos.y = this.mSize.y;
        this.mPos.x -= (float) Screen.width * 0.5f;
        this.mPos.y -= (float) Screen.height * 0.5f;
      }
    }
    else
    {
      this.mTooltip = (GameObject) null;
      this.mTarget = 0.0f;
    }
  }

  [Obsolete("Use UITooltip.Show instead")]
  public static void ShowText(string text)
  {
    if (!Object.op_Inequality((Object) UITooltip.mInstance, (Object) null))
      return;
    UITooltip.mInstance.SetText(text);
  }

  public static void Show(string text)
  {
    if (!Object.op_Inequality((Object) UITooltip.mInstance, (Object) null))
      return;
    UITooltip.mInstance.SetText(text);
  }

  public static void Hide()
  {
    if (!Object.op_Inequality((Object) UITooltip.mInstance, (Object) null))
      return;
    UITooltip.mInstance.mTooltip = (GameObject) null;
    UITooltip.mInstance.mTarget = 0.0f;
  }
}
