// Decompiled with JetBrains decompiler
// Type: UIDragDropItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
  public UIDragDropItem.Restriction restriction;
  public bool cloneOnDrag;
  [HideInInspector]
  public float pressAndHoldDelay = 1f;
  public bool interactable = true;
  [NonSerialized]
  protected Transform mTrans;
  [NonSerialized]
  protected Transform mParent;
  [NonSerialized]
  protected Collider mCollider;
  [NonSerialized]
  protected Collider2D mCollider2D;
  [NonSerialized]
  protected UIButton mButton;
  [NonSerialized]
  protected UIRoot mRoot;
  [NonSerialized]
  protected UIGrid mGrid;
  [NonSerialized]
  protected UITable mTable;
  [NonSerialized]
  protected float mDragStartTime;
  [NonSerialized]
  protected UIDragScrollView mDragScrollView;
  [NonSerialized]
  protected bool mPressed;
  [NonSerialized]
  protected bool mDragging;
  [NonSerialized]
  protected UICamera.MouseOrTouch mTouch;
  public static List<UIDragDropItem> draggedItems = new List<UIDragDropItem>();

  protected virtual void Awake()
  {
    this.mTrans = ((Component) this).transform;
    this.mCollider = ((Component) this).gameObject.GetComponent<Collider>();
    this.mCollider2D = ((Component) this).gameObject.GetComponent<Collider2D>();
  }

  protected virtual void OnEnable()
  {
  }

  protected virtual void OnDisable()
  {
    if (!this.mDragging)
      return;
    this.StopDragging(UICamera.hoveredObject);
  }

  protected virtual void Start()
  {
    this.mButton = ((Component) this).GetComponent<UIButton>();
    this.mDragScrollView = ((Component) this).GetComponent<UIDragScrollView>();
  }

  protected virtual void OnPress(bool isPressed)
  {
    if (!this.interactable || UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
      return;
    if (isPressed)
    {
      if (this.mPressed)
        return;
      this.mTouch = UICamera.currentTouch;
      this.mDragStartTime = RealTime.time + this.pressAndHoldDelay;
      this.mPressed = true;
    }
    else
    {
      if (!this.mPressed || this.mTouch != UICamera.currentTouch)
        return;
      this.mPressed = false;
      this.mTouch = (UICamera.MouseOrTouch) null;
    }
  }

  protected virtual void Update()
  {
    if (this.restriction != UIDragDropItem.Restriction.PressAndHold || !this.mPressed || this.mDragging || (double) this.mDragStartTime >= (double) RealTime.time)
      return;
    this.StartDragging();
  }

  protected virtual void OnDragStart()
  {
    if (!this.interactable || !((Behaviour) this).enabled || this.mTouch != UICamera.currentTouch)
      return;
    if (this.restriction != UIDragDropItem.Restriction.None)
    {
      if (this.restriction == UIDragDropItem.Restriction.Horizontal)
      {
        Vector2 totalDelta = this.mTouch.totalDelta;
        if ((double) Mathf.Abs(totalDelta.x) < (double) Mathf.Abs(totalDelta.y))
          return;
      }
      else if (this.restriction == UIDragDropItem.Restriction.Vertical)
      {
        Vector2 totalDelta = this.mTouch.totalDelta;
        if ((double) Mathf.Abs(totalDelta.x) > (double) Mathf.Abs(totalDelta.y))
          return;
      }
      else if (this.restriction == UIDragDropItem.Restriction.PressAndHold)
        return;
    }
    this.StartDragging();
  }

  public virtual void StartDragging()
  {
    if (!this.interactable || this.mDragging)
      return;
    if (this.cloneOnDrag)
    {
      this.mPressed = false;
      GameObject gameObject = NGUITools.AddChild(((Component) ((Component) this).transform.parent).gameObject, ((Component) this).gameObject);
      gameObject.transform.localPosition = ((Component) this).transform.localPosition;
      gameObject.transform.localRotation = ((Component) this).transform.localRotation;
      gameObject.transform.localScale = ((Component) this).transform.localScale;
      UIButtonColor component1 = gameObject.GetComponent<UIButtonColor>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        component1.defaultColor = ((Component) this).GetComponent<UIButtonColor>().defaultColor;
      if (this.mTouch != null && Object.op_Equality((Object) this.mTouch.pressed, (Object) ((Component) this).gameObject))
      {
        this.mTouch.current = gameObject;
        this.mTouch.pressed = gameObject;
        this.mTouch.dragged = gameObject;
        this.mTouch.last = gameObject;
      }
      UIDragDropItem component2 = gameObject.GetComponent<UIDragDropItem>();
      component2.mTouch = this.mTouch;
      component2.mPressed = true;
      component2.mDragging = true;
      component2.Start();
      component2.OnDragDropStart();
      if (UICamera.currentTouch == null)
        UICamera.currentTouch = this.mTouch;
      this.mTouch = (UICamera.MouseOrTouch) null;
      UICamera.Notify(((Component) this).gameObject, "OnPress", (object) false);
      UICamera.Notify(((Component) this).gameObject, "OnHover", (object) false);
    }
    else
    {
      this.mDragging = true;
      this.OnDragDropStart();
    }
  }

  protected virtual void OnDrag(Vector2 delta)
  {
    if (!this.interactable || !this.mDragging || !((Behaviour) this).enabled || this.mTouch != UICamera.currentTouch)
      return;
    this.OnDragDropMove(Vector2.op_Multiply(delta, this.mRoot.pixelSizeAdjustment));
  }

  protected virtual void OnDragEnd()
  {
    if (!this.interactable || !((Behaviour) this).enabled || this.mTouch != UICamera.currentTouch)
      return;
    this.StopDragging(UICamera.hoveredObject);
  }

  public void StopDragging(GameObject go)
  {
    if (!this.mDragging)
      return;
    this.mDragging = false;
    this.OnDragDropRelease(go);
  }

  protected virtual void OnDragDropStart()
  {
    if (!UIDragDropItem.draggedItems.Contains(this))
      UIDragDropItem.draggedItems.Add(this);
    if (Object.op_Inequality((Object) this.mDragScrollView, (Object) null))
      ((Behaviour) this.mDragScrollView).enabled = false;
    if (Object.op_Inequality((Object) this.mButton, (Object) null))
      this.mButton.isEnabled = false;
    else if (Object.op_Inequality((Object) this.mCollider, (Object) null))
      this.mCollider.enabled = false;
    else if (Object.op_Inequality((Object) this.mCollider2D, (Object) null))
      ((Behaviour) this.mCollider2D).enabled = false;
    this.mParent = this.mTrans.parent;
    this.mRoot = NGUITools.FindInParents<UIRoot>(this.mParent);
    this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
    this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
    if (Object.op_Inequality((Object) UIDragDropRoot.root, (Object) null))
      this.mTrans.parent = UIDragDropRoot.root;
    Vector3 localPosition = this.mTrans.localPosition;
    localPosition.z = 0.0f;
    this.mTrans.localPosition = localPosition;
    TweenPosition component1 = ((Component) this).GetComponent<TweenPosition>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      ((Behaviour) component1).enabled = false;
    SpringPosition component2 = ((Component) this).GetComponent<SpringPosition>();
    if (Object.op_Inequality((Object) component2, (Object) null))
      ((Behaviour) component2).enabled = false;
    NGUITools.MarkParentAsChanged(((Component) this).gameObject);
    if (Object.op_Inequality((Object) this.mTable, (Object) null))
      this.mTable.repositionNow = true;
    if (!Object.op_Inequality((Object) this.mGrid, (Object) null))
      return;
    this.mGrid.repositionNow = true;
  }

  protected virtual void OnDragDropMove(Vector2 delta)
  {
    Transform mTrans = this.mTrans;
    mTrans.localPosition = Vector3.op_Addition(mTrans.localPosition, Vector2.op_Implicit(delta));
  }

  protected virtual void OnDragDropRelease(GameObject surface)
  {
    if (!this.cloneOnDrag)
    {
      if (Object.op_Inequality((Object) this.mButton, (Object) null))
        this.mButton.isEnabled = true;
      else if (Object.op_Inequality((Object) this.mCollider, (Object) null))
        this.mCollider.enabled = true;
      else if (Object.op_Inequality((Object) this.mCollider2D, (Object) null))
        ((Behaviour) this.mCollider2D).enabled = true;
      UIDragDropContainer inParents = Object.op_Implicit((Object) surface) ? NGUITools.FindInParents<UIDragDropContainer>(surface) : (UIDragDropContainer) null;
      if (Object.op_Inequality((Object) inParents, (Object) null))
      {
        this.mTrans.parent = Object.op_Inequality((Object) inParents.reparentTarget, (Object) null) ? inParents.reparentTarget : ((Component) inParents).transform;
        Vector3 localPosition = this.mTrans.localPosition;
        localPosition.z = 0.0f;
        this.mTrans.localPosition = localPosition;
      }
      else
        this.mTrans.parent = this.mParent;
      this.mParent = this.mTrans.parent;
      this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
      this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
      if (Object.op_Inequality((Object) this.mDragScrollView, (Object) null))
        this.StartCoroutine(this.EnableDragScrollView());
      NGUITools.MarkParentAsChanged(((Component) this).gameObject);
      if (Object.op_Inequality((Object) this.mTable, (Object) null))
        this.mTable.repositionNow = true;
      if (Object.op_Inequality((Object) this.mGrid, (Object) null))
        this.mGrid.repositionNow = true;
      this.OnDragDropEnd();
    }
    else
      NGUITools.Destroy((Object) ((Component) this).gameObject);
  }

  protected virtual void OnDragDropEnd() => UIDragDropItem.draggedItems.Remove(this);

  protected IEnumerator EnableDragScrollView()
  {
    yield return (object) new WaitForEndOfFrame();
    if (Object.op_Inequality((Object) this.mDragScrollView, (Object) null))
      ((Behaviour) this.mDragScrollView).enabled = true;
  }

  public enum Restriction
  {
    None,
    Horizontal,
    Vertical,
    PressAndHold,
  }
}
