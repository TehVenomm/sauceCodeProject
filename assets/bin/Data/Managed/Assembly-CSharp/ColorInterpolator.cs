using System;
using UnityEngine;

[Serializable]
public class ColorInterpolator : InterpolatorBase<Color>
{
	protected override void Calc(float t, float r)
	{
		Color color = (endValue - beginValue) * r + beginValue;
		if (addCurve != null && addCurve.length > 0)
		{
			color = addValue * addCurve.Evaluate(t) + color;
		}
		if (color.r < 0f)
		{
			color.r = 0f;
		}
		else if (color.r > 1f)
		{
			color.r = 1f;
		}
		if (color.g < 0f)
		{
			color.g = 0f;
		}
		else if (color.g > 1f)
		{
			color.g = 1f;
		}
		if (color.b < 0f)
		{
			color.b = 0f;
		}
		else if (color.b > 1f)
		{
			color.b = 1f;
		}
		if (color.a < 0f)
		{
			color.a = 0f;
		}
		else if (color.a > 1f)
		{
			color.a = 1f;
		}
		nowValue = color;
	}
}
