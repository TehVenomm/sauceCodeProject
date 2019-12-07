public class GrowRate
{
	public XorInt rate = 0;

	public XorInt add = 0;

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		GrowRate growRate = obj as GrowRate;
		if (obj == null)
		{
			return false;
		}
		if (rate.value == growRate.rate.value)
		{
			return add.value == growRate.add.value;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override string ToString()
	{
		return "rate:" + rate + ", add:" + add;
	}
}
