using System.Collections.Generic;

public class XMLNode
{
	public XMLNode parent;

	public string tag = "";

	public List<XMLNode> children = new List<XMLNode>();

	public string content = "";

	public Dictionary<string, string> attributes = new Dictionary<string, string>();

	public XMLNode(string tagArg)
	{
		tag = tagArg;
	}

	public string Serialize(bool newlines, int spacesNumber)
	{
		string str = "";
		string text = "";
		string str2 = "";
		if (newlines)
		{
			str2 += "  ";
			for (int i = 0; i < spacesNumber; i++)
			{
				text += " ";
			}
			str = "\n";
		}
		string text2 = text + "<" + tag;
		foreach (string key in attributes.Keys)
		{
			text2 = text2 + " " + key + "=\"" + attributes[key] + "\"";
		}
		text2 += ">";
		foreach (XMLNode child in children)
		{
			text2 = text2 + str + child.Serialize(newlines, spacesNumber + 2);
		}
		if (content != "")
		{
			text2 += content;
		}
		text2 += str;
		return text2 + text + "</" + tag + ">";
	}

	public void AddChild(XMLNode child)
	{
		child.parent = this;
		children.Add(child);
	}
}
