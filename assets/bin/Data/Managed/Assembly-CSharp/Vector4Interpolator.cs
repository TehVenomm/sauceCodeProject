using System;
using UnityEngine;

[Serializable]
public class Vector4Interpolator : InterpolatorBase<Vector4>
{
	protected override void Calc(float t, float r)
	{
		Vector4 vector = (endValue - beginValue) * r + beginValue;
		if (addCurve != null && addCurve.length > 0)
		{
			vector = addValue * addCurve.Evaluate(t) + vector;
		}
		nowValue = vector;
	}
}
