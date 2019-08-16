using System;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
	public Vector3 from = Vector3.get_one();

	public Vector3 to = Vector3.get_one();

	public bool updateTable;

	private Transform mTrans;

	private UITable mTable;

	public Transform cachedTransform
	{
		get
		{
			if (mTrans == null)
			{
				mTrans = this.get_transform();
			}
			return mTrans;
		}
	}

	public Vector3 value
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return cachedTransform.get_localScale();
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			cachedTransform.set_localScale(value);
		}
	}

	[Obsolete("Use 'value' instead")]
	public Vector3 scale
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

	protected override void OnUpdate(float factor, bool isFinished)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		value = from * (1f - factor) + to * factor;
		if (!updateTable)
		{
			return;
		}
		if (mTable == null)
		{
			mTable = NGUITools.FindInParents<UITable>(this.get_gameObject());
			if (mTable == null)
			{
				updateTable = false;
				return;
			}
		}
		mTable.repositionNow = true;
	}

	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration);
		tweenScale.from = tweenScale.value;
		tweenScale.to = scale;
		if (duration <= 0f)
		{
			tweenScale.Sample(1f, isFinished: true);
			tweenScale.set_enabled(false);
		}
		return tweenScale;
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
