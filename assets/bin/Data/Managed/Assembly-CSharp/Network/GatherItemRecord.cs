// Decompiled with JetBrains decompiler
// Type: Network.GatherItemRecord
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class GatherItemRecord
{
  public int listId;
  public string name;
  public int num;
  public int maxValue;
  public int maxCrownType;

  public GATHER_ITEM_CROWN_TYPE GetCrownType() => (GATHER_ITEM_CROWN_TYPE) this.maxCrownType;

  public string GetSizeString() => GatherItemRecord.ShapeSize(this.maxValue);

  public static string ShapeSize(int size)
  {
    string str1;
    if (size < 10)
      str1 = "0.0" + size.ToString();
    else if (size < 100)
    {
      str1 = "0." + size.ToString();
    }
    else
    {
      string str2 = size.ToString();
      str1 = str2.Insert(str2.Length - 2, ".");
    }
    return str1;
  }
}
