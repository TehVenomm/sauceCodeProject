using System.Collections.Generic;

public class XMLNode
{
	public XMLNode parent;

	public string tag = string.Empty;

	public List<XMLNode> children = new List<XMLNode>();

	public string content = string.Empty;

	public Dictionary<string, string> attributes = new Dictionary<string, string>();

	public XMLNode(string tagArg)
	{
		tag = tagArg;
	}

	public string Serialize(bool newlines, int spacesNumber)
	{
		string str = string.Empty;
		string text = string.Empty;
		string empty = string.Empty;
		if (newlines)
		{
			empty += "  ";
			for (int i = 0; i < spacesNumber; i++)
			{
				text += " ";
			}
			str = "\n";
		}
		string text2 = text + "<" + tag;
		string text3;
		foreach (string key in attributes.Keys)
		{
			text3 = text2;
			text2 = text3 + " " + key + "=\"" + attributes[key] + "\"";
		}
		text2 += ">";
		foreach (XMLNode child in children)
		{
			text2 = text2 + str + child.Serialize(newlines, spacesNumber + 2);
		}
		if (content != string.Empty)
		{
			text2 += content;
		}
		text2 += str;
		text3 = text2;
		return text3 + text + "</" + tag + ">";
	}

	public void AddChild(XMLNode child)
	{
		child.parent = this;
		children.Add(child);
	}
}
