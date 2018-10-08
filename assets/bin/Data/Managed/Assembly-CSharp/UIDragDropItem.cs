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

	protected virtual void Awake()
	{
		mTrans = base.transform;
		mCollider = base.gameObject.GetComponent<Collider>();
		mCollider2D = base.gameObject.GetComponent<Collider2D>();
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
		mButton = GetComponent<UIButton>();
		mDragScrollView = GetComponent<UIDragScrollView>();
	}

	protected virtual void OnPress(bool isPressed)
	{
		if (interactable && UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3)
		{
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
		if (interactable && base.enabled && mTouch == UICamera.currentTouch)
		{
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
	}

	public virtual void StartDragging()
	{
		if (interactable && !mDragging)
		{
			if (cloneOnDrag)
			{
				mPressed = false;
				GameObject gameObject = NGUITools.AddChild(base.transform.parent.gameObject, base.gameObject);
				gameObject.transform.localPosition = base.transform.localPosition;
				gameObject.transform.localRotation = base.transform.localRotation;
				gameObject.transform.localScale = base.transform.localScale;
				UIButtonColor component = gameObject.GetComponent<UIButtonColor>();
				if ((UnityEngine.Object)component != (UnityEngine.Object)null)
				{
					component.defaultColor = GetComponent<UIButtonColor>().defaultColor;
				}
				if (mTouch != null && (UnityEngine.Object)mTouch.pressed == (UnityEngine.Object)base.gameObject)
				{
					mTouch.current = gameObject;
					mTouch.pressed = gameObject;
					mTouch.dragged = gameObject;
					mTouch.last = gameObject;
				}
				UIDragDropItem component2 = gameObject.GetComponent<UIDragDropItem>();
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
				UICamera.Notify(base.gameObject, "OnPress", false);
				UICamera.Notify(base.gameObject, "OnHover", false);
			}
			else
			{
				mDragging = true;
				OnDragDropStart();
			}
		}
	}

	protected virtual void OnDrag(Vector2 delta)
	{
		if (interactable && mDragging && base.enabled && mTouch == UICamera.currentTouch)
		{
			OnDragDropMove(delta * mRoot.pixelSizeAdjustment);
		}
	}

	protected virtual void OnDragEnd()
	{
		if (interactable && base.enabled && mTouch == UICamera.currentTouch)
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
		if (!draggedItems.Contains(this))
		{
			draggedItems.Add(this);
		}
		if ((UnityEngine.Object)mDragScrollView != (UnityEngine.Object)null)
		{
			mDragScrollView.enabled = false;
		}
		if ((UnityEngine.Object)mButton != (UnityEngine.Object)null)
		{
			mButton.isEnabled = false;
		}
		else if ((UnityEngine.Object)mCollider != (UnityEngine.Object)null)
		{
			mCollider.enabled = false;
		}
		else if ((UnityEngine.Object)mCollider2D != (UnityEngine.Object)null)
		{
			mCollider2D.enabled = false;
		}
		mParent = mTrans.parent;
		mRoot = NGUITools.FindInParents<UIRoot>(mParent);
		mGrid = NGUITools.FindInParents<UIGrid>(mParent);
		mTable = NGUITools.FindInParents<UITable>(mParent);
		if ((UnityEngine.Object)UIDragDropRoot.root != (UnityEngine.Object)null)
		{
			mTrans.parent = UIDragDropRoot.root;
		}
		Vector3 localPosition = mTrans.localPosition;
		localPosition.z = 0f;
		mTrans.localPosition = localPosition;
		TweenPosition component = GetComponent<TweenPosition>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.enabled = false;
		}
		SpringPosition component2 = GetComponent<SpringPosition>();
		if ((UnityEngine.Object)component2 != (UnityEngine.Object)null)
		{
			component2.enabled = false;
		}
		NGUITools.MarkParentAsChanged(base.gameObject);
		if ((UnityEngine.Object)mTable != (UnityEngine.Object)null)
		{
			mTable.repositionNow = true;
		}
		if ((UnityEngine.Object)mGrid != (UnityEngine.Object)null)
		{
			mGrid.repositionNow = true;
		}
	}

	protected virtual void OnDragDropMove(Vector2 delta)
	{
		mTrans.localPosition += (Vector3)delta;
	}

	protected virtual void OnDragDropRelease(GameObject surface)
	{
		if (!cloneOnDrag)
		{
			if ((UnityEngine.Object)mButton != (UnityEngine.Object)null)
			{
				mButton.isEnabled = true;
			}
			else if ((UnityEngine.Object)mCollider != (UnityEngine.Object)null)
			{
				mCollider.enabled = true;
			}
			else if ((UnityEngine.Object)mCollider2D != (UnityEngine.Object)null)
			{
				mCollider2D.enabled = true;
			}
			UIDragDropContainer uIDragDropContainer = (!(bool)surface) ? null : NGUITools.FindInParents<UIDragDropContainer>(surface);
			if ((UnityEngine.Object)uIDragDropContainer != (UnityEngine.Object)null)
			{
				mTrans.parent = ((!((UnityEngine.Object)uIDragDropContainer.reparentTarget != (UnityEngine.Object)null)) ? uIDragDropContainer.transform : uIDragDropContainer.reparentTarget);
				Vector3 localPosition = mTrans.localPosition;
				localPosition.z = 0f;
				mTrans.localPosition = localPosition;
			}
			else
			{
				mTrans.parent = mParent;
			}
			mParent = mTrans.parent;
			mGrid = NGUITools.FindInParents<UIGrid>(mParent);
			mTable = NGUITools.FindInParents<UITable>(mParent);
			if ((UnityEngine.Object)mDragScrollView != (UnityEngine.Object)null)
			{
				StartCoroutine(EnableDragScrollView());
			}
			NGUITools.MarkParentAsChanged(base.gameObject);
			if ((UnityEngine.Object)mTable != (UnityEngine.Object)null)
			{
				mTable.repositionNow = true;
			}
			if ((UnityEngine.Object)mGrid != (UnityEngine.Object)null)
			{
				mGrid.repositionNow = true;
			}
			OnDragDropEnd();
		}
		else
		{
			NGUITools.Destroy(base.gameObject);
		}
	}

	protected virtual void OnDragDropEnd()
	{
		draggedItems.Remove(this);
	}

	protected IEnumerator EnableDragScrollView()
	{
		yield return (object)new WaitForEndOfFrame();
		if ((UnityEngine.Object)mDragScrollView != (UnityEngine.Object)null)
		{
			mDragScrollView.enabled = true;
		}
	}
}
