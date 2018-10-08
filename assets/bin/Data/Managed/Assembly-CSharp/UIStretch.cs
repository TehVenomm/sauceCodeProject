using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Stretch")]
public class UIStretch
{
	public enum Style
	{
		None,
		Horizontal,
		Vertical,
		Both,
		BasedOnHeight,
		FillKeepingRatio,
		FitInternalKeepingRatio
	}

	public Camera uiCamera;

	public GameObject container;

	public Style style;

	public bool runOnlyOnce = true;

	public Vector2 relativeSize = Vector2.get_one();

	public Vector2 initialSize = Vector2.get_one();

	public Vector2 borderPadding = Vector2.get_zero();

	[HideInInspector]
	[SerializeField]
	private UIWidget widgetContainer;

	private Transform mTrans;

	private UIWidget mWidget;

	private UISprite mSprite;

	private UIPanel mPanel;

	private UIRoot mRoot;

	private Animation mAnim;

	private Rect mRect;

	private bool mStarted;

	public UIStretch()
		: this()
	{
	}//IL_0008: Unknown result type (might be due to invalid IL or missing references)
	//IL_000d: Unknown result type (might be due to invalid IL or missing references)
	//IL_0013: Unknown result type (might be due to invalid IL or missing references)
	//IL_0018: Unknown result type (might be due to invalid IL or missing references)
	//IL_001e: Unknown result type (might be due to invalid IL or missing references)
	//IL_0023: Unknown result type (might be due to invalid IL or missing references)


	private void Awake()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		mAnim = this.GetComponent<Animation>();
		mRect = default(Rect);
		mTrans = this.get_transform();
		mWidget = this.GetComponent<UIWidget>();
		mSprite = this.GetComponent<UISprite>();
		mPanel = this.GetComponent<UIPanel>();
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
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		if (container == null && widgetContainer != null)
		{
			container = widgetContainer.get_gameObject();
			widgetContainer = null;
		}
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(this.get_gameObject().get_layer());
		}
		mRoot = NGUITools.FindInParents<UIRoot>(this.get_gameObject());
		Update();
		mStarted = true;
	}

	private void Update()
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		if ((!(mAnim != null) || !mAnim.get_isPlaying()) && style != 0)
		{
			UIWidget uIWidget = (!(container == null)) ? container.GetComponent<UIWidget>() : null;
			UIPanel uIPanel = (!(container == null) || !(uIWidget == null)) ? container.GetComponent<UIPanel>() : null;
			float num = 1f;
			if (uIWidget != null)
			{
				Bounds val = uIWidget.CalculateBounds(this.get_transform().get_parent());
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
					float num2 = (!(mRoot != null)) ? 0.5f : ((float)mRoot.activeHeight / (float)Screen.get_height() * 0.5f);
					mRect.set_xMin((float)(-Screen.get_width()) * num2);
					mRect.set_yMin((float)(-Screen.get_height()) * num2);
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
				Transform val2 = this.get_transform().get_parent();
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
				mRect = uiCamera.get_pixelRect();
				if (mRoot != null)
				{
					num = mRoot.pixelSizeAdjustment;
				}
			}
			float num3 = mRect.get_width();
			float num4 = mRect.get_height();
			if (num != 1f && num4 > 1f)
			{
				float num5 = (float)mRoot.activeHeight / num4;
				num3 *= num5;
				num4 *= num5;
			}
			Vector3 val4 = (!(mWidget != null)) ? mTrans.get_localScale() : new Vector3((float)mWidget.width, (float)mWidget.height);
			if (style == Style.BasedOnHeight)
			{
				val4.x = relativeSize.x * num4;
				val4.y = relativeSize.y * num4;
			}
			else if (style == Style.FillKeepingRatio)
			{
				float num6 = num3 / num4;
				float num7 = initialSize.x / initialSize.y;
				if (num7 < num6)
				{
					float num8 = num3 / initialSize.x;
					val4.x = num3;
					val4.y = initialSize.y * num8;
				}
				else
				{
					float num9 = num4 / initialSize.y;
					val4.x = initialSize.x * num9;
					val4.y = num4;
				}
			}
			else if (style == Style.FitInternalKeepingRatio)
			{
				float num10 = num3 / num4;
				float num11 = initialSize.x / initialSize.y;
				if (num11 > num10)
				{
					float num12 = num3 / initialSize.x;
					val4.x = num3;
					val4.y = initialSize.y * num12;
				}
				else
				{
					float num13 = num4 / initialSize.y;
					val4.x = initialSize.x * num13;
					val4.y = num4;
				}
			}
			else
			{
				if (style != Style.Vertical)
				{
					val4.x = relativeSize.x * num3;
				}
				if (style != Style.Horizontal)
				{
					val4.y = relativeSize.y * num4;
				}
			}
			if (mSprite != null)
			{
				float num14 = (!(mSprite.atlas != null)) ? 1f : mSprite.atlas.pixelSize;
				val4.x -= borderPadding.x * num14;
				val4.y -= borderPadding.y * num14;
				if (style != Style.Vertical)
				{
					mSprite.width = Mathf.RoundToInt(val4.x);
				}
				if (style != Style.Horizontal)
				{
					mSprite.height = Mathf.RoundToInt(val4.y);
				}
				val4 = Vector3.get_one();
			}
			else if (mWidget != null)
			{
				if (style != Style.Vertical)
				{
					mWidget.width = Mathf.RoundToInt(val4.x - borderPadding.x);
				}
				if (style != Style.Horizontal)
				{
					mWidget.height = Mathf.RoundToInt(val4.y - borderPadding.y);
				}
				val4 = Vector3.get_one();
			}
			else if (mPanel != null)
			{
				Vector4 baseClipRegion = mPanel.baseClipRegion;
				if (style != Style.Vertical)
				{
					baseClipRegion.z = val4.x - borderPadding.x;
				}
				if (style != Style.Horizontal)
				{
					baseClipRegion.w = val4.y - borderPadding.y;
				}
				mPanel.baseClipRegion = baseClipRegion;
				val4 = Vector3.get_one();
			}
			else
			{
				if (style != Style.Vertical)
				{
					val4.x -= borderPadding.x;
				}
				if (style != Style.Horizontal)
				{
					val4.y -= borderPadding.y;
				}
			}
			if (mTrans.get_localScale() != val4)
			{
				mTrans.set_localScale(val4);
			}
			if (runOnlyOnce && Application.get_isPlaying())
			{
				this.set_enabled(false);
			}
		}
	}
}
