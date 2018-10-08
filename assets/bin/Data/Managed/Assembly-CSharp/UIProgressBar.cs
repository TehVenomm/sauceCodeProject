using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Progress Bar")]
public class UIProgressBar : UIWidgetContainer
{
	public enum FillDirection
	{
		LeftToRight,
		RightToLeft,
		BottomToTop,
		TopToBottom
	}

	public delegate void OnDragFinished();

	public static UIProgressBar current;

	public OnDragFinished onDragFinished;

	public Transform thumb;

	[HideInInspector]
	[SerializeField]
	protected UIWidget mBG;

	[SerializeField]
	[HideInInspector]
	protected UIWidget mFG;

	[SerializeField]
	[HideInInspector]
	protected float mValue = 1f;

	[SerializeField]
	[HideInInspector]
	protected FillDirection mFill;

	protected Transform mTrans;

	protected bool mIsDirty;

	protected Camera mCam;

	protected float mOffset;

	public int numberOfSteps;

	public List<EventDelegate> onChange = new List<EventDelegate>();

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

	public Camera cachedCamera
	{
		get
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (mCam == null)
			{
				mCam = NGUITools.FindCameraForLayer(this.get_gameObject().get_layer());
			}
			return mCam;
		}
	}

	public UIWidget foregroundWidget
	{
		get
		{
			return mFG;
		}
		set
		{
			if (mFG != value)
			{
				mFG = value;
				mIsDirty = true;
			}
		}
	}

	public UIWidget backgroundWidget
	{
		get
		{
			return mBG;
		}
		set
		{
			if (mBG != value)
			{
				mBG = value;
				mIsDirty = true;
			}
		}
	}

	public FillDirection fillDirection
	{
		get
		{
			return mFill;
		}
		set
		{
			if (mFill != value)
			{
				mFill = value;
				ForceUpdate();
			}
		}
	}

	public float value
	{
		get
		{
			if (numberOfSteps > 1)
			{
				return Mathf.Round(mValue * (float)(numberOfSteps - 1)) / (float)(numberOfSteps - 1);
			}
			return mValue;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (mValue != num)
			{
				float value2 = this.value;
				mValue = num;
				if (value2 != this.value)
				{
					ForceUpdate();
					if (NGUITools.GetActive(this) && EventDelegate.IsValid(onChange))
					{
						current = this;
						EventDelegate.Execute(onChange);
						current = null;
					}
				}
			}
		}
	}

	public float alpha
	{
		get
		{
			if (mFG != null)
			{
				return mFG.alpha;
			}
			if (mBG != null)
			{
				return mBG.alpha;
			}
			return 1f;
		}
		set
		{
			if (mFG != null)
			{
				mFG.alpha = value;
				if (mFG.GetComponent<Collider>() != null)
				{
					mFG.GetComponent<Collider>().set_enabled(mFG.alpha > 0.001f);
				}
				else if (mFG.GetComponent<Collider2D>() != null)
				{
					mFG.GetComponent<Collider2D>().set_enabled(mFG.alpha > 0.001f);
				}
			}
			if (mBG != null)
			{
				mBG.alpha = value;
				if (mBG.GetComponent<Collider>() != null)
				{
					mBG.GetComponent<Collider>().set_enabled(mBG.alpha > 0.001f);
				}
				else if (mBG.GetComponent<Collider2D>() != null)
				{
					mBG.GetComponent<Collider2D>().set_enabled(mBG.alpha > 0.001f);
				}
			}
			if (thumb != null)
			{
				UIWidget component = thumb.GetComponent<UIWidget>();
				if (component != null)
				{
					component.alpha = value;
					if (component.GetComponent<Collider>() != null)
					{
						component.GetComponent<Collider>().set_enabled(component.alpha > 0.001f);
					}
					else if (component.GetComponent<Collider2D>() != null)
					{
						component.GetComponent<Collider2D>().set_enabled(component.alpha > 0.001f);
					}
				}
			}
		}
	}

	protected bool isHorizontal => mFill == FillDirection.LeftToRight || mFill == FillDirection.RightToLeft;

	protected bool isInverted => mFill == FillDirection.RightToLeft || mFill == FillDirection.TopToBottom;

	protected void Start()
	{
		Upgrade();
		if (Application.get_isPlaying())
		{
			if (mBG != null)
			{
				mBG.autoResizeBoxCollider = true;
			}
			OnStart();
			if (current == null && onChange != null)
			{
				current = this;
				EventDelegate.Execute(onChange);
				current = null;
			}
		}
		ForceUpdate();
	}

	protected virtual void Upgrade()
	{
	}

	protected virtual void OnStart()
	{
	}

	protected void Update()
	{
		if (mIsDirty)
		{
			ForceUpdate();
		}
	}

	protected void OnValidate()
	{
		if (NGUITools.GetActive(this))
		{
			Upgrade();
			mIsDirty = true;
			float num = Mathf.Clamp01(mValue);
			if (mValue != num)
			{
				mValue = num;
			}
			if (numberOfSteps < 0)
			{
				numberOfSteps = 0;
			}
			else if (numberOfSteps > 20)
			{
				numberOfSteps = 20;
			}
			ForceUpdate();
		}
		else
		{
			float num2 = Mathf.Clamp01(mValue);
			if (mValue != num2)
			{
				mValue = num2;
			}
			if (numberOfSteps < 0)
			{
				numberOfSteps = 0;
			}
			else if (numberOfSteps > 20)
			{
				numberOfSteps = 20;
			}
		}
	}

	protected float ScreenToValue(Vector2 screenPos)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		Transform cachedTransform = this.cachedTransform;
		Plane val = default(Plane);
		val._002Ector(cachedTransform.get_rotation() * Vector3.get_back(), cachedTransform.get_position());
		Ray val2 = cachedCamera.ScreenPointToRay(Vector2.op_Implicit(screenPos));
		float num = default(float);
		if (!val.Raycast(val2, ref num))
		{
			return value;
		}
		return LocalToValue(Vector2.op_Implicit(cachedTransform.InverseTransformPoint(val2.GetPoint(num))));
	}

	protected virtual float LocalToValue(Vector2 localPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (mFG != null)
		{
			Vector3[] localCorners = mFG.localCorners;
			Vector3 val = localCorners[2] - localCorners[0];
			if (isHorizontal)
			{
				float num = (localPos.x - localCorners[0].x) / val.x;
				return (!isInverted) ? num : (1f - num);
			}
			float num2 = (localPos.y - localCorners[0].y) / val.y;
			return (!isInverted) ? num2 : (1f - num2);
		}
		return value;
	}

	public virtual void ForceUpdate()
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		mIsDirty = false;
		bool flag = false;
		if (mFG != null)
		{
			UIBasicSprite uIBasicSprite = mFG as UIBasicSprite;
			if (isHorizontal)
			{
				if (uIBasicSprite != null && uIBasicSprite.type == UIBasicSprite.Type.Filled)
				{
					if (uIBasicSprite.fillDirection == UIBasicSprite.FillDirection.Horizontal || uIBasicSprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
					{
						uIBasicSprite.fillDirection = UIBasicSprite.FillDirection.Horizontal;
						uIBasicSprite.invert = isInverted;
					}
					uIBasicSprite.fillAmount = value;
				}
				else
				{
					mFG.drawRegion = ((!isInverted) ? new Vector4(0f, 0f, value, 1f) : new Vector4(1f - value, 0f, 1f, 1f));
					mFG.set_enabled(true);
					flag = (value < 0.001f);
				}
			}
			else if (uIBasicSprite != null && uIBasicSprite.type == UIBasicSprite.Type.Filled)
			{
				if (uIBasicSprite.fillDirection == UIBasicSprite.FillDirection.Horizontal || uIBasicSprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
				{
					uIBasicSprite.fillDirection = UIBasicSprite.FillDirection.Vertical;
					uIBasicSprite.invert = isInverted;
				}
				uIBasicSprite.fillAmount = value;
			}
			else
			{
				mFG.drawRegion = ((!isInverted) ? new Vector4(0f, 0f, 1f, value) : new Vector4(0f, 1f - value, 1f, 1f));
				mFG.set_enabled(true);
				flag = (value < 0.001f);
			}
		}
		if (thumb != null && (mFG != null || mBG != null))
		{
			Vector3[] array = (!(mFG != null)) ? mBG.localCorners : mFG.localCorners;
			Vector4 val = (!(mFG != null)) ? mBG.border : mFG.border;
			array[0].x += val.x;
			array[1].x += val.x;
			array[2].x -= val.z;
			array[3].x -= val.z;
			array[0].y += val.y;
			array[1].y -= val.w;
			array[2].y -= val.w;
			array[3].y += val.y;
			Transform val2 = (!(mFG != null)) ? mBG.cachedTransform : mFG.cachedTransform;
			for (int i = 0; i < 4; i++)
			{
				array[i] = val2.TransformPoint(array[i]);
			}
			if (isHorizontal)
			{
				Vector3 val3 = Vector3.Lerp(array[0], array[1], 0.5f);
				Vector3 val4 = Vector3.Lerp(array[2], array[3], 0.5f);
				SetThumbPosition(Vector3.Lerp(val3, val4, (!isInverted) ? value : (1f - value)));
			}
			else
			{
				Vector3 val5 = Vector3.Lerp(array[0], array[3], 0.5f);
				Vector3 val6 = Vector3.Lerp(array[1], array[2], 0.5f);
				SetThumbPosition(Vector3.Lerp(val5, val6, (!isInverted) ? value : (1f - value)));
			}
		}
		if (flag)
		{
			mFG.set_enabled(false);
		}
	}

	protected void SetThumbPosition(Vector3 worldPos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Transform val = thumb.get_parent();
		if (val != null)
		{
			worldPos = val.InverseTransformPoint(worldPos);
			worldPos.x = Mathf.Round(worldPos.x);
			worldPos.y = Mathf.Round(worldPos.y);
			worldPos.z = 0f;
			if (Vector3.Distance(thumb.get_localPosition(), worldPos) > 0.001f)
			{
				thumb.set_localPosition(worldPos);
			}
		}
		else if (Vector3.Distance(thumb.get_position(), worldPos) > 1E-05f)
		{
			thumb.set_position(worldPos);
		}
	}

	public virtual void OnPan(Vector2 delta)
	{
		if (this.get_enabled())
		{
			switch (mFill)
			{
			case FillDirection.LeftToRight:
			{
				float num8 = mValue = (value = Mathf.Clamp01(mValue + delta.x));
				break;
			}
			case FillDirection.RightToLeft:
			{
				float num6 = mValue = (value = Mathf.Clamp01(mValue - delta.x));
				break;
			}
			case FillDirection.BottomToTop:
			{
				float num4 = mValue = (value = Mathf.Clamp01(mValue + delta.y));
				break;
			}
			case FillDirection.TopToBottom:
			{
				float num2 = mValue = (value = Mathf.Clamp01(mValue - delta.y));
				break;
			}
			}
		}
	}
}
