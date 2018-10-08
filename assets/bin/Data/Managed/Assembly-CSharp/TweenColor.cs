using System;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	public Color from = Color.white;

	public Color to = Color.white;

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
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	public Color value
	{
		get
		{
			if (!mCached)
			{
				Cache();
			}
			if ((UnityEngine.Object)mWidget != (UnityEngine.Object)null)
			{
				return mWidget.color;
			}
			if ((UnityEngine.Object)mMat != (UnityEngine.Object)null)
			{
				return mMat.color;
			}
			if ((UnityEngine.Object)mSr != (UnityEngine.Object)null)
			{
				return mSr.color;
			}
			if ((UnityEngine.Object)mLight != (UnityEngine.Object)null)
			{
				return mLight.color;
			}
			return Color.black;
		}
		set
		{
			if (!mCached)
			{
				Cache();
			}
			if ((UnityEngine.Object)mWidget != (UnityEngine.Object)null)
			{
				mWidget.color = value;
			}
			else if ((UnityEngine.Object)mMat != (UnityEngine.Object)null)
			{
				mMat.color = value;
			}
			else if ((UnityEngine.Object)mSr != (UnityEngine.Object)null)
			{
				mSr.color = value;
			}
			else if ((UnityEngine.Object)mLight != (UnityEngine.Object)null)
			{
				mLight.color = value;
				mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	private void Cache()
	{
		mCached = true;
		mWidget = GetComponent<UIWidget>();
		if (!((UnityEngine.Object)mWidget != (UnityEngine.Object)null))
		{
			mSr = GetComponent<SpriteRenderer>();
			if (!((UnityEngine.Object)mSr != (UnityEngine.Object)null))
			{
				Renderer component = GetComponent<Renderer>();
				if ((UnityEngine.Object)component != (UnityEngine.Object)null)
				{
					mMat = component.material;
				}
				else
				{
					mLight = GetComponent<Light>();
					if ((UnityEngine.Object)mLight == (UnityEngine.Object)null)
					{
						mWidget = GetComponentInChildren<UIWidget>();
					}
				}
			}
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		value = Color.Lerp(from, to, factor);
	}

	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration, true);
		tweenColor.from = tweenColor.value;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		from = value;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		to = value;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		value = from;
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		value = to;
	}
}
