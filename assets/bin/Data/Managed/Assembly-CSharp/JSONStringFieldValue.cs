public class JSONStringFieldValue : IJSONFieldValue
{
	public string value;

	public JSONStringFieldValue(string val)
	{
		value = val;
	}

	public string Serialize()
	{
		return "\"" + value + "\"";
	}
}
