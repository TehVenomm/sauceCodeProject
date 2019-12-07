using System.Collections;
using UnityEngine;

public class XMLParser
{
	private FlashCompatibleTextReader reader;

	private Stack elements;

	private XMLNode currentElement;

	public XMLParser()
	{
		elements = new Stack();
		currentElement = null;
	}

	public XMLNode Parse(FlashCompatibleTextReader reader)
	{
		this.reader = reader;
		if (reader.Peek() == -1)
		{
			return null;
		}
		SkipPrologs();
		if (reader.Peek() == -1)
		{
			return null;
		}
		XMLNode xMLNode;
		while (true)
		{
			SkipWhitespace();
			bool startingBracket = (ushort)reader.Peek() == 60;
			string text = ReadTag(startingBracket).Trim();
			if (text.StartsWith("<!"))
			{
				continue;
			}
			string text2;
			if (text.StartsWith("</"))
			{
				text2 = text.Substring(2, text.Length - 3);
				if (currentElement == null)
				{
					Debug.LogError("Got close tag '" + text2 + "' without open tag.");
					return null;
				}
				if (text2 != currentElement.tag)
				{
					Debug.LogError("Expected close tag for '" + currentElement.tag + "' but got '" + text2 + "'.");
					return null;
				}
				if (elements.Count == 0)
				{
					return currentElement;
				}
				currentElement = (XMLNode)elements.Pop();
				continue;
			}
			int num = text.IndexOf('"');
			if (num < 0)
			{
				num = text.IndexOf('\'');
			}
			if (num < 0)
			{
				if (text.EndsWith("/>"))
				{
					text2 = text.Substring(1, text.Length - 3).Trim();
					text = "/>";
				}
				else
				{
					text2 = text.Substring(1, text.Length - 2).Trim();
					text = "";
				}
			}
			else
			{
				int num2 = text.IndexOf(" ");
				text2 = text.Substring(1, num2).Trim();
				text = text.Substring(num2 + 1);
			}
			xMLNode = new XMLNode(text2);
			bool flag = false;
			while (text.Length > 0)
			{
				text = text.Trim();
				if (text == "/>")
				{
					flag = true;
					break;
				}
				if (text == ">")
				{
					break;
				}
				num = text.IndexOf("=");
				if (num < 0)
				{
					Debug.LogError("Invalid attribute for tag '" + text2 + "'.");
					return null;
				}
				string key = text.Substring(0, num);
				text = text.Substring(num + 1);
				bool flag2 = true;
				if (text.StartsWith("\""))
				{
					num = text.IndexOf('"', 1);
				}
				else if (text.StartsWith("'"))
				{
					num = text.IndexOf('\'', 1);
				}
				else
				{
					flag2 = false;
					num = text.IndexOf(' ');
					if (num < 0)
					{
						num = text.IndexOf('>');
						if (num < 0)
						{
							num = text.IndexOf('/');
						}
					}
				}
				if (num < 0)
				{
					Debug.LogError("Invalid attribute for tag '" + text2 + "'.");
					return null;
				}
				string value = (!flag2) ? text.Substring(0, num - 1) : text.Substring(1, num - 1);
				xMLNode.attributes[key] = value;
				text = text.Substring(num + 1);
			}
			if (!flag)
			{
				xMLNode.content = ReadText();
			}
			if (currentElement != null)
			{
				currentElement.AddChild(xMLNode);
			}
			if (!flag)
			{
				if (currentElement != null)
				{
					elements.Push(currentElement);
				}
				currentElement = xMLNode;
			}
			else if (currentElement == null)
			{
				break;
			}
		}
		return xMLNode;
	}

	private void SkipWhitespace()
	{
		while (true)
		{
			int num = reader.Peek();
			if (num == -1 || !char.IsWhiteSpace((char)num))
			{
				break;
			}
			reader.Read();
		}
	}

	private void SkipProlog()
	{
		reader.Read();
		while (true)
		{
			switch (reader.Peek())
			{
			case 62:
				reader.Read();
				return;
			case 60:
				SkipProlog();
				break;
			default:
				reader.Read();
				break;
			}
		}
	}

	private void SkipPrologs()
	{
		int num;
		while (true)
		{
			SkipWhitespace();
			num = reader.Read();
			if (num == -1)
			{
				return;
			}
			if ((ushort)num != 60)
			{
				break;
			}
			int num2 = reader.Peek();
			if (num != -1 && ((ushort)num2 == 63 || (ushort)num2 == 33))
			{
				SkipProlog();
				continue;
			}
			return;
		}
		Debug.LogError("Expected '<' but got '" + num + "'.");
	}

	private string ReadTag(bool startingBracket)
	{
		SkipWhitespace();
		string str = "";
		char c = (char)reader.Peek();
		if (startingBracket && c != '<')
		{
			Debug.LogError("Expected < but got " + c.ToString());
			return null;
		}
		if (!startingBracket)
		{
			str += "<";
		}
		str += ((char)reader.Read()).ToString();
		while (reader.Peek() != 62)
		{
			str += ((char)reader.Read()).ToString();
		}
		return str + ((char)reader.Read()).ToString();
	}

	private string ReadText()
	{
		string text = "";
		while (true)
		{
			int num = reader.Peek();
			if (num == -1)
			{
				return text.Trim();
			}
			if ((ushort)num == 60)
			{
				break;
			}
			text += ((char)reader.Read()).ToString();
		}
		return text.Trim();
	}
}
