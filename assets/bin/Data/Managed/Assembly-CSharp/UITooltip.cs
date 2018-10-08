using System;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip
{
	protected static UITooltip mInstance;

	public Camera uiCamera;

	public UILabel text;

	public UISprite background;

	public float appearSpeed = 10f;

	public bool scalingTransitions = true;

	protected GameObject mTooltip;

	protected Transform mTrans;

	protected float mTarget;

	protected float mCurrent;

	protected Vector3 mPos;

	protected Vector3 mSize = Vector3.get_zero();

	protected UIWidget[] mWidgets;

	public static bool isVisible => mInstance != null && mInstance.mTarget == 1f;

	public UITooltip()
		: this()
	{
	}//IL_0013: Unknown result type (might be due to invalid IL or missing references)
	//IL_0018: Unknown result type (might be due to invalid IL or missing references)


	private void Awake()
	{
		mInstance = this;
	}

	private void OnDestroy()
	{
		mInstance = null;
	}

	protected virtual void Start()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		mTrans = this.get_transform();
		mWidgets = this.GetComponentsInChildren<UIWidget>();
		mPos = mTrans.get_localPosition();
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(this.get_gameObject().get_layer());
		}
		SetAlpha(0f);
	}

	protected virtual void Update()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		if (mTooltip != UICamera.tooltipObject)
		{
			mTooltip = null;
			mTarget = 0f;
		}
		if (mCurrent != mTarget)
		{
			mCurrent = Mathf.Lerp(mCurrent, mTarget, RealTime.deltaTime * appearSpeed);
			if (Mathf.Abs(mCurrent - mTarget) < 0.001f)
			{
				mCurrent = mTarget;
			}
			SetAlpha(mCurrent * mCurrent);
			if (scalingTransitions)
			{
				Vector3 val = mSize * 0.25f;
				val.y = 0f - val.y;
				Vector3 localScale = Vector3.get_one() * (1.5f - mCurrent * 0.5f);
				Vector3 localPosition = Vector3.Lerp(mPos - val, mPos, mCurrent);
				mTrans.set_localPosition(localPosition);
				mTrans.set_localScale(localScale);
			}
		}
	}

	protected virtual void SetAlpha(float val)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int num = mWidgets.Length; i < num; i++)
		{
			UIWidget uIWidget = mWidgets[i];
			Color color = uIWidget.color;
			color.a = val;
			uIWidget.color = color;
		}
	}

	protected virtual void SetText(string tooltipText)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		if (text != null && !string.IsNullOrEmpty(tooltipText))
		{
			mTarget = 1f;
			mTooltip = UICamera.tooltipObject;
			text.text = tooltipText;
			mPos = Vector2.op_Implicit(UICamera.lastEventPosition);
			Transform val = text.get_transform();
			Vector3 localPosition = val.get_localPosition();
			Vector3 localScale = val.get_localScale();
			mSize = Vector2.op_Implicit(text.printedSize);
			mSize.x *= localScale.x;
			mSize.y *= localScale.y;
			if (background != null)
			{
				Vector4 border = background.border;
				mSize.x += border.x + border.z + (localPosition.x - border.x) * 2f;
				mSize.y += border.y + border.w + (0f - localPosition.y - border.y) * 2f;
				background.width = Mathf.RoundToInt(mSize.x);
				background.height = Mathf.RoundToInt(mSize.y);
			}
			if (uiCamera != null)
			{
				mPos.x = Mathf.Clamp01(mPos.x / (float)Screen.get_width());
				mPos.y = Mathf.Clamp01(mPos.y / (float)Screen.get_height());
				float orthographicSize = uiCamera.get_orthographicSize();
				Vector3 lossyScale = mTrans.get_parent().get_lossyScale();
				float num = orthographicSize / lossyScale.y;
				float num2 = (float)Screen.get_height() * 0.5f / num;
				Vector2 val2 = default(Vector2);
				val2._002Ector(num2 * mSize.x / (float)Screen.get_width(), num2 * mSize.y / (float)Screen.get_height());
				mPos.x = Mathf.Min(mPos.x, 1f - val2.x);
				mPos.y = Mathf.Max(mPos.y, val2.y);
				mTrans.set_position(uiCamera.ViewportToWorldPoint(mPos));
				mPos = mTrans.get_localPosition();
				mPos.x = Mathf.Round(mPos.x);
				mPos.y = Mathf.Round(mPos.y);
				mTrans.set_localPosition(mPos);
			}
			else
			{
				if (mPos.x + mSize.x > (float)Screen.get_width())
				{
					mPos.x = (float)Screen.get_width() - mSize.x;
				}
				if (mPos.y - mSize.y < 0f)
				{
					mPos.y = mSize.y;
				}
				mPos.x -= (float)Screen.get_width() * 0.5f;
				mPos.y -= (float)Screen.get_height() * 0.5f;
			}
		}
		else
		{
			mTooltip = null;
			mTarget = 0f;
		}
	}

	[Obsolete("Use UITooltip.Show instead")]
	public static void ShowText(string text)
	{
		if (mInstance != null)
		{
			mInstance.SetText(text);
		}
	}

	public static void Show(string text)
	{
		if (mInstance != null)
		{
			mInstance.SetText(text);
		}
	}

	public static void Hide()
	{
		if (mInstance != null)
		{
			mInstance.mTooltip = null;
			mInstance.mTarget = 0f;
		}
	}
}
