using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
	public enum Restriction
	{
		None,
		Horizontal,
		Vertical,
		PressAndHold
	}

	public Restriction restriction;

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

	public UIDragDropItem()
		: this()
	{
	}

	protected virtual void Awake()
	{
		mTrans = this.get_transform();
		mCollider = this.get_gameObject().GetComponent<Collider>();
		mCollider2D = this.get_gameObject().GetComponent<Collider2D>();
	}

	protected virtual void OnEnable()
	{
	}

	protected virtual void OnDisable()
	{
		if (mDragging)
		{
			StopDragging(UICamera.hoveredObject);
		}
	}

	protected virtual void Start()
	{
		mButton = this.GetComponent<UIButton>();
		mDragScrollView = this.GetComponent<UIDragScrollView>();
	}

	protected virtual void OnPress(bool isPressed)
	{
		if (!interactable || UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (isPressed)
		{
			if (!mPressed)
			{
				mTouch = UICamera.currentTouch;
				mDragStartTime = RealTime.time + pressAndHoldDelay;
				mPressed = true;
			}
		}
		else if (mPressed && mTouch == UICamera.currentTouch)
		{
			mPressed = false;
			mTouch = null;
		}
	}

	protected virtual void Update()
	{
		if (restriction == Restriction.PressAndHold && mPressed && !mDragging && mDragStartTime < RealTime.time)
		{
			StartDragging();
		}
	}

	protected virtual void OnDragStart()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (!interactable || !this.get_enabled() || mTouch != UICamera.currentTouch)
		{
			return;
		}
		if (restriction != 0)
		{
			if (restriction == Restriction.Horizontal)
			{
				Vector2 totalDelta = mTouch.totalDelta;
				if (Mathf.Abs(totalDelta.x) < Mathf.Abs(totalDelta.y))
				{
					return;
				}
			}
			else if (restriction == Restriction.Vertical)
			{
				Vector2 totalDelta2 = mTouch.totalDelta;
				if (Mathf.Abs(totalDelta2.x) > Mathf.Abs(totalDelta2.y))
				{
					return;
				}
			}
			else if (restriction == Restriction.PressAndHold)
			{
				return;
			}
		}
		StartDragging();
	}

	public virtual void StartDragging()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		if (!interactable || mDragging)
		{
			return;
		}
		if (cloneOnDrag)
		{
			mPressed = false;
			GameObject val = NGUITools.AddChild(this.get_transform().get_parent().get_gameObject(), this.get_gameObject());
			val.get_transform().set_localPosition(this.get_transform().get_localPosition());
			val.get_transform().set_localRotation(this.get_transform().get_localRotation());
			val.get_transform().set_localScale(this.get_transform().get_localScale());
			UIButtonColor component = val.GetComponent<UIButtonColor>();
			if (component != null)
			{
				component.defaultColor = this.GetComponent<UIButtonColor>().defaultColor;
			}
			if (mTouch != null && mTouch.pressed == this.get_gameObject())
			{
				mTouch.current = val;
				mTouch.pressed = val;
				mTouch.dragged = val;
				mTouch.last = val;
			}
			UIDragDropItem component2 = val.GetComponent<UIDragDropItem>();
			component2.mTouch = mTouch;
			component2.mPressed = true;
			component2.mDragging = true;
			component2.Start();
			component2.OnDragDropStart();
			if (UICamera.currentTouch == null)
			{
				UICamera.currentTouch = mTouch;
			}
			mTouch = null;
			UICamera.Notify(this.get_gameObject(), "OnPress", false);
			UICamera.Notify(this.get_gameObject(), "OnHover", false);
		}
		else
		{
			mDragging = true;
			OnDragDropStart();
		}
	}

	protected virtual void OnDrag(Vector2 delta)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (interactable && mDragging && this.get_enabled() && mTouch == UICamera.currentTouch)
		{
			OnDragDropMove(delta * mRoot.pixelSizeAdjustment);
		}
	}

	protected virtual void OnDragEnd()
	{
		if (interactable && this.get_enabled() && mTouch == UICamera.currentTouch)
		{
			StopDragging(UICamera.hoveredObject);
		}
	}

	public void StopDragging(GameObject go)
	{
		if (mDragging)
		{
			mDragging = false;
			OnDragDropRelease(go);
		}
	}

	protected virtual void OnDragDropStart()
	{
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		if (!draggedItems.Contains(this))
		{
			draggedItems.Add(this);
		}
		if (mDragScrollView != null)
		{
			mDragScrollView.set_enabled(false);
		}
		if (mButton != null)
		{
			mButton.isEnabled = false;
		}
		else if (mCollider != null)
		{
			mCollider.set_enabled(false);
		}
		else if (mCollider2D != null)
		{
			mCollider2D.set_enabled(false);
		}
		mParent = mTrans.get_parent();
		mRoot = NGUITools.FindInParents<UIRoot>(mParent);
		mGrid = NGUITools.FindInParents<UIGrid>(mParent);
		mTable = NGUITools.FindInParents<UITable>(mParent);
		if (UIDragDropRoot.root != null)
		{
			mTrans.set_parent(UIDragDropRoot.root);
		}
		Vector3 localPosition = mTrans.get_localPosition();
		localPosition.z = 0f;
		mTrans.set_localPosition(localPosition);
		TweenPosition component = this.GetComponent<TweenPosition>();
		if (component != null)
		{
			component.set_enabled(false);
		}
		SpringPosition component2 = this.GetComponent<SpringPosition>();
		if (component2 != null)
		{
			component2.set_enabled(false);
		}
		NGUITools.MarkParentAsChanged(this.get_gameObject());
		if (mTable != null)
		{
			mTable.repositionNow = true;
		}
		if (mGrid != null)
		{
			mGrid.repositionNow = true;
		}
	}

	protected virtual void OnDragDropMove(Vector2 delta)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		Transform obj = mTrans;
		obj.set_localPosition(obj.get_localPosition() + Vector2.op_Implicit(delta));
	}

	protected virtual void OnDragDropRelease(GameObject surface)
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		if (!cloneOnDrag)
		{
			if (mButton != null)
			{
				mButton.isEnabled = true;
			}
			else if (mCollider != null)
			{
				mCollider.set_enabled(true);
			}
			else if (mCollider2D != null)
			{
				mCollider2D.set_enabled(true);
			}
			UIDragDropContainer uIDragDropContainer = (!Object.op_Implicit(surface)) ? null : NGUITools.FindInParents<UIDragDropContainer>(surface);
			if (uIDragDropContainer != null)
			{
				mTrans.set_parent((!(uIDragDropContainer.reparentTarget != null)) ? uIDragDropContainer.get_transform() : uIDragDropContainer.reparentTarget);
				Vector3 localPosition = mTrans.get_localPosition();
				localPosition.z = 0f;
				mTrans.set_localPosition(localPosition);
			}
			else
			{
				mTrans.set_parent(mParent);
			}
			mParent = mTrans.get_parent();
			mGrid = NGUITools.FindInParents<UIGrid>(mParent);
			mTable = NGUITools.FindInParents<UITable>(mParent);
			if (mDragScrollView != null)
			{
				this.StartCoroutine(EnableDragScrollView());
			}
			NGUITools.MarkParentAsChanged(this.get_gameObject());
			if (mTable != null)
			{
				mTable.repositionNow = true;
			}
			if (mGrid != null)
			{
				mGrid.repositionNow = true;
			}
			OnDragDropEnd();
		}
		else
		{
			NGUITools.Destroy(this.get_gameObject());
		}
	}

	protected virtual void OnDragDropEnd()
	{
		draggedItems.Remove(this);
	}

	protected IEnumerator EnableDragScrollView()
	{
		yield return (object)new WaitForEndOfFrame();
		if (mDragScrollView != null)
		{
			mDragScrollView.set_enabled(true);
		}
	}
}
