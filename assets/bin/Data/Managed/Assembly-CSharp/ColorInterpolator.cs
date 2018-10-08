using System;
using UnityEngine;

[Serializable]
public class ColorInterpolator : InterpolatorBase<Color>
{
	protected override void Calc(float t, float r)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		Color val = (endValue - beginValue) * r + beginValue;
		if (addCurve != null && addCurve.get_length() > 0)
		{
			val = addValue * addCurve.Evaluate(t) + val;
		}
		if (val.r < 0f)
		{
			val.r = 0f;
		}
		else if (val.r > 1f)
		{
			val.r = 1f;
		}
		if (val.g < 0f)
		{
			val.g = 0f;
		}
		else if (val.g > 1f)
		{
			val.g = 1f;
		}
		if (val.b < 0f)
		{
			val.b = 0f;
		}
		else if (val.b > 1f)
		{
			val.b = 1f;
		}
		if (val.a < 0f)
		{
			val.a = 0f;
		}
		else if (val.a > 1f)
		{
			val.a = 1f;
		}
		nowValue = val;
	}
}
