using System;
using UnityEngine;

[Serializable]
public class QuaternionInterpolator : InterpolatorBase<Quaternion>
{
	protected override void Calc(float t, float r)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (addCurve != null && addCurve.get_length() > 0)
		{
			r *= addCurve.Evaluate(t);
		}
		nowValue = Quaternion.Slerp(beginValue, endValue, r);
	}
}
