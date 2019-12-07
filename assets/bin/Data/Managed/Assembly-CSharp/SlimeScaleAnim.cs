using UnityEngine;

public class SlimeScaleAnim : SlimeAnimBase<Vector3>
{
	public override Vector3 UpdateAnim()
	{
		float time = nowTime / playTime;
		float num = animCurve.Evaluate(time);
		if (isBlend && nowTime <= blendEndTime)
		{
			float num2 = blendCurve.Evaluate(time);
			num = blendParam.x + (num - blendParam.x) * num2;
		}
		return new Vector3(num, num, num);
	}
}
