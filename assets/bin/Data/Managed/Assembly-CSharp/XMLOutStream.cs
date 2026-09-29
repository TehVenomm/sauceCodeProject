// Decompiled with JetBrains decompiler
// Type: XMLOutStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class XMLOutStream
{
  public XMLNode current;
  public string prolog;

  public XMLOutStream Prolog(string encoding)
  {
    this.prolog = $"<?xml version=\"1.0\" encoding=\"{encoding}\"?>";
    return this;
  }

  public string Serialize() => this.Serialize(false);

  public string Serialize(bool newlines)
  {
    string str = "";
    if (newlines)
      str = "\n";
    return this.prolog + str + this.current.Serialize(newlines, 0);
  }

  public XMLOutStream Start(string tag)
  {
    if (this.current == null)
    {
      this.current = new XMLNode(tag);
    }
    else
    {
      XMLNode child = new XMLNode(tag);
      this.current.AddChild(child);
      this.current = child;
    }
    return this;
  }

  public XMLOutStream End()
  {
    if (this.current.parent != null)
      this.current = this.current.parent;
    return this;
  }

  public XMLOutStream Content(string value)
  {
    this.current.content = value;
    return this;
  }

  public XMLOutStream Content(float value)
  {
    this.current.content = value.ToString();
    return this;
  }

  public XMLOutStream Content(int value)
  {
    this.current.content = value.ToString();
    return this;
  }

  public XMLOutStream Content(bool value)
  {
    this.current.content = value.ToString();
    return this;
  }

  public XMLOutStream Content(string tag, string value) => this.Start(tag).Content(value).End();

  public XMLOutStream Content(string tag, bool value) => this.Start(tag).Content(value).End();

  public XMLOutStream Content(string tag, int value) => this.Start(tag).Content(value).End();

  public XMLOutStream Content(string tag, float value) => this.Start(tag).Content(value).End();

  public XMLOutStream Content(Vector2 value)
  {
    this.Content($"[{(object) value.x},{(object) value.y}]");
    return this;
  }

  public XMLOutStream Content(string tag, Vector2 value)
  {
    this.Start(tag).Attribute("x", value.x).Attribute("y", value.y).End();
    return this;
  }

  public XMLOutStream Content(Vector3 value)
  {
    this.Content($"[{(object) value.x},{(object) value.y},{(object) value.z}]");
    return this;
  }

  public XMLOutStream Content(string tag, Vector3 value)
  {
    this.Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("z", value.z).End();
    return this;
  }

  public XMLOutStream Content(Vector4 value)
  {
    this.Content($"[{(object) value.x},{(object) value.y},{(object) value.z},{(object) value.w}]");
    return this;
  }

  public XMLOutStream Content(string tag, Vector4 value)
  {
    this.Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("z", value.z).Attribute("w", value.w).End();
    return this;
  }

  public XMLOutStream Content(Quaternion value)
  {
    this.Content($"[{(object) value.x},{(object) value.y},{(object) value.z},{(object) value.w}]");
    return this;
  }

  public XMLOutStream Content(string tag, Quaternion value)
  {
    this.Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("z", value.z).Attribute("w", value.w).End();
    return this;
  }

  public XMLOutStream Content(Color value)
  {
    this.Content($"[{(object) value.r},{(object) value.g},{(object) value.b},{(object) value.a}]");
    return this;
  }

  public XMLOutStream Content(string tag, Color value)
  {
    this.Start(tag).Attribute("r", value.r).Attribute("g", value.g).Attribute("b", value.b).Attribute("a", value.a).End();
    return this;
  }

  public XMLOutStream Content(Rect value)
  {
    this.Content($"[{(object) ((Rect) ref value).x},{(object) ((Rect) ref value).y},{(object) ((Rect) ref value).width},{(object) ((Rect) ref value).height}]");
    return this;
  }

  public XMLOutStream Content(string tag, Rect value)
  {
    this.Start(tag).Attribute("x", ((Rect) ref value).x).Attribute("y", ((Rect) ref value).y).Attribute("width", ((Rect) ref value).width).Attribute("height", ((Rect) ref value).height).End();
    return this;
  }

  public XMLOutStream Attribute(string name, string value)
  {
    this.current.attributes[name] = value;
    return this;
  }

  public XMLOutStream Attribute(string name, int value)
  {
    this.current.attributes[name] = value.ToString();
    return this;
  }

  public XMLOutStream Attribute(string name, float value)
  {
    this.current.attributes[name] = value.ToString();
    return this;
  }

  public XMLOutStream Attribute(string name, bool value)
  {
    this.current.attributes[name] = value.ToString();
    return this;
  }
}
