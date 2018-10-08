using System;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Anchor")]
[ExecuteInEditMode]
public class UIAnchor
{
	public enum Side
	{
		BottomLeft,
		Left,
		TopLeft,
		Top,
		TopRight,
		Right,
		BottomRight,
		Bottom,
		Center
	}

	public Camera uiCamera;

	public GameObject container;

	public Side side = Side.Center;

	public bool runOnlyOnce = true;

	public Vector2 relativeOffset = Vector2.get_zero();

	public Vector2 pixelOffset = Vector2.get_zero();

	[SerializeField]
	[HideInInspector]
	private UIWidget widgetContainer;

	private Transform mTrans;

	private Animation mAnim;

	private Rect mRect = default(Rect);

	private UIRoot mRoot;

	private bool mStarted;

	public UIAnchor()
		: this()
	{
	}//IL_000f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0014: Unknown result type (might be due to invalid IL or missing references)
	//IL_001a: Unknown result type (might be due to invalid IL or missing references)
	//IL_001f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0027: Unknown result type (might be due to invalid IL or missing references)
	//IL_002d: Unknown result type (might be due to invalid IL or missing references)
	//IL_002e: Unknown result type (might be due to invalid IL or missing references)


	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		mTrans = this.get_transform();
		mAnim = this.GetComponent<Animation>();
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(ScreenSizeChanged));
	}

	private void OnDestroy()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(ScreenSizeChanged));
	}

	private void ScreenSizeChanged()
	{
		if (mStarted && runOnlyOnce)
		{
			Update();
		}
	}

	private void Start()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (container == null && widgetContainer != null)
		{
			container = widgetContainer.get_gameObject();
			widgetContainer = null;
		}
		mRoot = NGUITools.FindInParents<UIRoot>(this.get_gameObject());
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(this.get_gameObject().get_layer());
		}
		Update();
		mStarted = true;
	}

	private void Update()
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected O, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Expected O, but got Unknown
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Expected O, but got Unknown
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		if (!(mAnim != null) || !mAnim.get_enabled() || !mAnim.get_isPlaying())
		{
			bool flag = false;
			UIWidget uIWidget = (!(container == null)) ? container.GetComponent<UIWidget>() : null;
			UIPanel uIPanel = (!(container == null) || !(uIWidget == null)) ? container.GetComponent<UIPanel>() : null;
			if (uIWidget != null)
			{
				Bounds val = uIWidget.CalculateBounds(container.get_transform().get_parent());
				ref Rect reference = ref mRect;
				Vector3 min = val.get_min();
				reference.set_x(min.x);
				ref Rect reference2 = ref mRect;
				Vector3 min2 = val.get_min();
				reference2.set_y(min2.y);
				ref Rect reference3 = ref mRect;
				Vector3 size = val.get_size();
				reference3.set_width(size.x);
				ref Rect reference4 = ref mRect;
				Vector3 size2 = val.get_size();
				reference4.set_height(size2.y);
			}
			else if (uIPanel != null)
			{
				if (uIPanel.clipping == UIDrawCall.Clipping.None)
				{
					float num = (!(mRoot != null)) ? 0.5f : ((float)mRoot.activeHeight / (float)Screen.get_height() * 0.5f);
					mRect.set_xMin((float)(-Screen.get_width()) * num);
					mRect.set_yMin((float)(-Screen.get_height()) * num);
					mRect.set_xMax(0f - mRect.get_xMin());
					mRect.set_yMax(0f - mRect.get_yMin());
				}
				else
				{
					Vector4 finalClipRegion = uIPanel.finalClipRegion;
					mRect.set_x(finalClipRegion.x - finalClipRegion.z * 0.5f);
					mRect.set_y(finalClipRegion.y - finalClipRegion.w * 0.5f);
					mRect.set_width(finalClipRegion.z);
					mRect.set_height(finalClipRegion.w);
				}
			}
			else if (container != null)
			{
				Transform val2 = container.get_transform().get_parent();
				Bounds val3 = (!(val2 != null)) ? NGUIMath.CalculateRelativeWidgetBounds(container.get_transform()) : NGUIMath.CalculateRelativeWidgetBounds(val2, container.get_transform());
				ref Rect reference5 = ref mRect;
				Vector3 min3 = val3.get_min();
				reference5.set_x(min3.x);
				ref Rect reference6 = ref mRect;
				Vector3 min4 = val3.get_min();
				reference6.set_y(min4.y);
				ref Rect reference7 = ref mRect;
				Vector3 size3 = val3.get_size();
				reference7.set_width(size3.x);
				ref Rect reference8 = ref mRect;
				Vector3 size4 = val3.get_size();
				reference8.set_height(size4.y);
			}
			else
			{
				if (!(uiCamera != null))
				{
					return;
				}
				flag = true;
				mRect = uiCamera.get_pixelRect();
			}
			float num2 = (mRect.get_xMin() + mRect.get_xMax()) * 0.5f;
			float num3 = (mRect.get_yMin() + mRect.get_yMax()) * 0.5f;
			Vector3 val4 = default(Vector3);
			val4._002Ector(num2, num3, 0f);
			if (side != Side.Center)
			{
				if (side == Side.Right || side == Side.TopRight || side == Side.BottomRight)
				{
					val4.x = mRect.get_xMax();
				}
				else if (side == Side.Top || side == Side.Center || side == Side.Bottom)
				{
					val4.x = num2;
				}
				else
				{
					val4.x = mRect.get_xMin();
				}
				if (side == Side.Top || side == Side.TopRight || side == Side.TopLeft)
				{
					val4.y = mRect.get_yMax();
				}
				else if (side == Side.Left || side == Side.Center || side == Side.Right)
				{
					val4.y = num3;
				}
				else
				{
					val4.y = mRect.get_yMin();
				}
			}
			float width = mRect.get_width();
			float height = mRect.get_height();
			val4.x += pixelOffset.x + relativeOffset.x * width;
			val4.y += pixelOffset.y + relativeOffset.y * height;
			if (flag)
			{
				if (uiCamera.get_orthographic())
				{
					val4.x = Mathf.Round(val4.x);
					val4.y = Mathf.Round(val4.y);
				}
				Vector3 val5 = uiCamera.WorldToScreenPoint(mTrans.get_position());
				val4.z = val5.z;
				val4 = uiCamera.ScreenToWorldPoint(val4);
			}
			else
			{
				val4.x = Mathf.Round(val4.x);
				val4.y = Mathf.Round(val4.y);
				if (uIPanel != null)
				{
					val4 = uIPanel.cachedTransform.TransformPoint(val4);
				}
				else if (container != null)
				{
					Transform val6 = container.get_transform().get_parent();
					if (val6 != null)
					{
						val4 = val6.TransformPoint(val4);
					}
				}
				Vector3 position = mTrans.get_position();
				val4.z = position.z;
			}
			if (flag && uiCamera.get_orthographic() && mTrans.get_parent() != null)
			{
				val4 = mTrans.get_parent().InverseTransformPoint(val4);
				val4.x = (float)Mathf.RoundToInt(val4.x);
				val4.y = (float)Mathf.RoundToInt(val4.y);
				if (mTrans.get_localPosition() != val4)
				{
					mTrans.set_localPosition(val4);
				}
			}
			else if (mTrans.get_position() != val4)
			{
				mTrans.set_position(val4);
			}
			if (runOnlyOnce && Application.get_isPlaying())
			{
				this.set_enabled(false);
			}
		}
	}
}
