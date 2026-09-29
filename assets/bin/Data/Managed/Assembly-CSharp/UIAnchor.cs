// Decompiled with JetBrains decompiler
// Type: UIAnchor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor")]
public class UIAnchor : MonoBehaviour
{
  public Camera uiCamera;
  public GameObject container;
  public UIAnchor.Side side = UIAnchor.Side.Center;
  public bool runOnlyOnce = true;
  public Vector2 relativeOffset = Vector2.zero;
  public Vector2 pixelOffset = Vector2.zero;
  [HideInInspector]
  [SerializeField]
  private UIWidget widgetContainer;
  private Transform mTrans;
  private Animation mAnim;
  private Rect mRect;
  private UIRoot mRoot;
  private bool mStarted;

  private void Awake()
  {
    this.mTrans = ((Component) this).transform;
    this.mAnim = ((Component) this).GetComponent<Animation>();
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
    this.mRoot = NGUITools.FindInParents<UIRoot>(((Component) this).gameObject);
    if (Object.op_Equality((Object) this.uiCamera, (Object) null))
      this.uiCamera = NGUITools.FindCameraForLayer(((Component) this).gameObject.layer);
    this.Update();
    this.mStarted = true;
  }

  private void Update()
  {
    if (Object.op_Inequality((Object) this.mAnim, (Object) null) && ((Behaviour) this.mAnim).enabled && this.mAnim.isPlaying)
      return;
    bool flag = false;
    UIWidget component1 = Object.op_Equality((Object) this.container, (Object) null) ? (UIWidget) null : this.container.GetComponent<UIWidget>();
    UIPanel component2 = !Object.op_Equality((Object) this.container, (Object) null) || !Object.op_Equality((Object) component1, (Object) null) ? this.container.GetComponent<UIPanel>() : (UIPanel) null;
    if (Object.op_Inequality((Object) component1, (Object) null))
    {
      Bounds bounds = component1.CalculateBounds(this.container.transform.parent);
      ((Rect) ref this.mRect).x = ((Bounds) ref bounds).min.x;
      ((Rect) ref this.mRect).y = ((Bounds) ref bounds).min.y;
      ((Rect) ref this.mRect).width = ((Bounds) ref bounds).size.x;
      ((Rect) ref this.mRect).height = ((Bounds) ref bounds).size.y;
    }
    else if (Object.op_Inequality((Object) component2, (Object) null))
    {
      if (component2.clipping == UIDrawCall.Clipping.None)
      {
        float num = Object.op_Inequality((Object) this.mRoot, (Object) null) ? (float) ((double) this.mRoot.activeHeight / (double) Screen.height * 0.5) : 0.5f;
        ((Rect) ref this.mRect).xMin = (float) -Screen.width * num;
        ((Rect) ref this.mRect).yMin = (float) -Screen.height * num;
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
      Transform parent = this.container.transform.parent;
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
      flag = true;
      this.mRect = this.uiCamera.pixelRect;
    }
    float num1 = (float) (((double) ((Rect) ref this.mRect).xMin + (double) ((Rect) ref this.mRect).xMax) * 0.5);
    float num2 = (float) (((double) ((Rect) ref this.mRect).yMin + (double) ((Rect) ref this.mRect).yMax) * 0.5);
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(num1, num2, 0.0f);
    if (this.side != UIAnchor.Side.Center)
    {
      vector3.x = this.side == UIAnchor.Side.Right || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.BottomRight ? ((Rect) ref this.mRect).xMax : (this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Bottom ? num1 : ((Rect) ref this.mRect).xMin);
      vector3.y = this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.TopLeft ? ((Rect) ref this.mRect).yMax : (this.side == UIAnchor.Side.Left || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Right ? num2 : ((Rect) ref this.mRect).yMin);
    }
    float width = ((Rect) ref this.mRect).width;
    float height = ((Rect) ref this.mRect).height;
    vector3.x += this.pixelOffset.x + this.relativeOffset.x * width;
    vector3.y += this.pixelOffset.y + this.relativeOffset.y * height;
    if (flag)
    {
      if (this.uiCamera.orthographic)
      {
        vector3.x = Mathf.Round(vector3.x);
        vector3.y = Mathf.Round(vector3.y);
      }
      vector3.z = this.uiCamera.WorldToScreenPoint(this.mTrans.position).z;
      vector3 = this.uiCamera.ScreenToWorldPoint(vector3);
    }
    else
    {
      vector3.x = Mathf.Round(vector3.x);
      vector3.y = Mathf.Round(vector3.y);
      if (Object.op_Inequality((Object) component2, (Object) null))
        vector3 = component2.cachedTransform.TransformPoint(vector3);
      else if (Object.op_Inequality((Object) this.container, (Object) null))
      {
        Transform parent = this.container.transform.parent;
        if (Object.op_Inequality((Object) parent, (Object) null))
          vector3 = parent.TransformPoint(vector3);
      }
      vector3.z = this.mTrans.position.z;
    }
    if (flag && this.uiCamera.orthographic && Object.op_Inequality((Object) this.mTrans.parent, (Object) null))
    {
      vector3 = this.mTrans.parent.InverseTransformPoint(vector3);
      vector3.x = (float) Mathf.RoundToInt(vector3.x);
      vector3.y = (float) Mathf.RoundToInt(vector3.y);
      if (Vector3.op_Inequality(this.mTrans.localPosition, vector3))
        this.mTrans.localPosition = vector3;
    }
    else if (Vector3.op_Inequality(this.mTrans.position, vector3))
      this.mTrans.position = vector3;
    if (!this.runOnlyOnce || !Application.isPlaying)
      return;
    ((Behaviour) this).enabled = false;
  }

  public enum Side
  {
    BottomLeft,
    Left,
    TopLeft,
    Top,
    TopRight,
    Right,
    BottomRight,
    Bottom,
    Center,
  }
}
