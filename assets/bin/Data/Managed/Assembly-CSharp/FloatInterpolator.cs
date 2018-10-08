public class FloatInterpolator : InterpolatorBase<float>
{
	protected override void Calc(float t, float r)
	{
		float num = (endValue - beginValue) * r + beginValue;
		if (addCurve != null && addCurve.get_length() > 0)
		{
			num = addValue * addCurve.Evaluate(t) + num;
		}
		nowValue = num;
	}
}
