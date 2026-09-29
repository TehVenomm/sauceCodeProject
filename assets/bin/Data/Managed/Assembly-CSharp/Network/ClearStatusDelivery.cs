// Decompiled with JetBrains decompiler
// Type: Network.ClearStatusDelivery
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class ClearStatusDelivery
{
  public int deliveryId;
  public int deliveryStatus;
  public List<int> needCount = new List<int>();

  public int GetNeedCount(uint idx = 0)
  {
    return (long) idx >= (long) this.needCount.Count ? 0 : this.needCount[(int) idx];
  }

  public int GetAllNeedCount()
  {
    int allNeedCount = 0;
    int index = 0;
    for (int count = this.needCount.Count; index < count; ++index)
      allNeedCount += this.needCount[index];
    return allNeedCount;
  }
}
