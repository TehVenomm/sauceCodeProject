using System;
using UnityEngine;

[Serializable]
public class ShakeInterpolator : InterpolatorBase<Vector3>
{
	private Vector3Interpolator shakeAnimPos = new Vector3Interpolator();

	protected override void Calc(float t, float r)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		if (!shakeAnimPos.IsPlaying())
		{
			shakeAnimPos.Set(0.1f, beginValue, beginValue + new Vector3(Random.Range(-1f, 1f) * addValue.x, Random.Range(-1f, 1f) * addValue.y, 0f));
			shakeAnimPos.Play();
		}
		nowValue = shakeAnimPos.Update();
	}
}
