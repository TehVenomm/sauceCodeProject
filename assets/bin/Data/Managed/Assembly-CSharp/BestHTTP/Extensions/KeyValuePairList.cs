// Decompiled with JetBrains decompiler
// Type: BestHTTP.Extensions.KeyValuePairList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.ObjectModel;

#nullable disable
namespace BestHTTP.Extensions;

public class KeyValuePairList
{
  public ReadOnlyCollection<KeyValuePair> Values { get; protected set; }

  public bool TryGet(string value, out KeyValuePair param)
  {
    param = (KeyValuePair) null;
    for (int index = 0; index < this.Values.Count; ++index)
    {
      if (string.CompareOrdinal(this.Values[index].Key, value) == 0)
      {
        param = this.Values[index];
        return true;
      }
    }
    return false;
  }

  public bool HasAny(string val1, string val2 = "")
  {
    for (int index = 0; index < this.Values.Count; ++index)
    {
      if (string.CompareOrdinal(this.Values[index].Key, val1) == 0 || string.CompareOrdinal(this.Values[index].Key, val2) == 0)
        return true;
    }
    return false;
  }
}
