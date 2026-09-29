// Decompiled with JetBrains decompiler
// Type: UIWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Diagnostics;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Widget")]
public class UIWidget : UIRect
{
  [HideInInspector]
  [SerializeField]
  protected Color mColor = Color.white;
  [HideInInspector]
  [SerializeField]
  protected UIWidget.Pivot mPivot = UIWidget.Pivot.Center;
  [HideInInspector]
  [SerializeField]
  protected int mWidth = 100;
  [HideInInspector]
  [SerializeField]
  protected int mHeight = 100;
  [HideInInspector]
  [SerializeField]
  protected int mDepth;
  public UIWidget.OnDimensionsChanged onChange;
  public UIWidget.OnPostFillCallback onPostFill;
  public UIDrawCall.OnRenderCallback mOnRender;
  public bool autoResizeBoxCollider;
  public bool hideIfOffScreen;
  public UIWidget.AspectRatioSource keepAspectRatio;
  public float aspectRatio = 1f;
  public UIWidget.HitCheck hitCheck;
  [NonSerialized]
  public UIPanel panel;
  [NonSerialized]
  public UIGeometry geometry = new UIGeometry();
  [NonSerialized]
  public bool fillGeometry = true;
  [NonSerialized]
  protected bool mPlayMode = true;
  [NonSerialized]
  protected Vector4 mDrawRegion = new Vector4(0.0f, 0.0f, 1f, 1f);
  [NonSerialized]
  private Matrix4x4 mLocalToPanel;
  [NonSerialized]
  private bool mIsVisibleByAlpha = true;
  [NonSerialized]
  private bool mIsVisibleByPanel = true;
  [NonSerialized]
  private bool mIsInFront = true;
  [NonSerialized]
  private float mLastAlpha;
  [NonSerialized]
  private bool mMoved;
  [NonSerialized]
  public UIDrawCall drawCall;
  [NonSerialized]
  protected Vector3[] mCorners = new Vector3[4];
  [NonSerialized]
  private int mAlphaFrameID = -1;
  private int mMatrixFrame = -1;
  private Vector3 mOldV0;
  private Vector3 mOldV1;

  public UIDrawCall.OnRenderCallback onRender
  {
    get => this.mOnRender;
    set
    {
      if (!(this.mOnRender != value))
        return;
      if (Object.op_Inequality((Object) this.drawCall, (Object) null) && this.drawCall.onRender != null && this.mOnRender != null)
        this.drawCall.onRender -= this.mOnRender;
      this.mOnRender = value;
      if (!Object.op_Inequality((Object) this.drawCall, (Object) null))
        return;
      this.drawCall.onRender += value;
    }
  }

  public Vector4 drawRegion
  {
    get => this.mDrawRegion;
    set
    {
      if (!Vector4.op_Inequality(this.mDrawRegion, value))
        return;
      this.mDrawRegion = value;
      if (this.autoResizeBoxCollider)
        this.ResizeCollider();
      this.MarkAsChanged();
    }
  }

  public Vector2 pivotOffset => NGUIMath.GetPivotOffset(this.pivot);

  public int width
  {
    get => this.mWidth;
    set
    {
      int minWidth = this.minWidth;
      if (value < minWidth)
        value = minWidth;
      if (this.mWidth == value || this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
        return;
      if (this.isAnchoredHorizontally)
      {
        if (Object.op_Inequality((Object) this.leftAnchor.target, (Object) null) && Object.op_Inequality((Object) this.rightAnchor.target, (Object) null))
        {
          if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Left || this.mPivot == UIWidget.Pivot.TopLeft)
            NGUIMath.AdjustWidget(this, 0.0f, 0.0f, (float) (value - this.mWidth), 0.0f);
          else if (this.mPivot == UIWidget.Pivot.BottomRight || this.mPivot == UIWidget.Pivot.Right || this.mPivot == UIWidget.Pivot.TopRight)
          {
            NGUIMath.AdjustWidget(this, (float) (this.mWidth - value), 0.0f, 0.0f, 0.0f);
          }
          else
          {
            int num1 = value - this.mWidth;
            int num2 = num1 - (num1 & 1);
            if (num2 == 0)
              return;
            NGUIMath.AdjustWidget(this, (float) -num2 * 0.5f, 0.0f, (float) num2 * 0.5f, 0.0f);
          }
        }
        else if (Object.op_Inequality((Object) this.leftAnchor.target, (Object) null))
          NGUIMath.AdjustWidget(this, 0.0f, 0.0f, (float) (value - this.mWidth), 0.0f);
        else
          NGUIMath.AdjustWidget(this, (float) (this.mWidth - value), 0.0f, 0.0f, 0.0f);
      }
      else
        this.SetDimensions(value, this.mHeight);
    }
  }

  public int height
  {
    get => this.mHeight;
    set
    {
      int minHeight = this.minHeight;
      if (value < minHeight)
        value = minHeight;
      if (this.mHeight == value || this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnWidth)
        return;
      if (this.isAnchoredVertically)
      {
        if (Object.op_Inequality((Object) this.bottomAnchor.target, (Object) null) && Object.op_Inequality((Object) this.topAnchor.target, (Object) null))
        {
          if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Bottom || this.mPivot == UIWidget.Pivot.BottomRight)
            NGUIMath.AdjustWidget(this, 0.0f, 0.0f, 0.0f, (float) (value - this.mHeight));
          else if (this.mPivot == UIWidget.Pivot.TopLeft || this.mPivot == UIWidget.Pivot.Top || this.mPivot == UIWidget.Pivot.TopRight)
          {
            NGUIMath.AdjustWidget(this, 0.0f, (float) (this.mHeight - value), 0.0f, 0.0f);
          }
          else
          {
            int num1 = value - this.mHeight;
            int num2 = num1 - (num1 & 1);
            if (num2 == 0)
              return;
            NGUIMath.AdjustWidget(this, 0.0f, (float) -num2 * 0.5f, 0.0f, (float) num2 * 0.5f);
          }
        }
        else if (Object.op_Inequality((Object) this.bottomAnchor.target, (Object) null))
          NGUIMath.AdjustWidget(this, 0.0f, 0.0f, 0.0f, (float) (value - this.mHeight));
        else
          NGUIMath.AdjustWidget(this, 0.0f, (float) (this.mHeight - value), 0.0f, 0.0f);
      }
      else
        this.SetDimensions(this.mWidth, value);
    }
  }

  public Color color
  {
    get => this.mColor;
    set
    {
      if (!Color.op_Inequality(this.mColor, value))
        return;
      bool includeChildren = (double) this.mColor.a != (double) value.a;
      this.mColor = value;
      this.Invalidate(includeChildren);
    }
  }

  public override float alpha
  {
    get => this.mColor.a;
    set
    {
      if ((double) this.mColor.a == (double) value)
        return;
      this.mColor.a = value;
      this.Invalidate(true);
    }
  }

  public bool isVisible
  {
    get
    {
      return this.mIsVisibleByPanel && this.mIsVisibleByAlpha && this.mIsInFront && (double) this.finalAlpha > 1.0 / 1000.0 && NGUITools.GetActive((Behaviour) this);
    }
  }

  public bool hasVertices => this.geometry != null && this.geometry.hasVertices;

  public UIWidget.Pivot rawPivot
  {
    get => this.mPivot;
    set
    {
      if (this.mPivot == value)
        return;
      this.mPivot = value;
      if (this.autoResizeBoxCollider)
        this.ResizeCollider();
      this.MarkAsChanged();
    }
  }

  public UIWidget.Pivot pivot
  {
    get => this.mPivot;
    set
    {
      if (this.mPivot == value)
        return;
      Vector3 worldCorner1 = this.worldCorners[0];
      this.mPivot = value;
      this.mChanged = true;
      Vector3 worldCorner2 = this.worldCorners[0];
      Transform cachedTransform = this.cachedTransform;
      Vector3 position = cachedTransform.position;
      float z = cachedTransform.localPosition.z;
      position.x += worldCorner1.x - worldCorner2.x;
      position.y += worldCorner1.y - worldCorner2.y;
      this.cachedTransform.position = position;
      Vector3 localPosition = this.cachedTransform.localPosition;
      localPosition.x = Mathf.Round(localPosition.x);
      localPosition.y = Mathf.Round(localPosition.y);
      localPosition.z = z;
      this.cachedTransform.localPosition = localPosition;
    }
  }

  public int depth
  {
    get => this.mDepth;
    set
    {
      if (this.mDepth == value)
        return;
      if (Object.op_Inequality((Object) this.panel, (Object) null))
        this.panel.RemoveWidget(this);
      this.mDepth = value;
      if (!Object.op_Inequality((Object) this.panel, (Object) null))
        return;
      this.panel.AddWidget(this);
      if (Application.isPlaying)
        return;
      this.panel.SortWidgets();
      this.panel.RebuildAllDrawCalls();
    }
  }

  public int raycastDepth
  {
    get
    {
      if (Object.op_Equality((Object) this.panel, (Object) null))
        this.CreatePanel();
      return !Object.op_Inequality((Object) this.panel, (Object) null) ? this.mDepth : this.mDepth + this.panel.depth * 1000;
    }
  }

  public override Vector3[] localCorners
  {
    get
    {
      Vector2 pivotOffset = this.pivotOffset;
      float num1 = -pivotOffset.x * (float) this.mWidth;
      float num2 = -pivotOffset.y * (float) this.mHeight;
      float num3 = num1 + (float) this.mWidth;
      float num4 = num2 + (float) this.mHeight;
      this.mCorners[0] = new Vector3(num1, num2);
      this.mCorners[1] = new Vector3(num1, num4);
      this.mCorners[2] = new Vector3(num3, num4);
      this.mCorners[3] = new Vector3(num3, num2);
      return this.mCorners;
    }
  }

  public virtual Vector2 localSize
  {
    get
    {
      Vector3[] localCorners = this.localCorners;
      return Vector2.op_Implicit(Vector3.op_Subtraction(localCorners[2], localCorners[0]));
    }
  }

  public Vector3 localCenter
  {
    get
    {
      Vector3[] localCorners = this.localCorners;
      return Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
    }
  }

  public override Vector3[] worldCorners
  {
    get
    {
      Vector2 pivotOffset = this.pivotOffset;
      float num1 = -pivotOffset.x * (float) this.mWidth;
      float num2 = -pivotOffset.y * (float) this.mHeight;
      float num3 = num1 + (float) this.mWidth;
      float num4 = num2 + (float) this.mHeight;
      Transform cachedTransform = this.cachedTransform;
      this.mCorners[0] = cachedTransform.TransformPoint(num1, num2, 0.0f);
      this.mCorners[1] = cachedTransform.TransformPoint(num1, num4, 0.0f);
      this.mCorners[2] = cachedTransform.TransformPoint(num3, num4, 0.0f);
      this.mCorners[3] = cachedTransform.TransformPoint(num3, num2, 0.0f);
      return this.mCorners;
    }
  }

  public Vector3 worldCenter => this.cachedTransform.TransformPoint(this.localCenter);

  public virtual Vector4 drawingDimensions
  {
    get
    {
      Vector2 pivotOffset = this.pivotOffset;
      float num1 = -pivotOffset.x * (float) this.mWidth;
      float num2 = -pivotOffset.y * (float) this.mHeight;
      float num3 = num1 + (float) this.mWidth;
      float num4 = num2 + (float) this.mHeight;
      return new Vector4((double) this.mDrawRegion.x == 0.0 ? num1 : Mathf.Lerp(num1, num3, this.mDrawRegion.x), (double) this.mDrawRegion.y == 0.0 ? num2 : Mathf.Lerp(num2, num4, this.mDrawRegion.y), (double) this.mDrawRegion.z == 1.0 ? num3 : Mathf.Lerp(num1, num3, this.mDrawRegion.z), (double) this.mDrawRegion.w == 1.0 ? num4 : Mathf.Lerp(num2, num4, this.mDrawRegion.w));
    }
  }

  public virtual Material material
  {
    get => (Material) null;
    set
    {
      throw new NotImplementedException(((object) this).GetType().ToString() + " has no material setter");
    }
  }

  public virtual Texture mainTexture
  {
    get
    {
      Material material = this.material;
      if (Object.op_Inequality((Object) material, (Object) null) && Object.op_Inequality((Object) material.shader, (Object) null) && !material.shader.isSupported)
      {
        Log.Error("[UIWidget] no support shader : {0} : {1}", (object) ((Object) material.shader).name, (object) ((Object) this).name);
        return (Texture) null;
      }
      return !Object.op_Inequality((Object) material, (Object) null) ? (Texture) null : material.mainTexture;
    }
    set
    {
      throw new NotImplementedException(((object) this).GetType().ToString() + " has no mainTexture setter");
    }
  }

  public virtual Shader shader
  {
    get
    {
      Material material = this.material;
      return !Object.op_Inequality((Object) material, (Object) null) ? (Shader) null : material.shader;
    }
    set
    {
      throw new NotImplementedException(((object) this).GetType().ToString() + " has no shader setter");
    }
  }

  [Obsolete("There is no relative scale anymore. Widgets now have width and height instead")]
  public Vector2 relativeSize => Vector2.one;

  public bool hasBoxCollider
  {
    get
    {
      return Object.op_Inequality((Object) (((Component) this).GetComponent<Collider>() as BoxCollider), (Object) null) || Object.op_Inequality((Object) ((Component) this).GetComponent<BoxCollider2D>(), (Object) null);
    }
  }

  public void SetDimensions(int w, int h)
  {
    if (this.mWidth == w && this.mHeight == h)
      return;
    this.mWidth = w;
    this.mHeight = h;
    if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnWidth)
      this.mHeight = Mathf.RoundToInt((float) this.mWidth / this.aspectRatio);
    else if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
      this.mWidth = Mathf.RoundToInt((float) this.mHeight * this.aspectRatio);
    else if (this.keepAspectRatio == UIWidget.AspectRatioSource.Free)
      this.aspectRatio = (float) this.mWidth / (float) this.mHeight;
    this.mMoved = true;
    if (this.autoResizeBoxCollider)
      this.ResizeCollider();
    this.MarkAsChanged();
  }

  public override Vector3[] GetSides(Transform relativeTo)
  {
    Vector2 pivotOffset = this.pivotOffset;
    float num1 = -pivotOffset.x * (float) this.mWidth;
    float num2 = -pivotOffset.y * (float) this.mHeight;
    float num3 = num1 + (float) this.mWidth;
    float num4 = num2 + (float) this.mHeight;
    float num5 = (float) (((double) num1 + (double) num3) * 0.5);
    float num6 = (float) (((double) num2 + (double) num4) * 0.5);
    Transform cachedTransform = this.cachedTransform;
    this.mCorners[0] = cachedTransform.TransformPoint(num1, num6, 0.0f);
    this.mCorners[1] = cachedTransform.TransformPoint(num5, num4, 0.0f);
    this.mCorners[2] = cachedTransform.TransformPoint(num3, num6, 0.0f);
    this.mCorners[3] = cachedTransform.TransformPoint(num5, num2, 0.0f);
    if (Object.op_Inequality((Object) relativeTo, (Object) null))
    {
      for (int index = 0; index < 4; ++index)
        this.mCorners[index] = relativeTo.InverseTransformPoint(this.mCorners[index]);
    }
    return this.mCorners;
  }

  public override float CalculateFinalAlpha(int frameID)
  {
    if (this.mAlphaFrameID != frameID)
    {
      this.mAlphaFrameID = frameID;
      this.UpdateFinalAlpha(frameID);
    }
    return this.finalAlpha;
  }

  protected void UpdateFinalAlpha(int frameID)
  {
    if (!this.mIsVisibleByAlpha || !this.mIsInFront)
    {
      this.finalAlpha = 0.0f;
    }
    else
    {
      UIRect parent = this.parent;
      this.finalAlpha = Object.op_Inequality((Object) parent, (Object) null) ? parent.CalculateFinalAlpha(frameID) * this.mColor.a : this.mColor.a;
    }
  }

  public override void Invalidate(bool includeChildren)
  {
    this.mChanged = true;
    this.mAlphaFrameID = -1;
    if (!Object.op_Inequality((Object) this.panel, (Object) null))
      return;
    bool visibleByPanel = !this.hideIfOffScreen && !this.panel.hasCumulativeClipping || this.panel.IsVisible(this);
    this.UpdateVisibility((double) this.CalculateCumulativeAlpha(Time.frameCount) > 1.0 / 1000.0, visibleByPanel);
    this.UpdateFinalAlpha(Time.frameCount);
    if (!includeChildren)
      return;
    base.Invalidate(true);
  }

  public float CalculateCumulativeAlpha(int frameID)
  {
    UIRect parent = this.parent;
    return !Object.op_Inequality((Object) parent, (Object) null) ? this.mColor.a : parent.CalculateFinalAlpha(frameID) * this.mColor.a;
  }

  public override void SetRect(float x, float y, float width, float height)
  {
    Vector2 pivotOffset = this.pivotOffset;
    float num1 = Mathf.Lerp(x, x + width, pivotOffset.x);
    float num2 = Mathf.Lerp(y, y + height, pivotOffset.y);
    int num3 = Mathf.FloorToInt(width + 0.5f);
    int num4 = Mathf.FloorToInt(height + 0.5f);
    if ((double) pivotOffset.x == 0.5)
      num3 = num3 >> 1 << 1;
    if ((double) pivotOffset.y == 0.5)
      num4 = num4 >> 1 << 1;
    Transform cachedTransform = this.cachedTransform;
    Vector3 localPosition = cachedTransform.localPosition;
    localPosition.x = Mathf.Floor(num1 + 0.5f);
    localPosition.y = Mathf.Floor(num2 + 0.5f);
    if (num3 < this.minWidth)
      num3 = this.minWidth;
    if (num4 < this.minHeight)
      num4 = this.minHeight;
    cachedTransform.localPosition = localPosition;
    this.width = num3;
    this.height = num4;
    if (!this.isAnchored)
      return;
    Transform parent = cachedTransform.parent;
    if (Object.op_Implicit((Object) this.leftAnchor.target))
      this.leftAnchor.SetHorizontal(parent, x);
    if (Object.op_Implicit((Object) this.rightAnchor.target))
      this.rightAnchor.SetHorizontal(parent, x + width);
    if (Object.op_Implicit((Object) this.bottomAnchor.target))
      this.bottomAnchor.SetVertical(parent, y);
    if (!Object.op_Implicit((Object) this.topAnchor.target))
      return;
    this.topAnchor.SetVertical(parent, y + height);
  }

  public void ResizeCollider()
  {
    if (!NGUITools.GetActive((Behaviour) this))
      return;
    NGUITools.UpdateWidgetCollider(((Component) this).gameObject);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static int FullCompareFunc(UIWidget left, UIWidget right)
  {
    int num = UIPanel.CompareFunc(left.panel, right.panel);
    return num != 0 ? num : UIWidget.PanelCompareFunc(left, right);
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public static int PanelCompareFunc(UIWidget left, UIWidget right)
  {
    if (left.mDepth < right.mDepth)
      return -1;
    if (left.mDepth > right.mDepth)
      return 1;
    Material material1 = left.material;
    Material material2 = right.material;
    if (Object.op_Equality((Object) material1, (Object) material2))
      return 0;
    return Object.op_Inequality((Object) material1, (Object) null) || !Object.op_Inequality((Object) material2, (Object) null) && ((Object) material1).GetInstanceID() < ((Object) material2).GetInstanceID() ? -1 : 1;
  }

  public Bounds CalculateBounds() => this.CalculateBounds((Transform) null);

  public Bounds CalculateBounds(Transform relativeParent)
  {
    if (Object.op_Equality((Object) relativeParent, (Object) null))
    {
      Vector3[] localCorners = this.localCorners;
      Bounds bounds;
      // ISSUE: explicit constructor call
      ((Bounds) ref bounds).\u002Ector(localCorners[0], Vector3.zero);
      for (int index = 1; index < 4; ++index)
        ((Bounds) ref bounds).Encapsulate(localCorners[index]);
      return bounds;
    }
    Matrix4x4 worldToLocalMatrix = relativeParent.worldToLocalMatrix;
    Vector3[] worldCorners = this.worldCorners;
    Bounds bounds1;
    // ISSUE: explicit constructor call
    ((Bounds) ref bounds1).\u002Ector(((Matrix4x4) ref worldToLocalMatrix).MultiplyPoint3x4(worldCorners[0]), Vector3.zero);
    for (int index = 1; index < 4; ++index)
      ((Bounds) ref bounds1).Encapsulate(((Matrix4x4) ref worldToLocalMatrix).MultiplyPoint3x4(worldCorners[index]));
    return bounds1;
  }

  public void SetDirty()
  {
    if (Object.op_Inequality((Object) this.drawCall, (Object) null))
    {
      this.drawCall.isDirty = true;
    }
    else
    {
      if (!this.isVisible || !this.hasVertices)
        return;
      this.CreatePanel();
    }
  }

  public void RemoveFromPanel()
  {
    if (Object.op_Inequality((Object) this.panel, (Object) null))
    {
      this.panel.RemoveWidget(this);
      this.panel = (UIPanel) null;
    }
    this.drawCall = (UIDrawCall) null;
  }

  public virtual void MarkAsChanged()
  {
    if (!NGUITools.GetActive((Behaviour) this))
      return;
    this.mChanged = true;
    if (!Object.op_Inequality((Object) this.panel, (Object) null) || !((Behaviour) this).enabled || !NGUITools.GetActive(((Component) this).gameObject) || this.mPlayMode)
      return;
    this.SetDirty();
    this.CheckLayer();
  }

  public UIPanel CreatePanel()
  {
    if (this.mStarted && Object.op_Equality((Object) this.panel, (Object) null) && ((Behaviour) this).enabled && NGUITools.GetActive(((Component) this).gameObject))
    {
      this.panel = UIPanel.Find(this.cachedTransform, true, this.cachedGameObject.layer);
      if (Object.op_Inequality((Object) this.panel, (Object) null))
      {
        this.mParentFound = false;
        this.panel.AddWidget(this);
        this.CheckLayer();
        this.Invalidate(true);
      }
    }
    return this.panel;
  }

  public void CheckLayer()
  {
    if (!Object.op_Inequality((Object) this.panel, (Object) null) || ((Component) this.panel).gameObject.layer == ((Component) this).gameObject.layer)
      return;
    Debug.LogWarning((object) "You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", (Object) this);
    ((Component) this).gameObject.layer = ((Component) this.panel).gameObject.layer;
  }

  public override void ParentHasChanged()
  {
    base.ParentHasChanged();
    if (!Object.op_Inequality((Object) this.panel, (Object) null) || !Object.op_Inequality((Object) this.panel, (Object) UIPanel.Find(this.cachedTransform, true, this.cachedGameObject.layer)))
      return;
    this.RemoveFromPanel();
    this.CreatePanel();
  }

  protected virtual void Awake()
  {
    this.mGo = ((Component) this).gameObject;
    this.mPlayMode = Application.isPlaying;
  }

  protected override void OnInit()
  {
    base.OnInit();
    this.RemoveFromPanel();
    this.mMoved = true;
    if (this.mWidth == 100 && this.mHeight == 100)
    {
      Vector3 localScale = this.cachedTransform.localScale;
      if ((double) ((Vector3) ref localScale).magnitude > 8.0)
      {
        this.UpgradeFrom265();
        this.cachedTransform.localScale = Vector3.one;
      }
    }
    this._Update();
  }

  protected virtual void UpgradeFrom265()
  {
    Vector3 localScale = this.cachedTransform.localScale;
    this.mWidth = Mathf.Abs(Mathf.RoundToInt(localScale.x));
    this.mHeight = Mathf.Abs(Mathf.RoundToInt(localScale.y));
    NGUITools.UpdateWidgetCollider(((Component) this).gameObject, true);
  }

  protected override void OnStart() => this.CreatePanel();

  protected override void OnAnchor()
  {
    Transform cachedTransform = this.cachedTransform;
    Transform parent = cachedTransform.parent;
    Vector3 localPosition = cachedTransform.localPosition;
    Vector2 pivotOffset = this.pivotOffset;
    float num1;
    float num2;
    float num3;
    float num4;
    if (Object.op_Equality((Object) this.leftAnchor.target, (Object) this.bottomAnchor.target) && Object.op_Equality((Object) this.leftAnchor.target, (Object) this.rightAnchor.target) && Object.op_Equality((Object) this.leftAnchor.target, (Object) this.topAnchor.target))
    {
      Vector3[] sides = this.leftAnchor.GetSides(parent);
      if (sides != null)
      {
        num1 = NGUIMath.Lerp(sides[0].x, sides[2].x, this.leftAnchor.relative) + (float) this.leftAnchor.absolute;
        num2 = NGUIMath.Lerp(sides[0].x, sides[2].x, this.rightAnchor.relative) + (float) this.rightAnchor.absolute;
        num3 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.bottomAnchor.relative) + (float) this.bottomAnchor.absolute;
        num4 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.topAnchor.relative) + (float) this.topAnchor.absolute;
        this.mIsInFront = true;
      }
      else
      {
        Vector3 localPos = this.GetLocalPos(this.leftAnchor, parent);
        num1 = localPos.x + (float) this.leftAnchor.absolute;
        num3 = localPos.y + (float) this.bottomAnchor.absolute;
        num2 = localPos.x + (float) this.rightAnchor.absolute;
        num4 = localPos.y + (float) this.topAnchor.absolute;
        this.mIsInFront = !this.hideIfOffScreen || (double) localPos.z >= 0.0;
      }
    }
    else
    {
      this.mIsInFront = true;
      if (Object.op_Implicit((Object) this.leftAnchor.target))
      {
        Vector3[] sides = this.leftAnchor.GetSides(parent);
        num1 = sides == null ? this.GetLocalPos(this.leftAnchor, parent).x + (float) this.leftAnchor.absolute : NGUIMath.Lerp(sides[0].x, sides[2].x, this.leftAnchor.relative) + (float) this.leftAnchor.absolute;
      }
      else
        num1 = localPosition.x - pivotOffset.x * (float) this.mWidth;
      if (Object.op_Implicit((Object) this.rightAnchor.target))
      {
        Vector3[] sides = this.rightAnchor.GetSides(parent);
        num2 = sides == null ? this.GetLocalPos(this.rightAnchor, parent).x + (float) this.rightAnchor.absolute : NGUIMath.Lerp(sides[0].x, sides[2].x, this.rightAnchor.relative) + (float) this.rightAnchor.absolute;
      }
      else
        num2 = localPosition.x - pivotOffset.x * (float) this.mWidth + (float) this.mWidth;
      if (Object.op_Implicit((Object) this.bottomAnchor.target))
      {
        Vector3[] sides = this.bottomAnchor.GetSides(parent);
        num3 = sides == null ? this.GetLocalPos(this.bottomAnchor, parent).y + (float) this.bottomAnchor.absolute : NGUIMath.Lerp(sides[3].y, sides[1].y, this.bottomAnchor.relative) + (float) this.bottomAnchor.absolute;
      }
      else
        num3 = localPosition.y - pivotOffset.y * (float) this.mHeight;
      if (Object.op_Implicit((Object) this.topAnchor.target))
      {
        Vector3[] sides = this.topAnchor.GetSides(parent);
        num4 = sides == null ? this.GetLocalPos(this.topAnchor, parent).y + (float) this.topAnchor.absolute : NGUIMath.Lerp(sides[3].y, sides[1].y, this.topAnchor.relative) + (float) this.topAnchor.absolute;
      }
      else
        num4 = localPosition.y - pivotOffset.y * (float) this.mHeight + (float) this.mHeight;
    }
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(Mathf.Lerp(num1, num2, pivotOffset.x), Mathf.Lerp(num3, num4, pivotOffset.y), localPosition.z);
    vector3.x = Mathf.Round(vector3.x);
    vector3.y = Mathf.Round(vector3.y);
    int minWidth = Mathf.FloorToInt((float) ((double) num2 - (double) num1 + 0.5));
    int minHeight = Mathf.FloorToInt((float) ((double) num4 - (double) num3 + 0.5));
    if (this.keepAspectRatio != UIWidget.AspectRatioSource.Free && (double) this.aspectRatio != 0.0)
    {
      if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
        minWidth = Mathf.RoundToInt((float) minHeight * this.aspectRatio);
      else
        minHeight = Mathf.RoundToInt((float) minWidth / this.aspectRatio);
    }
    if (minWidth < this.minWidth)
      minWidth = this.minWidth;
    if (minHeight < this.minHeight)
      minHeight = this.minHeight;
    if ((double) Vector3.SqrMagnitude(Vector3.op_Subtraction(localPosition, vector3)) > 1.0 / 1000.0)
    {
      this.cachedTransform.localPosition = vector3;
      if (this.mIsInFront)
        this.mChanged = true;
    }
    if (this.mWidth == minWidth && this.mHeight == minHeight)
      return;
    this.mWidth = minWidth;
    this.mHeight = minHeight;
    if (this.mIsInFront)
      this.mChanged = true;
    if (!this.autoResizeBoxCollider)
      return;
    this.ResizeCollider();
  }

  protected override void OnUpdate()
  {
    if (!Object.op_Equality((Object) this.panel, (Object) null))
      return;
    this.CreatePanel();
  }

  private void OnApplicationPause(bool paused)
  {
    if (paused)
      return;
    this.MarkAsChanged();
  }

  protected override void OnDisable()
  {
    this.RemoveFromPanel();
    base.OnDisable();
  }

  private void OnDestroy() => this.RemoveFromPanel();

  public bool UpdateVisibility(bool visibleByAlpha, bool visibleByPanel)
  {
    if (this.mIsVisibleByAlpha == visibleByAlpha && this.mIsVisibleByPanel == visibleByPanel)
      return false;
    this.mChanged = true;
    this.mIsVisibleByAlpha = visibleByAlpha;
    this.mIsVisibleByPanel = visibleByPanel;
    return true;
  }

  public bool UpdateTransform(int frame)
  {
    Transform cachedTransform = this.cachedTransform;
    this.mPlayMode = Application.isPlaying;
    if (this.mMoved)
    {
      this.mMoved = true;
      this.mMatrixFrame = -1;
      cachedTransform.hasChanged = false;
      Vector2 pivotOffset = this.pivotOffset;
      float num1 = -pivotOffset.x * (float) this.mWidth;
      float num2 = -pivotOffset.y * (float) this.mHeight;
      float num3 = num1 + (float) this.mWidth;
      float num4 = num2 + (float) this.mHeight;
      this.mOldV0 = ((Matrix4x4) ref this.panel.worldToLocal).MultiplyPoint3x4(cachedTransform.TransformPoint(num1, num2, 0.0f));
      this.mOldV1 = ((Matrix4x4) ref this.panel.worldToLocal).MultiplyPoint3x4(cachedTransform.TransformPoint(num3, num4, 0.0f));
    }
    else if (!this.panel.widgetsAreStatic && cachedTransform.hasChanged)
    {
      this.mMoved = true;
      this.mMatrixFrame = -1;
      cachedTransform.hasChanged = false;
      Vector2 pivotOffset = this.pivotOffset;
      float num5 = -pivotOffset.x * (float) this.mWidth;
      float num6 = -pivotOffset.y * (float) this.mHeight;
      float num7 = num5 + (float) this.mWidth;
      float num8 = num6 + (float) this.mHeight;
      Vector3 vector3_1 = ((Matrix4x4) ref this.panel.worldToLocal).MultiplyPoint3x4(cachedTransform.TransformPoint(num5, num6, 0.0f));
      Vector3 vector3_2 = ((Matrix4x4) ref this.panel.worldToLocal).MultiplyPoint3x4(cachedTransform.TransformPoint(num7, num8, 0.0f));
      if ((double) Vector3.SqrMagnitude(Vector3.op_Subtraction(this.mOldV0, vector3_1)) > 9.9999999747524271E-07 || (double) Vector3.SqrMagnitude(Vector3.op_Subtraction(this.mOldV1, vector3_2)) > 9.9999999747524271E-07)
      {
        this.mMoved = true;
        this.mOldV0 = vector3_1;
        this.mOldV1 = vector3_2;
      }
    }
    if (this.mMoved && this.onChange != null)
      this.onChange();
    return this.mMoved || this.mChanged;
  }

  public bool UpdateGeometry(int frame)
  {
    float finalAlpha = this.CalculateFinalAlpha(frame);
    if (this.mIsVisibleByAlpha && (double) this.mLastAlpha != (double) finalAlpha)
      this.mChanged = true;
    this.mLastAlpha = finalAlpha;
    if (this.mChanged)
    {
      this.mChanged = false;
      if (this.mIsVisibleByAlpha && (double) finalAlpha > 1.0 / 1000.0 && Object.op_Inequality((Object) this.shader, (Object) null))
      {
        bool hasVertices = this.geometry.hasVertices;
        if (this.fillGeometry)
        {
          this.geometry.Clear();
          this.OnFill(this.geometry.verts, this.geometry.uvs, this.geometry.cols);
        }
        if (!this.geometry.hasVertices)
          return hasVertices;
        if (this.mMatrixFrame != frame)
        {
          this.mLocalToPanel = Matrix4x4.op_Multiply(this.panel.worldToLocal, this.cachedTransform.localToWorldMatrix);
          this.mMatrixFrame = frame;
        }
        this.geometry.ApplyTransform(this.mLocalToPanel, this.panel.generateNormals);
        this.mMoved = false;
        return true;
      }
      if (this.geometry.hasVertices)
      {
        if (this.fillGeometry)
          this.geometry.Clear();
        this.mMoved = false;
        return true;
      }
    }
    else if (this.mMoved && this.geometry.hasVertices)
    {
      if (this.mMatrixFrame != frame)
      {
        this.mLocalToPanel = Matrix4x4.op_Multiply(this.panel.worldToLocal, this.cachedTransform.localToWorldMatrix);
        this.mMatrixFrame = frame;
      }
      this.geometry.ApplyTransform(this.mLocalToPanel, this.panel.generateNormals);
      this.mMoved = false;
      return true;
    }
    this.mMoved = false;
    return false;
  }

  public void WriteToBuffers(
    BetterList<Vector3> v,
    BetterList<Vector2> u,
    BetterList<Color32> c,
    BetterList<Vector3> n,
    BetterList<Vector4> t)
  {
    this.geometry.WriteToBuffers(v, u, c, n, t);
  }

  public virtual void MakePixelPerfect()
  {
    Vector3 localPosition = this.cachedTransform.localPosition;
    localPosition.z = Mathf.Round(localPosition.z);
    localPosition.x = Mathf.Round(localPosition.x);
    localPosition.y = Mathf.Round(localPosition.y);
    this.cachedTransform.localPosition = localPosition;
    Vector3 localScale = this.cachedTransform.localScale;
    this.cachedTransform.localScale = new Vector3(Mathf.Sign(localScale.x), Mathf.Sign(localScale.y), 1f);
  }

  public virtual int minWidth => 2;

  public virtual int minHeight => 2;

  public virtual Vector4 border
  {
    get => Vector4.zero;
    set
    {
    }
  }

  public virtual void OnFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
  }

  public enum Pivot
  {
    TopLeft,
    Top,
    TopRight,
    Left,
    Center,
    Right,
    BottomLeft,
    Bottom,
    BottomRight,
  }

  public delegate void OnDimensionsChanged();

  public delegate void OnPostFillCallback(
    UIWidget widget,
    int bufferOffset,
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols);

  public enum AspectRatioSource
  {
    Free,
    BasedOnWidth,
    BasedOnHeight,
  }

  public delegate bool HitCheck(Vector3 worldPos);
}
