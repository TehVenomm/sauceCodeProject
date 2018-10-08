using UnityEngine;

public class SlimePosAnim : SlimeAnimBase<Vector3>
{
	public override Vector3 UpdateAnim()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		float num = nowTime / playTime;
		float num2 = animCurve.Evaluate(num);
		if (isBlend && nowTime <= blendEndTime)
		{
			float num3 = blendCurve.Evaluate(num);
			num2 = blendParam.y + (num2 - blendParam.y) * num3;
		}
		return new Vector3(0f, num2, 0f);
	}
}
