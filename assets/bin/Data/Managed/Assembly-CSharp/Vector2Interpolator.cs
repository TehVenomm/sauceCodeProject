using System;
using UnityEngine;

[Serializable]
public class Vector2Interpolator : InterpolatorBase<Vector2>
{
	protected override void Calc(float t, float r)
	{
		Vector2 vector = (endValue - beginValue) * r + beginValue;
		if (addCurve != null && addCurve.length > 0)
		{
			vector = addValue * addCurve.Evaluate(t) + vector;
		}
		nowValue = vector;
	}
}
