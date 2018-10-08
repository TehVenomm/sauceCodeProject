using UnityEngine;

public class SlimeScaleAnim : SlimeAnimBase<Vector3>
{
	public override Vector3 UpdateAnim()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		float num = nowTime / playTime;
		float num2 = animCurve.Evaluate(num);
		if (isBlend && nowTime <= blendEndTime)
		{
			float num3 = blendCurve.Evaluate(num);
			num2 = blendParam.x + (num2 - blendParam.x) * num3;
		}
		return new Vector3(num2, num2, num2);
	}
}
