// Decompiled with JetBrains decompiler
// Type: UIPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Panel")]
public class UIPanel : UIRect
{
  public static List<UIPanel> list = new List<UIPanel>();
  public UIPanel.OnGeometryUpdated onGeometryUpdated;
  public bool showInPanelTool = true;
  public bool generateNormals;
  public bool widgetsAreStatic;
  public bool cullWhileDragging = true;
  public bool alwaysOnScreen;
  public bool anchorOffset;
  public bool softBorderPadding = true;
  public UIPanel.RenderQueue renderQueue;
  public int startingRenderQueue = 3000;
  [NonSerialized]
  public List<UIWidget> widgets = new List<UIWidget>();
  [NonSerialized]
  public List<UIDrawCall> drawCalls = new List<UIDrawCall>();
  [NonSerialized]
  public Matrix4x4 worldToLocal = Matrix4x4.identity;
  [NonSerialized]
  public Vector4 drawCallClipRange = new Vector4(0.0f, 0.0f, 1f, 1f);
  public UIPanel.OnClippingMoved onClipMove;
  [HideInInspector]
  [SerializeField]
  private Texture2D mClipTexture;
  [HideInInspector]
  [SerializeField]
  private float mAlpha = 1f;
  [HideInInspector]
  [SerializeField]
  private UIDrawCall.Clipping mClipping;
  [HideInInspector]
  [SerializeField]
  private Vector4 mClipRange = new Vector4(0.0f, 0.0f, 300f, 200f);
  [HideInInspector]
  [SerializeField]
  private Vector2 mClipSoftness = new Vector2(4f, 4f);
  [HideInInspector]
  [SerializeField]
  private int mDepth;
  [HideInInspector]
  [SerializeField]
  private int mSortingOrder;
  private bool mRebuild;
  private bool mResized;
  [SerializeField]
  private Vector2 mClipOffset = Vector2.zero;
  private int mMatrixFrame = -1;
  private int mAlphaFrameID;
  private int mLayer = -1;
  private static float[] mTemp = new float[4];
  private Vector2 mMin = Vector2.zero;
  private Vector2 mMax = Vector2.zero;
  private bool mHalfPixelOffset;
  private bool mSortWidgets;
  private bool mUpdateScroll;
  private UIPanel mParentPanel;
  private static Vector3[] mCorners = new Vector3[4];
  private static int mUpdateFrame = -1;
  private UIDrawCall.OnRenderCallback mOnRender;
  private bool mForced;

  public static int nextUnusedDepth
  {
    get
    {
      int num = int.MinValue;
      int index = 0;
      for (int count = UIPanel.list.Count; index < count; ++index)
        num = Mathf.Max(num, UIPanel.list[index].depth);
      return num != int.MinValue ? num + 1 : 0;
    }
  }

  public override bool canBeAnchored => this.mClipping != 0;

  public override float alpha
  {
    get => this.mAlpha;
    set
    {
      float num = Mathf.Clamp01(value);
      if ((double) this.mAlpha == (double) num)
        return;
      this.mAlphaFrameID = -1;
      this.mResized = true;
      this.mAlpha = num;
      this.SetDirty();
    }
  }

  public int depth
  {
    get => this.mDepth;
    set
    {
      if (this.mDepth == value)
        return;
      this.mDepth = value;
      UIPanel.list.Sort(new Comparison<UIPanel>(UIPanel.CompareFunc));
    }
  }

  public int sortingOrder
  {
    get => this.mSortingOrder;
    set
    {
      if (this.mSortingOrder == value)
        return;
      this.mSortingOrder = value;
      this.UpdateDrawCalls();
    }
  }

  public static int CompareFunc(UIPanel a, UIPanel b)
  {
    if (!Object.op_Inequality((Object) a, (Object) b) || !Object.op_Inequality((Object) a, (Object) null) || !Object.op_Inequality((Object) b, (Object) null))
      return 0;
    return a.mDepth < b.mDepth || a.mDepth <= b.mDepth && ((Object) a).GetInstanceID() < ((Object) b).GetInstanceID() ? -1 : 1;
  }

  public float width => this.GetViewSize().x;

  public float height => this.GetViewSize().y;

  public bool halfPixelOffset => this.mHalfPixelOffset;

  public bool usedForUI
  {
    get
    {
      return Object.op_Inequality((Object) this.anchorCamera, (Object) null) && this.mCam.orthographic;
    }
  }

  public Vector3 drawCallOffset
  {
    get
    {
      if (!Object.op_Inequality((Object) this.anchorCamera, (Object) null) || !this.mCam.orthographic)
        return Vector3.zero;
      Vector2 windowSize = this.GetWindowSize();
      float num = (Object.op_Inequality((Object) this.root, (Object) null) ? this.root.pixelSizeAdjustment : 1f) / windowSize.y / this.mCam.orthographicSize;
      bool flag1 = this.mHalfPixelOffset;
      bool flag2 = this.mHalfPixelOffset;
      if ((Mathf.RoundToInt(windowSize.x) & 1) == 1)
        flag1 = !flag1;
      if ((Mathf.RoundToInt(windowSize.y) & 1) == 1)
        flag2 = !flag2;
      return new Vector3(flag1 ? -num : 0.0f, flag2 ? num : 0.0f);
    }
  }

  public UIDrawCall.Clipping clipping
  {
    get => this.mClipping;
    set
    {
      if (this.mClipping == value)
        return;
      this.mResized = true;
      this.mClipping = value;
      this.mMatrixFrame = -1;
    }
  }

  public UIPanel parentPanel => this.mParentPanel;

  public int clipCount
  {
    get
    {
      int clipCount = 0;
      for (UIPanel uiPanel = this; Object.op_Inequality((Object) uiPanel, (Object) null); uiPanel = uiPanel.mParentPanel)
      {
        if (uiPanel.mClipping == UIDrawCall.Clipping.SoftClip || uiPanel.mClipping == UIDrawCall.Clipping.TextureMask)
          ++clipCount;
      }
      return clipCount;
    }
  }

  public bool hasClipping
  {
    get
    {
      return this.mClipping == UIDrawCall.Clipping.SoftClip || this.mClipping == UIDrawCall.Clipping.TextureMask;
    }
  }

  public bool hasCumulativeClipping => this.clipCount != 0;

  [Obsolete("Use 'hasClipping' or 'hasCumulativeClipping' instead")]
  public bool clipsChildren => this.hasCumulativeClipping;

  public Vector2 clipOffset
  {
    get => this.mClipOffset;
    set
    {
      if ((double) Mathf.Abs(this.mClipOffset.x - value.x) <= 1.0 / 1000.0 && (double) Mathf.Abs(this.mClipOffset.y - value.y) <= 1.0 / 1000.0)
        return;
      this.mClipOffset = value;
      this.InvalidateClipping();
      if (this.onClipMove == null)
        return;
      this.onClipMove(this);
    }
  }

  private void InvalidateClipping()
  {
    this.mResized = true;
    this.mMatrixFrame = -1;
    int index = 0;
    for (int count = UIPanel.list.Count; index < count; ++index)
    {
      UIPanel uiPanel = UIPanel.list[index];
      if (Object.op_Inequality((Object) uiPanel, (Object) this) && Object.op_Equality((Object) uiPanel.parentPanel, (Object) this))
        uiPanel.InvalidateClipping();
    }
  }

  public Texture2D clipTexture
  {
    get => this.mClipTexture;
    set
    {
      if (!Object.op_Inequality((Object) this.mClipTexture, (Object) value))
        return;
      this.mClipTexture = value;
    }
  }

  [Obsolete("Use 'finalClipRegion' or 'baseClipRegion' instead")]
  public Vector4 clipRange
  {
    get => this.baseClipRegion;
    set => this.baseClipRegion = value;
  }

  public Vector4 baseClipRegion
  {
    get => this.mClipRange;
    set
    {
      if ((double) Mathf.Abs(this.mClipRange.x - value.x) <= 1.0 / 1000.0 && (double) Mathf.Abs(this.mClipRange.y - value.y) <= 1.0 / 1000.0 && (double) Mathf.Abs(this.mClipRange.z - value.z) <= 1.0 / 1000.0 && (double) Mathf.Abs(this.mClipRange.w - value.w) <= 1.0 / 1000.0)
        return;
      this.mResized = true;
      this.mClipRange = value;
      this.mMatrixFrame = -1;
      UIScrollView component = ((Component) this).GetComponent<UIScrollView>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.UpdatePosition();
      if (this.onClipMove == null)
        return;
      this.onClipMove(this);
    }
  }

  public Vector4 finalClipRegion
  {
    get
    {
      Vector2 viewSize = this.GetViewSize();
      return this.mClipping != UIDrawCall.Clipping.None ? new Vector4(this.mClipRange.x + this.mClipOffset.x, this.mClipRange.y + this.mClipOffset.y, viewSize.x, viewSize.y) : new Vector4(0.0f, 0.0f, viewSize.x, viewSize.y);
    }
  }

  public Vector2 clipSoftness
  {
    get => this.mClipSoftness;
    set
    {
      if (!Vector2.op_Inequality(this.mClipSoftness, value))
        return;
      this.mClipSoftness = value;
    }
  }

  public override Vector3[] localCorners
  {
    get
    {
      if (this.mClipping == UIDrawCall.Clipping.None)
      {
        Vector3[] worldCorners = this.worldCorners;
        Transform cachedTransform = this.cachedTransform;
        for (int index = 0; index < 4; ++index)
          worldCorners[index] = cachedTransform.InverseTransformPoint(worldCorners[index]);
        return worldCorners;
      }
      float num1 = (float) ((double) this.mClipOffset.x + (double) this.mClipRange.x - 0.5 * (double) this.mClipRange.z);
      float num2 = (float) ((double) this.mClipOffset.y + (double) this.mClipRange.y - 0.5 * (double) this.mClipRange.w);
      float num3 = num1 + this.mClipRange.z;
      float num4 = num2 + this.mClipRange.w;
      UIPanel.mCorners[0] = new Vector3(num1, num2);
      UIPanel.mCorners[1] = new Vector3(num1, num4);
      UIPanel.mCorners[2] = new Vector3(num3, num4);
      UIPanel.mCorners[3] = new Vector3(num3, num2);
      return UIPanel.mCorners;
    }
  }

  public override Vector3[] worldCorners
  {
    get
    {
      if (this.mClipping != UIDrawCall.Clipping.None)
      {
        float num1 = (float) ((double) this.mClipOffset.x + (double) this.mClipRange.x - 0.5 * (double) this.mClipRange.z);
        float num2 = (float) ((double) this.mClipOffset.y + (double) this.mClipRange.y - 0.5 * (double) this.mClipRange.w);
        float num3 = num1 + this.mClipRange.z;
        float num4 = num2 + this.mClipRange.w;
        Transform cachedTransform = this.cachedTransform;
        UIPanel.mCorners[0] = cachedTransform.TransformPoint(num1, num2, 0.0f);
        UIPanel.mCorners[1] = cachedTransform.TransformPoint(num1, num4, 0.0f);
        UIPanel.mCorners[2] = cachedTransform.TransformPoint(num3, num4, 0.0f);
        UIPanel.mCorners[3] = cachedTransform.TransformPoint(num3, num2, 0.0f);
      }
      else
      {
        if (Object.op_Inequality((Object) this.anchorCamera, (Object) null))
          return this.mCam.GetWorldCorners(this.cameraRayDistance);
        Vector2 viewSize = this.GetViewSize();
        float num5 = -0.5f * viewSize.x;
        float num6 = -0.5f * viewSize.y;
        float num7 = num5 + viewSize.x;
        float num8 = num6 + viewSize.y;
        UIPanel.mCorners[0] = new Vector3(num5, num6);
        UIPanel.mCorners[1] = new Vector3(num5, num8);
        UIPanel.mCorners[2] = new Vector3(num7, num8);
        UIPanel.mCorners[3] = new Vector3(num7, num6);
        if (this.anchorOffset && (Object.op_Equality((Object) this.mCam, (Object) null) || Object.op_Inequality((Object) ((Component) this.mCam).transform.parent, (Object) this.cachedTransform)))
        {
          Vector3 position = this.cachedTransform.position;
          for (int index = 0; index < 4; ++index)
          {
            ref Vector3 local = ref UIPanel.mCorners[index];
            local = Vector3.op_Addition(local, position);
          }
        }
      }
      return UIPanel.mCorners;
    }
  }

  public override Vector3[] GetSides(Transform relativeTo)
  {
    if (this.mClipping != UIDrawCall.Clipping.None)
    {
      float num1 = (float) ((double) this.mClipOffset.x + (double) this.mClipRange.x - 0.5 * (double) this.mClipRange.z);
      float num2 = (float) ((double) this.mClipOffset.y + (double) this.mClipRange.y - 0.5 * (double) this.mClipRange.w);
      float num3 = num1 + this.mClipRange.z;
      float num4 = num2 + this.mClipRange.w;
      float num5 = (float) (((double) num1 + (double) num3) * 0.5);
      float num6 = (float) (((double) num2 + (double) num4) * 0.5);
      Transform cachedTransform = this.cachedTransform;
      UIRect.mSides[0] = cachedTransform.TransformPoint(num1, num6, 0.0f);
      UIRect.mSides[1] = cachedTransform.TransformPoint(num5, num4, 0.0f);
      UIRect.mSides[2] = cachedTransform.TransformPoint(num3, num6, 0.0f);
      UIRect.mSides[3] = cachedTransform.TransformPoint(num5, num2, 0.0f);
      if (Object.op_Inequality((Object) relativeTo, (Object) null))
      {
        for (int index = 0; index < 4; ++index)
          UIRect.mSides[index] = relativeTo.InverseTransformPoint(UIRect.mSides[index]);
      }
      return UIRect.mSides;
    }
    if (!Object.op_Inequality((Object) this.anchorCamera, (Object) null) || !this.anchorOffset)
      return base.GetSides(relativeTo);
    Vector3[] sides = this.mCam.GetSides(this.cameraRayDistance);
    Vector3 position = this.cachedTransform.position;
    for (int index = 0; index < 4; ++index)
    {
      ref Vector3 local = ref sides[index];
      local = Vector3.op_Addition(local, position);
    }
    if (Object.op_Inequality((Object) relativeTo, (Object) null))
    {
      for (int index = 0; index < 4; ++index)
        sides[index] = relativeTo.InverseTransformPoint(sides[index]);
    }
    return sides;
  }

  public override void Invalidate(bool includeChildren)
  {
    this.mAlphaFrameID = -1;
    base.Invalidate(includeChildren);
  }

  public override float CalculateFinalAlpha(int frameID)
  {
    if (this.mAlphaFrameID != frameID)
    {
      this.mAlphaFrameID = frameID;
      UIRect parent = this.parent;
      this.finalAlpha = Object.op_Inequality((Object) this.parent, (Object) null) ? parent.CalculateFinalAlpha(frameID) * this.mAlpha : this.mAlpha;
    }
    return this.finalAlpha;
  }

  public override void SetRect(float x, float y, float width, float height)
  {
    int num1 = Mathf.FloorToInt(width + 0.5f);
    int num2 = Mathf.FloorToInt(height + 0.5f);
    int num3 = num1 >> 1 << 1;
    int num4 = num2 >> 1 << 1;
    Transform cachedTransform = this.cachedTransform;
    Vector3 localPosition = cachedTransform.localPosition;
    localPosition.x = Mathf.Floor(x + 0.5f);
    localPosition.y = Mathf.Floor(y + 0.5f);
    if (num3 < 2)
      num3 = 2;
    if (num4 < 2)
      num4 = 2;
    this.baseClipRegion = new Vector4(localPosition.x, localPosition.y, (float) num3, (float) num4);
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

  public bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
  {
    this.UpdateTransformMatrix();
    a = ((Matrix4x4) ref this.worldToLocal).MultiplyPoint3x4(a);
    b = ((Matrix4x4) ref this.worldToLocal).MultiplyPoint3x4(b);
    c = ((Matrix4x4) ref this.worldToLocal).MultiplyPoint3x4(c);
    d = ((Matrix4x4) ref this.worldToLocal).MultiplyPoint3x4(d);
    UIPanel.mTemp[0] = a.x;
    UIPanel.mTemp[1] = b.x;
    UIPanel.mTemp[2] = c.x;
    UIPanel.mTemp[3] = d.x;
    float num1 = Mathf.Min(UIPanel.mTemp);
    double num2 = (double) Mathf.Max(UIPanel.mTemp);
    UIPanel.mTemp[0] = a.y;
    UIPanel.mTemp[1] = b.y;
    UIPanel.mTemp[2] = c.y;
    UIPanel.mTemp[3] = d.y;
    float num3 = Mathf.Min(UIPanel.mTemp);
    float num4 = Mathf.Max(UIPanel.mTemp);
    double x = (double) this.mMin.x;
    return num2 >= x && (double) num4 >= (double) this.mMin.y && (double) num1 <= (double) this.mMax.x && (double) num3 <= (double) this.mMax.y;
  }

  public bool IsVisible(Vector3 worldPos)
  {
    if ((double) this.mAlpha < 1.0 / 1000.0)
      return false;
    if (this.mClipping == UIDrawCall.Clipping.None || this.mClipping == UIDrawCall.Clipping.ConstrainButDontClip)
      return true;
    this.UpdateTransformMatrix();
    Vector3 vector3 = ((Matrix4x4) ref this.worldToLocal).MultiplyPoint3x4(worldPos);
    return (double) vector3.x >= (double) this.mMin.x && (double) vector3.y >= (double) this.mMin.y && (double) vector3.x <= (double) this.mMax.x && (double) vector3.y <= (double) this.mMax.y;
  }

  public bool IsVisible(UIWidget w)
  {
    UIPanel uiPanel = this;
    Vector3[] vector3Array = (Vector3[]) null;
    while (Object.op_Inequality((Object) uiPanel, (Object) null))
    {
      if ((uiPanel.mClipping == UIDrawCall.Clipping.None || uiPanel.mClipping == UIDrawCall.Clipping.ConstrainButDontClip) && !w.hideIfOffScreen)
      {
        uiPanel = uiPanel.mParentPanel;
      }
      else
      {
        if (vector3Array == null)
          vector3Array = w.worldCorners;
        if (!uiPanel.IsVisible(vector3Array[0], vector3Array[1], vector3Array[2], vector3Array[3]))
          return false;
        uiPanel = uiPanel.mParentPanel;
      }
    }
    return true;
  }

  public bool Affects(UIWidget w)
  {
    if (Object.op_Equality((Object) w, (Object) null))
      return false;
    UIPanel panel = w.panel;
    if (Object.op_Equality((Object) panel, (Object) null))
      return false;
    for (UIPanel uiPanel = this; Object.op_Inequality((Object) uiPanel, (Object) null); uiPanel = uiPanel.mParentPanel)
    {
      if (Object.op_Equality((Object) uiPanel, (Object) panel))
        return true;
      if (!uiPanel.hasCumulativeClipping)
        return false;
    }
    return false;
  }

  [ContextMenu("Force Refresh")]
  public void RebuildAllDrawCalls() => this.mRebuild = true;

  public void SetDirty()
  {
    int index = 0;
    for (int count = this.drawCalls.Count; index < count; ++index)
      this.drawCalls[index].isDirty = true;
    this.Invalidate(true);
  }

  private void Awake()
  {
    this.mGo = ((Component) this).gameObject;
    this.mTrans = ((Component) this).transform;
    this.mHalfPixelOffset = Application.platform == 2 || Application.platform == 10 || Application.platform == 7;
    if (!this.mHalfPixelOffset || !SystemInfo.graphicsDeviceVersion.Contains("Direct3D"))
      return;
    this.mHalfPixelOffset = SystemInfo.graphicsShaderLevel < 40;
  }

  private void FindParent()
  {
    Transform parent = this.cachedTransform.parent;
    this.mParentPanel = Object.op_Inequality((Object) parent, (Object) null) ? NGUITools.FindInParents<UIPanel>(((Component) parent).gameObject) : (UIPanel) null;
  }

  public override void ParentHasChanged()
  {
    base.ParentHasChanged();
    this.FindParent();
  }

  protected override void OnStart() => this.mLayer = this.mGo.layer;

  protected override void OnEnable()
  {
    this.mRebuild = true;
    this.mAlphaFrameID = -1;
    this.mMatrixFrame = -1;
    this.OnStart();
    base.OnEnable();
    this.mMatrixFrame = -1;
  }

  protected override void OnInit()
  {
    if (UIPanel.list.Contains(this))
      return;
    base.OnInit();
    this.FindParent();
    if (Object.op_Equality((Object) ((Component) this).GetComponent<Rigidbody>(), (Object) null) && Object.op_Equality((Object) this.mParentPanel, (Object) null))
    {
      UICamera component = Object.op_Inequality((Object) this.anchorCamera, (Object) null) ? ((Component) this.mCam).GetComponent<UICamera>() : (UICamera) null;
      if (Object.op_Inequality((Object) component, (Object) null) && (component.eventType == UICamera.EventType.UI_3D || component.eventType == UICamera.EventType.World_3D))
      {
        Rigidbody rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
      }
    }
    this.mRebuild = true;
    this.mAlphaFrameID = -1;
    this.mMatrixFrame = -1;
    UIPanel.list.Add(this);
    UIPanel.list.Sort(new Comparison<UIPanel>(UIPanel.CompareFunc));
  }

  protected override void OnDisable()
  {
    int index = 0;
    for (int count = this.drawCalls.Count; index < count; ++index)
    {
      UIDrawCall drawCall = this.drawCalls[index];
      if (Object.op_Inequality((Object) drawCall, (Object) null))
        UIDrawCall.Destroy(drawCall);
    }
    this.drawCalls.Clear();
    UIPanel.list.Remove(this);
    this.mAlphaFrameID = -1;
    this.mMatrixFrame = -1;
    if (UIPanel.list.Count == 0)
    {
      UIDrawCall.ReleaseAll();
      UIPanel.mUpdateFrame = -1;
    }
    base.OnDisable();
  }

  private void UpdateTransformMatrix()
  {
    int frameCount = Time.frameCount;
    if (this.mMatrixFrame == frameCount)
      return;
    this.mMatrixFrame = frameCount;
    this.worldToLocal = this.cachedTransform.worldToLocalMatrix;
    Vector2 vector2 = Vector2.op_Multiply(this.GetViewSize(), 0.5f);
    float num1 = this.mClipOffset.x + this.mClipRange.x;
    float num2 = this.mClipOffset.y + this.mClipRange.y;
    this.mMin.x = num1 - vector2.x;
    this.mMin.y = num2 - vector2.y;
    this.mMax.x = num1 + vector2.x;
    this.mMax.y = num2 + vector2.y;
  }

  protected override void OnAnchor()
  {
    if (this.mClipping == UIDrawCall.Clipping.None)
      return;
    Transform cachedTransform = this.cachedTransform;
    Transform parent = cachedTransform.parent;
    Vector2 viewSize = this.GetViewSize();
    Vector2 vector2_1 = Vector2.op_Implicit(cachedTransform.localPosition);
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
      }
      else
      {
        Vector2 vector2_2 = Vector2.op_Implicit(this.GetLocalPos(this.leftAnchor, parent));
        num1 = vector2_2.x + (float) this.leftAnchor.absolute;
        num3 = vector2_2.y + (float) this.bottomAnchor.absolute;
        num2 = vector2_2.x + (float) this.rightAnchor.absolute;
        num4 = vector2_2.y + (float) this.topAnchor.absolute;
      }
    }
    else
    {
      if (Object.op_Implicit((Object) this.leftAnchor.target))
      {
        Vector3[] sides = this.leftAnchor.GetSides(parent);
        num1 = sides == null ? this.GetLocalPos(this.leftAnchor, parent).x + (float) this.leftAnchor.absolute : NGUIMath.Lerp(sides[0].x, sides[2].x, this.leftAnchor.relative) + (float) this.leftAnchor.absolute;
      }
      else
        num1 = this.mClipRange.x - 0.5f * viewSize.x;
      if (Object.op_Implicit((Object) this.rightAnchor.target))
      {
        Vector3[] sides = this.rightAnchor.GetSides(parent);
        num2 = sides == null ? this.GetLocalPos(this.rightAnchor, parent).x + (float) this.rightAnchor.absolute : NGUIMath.Lerp(sides[0].x, sides[2].x, this.rightAnchor.relative) + (float) this.rightAnchor.absolute;
      }
      else
        num2 = this.mClipRange.x + 0.5f * viewSize.x;
      if (Object.op_Implicit((Object) this.bottomAnchor.target))
      {
        Vector3[] sides = this.bottomAnchor.GetSides(parent);
        num3 = sides == null ? this.GetLocalPos(this.bottomAnchor, parent).y + (float) this.bottomAnchor.absolute : NGUIMath.Lerp(sides[3].y, sides[1].y, this.bottomAnchor.relative) + (float) this.bottomAnchor.absolute;
      }
      else
        num3 = this.mClipRange.y - 0.5f * viewSize.y;
      if (Object.op_Implicit((Object) this.topAnchor.target))
      {
        Vector3[] sides = this.topAnchor.GetSides(parent);
        num4 = sides == null ? this.GetLocalPos(this.topAnchor, parent).y + (float) this.topAnchor.absolute : NGUIMath.Lerp(sides[3].y, sides[1].y, this.topAnchor.relative) + (float) this.topAnchor.absolute;
      }
      else
        num4 = this.mClipRange.y + 0.5f * viewSize.y;
    }
    float num5 = num1 - (vector2_1.x + this.mClipOffset.x);
    float num6 = num2 - (vector2_1.x + this.mClipOffset.x);
    float num7 = num3 - (vector2_1.y + this.mClipOffset.y);
    float num8 = num4 - (vector2_1.y + this.mClipOffset.y);
    float num9 = Mathf.Lerp(num5, num6, 0.5f);
    float num10 = Mathf.Lerp(num7, num8, 0.5f);
    float num11 = num6 - num5;
    float num12 = num8 - num7;
    float num13 = Mathf.Max(2f, this.mClipSoftness.x);
    float num14 = Mathf.Max(2f, this.mClipSoftness.y);
    if ((double) num11 < (double) num13)
      num11 = num13;
    if ((double) num12 < (double) num14)
      num12 = num14;
    this.baseClipRegion = new Vector4(num9, num10, num11, num12);
  }

  private void LateUpdate()
  {
    if (UIPanel.mUpdateFrame == Time.frameCount)
      return;
    UIPanel.mUpdateFrame = Time.frameCount;
    int index1 = 0;
    for (int count = UIPanel.list.Count; index1 < count; ++index1)
      UIPanel.list[index1].UpdateSelf();
    int num = 3000;
    int index2 = 0;
    for (int count = UIPanel.list.Count; index2 < count; ++index2)
    {
      UIPanel uiPanel = UIPanel.list[index2];
      if (uiPanel.renderQueue == UIPanel.RenderQueue.Automatic)
      {
        uiPanel.startingRenderQueue = num;
        uiPanel.UpdateDrawCalls();
        num += uiPanel.drawCalls.Count;
      }
      else if (uiPanel.renderQueue == UIPanel.RenderQueue.StartAt)
      {
        uiPanel.UpdateDrawCalls();
        if (uiPanel.drawCalls.Count != 0)
          num = Mathf.Max(num, uiPanel.startingRenderQueue + uiPanel.drawCalls.Count);
      }
      else
      {
        uiPanel.UpdateDrawCalls();
        if (uiPanel.drawCalls.Count != 0)
          num = Mathf.Max(num, uiPanel.startingRenderQueue + 1);
      }
    }
  }

  private void UpdateSelf()
  {
    this.UpdateTransformMatrix();
    this.UpdateLayers();
    this.UpdateWidgets();
    if (this.mRebuild)
    {
      this.mRebuild = false;
      this.FillAllDrawCalls();
    }
    else
    {
      int index = 0;
      while (index < this.drawCalls.Count)
      {
        UIDrawCall drawCall = this.drawCalls[index];
        if (drawCall.isDirty && !this.FillDrawCall(drawCall))
        {
          UIDrawCall.Destroy(drawCall);
          this.drawCalls.RemoveAt(index);
        }
        else
          ++index;
      }
    }
    if (!this.mUpdateScroll)
      return;
    this.mUpdateScroll = false;
    UIScrollView component = ((Component) this).GetComponent<UIScrollView>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.UpdateScrollbars();
  }

  public void SortWidgets()
  {
    this.mSortWidgets = false;
    this.widgets.Sort(new Comparison<UIWidget>(UIWidget.PanelCompareFunc));
  }

  private void FillAllDrawCalls()
  {
    for (int index = 0; index < this.drawCalls.Count; ++index)
      UIDrawCall.Destroy(this.drawCalls[index]);
    this.drawCalls.Clear();
    Material mat = (Material) null;
    Texture tex = (Texture) null;
    Shader shader1 = (Shader) null;
    UIDrawCall uiDrawCall = (UIDrawCall) null;
    int widgetCount = 0;
    if (this.mSortWidgets)
      this.SortWidgets();
    for (int index = 0; index < this.widgets.Count; ++index)
    {
      UIWidget widget = this.widgets[index];
      if (widget.isVisible && widget.hasVertices)
      {
        Material material = widget.material;
        Texture mainTexture = widget.mainTexture;
        Shader shader2 = widget.shader;
        if (Object.op_Inequality((Object) mat, (Object) material) || Object.op_Inequality((Object) tex, (Object) mainTexture) || Object.op_Inequality((Object) shader1, (Object) shader2))
        {
          if (Object.op_Inequality((Object) uiDrawCall, (Object) null) && uiDrawCall.verts.size != 0)
          {
            this.drawCalls.Add(uiDrawCall);
            uiDrawCall.UpdateGeometry(widgetCount);
            uiDrawCall.onRender = this.mOnRender;
            this.mOnRender = (UIDrawCall.OnRenderCallback) null;
            widgetCount = 0;
            uiDrawCall = (UIDrawCall) null;
          }
          mat = material;
          tex = mainTexture;
          shader1 = shader2;
        }
        if (Object.op_Inequality((Object) mat, (Object) null) || Object.op_Inequality((Object) shader1, (Object) null) || Object.op_Inequality((Object) tex, (Object) null))
        {
          if (Object.op_Equality((Object) uiDrawCall, (Object) null))
          {
            uiDrawCall = UIDrawCall.Create(this, mat, tex, shader1);
            uiDrawCall.depthStart = widget.depth;
            uiDrawCall.depthEnd = uiDrawCall.depthStart;
            uiDrawCall.panel = this;
          }
          else
          {
            int depth = widget.depth;
            if (depth < uiDrawCall.depthStart)
              uiDrawCall.depthStart = depth;
            if (depth > uiDrawCall.depthEnd)
              uiDrawCall.depthEnd = depth;
          }
          widget.drawCall = uiDrawCall;
          ++widgetCount;
          if (this.generateNormals)
            widget.WriteToBuffers(uiDrawCall.verts, uiDrawCall.uvs, uiDrawCall.cols, uiDrawCall.norms, uiDrawCall.tans);
          else
            widget.WriteToBuffers(uiDrawCall.verts, uiDrawCall.uvs, uiDrawCall.cols, (BetterList<Vector3>) null, (BetterList<Vector4>) null);
          if (widget.mOnRender != null)
          {
            if (this.mOnRender == null)
              this.mOnRender = widget.mOnRender;
            else
              this.mOnRender += widget.mOnRender;
          }
        }
      }
      else
        widget.drawCall = (UIDrawCall) null;
    }
    if (!Object.op_Inequality((Object) uiDrawCall, (Object) null) || uiDrawCall.verts.size == 0)
      return;
    this.drawCalls.Add(uiDrawCall);
    uiDrawCall.UpdateGeometry(widgetCount);
    uiDrawCall.onRender = this.mOnRender;
    this.mOnRender = (UIDrawCall.OnRenderCallback) null;
  }

  private bool FillDrawCall(UIDrawCall dc)
  {
    if (Object.op_Inequality((Object) dc, (Object) null))
    {
      dc.isDirty = false;
      int widgetCount = 0;
      int index = 0;
      while (index < this.widgets.Count)
      {
        UIWidget widget = this.widgets[index];
        if (Object.op_Equality((Object) widget, (Object) null))
        {
          this.widgets.RemoveAt(index);
        }
        else
        {
          if (Object.op_Equality((Object) widget.drawCall, (Object) dc))
          {
            if (widget.isVisible && widget.hasVertices)
            {
              ++widgetCount;
              if (this.generateNormals)
                widget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, dc.norms, dc.tans);
              else
                widget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, (BetterList<Vector3>) null, (BetterList<Vector4>) null);
              if (widget.mOnRender != null)
              {
                if (this.mOnRender == null)
                  this.mOnRender = widget.mOnRender;
                else
                  this.mOnRender += widget.mOnRender;
              }
            }
            else
              widget.drawCall = (UIDrawCall) null;
          }
          ++index;
        }
      }
      if (dc.verts.size != 0)
      {
        dc.UpdateGeometry(widgetCount);
        dc.onRender = this.mOnRender;
        this.mOnRender = (UIDrawCall.OnRenderCallback) null;
        return true;
      }
    }
    return false;
  }

  private void UpdateDrawCalls()
  {
    Transform cachedTransform1 = this.cachedTransform;
    int num = this.usedForUI ? 1 : 0;
    if (this.clipping != UIDrawCall.Clipping.None)
    {
      this.drawCallClipRange = this.finalClipRegion;
      this.drawCallClipRange.z *= 0.5f;
      this.drawCallClipRange.w *= 0.5f;
    }
    else
      this.drawCallClipRange = Vector4.zero;
    int width = Screen.width;
    int height = Screen.height;
    if ((double) this.drawCallClipRange.z == 0.0)
      this.drawCallClipRange.z = (float) width * 0.5f;
    if ((double) this.drawCallClipRange.w == 0.0)
      this.drawCallClipRange.w = (float) height * 0.5f;
    if (this.halfPixelOffset)
    {
      this.drawCallClipRange.x -= 0.5f;
      this.drawCallClipRange.y += 0.5f;
    }
    Vector3 vector3_1;
    if (num != 0)
    {
      Transform parent = this.cachedTransform.parent;
      Vector3 vector3_2 = this.cachedTransform.localPosition;
      if (this.clipping != UIDrawCall.Clipping.None)
      {
        vector3_2.x = (float) Mathf.RoundToInt(vector3_2.x);
        vector3_2.y = (float) Mathf.RoundToInt(vector3_2.y);
      }
      if (Object.op_Inequality((Object) parent, (Object) null))
        vector3_2 = parent.TransformPoint(vector3_2);
      vector3_1 = Vector3.op_Addition(vector3_2, this.drawCallOffset);
    }
    else
      vector3_1 = cachedTransform1.position;
    Quaternion rotation = cachedTransform1.rotation;
    Vector3 lossyScale = cachedTransform1.lossyScale;
    for (int index = 0; index < this.drawCalls.Count; ++index)
    {
      UIDrawCall drawCall = this.drawCalls[index];
      Transform cachedTransform2 = drawCall.cachedTransform;
      cachedTransform2.position = vector3_1;
      cachedTransform2.rotation = rotation;
      cachedTransform2.localScale = lossyScale;
      drawCall.renderQueue = this.renderQueue == UIPanel.RenderQueue.Explicit ? this.startingRenderQueue : this.startingRenderQueue + index;
      drawCall.alwaysOnScreen = this.alwaysOnScreen && (this.mClipping == UIDrawCall.Clipping.None || this.mClipping == UIDrawCall.Clipping.ConstrainButDontClip);
      drawCall.sortingOrder = this.mSortingOrder;
      drawCall.clipTexture = this.mClipTexture;
    }
  }

  private void UpdateLayers()
  {
    if (this.mLayer == this.cachedGameObject.layer)
      return;
    this.mLayer = this.mGo.layer;
    int index1 = 0;
    for (int count = this.widgets.Count; index1 < count; ++index1)
    {
      UIWidget widget = this.widgets[index1];
      if (Object.op_Implicit((Object) widget) && Object.op_Equality((Object) widget.parent, (Object) this))
        ((Component) widget).gameObject.layer = this.mLayer;
    }
    this.ResetAnchors();
    for (int index2 = 0; index2 < this.drawCalls.Count; ++index2)
      ((Component) this.drawCalls[index2]).gameObject.layer = this.mLayer;
  }

  private void UpdateWidgets()
  {
    bool flag1 = false;
    bool flag2 = false;
    bool cumulativeClipping = this.hasCumulativeClipping;
    if (!this.cullWhileDragging)
    {
      for (int i = 0; i < UIScrollView.list.size; ++i)
      {
        UIScrollView uiScrollView = UIScrollView.list[i];
        if (Object.op_Equality((Object) uiScrollView.panel, (Object) this) && uiScrollView.isDragging)
          flag2 = true;
      }
    }
    if (this.mForced != flag2)
    {
      this.mForced = flag2;
      this.mResized = true;
    }
    int frameCount = Time.frameCount;
    int index = 0;
    for (int count = this.widgets.Count; index < count; ++index)
    {
      UIWidget widget = this.widgets[index];
      if (Object.op_Equality((Object) widget.panel, (Object) this) && ((Behaviour) widget).enabled)
      {
        if (widget.UpdateTransform(frameCount) || this.mResized)
        {
          bool visibleByAlpha = flag2 || (double) widget.CalculateCumulativeAlpha(frameCount) > 1.0 / 1000.0;
          widget.UpdateVisibility(visibleByAlpha, flag2 || !cumulativeClipping && !widget.hideIfOffScreen || this.IsVisible(widget));
        }
        if (widget.UpdateGeometry(frameCount))
        {
          flag1 = true;
          if (!this.mRebuild)
          {
            if (Object.op_Inequality((Object) widget.drawCall, (Object) null))
              widget.drawCall.isDirty = true;
            else
              this.FindDrawCall(widget);
          }
        }
      }
    }
    if (flag1 && this.onGeometryUpdated != null)
      this.onGeometryUpdated();
    this.mResized = false;
  }

  public UIDrawCall FindDrawCall(UIWidget w)
  {
    Material material = w.material;
    Texture mainTexture = w.mainTexture;
    int depth = w.depth;
    for (int index = 0; index < this.drawCalls.Count; ++index)
    {
      UIDrawCall drawCall = this.drawCalls[index];
      int num1 = index == 0 ? int.MinValue : this.drawCalls[index - 1].depthEnd + 1;
      int num2 = index + 1 == this.drawCalls.Count ? int.MaxValue : this.drawCalls[index + 1].depthStart - 1;
      int num3 = depth;
      if (num1 <= num3 && num2 >= depth)
      {
        if (Object.op_Equality((Object) drawCall.baseMaterial, (Object) material) && Object.op_Equality((Object) drawCall.mainTexture, (Object) mainTexture))
        {
          if (w.isVisible)
          {
            w.drawCall = drawCall;
            if (w.hasVertices)
              drawCall.isDirty = true;
            return drawCall;
          }
        }
        else
          this.mRebuild = true;
        return (UIDrawCall) null;
      }
    }
    this.mRebuild = true;
    return (UIDrawCall) null;
  }

  public void AddWidget(UIWidget w)
  {
    this.mUpdateScroll = true;
    if (this.widgets.Count == 0)
      this.widgets.Add(w);
    else if (this.mSortWidgets)
    {
      this.widgets.Add(w);
      this.SortWidgets();
    }
    else if (UIWidget.PanelCompareFunc(w, this.widgets[0]) == -1)
    {
      this.widgets.Insert(0, w);
    }
    else
    {
      int count = this.widgets.Count;
      while (count > 0)
      {
        if (UIWidget.PanelCompareFunc(w, this.widgets[--count]) != -1)
        {
          this.widgets.Insert(count + 1, w);
          break;
        }
      }
    }
    this.FindDrawCall(w);
  }

  public void RemoveWidget(UIWidget w)
  {
    if (!this.widgets.Remove(w) || !Object.op_Inequality((Object) w.drawCall, (Object) null))
      return;
    int depth = w.depth;
    if (depth == w.drawCall.depthStart || depth == w.drawCall.depthEnd)
      this.mRebuild = true;
    w.drawCall.isDirty = true;
    w.drawCall = (UIDrawCall) null;
  }

  public void Refresh()
  {
    this.mRebuild = true;
    UIPanel.mUpdateFrame = -1;
    if (UIPanel.list.Count <= 0)
      return;
    UIPanel.list[0].LateUpdate();
  }

  public void ForceUpDate() => UIPanel.mUpdateFrame = -1;

  public virtual Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
  {
    Vector4 finalClipRegion = this.finalClipRegion;
    float num1 = finalClipRegion.z * 0.5f;
    float num2 = finalClipRegion.w * 0.5f;
    Vector2 minRect = new Vector2(min.x, min.y);
    Vector2 vector2_1;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_1).\u002Ector(max.x, max.y);
    Vector2 vector2_2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_2).\u002Ector(finalClipRegion.x - num1, finalClipRegion.y - num2);
    Vector2 vector2_3;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_3).\u002Ector(finalClipRegion.x + num1, finalClipRegion.y + num2);
    if (this.softBorderPadding && this.clipping == UIDrawCall.Clipping.SoftClip)
    {
      vector2_2.x += this.mClipSoftness.x;
      vector2_2.y += this.mClipSoftness.y;
      vector2_3.x -= this.mClipSoftness.x;
      vector2_3.y -= this.mClipSoftness.y;
    }
    Vector2 maxRect = vector2_1;
    Vector2 minArea = vector2_2;
    Vector2 maxArea = vector2_3;
    return Vector2.op_Implicit(NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea));
  }

  public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
  {
    Vector3 vector3_1 = ((Bounds) ref targetBounds).min;
    Vector3 vector3_2 = ((Bounds) ref targetBounds).max;
    float num = 1f;
    if (this.mClipping == UIDrawCall.Clipping.None)
    {
      UIRoot root = this.root;
      if (Object.op_Inequality((Object) root, (Object) null))
        num = root.pixelSizeAdjustment;
    }
    if ((double) num != 1.0)
    {
      vector3_1 = Vector3.op_Division(vector3_1, num);
      vector3_2 = Vector3.op_Division(vector3_2, num);
    }
    Vector3 vector3_3 = Vector3.op_Multiply(this.CalculateConstrainOffset(Vector2.op_Implicit(vector3_1), Vector2.op_Implicit(vector3_2)), num);
    if ((double) ((Vector3) ref vector3_3).sqrMagnitude <= 0.0)
      return false;
    if (immediate)
    {
      Transform transform = target;
      transform.localPosition = Vector3.op_Addition(transform.localPosition, vector3_3);
      ref Bounds local = ref targetBounds;
      ((Bounds) ref local).center = Vector3.op_Addition(((Bounds) ref local).center, vector3_3);
      SpringPosition component = ((Component) target).GetComponent<SpringPosition>();
      if (Object.op_Inequality((Object) component, (Object) null))
        ((Behaviour) component).enabled = false;
    }
    else
    {
      SpringPosition springPosition = SpringPosition.Begin(((Component) target).gameObject, Vector3.op_Addition(target.localPosition, vector3_3), 13f);
      springPosition.ignoreTimeScale = true;
      springPosition.worldSpace = false;
    }
    return true;
  }

  public bool ConstrainTargetToBounds(Transform target, bool immediate)
  {
    Bounds relativeWidgetBounds = NGUIMath.CalculateRelativeWidgetBounds(this.cachedTransform, target);
    return this.ConstrainTargetToBounds(target, ref relativeWidgetBounds, immediate);
  }

  public static UIPanel Find(Transform trans) => UIPanel.Find(trans, false, -1);

  public static UIPanel Find(Transform trans, bool createIfMissing)
  {
    return UIPanel.Find(trans, createIfMissing, -1);
  }

  public static UIPanel Find(Transform trans, bool createIfMissing, int layer)
  {
    UIPanel inParents = NGUITools.FindInParents<UIPanel>(trans);
    if (Object.op_Inequality((Object) inParents, (Object) null))
      return inParents;
    while (Object.op_Inequality((Object) trans.parent, (Object) null))
      trans = trans.parent;
    return !createIfMissing ? (UIPanel) null : NGUITools.CreateUI(trans, false, layer);
  }

  public Vector2 GetWindowSize()
  {
    UIRoot root = this.root;
    Vector2 windowSize = NGUITools.screenSize;
    if (Object.op_Inequality((Object) root, (Object) null))
      windowSize = Vector2.op_Multiply(windowSize, root.GetPixelSizeAdjustment(Mathf.RoundToInt(windowSize.y)));
    return windowSize;
  }

  public Vector2 GetViewSize()
  {
    return this.mClipping != UIDrawCall.Clipping.None ? new Vector2(this.mClipRange.z, this.mClipRange.w) : NGUITools.screenSize;
  }

  public enum RenderQueue
  {
    Automatic,
    StartAt,
    Explicit,
  }

  public delegate void OnGeometryUpdated();

  public delegate void OnClippingMoved(UIPanel panel);
}
