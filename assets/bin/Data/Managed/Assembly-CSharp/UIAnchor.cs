using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor")]
public class UIAnchor : MonoBehaviour
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

	[HideInInspector]
	[SerializeField]
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
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		if (mAnim != null && mAnim.get_enabled() && mAnim.get_isPlaying())
		{
			return;
		}
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
			Transform parent = container.get_transform().get_parent();
			Bounds val2 = (!(parent != null)) ? NGUIMath.CalculateRelativeWidgetBounds(container.get_transform()) : NGUIMath.CalculateRelativeWidgetBounds(parent, container.get_transform());
			ref Rect reference5 = ref mRect;
			Vector3 min3 = val2.get_min();
			reference5.set_x(min3.x);
			ref Rect reference6 = ref mRect;
			Vector3 min4 = val2.get_min();
			reference6.set_y(min4.y);
			ref Rect reference7 = ref mRect;
			Vector3 size3 = val2.get_size();
			reference7.set_width(size3.x);
			ref Rect reference8 = ref mRect;
			Vector3 size4 = val2.get_size();
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
		Vector3 val3 = default(Vector3);
		val3._002Ector(num2, num3, 0f);
		if (side != Side.Center)
		{
			if (side == Side.Right || side == Side.TopRight || side == Side.BottomRight)
			{
				val3.x = mRect.get_xMax();
			}
			else if (side == Side.Top || side == Side.Center || side == Side.Bottom)
			{
				val3.x = num2;
			}
			else
			{
				val3.x = mRect.get_xMin();
			}
			if (side == Side.Top || side == Side.TopRight || side == Side.TopLeft)
			{
				val3.y = mRect.get_yMax();
			}
			else if (side == Side.Left || side == Side.Center || side == Side.Right)
			{
				val3.y = num3;
			}
			else
			{
				val3.y = mRect.get_yMin();
			}
		}
		float width = mRect.get_width();
		float height = mRect.get_height();
		val3.x += pixelOffset.x + relativeOffset.x * width;
		val3.y += pixelOffset.y + relativeOffset.y * height;
		if (flag)
		{
			if (uiCamera.get_orthographic())
			{
				val3.x = Mathf.Round(val3.x);
				val3.y = Mathf.Round(val3.y);
			}
			Vector3 val4 = uiCamera.WorldToScreenPoint(mTrans.get_position());
			val3.z = val4.z;
			val3 = uiCamera.ScreenToWorldPoint(val3);
		}
		else
		{
			val3.x = Mathf.Round(val3.x);
			val3.y = Mathf.Round(val3.y);
			if (uIPanel != null)
			{
				val3 = uIPanel.cachedTransform.TransformPoint(val3);
			}
			else if (container != null)
			{
				Transform parent2 = container.get_transform().get_parent();
				if (parent2 != null)
				{
					val3 = parent2.TransformPoint(val3);
				}
			}
			Vector3 position = mTrans.get_position();
			val3.z = position.z;
		}
		if (flag && uiCamera.get_orthographic() && mTrans.get_parent() != null)
		{
			val3 = mTrans.get_parent().InverseTransformPoint(val3);
			val3.x = Mathf.RoundToInt(val3.x);
			val3.y = Mathf.RoundToInt(val3.y);
			if (mTrans.get_localPosition() != val3)
			{
				mTrans.set_localPosition(val3);
			}
		}
		else if (mTrans.get_position() != val3)
		{
			mTrans.set_position(val3);
		}
		if (runOnlyOnce && Application.get_isPlaying())
		{
			this.set_enabled(false);
		}
	}
}
