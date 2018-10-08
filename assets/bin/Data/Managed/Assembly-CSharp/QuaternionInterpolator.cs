using System;
using UnityEngine;

[Serializable]
public class QuaternionInterpolator : InterpolatorBase<Quaternion>
{
	protected override void Calc(float t, float r)
	{
		if (addCurve != null && addCurve.length > 0)
		{
			r *= addCurve.Evaluate(t);
		}
		nowValue = Quaternion.Slerp(beginValue, endValue, r);
	}
}
