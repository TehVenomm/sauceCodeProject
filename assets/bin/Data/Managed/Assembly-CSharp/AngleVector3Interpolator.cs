using System;
using UnityEngine;

[Serializable]
public class AngleVector3Interpolator : Vector3Interpolator
{
	protected override void Calc(float t, float r)
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = default(Vector3);
		val.x = Mathf.LerpAngle(beginValue.x, endValue.x, r);
		val.y = Mathf.LerpAngle(beginValue.y, endValue.y, r);
		val.z = Mathf.LerpAngle(beginValue.z, endValue.z, r);
		if (addCurve != null && addCurve.get_length() > 0)
		{
			val = addValue * addCurve.Evaluate(t) + val;
		}
		nowValue = val;
	}
}
