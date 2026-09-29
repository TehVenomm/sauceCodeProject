// Decompiled with JetBrains decompiler
// Type: XMLInStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class XMLInStream
{
  public XMLNode current;

  public XMLInStream(XMLNode node) => this.current = node;

  public XMLInStream(string input)
  {
    this.current = new XMLParser().Parse(new FlashCompatibleTextReader(input));
  }

  public XMLInStream Clone() => new XMLInStream(this.current);

  public string Tag => this.current.tag;

  public System.Collections.Generic.List<XMLInStream> Children
  {
    get
    {
      System.Collections.Generic.List<XMLInStream> children = new System.Collections.Generic.List<XMLInStream>();
      foreach (XMLNode child in this.current.children)
        children.Add(new XMLInStream(child));
      return children;
    }
  }

  public bool Has(string tag)
  {
    foreach (XMLNode child in this.current.children)
    {
      if (child.tag == tag)
        return true;
    }
    return false;
  }

  public int NumberChildren(string tag)
  {
    int num = 0;
    foreach (XMLNode child in this.current.children)
    {
      if (child.tag == tag)
        ++num;
    }
    return num;
  }

  public bool HasAttribute(string tag) => this.current.attributes.ContainsKey(tag);

  public XMLInStream Start(string tag)
  {
    foreach (XMLNode child in this.current.children)
    {
      if (child.tag == tag)
      {
        this.current = child;
        return this;
      }
    }
    Debug.LogError((object) $"No child node named: {tag} in node {this.current.tag}");
    return (XMLInStream) null;
  }

  public XMLInStream End()
  {
    if (this.current.parent == null)
    {
      Debug.LogError((object) ("No parent node for tag: " + this.current.tag));
      return (XMLInStream) null;
    }
    this.current = this.current.parent;
    return this;
  }

  public XMLInStream Content(string tag, out string value)
  {
    return this.Start(tag).Content(out value).End();
  }

  public XMLInStream Content(out string value)
  {
    value = this.current.content;
    return this;
  }

  public XMLInStream Content(string tag, out bool value)
  {
    return this.Start(tag).Content(out value).End();
  }

  public XMLInStream Content(out bool value)
  {
    value = FlashCompatibleConvert.ToBoolean(this.current.content);
    return this;
  }

  public XMLInStream Content(string tag, out int value) => this.Start(tag).Content(out value).End();

  public XMLInStream Content(out int value)
  {
    value = FlashCompatibleConvert.ToInt32(this.current.content);
    return this;
  }

  public XMLInStream Content(string tag, out float value)
  {
    return this.Start(tag).Content(out value).End();
  }

  public XMLInStream Content(out float value)
  {
    value = (float) this.GetDouble(this.current.content);
    return this;
  }

  public XMLInStream Content(string tag, out Color value)
  {
    if (tag != null)
      this.Start(tag);
    float num1;
    float num2;
    float num3;
    float num4;
    if (this.HasAttribute("r"))
    {
      this.Attribute("r", out num1).Attribute("g", out num2).Attribute("b", out num3).Attribute("a", out num4).End();
    }
    else
    {
      string str;
      if (tag != null)
      {
        this.End();
        this.Content(tag, out str);
      }
      else
        this.Content(out str);
      str = str.Replace("[", "").Replace("]", "");
      string[] strArray = str.Split(',');
      num1 = (float) this.GetDouble(strArray[0]);
      num2 = (float) this.GetDouble(strArray[1]);
      num3 = (float) this.GetDouble(strArray[2]);
      num4 = (float) this.GetDouble(strArray[3]);
    }
    value = new Color(num1, num2, num3, num4);
    return this;
  }

  public XMLInStream Content(out Color value) => this.Content((string) null, out value);

  public XMLInStream Content(string tag, out Vector2 value)
  {
    if (tag != null)
      this.Start(tag);
    float num1;
    float num2;
    if (this.HasAttribute("x"))
    {
      this.Attribute("x", out num1).Attribute("y", out num2).End();
    }
    else
    {
      string str;
      if (tag != null)
      {
        this.End();
        this.Content(tag, out str);
      }
      else
        this.Content(out str);
      str = str.Replace("[", "").Replace("]", "").Replace(" ", "");
      string[] strArray = str.Split(',');
      num1 = (float) this.GetDouble(strArray[0]);
      num2 = (float) this.GetDouble(strArray[1]);
    }
    value = new Vector2(num1, num2);
    return this;
  }

  public XMLInStream Content(out Vector2 value) => this.Content((string) null, out value);

  public XMLInStream Content(string tag, out Vector3 value)
  {
    if (tag != null)
      this.Start(tag);
    float num1;
    float num2;
    float num3;
    if (this.HasAttribute("x"))
    {
      this.Attribute("x", out num1).Attribute("y", out num2).Attribute("z", out num3).End();
    }
    else
    {
      string str;
      if (tag != null)
      {
        this.End();
        this.Content(tag, out str);
      }
      else
        this.Content(out str);
      str = str.Replace("[", "").Replace("]", "").Replace(" ", "");
      string[] strArray = str.Split(',');
      num1 = (float) this.GetDouble(strArray[0]);
      num2 = (float) this.GetDouble(strArray[1]);
      num3 = (float) this.GetDouble(strArray[2]);
    }
    value = new Vector3(num1, num2, num3);
    return this;
  }

  public XMLInStream Content(out Vector3 value) => this.Content((string) null, out value);

  public XMLInStream Content(string tag, out Quaternion value)
  {
    if (tag != null)
      this.Start(tag);
    float num1;
    float num2;
    float num3;
    float num4;
    if (this.HasAttribute("x"))
    {
      this.Attribute("x", out num1).Attribute("y", out num2).Attribute("z", out num3).Attribute("w", out num4).End();
    }
    else
    {
      string str;
      if (tag != null)
      {
        this.End();
        this.Content(tag, out str);
      }
      else
        this.Content(out str);
      str = str.Replace("[", "").Replace("]", "");
      string[] strArray = str.Split(',');
      num1 = (float) this.GetDouble(strArray[0]);
      num2 = (float) this.GetDouble(strArray[1]);
      num3 = (float) this.GetDouble(strArray[2]);
      num4 = (float) this.GetDouble(strArray[3]);
    }
    value = new Quaternion(num1, num2, num3, num4);
    return this;
  }

  public XMLInStream Content(out Quaternion value) => this.Content((string) null, out value);

  public XMLInStream Content(string tag, out Vector4 value)
  {
    if (tag != null)
      this.Start(tag);
    float num1;
    float num2;
    float num3;
    float num4;
    if (this.HasAttribute("x"))
    {
      this.Attribute("x", out num1).Attribute("y", out num2).Attribute("z", out num3).Attribute("w", out num4).End();
    }
    else
    {
      string str;
      if (tag != null)
      {
        this.End();
        this.Content(tag, out str);
      }
      else
        this.Content(out str);
      str = str.Replace("[", "").Replace("]", "");
      string[] strArray = str.Split(',');
      num1 = (float) this.GetDouble(strArray[0]);
      num2 = (float) this.GetDouble(strArray[1]);
      num3 = (float) this.GetDouble(strArray[2]);
      num4 = (float) this.GetDouble(strArray[3]);
    }
    value = new Vector4(num1, num2, num3, num4);
    return this;
  }

  public XMLInStream Content(out Vector4 value) => this.Content((string) null, out value);

  public XMLInStream Content(string tag, out Rect value)
  {
    if (tag != null)
      this.Start(tag);
    float num1;
    float num2;
    float num3;
    float num4;
    if (this.HasAttribute("x"))
    {
      this.Attribute("x", out num1).Attribute("y", out num2).Attribute("width", out num3).Attribute("height", out num4).End();
    }
    else
    {
      string str;
      if (tag != null)
      {
        this.End();
        this.Content(tag, out str);
      }
      else
        this.Content(out str);
      str = str.Replace("[", "").Replace("]", "");
      string[] strArray = str.Split(',');
      num1 = (float) this.GetDouble(strArray[0]);
      num2 = (float) this.GetDouble(strArray[1]);
      num3 = (float) this.GetDouble(strArray[2]);
      num4 = (float) this.GetDouble(strArray[3]);
    }
    value = new Rect(num1, num2, num3, num4);
    return this;
  }

  public XMLInStream Content(out Rect value) => this.Content((string) null, out value);

  public XMLInStream ContentOptional(string tag, ref string value)
  {
    return this.Has(tag) ? this.Content(tag, out value) : this;
  }

  public XMLInStream ContentOptional(string tag, ref bool value)
  {
    return this.Has(tag) ? this.Content(tag, out value) : this;
  }

  public XMLInStream ContentOptional(string tag, ref int value)
  {
    return this.Has(tag) ? this.Content(tag, out value) : this;
  }

  public XMLInStream ContentOptional(string tag, ref float value)
  {
    return this.Has(tag) ? this.Content(tag, out value) : this;
  }

  public XMLInStream ContentOptional(string tag, ref Vector2 value)
  {
    return this.Has(tag) ? this.Content(tag, out value) : this;
  }

  public XMLInStream ContentOptional(string tag, ref Vector3 value)
  {
    return this.Has(tag) ? this.Content(tag, out value) : this;
  }

  public XMLInStream ContentOptional(string tag, ref Quaternion value)
  {
    return this.Has(tag) ? this.Content(tag, out value) : this;
  }

  public XMLInStream ContentOptional(string tag, ref Color value)
  {
    return this.Has(tag) ? this.Content(tag, out value) : this;
  }

  public XMLInStream ContentOptional(string tag, ref Rect value)
  {
    return this.Has(tag) ? this.Content(tag, out value) : this;
  }

  public XMLInStream AttributeOptional(string name, ref string value)
  {
    if (this.current.attributes.ContainsKey(name))
      value = this.current.attributes[name];
    return this;
  }

  public XMLInStream AttributeOptional(string name, ref bool value)
  {
    if (this.current.attributes.ContainsKey(name))
      value = FlashCompatibleConvert.ToBoolean(this.GetAttribute(name));
    return this;
  }

  public XMLInStream AttributeOptional(string name, ref int value)
  {
    if (this.current.attributes.ContainsKey(name))
      value = FlashCompatibleConvert.ToInt32(this.GetAttribute(name));
    return this;
  }

  public XMLInStream AttributeOptional(string name, ref float value)
  {
    if (this.current.attributes.ContainsKey(name))
      value = (float) this.GetDouble(this.GetAttribute(name));
    return this;
  }

  public XMLInStream Attribute(string name, out string value)
  {
    value = this.GetAttribute(name);
    return this;
  }

  public XMLInStream Attribute(string name, out int value)
  {
    value = FlashCompatibleConvert.ToInt32(this.GetAttribute(name));
    return this;
  }

  public XMLInStream Attribute(string name, out float value)
  {
    value = (float) this.GetDouble(this.GetAttribute(name));
    return this;
  }

  public XMLInStream Attribute(string name, out bool value)
  {
    value = FlashCompatibleConvert.ToBoolean(this.GetAttribute(name));
    return this;
  }

  private string GetAttribute(string name)
  {
    if (this.current.attributes.ContainsKey(name))
      return this.current.attributes[name];
    Debug.LogError((object) $"Attribute {name} don't exist in node {this.current.tag}");
    return (string) null;
  }

  public XMLInStream List(string tag, Action<XMLInStream> callback)
  {
    foreach (XMLNode child in this.current.children)
    {
      if (child.tag == tag)
        callback(new XMLInStream(child));
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
      return val.Contains(".") ? FlashCompatibleConvert.ToDouble(val.Replace('.', ',')) : FlashCompatibleConvert.ToDouble(val.Replace(',', '.'));
    }
  }
}
