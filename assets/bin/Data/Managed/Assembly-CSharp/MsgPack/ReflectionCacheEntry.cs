// Decompiled with JetBrains decompiler
// Type: MsgPack.ReflectionCacheEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace MsgPack;

public class ReflectionCacheEntry
{
  private const BindingFlags FieldBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField;

  public ReflectionCacheEntry(Type t)
  {
    FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField);
    IDictionary<string, FieldInfo> dictionary = (IDictionary<string, FieldInfo>) new Dictionary<string, FieldInfo>(fields.Length);
    for (int index = 0; index < fields.Length; ++index)
    {
      FieldInfo fieldInfo = fields[index];
      string key = fieldInfo.Name;
      int num;
      if (key[0] == '<' && (num = key.IndexOf('>')) > 1)
        key = key.Substring(1, num - 1);
      dictionary[key] = fieldInfo;
    }
    this.FieldMap = dictionary;
  }

  public IDictionary<string, FieldInfo> FieldMap { get; private set; }
}
