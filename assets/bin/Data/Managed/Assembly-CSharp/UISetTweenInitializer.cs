using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class UISetTweenInitializer
{
	public UISetTweenInitializer()
		: this()
	{
	}

	private void Awake()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		if (Application.get_isPlaying())
		{
			Transform val = this.get_transform();
			UIWidget component = this.GetComponent<UIWidget>();
			if (!(component == null))
			{
				UITweener[] components = this.GetComponents<UITweener>();
				if (components != null && components.Length > 0)
				{
					int num = components.Length;
					for (int i = 0; i < num; i++)
					{
						UITweener uITweener = components[i];
						if (uITweener != null)
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
								val.set_localPosition(tweenPosition.from);
							}
							else if (uITweener is TweenRotation)
							{
								TweenRotation tweenRotation = uITweener as TweenRotation;
								val.set_localEulerAngles(tweenRotation.from);
							}
							else if (uITweener is TweenScale)
							{
								TweenScale tweenScale = uITweener as TweenScale;
								val.set_localScale(tweenScale.from);
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
