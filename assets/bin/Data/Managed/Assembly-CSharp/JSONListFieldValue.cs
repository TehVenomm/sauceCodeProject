using System.Collections.Generic;

public class JSONListFieldValue : IJSONFieldValue
{
	public List<IJSONFieldValue> value;

	public JSONListFieldValue()
	{
		value = new List<IJSONFieldValue>();
	}

	public JSONListFieldValue(List<IJSONFieldValue> val)
	{
		value = val;
	}

	public string Serialize()
	{
		string str = "[";
		if (value.Count > 0)
		{
			str += value[0].Serialize();
		}
		for (int i = 1; i < value.Count; i++)
		{
			str = str + "," + value[i].Serialize();
		}
		return str + "]";
	}
}
