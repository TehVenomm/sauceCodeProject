public class JSONObjectFieldValue : IJSONFieldValue
{
	public JSONNode value;

	public JSONObjectFieldValue(JSONNode val)
	{
		value = val;
	}

	public string Serialize()
	{
		return value.Serialize();
	}
}
