using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class UISetTweenInitializer : MonoBehaviour
{
	private void Awake()
	{
		if (Application.isPlaying)
		{
			Transform transform = base.transform;
			UIWidget component = GetComponent<UIWidget>();
			if (!((Object)component == (Object)null))
			{
				UITweener[] components = GetComponents<UITweener>();
				if (components != null && components.Length > 0)
				{
					int num = components.Length;
					for (int i = 0; i < num; i++)
					{
						UITweener uITweener = components[i];
						if ((Object)uITweener != (Object)null)
						{
							if (uITweener is TweenAlpha)
							{
								TweenAlpha tweenAlpha = uITweener as TweenAlpha;
								component.alpha = tweenAlpha.from;
							}
							else if (uITweener is TweenColor)
							{
								TweenColor tweenColor = uITweener as TweenColor;
								component.color = tweenColor.from;
							}
							else if (uITweener is TweenPosition)
							{
								TweenPosition tweenPosition = uITweener as TweenPosition;
								transform.localPosition = tweenPosition.from;
							}
							else if (uITweener is TweenRotation)
							{
								TweenRotation tweenRotation = uITweener as TweenRotation;
								transform.localEulerAngles = tweenRotation.from;
							}
							else if (uITweener is TweenScale)
							{
								TweenScale tweenScale = uITweener as TweenScale;
								transform.localScale = tweenScale.from;
							}
							else if (uITweener is TweenWidth)
							{
								TweenWidth tweenWidth = uITweener as TweenWidth;
								component.width = tweenWidth.from;
							}
							else if (uITweener is TweenHeight)
							{
								TweenHeight tweenHeight = uITweener as TweenHeight;
								component.height = tweenHeight.from;
							}
						}
					}
				}
			}
		}
	}
}
