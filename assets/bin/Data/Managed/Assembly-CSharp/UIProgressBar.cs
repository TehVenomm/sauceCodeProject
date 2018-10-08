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

	[SerializeField]
	[HideInInspector]
	protected UIWidget mBG;

	[HideInInspector]
	[SerializeField]
	protected UIWidget mFG;

	[HideInInspector]
	[SerializeField]
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
			if ((Object)mTrans == (Object)null)
			{
				mTrans = base.transform;
			}
			return mTrans;
		}
	}

	public Camera cachedCamera
	{
		get
		{
			if ((Object)mCam == (Object)null)
			{
				mCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
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
			if ((Object)mFG != (Object)value)
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
			if ((Object)mBG != (Object)value)
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
			if ((Object)mFG != (Object)null)
			{
				return mFG.alpha;
			}
			if ((Object)mBG != (Object)null)
			{
				return mBG.alpha;
			}
			return 1f;
		}
		set
		{
			if ((Object)mFG != (Object)null)
			{
				mFG.alpha = value;
				if ((Object)mFG.GetComponent<Collider>() != (Object)null)
				{
					mFG.GetComponent<Collider>().enabled = (mFG.alpha > 0.001f);
				}
				else if ((Object)mFG.GetComponent<Collider2D>() != (Object)null)
				{
					mFG.GetComponent<Collider2D>().enabled = (mFG.alpha > 0.001f);
				}
			}
			if ((Object)mBG != (Object)null)
			{
				mBG.alpha = value;
				if ((Object)mBG.GetComponent<Collider>() != (Object)null)
				{
					mBG.GetComponent<Collider>().enabled = (mBG.alpha > 0.001f);
				}
				else if ((Object)mBG.GetComponent<Collider2D>() != (Object)null)
				{
					mBG.GetComponent<Collider2D>().enabled = (mBG.alpha > 0.001f);
				}
			}
			if ((Object)thumb != (Object)null)
			{
				UIWidget component = thumb.GetComponent<UIWidget>();
				if ((Object)component != (Object)null)
				{
					component.alpha = value;
					if ((Object)component.GetComponent<Collider>() != (Object)null)
					{
						component.GetComponent<Collider>().enabled = (component.alpha > 0.001f);
					}
					else if ((Object)component.GetComponent<Collider2D>() != (Object)null)
					{
						component.GetComponent<Collider2D>().enabled = (component.alpha > 0.001f);
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
		if (Application.isPlaying)
		{
			if ((Object)mBG != (Object)null)
			{
				mBG.autoResizeBoxCollider = true;
			}
			OnStart();
			if ((Object)current == (Object)null && onChange != null)
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
		Transform cachedTransform = this.cachedTransform;
		Plane plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
		Ray ray = cachedCamera.ScreenPointToRay(screenPos);
		if (!plane.Raycast(ray, out float enter))
		{
			return value;
		}
		return LocalToValue(cachedTransform.InverseTransformPoint(ray.GetPoint(enter)));
	}

	protected virtual float LocalToValue(Vector2 localPos)
	{
		if ((Object)mFG != (Object)null)
		{
			Vector3[] localCorners = mFG.localCorners;
			Vector3 vector = localCorners[2] - localCorners[0];
			if (isHorizontal)
			{
				float num = (localPos.x - localCorners[0].x) / vector.x;
				return (!isInverted) ? num : (1f - num);
			}
			float num2 = (localPos.y - localCorners[0].y) / vector.y;
			return (!isInverted) ? num2 : (1f - num2);
		}
		return value;
	}

	public virtual void ForceUpdate()
	{
		mIsDirty = false;
		bool flag = false;
		if ((Object)mFG != (Object)null)
		{
			UIBasicSprite uIBasicSprite = mFG as UIBasicSprite;
			if (isHorizontal)
			{
				if ((Object)uIBasicSprite != (Object)null && uIBasicSprite.type == UIBasicSprite.Type.Filled)
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
					mFG.enabled = true;
					flag = (value < 0.001f);
				}
			}
			else if ((Object)uIBasicSprite != (Object)null && uIBasicSprite.type == UIBasicSprite.Type.Filled)
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
				mFG.enabled = true;
				flag = (value < 0.001f);
			}
		}
		if ((Object)thumb != (Object)null && ((Object)mFG != (Object)null || (Object)mBG != (Object)null))
		{
			Vector3[] array = (!((Object)mFG != (Object)null)) ? mBG.localCorners : mFG.localCorners;
			Vector4 vector = (!((Object)mFG != (Object)null)) ? mBG.border : mFG.border;
			array[0].x += vector.x;
			array[1].x += vector.x;
			array[2].x -= vector.z;
			array[3].x -= vector.z;
			array[0].y += vector.y;
			array[1].y -= vector.w;
			array[2].y -= vector.w;
			array[3].y += vector.y;
			Transform transform = (!((Object)mFG != (Object)null)) ? mBG.cachedTransform : mFG.cachedTransform;
			for (int i = 0; i < 4; i++)
			{
				array[i] = transform.TransformPoint(array[i]);
			}
			if (isHorizontal)
			{
				Vector3 a = Vector3.Lerp(array[0], array[1], 0.5f);
				Vector3 b = Vector3.Lerp(array[2], array[3], 0.5f);
				SetThumbPosition(Vector3.Lerp(a, b, (!isInverted) ? value : (1f - value)));
			}
			else
			{
				Vector3 a2 = Vector3.Lerp(array[0], array[3], 0.5f);
				Vector3 b2 = Vector3.Lerp(array[1], array[2], 0.5f);
				SetThumbPosition(Vector3.Lerp(a2, b2, (!isInverted) ? value : (1f - value)));
			}
		}
		if (flag)
		{
			mFG.enabled = false;
		}
	}

	protected void SetThumbPosition(Vector3 worldPos)
	{
		Transform parent = thumb.parent;
		if ((Object)parent != (Object)null)
		{
			worldPos = parent.InverseTransformPoint(worldPos);
			worldPos.x = Mathf.Round(worldPos.x);
			worldPos.y = Mathf.Round(worldPos.y);
			worldPos.z = 0f;
			if (Vector3.Distance(thumb.localPosition, worldPos) > 0.001f)
			{
				thumb.localPosition = worldPos;
			}
		}
		else if (Vector3.Distance(thumb.position, worldPos) > 1E-05f)
		{
			thumb.position = worldPos;
		}
	}

	public virtual void OnPan(Vector2 delta)
	{
		if (base.enabled)
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
