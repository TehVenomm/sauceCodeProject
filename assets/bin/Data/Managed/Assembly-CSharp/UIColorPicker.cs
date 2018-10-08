using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UITexture))]
public class UIColorPicker
{
	public static UIColorPicker current;

	public Color value = Color.get_white();

	public UIWidget selectionWidget;

	public List<EventDelegate> onChange = new List<EventDelegate>();

	[NonSerialized]
	private Transform mTrans;

	[NonSerialized]
	private UITexture mUITex;

	[NonSerialized]
	private Texture2D mTex;

	[NonSerialized]
	private UICamera mCam;

	[NonSerialized]
	private Vector2 mPos;

	[NonSerialized]
	private int mWidth;

	[NonSerialized]
	private int mHeight;

	private static AnimationCurve mRed;

	private static AnimationCurve mGreen;

	private static AnimationCurve mBlue;

	public UIColorPicker()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)


	private void Start()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		mTrans = this.get_transform();
		mUITex = this.GetComponent<UITexture>();
		mCam = UICamera.FindCameraForLayer(this.get_gameObject().get_layer());
		mWidth = mUITex.width;
		mHeight = mUITex.height;
		Color[] array = (Color[])new Color[mWidth * mHeight];
		for (int i = 0; i < mHeight; i++)
		{
			float y = ((float)i - 1f) / (float)mHeight;
			for (int j = 0; j < mWidth; j++)
			{
				float x = ((float)j - 1f) / (float)mWidth;
				int num = j + i * mWidth;
				array[num] = Sample(x, y);
			}
		}
		mTex = new Texture2D(mWidth, mHeight, 3, false);
		mTex.SetPixels(array);
		mTex.set_filterMode(2);
		mTex.set_wrapMode(1);
		mTex.Apply();
		mUITex.mainTexture = mTex;
		Select(value);
	}

	private void OnDestroy()
	{
		Object.Destroy(mTex);
		mTex = null;
	}

	private void OnPress(bool pressed)
	{
		if (this.get_enabled() && pressed && UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			Sample();
		}
	}

	private void OnDrag(Vector2 delta)
	{
		if (this.get_enabled())
		{
			Sample();
		}
	}

	private void OnPan(Vector2 delta)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled())
		{
			mPos.x = Mathf.Clamp01(mPos.x + delta.x);
			mPos.y = Mathf.Clamp01(mPos.y + delta.y);
			Select(mPos);
		}
	}

	private void Sample()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = Vector2.op_Implicit(UICamera.lastEventPosition);
		val = mCam.cachedCamera.ScreenToWorldPoint(val);
		val = mTrans.InverseTransformPoint(val);
		Vector3[] localCorners = mUITex.localCorners;
		mPos.x = Mathf.Clamp01((val.x - localCorners[0].x) / (localCorners[2].x - localCorners[0].x));
		mPos.y = Mathf.Clamp01((val.y - localCorners[0].y) / (localCorners[2].y - localCorners[0].y));
		if (selectionWidget != null)
		{
			val.x = Mathf.Lerp(localCorners[0].x, localCorners[2].x, mPos.x);
			val.y = Mathf.Lerp(localCorners[0].y, localCorners[2].y, mPos.y);
			val = mTrans.TransformPoint(val);
			selectionWidget.get_transform().OverlayPosition(val, mCam.cachedCamera);
		}
		value = Sample(mPos.x, mPos.y);
		current = this;
		EventDelegate.Execute(onChange);
		current = null;
	}

	public void Select(Vector2 v)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		v.x = Mathf.Clamp01(v.x);
		v.y = Mathf.Clamp01(v.y);
		mPos = v;
		if (selectionWidget != null)
		{
			Vector3[] localCorners = mUITex.localCorners;
			v.x = Mathf.Lerp(localCorners[0].x, localCorners[2].x, mPos.x);
			v.y = Mathf.Lerp(localCorners[0].y, localCorners[2].y, mPos.y);
			v = Vector2.op_Implicit(mTrans.TransformPoint(Vector2.op_Implicit(v)));
			selectionWidget.get_transform().OverlayPosition(Vector2.op_Implicit(v), mCam.cachedCamera);
		}
		value = Sample(mPos.x, mPos.y);
		current = this;
		EventDelegate.Execute(onChange);
		current = null;
	}

	public Vector2 Select(Color c)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		if (mUITex == null)
		{
			value = c;
			return mPos;
		}
		float num = 3.40282347E+38f;
		for (int i = 0; i < mHeight; i++)
		{
			float y = ((float)i - 1f) / (float)mHeight;
			for (int j = 0; j < mWidth; j++)
			{
				float x = ((float)j - 1f) / (float)mWidth;
				Color val = Sample(x, y);
				Color val2 = val;
				val2.r -= c.r;
				val2.g -= c.g;
				val2.b -= c.b;
				float num2 = val2.r * val2.r + val2.g * val2.g + val2.b * val2.b;
				if (num2 < num)
				{
					num = num2;
					mPos.x = x;
					mPos.y = y;
				}
			}
		}
		if (selectionWidget != null)
		{
			Vector3[] localCorners = mUITex.localCorners;
			Vector3 val3 = default(Vector3);
			val3.x = Mathf.Lerp(localCorners[0].x, localCorners[2].x, mPos.x);
			val3.y = Mathf.Lerp(localCorners[0].y, localCorners[2].y, mPos.y);
			val3.z = 0f;
			val3 = mTrans.TransformPoint(val3);
			selectionWidget.get_transform().OverlayPosition(val3, mCam.cachedCamera);
		}
		value = c;
		current = this;
		EventDelegate.Execute(onChange);
		current = null;
		return mPos;
	}

	public static Color Sample(float x, float y)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		if (mRed == null)
		{
			mRed = new AnimationCurve((Keyframe[])new Keyframe[8]
			{
				new Keyframe(0f, 1f),
				new Keyframe(0.142857149f, 1f),
				new Keyframe(0.2857143f, 0f),
				new Keyframe(0.428571433f, 0f),
				new Keyframe(0.5714286f, 0f),
				new Keyframe(0.714285731f, 1f),
				new Keyframe(0.857142866f, 1f),
				new Keyframe(1f, 0.5f)
			});
			mGreen = new AnimationCurve((Keyframe[])new Keyframe[8]
			{
				new Keyframe(0f, 0f),
				new Keyframe(0.142857149f, 1f),
				new Keyframe(0.2857143f, 1f),
				new Keyframe(0.428571433f, 1f),
				new Keyframe(0.5714286f, 0f),
				new Keyframe(0.714285731f, 0f),
				new Keyframe(0.857142866f, 0f),
				new Keyframe(1f, 0.5f)
			});
			mBlue = new AnimationCurve((Keyframe[])new Keyframe[8]
			{
				new Keyframe(0f, 0f),
				new Keyframe(0.142857149f, 0f),
				new Keyframe(0.2857143f, 0f),
				new Keyframe(0.428571433f, 1f),
				new Keyframe(0.5714286f, 1f),
				new Keyframe(0.714285731f, 1f),
				new Keyframe(0.857142866f, 0f),
				new Keyframe(1f, 0.5f)
			});
		}
		Vector3 val = default(Vector3);
		val._002Ector(mRed.Evaluate(x), mGreen.Evaluate(x), mBlue.Evaluate(x));
		if (y < 0.5f)
		{
			y *= 2f;
			val.x *= y;
			val.y *= y;
			val.z *= y;
		}
		else
		{
			val = Vector3.Lerp(val, Vector3.get_one(), y * 2f - 1f);
		}
		return new Color(val.x, val.y, val.z, 1f);
	}
}
