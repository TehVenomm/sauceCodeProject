using UnityEngine;

public class XMLOutStream
{
	public XMLNode current;

	public string prolog;

	public XMLOutStream Prolog(string encoding)
	{
		prolog = "<?xml version=\"1.0\" encoding=\"" + encoding + "\"?>";
		return this;
	}

	public string Serialize()
	{
		return Serialize(newlines: false);
	}

	public string Serialize(bool newlines)
	{
		string str = string.Empty;
		if (newlines)
		{
			str = "\n";
		}
		return prolog + str + current.Serialize(newlines, 0);
	}

	public XMLOutStream Start(string tag)
	{
		if (current == null)
		{
			current = new XMLNode(tag);
		}
		else
		{
			XMLNode child = new XMLNode(tag);
			current.AddChild(child);
			current = child;
		}
		return this;
	}

	public XMLOutStream End()
	{
		if (current.parent != null)
		{
			current = current.parent;
		}
		return this;
	}

	public XMLOutStream Content(string value)
	{
		current.content = value;
		return this;
	}

	public XMLOutStream Content(float value)
	{
		current.content = value.ToString();
		return this;
	}

	public XMLOutStream Content(int value)
	{
		current.content = value.ToString();
		return this;
	}

	public XMLOutStream Content(bool value)
	{
		current.content = value.ToString();
		return this;
	}

	public XMLOutStream Content(string tag, string value)
	{
		return Start(tag).Content(value).End();
	}

	public XMLOutStream Content(string tag, bool value)
	{
		return Start(tag).Content(value).End();
	}

	public XMLOutStream Content(string tag, int value)
	{
		return Start(tag).Content(value).End();
	}

	public XMLOutStream Content(string tag, float value)
	{
		return Start(tag).Content(value).End();
	}

	public XMLOutStream Content(Vector2 value)
	{
		Content("[" + value.x + "," + value.y + "]");
		return this;
	}

	public XMLOutStream Content(string tag, Vector2 value)
	{
		Start(tag).Attribute("x", value.x).Attribute("y", value.y).End();
		return this;
	}

	public XMLOutStream Content(Vector3 value)
	{
		Content("[" + value.x + "," + value.y + "," + value.z + "]");
		return this;
	}

	public XMLOutStream Content(string tag, Vector3 value)
	{
		Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("z", value.z)
			.End();
		return this;
	}

	public XMLOutStream Content(Vector4 value)
	{
		Content("[" + value.x + "," + value.y + "," + value.z + "," + value.w + "]");
		return this;
	}

	public XMLOutStream Content(string tag, Vector4 value)
	{
		Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("z", value.z)
			.Attribute("w", value.w)
			.End();
		return this;
	}

	public XMLOutStream Content(Quaternion value)
	{
		Content("[" + value.x + "," + value.y + "," + value.z + "," + value.w + "]");
		return this;
	}

	public XMLOutStream Content(string tag, Quaternion value)
	{
		Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("z", value.z)
			.Attribute("w", value.w)
			.End();
		return this;
	}

	public XMLOutStream Content(Color value)
	{
		Content("[" + value.r + "," + value.g + "," + value.b + "," + value.a + "]");
		return this;
	}

	public XMLOutStream Content(string tag, Color value)
	{
		Start(tag).Attribute("r", value.r).Attribute("g", value.g).Attribute("b", value.b)
			.Attribute("a", value.a)
			.End();
		return this;
	}

	public XMLOutStream Content(Rect value)
	{
		Content("[" + value.get_x() + "," + value.get_y() + "," + value.get_width() + "," + value.get_height() + "]");
		return this;
	}

	public XMLOutStream Content(string tag, Rect value)
	{
		Start(tag).Attribute("x", value.get_x()).Attribute("y", value.get_y()).Attribute("width", value.get_width())
			.Attribute("height", value.get_height())
			.End();
		return this;
	}

	public XMLOutStream Attribute(string name, string value)
	{
		current.attributes[name] = value;
		return this;
	}

	public XMLOutStream Attribute(string name, int value)
	{
		current.attributes[name] = value.ToString();
		return this;
	}

	public XMLOutStream Attribute(string name, float value)
	{
		current.attributes[name] = value.ToString();
		return this;
	}

	public XMLOutStream Attribute(string name, bool value)
	{
		current.attributes[name] = value.ToString();
		return this;
	}
}
