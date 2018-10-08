using System;
using UnityEngine;

public abstract class UIRect
{
	[Serializable]
	public class AnchorPoint
	{
		public Transform target;

		public float relative;

		public int absolute;

		[NonSerialized]
		public UIRect rect;

		[NonSerialized]
		public Camera targetCam;

		public AnchorPoint()
		{
		}

		public AnchorPoint(float relative)
		{
			this.relative = relative;
		}

		public void Set(float relative, float absolute)
		{
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		public void Set(Transform target, float relative, float absolute)
		{
			this.target = target;
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		public void SetToNearest(float abs0, float abs1, float abs2)
		{
			SetToNearest(0f, 0.5f, 1f, abs0, abs1, abs2);
		}

		public void SetToNearest(float rel0, float rel1, float rel2, float abs0, float abs1, float abs2)
		{
			float num = Mathf.Abs(abs0);
			float num2 = Mathf.Abs(abs1);
			float num3 = Mathf.Abs(abs2);
			if (num < num2 && num < num3)
			{
				Set(rel0, abs0);
			}
			else if (num2 < num && num2 < num3)
			{
				Set(rel1, abs1);
			}
			else
			{
				Set(rel2, abs2);
			}
		}

		public void SetHorizontal(Transform parent, float localPos)
		{
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			if (Object.op_Implicit(rect))
			{
				Vector3[] sides = rect.GetSides(parent);
				float num = Mathf.Lerp(sides[0].x, sides[2].x, relative);
				absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 val = target.get_position();
				if (parent != null)
				{
					val = parent.InverseTransformPoint(val);
				}
				absolute = Mathf.FloorToInt(localPos - val.x + 0.5f);
			}
		}

		public void SetVertical(Transform parent, float localPos)
		{
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			if (Object.op_Implicit(rect))
			{
				Vector3[] sides = rect.GetSides(parent);
				float num = Mathf.Lerp(sides[3].y, sides[1].y, relative);
				absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 val = target.get_position();
				if (parent != null)
				{
					val = parent.InverseTransformPoint(val);
				}
				absolute = Mathf.FloorToInt(localPos - val.y + 0.5f);
			}
		}

		public Vector3[] GetSides(Transform relativeTo)
		{
			if (target != null)
			{
				if (rect != null)
				{
					return rect.GetSides(relativeTo);
				}
				if (target.GetComponent<Camera>() != null)
				{
					return target.GetComponent<Camera>().GetSides(relativeTo);
				}
			}
			return null;
		}
	}

	public enum AnchorUpdate
	{
		OnEnable,
		OnStart,
		OnStart2,
		OnUpdate
	}

	public AnchorPoint leftAnchor = new AnchorPoint();

	public AnchorPoint rightAnchor = new AnchorPoint(1f);

	public AnchorPoint bottomAnchor = new AnchorPoint();

	public AnchorPoint topAnchor = new AnchorPoint(1f);

	public AnchorUpdate updateAnchors = AnchorUpdate.OnStart;

	protected GameObject mGo;

	protected Transform mTrans;

	protected BetterList<UIRect> mChildren = new BetterList<UIRect>();

	protected bool mChanged = true;

	protected bool mStarted;

	protected bool mParentFound;

	[NonSerialized]
	private bool mUpdateAnchors = true;

	[NonSerialized]
	private int mUpdateFrame = -1;

	[NonSerialized]
	private bool mAnchorsCached;

	[NonSerialized]
	private UIRoot mRoot;

	[NonSerialized]
	private UIRect mParent;

	[NonSerialized]
	private bool mRootSet;

	[NonSerialized]
	protected Camera mCam;

	[NonSerialized]
	public float finalAlpha = 1f;

	protected static Vector3[] mSides = (Vector3[])new Vector3[4];

	public GameObject cachedGameObject
	{
		get
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			if (mGo == null)
			{
				mGo = this.get_gameObject();
			}
			return mGo;
		}
	}

	public Transform cachedTransform
	{
		get
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			if (mTrans == null)
			{
				mTrans = this.get_transform();
			}
			return mTrans;
		}
	}

	public Camera anchorCamera
	{
		get
		{
			if (!mAnchorsCached)
			{
				ResetAnchors();
			}
			return mCam;
		}
	}

	public bool isFullyAnchored => Object.op_Implicit(leftAnchor.target) && Object.op_Implicit(rightAnchor.target) && Object.op_Implicit(topAnchor.target) && Object.op_Implicit(bottomAnchor.target);

	public virtual bool isAnchoredHorizontally => Object.op_Implicit(leftAnchor.target) || Object.op_Implicit(rightAnchor.target);

	public virtual bool isAnchoredVertically => Object.op_Implicit(bottomAnchor.target) || Object.op_Implicit(topAnchor.target);

	public virtual bool canBeAnchored => true;

	public UIRect parent
	{
		get
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			if (!mParentFound)
			{
				mParentFound = true;
				mParent = NGUITools.FindInParents<UIRect>(cachedTransform.get_parent());
			}
			return mParent;
		}
	}

	public UIRoot root
	{
		get
		{
			if (parent != null)
			{
				return mParent.root;
			}
			if (!mRootSet)
			{
				mRootSet = true;
				mRoot = NGUITools.FindInParents<UIRoot>(cachedTransform);
			}
			return mRoot;
		}
	}

	public bool isAnchored => (Object.op_Implicit(leftAnchor.target) || Object.op_Implicit(rightAnchor.target) || Object.op_Implicit(topAnchor.target) || Object.op_Implicit(bottomAnchor.target)) && canBeAnchored;

	public abstract float alpha
	{
		get;
		set;
	}

	public abstract Vector3[] localCorners
	{
		get;
	}

	public abstract Vector3[] worldCorners
	{
		get;
	}

	protected float cameraRayDistance
	{
		get
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			if (anchorCamera == null)
			{
				return 0f;
			}
			if (!mCam.get_orthographic())
			{
				Transform cachedTransform = this.cachedTransform;
				Transform val = mCam.get_transform();
				Plane val2 = default(Plane);
				val2._002Ector(cachedTransform.get_rotation() * Vector3.get_back(), cachedTransform.get_position());
				Ray val3 = default(Ray);
				val3._002Ector(val.get_position(), val.get_rotation() * Vector3.get_forward());
				float result = default(float);
				if (val2.Raycast(val3, ref result))
				{
					return result;
				}
			}
			return Mathf.Lerp(mCam.get_nearClipPlane(), mCam.get_farClipPlane(), 0.5f);
		}
	}

	protected UIRect()
		: this()
	{
	}

	public abstract float CalculateFinalAlpha(int frameID);

	public virtual void Invalidate(bool includeChildren)
	{
		mChanged = true;
		if (includeChildren)
		{
			for (int i = 0; i < mChildren.size; i++)
			{
				mChildren.buffer[i].Invalidate(true);
			}
		}
	}

	public virtual Vector3[] GetSides(Transform relativeTo)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (anchorCamera != null)
		{
			return mCam.GetSides(cameraRayDistance, relativeTo);
		}
		Vector3 position = cachedTransform.get_position();
		for (int i = 0; i < 4; i++)
		{
			mSides[i] = position;
		}
		if (relativeTo != null)
		{
			for (int j = 0; j < 4; j++)
			{
				mSides[j] = relativeTo.InverseTransformPoint(mSides[j]);
			}
		}
		return mSides;
	}

	protected Vector3 GetLocalPos(AnchorPoint ac, Transform trans)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		if (anchorCamera == null || ac.targetCam == null)
		{
			return cachedTransform.get_localPosition();
		}
		Rect rect = ac.targetCam.get_rect();
		Vector3 val = ac.targetCam.WorldToViewportPoint(ac.target.get_position());
		Vector3 val2 = default(Vector3);
		val2._002Ector(val.x * rect.get_width() + rect.get_x(), val.y * rect.get_height() + rect.get_y(), val.z);
		val2 = mCam.ViewportToWorldPoint(val2);
		if (trans != null)
		{
			val2 = trans.InverseTransformPoint(val2);
		}
		val2.x = Mathf.Floor(val2.x + 0.5f);
		val2.y = Mathf.Floor(val2.y + 0.5f);
		return val2;
	}

	protected virtual void OnEnable()
	{
		UpdaterBase.Add(_Update);
		mUpdateFrame = -1;
		if (updateAnchors == AnchorUpdate.OnEnable)
		{
			mAnchorsCached = false;
			mUpdateAnchors = true;
		}
		if (mStarted)
		{
			OnInit();
		}
		mUpdateFrame = -1;
	}

	protected virtual void OnInit()
	{
		mChanged = true;
		mRootSet = false;
		mParentFound = false;
		if (parent != null)
		{
			mParent.mChildren.Add(this);
		}
	}

	protected virtual void OnDisable()
	{
		if (Object.op_Implicit(mParent))
		{
			mParent.mChildren.Remove(this);
		}
		mParent = null;
		mRoot = null;
		mRootSet = false;
		mParentFound = false;
		UpdaterBase.Remove(_Update);
	}

	protected void Start()
	{
		mStarted = true;
		OnInit();
		OnStart();
	}

	public void _Update()
	{
		if (!mAnchorsCached)
		{
			ResetAnchors();
		}
		int frameCount = Time.get_frameCount();
		if (mUpdateFrame != frameCount)
		{
			if (updateAnchors == AnchorUpdate.OnUpdate || mUpdateAnchors)
			{
				UpdateAnchorsInternal(frameCount);
			}
			OnUpdate();
		}
	}

	protected void UpdateAnchorsInternal(int frame)
	{
		mUpdateFrame = frame;
		mUpdateAnchors = false;
		bool flag = false;
		if (Object.op_Implicit(leftAnchor.target))
		{
			flag = true;
			if (leftAnchor.rect != null && leftAnchor.rect.mUpdateFrame != frame)
			{
				leftAnchor.rect._Update();
			}
		}
		if (Object.op_Implicit(bottomAnchor.target))
		{
			flag = true;
			if (bottomAnchor.rect != null && bottomAnchor.rect.mUpdateFrame != frame)
			{
				bottomAnchor.rect._Update();
			}
		}
		if (Object.op_Implicit(rightAnchor.target))
		{
			flag = true;
			if (rightAnchor.rect != null && rightAnchor.rect.mUpdateFrame != frame)
			{
				rightAnchor.rect._Update();
			}
		}
		if (Object.op_Implicit(topAnchor.target))
		{
			flag = true;
			if (topAnchor.rect != null && topAnchor.rect.mUpdateFrame != frame)
			{
				topAnchor.rect._Update();
			}
		}
		if (flag)
		{
			OnAnchor();
		}
	}

	public void UpdateAnchors()
	{
		if (isAnchored)
		{
			mUpdateFrame = -1;
			mUpdateAnchors = true;
			if (!(mGo != null) || mGo.get_activeInHierarchy())
			{
				UpdateAnchorsInternal(Time.get_frameCount());
			}
		}
	}

	protected abstract void OnAnchor();

	public void SetAnchor(Transform t)
	{
		leftAnchor.target = t;
		rightAnchor.target = t;
		topAnchor.target = t;
		bottomAnchor.target = t;
		ResetAnchors();
		UpdateAnchors();
	}

	public void SetAnchor(GameObject go)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		Transform target = (!(go != null)) ? null : go.get_transform();
		leftAnchor.target = target;
		rightAnchor.target = target;
		topAnchor.target = target;
		bottomAnchor.target = target;
		ResetAnchors();
		UpdateAnchors();
	}

	public void SetAnchor(GameObject go, int left, int bottom, int right, int top)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		Transform target = (!(go != null)) ? null : go.get_transform();
		leftAnchor.target = target;
		rightAnchor.target = target;
		topAnchor.target = target;
		bottomAnchor.target = target;
		leftAnchor.relative = 0f;
		rightAnchor.relative = 1f;
		bottomAnchor.relative = 0f;
		topAnchor.relative = 1f;
		leftAnchor.absolute = left;
		rightAnchor.absolute = right;
		bottomAnchor.absolute = bottom;
		topAnchor.absolute = top;
		ResetAnchors();
		UpdateAnchors();
	}

	public void ResetAnchors()
	{
		mAnchorsCached = true;
		leftAnchor.rect = ((!Object.op_Implicit(leftAnchor.target)) ? null : leftAnchor.target.GetComponent<UIRect>());
		bottomAnchor.rect = ((!Object.op_Implicit(bottomAnchor.target)) ? null : bottomAnchor.target.GetComponent<UIRect>());
		rightAnchor.rect = ((!Object.op_Implicit(rightAnchor.target)) ? null : rightAnchor.target.GetComponent<UIRect>());
		topAnchor.rect = ((!Object.op_Implicit(topAnchor.target)) ? null : topAnchor.target.GetComponent<UIRect>());
		mCam = NGUITools.FindCameraForLayer(cachedGameObject.get_layer());
		FindCameraFor(leftAnchor);
		FindCameraFor(bottomAnchor);
		FindCameraFor(rightAnchor);
		FindCameraFor(topAnchor);
		mUpdateAnchors = true;
	}

	public void ResetAndUpdateAnchors()
	{
		ResetAnchors();
		UpdateAnchors();
	}

	public abstract void SetRect(float x, float y, float width, float height);

	private void FindCameraFor(AnchorPoint ap)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (ap.target == null || ap.rect != null)
		{
			ap.targetCam = null;
		}
		else
		{
			ap.targetCam = NGUITools.FindCameraForLayer(ap.target.get_gameObject().get_layer());
		}
	}

	public virtual void ParentHasChanged()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		mParentFound = false;
		UIRect uIRect = NGUITools.FindInParents<UIRect>(cachedTransform.get_parent());
		if (mParent != uIRect)
		{
			if (Object.op_Implicit(mParent))
			{
				mParent.mChildren.Remove(this);
			}
			mParent = uIRect;
			if (Object.op_Implicit(mParent))
			{
				mParent.mChildren.Add(this);
			}
			mRootSet = false;
		}
	}

	protected abstract void OnStart();

	protected virtual void OnUpdate()
	{
	}
}
