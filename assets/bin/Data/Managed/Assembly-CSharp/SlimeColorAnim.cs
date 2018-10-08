using UnityEngine;

public class SlimeColorAnim : SlimeAnimBase<Color>
{
	public override Color UpdateAnim()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (base.isPlaying)
		{
			float num = nowTime / playTime;
			float num2 = animCurve.Evaluate(num);
			if (isBlend && nowTime <= blendEndTime)
			{
				float num3 = blendCurve.Evaluate(num);
				num2 = blendParam.a + (num2 - blendParam.a) * num3;
			}
			return new Color(1f, 1f, 1f, num2);
		}
		return new Color(1f, 1f, 1f, 0f);
	}
}
