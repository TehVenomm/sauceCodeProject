// Decompiled with JetBrains decompiler
// Type: Network.FortuneWheelInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class FortuneWheelInfo
{
  public int curTicket;
  public int requiredTicket;
  public int requiredTicketx10;
  public int jackpot;
  public List<FortuneWheelItem> itemList;
  public int ticketPrice;
  public bool freeSpin;
}
