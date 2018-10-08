using System;
using UnityEngine;

[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Height")]
public class TweenHeight : UITweener
{
	public int from = 100;

	public int to = 100;

	public bool updateTable;

	private UIWidget mWidget;

	private UITable mTable;

	public UIWidget cachedWidget
	{
		get
		{
			if (mWidget == null)
			{
				mWidget = this.GetComponent<UIWidget>();
			}
			return mWidget;
		}
	}

	[Obsolete("Use 'value' instead")]
	public int height
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

	public int value
	{
		get
		{
			return cachedWidget.height;
		}
		set
		{
			cachedWidget.height = value;
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		value = Mathf.RoundToInt((float)from * (1f - factor) + (float)to * factor);
		if (updateTable)
		{
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
	}

	public static TweenHeight Begin(UIWidget widget, float duration, int height)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		TweenHeight tweenHeight = UITweener.Begin<TweenHeight>(widget.get_gameObject(), duration, true);
		tweenHeight.from = widget.height;
		tweenHeight.to = height;
		if (duration <= 0f)
		{
			tweenHeight.Sample(1f, true);
			tweenHeight.set_enabled(false);
		}
		return tweenHeight;
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
