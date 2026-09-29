// Decompiled with JetBrains decompiler
// Type: XMLNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class XMLNode
{
  public XMLNode parent;
  public string tag = "";
  public List<XMLNode> children = new List<XMLNode>();
  public string content = "";
  public Dictionary<string, string> attributes = new Dictionary<string, string>();

  public XMLNode(string tagArg) => this.tag = tagArg;

  public string Serialize(bool newlines, int spacesNumber)
  {
    string str1 = "";
    string str2 = "";
    string str3 = "";
    if (newlines)
    {
      string str4 = str3 + "  ";
      for (int index = 0; index < spacesNumber; ++index)
        str2 += " ";
      str1 = "\n";
    }
    string str5 = $"{str2}<{this.tag}";
    foreach (string key in this.attributes.Keys)
      str5 = $"{str5} {key}=\"{this.attributes[key]}\"";
    string str6 = str5 + ">";
    foreach (XMLNode child in this.children)
      str6 = str6 + str1 + child.Serialize(newlines, spacesNumber + 2);
    if (this.content != "")
      str6 += this.content;
    return $"{str6 + str1}{str2}</{this.tag}>";
  }

  public void AddChild(XMLNode child)
  {
    child.parent = this;
    this.children.Add(child);
  }
}
