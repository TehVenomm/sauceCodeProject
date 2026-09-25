// Decompiled with JetBrains decompiler
// Type: Network.DarkMarketItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class DarkMarketItem
{
  public int id;
  public int limit;
  public float baseNum;
  public float saleNum;
  public string saleoffProductId;
  public string refProductId;
  public int saleType;
  public int usedCount;
  public int feature;
  public string imgId;
  public string name;
  public int remain = -1;
  public List<DarkMarketReward> rewards;
}
