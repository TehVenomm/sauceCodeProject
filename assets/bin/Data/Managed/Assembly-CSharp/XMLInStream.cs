using System;
using System.Collections.Generic;
using UnityEngine;

public class XMLInStream
{
	public XMLNode current;

	public string Tag => current.tag;

	public List<XMLInStream> Children
	{
		get
		{
			List<XMLInStream> list = new List<XMLInStream>();
			foreach (XMLNode child in current.children)
			{
				list.Add(new XMLInStream(child));
			}
			return list;
		}
	}

	public XMLInStream(XMLNode node)
	{
		current = node;
	}

	public XMLInStream(string input)
	{
		XMLParser xMLParser = new XMLParser();
		current = xMLParser.Parse(new FlashCompatibleTextReader(input));
	}

	public XMLInStream Clone()
	{
		return new XMLInStream(current);
	}

	public bool Has(string tag)
	{
		foreach (XMLNode child in current.children)
		{
			if (child.tag == tag)
			{
				return true;
			}
		}
		return false;
	}

	public int NumberChildren(string tag)
	{
		int num = 0;
		foreach (XMLNode child in current.children)
		{
			if (child.tag == tag)
			{
				num++;
			}
		}
		return num;
	}

	public bool HasAttribute(string tag)
	{
		return current.attributes.ContainsKey(tag);
	}

	public XMLInStream Start(string tag)
	{
		foreach (XMLNode child in current.children)
		{
			if (child.tag == tag)
			{
				current = child;
				return this;
			}
		}
		Debug.LogError((object)("No child node named: " + tag + " in node " + current.tag));
		return null;
	}

	public XMLInStream End()
	{
		if (current.parent == null)
		{
			Debug.LogError((object)("No parent node for tag: " + current.tag));
			return null;
		}
		current = current.parent;
		return this;
	}

	public XMLInStream Content(string tag, out string value)
	{
		return Start(tag).Content(out value).End();
	}

	public XMLInStream Content(out string value)
	{
		value = current.content;
		return this;
	}

	public XMLInStream Content(string tag, out bool value)
	{
		return Start(tag).Content(out value).End();
	}

	public XMLInStream Content(out bool value)
	{
		value = FlashCompatibleConvert.ToBoolean(current.content);
		return this;
	}

	public XMLInStream Content(string tag, out int value)
	{
		return Start(tag).Content(out value).End();
	}

	public XMLInStream Content(out int value)
	{
		value = FlashCompatibleConvert.ToInt32(current.content);
		return this;
	}

	public XMLInStream Content(string tag, out float value)
	{
		return Start(tag).Content(out value).End();
	}

	public XMLInStream Content(out float value)
	{
		value = (float)GetDouble(current.content);
		return this;
	}

	public XMLInStream Content(string tag, out Color value)
	{
		if (tag != null)
		{
			Start(tag);
		}
		float value2;
		float value3;
		float value4;
		float value5;
		if (HasAttribute("r"))
		{
			Attribute("r", out value2).Attribute("g", out value3).Attribute("b", out value4).Attribute("a", out value5)
				.End();
		}
		else
		{
			string value6;
			if (tag != null)
			{
				End();
				Content(tag, out value6);
			}
			else
			{
				Content(out value6);
			}
			value6 = value6.Replace("[", string.Empty).Replace("]", string.Empty);
			string[] array = value6.Split(',');
			value2 = (float)GetDouble(array[0]);
			value3 = (float)GetDouble(array[1]);
			value4 = (float)GetDouble(array[2]);
			value5 = (float)GetDouble(array[3]);
		}
		value._002Ector(value2, value3, value4, value5);
		return this;
	}

	public XMLInStream Content(out Color value)
	{
		return Content(null, out value);
	}

	public XMLInStream Content(string tag, out Vector2 value)
	{
		if (tag != null)
		{
			Start(tag);
		}
		float value2;
		float value3;
		if (HasAttribute("x"))
		{
			Attribute("x", out value2).Attribute("y", out value3).End();
		}
		else
		{
			string value4;
			if (tag != null)
			{
				End();
				Content(tag, out value4);
			}
			else
			{
				Content(out value4);
			}
			value4 = value4.Replace("[", string.Empty).Replace("]", string.Empty).Replace(" ", string.Empty);
			string[] array = value4.Split(',');
			value2 = (float)GetDouble(array[0]);
			value3 = (float)GetDouble(array[1]);
		}
		value._002Ector(value2, value3);
		return this;
	}

	public XMLInStream Content(out Vector2 value)
	{
		return Content(null, out value);
	}

	public XMLInStream Content(string tag, out Vector3 value)
	{
		if (tag != null)
		{
			Start(tag);
		}
		float value2;
		float value3;
		float value4;
		if (HasAttribute("x"))
		{
			Attribute("x", out value2).Attribute("y", out value3).Attribute("z", out value4).End();
		}
		else
		{
			string value5;
			if (tag != null)
			{
				End();
				Content(tag, out value5);
			}
			else
			{
				Content(out value5);
			}
			value5 = value5.Replace("[", string.Empty).Replace("]", string.Empty).Replace(" ", string.Empty);
			string[] array = value5.Split(',');
			value2 = (float)GetDouble(array[0]);
			value3 = (float)GetDouble(array[1]);
			value4 = (float)GetDouble(array[2]);
		}
		value._002Ector(value2, value3, value4);
		return this;
	}

	public XMLInStream Content(out Vector3 value)
	{
		return Content(null, out value);
	}

	public XMLInStream Content(string tag, out Quaternion value)
	{
		if (tag != null)
		{
			Start(tag);
		}
		float value2;
		float value3;
		float value4;
		float value5;
		if (HasAttribute("x"))
		{
			Attribute("x", out value2).Attribute("y", out value3).Attribute("z", out value4).Attribute("w", out value5)
				.End();
		}
		else
		{
			string value6;
			if (tag != null)
			{
				End();
				Content(tag, out value6);
			}
			else
			{
				Content(out value6);
			}
			value6 = value6.Replace("[", string.Empty).Replace("]", string.Empty);
			string[] array = value6.Split(',');
			value2 = (float)GetDouble(array[0]);
			value3 = (float)GetDouble(array[1]);
			value4 = (float)GetDouble(array[2]);
			value5 = (float)GetDouble(array[3]);
		}
		value._002Ector(value2, value3, value4, value5);
		return this;
	}

	public XMLInStream Content(out Quaternion value)
	{
		return Content(null, out value);
	}

	public XMLInStream Content(string tag, out Vector4 value)
	{
		if (tag != null)
		{
			Start(tag);
		}
		float value2;
		float value3;
		float value4;
		float value5;
		if (HasAttribute("x"))
		{
			Attribute("x", out value2).Attribute("y", out value3).Attribute("z", out value4).Attribute("w", out value5)
				.End();
		}
		else
		{
			string value6;
			if (tag != null)
			{
				End();
				Content(tag, out value6);
			}
			else
			{
				Content(out value6);
			}
			value6 = value6.Replace("[", string.Empty).Replace("]", string.Empty);
			string[] array = value6.Split(',');
			value2 = (float)GetDouble(array[0]);
			value3 = (float)GetDouble(array[1]);
			value4 = (float)GetDouble(array[2]);
			value5 = (float)GetDouble(array[3]);
		}
		value._002Ector(value2, value3, value4, value5);
		return this;
	}

	public XMLInStream Content(out Vector4 value)
	{
		return Content(null, out value);
	}

	public XMLInStream Content(string tag, out Rect value)
	{
		if (tag != null)
		{
			Start(tag);
		}
		float value2;
		float value3;
		float value4;
		float value5;
		if (HasAttribute("x"))
		{
			Attribute("x", out value2).Attribute("y", out value3).Attribute("width", out value4).Attribute("height", out value5)
				.End();
		}
		else
		{
			string value6;
			if (tag != null)
			{
				End();
				Content(tag, out value6);
			}
			else
			{
				Content(out value6);
			}
			value6 = value6.Replace("[", string.Empty).Replace("]", string.Empty);
			string[] array = value6.Split(',');
			value2 = (float)GetDouble(array[0]);
			value3 = (float)GetDouble(array[1]);
			value4 = (float)GetDouble(array[2]);
			value5 = (float)GetDouble(array[3]);
		}
		value._002Ector(value2, value3, value4, value5);
		return this;
	}

	public XMLInStream Content(out Rect value)
	{
		return Content(null, out value);
	}

	public XMLInStream ContentOptional(string tag, ref string value)
	{
		if (Has(tag))
		{
			return Content(tag, out value);
		}
		return this;
	}

	public XMLInStream ContentOptional(string tag, ref bool value)
	{
		if (Has(tag))
		{
			return Content(tag, out value);
		}
		return this;
	}

	public XMLInStream ContentOptional(string tag, ref int value)
	{
		if (Has(tag))
		{
			return Content(tag, out value);
		}
		return this;
	}

	public XMLInStream ContentOptional(string tag, ref float value)
	{
		if (Has(tag))
		{
			return Content(tag, out value);
		}
		return this;
	}

	public XMLInStream ContentOptional(string tag, ref Vector2 value)
	{
		if (Has(tag))
		{
			return Content(tag, out value);
		}
		return this;
	}

	public XMLInStream ContentOptional(string tag, ref Vector3 value)
	{
		if (Has(tag))
		{
			return Content(tag, out value);
		}
		return this;
	}

	public XMLInStream ContentOptional(string tag, ref Quaternion value)
	{
		if (Has(tag))
		{
			return Content(tag, out value);
		}
		return this;
	}

	public XMLInStream ContentOptional(string tag, ref Color value)
	{
		if (Has(tag))
		{
			return Content(tag, out value);
		}
		return this;
	}

	public XMLInStream ContentOptional(string tag, ref Rect value)
	{
		if (Has(tag))
		{
			return Content(tag, out value);
		}
		return this;
	}

	public XMLInStream AttributeOptional(string name, ref string value)
	{
		if (current.attributes.ContainsKey(name))
		{
			value = current.attributes[name];
		}
		return this;
	}

	public XMLInStream AttributeOptional(string name, ref bool value)
	{
		if (current.attributes.ContainsKey(name))
		{
			value = FlashCompatibleConvert.ToBoolean(GetAttribute(name));
		}
		return this;
	}

	public XMLInStream AttributeOptional(string name, ref int value)
	{
		if (current.attributes.ContainsKey(name))
		{
			value = FlashCompatibleConvert.ToInt32(GetAttribute(name));
		}
		return this;
	}

	public XMLInStream AttributeOptional(string name, ref float value)
	{
		if (current.attributes.ContainsKey(name))
		{
			value = (float)GetDouble(GetAttribute(name));
		}
		return this;
	}

	public XMLInStream Attribute(string name, out string value)
	{
		value = GetAttribute(name);
		return this;
	}

	public XMLInStream Attribute(string name, out int value)
	{
		value = FlashCompatibleConvert.ToInt32(GetAttribute(name));
		return this;
	}

	public XMLInStream Attribute(string name, out float value)
	{
		value = (float)GetDouble(GetAttribute(name));
		return this;
	}

	public XMLInStream Attribute(string name, out bool value)
	{
		value = FlashCompatibleConvert.ToBoolean(GetAttribute(name));
		return this;
	}

	private string GetAttribute(string name)
	{
		if (current.attributes.ContainsKey(name))
		{
			return current.attributes[name];
		}
		Debug.LogError((object)("Attribute " + name + " don't exist in node " + current.tag));
		return null;
	}

	public XMLInStream List(string tag, Action<XMLInStream> callback)
	{
		foreach (XMLNode child in current.children)
		{
			if (child.tag == tag)
			{
				callback(new XMLInStream(child));
			}
		}
		return this;
	}

	private double GetDouble(string val)
	{
		try
		{
			return FlashCompatibleConvert.ToDouble(val);
		}
		catch
		{
			if (val.Contains("."))
			{
				return FlashCompatibleConvert.ToDouble(val.Replace('.', ','));
			}
			return FlashCompatibleConvert.ToDouble(val.Replace(',', '.'));
		}
	}
}
