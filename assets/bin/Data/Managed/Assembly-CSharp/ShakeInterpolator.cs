using System;
using UnityEngine;

[Serializable]
public class ShakeInterpolator : InterpolatorBase<Vector3>
{
	private Vector3Interpolator shakeAnimPos = new Vector3Interpolator();

	protected override void Calc(float t, float r)
	{
		if (!shakeAnimPos.IsPlaying())
		{
			shakeAnimPos.Set(0.1f, beginValue, beginValue + new Vector3(UnityEngine.Random.Range(-1f, 1f) * addValue.x, UnityEngine.Random.Range(-1f, 1f) * addValue.y, 0f));
			shakeAnimPos.Play();
		}
		nowValue = shakeAnimPos.Update();
	}
}
