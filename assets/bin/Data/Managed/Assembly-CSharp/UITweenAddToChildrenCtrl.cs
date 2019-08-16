using UnityEngine;

public class UITweenAddToChildrenCtrl : MonoBehaviour
{
	public UITweener baseTween;

	public float dispDuration;

	public float dispStartDelay;

	public int repetitionStartIndex = -1;

	public UITweenAddToChildrenCtrl()
		: this()
	{
	}

	private void Awake()
	{
		if (baseTween == null)
		{
			baseTween = this.GetComponent<UITweener>();
		}
	}

	[ContextMenu("TweenAdd")]
	public void TweenAdd()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		if (!this.get_enabled() || baseTween == null)
		{
			return;
		}
		int childCount = this.get_transform().get_childCount();
		Transform[] array = (Transform[])new Transform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = this.get_transform().GetChild(i);
		}
		for (int j = 0; j < childCount; j++)
		{
			Transform val = array[j];
			if (val == null)
			{
				return;
			}
			UITweenAddCtrlChild[] componentsInChildren = val.GetComponentsInChildren<UITweenAddCtrlChild>();
			if (componentsInChildren == null || componentsInChildren.Length == 0)
			{
				GameObject val2 = new GameObject(val.get_name());
				val2.set_layer(5);
				val2.get_transform().set_parent(val.get_transform().get_parent());
				val2.get_transform().set_localPosition(val.get_transform().get_localPosition());
				val2.get_transform().set_localScale(Vector3.get_one());
				val2.AddComponent<UITweenAddCtrlChild>();
				UIWidget component = val.GetComponent<UIWidget>();
				if (component != null)
				{
					UIWidget uIWidget = val2.AddComponent<UIWidget>();
					uIWidget.width = component.width;
					uIWidget.height = component.height;
					uIWidget.keepAspectRatio = component.keepAspectRatio;
					uIWidget.pivot = component.pivot;
					uIWidget.depth = component.depth;
					uIWidget.alpha = component.alpha;
				}
				val.get_transform().set_parent(val2.get_transform());
				Component component2 = val.get_gameObject().GetComponent(((object)baseTween).GetType());
				if (component2 == null)
				{
					component2 = val.get_gameObject().AddComponent(((object)baseTween).GetType());
				}
			}
		}
		InitTween();
	}

	public void SkipTween()
	{
		_InitTween(is_skip: true);
	}

	public void InitTween()
	{
		_InitTween(is_skip: false);
	}

	private void _InitTween(bool is_skip)
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		if (!this.get_enabled())
		{
			return;
		}
		int childCount = this.get_transform().get_childCount();
		for (int i = 0; i < childCount; i++)
		{
			Transform child = this.get_transform().GetChild(i);
			if (child != null)
			{
				Component componentInChildren = child.get_gameObject().GetComponentInChildren(((object)baseTween).GetType());
				if (componentInChildren is TweenAlpha)
				{
					TweenAlpha tweenAlpha = componentInChildren as TweenAlpha;
					TweenAlpha tweenAlpha2 = baseTween as TweenAlpha;
					tweenAlpha.from = tweenAlpha2.from;
					tweenAlpha.to = tweenAlpha2.to;
					InitTween(tweenAlpha, tweenAlpha2, i, is_skip);
				}
				else if (componentInChildren is TweenColor)
				{
					TweenColor tweenColor = componentInChildren as TweenColor;
					TweenColor tweenColor2 = baseTween as TweenColor;
					tweenColor.from = tweenColor2.from;
					tweenColor.to = tweenColor2.to;
					InitTween(tweenColor, tweenColor2, i, is_skip);
				}
				else if (componentInChildren is TweenPosition)
				{
					TweenPosition tweenPosition = componentInChildren as TweenPosition;
					TweenPosition tweenPosition2 = baseTween as TweenPosition;
					tweenPosition.from = tweenPosition2.from;
					tweenPosition.to = tweenPosition2.to;
					InitTween(tweenPosition, tweenPosition2, i, is_skip);
				}
				else if (componentInChildren is TweenRotation)
				{
					TweenRotation tweenRotation = componentInChildren as TweenRotation;
					TweenRotation tweenRotation2 = baseTween as TweenRotation;
					tweenRotation.from = tweenRotation2.from;
					tweenRotation.to = tweenRotation2.to;
					InitTween(tweenRotation, tweenRotation2, i, is_skip);
				}
				else if (componentInChildren is TweenScale)
				{
					TweenScale tweenScale = componentInChildren as TweenScale;
					TweenScale tweenScale2 = baseTween as TweenScale;
					tweenScale.from = tweenScale2.from;
					tweenScale.to = tweenScale2.to;
					InitTween(tweenScale, tweenScale2, i, is_skip);
				}
				else if (componentInChildren is TweenWidth)
				{
					TweenWidth tweenWidth = componentInChildren as TweenWidth;
					TweenWidth tweenWidth2 = baseTween as TweenWidth;
					tweenWidth.from = tweenWidth2.from;
					tweenWidth.to = tweenWidth2.to;
					InitTween(tweenWidth, tweenWidth2, i, is_skip);
				}
				else if (componentInChildren is TweenHeight)
				{
					TweenHeight tweenHeight = componentInChildren as TweenHeight;
					TweenHeight tweenHeight2 = baseTween as TweenHeight;
					tweenHeight.from = tweenHeight2.from;
					tweenHeight.to = tweenHeight2.to;
					InitTween(tweenHeight, tweenHeight2, i, is_skip);
				}
			}
		}
	}

	private void InitTween(UITweener new_tw, UITweener base_tw, int i, bool is_skip)
	{
		int num = (repetitionStartIndex <= -1) ? i : Mathf.Min(repetitionStartIndex, i);
		new_tw.animationCurve = base_tw.animationCurve;
		new_tw.style = base_tw.style;
		new_tw.duration = base_tw.duration + dispDuration * (float)num;
		new_tw.delay = base_tw.delay + dispStartDelay * (float)num;
		new_tw.ignoreTimeScale = base_tw.ignoreTimeScale;
		new_tw.tweenGroup = base_tw.tweenGroup;
		new_tw.set_enabled(true);
		if (!is_skip)
		{
			new_tw.ResetToBeginning();
			return;
		}
		new_tw.tweenFactor = 1f;
		new_tw.Sample(new_tw.tweenFactor, isFinished: false);
	}
}
