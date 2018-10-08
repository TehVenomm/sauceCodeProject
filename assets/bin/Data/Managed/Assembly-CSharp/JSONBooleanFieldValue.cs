public class JSONBooleanFieldValue : IJSONFieldValue
{
	public bool value;

	public JSONBooleanFieldValue(bool val)
	{
		value = val;
	}

	public string Serialize()
	{
		return value.ToString().ToLower();
	}
}
