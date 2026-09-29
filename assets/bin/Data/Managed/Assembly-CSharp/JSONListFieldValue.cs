// Decompiled with JetBrains decompiler
// Type: JSONListFieldValue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class JSONListFieldValue : IJSONFieldValue
{
  public List<IJSONFieldValue> value;

  public JSONListFieldValue() => this.value = new List<IJSONFieldValue>();

  public JSONListFieldValue(List<IJSONFieldValue> val) => this.value = val;

  public string Serialize()
  {
    string str = "[";
    if (this.value.Count > 0)
      str += this.value[0].Serialize();
    for (int index = 1; index < this.value.Count; ++index)
      str = $"{str},{this.value[index].Serialize()}";
    return str + "]";
  }
}
