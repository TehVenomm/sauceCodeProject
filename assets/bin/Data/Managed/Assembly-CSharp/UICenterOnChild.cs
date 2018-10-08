using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Center Scroll View on Child")]
public class UICenterOnChild
{
	public delegate void OnCenterCallback(GameObject centeredObject);

	public float springStrength = 8f;

	public float nextPageThreshold;

	public SpringPanel.OnFinished onFinished;

	public OnCenterCallback onCenter;

	private UIScrollView mScrollView;

	private GameObject mCenteredObject;

	public GameObject centeredObject => mCenteredObject;

	public UICenterOnChild()
		: this()
	{
	}

	private void Start()
	{
		Recenter();
	}

	private void OnEnable()
	{
		if (Object.op_Implicit(mScrollView))
		{
			mScrollView.centerOnChild = this;
			Recenter();
		}
	}

	private void OnDisable()
	{
		if (Object.op_Implicit(mScrollView))
		{
			mScrollView.centerOnChild = null;
		}
	}

	private void OnDragFinished()
	{
		if (this.get_enabled())
		{
			Recenter();
		}
	}

	private void OnValidate()
	{
		nextPageThreshold = Mathf.Abs(nextPageThreshold);
	}

	[ContextMenu("Execute")]
	public void Recenter()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Expected O, but got Unknown
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Expected O, but got Unknown
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Expected O, but got Unknown
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Expected O, but got Unknown
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		if (mScrollView == null)
		{
			mScrollView = NGUITools.FindInParents<UIScrollView>(this.get_gameObject());
			if (mScrollView == null)
			{
				Debug.LogWarning((object)(GetType() + " requires " + typeof(UIScrollView) + " on a parent object in order to work"), this);
				this.set_enabled(false);
				return;
			}
			if (Object.op_Implicit(mScrollView))
			{
				mScrollView.centerOnChild = this;
				UIScrollView uIScrollView = mScrollView;
				uIScrollView.onDragFinished = (UIScrollView.OnDragNotification)Delegate.Combine(uIScrollView.onDragFinished, new UIScrollView.OnDragNotification(OnDragFinished));
			}
			if (mScrollView.horizontalScrollBar != null)
			{
				UIProgressBar horizontalScrollBar = mScrollView.horizontalScrollBar;
				horizontalScrollBar.onDragFinished = (UIProgressBar.OnDragFinished)Delegate.Combine(horizontalScrollBar.onDragFinished, new UIProgressBar.OnDragFinished(OnDragFinished));
			}
			if (mScrollView.verticalScrollBar != null)
			{
				UIProgressBar verticalScrollBar = mScrollView.verticalScrollBar;
				verticalScrollBar.onDragFinished = (UIProgressBar.OnDragFinished)Delegate.Combine(verticalScrollBar.onDragFinished, new UIProgressBar.OnDragFinished(OnDragFinished));
			}
		}
		if (!(mScrollView.panel == null))
		{
			Transform val = this.get_transform();
			if (val.get_childCount() != 0)
			{
				Vector3[] worldCorners = mScrollView.panel.worldCorners;
				Vector3 val2 = (worldCorners[2] + worldCorners[0]) * 0.5f;
				Vector3 velocity = mScrollView.currentMomentum * mScrollView.momentumAmount;
				Vector3 val3 = NGUIMath.SpringDampen(ref velocity, 9f, 2f);
				Vector3 val4 = val2 - val3 * 0.01f;
				float num = 3.40282347E+38f;
				Transform target = null;
				int num2 = 0;
				int num3 = 0;
				UIGrid component = this.GetComponent<UIGrid>();
				List<Transform> list = null;
				if (component != null)
				{
					list = component.GetChildList();
					int i = 0;
					int count = list.Count;
					int num4 = 0;
					for (; i < count; i++)
					{
						Transform val5 = list[i];
						if (val5.get_gameObject().get_activeInHierarchy())
						{
							float num5 = Vector3.SqrMagnitude(val5.get_position() - val4);
							if (num5 < num)
							{
								num = num5;
								target = val5;
								num2 = i;
								num3 = num4;
							}
							num4++;
						}
					}
				}
				else
				{
					int j = 0;
					int childCount = val.get_childCount();
					int num6 = 0;
					for (; j < childCount; j++)
					{
						Transform val6 = val.GetChild(j);
						if (val6.get_gameObject().get_activeInHierarchy())
						{
							float num7 = Vector3.SqrMagnitude(val6.get_position() - val4);
							if (num7 < num)
							{
								num = num7;
								target = val6;
								num2 = j;
								num3 = num6;
							}
							num6++;
						}
					}
				}
				if (nextPageThreshold > 0f && UICamera.currentTouch != null && mCenteredObject != null && mCenteredObject.get_transform() == ((list == null) ? ((object)val.GetChild(num2)) : ((object)list[num2])))
				{
					Vector3 val7 = Vector2.op_Implicit(UICamera.currentTouch.totalDelta);
					val7 = this.get_transform().get_rotation() * val7;
					float num8 = 0f;
					switch (mScrollView.movement)
					{
					case UIScrollView.Movement.Horizontal:
						num8 = val7.x;
						break;
					case UIScrollView.Movement.Vertical:
						num8 = val7.y;
						break;
					default:
						num8 = val7.get_magnitude();
						break;
					}
					if (Mathf.Abs(num8) > nextPageThreshold)
					{
						if (num8 > nextPageThreshold)
						{
							target = ((list != null) ? ((object)((num3 <= 0) ? ((!(this.GetComponent<UIWrapContent>() == null)) ? list[list.Count - 1] : list[0]) : list[num3 - 1])) : ((object)((num3 <= 0) ? ((!(this.GetComponent<UIWrapContent>() == null)) ? val.GetChild(val.get_childCount() - 1) : val.GetChild(0)) : val.GetChild(num3 - 1))));
						}
						else if (num8 < 0f - nextPageThreshold)
						{
							target = ((list != null) ? ((object)((num3 >= list.Count - 1) ? ((!(this.GetComponent<UIWrapContent>() == null)) ? list[0] : list[list.Count - 1]) : list[num3 + 1])) : ((object)((num3 >= val.get_childCount() - 1) ? ((!(this.GetComponent<UIWrapContent>() == null)) ? val.GetChild(0) : val.GetChild(val.get_childCount() - 1)) : val.GetChild(num3 + 1))));
						}
					}
				}
				CenterOn(target, val2);
			}
		}
	}

	private void CenterOn(Transform target, Vector3 panelCenter)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		if (target != null && mScrollView != null && mScrollView.panel != null)
		{
			Transform cachedTransform = mScrollView.panel.cachedTransform;
			mCenteredObject = target.get_gameObject();
			Vector3 val = cachedTransform.InverseTransformPoint(target.get_position());
			Vector3 val2 = cachedTransform.InverseTransformPoint(panelCenter);
			Vector3 val3 = val - val2;
			if (!mScrollView.canMoveHorizontally)
			{
				val3.x = 0f;
			}
			if (!mScrollView.canMoveVertically)
			{
				val3.y = 0f;
			}
			val3.z = 0f;
			SpringPanel.Begin(mScrollView.panel.cachedGameObject, cachedTransform.get_localPosition() - val3, springStrength).onFinished = onFinished;
		}
		else
		{
			mCenteredObject = null;
		}
		if (onCenter != null)
		{
			onCenter(mCenteredObject);
		}
	}

	public void CenterOn(Transform target)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (mScrollView != null && mScrollView.panel != null)
		{
			Vector3[] worldCorners = mScrollView.panel.worldCorners;
			Vector3 panelCenter = (worldCorners[2] + worldCorners[0]) * 0.5f;
			CenterOn(target, panelCenter);
		}
	}
}
