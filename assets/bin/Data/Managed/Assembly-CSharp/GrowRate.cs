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
		return rate.value == growRate.rate.value && add.value == growRate.add.value;
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
