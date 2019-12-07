using UnityEngine;

public class SlimeColorAnim : SlimeAnimBase<Color>
{
	public override Color UpdateAnim()
	{
		if (base.isPlaying)
		{
			float time = nowTime / playTime;
			float num = animCurve.Evaluate(time);
			if (isBlend && nowTime <= blendEndTime)
			{
				float num2 = blendCurve.Evaluate(time);
				num = blendParam.a + (num - blendParam.a) * num2;
			}
			return new Color(1f, 1f, 1f, num);
		}
		return new Color(1f, 1f, 1f, 0f);
	}
}
