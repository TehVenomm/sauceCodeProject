public class JSONNumberFieldValue : IJSONFieldValue
{
	public double value;

	public JSONNumberFieldValue(double val)
	{
		value = val;
	}

	public string Serialize()
	{
		return value.ToString();
	}
}
