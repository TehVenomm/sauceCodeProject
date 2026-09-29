// Decompiled with JetBrains decompiler
// Type: Network.MysteryGift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class MysteryGift
{
  public List<MysteryGift.MysteryGiftReward> present;
  public int received;
  public int next;

  public class MysteryGiftReward
  {
    public string name;
    public int type;
    public int itemId;
    public int num;
  }
}
