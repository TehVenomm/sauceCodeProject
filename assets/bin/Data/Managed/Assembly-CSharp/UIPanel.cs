using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Panel")]
public class UIPanel : UIRect
{
	public enum RenderQueue
	{
		Automatic,
		StartAt,
		Explicit
	}

	public delegate void OnGeometryUpdated();

	public delegate void OnClippingMoved(UIPanel panel);

	public static List<UIPanel> list = new List<UIPanel>();

	public OnGeometryUpdated onGeometryUpdated;

	public bool showInPanelTool = true;

	public bool generateNormals;

	public bool widgetsAreStatic;

	public bool cullWhileDragging = true;

	public bool alwaysOnScreen;

	public bool anchorOffset;

	public bool softBorderPadding = true;

	public RenderQueue renderQueue;

	public int startingRenderQueue = 3000;

	[NonSerialized]
	public List<UIWidget> widgets = new List<UIWidget>();

	[NonSerialized]
	public List<UIDrawCall> drawCalls = new List<UIDrawCall>();

	[NonSerialized]
	public Matrix4x4 worldToLocal = Matrix4x4.get_identity();

	[NonSerialized]
	public Vector4 drawCallClipRange = new Vector4(0f, 0f, 1f, 1f);

	public OnClippingMoved onClipMove;

	[SerializeField]
	[HideInInspector]
	private Texture2D mClipTexture;

	[SerializeField]
	[HideInInspector]
	private float mAlpha = 1f;

	[SerializeField]
	[HideInInspector]
	private UIDrawCall.Clipping mClipping;

	[SerializeField]
	[HideInInspector]
	private Vector4 mClipRange = new Vector4(0f, 0f, 300f, 200f);

	[HideInInspector]
	[SerializeField]
	private Vector2 mClipSoftness = new Vector2(4f, 4f);

	[HideInInspector]
	[SerializeField]
	private int mDepth;

	[SerializeField]
	[HideInInspector]
	private int mSortingOrder;

	private bool mRebuild;

	private bool mResized;

	[SerializeField]
	private Vector2 mClipOffset = Vector2.get_zero();

	private int mMatrixFrame = -1;

	private int mAlphaFrameID;

	private int mLayer = -1;

	private static float[] mTemp = new float[4];

	private Vector2 mMin = Vector2.get_zero();

	private Vector2 mMax = Vector2.get_zero();

	private bool mHalfPixelOffset;

	private bool mSortWidgets;

	private bool mUpdateScroll;

	private UIPanel mParentPanel;

	private static Vector3[] mCorners = (Vector3[])new Vector3[4];

	private static int mUpdateFrame = -1;

	private UIDrawCall.OnRenderCallback mOnRender;

	private bool mForced;

	public static int nextUnusedDepth
	{
		get
		{
			int num = -2147483648;
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				num = Mathf.Max(num, list[i].depth);
			}
			return (num != -2147483648) ? (num + 1) : 0;
		}
	}

	public override bool canBeAnchored => mClipping != UIDrawCall.Clipping.None;

	public override float alpha
	{
		get
		{
			return mAlpha;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (mAlpha != num)
			{
				mAlphaFrameID = -1;
				mResized = true;
				mAlpha = num;
				SetDirty();
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
				mDepth = value;
				list.Sort(CompareFunc);
			}
		}
	}

	public int sortingOrder
	{
		get
		{
			return mSortingOrder;
		}
		set
		{
			if (mSortingOrder != value)
			{
				mSortingOrder = value;
				UpdateDrawCalls();
			}
		}
	}

	public float width
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			Vector2 viewSize = GetViewSize();
			return viewSize.x;
		}
	}

	public float height
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			Vector2 viewSize = GetViewSize();
			return viewSize.y;
		}
	}

	public bool halfPixelOffset => mHalfPixelOffset;

	public bool usedForUI => base.anchorCamera != null && mCam.get_orthographic();

	public Vector3 drawCallOffset
	{
		get
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			if (base.anchorCamera != null && mCam.get_orthographic())
			{
				Vector2 windowSize = GetWindowSize();
				float num = (!(base.root != null)) ? 1f : base.root.pixelSizeAdjustment;
				float num2 = num / windowSize.y / mCam.get_orthographicSize();
				bool flag = mHalfPixelOffset;
				bool flag2 = mHalfPixelOffset;
				if ((Mathf.RoundToInt(windowSize.x) & 1) == 1)
				{
					flag = !flag;
				}
				if ((Mathf.RoundToInt(windowSize.y) & 1) == 1)
				{
					flag2 = !flag2;
				}
				return new Vector3((!flag) ? 0f : (0f - num2), (!flag2) ? 0f : num2);
			}
			return Vector3.get_zero();
		}
	}

	public UIDrawCall.Clipping clipping
	{
		get
		{
			return mClipping;
		}
		set
		{
			if (mClipping != value)
			{
				mResized = true;
				mClipping = value;
				mMatrixFrame = -1;
			}
		}
	}

	public UIPanel parentPanel => mParentPanel;

	public int clipCount
	{
		get
		{
			int num = 0;
			UIPanel uIPanel = this;
			while (uIPanel != null)
			{
				if (uIPanel.mClipping == UIDrawCall.Clipping.SoftClip || uIPanel.mClipping == UIDrawCall.Clipping.TextureMask)
				{
					num++;
				}
				uIPanel = uIPanel.mParentPanel;
			}
			return num;
		}
	}

	public bool hasClipping => mClipping == UIDrawCall.Clipping.SoftClip || mClipping == UIDrawCall.Clipping.TextureMask;

	public bool hasCumulativeClipping => clipCount != 0;

	[Obsolete("Use 'hasClipping' or 'hasCumulativeClipping' instead")]
	public bool clipsChildren
	{
		get
		{
			return hasCumulativeClipping;
		}
	}

	public Vector2 clipOffset
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mClipOffset;
		}
		set
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			if (Mathf.Abs(mClipOffset.x - value.x) > 0.001f || Mathf.Abs(mClipOffset.y - value.y) > 0.001f)
			{
				mClipOffset = value;
				InvalidateClipping();
				if (onClipMove != null)
				{
					onClipMove(this);
				}
			}
		}
	}

	public Texture2D clipTexture
	{
		get
		{
			return mClipTexture;
		}
		set
		{
			if (mClipTexture != value)
			{
				mClipTexture = value;
			}
		}
	}

	[Obsolete("Use 'finalClipRegion' or 'baseClipRegion' instead")]
	public Vector4 clipRange
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return baseClipRegion;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			baseClipRegion = value;
		}
	}

	public Vector4 baseClipRegion
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mClipRange;
		}
		set
		{
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			if (Mathf.Abs(mClipRange.x - value.x) > 0.001f || Mathf.Abs(mClipRange.y - value.y) > 0.001f || Mathf.Abs(mClipRange.z - value.z) > 0.001f || Mathf.Abs(mClipRange.w - value.w) > 0.001f)
			{
				mResized = true;
				mClipRange = value;
				mMatrixFrame = -1;
				UIScrollView component = this.GetComponent<UIScrollView>();
				if (component != null)
				{
					component.UpdatePosition();
				}
				if (onClipMove != null)
				{
					onClipMove(this);
				}
			}
		}
	}

	public Vector4 finalClipRegion
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			Vector2 viewSize = GetViewSize();
			if (mClipping != 0)
			{
				return new Vector4(mClipRange.x + mClipOffset.x, mClipRange.y + mClipOffset.y, viewSize.x, viewSize.y);
			}
			return new Vector4(0f, 0f, viewSize.x, viewSize.y);
		}
	}

	public Vector2 clipSoftness
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mClipSoftness;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (mClipSoftness != value)
			{
				mClipSoftness = value;
			}
		}
	}

	public override Vector3[] localCorners
	{
		get
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			if (mClipping == UIDrawCall.Clipping.None)
			{
				Vector3[] worldCorners = this.worldCorners;
				Transform cachedTransform = base.cachedTransform;
				for (int i = 0; i < 4; i++)
				{
					worldCorners[i] = cachedTransform.InverseTransformPoint(worldCorners[i]);
				}
				return worldCorners;
			}
			float num = mClipOffset.x + mClipRange.x - 0.5f * mClipRange.z;
			float num2 = mClipOffset.y + mClipRange.y - 0.5f * mClipRange.w;
			float num3 = num + mClipRange.z;
			float num4 = num2 + mClipRange.w;
			mCorners[0] = new Vector3(num, num2);
			mCorners[1] = new Vector3(num, num4);
			mCorners[2] = new Vector3(num3, num4);
			mCorners[3] = new Vector3(num3, num2);
			return mCorners;
		}
	}

	public override Vector3[] worldCorners
	{
		get
		{
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_0179: Unknown result type (might be due to invalid IL or missing references)
			//IL_018d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_020b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0210: Unknown result type (might be due to invalid IL or missing references)
			//IL_0227: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Unknown result type (might be due to invalid IL or missing references)
			//IL_022e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0233: Unknown result type (might be due to invalid IL or missing references)
			if (mClipping != 0)
			{
				float num = mClipOffset.x + mClipRange.x - 0.5f * mClipRange.z;
				float num2 = mClipOffset.y + mClipRange.y - 0.5f * mClipRange.w;
				float num3 = num + mClipRange.z;
				float num4 = num2 + mClipRange.w;
				Transform cachedTransform = base.cachedTransform;
				mCorners[0] = cachedTransform.TransformPoint(num, num2, 0f);
				mCorners[1] = cachedTransform.TransformPoint(num, num4, 0f);
				mCorners[2] = cachedTransform.TransformPoint(num3, num4, 0f);
				mCorners[3] = cachedTransform.TransformPoint(num3, num2, 0f);
			}
			else
			{
				if (base.anchorCamera != null)
				{
					return mCam.GetWorldCorners(base.cameraRayDistance);
				}
				Vector2 viewSize = GetViewSize();
				float num5 = -0.5f * viewSize.x;
				float num6 = -0.5f * viewSize.y;
				float num7 = num5 + viewSize.x;
				float num8 = num6 + viewSize.y;
				mCorners[0] = new Vector3(num5, num6);
				mCorners[1] = new Vector3(num5, num8);
				mCorners[2] = new Vector3(num7, num8);
				mCorners[3] = new Vector3(num7, num6);
				if (anchorOffset && (mCam == null || mCam.get_transform().get_parent() != base.cachedTransform))
				{
					Vector3 position = base.cachedTransform.get_position();
					for (int i = 0; i < 4; i++)
					{
						ref Vector3 reference = ref mCorners[i];
						reference += position;
					}
				}
			}
			return mCorners;
		}
	}

	public static int CompareFunc(UIPanel a, UIPanel b)
	{
		if (a != b && a != null && b != null)
		{
			if (a.mDepth < b.mDepth)
			{
				return -1;
			}
			if (a.mDepth > b.mDepth)
			{
				return 1;
			}
			return (a.GetInstanceID() >= b.GetInstanceID()) ? 1 : (-1);
		}
		return 0;
	}

	private void InvalidateClipping()
	{
		mResized = true;
		mMatrixFrame = -1;
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			UIPanel uIPanel = list[i];
			if (uIPanel != this && uIPanel.parentPanel == this)
			{
				uIPanel.InvalidateClipping();
			}
		}
	}

	public override Vector3[] GetSides(Transform relativeTo)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		if (mClipping != 0)
		{
			float num = mClipOffset.x + mClipRange.x - 0.5f * mClipRange.z;
			float num2 = mClipOffset.y + mClipRange.y - 0.5f * mClipRange.w;
			float num3 = num + mClipRange.z;
			float num4 = num2 + mClipRange.w;
			float num5 = (num + num3) * 0.5f;
			float num6 = (num2 + num4) * 0.5f;
			Transform cachedTransform = base.cachedTransform;
			UIRect.mSides[0] = cachedTransform.TransformPoint(num, num6, 0f);
			UIRect.mSides[1] = cachedTransform.TransformPoint(num5, num4, 0f);
			UIRect.mSides[2] = cachedTransform.TransformPoint(num3, num6, 0f);
			UIRect.mSides[3] = cachedTransform.TransformPoint(num5, num2, 0f);
			if (relativeTo != null)
			{
				for (int i = 0; i < 4; i++)
				{
					UIRect.mSides[i] = relativeTo.InverseTransformPoint(UIRect.mSides[i]);
				}
			}
			return UIRect.mSides;
		}
		if (base.anchorCamera != null && anchorOffset)
		{
			Vector3[] sides = mCam.GetSides(base.cameraRayDistance);
			Vector3 position = base.cachedTransform.get_position();
			for (int j = 0; j < 4; j++)
			{
				ref Vector3 reference = ref sides[j];
				reference += position;
			}
			if (relativeTo != null)
			{
				for (int k = 0; k < 4; k++)
				{
					sides[k] = relativeTo.InverseTransformPoint(sides[k]);
				}
			}
			return sides;
		}
		return base.GetSides(relativeTo);
	}

	public override void Invalidate(bool includeChildren)
	{
		mAlphaFrameID = -1;
		base.Invalidate(includeChildren);
	}

	public override float CalculateFinalAlpha(int frameID)
	{
		if (mAlphaFrameID != frameID)
		{
			mAlphaFrameID = frameID;
			UIRect parent = base.parent;
			finalAlpha = ((!(base.parent != null)) ? mAlpha : (parent.CalculateFinalAlpha(frameID) * mAlpha));
		}
		return finalAlpha;
	}

	public override void SetRect(float x, float y, float width, float height)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		int num = Mathf.FloorToInt(width + 0.5f);
		int num2 = Mathf.FloorToInt(height + 0.5f);
		num = num >> 1 << 1;
		num2 = num2 >> 1 << 1;
		Transform cachedTransform = base.cachedTransform;
		Vector3 localPosition = cachedTransform.get_localPosition();
		localPosition.x = Mathf.Floor(x + 0.5f);
		localPosition.y = Mathf.Floor(y + 0.5f);
		if (num < 2)
		{
			num = 2;
		}
		if (num2 < 2)
		{
			num2 = 2;
		}
		baseClipRegion = new Vector4(localPosition.x, localPosition.y, (float)num, (float)num2);
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

	public bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		UpdateTransformMatrix();
		a = worldToLocal.MultiplyPoint3x4(a);
		b = worldToLocal.MultiplyPoint3x4(b);
		c = worldToLocal.MultiplyPoint3x4(c);
		d = worldToLocal.MultiplyPoint3x4(d);
		mTemp[0] = a.x;
		mTemp[1] = b.x;
		mTemp[2] = c.x;
		mTemp[3] = d.x;
		float num = Mathf.Min(mTemp);
		float num2 = Mathf.Max(mTemp);
		mTemp[0] = a.y;
		mTemp[1] = b.y;
		mTemp[2] = c.y;
		mTemp[3] = d.y;
		float num3 = Mathf.Min(mTemp);
		float num4 = Mathf.Max(mTemp);
		if (num2 < mMin.x)
		{
			return false;
		}
		if (num4 < mMin.y)
		{
			return false;
		}
		if (num > mMax.x)
		{
			return false;
		}
		if (num3 > mMax.y)
		{
			return false;
		}
		return true;
	}

	public bool IsVisible(Vector3 worldPos)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (mAlpha < 0.001f)
		{
			return false;
		}
		if (mClipping == UIDrawCall.Clipping.None || mClipping == UIDrawCall.Clipping.ConstrainButDontClip)
		{
			return true;
		}
		UpdateTransformMatrix();
		Vector3 val = worldToLocal.MultiplyPoint3x4(worldPos);
		if (val.x < mMin.x)
		{
			return false;
		}
		if (val.y < mMin.y)
		{
			return false;
		}
		if (val.x > mMax.x)
		{
			return false;
		}
		if (val.y > mMax.y)
		{
			return false;
		}
		return true;
	}

	public bool IsVisible(UIWidget w)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		UIPanel uIPanel = this;
		Vector3[] array = null;
		while (uIPanel != null)
		{
			if ((uIPanel.mClipping == UIDrawCall.Clipping.None || uIPanel.mClipping == UIDrawCall.Clipping.ConstrainButDontClip) && !w.hideIfOffScreen)
			{
				uIPanel = uIPanel.mParentPanel;
			}
			else
			{
				if (array == null)
				{
					array = w.worldCorners;
				}
				if (!uIPanel.IsVisible(array[0], array[1], array[2], array[3]))
				{
					return false;
				}
				uIPanel = uIPanel.mParentPanel;
			}
		}
		return true;
	}

	public bool Affects(UIWidget w)
	{
		if (w == null)
		{
			return false;
		}
		UIPanel panel = w.panel;
		if (panel == null)
		{
			return false;
		}
		UIPanel uIPanel = this;
		while (uIPanel != null)
		{
			if (uIPanel == panel)
			{
				return true;
			}
			if (!uIPanel.hasCumulativeClipping)
			{
				return false;
			}
			uIPanel = uIPanel.mParentPanel;
		}
		return false;
	}

	[ContextMenu("Force Refresh")]
	public void RebuildAllDrawCalls()
	{
		mRebuild = true;
	}

	public void SetDirty()
	{
		int i = 0;
		for (int count = drawCalls.Count; i < count; i++)
		{
			drawCalls[i].isDirty = true;
		}
		Invalidate(true);
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Invalid comparison between Unknown and I4
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between Unknown and I4
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Invalid comparison between Unknown and I4
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		mGo = this.get_gameObject();
		mTrans = this.get_transform();
		mHalfPixelOffset = ((int)Application.get_platform() == 2 || (int)Application.get_platform() == 10 || (int)Application.get_platform() == 5 || (int)Application.get_platform() == 7);
		if (mHalfPixelOffset && SystemInfo.get_graphicsDeviceVersion().Contains("Direct3D"))
		{
			mHalfPixelOffset = (SystemInfo.get_graphicsShaderLevel() < 40);
		}
	}

	private void FindParent()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		Transform val = base.cachedTransform.get_parent();
		mParentPanel = ((!(val != null)) ? null : NGUITools.FindInParents<UIPanel>(val.get_gameObject()));
	}

	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		FindParent();
	}

	protected override void OnStart()
	{
		mLayer = mGo.get_layer();
	}

	protected override void OnEnable()
	{
		mRebuild = true;
		mAlphaFrameID = -1;
		mMatrixFrame = -1;
		OnStart();
		base.OnEnable();
		mMatrixFrame = -1;
	}

	protected override void OnInit()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (!list.Contains(this))
		{
			base.OnInit();
			FindParent();
			if (this.GetComponent<Rigidbody>() == null && mParentPanel == null)
			{
				UICamera uICamera = (!(base.anchorCamera != null)) ? null : mCam.GetComponent<UICamera>();
				if (uICamera != null && (uICamera.eventType == UICamera.EventType.UI_3D || uICamera.eventType == UICamera.EventType.World_3D))
				{
					Rigidbody val = this.get_gameObject().AddComponent<Rigidbody>();
					val.set_isKinematic(true);
					val.set_useGravity(false);
				}
			}
			mRebuild = true;
			mAlphaFrameID = -1;
			mMatrixFrame = -1;
			list.Add(this);
			list.Sort(CompareFunc);
		}
	}

	protected override void OnDisable()
	{
		int i = 0;
		for (int count = drawCalls.Count; i < count; i++)
		{
			UIDrawCall uIDrawCall = drawCalls[i];
			if (uIDrawCall != null)
			{
				UIDrawCall.Destroy(uIDrawCall);
			}
		}
		drawCalls.Clear();
		list.Remove(this);
		mAlphaFrameID = -1;
		mMatrixFrame = -1;
		if (list.Count == 0)
		{
			UIDrawCall.ReleaseAll();
			mUpdateFrame = -1;
		}
		base.OnDisable();
	}

	private void UpdateTransformMatrix()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		int frameCount = Time.get_frameCount();
		if (mMatrixFrame != frameCount)
		{
			mMatrixFrame = frameCount;
			worldToLocal = base.cachedTransform.get_worldToLocalMatrix();
			Vector2 val = GetViewSize() * 0.5f;
			float num = mClipOffset.x + mClipRange.x;
			float num2 = mClipOffset.y + mClipRange.y;
			mMin.x = num - val.x;
			mMin.y = num2 - val.y;
			mMax.x = num + val.x;
			mMax.y = num2 + val.y;
		}
	}

	protected override void OnAnchor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		if (mClipping != 0)
		{
			Transform cachedTransform = base.cachedTransform;
			Transform val = cachedTransform.get_parent();
			Vector2 viewSize = GetViewSize();
			Vector2 val2 = Vector2.op_Implicit(cachedTransform.get_localPosition());
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
				}
				else
				{
					Vector2 val3 = Vector2.op_Implicit(GetLocalPos(leftAnchor, val));
					num = val3.x + (float)leftAnchor.absolute;
					num3 = val3.y + (float)bottomAnchor.absolute;
					num2 = val3.x + (float)rightAnchor.absolute;
					num4 = val3.y + (float)topAnchor.absolute;
				}
			}
			else
			{
				if (Object.op_Implicit(leftAnchor.target))
				{
					Vector3[] sides2 = leftAnchor.GetSides(val);
					if (sides2 != null)
					{
						num = NGUIMath.Lerp(sides2[0].x, sides2[2].x, leftAnchor.relative) + (float)leftAnchor.absolute;
					}
					else
					{
						Vector3 localPos = GetLocalPos(leftAnchor, val);
						num = localPos.x + (float)leftAnchor.absolute;
					}
				}
				else
				{
					num = mClipRange.x - 0.5f * viewSize.x;
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
						Vector3 localPos2 = GetLocalPos(rightAnchor, val);
						num2 = localPos2.x + (float)rightAnchor.absolute;
					}
				}
				else
				{
					num2 = mClipRange.x + 0.5f * viewSize.x;
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
						Vector3 localPos3 = GetLocalPos(bottomAnchor, val);
						num3 = localPos3.y + (float)bottomAnchor.absolute;
					}
				}
				else
				{
					num3 = mClipRange.y - 0.5f * viewSize.y;
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
						Vector3 localPos4 = GetLocalPos(topAnchor, val);
						num4 = localPos4.y + (float)topAnchor.absolute;
					}
				}
				else
				{
					num4 = mClipRange.y + 0.5f * viewSize.y;
				}
			}
			num -= val2.x + mClipOffset.x;
			num2 -= val2.x + mClipOffset.x;
			num3 -= val2.y + mClipOffset.y;
			num4 -= val2.y + mClipOffset.y;
			float num5 = Mathf.Lerp(num, num2, 0.5f);
			float num6 = Mathf.Lerp(num3, num4, 0.5f);
			float num7 = num2 - num;
			float num8 = num4 - num3;
			float num9 = Mathf.Max(2f, mClipSoftness.x);
			float num10 = Mathf.Max(2f, mClipSoftness.y);
			if (num7 < num9)
			{
				num7 = num9;
			}
			if (num8 < num10)
			{
				num8 = num10;
			}
			baseClipRegion = new Vector4(num5, num6, num7, num8);
		}
	}

	private void LateUpdate()
	{
		if (mUpdateFrame != Time.get_frameCount())
		{
			mUpdateFrame = Time.get_frameCount();
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				list[i].UpdateSelf();
			}
			int num = 3000;
			int j = 0;
			for (int count2 = list.Count; j < count2; j++)
			{
				UIPanel uIPanel = list[j];
				if (uIPanel.renderQueue == RenderQueue.Automatic)
				{
					uIPanel.startingRenderQueue = num;
					uIPanel.UpdateDrawCalls();
					num += uIPanel.drawCalls.Count;
				}
				else if (uIPanel.renderQueue == RenderQueue.StartAt)
				{
					uIPanel.UpdateDrawCalls();
					if (uIPanel.drawCalls.Count != 0)
					{
						num = Mathf.Max(num, uIPanel.startingRenderQueue + uIPanel.drawCalls.Count);
					}
				}
				else
				{
					uIPanel.UpdateDrawCalls();
					if (uIPanel.drawCalls.Count != 0)
					{
						num = Mathf.Max(num, uIPanel.startingRenderQueue + 1);
					}
				}
			}
		}
	}

	private void UpdateSelf()
	{
		UpdateTransformMatrix();
		UpdateLayers();
		UpdateWidgets();
		if (mRebuild)
		{
			mRebuild = false;
			FillAllDrawCalls();
		}
		else
		{
			int num = 0;
			while (num < drawCalls.Count)
			{
				UIDrawCall uIDrawCall = drawCalls[num];
				if (uIDrawCall.isDirty && !FillDrawCall(uIDrawCall))
				{
					UIDrawCall.Destroy(uIDrawCall);
					drawCalls.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}
		if (mUpdateScroll)
		{
			mUpdateScroll = false;
			UIScrollView component = this.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars();
			}
		}
	}

	public void SortWidgets()
	{
		mSortWidgets = false;
		widgets.Sort(UIWidget.PanelCompareFunc);
	}

	private void FillAllDrawCalls()
	{
		for (int i = 0; i < drawCalls.Count; i++)
		{
			UIDrawCall.Destroy(drawCalls[i]);
		}
		drawCalls.Clear();
		Material val = null;
		Texture val2 = null;
		Shader val3 = null;
		UIDrawCall uIDrawCall = null;
		int num = 0;
		if (mSortWidgets)
		{
			SortWidgets();
		}
		for (int j = 0; j < widgets.Count; j++)
		{
			UIWidget uIWidget = widgets[j];
			if (uIWidget.isVisible && uIWidget.hasVertices)
			{
				Material material = uIWidget.material;
				Texture mainTexture = uIWidget.mainTexture;
				Shader shader = uIWidget.shader;
				if (val != material || val2 != mainTexture || val3 != shader)
				{
					if (uIDrawCall != null && uIDrawCall.verts.size != 0)
					{
						drawCalls.Add(uIDrawCall);
						uIDrawCall.UpdateGeometry(num);
						uIDrawCall.onRender = mOnRender;
						mOnRender = null;
						num = 0;
						uIDrawCall = null;
					}
					val = material;
					val2 = mainTexture;
					val3 = shader;
				}
				if (val != null || val3 != null || val2 != null)
				{
					if (uIDrawCall == null)
					{
						uIDrawCall = UIDrawCall.Create(this, val, val2, val3);
						uIDrawCall.depthStart = uIWidget.depth;
						uIDrawCall.depthEnd = uIDrawCall.depthStart;
						uIDrawCall.panel = this;
					}
					else
					{
						int depth = uIWidget.depth;
						if (depth < uIDrawCall.depthStart)
						{
							uIDrawCall.depthStart = depth;
						}
						if (depth > uIDrawCall.depthEnd)
						{
							uIDrawCall.depthEnd = depth;
						}
					}
					uIWidget.drawCall = uIDrawCall;
					num++;
					if (generateNormals)
					{
						uIWidget.WriteToBuffers(uIDrawCall.verts, uIDrawCall.uvs, uIDrawCall.cols, uIDrawCall.norms, uIDrawCall.tans);
					}
					else
					{
						uIWidget.WriteToBuffers(uIDrawCall.verts, uIDrawCall.uvs, uIDrawCall.cols, null, null);
					}
					if (uIWidget.mOnRender != null)
					{
						if (mOnRender == null)
						{
							mOnRender = uIWidget.mOnRender;
						}
						else
						{
							mOnRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(mOnRender, uIWidget.mOnRender);
						}
					}
				}
			}
			else
			{
				uIWidget.drawCall = null;
			}
		}
		if (uIDrawCall != null && uIDrawCall.verts.size != 0)
		{
			drawCalls.Add(uIDrawCall);
			uIDrawCall.UpdateGeometry(num);
			uIDrawCall.onRender = mOnRender;
			mOnRender = null;
		}
	}

	private bool FillDrawCall(UIDrawCall dc)
	{
		if (dc != null)
		{
			dc.isDirty = false;
			int num = 0;
			int num2 = 0;
			while (num2 < widgets.Count)
			{
				UIWidget uIWidget = widgets[num2];
				if (uIWidget == null)
				{
					widgets.RemoveAt(num2);
				}
				else
				{
					if (uIWidget.drawCall == dc)
					{
						if (uIWidget.isVisible && uIWidget.hasVertices)
						{
							num++;
							if (generateNormals)
							{
								uIWidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, dc.norms, dc.tans);
							}
							else
							{
								uIWidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, null, null);
							}
							if (uIWidget.mOnRender != null)
							{
								if (mOnRender == null)
								{
									mOnRender = uIWidget.mOnRender;
								}
								else
								{
									mOnRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(mOnRender, uIWidget.mOnRender);
								}
							}
						}
						else
						{
							uIWidget.drawCall = null;
						}
					}
					num2++;
				}
			}
			if (dc.verts.size != 0)
			{
				dc.UpdateGeometry(num);
				dc.onRender = mOnRender;
				mOnRender = null;
				return true;
			}
		}
		return false;
	}

	private void UpdateDrawCalls()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		Transform cachedTransform = base.cachedTransform;
		bool usedForUI = this.usedForUI;
		if (clipping != 0)
		{
			drawCallClipRange = finalClipRegion;
			drawCallClipRange.z *= 0.5f;
			drawCallClipRange.w *= 0.5f;
		}
		else
		{
			drawCallClipRange = Vector4.get_zero();
		}
		int width = Screen.get_width();
		int height = Screen.get_height();
		if (drawCallClipRange.z == 0f)
		{
			drawCallClipRange.z = (float)width * 0.5f;
		}
		if (drawCallClipRange.w == 0f)
		{
			drawCallClipRange.w = (float)height * 0.5f;
		}
		if (halfPixelOffset)
		{
			drawCallClipRange.x -= 0.5f;
			drawCallClipRange.y += 0.5f;
		}
		Vector3 val2;
		if (usedForUI)
		{
			Transform val = base.cachedTransform.get_parent();
			val2 = base.cachedTransform.get_localPosition();
			if (clipping != 0)
			{
				val2.x = (float)Mathf.RoundToInt(val2.x);
				val2.y = (float)Mathf.RoundToInt(val2.y);
			}
			if (val != null)
			{
				val2 = val.TransformPoint(val2);
			}
			val2 += drawCallOffset;
		}
		else
		{
			val2 = cachedTransform.get_position();
		}
		Quaternion rotation = cachedTransform.get_rotation();
		Vector3 lossyScale = cachedTransform.get_lossyScale();
		for (int i = 0; i < drawCalls.Count; i++)
		{
			UIDrawCall uIDrawCall = drawCalls[i];
			Transform cachedTransform2 = uIDrawCall.cachedTransform;
			cachedTransform2.set_position(val2);
			cachedTransform2.set_rotation(rotation);
			cachedTransform2.set_localScale(lossyScale);
			uIDrawCall.renderQueue = ((renderQueue != RenderQueue.Explicit) ? (startingRenderQueue + i) : startingRenderQueue);
			uIDrawCall.alwaysOnScreen = (alwaysOnScreen && (mClipping == UIDrawCall.Clipping.None || mClipping == UIDrawCall.Clipping.ConstrainButDontClip));
			uIDrawCall.sortingOrder = mSortingOrder;
			uIDrawCall.clipTexture = mClipTexture;
		}
	}

	private void UpdateLayers()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (mLayer != base.cachedGameObject.get_layer())
		{
			mLayer = mGo.get_layer();
			int i = 0;
			for (int count = widgets.Count; i < count; i++)
			{
				UIWidget uIWidget = widgets[i];
				if (Object.op_Implicit(uIWidget) && uIWidget.parent == this)
				{
					uIWidget.get_gameObject().set_layer(mLayer);
				}
			}
			ResetAnchors();
			for (int j = 0; j < drawCalls.Count; j++)
			{
				drawCalls[j].get_gameObject().set_layer(mLayer);
			}
		}
	}

	private void UpdateWidgets()
	{
		bool flag = false;
		bool flag2 = false;
		bool hasCumulativeClipping = this.hasCumulativeClipping;
		if (!cullWhileDragging)
		{
			for (int i = 0; i < UIScrollView.list.size; i++)
			{
				UIScrollView uIScrollView = UIScrollView.list[i];
				if (uIScrollView.panel == this && uIScrollView.isDragging)
				{
					flag2 = true;
				}
			}
		}
		if (mForced != flag2)
		{
			mForced = flag2;
			mResized = true;
		}
		int frameCount = Time.get_frameCount();
		int j = 0;
		for (int count = widgets.Count; j < count; j++)
		{
			UIWidget uIWidget = widgets[j];
			if (uIWidget.panel == this && uIWidget.get_enabled())
			{
				if (uIWidget.UpdateTransform(frameCount) || mResized)
				{
					bool visibleByAlpha = flag2 || uIWidget.CalculateCumulativeAlpha(frameCount) > 0.001f;
					uIWidget.UpdateVisibility(visibleByAlpha, flag2 || (!hasCumulativeClipping && !uIWidget.hideIfOffScreen) || IsVisible(uIWidget));
				}
				if (uIWidget.UpdateGeometry(frameCount))
				{
					flag = true;
					if (!mRebuild)
					{
						if (uIWidget.drawCall != null)
						{
							uIWidget.drawCall.isDirty = true;
						}
						else
						{
							FindDrawCall(uIWidget);
						}
					}
				}
			}
		}
		if (flag && onGeometryUpdated != null)
		{
			onGeometryUpdated();
		}
		mResized = false;
	}

	public UIDrawCall FindDrawCall(UIWidget w)
	{
		Material material = w.material;
		Texture mainTexture = w.mainTexture;
		int depth = w.depth;
		for (int i = 0; i < drawCalls.Count; i++)
		{
			UIDrawCall uIDrawCall = drawCalls[i];
			int num = (i != 0) ? (drawCalls[i - 1].depthEnd + 1) : (-2147483648);
			int num2 = (i + 1 != drawCalls.Count) ? (drawCalls[i + 1].depthStart - 1) : 2147483647;
			if (num <= depth && num2 >= depth)
			{
				if (uIDrawCall.baseMaterial == material && uIDrawCall.mainTexture == mainTexture)
				{
					if (w.isVisible)
					{
						w.drawCall = uIDrawCall;
						if (w.hasVertices)
						{
							uIDrawCall.isDirty = true;
						}
						return uIDrawCall;
					}
				}
				else
				{
					mRebuild = true;
				}
				return null;
			}
		}
		mRebuild = true;
		return null;
	}

	public void AddWidget(UIWidget w)
	{
		mUpdateScroll = true;
		if (widgets.Count == 0)
		{
			widgets.Add(w);
		}
		else if (mSortWidgets)
		{
			widgets.Add(w);
			SortWidgets();
		}
		else if (UIWidget.PanelCompareFunc(w, widgets[0]) == -1)
		{
			widgets.Insert(0, w);
		}
		else
		{
			int num = widgets.Count;
			while (num > 0)
			{
				if (UIWidget.PanelCompareFunc(w, widgets[--num]) != -1)
				{
					widgets.Insert(num + 1, w);
					break;
				}
			}
		}
		FindDrawCall(w);
	}

	public void RemoveWidget(UIWidget w)
	{
		if (widgets.Remove(w) && w.drawCall != null)
		{
			int depth = w.depth;
			if (depth == w.drawCall.depthStart || depth == w.drawCall.depthEnd)
			{
				mRebuild = true;
			}
			w.drawCall.isDirty = true;
			w.drawCall = null;
		}
	}

	public void Refresh()
	{
		mRebuild = true;
		mUpdateFrame = -1;
		if (list.Count > 0)
		{
			list[0].LateUpdate();
		}
	}

	public void ForceUpDate()
	{
		mUpdateFrame = -1;
	}

	public virtual Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		Vector4 finalClipRegion = this.finalClipRegion;
		float num = finalClipRegion.z * 0.5f;
		float num2 = finalClipRegion.w * 0.5f;
		Vector2 minRect = default(Vector2);
		minRect._002Ector(min.x, min.y);
		Vector2 maxRect = default(Vector2);
		maxRect._002Ector(max.x, max.y);
		Vector2 minArea = default(Vector2);
		minArea._002Ector(finalClipRegion.x - num, finalClipRegion.y - num2);
		Vector2 maxArea = default(Vector2);
		maxArea._002Ector(finalClipRegion.x + num, finalClipRegion.y + num2);
		if (softBorderPadding && clipping == UIDrawCall.Clipping.SoftClip)
		{
			minArea.x += mClipSoftness.x;
			minArea.y += mClipSoftness.y;
			maxArea.x -= mClipSoftness.x;
			maxArea.y -= mClipSoftness.y;
		}
		return Vector2.op_Implicit(NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea));
	}

	public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		Vector3 val = targetBounds.get_min();
		Vector3 val2 = targetBounds.get_max();
		float num = 1f;
		if (mClipping == UIDrawCall.Clipping.None)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				num = root.pixelSizeAdjustment;
			}
		}
		if (num != 1f)
		{
			val /= num;
			val2 /= num;
		}
		Vector3 val3 = CalculateConstrainOffset(Vector2.op_Implicit(val), Vector2.op_Implicit(val2)) * num;
		if (val3.get_sqrMagnitude() > 0f)
		{
			if (immediate)
			{
				target.set_localPosition(target.get_localPosition() + val3);
				targetBounds.set_center(targetBounds.get_center() + val3);
				SpringPosition component = target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.set_enabled(false);
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(target.get_gameObject(), target.get_localPosition() + val3, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		Bounds targetBounds = NGUIMath.CalculateRelativeWidgetBounds(base.cachedTransform, target);
		return ConstrainTargetToBounds(target, ref targetBounds, immediate);
	}

	public static UIPanel Find(Transform trans)
	{
		return Find(trans, false, -1);
	}

	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		return Find(trans, createIfMissing, -1);
	}

	public static UIPanel Find(Transform trans, bool createIfMissing, int layer)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(trans);
		if (uIPanel != null)
		{
			return uIPanel;
		}
		while (trans.get_parent() != null)
		{
			trans = trans.get_parent();
		}
		return (!createIfMissing) ? null : NGUITools.CreateUI(trans, false, layer);
	}

	public Vector2 GetWindowSize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		UIRoot root = base.root;
		Vector2 val = NGUITools.screenSize;
		if (root != null)
		{
			val *= root.GetPixelSizeAdjustment(Mathf.RoundToInt(val.y));
		}
		return val;
	}

	public Vector2 GetViewSize()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (mClipping != 0)
		{
			return new Vector2(mClipRange.z, mClipRange.w);
		}
		return NGUITools.screenSize;
	}
}
