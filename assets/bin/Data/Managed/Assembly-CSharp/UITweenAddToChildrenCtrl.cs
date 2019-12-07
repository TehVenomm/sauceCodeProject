using UnityEngine;

public class UITweenAddToChildrenCtrl : MonoBehaviour
{
	public UITweener baseTween;

	public float dispDuration;

	public float dispStartDelay;

	public int repetitionStartIndex = -1;

	private void Awake()
	{
		if (baseTween == null)
		{
			baseTween = GetComponent<UITweener>();
		}
	}

	[ContextMenu("TweenAdd")]
	public void TweenAdd()
	{
		if (!base.enabled || baseTween == null)
		{
			return;
		}
		int childCount = base.transform.childCount;
		Transform[] array = new Transform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = base.transform.GetChild(i);
		}
		for (int j = 0; j < childCount; j++)
		{
			Transform transform = array[j];
			if (transform == null)
			{
				return;
			}
			UITweenAddCtrlChild[] componentsInChildren = transform.GetComponentsInChildren<UITweenAddCtrlChild>();
			if (componentsInChildren == null || componentsInChildren.Length == 0)
			{
				GameObject gameObject = new GameObject(transform.name);
				gameObject.layer = 5;
				gameObject.transform.parent = transform.transform.parent;
				gameObject.transform.localPosition = transform.transform.localPosition;
				gameObject.transform.localScale = Vector3.one;
				gameObject.AddComponent<UITweenAddCtrlChild>();
				UIWidget component = transform.GetComponent<UIWidget>();
				if (component != null)
				{
					UIWidget uIWidget = gameObject.AddComponent<UIWidget>();
					uIWidget.width = component.width;
					uIWidget.height = component.height;
					uIWidget.keepAspectRatio = component.keepAspectRatio;
					uIWidget.pivot = component.pivot;
					uIWidget.depth = component.depth;
					uIWidget.alpha = component.alpha;
				}
				transform.transform.parent = gameObject.transform;
				if (transform.gameObject.GetComponent(baseTween.GetType()) == null)
				{
					transform.gameObject.AddComponent(baseTween.GetType());
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
		if (!base.enabled)
		{
			return;
		}
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			if (child != null)
			{
				Component componentInChildren = child.gameObject.GetComponentInChildren(baseTween.GetType());
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
		int num = (repetitionStartIndex > -1) ? Mathf.Min(repetitionStartIndex, i) : i;
		new_tw.animationCurve = base_tw.animationCurve;
		new_tw.style = base_tw.style;
		new_tw.duration = base_tw.duration + dispDuration * (float)num;
		new_tw.delay = base_tw.delay + dispStartDelay * (float)num;
		new_tw.ignoreTimeScale = base_tw.ignoreTimeScale;
		new_tw.tweenGroup = base_tw.tweenGroup;
		new_tw.enabled = true;
		if (!is_skip)
		{
			new_tw.ResetToBeginning();
			return;
		}
		new_tw.tweenFactor = 1f;
		new_tw.Sample(new_tw.tweenFactor, isFinished: false);
	}
}
