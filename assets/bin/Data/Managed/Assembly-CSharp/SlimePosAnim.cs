using UnityEngine;

public class SlimePosAnim : SlimeAnimBase<Vector3>
{
	public override Vector3 UpdateAnim()
	{
		float time = nowTime / playTime;
		float num = animCurve.Evaluate(time);
		if (isBlend && nowTime <= blendEndTime)
		{
			float num2 = blendCurve.Evaluate(time);
			num = blendParam.y + (num - blendParam.y) * num2;
		}
		return new Vector3(0f, num, 0f);
	}
}
