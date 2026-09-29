// Decompiled with JetBrains decompiler
// Type: Network.ShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class ShopList
{
  public List<ShopList.ShopLineup> lineups = new List<ShopList.ShopLineup>();

  public class ShopLineup
  {
    public int shopLineupId;
    public string name;
    public string description;
    public int crystalNum;
    public List<int> itemIds = new List<int>();
  }
}
