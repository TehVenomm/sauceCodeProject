public class JSONField
{
	public string name;

	public IJSONFieldValue value;

	public JSONField(string n, IJSONFieldValue val)
	{
		name = n;
		value = val;
	}
}
