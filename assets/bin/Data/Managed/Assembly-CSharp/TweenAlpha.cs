using System;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	[Range(0f, 1f)]
	public float from = 1f;

	[Range(0f, 1f)]
	public float to = 1f;

	private bool mCached;

	private UIRect mRect;

	private Material mMat;

	private SpriteRenderer mSr;

	[Obsolete("Use 'value' instead")]
	public float alpha
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	public float value
	{
		get
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			if (!mCached)
			{
				Cache();
			}
			if (mRect != null)
			{
				return mRect.alpha;
			}
			if (mSr != null)
			{
				Color color = mSr.get_color();
				return color.a;
			}
			float result;
			if (mMat != null)
			{
				Color color2 = mMat.get_color();
				result = color2.a;
			}
			else
			{
				result = 1f;
			}
			return result;
		}
		set
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			if (!mCached)
			{
				Cache();
			}
			if (mRect != null)
			{
				mRect.alpha = value;
			}
			else if (mSr != null)
			{
				Color color = mSr.get_color();
				color.a = value;
				mSr.set_color(color);
			}
			else if (mMat != null)
			{
				Color color2 = mMat.get_color();
				color2.a = value;
				mMat.set_color(color2);
			}
		}
	}

	private void Cache()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		mCached = true;
		mRect = this.GetComponent<UIRect>();
		mSr = this.GetComponent<SpriteRenderer>();
		if (mRect == null && mSr == null)
		{
			Renderer component = this.GetComponent<Renderer>();
			if (component != null)
			{
				mMat = component.get_material();
			}
			if (mMat == null)
			{
				mRect = this.GetComponentInChildren<UIRect>();
			}
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		value = Mathf.Lerp(from, to, factor);
	}

	public static TweenAlpha Begin(GameObject go, float duration, float alpha)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration, true);
		tweenAlpha.from = tweenAlpha.value;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.set_enabled(false);
		}
		return tweenAlpha;
	}

	public override void SetStartToCurrentValue()
	{
		from = value;
	}

	public override void SetEndToCurrentValue()
	{
		to = value;
	}
}
