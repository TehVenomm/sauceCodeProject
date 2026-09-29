// Decompiled with JetBrains decompiler
// Type: UIStretch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Stretch")]
public class UIStretch : MonoBehaviour
{
  public Camera uiCamera;
  public GameObject container;
  public UIStretch.Style style;
  public bool runOnlyOnce = true;
  public Vector2 relativeSize = Vector2.one;
  public Vector2 initialSize = Vector2.one;
  public Vector2 borderPadding = Vector2.zero;
  [HideInInspector]
  [SerializeField]
  private UIWidget widgetContainer;
  private Transform mTrans;
  private UIWidget mWidget;
  private UISprite mSprite;
  private UIPanel mPanel;
  private UIRoot mRoot;
  private Animation mAnim;
  private Rect mRect;
  private bool mStarted;

  private void Awake()
  {
    this.mAnim = ((Component) this).GetComponent<Animation>();
    this.mRect = new Rect();
    this.mTrans = ((Component) this).transform;
    this.mWidget = ((Component) this).GetComponent<UIWidget>();
    this.mSprite = ((Component) this).GetComponent<UISprite>();
    this.mPanel = ((Component) this).GetComponent<UIPanel>();
    UICamera.onScreenResize += new UICamera.OnScreenResize(this.ScreenSizeChanged);
  }

  private void OnDestroy()
  {
    UICamera.onScreenResize -= new UICamera.OnScreenResize(this.ScreenSizeChanged);
  }

  private void ScreenSizeChanged()
  {
    if (!this.mStarted || !this.runOnlyOnce)
      return;
    this.Update();
  }

  private void Start()
  {
    if (Object.op_Equality((Object) this.container, (Object) null) && Object.op_Inequality((Object) this.widgetContainer, (Object) null))
    {
      this.container = ((Component) this.widgetContainer).gameObject;
      this.widgetContainer = (UIWidget) null;
    }
    if (Object.op_Equality((Object) this.uiCamera, (Object) null))
      this.uiCamera = NGUITools.FindCameraForLayer(((Component) this).gameObject.layer);
    this.mRoot = NGUITools.FindInParents<UIRoot>(((Component) this).gameObject);
    this.Update();
    this.mStarted = true;
  }

  private void Update()
  {
    if (Object.op_Inequality((Object) this.mAnim, (Object) null) && this.mAnim.isPlaying || this.style == UIStretch.Style.None)
      return;
    UIWidget component1 = Object.op_Equality((Object) this.container, (Object) null) ? (UIWidget) null : this.container.GetComponent<UIWidget>();
    UIPanel component2 = !Object.op_Equality((Object) this.container, (Object) null) || !Object.op_Equality((Object) component1, (Object) null) ? this.container.GetComponent<UIPanel>() : (UIPanel) null;
    float num1 = 1f;
    if (Object.op_Inequality((Object) component1, (Object) null))
    {
      Bounds bounds = component1.CalculateBounds(((Component) this).transform.parent);
      ((Rect) ref this.mRect).x = ((Bounds) ref bounds).min.x;
      ((Rect) ref this.mRect).y = ((Bounds) ref bounds).min.y;
      ((Rect) ref this.mRect).width = ((Bounds) ref bounds).size.x;
      ((Rect) ref this.mRect).height = ((Bounds) ref bounds).size.y;
    }
    else if (Object.op_Inequality((Object) component2, (Object) null))
    {
      if (component2.clipping == UIDrawCall.Clipping.None)
      {
        float num2 = Object.op_Inequality((Object) this.mRoot, (Object) null) ? (float) ((double) this.mRoot.activeHeight / (double) Screen.height * 0.5) : 0.5f;
        ((Rect) ref this.mRect).xMin = (float) -Screen.width * num2;
        ((Rect) ref this.mRect).yMin = (float) -Screen.height * num2;
        ((Rect) ref this.mRect).xMax = -((Rect) ref this.mRect).xMin;
        ((Rect) ref this.mRect).yMax = -((Rect) ref this.mRect).yMin;
      }
      else
      {
        Vector4 finalClipRegion = component2.finalClipRegion;
        ((Rect) ref this.mRect).x = finalClipRegion.x - finalClipRegion.z * 0.5f;
        ((Rect) ref this.mRect).y = finalClipRegion.y - finalClipRegion.w * 0.5f;
        ((Rect) ref this.mRect).width = finalClipRegion.z;
        ((Rect) ref this.mRect).height = finalClipRegion.w;
      }
    }
    else if (Object.op_Inequality((Object) this.container, (Object) null))
    {
      Transform parent = ((Component) this).transform.parent;
      Bounds bounds = Object.op_Inequality((Object) parent, (Object) null) ? NGUIMath.CalculateRelativeWidgetBounds(parent, this.container.transform) : NGUIMath.CalculateRelativeWidgetBounds(this.container.transform);
      ((Rect) ref this.mRect).x = ((Bounds) ref bounds).min.x;
      ((Rect) ref this.mRect).y = ((Bounds) ref bounds).min.y;
      ((Rect) ref this.mRect).width = ((Bounds) ref bounds).size.x;
      ((Rect) ref this.mRect).height = ((Bounds) ref bounds).size.y;
    }
    else
    {
      if (!Object.op_Inequality((Object) this.uiCamera, (Object) null))
        return;
      this.mRect = this.uiCamera.pixelRect;
      if (Object.op_Inequality((Object) this.mRoot, (Object) null))
        num1 = this.mRoot.pixelSizeAdjustment;
    }
    float width = ((Rect) ref this.mRect).width;
    float height = ((Rect) ref this.mRect).height;
    if ((double) num1 != 1.0 && (double) height > 1.0)
    {
      float num3 = (float) this.mRoot.activeHeight / height;
      width *= num3;
      height *= num3;
    }
    Vector3 vector3 = Object.op_Inequality((Object) this.mWidget, (Object) null) ? new Vector3((float) this.mWidget.width, (float) this.mWidget.height) : this.mTrans.localScale;
    if (this.style == UIStretch.Style.BasedOnHeight)
    {
      vector3.x = this.relativeSize.x * height;
      vector3.y = this.relativeSize.y * height;
    }
    else if (this.style == UIStretch.Style.FillKeepingRatio)
    {
      if ((double) this.initialSize.x / (double) this.initialSize.y < (double) (width / height))
      {
        float num4 = width / this.initialSize.x;
        vector3.x = width;
        vector3.y = this.initialSize.y * num4;
      }
      else
      {
        float num5 = height / this.initialSize.y;
        vector3.x = this.initialSize.x * num5;
        vector3.y = height;
      }
    }
    else if (this.style == UIStretch.Style.FitInternalKeepingRatio)
    {
      if ((double) this.initialSize.x / (double) this.initialSize.y > (double) (width / height))
      {
        float num6 = width / this.initialSize.x;
        vector3.x = width;
        vector3.y = this.initialSize.y * num6;
      }
      else
      {
        float num7 = height / this.initialSize.y;
        vector3.x = this.initialSize.x * num7;
        vector3.y = height;
      }
    }
    else
    {
      if (this.style != UIStretch.Style.Vertical)
        vector3.x = this.relativeSize.x * width;
      if (this.style != UIStretch.Style.Horizontal)
        vector3.y = this.relativeSize.y * height;
    }
    if (Object.op_Inequality((Object) this.mSprite, (Object) null))
    {
      float num8 = Object.op_Inequality((Object) this.mSprite.atlas, (Object) null) ? this.mSprite.atlas.pixelSize : 1f;
      vector3.x -= this.borderPadding.x * num8;
      vector3.y -= this.borderPadding.y * num8;
      if (this.style != UIStretch.Style.Vertical)
        this.mSprite.width = Mathf.RoundToInt(vector3.x);
      if (this.style != UIStretch.Style.Horizontal)
        this.mSprite.height = Mathf.RoundToInt(vector3.y);
      vector3 = Vector3.one;
    }
    else if (Object.op_Inequality((Object) this.mWidget, (Object) null))
    {
      if (this.style != UIStretch.Style.Vertical)
        this.mWidget.width = Mathf.RoundToInt(vector3.x - this.borderPadding.x);
      if (this.style != UIStretch.Style.Horizontal)
        this.mWidget.height = Mathf.RoundToInt(vector3.y - this.borderPadding.y);
      vector3 = Vector3.one;
    }
    else if (Object.op_Inequality((Object) this.mPanel, (Object) null))
    {
      Vector4 baseClipRegion = this.mPanel.baseClipRegion;
      if (this.style != UIStretch.Style.Vertical)
        baseClipRegion.z = vector3.x - this.borderPadding.x;
      if (this.style != UIStretch.Style.Horizontal)
        baseClipRegion.w = vector3.y - this.borderPadding.y;
      this.mPanel.baseClipRegion = baseClipRegion;
      vector3 = Vector3.one;
    }
    else
    {
      if (this.style != UIStretch.Style.Vertical)
        vector3.x -= this.borderPadding.x;
      if (this.style != UIStretch.Style.Horizontal)
        vector3.y -= this.borderPadding.y;
    }
    if (Vector3.op_Inequality(this.mTrans.localScale, vector3))
      this.mTrans.localScale = vector3;
    if (!this.runOnlyOnce || !Application.isPlaying)
      return;
    ((Behaviour) this).enabled = false;
  }

  public enum Style
  {
    None,
    Horizontal,
    Vertical,
    Both,
    BasedOnHeight,
    FillKeepingRatio,
    FitInternalKeepingRatio,
  }
}
