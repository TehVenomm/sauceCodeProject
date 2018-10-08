using System.Collections.Generic;

public class JSONNode : IJSONFieldValue
{
	public JSONNode parent;

	public List<JSONField> fields_ = new List<JSONField>();

	public bool isList;

	public string listName = string.Empty;

	public JSONNode()
	{
	}

	public JSONNode(JSONNode parent)
	{
		this.parent = parent;
	}

	public JSONNode(IJSONFieldValue val)
	{
		fields_.Add(new JSONField("0", val));
	}

	public JSONNode(List<IJSONFieldValue> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			fields_.Add(new JSONField(i.ToString(), list[i]));
		}
	}

	public void AddField(string fieldName, IJSONFieldValue val)
	{
		fields_.Add(new JSONField(fieldName, val));
	}

	public void AddField(int idx, IJSONFieldValue val)
	{
		fields_.Add(new JSONField(idx.ToString(), val));
	}

	public void AddField(IJSONFieldValue val)
	{
		fields_.Add(new JSONField(null, val));
	}

	public IJSONFieldValue GetField(string name)
	{
		foreach (JSONField item in fields_)
		{
			if (item.name == name)
			{
				return item.value;
			}
		}
		return null;
	}

	public IJSONFieldValue GetField(int index)
	{
		return fields_[index].value;
	}

	public int GetFieldCount()
	{
		return fields_.Count;
	}

	public JSONListFieldValue GetListFieldValue()
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		for (int i = 0; i < fields_.Count; i++)
		{
			list.Add(fields_[i].value);
		}
		return new JSONListFieldValue(list);
	}

	public string Serialize()
	{
		if (fields_.Count == 1 && (fields_[0].name == string.Empty || fields_[0].name == null))
		{
			return fields_[0].value.Serialize();
		}
		string text = "{";
		if (fields_.Count > 0)
		{
			string text2 = text;
			text = text2 + "\"" + fields_[0].name + "\":" + fields_[0].value.Serialize();
		}
		for (int i = 1; i < fields_.Count; i++)
		{
			string text2 = text;
			text = text2 + ",\"" + fields_[i].name + "\":" + fields_[i].value.Serialize();
		}
		return text + "}";
	}
}
