// Decompiled with JetBrains decompiler
// Type: UIDragObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : MonoBehaviour
{
  public Transform target;
  public UIPanel panelRegion;
  public Vector3 scrollMomentum = Vector3.zero;
  public bool restrictWithinPanel;
  public UIRect contentRect;
  public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;
  public float momentumAmount = 35f;
  [SerializeField]
  protected Vector3 scale = new Vector3(1f, 1f, 0.0f);
  [SerializeField]
  [HideInInspector]
  private float scrollWheelFactor;
  private Plane mPlane;
  private Vector3 mTargetPos;
  private Vector3 mLastPos;
  private Vector3 mMomentum = Vector3.zero;
  private Vector3 mScroll = Vector3.zero;
  private Bounds mBounds;
  private int mTouchID;
  private bool mStarted;
  private bool mPressed;

  public Vector3 dragMovement
  {
    get => this.scale;
    set => this.scale = value;
  }

  private void OnEnable()
  {
    if ((double) this.scrollWheelFactor != 0.0)
    {
      this.scrollMomentum = Vector3.op_Multiply(this.scale, this.scrollWheelFactor);
      this.scrollWheelFactor = 0.0f;
    }
    if (Object.op_Equality((Object) this.contentRect, (Object) null) && Object.op_Inequality((Object) this.target, (Object) null) && Application.isPlaying)
    {
      UIWidget component = ((Component) this.target).GetComponent<UIWidget>();
      if (Object.op_Inequality((Object) component, (Object) null))
        this.contentRect = (UIRect) component;
    }
    this.mTargetPos = Object.op_Inequality((Object) this.target, (Object) null) ? this.target.position : Vector3.zero;
  }

  private void OnDisable() => this.mStarted = false;

  private void FindPanel()
  {
    this.panelRegion = Object.op_Inequality((Object) this.target, (Object) null) ? UIPanel.Find(((Component) this.target).transform.parent) : (UIPanel) null;
    if (!Object.op_Equality((Object) this.panelRegion, (Object) null))
      return;
    this.restrictWithinPanel = false;
  }

  private void UpdateBounds()
  {
    if (Object.op_Implicit((Object) this.contentRect))
    {
      Matrix4x4 worldToLocalMatrix = this.panelRegion.cachedTransform.worldToLocalMatrix;
      Vector3[] worldCorners = this.contentRect.worldCorners;
      for (int index = 0; index < 4; ++index)
        worldCorners[index] = ((Matrix4x4) ref worldToLocalMatrix).MultiplyPoint3x4(worldCorners[index]);
      this.mBounds = new Bounds(worldCorners[0], Vector3.zero);
      for (int index = 1; index < 4; ++index)
        ((Bounds) ref this.mBounds).Encapsulate(worldCorners[index]);
    }
    else
      this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.panelRegion.cachedTransform, this.target);
  }

  private void OnPress(bool pressed)
  {
    if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
      return;
    float timeScale = Time.timeScale;
    if ((double) timeScale < 0.0099999997764825821 && (double) timeScale != 0.0 || !((Behaviour) this).enabled || !NGUITools.GetActive(((Component) this).gameObject) || !Object.op_Inequality((Object) this.target, (Object) null))
      return;
    if (pressed)
    {
      if (this.mPressed)
        return;
      this.mTouchID = UICamera.currentTouchID;
      this.mPressed = true;
      this.mStarted = false;
      this.CancelMovement();
      if (this.restrictWithinPanel && Object.op_Equality((Object) this.panelRegion, (Object) null))
        this.FindPanel();
      if (this.restrictWithinPanel)
        this.UpdateBounds();
      this.CancelSpring();
      Transform transform = ((Component) UICamera.currentCamera).transform;
      this.mPlane = new Plane(Quaternion.op_Multiply(Object.op_Inequality((Object) this.panelRegion, (Object) null) ? this.panelRegion.cachedTransform.rotation : transform.rotation, Vector3.back), UICamera.lastWorldPosition);
    }
    else
    {
      if (!this.mPressed || this.mTouchID != UICamera.currentTouchID)
        return;
      this.mPressed = false;
      if (!this.restrictWithinPanel || this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring || !this.panelRegion.ConstrainTargetToBounds(this.target, ref this.mBounds, false))
        return;
      this.CancelMovement();
    }
  }

  private void OnDrag(Vector2 delta)
  {
    if (!this.mPressed || this.mTouchID != UICamera.currentTouchID || !((Behaviour) this).enabled || !NGUITools.GetActive(((Component) this).gameObject) || !Object.op_Inequality((Object) this.target, (Object) null))
      return;
    UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
    Ray ray = UICamera.currentCamera.ScreenPointToRay(Vector2.op_Implicit(UICamera.currentTouch.pos));
    float num = 0.0f;
    if (!((Plane) ref this.mPlane).Raycast(ray, ref num))
      return;
    Vector3 point = ((Ray) ref ray).GetPoint(num);
    Vector3 worldDelta = Vector3.op_Subtraction(point, this.mLastPos);
    this.mLastPos = point;
    if (!this.mStarted)
    {
      this.mStarted = true;
      worldDelta = Vector3.zero;
    }
    if ((double) worldDelta.x != 0.0 || (double) worldDelta.y != 0.0)
    {
      Vector3 vector3 = this.target.InverseTransformDirection(worldDelta);
      ((Vector3) ref vector3).Scale(this.scale);
      worldDelta = this.target.TransformDirection(vector3);
    }
    if (this.dragEffect != UIDragObject.DragEffect.None)
      this.mMomentum = Vector3.Lerp(this.mMomentum, Vector3.op_Addition(this.mMomentum, Vector3.op_Multiply(worldDelta, 0.01f * this.momentumAmount)), 0.67f);
    Vector3 localPosition = this.target.localPosition;
    this.Move(worldDelta);
    if (!this.restrictWithinPanel)
      return;
    ((Bounds) ref this.mBounds).center = Vector3.op_Addition(((Bounds) ref this.mBounds).center, Vector3.op_Subtraction(this.target.localPosition, localPosition));
    if (this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring || !this.panelRegion.ConstrainTargetToBounds(this.target, ref this.mBounds, true))
      return;
    this.CancelMovement();
  }

  private void Move(Vector3 worldDelta)
  {
    if (Object.op_Inequality((Object) this.panelRegion, (Object) null))
    {
      this.mTargetPos = Vector3.op_Addition(this.mTargetPos, worldDelta);
      this.target.position = this.mTargetPos;
      Vector3 localPosition = this.target.localPosition;
      localPosition.x = Mathf.Round(localPosition.x);
      localPosition.y = Mathf.Round(localPosition.y);
      this.target.localPosition = localPosition;
      UIScrollView component = ((Component) this.panelRegion).GetComponent<UIScrollView>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.UpdateScrollbars(true);
    }
    else
    {
      Transform target = this.target;
      target.position = Vector3.op_Addition(target.position, worldDelta);
    }
  }

  private void LateUpdate()
  {
    if (Object.op_Equality((Object) this.target, (Object) null))
      return;
    float deltaTime = RealTime.deltaTime;
    this.mMomentum = Vector3.op_Subtraction(this.mMomentum, this.mScroll);
    this.mScroll = NGUIMath.SpringLerp(this.mScroll, Vector3.zero, 20f, deltaTime);
    if ((double) ((Vector3) ref this.mMomentum).magnitude < 9.9999997473787516E-05)
      return;
    if (!this.mPressed)
    {
      if (Object.op_Equality((Object) this.panelRegion, (Object) null))
        this.FindPanel();
      this.Move(NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime));
      if (this.restrictWithinPanel && Object.op_Inequality((Object) this.panelRegion, (Object) null))
      {
        this.UpdateBounds();
        if (this.panelRegion.ConstrainTargetToBounds(this.target, ref this.mBounds, this.dragEffect == UIDragObject.DragEffect.None))
          this.CancelMovement();
        else
          this.CancelSpring();
      }
      NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
      if ((double) ((Vector3) ref this.mMomentum).magnitude >= 9.9999997473787516E-05)
        return;
      this.CancelMovement();
    }
    else
      NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
  }

  public void CancelMovement()
  {
    if (Object.op_Inequality((Object) this.target, (Object) null))
    {
      Vector3 localPosition = this.target.localPosition;
      localPosition.x = (float) Mathf.RoundToInt(localPosition.x);
      localPosition.y = (float) Mathf.RoundToInt(localPosition.y);
      localPosition.z = (float) Mathf.RoundToInt(localPosition.z);
      this.target.localPosition = localPosition;
    }
    this.mTargetPos = Object.op_Inequality((Object) this.target, (Object) null) ? this.target.position : Vector3.zero;
    this.mMomentum = Vector3.zero;
    this.mScroll = Vector3.zero;
  }

  public void CancelSpring()
  {
    SpringPosition component = ((Component) this.target).GetComponent<SpringPosition>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    ((Behaviour) component).enabled = false;
  }

  private void OnScroll(float delta)
  {
    if (!((Behaviour) this).enabled || !NGUITools.GetActive(((Component) this).gameObject))
      return;
    this.mScroll = Vector3.op_Subtraction(this.mScroll, Vector3.op_Multiply(this.scrollMomentum, delta * 0.05f));
  }

  public enum DragEffect
  {
    None,
    Momentum,
    MomentumAndSpring,
  }
}
