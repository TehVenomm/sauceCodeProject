using System;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	public Color from = Color.get_white();

	public Color to = Color.get_white();

	private bool mCached;

	private UIWidget mWidget;

	private Material mMat;

	private Light mLight;

	private SpriteRenderer mSr;

	[Obsolete("Use 'value' instead")]
	public Color color
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return value;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			this.value = value;
		}
	}

	public Color value
	{
		get
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			if (!mCached)
			{
				Cache();
			}
			if (mWidget != null)
			{
				return mWidget.color;
			}
			if (mMat != null)
			{
				return mMat.get_color();
			}
			if (mSr != null)
			{
				return mSr.get_color();
			}
			if (mLight != null)
			{
				return mLight.get_color();
			}
			return Color.get_black();
		}
		set
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			if (!mCached)
			{
				Cache();
			}
			if (mWidget != null)
			{
				mWidget.color = value;
			}
			else if (mMat != null)
			{
				mMat.set_color(value);
			}
			else if (mSr != null)
			{
				mSr.set_color(value);
			}
			else if (mLight != null)
			{
				mLight.set_color(value);
				mLight.set_enabled(value.r + value.g + value.b > 0.01f);
			}
		}
	}

	private void Cache()
	{
		mCached = true;
		mWidget = this.GetComponent<UIWidget>();
		if (mWidget != null)
		{
			return;
		}
		mSr = this.GetComponent<SpriteRenderer>();
		if (mSr != null)
		{
			return;
		}
		Renderer component = this.GetComponent<Renderer>();
		if (component != null)
		{
			mMat = component.get_material();
			return;
		}
		mLight = this.GetComponent<Light>();
		if (mLight == null)
		{
			mWidget = this.GetComponentInChildren<UIWidget>();
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		value = Color.Lerp(from, to, factor);
	}

	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration);
		tweenColor.from = tweenColor.value;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, isFinished: true);
			tweenColor.set_enabled(false);
		}
		return tweenColor;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		from = value;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		to = value;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		value = from;
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		value = to;
	}
}
