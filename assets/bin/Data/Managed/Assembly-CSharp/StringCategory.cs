// Decompiled with JetBrains decompiler
// Type: StringCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class StringCategory
{
  public static readonly string[] categorice = Enum.GetNames(typeof (STRING_CATEGORY));

  public static STRING_CATEGORY FromString(string str)
  {
    int index = 0;
    for (int length = StringCategory.categorice.Length; index < length; ++index)
    {
      if (StringCategory.categorice[index] == str)
        return (STRING_CATEGORY) index;
    }
    Log.Error("{0} is not found, on STRING_CATEGORY.", (object) str);
    return STRING_CATEGORY.COMMON;
  }
}
