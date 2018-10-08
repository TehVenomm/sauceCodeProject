using System;
using UnityEngine;

[Serializable]
public class AngleVector3Interpolator : Vector3Interpolator
{
	protected override void Calc(float t, float r)
	{
		Vector3 vector = default(Vector3);
		vector.x = Mathf.LerpAngle(beginValue.x, endValue.x, r);
		vector.y = Mathf.LerpAngle(beginValue.y, endValue.y, r);
		vector.z = Mathf.LerpAngle(beginValue.z, endValue.z, r);
		if (addCurve != null && addCurve.length > 0)
		{
			vector = addValue * addCurve.Evaluate(t) + vector;
		}
		nowValue = vector;
	}
}
