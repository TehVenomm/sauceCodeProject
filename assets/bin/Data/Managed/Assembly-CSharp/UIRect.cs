using System;
using UnityEngine;

public abstract class UIRect : MonoBehaviour
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
			if ((bool)rect)
			{
				Vector3[] sides = rect.GetSides(parent);
				float num = Mathf.Lerp(sides[0].x, sides[2].x, relative);
				absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 position = target.position;
				if ((UnityEngine.Object)parent != (UnityEngine.Object)null)
				{
					position = parent.InverseTransformPoint(position);
				}
				absolute = Mathf.FloorToInt(localPos - position.x + 0.5f);
			}
		}

		public void SetVertical(Transform parent, float localPos)
		{
			if ((bool)rect)
			{
				Vector3[] sides = rect.GetSides(parent);
				float num = Mathf.Lerp(sides[3].y, sides[1].y, relative);
				absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 position = target.position;
				if ((UnityEngine.Object)parent != (UnityEngine.Object)null)
				{
					position = parent.InverseTransformPoint(position);
				}
				absolute = Mathf.FloorToInt(localPos - position.y + 0.5f);
			}
		}

		public Vector3[] GetSides(Transform relativeTo)
		{
			if ((UnityEngine.Object)target != (UnityEngine.Object)null)
			{
				if ((UnityEngine.Object)rect != (UnityEngine.Object)null)
				{
					return rect.GetSides(relativeTo);
				}
				if ((UnityEngine.Object)target.GetComponent<Camera>() != (UnityEngine.Object)null)
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

	protected static Vector3[] mSides = new Vector3[4];

	public GameObject cachedGameObject
	{
		get
		{
			if ((UnityEngine.Object)mGo == (UnityEngine.Object)null)
			{
				mGo = base.gameObject;
			}
			return mGo;
		}
	}

	public Transform cachedTransform
	{
		get
		{
			if ((UnityEngine.Object)mTrans == (UnityEngine.Object)null)
			{
				mTrans = base.transform;
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

	public bool isFullyAnchored => (bool)leftAnchor.target && (bool)rightAnchor.target && (bool)topAnchor.target && (bool)bottomAnchor.target;

	public virtual bool isAnchoredHorizontally => (bool)leftAnchor.target || (bool)rightAnchor.target;

	public virtual bool isAnchoredVertically => (bool)bottomAnchor.target || (bool)topAnchor.target;

	public virtual bool canBeAnchored => true;

	public UIRect parent
	{
		get
		{
			if (!mParentFound)
			{
				mParentFound = true;
				mParent = NGUITools.FindInParents<UIRect>(cachedTransform.parent);
			}
			return mParent;
		}
	}

	public UIRoot root
	{
		get
		{
			if ((UnityEngine.Object)parent != (UnityEngine.Object)null)
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

	public bool isAnchored => ((bool)leftAnchor.target || (bool)rightAnchor.target || (bool)topAnchor.target || (bool)bottomAnchor.target) && canBeAnchored;

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
			if ((UnityEngine.Object)anchorCamera == (UnityEngine.Object)null)
			{
				return 0f;
			}
			if (!mCam.orthographic)
			{
				Transform cachedTransform = this.cachedTransform;
				Transform transform = mCam.transform;
				Plane plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
				Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);
				if (plane.Raycast(ray, out float enter))
				{
					return enter;
				}
			}
			return Mathf.Lerp(mCam.nearClipPlane, mCam.farClipPlane, 0.5f);
		}
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
		if ((UnityEngine.Object)anchorCamera != (UnityEngine.Object)null)
		{
			return mCam.GetSides(cameraRayDistance, relativeTo);
		}
		Vector3 position = cachedTransform.position;
		for (int i = 0; i < 4; i++)
		{
			mSides[i] = position;
		}
		if ((UnityEngine.Object)relativeTo != (UnityEngine.Object)null)
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
		if ((UnityEngine.Object)anchorCamera == (UnityEngine.Object)null || (UnityEngine.Object)ac.targetCam == (UnityEngine.Object)null)
		{
			return cachedTransform.localPosition;
		}
		Rect rect = ac.targetCam.rect;
		Vector3 vector = ac.targetCam.WorldToViewportPoint(ac.target.position);
		Vector3 vector2 = new Vector3(vector.x * rect.width + rect.x, vector.y * rect.height + rect.y, vector.z);
		vector2 = mCam.ViewportToWorldPoint(vector2);
		if ((UnityEngine.Object)trans != (UnityEngine.Object)null)
		{
			vector2 = trans.InverseTransformPoint(vector2);
		}
		vector2.x = Mathf.Floor(vector2.x + 0.5f);
		vector2.y = Mathf.Floor(vector2.y + 0.5f);
		return vector2;
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
		if ((UnityEngine.Object)parent != (UnityEngine.Object)null)
		{
			mParent.mChildren.Add(this);
		}
	}

	protected virtual void OnDisable()
	{
		if ((bool)mParent)
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
		int frameCount = Time.frameCount;
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
		if ((bool)leftAnchor.target)
		{
			flag = true;
			if ((UnityEngine.Object)leftAnchor.rect != (UnityEngine.Object)null && leftAnchor.rect.mUpdateFrame != frame)
			{
				leftAnchor.rect._Update();
			}
		}
		if ((bool)bottomAnchor.target)
		{
			flag = true;
			if ((UnityEngine.Object)bottomAnchor.rect != (UnityEngine.Object)null && bottomAnchor.rect.mUpdateFrame != frame)
			{
				bottomAnchor.rect._Update();
			}
		}
		if ((bool)rightAnchor.target)
		{
			flag = true;
			if ((UnityEngine.Object)rightAnchor.rect != (UnityEngine.Object)null && rightAnchor.rect.mUpdateFrame != frame)
			{
				rightAnchor.rect._Update();
			}
		}
		if ((bool)topAnchor.target)
		{
			flag = true;
			if ((UnityEngine.Object)topAnchor.rect != (UnityEngine.Object)null && topAnchor.rect.mUpdateFrame != frame)
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
			if (!((UnityEngine.Object)mGo != (UnityEngine.Object)null) || mGo.activeInHierarchy)
			{
				UpdateAnchorsInternal(Time.frameCount);
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
		Transform target = (!((UnityEngine.Object)go != (UnityEngine.Object)null)) ? null : go.transform;
		leftAnchor.target = target;
		rightAnchor.target = target;
		topAnchor.target = target;
		bottomAnchor.target = target;
		ResetAnchors();
		UpdateAnchors();
	}

	public void SetAnchor(GameObject go, int left, int bottom, int right, int top)
	{
		Transform target = (!((UnityEngine.Object)go != (UnityEngine.Object)null)) ? null : go.transform;
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
		leftAnchor.rect = ((!(bool)leftAnchor.target) ? null : leftAnchor.target.GetComponent<UIRect>());
		bottomAnchor.rect = ((!(bool)bottomAnchor.target) ? null : bottomAnchor.target.GetComponent<UIRect>());
		rightAnchor.rect = ((!(bool)rightAnchor.target) ? null : rightAnchor.target.GetComponent<UIRect>());
		topAnchor.rect = ((!(bool)topAnchor.target) ? null : topAnchor.target.GetComponent<UIRect>());
		mCam = NGUITools.FindCameraForLayer(cachedGameObject.layer);
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
		if ((UnityEngine.Object)ap.target == (UnityEngine.Object)null || (UnityEngine.Object)ap.rect != (UnityEngine.Object)null)
		{
			ap.targetCam = null;
		}
		else
		{
			ap.targetCam = NGUITools.FindCameraForLayer(ap.target.gameObject.layer);
		}
	}

	public virtual void ParentHasChanged()
	{
		mParentFound = false;
		UIRect y = NGUITools.FindInParents<UIRect>(cachedTransform.parent);
		if ((UnityEngine.Object)mParent != (UnityEngine.Object)y)
		{
			if ((bool)mParent)
			{
				mParent.mChildren.Remove(this);
			}
			mParent = y;
			if ((bool)mParent)
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
