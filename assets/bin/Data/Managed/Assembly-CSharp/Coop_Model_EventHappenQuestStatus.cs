// Decompiled with JetBrains decompiler
// Type: Coop_Model_EventHappenQuestStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class Coop_Model_EventHappenQuestStatus : Coop_Model_Base
{
  public List<Coop_Model_EventHappenQuestStatus.Status> statusList = new List<Coop_Model_EventHappenQuestStatus.Status>();

  public Coop_Model_EventHappenQuestStatus()
  {
    this.packetType = PACKET_TYPE.EVENT_HAPPEN_QUEST_STATUS;
  }

  public override string ToString()
  {
    string status_str = "";
    this.statusList.ForEach((Action<Coop_Model_EventHappenQuestStatus.Status>) (s => status_str += $"(type:{s.orderType},0:{s.order_0},1:{s.order_1},2:{s.order_2},now:{(s.orderType == 2 ? s.defeatEnemyNum : (s.orderType == 3 ? s.remainingTime : 0))}"));
    return base.ToString() + status_str;
  }

  [Serializable]
  public class Status
  {
    public const int ORDER_TYPE_DEFEAT_ENEMY = 2;
    public const int ORDER_TYPE_REMAINING_TIME = 3;
    public int orderType;
    public int order_0;
    public int order_1;
    public int order_2;
    public int defeatEnemyNum;
    public int remainingTime;
  }
}
