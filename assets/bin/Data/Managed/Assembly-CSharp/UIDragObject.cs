using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag Object")]
[ExecuteInEditMode]
public class UIDragObject
{
	public enum DragEffect
	{
		None,
		Momentum,
		MomentumAndSpring
	}

	public Transform target;

	public UIPanel panelRegion;

	public Vector3 scrollMomentum = Vector3.get_zero();

	public bool restrictWithinPanel;

	public UIRect contentRect;

	public DragEffect dragEffect = DragEffect.MomentumAndSpring;

	public float momentumAmount = 35f;

	[SerializeField]
	protected Vector3 scale = new Vector3(1f, 1f, 0f);

	[SerializeField]
	[HideInInspector]
	private float scrollWheelFactor;

	private Plane mPlane;

	private Vector3 mTargetPos;

	private Vector3 mLastPos;

	private Vector3 mMomentum = Vector3.get_zero();

	private Vector3 mScroll = Vector3.get_zero();

	private Bounds mBounds;

	private int mTouchID;

	private bool mStarted;

	private bool mPressed;

	public Vector3 dragMovement
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return scale;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			scale = value;
		}
	}

	public UIDragObject()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)
	//IL_002d: Unknown result type (might be due to invalid IL or missing references)
	//IL_0032: Unknown result type (might be due to invalid IL or missing references)
	//IL_0038: Unknown result type (might be due to invalid IL or missing references)
	//IL_003d: Unknown result type (might be due to invalid IL or missing references)
	//IL_0043: Unknown result type (might be due to invalid IL or missing references)
	//IL_0048: Unknown result type (might be due to invalid IL or missing references)


	private void OnEnable()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		if (scrollWheelFactor != 0f)
		{
			scrollMomentum = scale * scrollWheelFactor;
			scrollWheelFactor = 0f;
		}
		if (contentRect == null && target != null && Application.get_isPlaying())
		{
			UIWidget component = target.GetComponent<UIWidget>();
			if (component != null)
			{
				contentRect = component;
			}
		}
		mTargetPos = ((!(target != null)) ? Vector3.get_zero() : target.get_position());
	}

	private void OnDisable()
	{
		mStarted = false;
	}

	private void FindPanel()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		panelRegion = ((!(target != null)) ? null : UIPanel.Find(target.get_transform().get_parent()));
		if (panelRegion == null)
		{
			restrictWithinPanel = false;
		}
	}

	private void UpdateBounds()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit(contentRect))
		{
			Transform cachedTransform = panelRegion.cachedTransform;
			Matrix4x4 worldToLocalMatrix = cachedTransform.get_worldToLocalMatrix();
			Vector3[] worldCorners = contentRect.worldCorners;
			for (int i = 0; i < 4; i++)
			{
				worldCorners[i] = worldToLocalMatrix.MultiplyPoint3x4(worldCorners[i]);
			}
			mBounds = new Bounds(worldCorners[0], Vector3.get_zero());
			for (int j = 1; j < 4; j++)
			{
				mBounds.Encapsulate(worldCorners[j]);
			}
		}
		else
		{
			mBounds = NGUIMath.CalculateRelativeWidgetBounds(panelRegion.cachedTransform, target);
		}
	}

	private void OnPress(bool pressed)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3)
		{
			float timeScale = Time.get_timeScale();
			if ((!(timeScale < 0.01f) || timeScale == 0f) && this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && target != null)
			{
				if (pressed)
				{
					if (!mPressed)
					{
						mTouchID = UICamera.currentTouchID;
						mPressed = true;
						mStarted = false;
						CancelMovement();
						if (restrictWithinPanel && panelRegion == null)
						{
							FindPanel();
						}
						if (restrictWithinPanel)
						{
							UpdateBounds();
						}
						CancelSpring();
						Transform val = UICamera.currentCamera.get_transform();
						mPlane = new Plane(((!(panelRegion != null)) ? val.get_rotation() : panelRegion.cachedTransform.get_rotation()) * Vector3.get_back(), UICamera.lastWorldPosition);
					}
				}
				else if (mPressed && mTouchID == UICamera.currentTouchID)
				{
					mPressed = false;
					if (restrictWithinPanel && dragEffect == DragEffect.MomentumAndSpring && panelRegion.ConstrainTargetToBounds(target, ref mBounds, false))
					{
						CancelMovement();
					}
				}
			}
		}
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		if (mPressed && mTouchID == UICamera.currentTouchID && this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && target != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			Ray val = UICamera.currentCamera.ScreenPointToRay(Vector2.op_Implicit(UICamera.currentTouch.pos));
			float num = 0f;
			if (mPlane.Raycast(val, ref num))
			{
				Vector3 point = val.GetPoint(num);
				Vector3 val2 = point - mLastPos;
				mLastPos = point;
				if (!mStarted)
				{
					mStarted = true;
					val2 = Vector3.get_zero();
				}
				if (val2.x != 0f || val2.y != 0f)
				{
					val2 = target.InverseTransformDirection(val2);
					val2.Scale(scale);
					val2 = target.TransformDirection(val2);
				}
				if (dragEffect != 0)
				{
					mMomentum = Vector3.Lerp(mMomentum, mMomentum + val2 * (0.01f * momentumAmount), 0.67f);
				}
				Vector3 localPosition = target.get_localPosition();
				Move(val2);
				if (restrictWithinPanel)
				{
					mBounds.set_center(mBounds.get_center() + (target.get_localPosition() - localPosition));
					if (dragEffect != DragEffect.MomentumAndSpring && panelRegion.ConstrainTargetToBounds(target, ref mBounds, true))
					{
						CancelMovement();
					}
				}
			}
		}
	}

	private void Move(Vector3 worldDelta)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		if (panelRegion != null)
		{
			mTargetPos += worldDelta;
			target.set_position(mTargetPos);
			Vector3 localPosition = target.get_localPosition();
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			target.set_localPosition(localPosition);
			UIScrollView component = panelRegion.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		else
		{
			Transform obj = target;
			obj.set_position(obj.get_position() + worldDelta);
		}
	}

	private void LateUpdate()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		if (!(target == null))
		{
			float deltaTime = RealTime.deltaTime;
			mMomentum -= mScroll;
			mScroll = NGUIMath.SpringLerp(mScroll, Vector3.get_zero(), 20f, deltaTime);
			if (!(mMomentum.get_magnitude() < 0.0001f))
			{
				if (!mPressed)
				{
					if (panelRegion == null)
					{
						FindPanel();
					}
					Move(NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime));
					if (restrictWithinPanel && panelRegion != null)
					{
						UpdateBounds();
						if (panelRegion.ConstrainTargetToBounds(target, ref mBounds, dragEffect == DragEffect.None))
						{
							CancelMovement();
						}
						else
						{
							CancelSpring();
						}
					}
					NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
					if (mMomentum.get_magnitude() < 0.0001f)
					{
						CancelMovement();
					}
				}
				else
				{
					NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
				}
			}
		}
	}

	public void CancelMovement()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (target != null)
		{
			Vector3 localPosition = target.get_localPosition();
			localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
			localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			target.set_localPosition(localPosition);
		}
		mTargetPos = ((!(target != null)) ? Vector3.get_zero() : target.get_position());
		mMomentum = Vector3.get_zero();
		mScroll = Vector3.get_zero();
	}

	public void CancelSpring()
	{
		SpringPosition component = target.GetComponent<SpringPosition>();
		if (component != null)
		{
			component.set_enabled(false);
		}
	}

	private void OnScroll(float delta)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()))
		{
			mScroll -= scrollMomentum * (delta * 0.05f);
		}
	}
}
