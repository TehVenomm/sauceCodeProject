using System;
using System.Diagnostics;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Widget")]
public class UIWidget : UIRect
{
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
		BottomRight
	}

	public enum AspectRatioSource
	{
		Free,
		BasedOnWidth,
		BasedOnHeight
	}

	public delegate void OnDimensionsChanged();

	public delegate void OnPostFillCallback(UIWidget widget, int bufferOffset, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols);

	public delegate bool HitCheck(Vector3 worldPos);

	[HideInInspector]
	[SerializeField]
	protected Color mColor = Color.get_white();

	[SerializeField]
	[HideInInspector]
	protected Pivot mPivot = Pivot.Center;

	[SerializeField]
	[HideInInspector]
	protected int mWidth = 100;

	[SerializeField]
	[HideInInspector]
	protected int mHeight = 100;

	[SerializeField]
	[HideInInspector]
	protected int mDepth;

	public OnDimensionsChanged onChange;

	public OnPostFillCallback onPostFill;

	public UIDrawCall.OnRenderCallback mOnRender;

	public bool autoResizeBoxCollider;

	public bool hideIfOffScreen;

	public AspectRatioSource keepAspectRatio;

	public float aspectRatio = 1f;

	public HitCheck hitCheck;

	[NonSerialized]
	public UIPanel panel;

	[NonSerialized]
	public UIGeometry geometry = new UIGeometry();

	[NonSerialized]
	public bool fillGeometry = true;

	[NonSerialized]
	protected bool mPlayMode = true;

	[NonSerialized]
	protected Vector4 mDrawRegion = new Vector4(0f, 0f, 1f, 1f);

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
	protected Vector3[] mCorners = (Vector3[])new Vector3[4];

	[NonSerialized]
	private int mAlphaFrameID = -1;

	private int mMatrixFrame = -1;

	private Vector3 mOldV0;

	private Vector3 mOldV1;

	public UIDrawCall.OnRenderCallback onRender
	{
		get
		{
			return mOnRender;
		}
		set
		{
			if ((MulticastDelegate)mOnRender != (MulticastDelegate)value)
			{
				if (drawCall != null && drawCall.onRender != null && mOnRender != null)
				{
					UIDrawCall uIDrawCall = drawCall;
					uIDrawCall.onRender = (UIDrawCall.OnRenderCallback)Delegate.Remove(uIDrawCall.onRender, mOnRender);
				}
				mOnRender = value;
				if (drawCall != null)
				{
					UIDrawCall uIDrawCall2 = drawCall;
					uIDrawCall2.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uIDrawCall2.onRender, value);
				}
			}
		}
	}

	public Vector4 drawRegion
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mDrawRegion;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (mDrawRegion != value)
			{
				mDrawRegion = value;
				if (autoResizeBoxCollider)
				{
					ResizeCollider();
				}
				MarkAsChanged();
			}
		}
	}

	public Vector2 pivotOffset => NGUIMath.GetPivotOffset(pivot);

	public int width
	{
		get
		{
			return mWidth;
		}
		set
		{
			int minWidth = this.minWidth;
			if (value < minWidth)
			{
				value = minWidth;
			}
			if (mWidth != value && keepAspectRatio != AspectRatioSource.BasedOnHeight)
			{
				if (isAnchoredHorizontally)
				{
					if (leftAnchor.target != null && rightAnchor.target != null)
					{
						if (mPivot == Pivot.BottomLeft || mPivot == Pivot.Left || mPivot == Pivot.TopLeft)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - mWidth), 0f);
						}
						else if (mPivot == Pivot.BottomRight || mPivot == Pivot.Right || mPivot == Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, (float)(mWidth - value), 0f, 0f, 0f);
						}
						else
						{
							int num = value - mWidth;
							num -= (num & 1);
							if (num != 0)
							{
								NGUIMath.AdjustWidget(this, (float)(-num) * 0.5f, 0f, (float)num * 0.5f, 0f);
							}
						}
					}
					else if (leftAnchor.target != null)
					{
						NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - mWidth), 0f);
					}
					else
					{
						NGUIMath.AdjustWidget(this, (float)(mWidth - value), 0f, 0f, 0f);
					}
				}
				else
				{
					SetDimensions(value, mHeight);
				}
			}
		}
	}

	public int height
	{
		get
		{
			return mHeight;
		}
		set
		{
			int minHeight = this.minHeight;
			if (value < minHeight)
			{
				value = minHeight;
			}
			if (mHeight != value && keepAspectRatio != AspectRatioSource.BasedOnWidth)
			{
				if (isAnchoredVertically)
				{
					if (bottomAnchor.target != null && topAnchor.target != null)
					{
						if (mPivot == Pivot.BottomLeft || mPivot == Pivot.Bottom || mPivot == Pivot.BottomRight)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - mHeight));
						}
						else if (mPivot == Pivot.TopLeft || mPivot == Pivot.Top || mPivot == Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, 0f, (float)(mHeight - value), 0f, 0f);
						}
						else
						{
							int num = value - mHeight;
							num -= (num & 1);
							if (num != 0)
							{
								NGUIMath.AdjustWidget(this, 0f, (float)(-num) * 0.5f, 0f, (float)num * 0.5f);
							}
						}
					}
					else if (bottomAnchor.target != null)
					{
						NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - mHeight));
					}
					else
					{
						NGUIMath.AdjustWidget(this, 0f, (float)(mHeight - value), 0f, 0f);
					}
				}
				else
				{
					SetDimensions(mWidth, value);
				}
			}
		}
	}

	public Color color
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mColor;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (mColor != value)
			{
				bool includeChildren = mColor.a != value.a;
				mColor = value;
				Invalidate(includeChildren);
			}
		}
	}

	public override float alpha
	{
		get
		{
			return mColor.a;
		}
		set
		{
			if (mColor.a != value)
			{
				mColor.a = value;
				Invalidate(true);
			}
		}
	}

	public bool isVisible => mIsVisibleByPanel && mIsVisibleByAlpha && mIsInFront && finalAlpha > 0.001f && NGUITools.GetActive(this);

	public bool hasVertices => geometry != null && geometry.hasVertices;

	public Pivot rawPivot
	{
		get
		{
			return mPivot;
		}
		set
		{
			if (mPivot != value)
			{
				mPivot = value;
				if (autoResizeBoxCollider)
				{
					ResizeCollider();
				}
				MarkAsChanged();
			}
		}
	}

	public Pivot pivot
	{
		get
		{
			return mPivot;
		}
		set
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			if (mPivot != value)
			{
				Vector3 val = worldCorners[0];
				mPivot = value;
				mChanged = true;
				Vector3 val2 = worldCorners[0];
				Transform cachedTransform = base.cachedTransform;
				Vector3 val3 = cachedTransform.get_position();
				Vector3 localPosition = cachedTransform.get_localPosition();
				float z = localPosition.z;
				val3.x += val.x - val2.x;
				val3.y += val.y - val2.y;
				base.cachedTransform.set_position(val3);
				val3 = base.cachedTransform.get_localPosition();
				val3.x = Mathf.Round(val3.x);
				val3.y = Mathf.Round(val3.y);
				val3.z = z;
				base.cachedTransform.set_localPosition(val3);
			}
		}
	}

	public int depth
	{
		get
		{
			return mDepth;
		}
		set
		{
			if (mDepth != value)
			{
				if (panel != null)
				{
					panel.RemoveWidget(this);
				}
				mDepth = value;
				if (panel != null)
				{
					panel.AddWidget(this);
					if (!Application.get_isPlaying())
					{
						panel.SortWidgets();
						panel.RebuildAllDrawCalls();
					}
				}
			}
		}
	}

	public int raycastDepth
	{
		get
		{
			if (panel == null)
			{
				CreatePanel();
			}
			return (!(panel != null)) ? mDepth : (mDepth + panel.depth * 1000);
		}
	}

	public override Vector3[] localCorners
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			Vector2 pivotOffset = this.pivotOffset;
			float num = (0f - pivotOffset.x) * (float)mWidth;
			float num2 = (0f - pivotOffset.y) * (float)mHeight;
			float num3 = num + (float)mWidth;
			float num4 = num2 + (float)mHeight;
			mCorners[0] = new Vector3(num, num2);
			mCorners[1] = new Vector3(num, num4);
			mCorners[2] = new Vector3(num3, num4);
			mCorners[3] = new Vector3(num3, num2);
			return mCorners;
		}
	}

	public virtual Vector2 localSize
	{
		get
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			Vector3[] localCorners = this.localCorners;
			return Vector2.op_Implicit(localCorners[2] - localCorners[0]);
		}
	}

	public Vector3 localCenter
	{
		get
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			Vector3[] localCorners = this.localCorners;
			return Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
		}
	}

	public override Vector3[] worldCorners
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			Vector2 pivotOffset = this.pivotOffset;
			float num = (0f - pivotOffset.x) * (float)mWidth;
			float num2 = (0f - pivotOffset.y) * (float)mHeight;
			float num3 = num + (float)mWidth;
			float num4 = num2 + (float)mHeight;
			Transform cachedTransform = base.cachedTransform;
			mCorners[0] = cachedTransform.TransformPoint(num, num2, 0f);
			mCorners[1] = cachedTransform.TransformPoint(num, num4, 0f);
			mCorners[2] = cachedTransform.TransformPoint(num3, num4, 0f);
			mCorners[3] = cachedTransform.TransformPoint(num3, num2, 0f);
			return mCorners;
		}
	}

	public Vector3 worldCenter => base.cachedTransform.TransformPoint(localCenter);

	public virtual Vector4 drawingDimensions
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			Vector2 pivotOffset = this.pivotOffset;
			float num = (0f - pivotOffset.x) * (float)mWidth;
			float num2 = (0f - pivotOffset.y) * (float)mHeight;
			float num3 = num + (float)mWidth;
			float num4 = num2 + (float)mHeight;
			return new Vector4((mDrawRegion.x != 0f) ? Mathf.Lerp(num, num3, mDrawRegion.x) : num, (mDrawRegion.y != 0f) ? Mathf.Lerp(num2, num4, mDrawRegion.y) : num2, (mDrawRegion.z != 1f) ? Mathf.Lerp(num, num3, mDrawRegion.z) : num3, (mDrawRegion.w != 1f) ? Mathf.Lerp(num2, num4, mDrawRegion.w) : num4);
		}
	}

	public virtual Material material
	{
		get
		{
			return null;
		}
		set
		{
			throw new NotImplementedException(GetType() + " has no material setter");
		}
	}

	public virtual Texture mainTexture
	{
		get
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			Material material = this.material;
			if (material != null && material.get_shader() != null && !material.get_shader().get_isSupported())
			{
				Log.Error("[UIWidget] no support shader : {0} : {1}", material.get_shader().get_name(), this.get_name());
				return null;
			}
			return (!(material != null)) ? null : material.get_mainTexture();
		}
		set
		{
			throw new NotImplementedException(GetType() + " has no mainTexture setter");
		}
	}

	public virtual Shader shader
	{
		get
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			Material material = this.material;
			return (!(material != null)) ? null : material.get_shader();
		}
		set
		{
			throw new NotImplementedException(GetType() + " has no shader setter");
		}
	}

	[Obsolete("There is no relative scale anymore. Widgets now have width and height instead")]
	public Vector2 relativeSize
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Vector2.get_one();
		}
	}

	public bool hasBoxCollider
	{
		get
		{
			BoxCollider val = this.GetComponent<Collider>() as BoxCollider;
			if (val != null)
			{
				return true;
			}
			return this.GetComponent<BoxCollider2D>() != null;
		}
	}

	public virtual int minWidth => 2;

	public virtual int minHeight => 2;

	public virtual Vector4 border
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Vector4.get_zero();
		}
		set
		{
		}
	}

	public void SetDimensions(int w, int h)
	{
		if (mWidth != w || mHeight != h)
		{
			mWidth = w;
			mHeight = h;
			if (keepAspectRatio == AspectRatioSource.BasedOnWidth)
			{
				mHeight = Mathf.RoundToInt((float)mWidth / aspectRatio);
			}
			else if (keepAspectRatio == AspectRatioSource.BasedOnHeight)
			{
				mWidth = Mathf.RoundToInt((float)mHeight * aspectRatio);
			}
			else if (keepAspectRatio == AspectRatioSource.Free)
			{
				aspectRatio = (float)mWidth / (float)mHeight;
			}
			mMoved = true;
			if (autoResizeBoxCollider)
			{
				ResizeCollider();
			}
			MarkAsChanged();
		}
	}

	public override Vector3[] GetSides(Transform relativeTo)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pivotOffset = this.pivotOffset;
		float num = (0f - pivotOffset.x) * (float)mWidth;
		float num2 = (0f - pivotOffset.y) * (float)mHeight;
		float num3 = num + (float)mWidth;
		float num4 = num2 + (float)mHeight;
		float num5 = (num + num3) * 0.5f;
		float num6 = (num2 + num4) * 0.5f;
		Transform cachedTransform = base.cachedTransform;
		mCorners[0] = cachedTransform.TransformPoint(num, num6, 0f);
		mCorners[1] = cachedTransform.TransformPoint(num5, num4, 0f);
		mCorners[2] = cachedTransform.TransformPoint(num3, num6, 0f);
		mCorners[3] = cachedTransform.TransformPoint(num5, num2, 0f);
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				mCorners[i] = relativeTo.InverseTransformPoint(mCorners[i]);
			}
		}
		return mCorners;
	}

	public override float CalculateFinalAlpha(int frameID)
	{
		if (mAlphaFrameID != frameID)
		{
			mAlphaFrameID = frameID;
			UpdateFinalAlpha(frameID);
		}
		return finalAlpha;
	}

	protected void UpdateFinalAlpha(int frameID)
	{
		if (!mIsVisibleByAlpha || !mIsInFront)
		{
			finalAlpha = 0f;
		}
		else
		{
			UIRect parent = base.parent;
			finalAlpha = ((!(parent != null)) ? mColor.a : (parent.CalculateFinalAlpha(frameID) * mColor.a));
		}
	}

	public override void Invalidate(bool includeChildren)
	{
		mChanged = true;
		mAlphaFrameID = -1;
		if (panel != null)
		{
			bool visibleByPanel = (!hideIfOffScreen && !panel.hasCumulativeClipping) || panel.IsVisible(this);
			UpdateVisibility(CalculateCumulativeAlpha(Time.get_frameCount()) > 0.001f, visibleByPanel);
			UpdateFinalAlpha(Time.get_frameCount());
			if (includeChildren)
			{
				base.Invalidate(true);
			}
		}
	}

	public float CalculateCumulativeAlpha(int frameID)
	{
		UIRect parent = base.parent;
		return (!(parent != null)) ? mColor.a : (parent.CalculateFinalAlpha(frameID) * mColor.a);
	}

	public override void SetRect(float x, float y, float width, float height)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		Vector2 pivotOffset = this.pivotOffset;
		float num = Mathf.Lerp(x, x + width, pivotOffset.x);
		float num2 = Mathf.Lerp(y, y + height, pivotOffset.y);
		int num3 = Mathf.FloorToInt(width + 0.5f);
		int num4 = Mathf.FloorToInt(height + 0.5f);
		if (pivotOffset.x == 0.5f)
		{
			num3 = num3 >> 1 << 1;
		}
		if (pivotOffset.y == 0.5f)
		{
			num4 = num4 >> 1 << 1;
		}
		Transform cachedTransform = base.cachedTransform;
		Vector3 localPosition = cachedTransform.get_localPosition();
		localPosition.x = Mathf.Floor(num + 0.5f);
		localPosition.y = Mathf.Floor(num2 + 0.5f);
		if (num3 < minWidth)
		{
			num3 = minWidth;
		}
		if (num4 < minHeight)
		{
			num4 = minHeight;
		}
		cachedTransform.set_localPosition(localPosition);
		this.width = num3;
		this.height = num4;
		if (base.isAnchored)
		{
			cachedTransform = cachedTransform.get_parent();
			if (Object.op_Implicit(leftAnchor.target))
			{
				leftAnchor.SetHorizontal(cachedTransform, x);
			}
			if (Object.op_Implicit(rightAnchor.target))
			{
				rightAnchor.SetHorizontal(cachedTransform, x + width);
			}
			if (Object.op_Implicit(bottomAnchor.target))
			{
				bottomAnchor.SetVertical(cachedTransform, y);
			}
			if (Object.op_Implicit(topAnchor.target))
			{
				topAnchor.SetVertical(cachedTransform, y + height);
			}
		}
	}

	public void ResizeCollider()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		if (NGUITools.GetActive(this))
		{
			NGUITools.UpdateWidgetCollider(this.get_gameObject());
		}
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static int FullCompareFunc(UIWidget left, UIWidget right)
	{
		int num = UIPanel.CompareFunc(left.panel, right.panel);
		return (num != 0) ? num : PanelCompareFunc(left, right);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int PanelCompareFunc(UIWidget left, UIWidget right)
	{
		if (left.mDepth < right.mDepth)
		{
			return -1;
		}
		if (left.mDepth > right.mDepth)
		{
			return 1;
		}
		Material material = left.material;
		Material material2 = right.material;
		if (material == material2)
		{
			return 0;
		}
		if (material != null)
		{
			return -1;
		}
		if (material2 != null)
		{
			return 1;
		}
		return (material.GetInstanceID() >= material2.GetInstanceID()) ? 1 : (-1);
	}

	public Bounds CalculateBounds()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return CalculateBounds(null);
	}

	public Bounds CalculateBounds(Transform relativeParent)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		if (relativeParent == null)
		{
			Vector3[] localCorners = this.localCorners;
			Bounds result = default(Bounds);
			result._002Ector(localCorners[0], Vector3.get_zero());
			for (int i = 1; i < 4; i++)
			{
				result.Encapsulate(localCorners[i]);
			}
			return result;
		}
		Matrix4x4 worldToLocalMatrix = relativeParent.get_worldToLocalMatrix();
		Vector3[] worldCorners = this.worldCorners;
		Bounds result2 = default(Bounds);
		result2._002Ector(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[0]), Vector3.get_zero());
		for (int j = 1; j < 4; j++)
		{
			result2.Encapsulate(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[j]));
		}
		return result2;
	}

	public void SetDirty()
	{
		if (drawCall != null)
		{
			drawCall.isDirty = true;
		}
		else if (isVisible && hasVertices)
		{
			CreatePanel();
		}
	}

	public void RemoveFromPanel()
	{
		if (panel != null)
		{
			panel.RemoveWidget(this);
			panel = null;
		}
		drawCall = null;
	}

	public virtual void MarkAsChanged()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		if (NGUITools.GetActive(this))
		{
			mChanged = true;
			if (panel != null && this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && !mPlayMode)
			{
				SetDirty();
				CheckLayer();
			}
		}
	}

	public UIPanel CreatePanel()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		if (mStarted && panel == null && this.get_enabled() && NGUITools.GetActive(this.get_gameObject()))
		{
			panel = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.get_layer());
			if (panel != null)
			{
				mParentFound = false;
				panel.AddWidget(this);
				CheckLayer();
				Invalidate(true);
			}
		}
		return panel;
	}

	public void CheckLayer()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (panel != null && panel.get_gameObject().get_layer() != this.get_gameObject().get_layer())
		{
			Debug.LogWarning((object)"You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			this.get_gameObject().set_layer(panel.get_gameObject().get_layer());
		}
	}

	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		if (panel != null)
		{
			UIPanel uIPanel = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.get_layer());
			if (panel != uIPanel)
			{
				RemoveFromPanel();
				CreatePanel();
			}
		}
	}

	protected virtual void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		mGo = this.get_gameObject();
		mPlayMode = Application.get_isPlaying();
	}

	protected override void OnInit()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		base.OnInit();
		RemoveFromPanel();
		mMoved = true;
		if (mWidth == 100 && mHeight == 100)
		{
			Vector3 localScale = base.cachedTransform.get_localScale();
			if (localScale.get_magnitude() > 8f)
			{
				UpgradeFrom265();
				base.cachedTransform.set_localScale(Vector3.get_one());
			}
		}
		_Update();
	}

	protected virtual void UpgradeFrom265()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		Vector3 localScale = base.cachedTransform.get_localScale();
		mWidth = Mathf.Abs(Mathf.RoundToInt(localScale.x));
		mHeight = Mathf.Abs(Mathf.RoundToInt(localScale.y));
		NGUITools.UpdateWidgetCollider(this.get_gameObject(), true);
	}

	protected override void OnStart()
	{
		CreatePanel();
	}

	protected override void OnAnchor()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		Transform cachedTransform = base.cachedTransform;
		Transform val = cachedTransform.get_parent();
		Vector3 localPosition = cachedTransform.get_localPosition();
		Vector2 pivotOffset = this.pivotOffset;
		float num;
		float num2;
		float num3;
		float num4;
		if (leftAnchor.target == bottomAnchor.target && leftAnchor.target == rightAnchor.target && leftAnchor.target == topAnchor.target)
		{
			Vector3[] sides = leftAnchor.GetSides(val);
			if (sides != null)
			{
				num = NGUIMath.Lerp(sides[0].x, sides[2].x, leftAnchor.relative) + (float)leftAnchor.absolute;
				num2 = NGUIMath.Lerp(sides[0].x, sides[2].x, rightAnchor.relative) + (float)rightAnchor.absolute;
				num3 = NGUIMath.Lerp(sides[3].y, sides[1].y, bottomAnchor.relative) + (float)bottomAnchor.absolute;
				num4 = NGUIMath.Lerp(sides[3].y, sides[1].y, topAnchor.relative) + (float)topAnchor.absolute;
				mIsInFront = true;
			}
			else
			{
				Vector3 localPos = GetLocalPos(leftAnchor, val);
				num = localPos.x + (float)leftAnchor.absolute;
				num3 = localPos.y + (float)bottomAnchor.absolute;
				num2 = localPos.x + (float)rightAnchor.absolute;
				num4 = localPos.y + (float)topAnchor.absolute;
				mIsInFront = (!hideIfOffScreen || localPos.z >= 0f);
			}
		}
		else
		{
			mIsInFront = true;
			if (Object.op_Implicit(leftAnchor.target))
			{
				Vector3[] sides2 = leftAnchor.GetSides(val);
				if (sides2 != null)
				{
					num = NGUIMath.Lerp(sides2[0].x, sides2[2].x, leftAnchor.relative) + (float)leftAnchor.absolute;
				}
				else
				{
					Vector3 localPos2 = GetLocalPos(leftAnchor, val);
					num = localPos2.x + (float)leftAnchor.absolute;
				}
			}
			else
			{
				num = localPosition.x - pivotOffset.x * (float)mWidth;
			}
			if (Object.op_Implicit(rightAnchor.target))
			{
				Vector3[] sides3 = rightAnchor.GetSides(val);
				if (sides3 != null)
				{
					num2 = NGUIMath.Lerp(sides3[0].x, sides3[2].x, rightAnchor.relative) + (float)rightAnchor.absolute;
				}
				else
				{
					Vector3 localPos3 = GetLocalPos(rightAnchor, val);
					num2 = localPos3.x + (float)rightAnchor.absolute;
				}
			}
			else
			{
				num2 = localPosition.x - pivotOffset.x * (float)mWidth + (float)mWidth;
			}
			if (Object.op_Implicit(bottomAnchor.target))
			{
				Vector3[] sides4 = bottomAnchor.GetSides(val);
				if (sides4 != null)
				{
					num3 = NGUIMath.Lerp(sides4[3].y, sides4[1].y, bottomAnchor.relative) + (float)bottomAnchor.absolute;
				}
				else
				{
					Vector3 localPos4 = GetLocalPos(bottomAnchor, val);
					num3 = localPos4.y + (float)bottomAnchor.absolute;
				}
			}
			else
			{
				num3 = localPosition.y - pivotOffset.y * (float)mHeight;
			}
			if (Object.op_Implicit(topAnchor.target))
			{
				Vector3[] sides5 = topAnchor.GetSides(val);
				if (sides5 != null)
				{
					num4 = NGUIMath.Lerp(sides5[3].y, sides5[1].y, topAnchor.relative) + (float)topAnchor.absolute;
				}
				else
				{
					Vector3 localPos5 = GetLocalPos(topAnchor, val);
					num4 = localPos5.y + (float)topAnchor.absolute;
				}
			}
			else
			{
				num4 = localPosition.y - pivotOffset.y * (float)mHeight + (float)mHeight;
			}
		}
		Vector3 val2 = default(Vector3);
		val2._002Ector(Mathf.Lerp(num, num2, pivotOffset.x), Mathf.Lerp(num3, num4, pivotOffset.y), localPosition.z);
		val2.x = Mathf.Round(val2.x);
		val2.y = Mathf.Round(val2.y);
		int num5 = Mathf.FloorToInt(num2 - num + 0.5f);
		int num6 = Mathf.FloorToInt(num4 - num3 + 0.5f);
		if (keepAspectRatio != 0 && aspectRatio != 0f)
		{
			if (keepAspectRatio == AspectRatioSource.BasedOnHeight)
			{
				num5 = Mathf.RoundToInt((float)num6 * aspectRatio);
			}
			else
			{
				num6 = Mathf.RoundToInt((float)num5 / aspectRatio);
			}
		}
		if (num5 < minWidth)
		{
			num5 = minWidth;
		}
		if (num6 < minHeight)
		{
			num6 = minHeight;
		}
		if (Vector3.SqrMagnitude(localPosition - val2) > 0.001f)
		{
			base.cachedTransform.set_localPosition(val2);
			if (mIsInFront)
			{
				mChanged = true;
			}
		}
		if (mWidth != num5 || mHeight != num6)
		{
			mWidth = num5;
			mHeight = num6;
			if (mIsInFront)
			{
				mChanged = true;
			}
			if (autoResizeBoxCollider)
			{
				ResizeCollider();
			}
		}
	}

	protected override void OnUpdate()
	{
		if (panel == null)
		{
			CreatePanel();
		}
	}

	private void OnApplicationPause(bool paused)
	{
		if (!paused)
		{
			MarkAsChanged();
		}
	}

	protected override void OnDisable()
	{
		RemoveFromPanel();
		base.OnDisable();
	}

	private void OnDestroy()
	{
		RemoveFromPanel();
	}

	public bool UpdateVisibility(bool visibleByAlpha, bool visibleByPanel)
	{
		if (mIsVisibleByAlpha != visibleByAlpha || mIsVisibleByPanel != visibleByPanel)
		{
			mChanged = true;
			mIsVisibleByAlpha = visibleByAlpha;
			mIsVisibleByPanel = visibleByPanel;
			return true;
		}
		return false;
	}

	public bool UpdateTransform(int frame)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		Transform cachedTransform = base.cachedTransform;
		mPlayMode = Application.get_isPlaying();
		if (mMoved)
		{
			mMoved = true;
			mMatrixFrame = -1;
			cachedTransform.set_hasChanged(false);
			Vector2 pivotOffset = this.pivotOffset;
			float num = (0f - pivotOffset.x) * (float)mWidth;
			float num2 = (0f - pivotOffset.y) * (float)mHeight;
			float num3 = num + (float)mWidth;
			float num4 = num2 + (float)mHeight;
			mOldV0 = panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(num, num2, 0f));
			mOldV1 = panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(num3, num4, 0f));
		}
		else if (!panel.widgetsAreStatic && cachedTransform.get_hasChanged())
		{
			mMoved = true;
			mMatrixFrame = -1;
			cachedTransform.set_hasChanged(false);
			Vector2 pivotOffset2 = this.pivotOffset;
			float num5 = (0f - pivotOffset2.x) * (float)mWidth;
			float num6 = (0f - pivotOffset2.y) * (float)mHeight;
			float num7 = num5 + (float)mWidth;
			float num8 = num6 + (float)mHeight;
			Vector3 val = panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(num5, num6, 0f));
			Vector3 val2 = panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(num7, num8, 0f));
			if (Vector3.SqrMagnitude(mOldV0 - val) > 1E-06f || Vector3.SqrMagnitude(mOldV1 - val2) > 1E-06f)
			{
				mMoved = true;
				mOldV0 = val;
				mOldV1 = val2;
			}
		}
		if (mMoved && onChange != null)
		{
			onChange();
		}
		return mMoved || mChanged;
	}

	public bool UpdateGeometry(int frame)
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		float num = CalculateFinalAlpha(frame);
		if (mIsVisibleByAlpha && mLastAlpha != num)
		{
			mChanged = true;
		}
		mLastAlpha = num;
		if (mChanged)
		{
			mChanged = false;
			if (mIsVisibleByAlpha && num > 0.001f && shader != null)
			{
				bool hasVertices = geometry.hasVertices;
				if (fillGeometry)
				{
					geometry.Clear();
					OnFill(geometry.verts, geometry.uvs, geometry.cols);
				}
				if (geometry.hasVertices)
				{
					if (mMatrixFrame != frame)
					{
						mLocalToPanel = panel.worldToLocal * base.cachedTransform.get_localToWorldMatrix();
						mMatrixFrame = frame;
					}
					geometry.ApplyTransform(mLocalToPanel, panel.generateNormals);
					mMoved = false;
					return true;
				}
				return hasVertices;
			}
			if (geometry.hasVertices)
			{
				if (fillGeometry)
				{
					geometry.Clear();
				}
				mMoved = false;
				return true;
			}
		}
		else if (mMoved && geometry.hasVertices)
		{
			if (mMatrixFrame != frame)
			{
				mLocalToPanel = panel.worldToLocal * base.cachedTransform.get_localToWorldMatrix();
				mMatrixFrame = frame;
			}
			geometry.ApplyTransform(mLocalToPanel, panel.generateNormals);
			mMoved = false;
			return true;
		}
		mMoved = false;
		return false;
	}

	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		geometry.WriteToBuffers(v, u, c, n, t);
	}

	public virtual void MakePixelPerfect()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localPosition = base.cachedTransform.get_localPosition();
		localPosition.z = Mathf.Round(localPosition.z);
		localPosition.x = Mathf.Round(localPosition.x);
		localPosition.y = Mathf.Round(localPosition.y);
		base.cachedTransform.set_localPosition(localPosition);
		Vector3 localScale = base.cachedTransform.get_localScale();
		base.cachedTransform.set_localScale(new Vector3(Mathf.Sign(localScale.x), Mathf.Sign(localScale.y), 1f));
	}

	public virtual void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
	}
}
