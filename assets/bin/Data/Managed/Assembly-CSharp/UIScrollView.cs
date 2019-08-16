using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Scroll View")]
public class UIScrollView : MonoBehaviour
{
	public enum Movement
	{
		Horizontal,
		Vertical,
		Unrestricted,
		Custom
	}

	public enum DragEffect
	{
		None,
		Momentum,
		MomentumAndSpring
	}

	public enum ShowCondition
	{
		Always,
		OnlyIfNeeded,
		WhenDragging
	}

	public delegate void OnDragNotification();

	public static BetterList<UIScrollView> list = new BetterList<UIScrollView>();

	public Movement movement;

	public DragEffect dragEffect = DragEffect.MomentumAndSpring;

	public bool restrictWithinPanel = true;

	public bool disableDragIfFits;

	public bool smoothDragStart = true;

	public bool iOSDragEmulation = true;

	public float scrollWheelFactor = 0.25f;

	public float momentumAmount = 35f;

	public float dampenStrength = 9f;

	public UIProgressBar horizontalScrollBar;

	public UIProgressBar verticalScrollBar;

	public ShowCondition showScrollBars = ShowCondition.OnlyIfNeeded;

	public Vector2 customMovement = new Vector2(1f, 0f);

	public UIWidget.Pivot contentPivot;

	public OnDragNotification onDragStarted;

	public OnDragNotification onDragFinished;

	public OnDragNotification onMomentumMove;

	public OnDragNotification onStoppedMoving;

	public OnDragNotification onPressStart;

	public OnDragNotification onPressEnd;

	public OnDragNotification onReachBottom;

	[HideInInspector]
	[SerializeField]
	private Vector3 scale = new Vector3(1f, 0f, 0f);

	[SerializeField]
	[HideInInspector]
	private Vector2 relativePositionOnReset = Vector2.get_zero();

	protected Transform mTrans;

	protected UIPanel mPanel;

	protected Plane mPlane;

	protected Vector3 mLastPos;

	protected bool mPressed;

	protected Vector3 mMomentum = Vector3.get_zero();

	protected float mScroll;

	protected Bounds mBounds;

	protected bool mCalculatedBounds;

	protected bool mShouldMove;

	protected bool mIgnoreCallbacks;

	protected int mDragID = -10;

	protected Vector2 mDragStartOffset = Vector2.get_zero();

	protected bool mDragStarted;

	protected bool mReachBottom;

	[NonSerialized]
	private bool mStarted;

	[HideInInspector]
	public UICenterOnChild centerOnChild;

	public UIPanel panel => mPanel;

	public bool isDragging => mPressed && mDragStarted;

	public bool isPressing => mPressed;

	public virtual Bounds bounds
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			if (!mCalculatedBounds)
			{
				mCalculatedBounds = true;
				mTrans = this.get_transform();
				mBounds = NGUIMath.CalculateRelativeWidgetBounds(mTrans, mTrans);
			}
			return mBounds;
		}
	}

	public bool canMoveHorizontally => movement == Movement.Horizontal || movement == Movement.Unrestricted || (movement == Movement.Custom && customMovement.x != 0f);

	public bool canMoveVertically => movement == Movement.Vertical || movement == Movement.Unrestricted || (movement == Movement.Custom && customMovement.y != 0f);

	public virtual bool shouldMoveHorizontally
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			Bounds bounds = this.bounds;
			Vector3 size = bounds.get_size();
			float num = size.x;
			if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				float num2 = num;
				Vector2 clipSoftness = mPanel.clipSoftness;
				num = num2 + clipSoftness.x * 2f;
			}
			return Mathf.RoundToInt(num - mPanel.width) > 0;
		}
	}

	public virtual bool shouldMoveVertically
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			Bounds bounds = this.bounds;
			Vector3 size = bounds.get_size();
			float num = size.y;
			if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				float num2 = num;
				Vector2 clipSoftness = mPanel.clipSoftness;
				num = num2 + clipSoftness.y * 2f;
			}
			return Mathf.RoundToInt(num - mPanel.height) > 0;
		}
	}

	protected virtual bool shouldMove
	{
		get
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			if (!disableDragIfFits)
			{
				return true;
			}
			if (mPanel == null)
			{
				mPanel = this.GetComponent<UIPanel>();
			}
			Vector4 finalClipRegion = mPanel.finalClipRegion;
			Bounds bounds = this.bounds;
			float num = (finalClipRegion.z != 0f) ? (finalClipRegion.z * 0.5f) : ((float)Screen.get_width());
			float num2 = (finalClipRegion.w != 0f) ? (finalClipRegion.w * 0.5f) : ((float)Screen.get_height());
			if (canMoveHorizontally)
			{
				Vector3 min = bounds.get_min();
				if (min.x < finalClipRegion.x - num)
				{
					return true;
				}
				Vector3 max = bounds.get_max();
				if (max.x > finalClipRegion.x + num)
				{
					return true;
				}
			}
			if (canMoveVertically)
			{
				Vector3 min2 = bounds.get_min();
				if (min2.y < finalClipRegion.y - num2)
				{
					return true;
				}
				Vector3 max2 = bounds.get_max();
				if (max2.y > finalClipRegion.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	public Vector3 currentMomentum
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mMomentum;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			mMomentum = value;
			mShouldMove = true;
		}
	}

	public UIScrollView()
		: this()
	{
	}//IL_004f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0054: Unknown result type (might be due to invalid IL or missing references)
	//IL_0069: Unknown result type (might be due to invalid IL or missing references)
	//IL_006e: Unknown result type (might be due to invalid IL or missing references)
	//IL_0074: Unknown result type (might be due to invalid IL or missing references)
	//IL_0079: Unknown result type (might be due to invalid IL or missing references)
	//IL_007f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0084: Unknown result type (might be due to invalid IL or missing references)
	//IL_0092: Unknown result type (might be due to invalid IL or missing references)
	//IL_0097: Unknown result type (might be due to invalid IL or missing references)


	public bool CurrentFit()
	{
		return shouldMove;
	}

	private void Awake()
	{
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		mTrans = this.get_transform();
		mPanel = this.GetComponent<UIPanel>();
		if (mPanel.clipping == UIDrawCall.Clipping.None)
		{
			mPanel.clipping = UIDrawCall.Clipping.ConstrainButDontClip;
		}
		if (movement != Movement.Custom && scale.get_sqrMagnitude() > 0.001f)
		{
			if (scale.x == 1f && scale.y == 0f)
			{
				movement = Movement.Horizontal;
			}
			else if (scale.x == 0f && scale.y == 1f)
			{
				movement = Movement.Vertical;
			}
			else if (scale.x == 1f && scale.y == 1f)
			{
				movement = Movement.Unrestricted;
			}
			else
			{
				movement = Movement.Custom;
				customMovement.x = scale.x;
				customMovement.y = scale.y;
			}
			scale = Vector3.get_zero();
		}
		if (contentPivot == UIWidget.Pivot.TopLeft && relativePositionOnReset != Vector2.get_zero())
		{
			contentPivot = NGUIMath.GetPivot(new Vector2(relativePositionOnReset.x, 1f - relativePositionOnReset.y));
			relativePositionOnReset = Vector2.get_zero();
		}
	}

	private void OnEnable()
	{
		list.Add(this);
		if (mStarted && Application.get_isPlaying())
		{
			CheckScrollbars();
		}
	}

	private void Start()
	{
		mStarted = true;
		if (Application.get_isPlaying())
		{
			CheckScrollbars();
		}
	}

	private void CheckScrollbars()
	{
		if (horizontalScrollBar != null)
		{
			EventDelegate.Add(horizontalScrollBar.onChange, OnScrollBar);
			horizontalScrollBar.BroadcastMessage("CacheDefaultColor", 1);
			horizontalScrollBar.alpha = ((showScrollBars != 0 && !shouldMoveHorizontally) ? 0f : 1f);
		}
		if (verticalScrollBar != null)
		{
			EventDelegate.Add(verticalScrollBar.onChange, OnScrollBar);
			verticalScrollBar.BroadcastMessage("CacheDefaultColor", 1);
			verticalScrollBar.alpha = ((showScrollBars != 0 && !shouldMoveVertically) ? 0f : 1f);
		}
	}

	private void OnDisable()
	{
		list.Remove(this);
	}

	public bool RestrictWithinBounds(bool instant)
	{
		return RestrictWithinBounds(instant, horizontal: true, vertical: true);
	}

	public bool RestrictWithinBounds(bool instant, bool horizontal, bool vertical)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		if (mPanel == null)
		{
			return false;
		}
		Bounds bounds = this.bounds;
		Vector3 val = mPanel.CalculateConstrainOffset(Vector2.op_Implicit(bounds.get_min()), Vector2.op_Implicit(bounds.get_max()));
		if (!horizontal)
		{
			val.x = 0f;
		}
		if (!vertical)
		{
			val.y = 0f;
		}
		if (val.get_sqrMagnitude() > 0.1f)
		{
			if (!instant && dragEffect == DragEffect.MomentumAndSpring)
			{
				Vector3 pos = mTrans.get_localPosition() + val;
				pos.x = Mathf.Round(pos.x);
				pos.y = Mathf.Round(pos.y);
				SpringPanel.Begin(mPanel.get_gameObject(), pos, 13f).strength = 8f;
			}
			else
			{
				MoveRelative(val);
				if (Mathf.Abs(val.x) > 0.01f)
				{
					mMomentum.x = 0f;
				}
				if (Mathf.Abs(val.y) > 0.01f)
				{
					mMomentum.y = 0f;
				}
				if (Mathf.Abs(val.z) > 0.01f)
				{
					mMomentum.z = 0f;
				}
				mScroll = 0f;
			}
			if (val.y < 0f)
			{
				mReachBottom = true;
			}
			return true;
		}
		return false;
	}

	public void DisableSpring()
	{
		SpringPanel component = this.GetComponent<SpringPanel>();
		if (component != null)
		{
			component.set_enabled(false);
		}
	}

	public void UpdateScrollbars()
	{
		UpdateScrollbars(recalculateBounds: true);
	}

	public virtual void UpdateScrollbars(bool recalculateBounds)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		if (mPanel == null)
		{
			return;
		}
		if (horizontalScrollBar != null || verticalScrollBar != null)
		{
			if (recalculateBounds)
			{
				mCalculatedBounds = false;
				mShouldMove = shouldMove;
			}
			Bounds bounds = this.bounds;
			Vector2 val = Vector2.op_Implicit(bounds.get_min());
			Vector2 val2 = Vector2.op_Implicit(bounds.get_max());
			if (horizontalScrollBar != null && val2.x > val.x)
			{
				Vector4 finalClipRegion = mPanel.finalClipRegion;
				int num = Mathf.RoundToInt(finalClipRegion.z);
				if ((num & 1) != 0)
				{
					num--;
				}
				float num2 = (float)num * 0.5f;
				num2 = Mathf.Round(num2);
				if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					float num3 = num2;
					Vector2 clipSoftness = mPanel.clipSoftness;
					num2 = num3 - clipSoftness.x;
				}
				float contentSize = val2.x - val.x;
				float viewSize = num2 * 2f;
				float x = val.x;
				float x2 = val2.x;
				float num4 = finalClipRegion.x - num2;
				float num5 = finalClipRegion.x + num2;
				x = num4 - x;
				x2 -= num5;
				UpdateScrollbars(horizontalScrollBar, x, x2, contentSize, viewSize, inverted: false);
			}
			if (verticalScrollBar != null && val2.y > val.y)
			{
				Vector4 finalClipRegion2 = mPanel.finalClipRegion;
				int num6 = Mathf.RoundToInt(finalClipRegion2.w);
				if ((num6 & 1) != 0)
				{
					num6--;
				}
				float num7 = (float)num6 * 0.5f;
				num7 = Mathf.Round(num7);
				if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					float num8 = num7;
					Vector2 clipSoftness2 = mPanel.clipSoftness;
					num7 = num8 - clipSoftness2.y;
				}
				float contentSize2 = val2.y - val.y;
				float viewSize2 = num7 * 2f;
				float y = val.y;
				float y2 = val2.y;
				float num9 = finalClipRegion2.y - num7;
				float num10 = finalClipRegion2.y + num7;
				y = num9 - y;
				y2 -= num10;
				UpdateScrollbars(verticalScrollBar, y, y2, contentSize2, viewSize2, inverted: true);
			}
		}
		else if (recalculateBounds)
		{
			mCalculatedBounds = false;
		}
	}

	protected void UpdateScrollbars(UIProgressBar slider, float contentMin, float contentMax, float contentSize, float viewSize, bool inverted)
	{
		if (slider == null)
		{
			return;
		}
		mIgnoreCallbacks = true;
		float num;
		if (viewSize < contentSize)
		{
			contentMin = Mathf.Clamp01(contentMin / contentSize);
			contentMax = Mathf.Clamp01(contentMax / contentSize);
			num = contentMin + contentMax;
			slider.value = (inverted ? ((!(num > 0.001f)) ? 0f : (1f - contentMin / num)) : ((!(num > 0.001f)) ? 1f : (contentMin / num)));
		}
		else
		{
			contentMin = Mathf.Clamp01((0f - contentMin) / contentSize);
			contentMax = Mathf.Clamp01((0f - contentMax) / contentSize);
			num = contentMin + contentMax;
			slider.value = (inverted ? ((!(num > 0.001f)) ? 0f : (1f - contentMin / num)) : ((!(num > 0.001f)) ? 1f : (contentMin / num)));
			if (contentSize > 0f)
			{
				contentMin = Mathf.Clamp01(contentMin / contentSize);
				contentMax = Mathf.Clamp01(contentMax / contentSize);
				num = contentMin + contentMax;
			}
		}
		UIScrollBar uIScrollBar = slider as UIScrollBar;
		if (uIScrollBar != null)
		{
			uIScrollBar.barSize = 1f - num;
		}
		mIgnoreCallbacks = false;
	}

	public virtual void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		if (mPanel == null)
		{
			mPanel = this.GetComponent<UIPanel>();
		}
		DisableSpring();
		Bounds bounds = this.bounds;
		Vector3 min = bounds.get_min();
		float x2 = min.x;
		Vector3 max = bounds.get_max();
		if (x2 == max.x)
		{
			return;
		}
		Vector3 min2 = bounds.get_min();
		float y2 = min2.y;
		Vector3 max2 = bounds.get_max();
		if (y2 == max2.y)
		{
			return;
		}
		Vector4 finalClipRegion = mPanel.finalClipRegion;
		float num = finalClipRegion.z * 0.5f;
		float num2 = finalClipRegion.w * 0.5f;
		Vector3 min3 = bounds.get_min();
		float num3 = min3.x + num;
		Vector3 max3 = bounds.get_max();
		float num4 = max3.x - num;
		Vector3 min4 = bounds.get_min();
		float num5 = min4.y + num2;
		Vector3 max4 = bounds.get_max();
		float num6 = max4.y - num2;
		if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			float num7 = num3;
			Vector2 clipSoftness = mPanel.clipSoftness;
			num3 = num7 - clipSoftness.x;
			float num8 = num4;
			Vector2 clipSoftness2 = mPanel.clipSoftness;
			num4 = num8 + clipSoftness2.x;
			float num9 = num5;
			Vector2 clipSoftness3 = mPanel.clipSoftness;
			num5 = num9 - clipSoftness3.y;
			float num10 = num6;
			Vector2 clipSoftness4 = mPanel.clipSoftness;
			num6 = num10 + clipSoftness4.y;
		}
		float num11 = Mathf.Lerp(num3, num4, x);
		float num12 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = mTrans.get_localPosition();
			if (canMoveHorizontally)
			{
				localPosition.x += finalClipRegion.x - num11;
			}
			if (canMoveVertically)
			{
				localPosition.y += finalClipRegion.y - num12;
			}
			mTrans.set_localPosition(localPosition);
		}
		if (canMoveHorizontally)
		{
			finalClipRegion.x = num11;
		}
		if (canMoveVertically)
		{
			finalClipRegion.y = num12;
		}
		Vector4 baseClipRegion = mPanel.baseClipRegion;
		mPanel.clipOffset = new Vector2(finalClipRegion.x - baseClipRegion.x, finalClipRegion.y - baseClipRegion.y);
		if (updateScrollbars)
		{
			UpdateScrollbars(mDragID == -10);
		}
	}

	public void InvalidateBounds()
	{
		mCalculatedBounds = false;
	}

	[ContextMenu("Reset Clipping Position")]
	public void ResetPosition()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (NGUITools.GetActive(this))
		{
			mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(contentPivot);
			SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, updateScrollbars: false);
			SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, updateScrollbars: true);
		}
	}

	public void UpdatePosition()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (!mIgnoreCallbacks && (horizontalScrollBar != null || verticalScrollBar != null))
		{
			mIgnoreCallbacks = true;
			mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(contentPivot);
			float x = (!(horizontalScrollBar != null)) ? pivotOffset.x : horizontalScrollBar.value;
			float y = (!(verticalScrollBar != null)) ? (1f - pivotOffset.y) : verticalScrollBar.value;
			SetDragAmount(x, y, updateScrollbars: false);
			UpdateScrollbars(recalculateBounds: true);
			mIgnoreCallbacks = false;
		}
	}

	public void OnScrollBar()
	{
		if (!mIgnoreCallbacks)
		{
			mIgnoreCallbacks = true;
			float x = (!(horizontalScrollBar != null)) ? 0f : horizontalScrollBar.value;
			float y = (!(verticalScrollBar != null)) ? 0f : verticalScrollBar.value;
			SetDragAmount(x, y, updateScrollbars: false);
			mIgnoreCallbacks = false;
		}
	}

	public virtual void MoveRelative(Vector3 relative)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Transform obj = mTrans;
		obj.set_localPosition(obj.get_localPosition() + relative);
		Vector2 clipOffset = mPanel.clipOffset;
		clipOffset.x -= relative.x;
		clipOffset.y -= relative.y;
		mPanel.clipOffset = clipOffset;
		UpdateScrollbars(recalculateBounds: false);
	}

	public void MoveAbsolute(Vector3 absolute)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = mTrans.InverseTransformPoint(absolute);
		Vector3 val2 = mTrans.InverseTransformPoint(Vector3.get_zero());
		MoveRelative(val - val2);
	}

	public void Press(bool pressed)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		if (pressed)
		{
			if (onPressStart != null)
			{
				onPressStart();
			}
		}
		else if (onPressEnd != null)
		{
			onPressEnd();
		}
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		if (smoothDragStart && pressed)
		{
			mDragStarted = false;
			mDragStartOffset = Vector2.get_zero();
		}
		if (!this.get_enabled() || !NGUITools.GetActive(this.get_gameObject()))
		{
			return;
		}
		if (!pressed && mDragID == UICamera.currentTouchID)
		{
			mDragID = -10;
		}
		mCalculatedBounds = false;
		mShouldMove = shouldMove;
		if (!mShouldMove)
		{
			return;
		}
		mPressed = pressed;
		if (pressed)
		{
			mMomentum = Vector3.get_zero();
			mScroll = 0f;
			DisableSpring();
			mLastPos = UICamera.lastWorldPosition;
			mPlane = new Plane(mTrans.get_rotation() * Vector3.get_back(), mLastPos);
			Vector2 clipOffset = mPanel.clipOffset;
			clipOffset.x = Mathf.Round(clipOffset.x);
			clipOffset.y = Mathf.Round(clipOffset.y);
			mPanel.clipOffset = clipOffset;
			Vector3 localPosition = mTrans.get_localPosition();
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			mTrans.set_localPosition(localPosition);
			if (!smoothDragStart)
			{
				mDragStarted = true;
				mDragStartOffset = Vector2.get_zero();
				if (onDragStarted != null)
				{
					onDragStarted();
				}
			}
		}
		else if (Object.op_Implicit(centerOnChild))
		{
			centerOnChild.Recenter();
		}
		else
		{
			if (restrictWithinPanel && mPanel.clipping != 0)
			{
				RestrictWithinBounds(dragEffect == DragEffect.None, canMoveHorizontally, canMoveVertically);
			}
			if (mDragStarted && onDragFinished != null)
			{
				onDragFinished();
			}
			if (!mShouldMove && onStoppedMoving != null)
			{
				onStoppedMoving();
			}
		}
	}

	public void Drag()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller || !this.get_enabled() || !NGUITools.GetActive(this.get_gameObject()) || !mShouldMove)
		{
			return;
		}
		if (mDragID == -10)
		{
			mDragID = UICamera.currentTouchID;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		if (smoothDragStart && !mDragStarted)
		{
			mDragStarted = true;
			mDragStartOffset = UICamera.currentTouch.totalDelta;
			if (onDragStarted != null)
			{
				onDragStarted();
			}
		}
		Ray val = (!smoothDragStart) ? UICamera.currentCamera.ScreenPointToRay(Vector2.op_Implicit(UICamera.currentTouch.pos)) : UICamera.currentCamera.ScreenPointToRay(Vector2.op_Implicit(UICamera.currentTouch.pos - mDragStartOffset));
		float num = 0f;
		if (!mPlane.Raycast(val, ref num))
		{
			return;
		}
		Vector3 point = val.GetPoint(num);
		Vector3 val2 = point - mLastPos;
		mLastPos = point;
		if (val2.x != 0f || val2.y != 0f || val2.z != 0f)
		{
			val2 = mTrans.InverseTransformDirection(val2);
			if (movement == Movement.Horizontal)
			{
				val2.y = 0f;
				val2.z = 0f;
			}
			else if (movement == Movement.Vertical)
			{
				val2.x = 0f;
				val2.z = 0f;
			}
			else if (movement == Movement.Unrestricted)
			{
				val2.z = 0f;
			}
			else
			{
				val2.Scale(Vector2.op_Implicit(customMovement));
			}
			val2 = mTrans.TransformDirection(val2);
		}
		if (dragEffect == DragEffect.None)
		{
			mMomentum = Vector3.get_zero();
		}
		else
		{
			mMomentum = Vector3.Lerp(mMomentum, mMomentum + val2 * (0.01f * momentumAmount), 0.67f);
		}
		if (!iOSDragEmulation || dragEffect != DragEffect.MomentumAndSpring)
		{
			MoveAbsolute(val2);
		}
		else
		{
			UIPanel uIPanel = mPanel;
			Bounds bounds = this.bounds;
			Vector2 min = Vector2.op_Implicit(bounds.get_min());
			Bounds bounds2 = this.bounds;
			Vector3 val3 = uIPanel.CalculateConstrainOffset(min, Vector2.op_Implicit(bounds2.get_max()));
			if (val3.get_magnitude() > 1f)
			{
				MoveAbsolute(val2 * 0.5f);
				mMomentum *= 0.5f;
			}
			else
			{
				MoveAbsolute(val2);
			}
		}
		if (restrictWithinPanel && mPanel.clipping != 0 && dragEffect != DragEffect.MomentumAndSpring)
		{
			RestrictWithinBounds(instant: true, canMoveHorizontally, canMoveVertically);
		}
	}

	public void Scroll(float delta)
	{
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && scrollWheelFactor != 0f)
		{
			DisableSpring();
			mShouldMove |= shouldMove;
			if (Mathf.Sign(mScroll) != Mathf.Sign(delta))
			{
				mScroll = 0f;
			}
			mScroll += delta * scrollWheelFactor;
		}
	}

	private void LateUpdate()
	{
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		if (!Application.get_isPlaying())
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		if (showScrollBars != 0 && (Object.op_Implicit(verticalScrollBar) || Object.op_Implicit(horizontalScrollBar)))
		{
			bool flag = false;
			bool flag2 = false;
			if (showScrollBars != ShowCondition.WhenDragging || mDragID != -10 || mMomentum.get_magnitude() > 0.01f)
			{
				flag = shouldMoveVertically;
				flag2 = shouldMoveHorizontally;
			}
			if (Object.op_Implicit(verticalScrollBar))
			{
				float alpha = verticalScrollBar.alpha;
				alpha += ((!flag) ? ((0f - deltaTime) * 3f) : (deltaTime * 6f));
				alpha = Mathf.Clamp01(alpha);
				if (verticalScrollBar.alpha != alpha)
				{
					verticalScrollBar.alpha = alpha;
				}
			}
			if (Object.op_Implicit(horizontalScrollBar))
			{
				float alpha2 = horizontalScrollBar.alpha;
				alpha2 += ((!flag2) ? ((0f - deltaTime) * 3f) : (deltaTime * 6f));
				alpha2 = Mathf.Clamp01(alpha2);
				if (horizontalScrollBar.alpha != alpha2)
				{
					horizontalScrollBar.alpha = alpha2;
				}
			}
		}
		if (!mShouldMove)
		{
			return;
		}
		if (!mPressed)
		{
			if (mMomentum.get_magnitude() > 0.0001f || mScroll != 0f)
			{
				if (movement == Movement.Horizontal)
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(mScroll * 0.05f, 0f, 0f));
				}
				else if (movement == Movement.Vertical)
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(0f, mScroll * 0.05f, 0f));
				}
				else if (movement == Movement.Unrestricted)
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(mScroll * 0.05f, mScroll * 0.05f, 0f));
				}
				else
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(mScroll * customMovement.x * 0.05f, mScroll * customMovement.y * 0.05f, 0f));
				}
				mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
				Vector3 absolute = NGUIMath.SpringDampen(ref mMomentum, dampenStrength, deltaTime);
				MoveAbsolute(absolute);
				if (restrictWithinPanel && mPanel.clipping != 0)
				{
					if (NGUITools.GetActive(centerOnChild))
					{
						if (centerOnChild.nextPageThreshold != 0f)
						{
							mMomentum = Vector3.get_zero();
							mScroll = 0f;
						}
						else
						{
							centerOnChild.Recenter();
						}
					}
					else
					{
						RestrictWithinBounds(instant: false, canMoveHorizontally, canMoveVertically);
					}
				}
				if (onMomentumMove != null)
				{
					onMomentumMove();
				}
				return;
			}
			mScroll = 0f;
			mMomentum = Vector3.get_zero();
			SpringPanel component = this.GetComponent<SpringPanel>();
			if (!(component != null) || !component.get_enabled())
			{
				mShouldMove = false;
				if (onStoppedMoving != null)
				{
					onStoppedMoving();
				}
				if (mReachBottom && onReachBottom != null)
				{
					onReachBottom();
					mReachBottom = false;
				}
			}
		}
		else
		{
			mScroll = 0f;
			NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
		}
	}

	public void OnPan(Vector2 delta)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (horizontalScrollBar != null)
		{
			horizontalScrollBar.OnPan(delta);
		}
		if (verticalScrollBar != null)
		{
			verticalScrollBar.OnPan(delta);
		}
		if (horizontalScrollBar == null && verticalScrollBar == null)
		{
			if (scale.x != 0f)
			{
				Scroll(delta.x);
			}
			else if (scale.y != 0f)
			{
				Scroll(delta.y);
			}
		}
	}
}
