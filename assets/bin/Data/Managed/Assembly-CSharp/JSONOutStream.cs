using System.Collections.Generic;
using UnityEngine;

public class JSONOutStream
{
	public JSONNode node = new JSONNode();

	public string Serialize()
	{
		return node.Serialize();
	}

	public JSONOutStream Content(string tag, string value)
	{
		node.AddField(tag, new JSONStringFieldValue(value));
		return this;
	}

	public JSONOutStream Content(int idx, string value)
	{
		node.AddField(idx, new JSONStringFieldValue(value));
		return this;
	}

	public JSONOutStream Content(string value)
	{
		node.AddField(new JSONStringFieldValue(value));
		return this;
	}

	public JSONOutStream Content(string tag, double value)
	{
		node.AddField(tag, new JSONNumberFieldValue(value));
		return this;
	}

	public JSONOutStream Content(int idx, double value)
	{
		node.AddField(idx, new JSONNumberFieldValue(value));
		return this;
	}

	public JSONOutStream Content(double value)
	{
		node.AddField(new JSONNumberFieldValue(value));
		return this;
	}

	public JSONOutStream Content(string tag, bool value)
	{
		node.AddField(tag, new JSONBooleanFieldValue(value));
		return this;
	}

	public JSONOutStream Content(int idx, bool value)
	{
		node.AddField(idx, new JSONBooleanFieldValue(value));
		return this;
	}

	public JSONOutStream Content(bool value)
	{
		node.AddField(new JSONBooleanFieldValue(value));
		return this;
	}

	public JSONOutStream Content(string tag, int value)
	{
		node.AddField(tag, new JSONNumberFieldValue((double)value));
		return this;
	}

	public JSONOutStream Content(int idx, int value)
	{
		node.AddField(idx, new JSONNumberFieldValue((double)value));
		return this;
	}

	public JSONOutStream Content(int value)
	{
		node.AddField(new JSONNumberFieldValue((double)value));
		return this;
	}

	public JSONOutStream Content(string tag, XorInt value)
	{
		node.AddField(tag, new JSONNumberFieldValue((double)(int)value));
		return this;
	}

	public JSONOutStream Content(int idx, XorInt value)
	{
		node.AddField(idx, new JSONNumberFieldValue((double)(int)value));
		return this;
	}

	public JSONOutStream Content(XorInt value)
	{
		node.AddField(new JSONNumberFieldValue((double)(int)value));
		return this;
	}

	public JSONOutStream Content(string tag, XorUInt value)
	{
		node.AddField(tag, new JSONNumberFieldValue((double)(uint)value));
		return this;
	}

	public JSONOutStream Content(int idx, XorUInt value)
	{
		node.AddField(idx, new JSONNumberFieldValue((double)(uint)value));
		return this;
	}

	public JSONOutStream Content(XorUInt value)
	{
		node.AddField(new JSONNumberFieldValue((double)(uint)value));
		return this;
	}

	public JSONOutStream Content(string tag, XorFloat value)
	{
		node.AddField(tag, new JSONNumberFieldValue((double)(float)value));
		return this;
	}

	public JSONOutStream Content(int idx, XorFloat value)
	{
		node.AddField(idx, new JSONNumberFieldValue((double)(float)value));
		return this;
	}

	public JSONOutStream Content(XorFloat value)
	{
		node.AddField(new JSONNumberFieldValue((double)(float)value));
		return this;
	}

	public JSONOutStream Content(string tag, Vector2 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(int idx, Vector2 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(Vector2 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(string tag, Vector3 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		list.Add(new JSONNumberFieldValue((double)value.z));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(int idx, Vector3 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		list.Add(new JSONNumberFieldValue((double)value.z));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(Vector3 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		list.Add(new JSONNumberFieldValue((double)value.z));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(string tag, Vector4 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		list.Add(new JSONNumberFieldValue((double)value.z));
		list.Add(new JSONNumberFieldValue((double)value.w));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(int idx, Vector4 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		list.Add(new JSONNumberFieldValue((double)value.z));
		list.Add(new JSONNumberFieldValue((double)value.w));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(Vector4 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		list.Add(new JSONNumberFieldValue((double)value.z));
		list.Add(new JSONNumberFieldValue((double)value.w));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(string tag, Quaternion value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		list.Add(new JSONNumberFieldValue((double)value.z));
		list.Add(new JSONNumberFieldValue((double)value.w));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(int idx, Quaternion value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		list.Add(new JSONNumberFieldValue((double)value.z));
		list.Add(new JSONNumberFieldValue((double)value.w));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(Quaternion value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.x));
		list.Add(new JSONNumberFieldValue((double)value.y));
		list.Add(new JSONNumberFieldValue((double)value.z));
		list.Add(new JSONNumberFieldValue((double)value.w));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(string tag, Color value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.r));
		list.Add(new JSONNumberFieldValue((double)value.g));
		list.Add(new JSONNumberFieldValue((double)value.b));
		list.Add(new JSONNumberFieldValue((double)value.a));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(int idx, Color value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.r));
		list.Add(new JSONNumberFieldValue((double)value.g));
		list.Add(new JSONNumberFieldValue((double)value.b));
		list.Add(new JSONNumberFieldValue((double)value.a));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(Color value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.r));
		list.Add(new JSONNumberFieldValue((double)value.g));
		list.Add(new JSONNumberFieldValue((double)value.b));
		list.Add(new JSONNumberFieldValue((double)value.a));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(string tag, Rect value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.get_x()));
		list.Add(new JSONNumberFieldValue((double)value.get_y()));
		list.Add(new JSONNumberFieldValue((double)value.get_width()));
		list.Add(new JSONNumberFieldValue((double)value.get_height()));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(int idx, Rect value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.get_x()));
		list.Add(new JSONNumberFieldValue((double)value.get_y()));
		list.Add(new JSONNumberFieldValue((double)value.get_width()));
		list.Add(new JSONNumberFieldValue((double)value.get_height()));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream Content(Rect value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue((double)value.get_x()));
		list.Add(new JSONNumberFieldValue((double)value.get_y()));
		list.Add(new JSONNumberFieldValue((double)value.get_width()));
		list.Add(new JSONNumberFieldValue((double)value.get_height()));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	public JSONOutStream List(string tag)
	{
		JSONNode jSONNode = new JSONNode(node);
		jSONNode.isList = true;
		jSONNode.listName = tag;
		node = jSONNode;
		return this;
	}

	public JSONOutStream List()
	{
		JSONNode jSONNode = new JSONNode(node);
		jSONNode.isList = true;
		jSONNode.listName = null;
		node = jSONNode;
		return this;
	}

	public JSONOutStream Start(string tag)
	{
		JSONNode val = new JSONNode(node);
		node.AddField(tag, new JSONObjectFieldValue(val));
		node = val;
		return this;
	}

	public JSONOutStream Start(int idx)
	{
		JSONNode val = new JSONNode(node);
		node.AddField(idx, new JSONObjectFieldValue(val));
		node = val;
		return this;
	}

	public JSONOutStream Start()
	{
		JSONNode val = new JSONNode(node);
		node.AddField(new JSONObjectFieldValue(val));
		node = val;
		return this;
	}

	public JSONOutStream End()
	{
		if (node.parent != null)
		{
			if (node.isList && node.listName != null)
			{
				node.parent.AddField(node.listName, node.GetListFieldValue());
			}
			else if (node.isList)
			{
				node.parent.AddField(node.GetListFieldValue());
			}
			node = node.parent;
		}
		return this;
	}
}
