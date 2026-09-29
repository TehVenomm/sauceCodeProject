// Decompiled with JetBrains decompiler
// Type: JSONNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class JSONNode : IJSONFieldValue
{
  public JSONNode parent;
  public List<JSONField> fields_ = new List<JSONField>();
  public bool isList;
  public string listName = "";

  public JSONNode()
  {
  }

  public JSONNode(JSONNode parent) => this.parent = parent;

  public JSONNode(IJSONFieldValue val) => this.fields_.Add(new JSONField("0", val));

  public JSONNode(List<IJSONFieldValue> list)
  {
    for (int index = 0; index < list.Count; ++index)
      this.fields_.Add(new JSONField(index.ToString(), list[index]));
  }

  public void AddField(string fieldName, IJSONFieldValue val)
  {
    this.fields_.Add(new JSONField(fieldName, val));
  }

  public void AddField(int idx, IJSONFieldValue val)
  {
    this.fields_.Add(new JSONField(idx.ToString(), val));
  }

  public void AddField(IJSONFieldValue val) => this.fields_.Add(new JSONField((string) null, val));

  public IJSONFieldValue GetField(string name)
  {
    foreach (JSONField field in this.fields_)
    {
      if (field.name == name)
        return field.value;
    }
    return (IJSONFieldValue) null;
  }

  public IJSONFieldValue GetField(int index) => this.fields_[index].value;

  public int GetFieldCount() => this.fields_.Count;

  public JSONListFieldValue GetListFieldValue()
  {
    List<IJSONFieldValue> val = new List<IJSONFieldValue>();
    for (int index = 0; index < this.fields_.Count; ++index)
      val.Add(this.fields_[index].value);
    return new JSONListFieldValue(val);
  }

  public string Serialize()
  {
    if (this.fields_.Count == 1 && (this.fields_[0].name == "" || this.fields_[0].name == null))
      return this.fields_[0].value.Serialize();
    string str = "{";
    if (this.fields_.Count > 0)
      str = $"{str}\"{this.fields_[0].name}\":{this.fields_[0].value.Serialize()}";
    for (int index = 1; index < this.fields_.Count; ++index)
      str = $"{str},\"{this.fields_[index].name}\":{this.fields_[index].value.Serialize()}";
    return str + "}";
  }
}
