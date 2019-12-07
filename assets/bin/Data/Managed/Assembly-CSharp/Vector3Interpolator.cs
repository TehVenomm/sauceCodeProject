using System;
using UnityEngine;

[Serializable]
public class Vector3Interpolator : InterpolatorBase<Vector3>
{
	protected override void Calc(float t, float r)
	{
		Vector3 vector = (endValue - beginValue) * r + beginValue;
		if (addCurve != null && addCurve.length > 0)
		{
			vector = addValue * addCurve.Evaluate(t) + vector;
		}
		nowValue = vector;
	}
}
