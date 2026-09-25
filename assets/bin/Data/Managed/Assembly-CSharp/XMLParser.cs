// Decompiled with JetBrains decompiler
// Type: XMLParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class XMLParser
{
  private FlashCompatibleTextReader reader;
  private Stack elements;
  private XMLNode currentElement;

  public XMLParser()
  {
    this.elements = new Stack();
    this.currentElement = (XMLNode) null;
  }

  public XMLNode Parse(FlashCompatibleTextReader reader)
  {
    this.reader = reader;
    if (reader.Peek() == -1)
      return (XMLNode) null;
    this.SkipPrologs();
    if (reader.Peek() == -1)
      return (XMLNode) null;
    XMLNode child;
    do
    {
      string str1;
      do
      {
        this.SkipWhitespace();
        str1 = this.ReadTag((ushort) reader.Peek() == (ushort) 60).Trim();
      }
      while (str1.StartsWith("<!"));
      if (str1.StartsWith("</"))
      {
        string str2 = str1.Substring(2, str1.Length - 3);
        if (this.currentElement == null)
        {
          Debug.LogError((object) $"Got close tag '{str2}' without open tag.");
          return (XMLNode) null;
        }
        if (str2 != this.currentElement.tag)
        {
          Debug.LogError((object) $"Expected close tag for '{this.currentElement.tag}' but got '{str2}'.");
          return (XMLNode) null;
        }
        if (this.elements.Count == 0)
          return this.currentElement;
        this.currentElement = (XMLNode) this.elements.Pop();
      }
      else
      {
        int num1 = str1.IndexOf('"');
        if (num1 < 0)
          num1 = str1.IndexOf('\'');
        string tagArg;
        string str3;
        if (num1 < 0)
        {
          if (str1.EndsWith("/>"))
          {
            tagArg = str1.Substring(1, str1.Length - 3).Trim();
            str3 = "/>";
          }
          else
          {
            tagArg = str1.Substring(1, str1.Length - 2).Trim();
            str3 = "";
          }
        }
        else
        {
          int length = str1.IndexOf(" ");
          tagArg = str1.Substring(1, length).Trim();
          str3 = str1.Substring(length + 1);
        }
        child = new XMLNode(tagArg);
        bool flag1 = false;
        string str4;
        int num2;
        for (; str3.Length > 0; str3 = str4.Substring(num2 + 1))
        {
          string str5 = str3.Trim();
          switch (str5)
          {
            case "/>":
              flag1 = true;
              goto label_36;
            case ">":
              goto label_36;
            default:
              int length = str5.IndexOf("=");
              if (length < 0)
              {
                Debug.LogError((object) $"Invalid attribute for tag '{tagArg}'.");
                return (XMLNode) null;
              }
              string key = str5.Substring(0, length);
              str4 = str5.Substring(length + 1);
              bool flag2 = true;
              if (str4.StartsWith("\""))
                num2 = str4.IndexOf('"', 1);
              else if (str4.StartsWith("'"))
              {
                num2 = str4.IndexOf('\'', 1);
              }
              else
              {
                flag2 = false;
                num2 = str4.IndexOf(' ');
                if (num2 < 0)
                {
                  num2 = str4.IndexOf('>');
                  if (num2 < 0)
                    num2 = str4.IndexOf('/');
                }
              }
              if (num2 < 0)
              {
                Debug.LogError((object) $"Invalid attribute for tag '{tagArg}'.");
                return (XMLNode) null;
              }
              string str6 = !flag2 ? str4.Substring(0, num2 - 1) : str4.Substring(1, num2 - 1);
              child.attributes[key] = str6;
              continue;
          }
        }
label_36:
        if (!flag1)
          child.content = this.ReadText();
        if (this.currentElement != null)
          this.currentElement.AddChild(child);
        if (!flag1)
        {
          if (this.currentElement != null)
            this.elements.Push((object) this.currentElement);
          this.currentElement = child;
        }
      }
    }
    while (this.currentElement != null);
    return child;
  }

  private void SkipWhitespace()
  {
    while (true)
    {
      int c = this.reader.Peek();
      if (c != -1 && char.IsWhiteSpace((char) c))
        this.reader.Read();
      else
        break;
    }
  }

  private void SkipProlog()
  {
    this.reader.Read();
    while (true)
    {
      switch (this.reader.Peek())
      {
        case 60:
          this.SkipProlog();
          continue;
        case 62:
          goto label_2;
        default:
          this.reader.Read();
          continue;
      }
    }
label_2:
    this.reader.Read();
  }

  private void SkipPrologs()
  {
    int num1;
    while (true)
    {
      this.SkipWhitespace();
      num1 = this.reader.Read();
      if (num1 != -1)
      {
        if ((ushort) num1 == (ushort) 60)
        {
          int num2 = this.reader.Peek();
          if (num1 != -1 && ((ushort) num2 == (ushort) 63 /*0x3F*/ || (ushort) num2 == (ushort) 33))
            this.SkipProlog();
          else
            goto label_1;
        }
        else
          goto label_3;
      }
      else
        break;
    }
    return;
label_3:
    Debug.LogError((object) $"Expected '<' but got '{(object) num1}'.");
    return;
label_1:;
  }

  private string ReadTag(bool startingBracket)
  {
    this.SkipWhitespace();
    string str1 = "";
    char ch = (char) this.reader.Peek();
    if (startingBracket && ch != '<')
    {
      Debug.LogError((object) ("Expected < but got " + ch.ToString()));
      return (string) null;
    }
    if (!startingBracket)
      str1 += "<";
    string str2 = str1 + ((char) this.reader.Read()).ToString();
    while (this.reader.Peek() != 62)
      str2 += ((char) this.reader.Read()).ToString();
    return str2 + ((char) this.reader.Read()).ToString();
  }

  private string ReadText()
  {
    string str = "";
    while (true)
    {
      int num = this.reader.Peek();
      if (num != -1 && (ushort) num != (ushort) 60)
        str += ((char) this.reader.Read()).ToString();
      else
        break;
    }
    return str.Trim();
  }
}
