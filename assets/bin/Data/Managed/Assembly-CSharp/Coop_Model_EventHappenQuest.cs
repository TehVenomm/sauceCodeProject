// Decompiled with JetBrains decompiler
// Type: Coop_Model_EventHappenQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class Coop_Model_EventHappenQuest : Coop_Model_Base
{
  public int qId;
  public List<List<int>> rewards = new List<List<int>>();
  public int rareBossType;

  public Coop_Model_EventHappenQuest() => this.packetType = PACKET_TYPE.EVENT_HAPPEN_QUEST;

  public override string ToString()
  {
    string reward_str = "";
    this.rewards.ForEach((Action<List<int>>) (r => reward_str = $"{reward_str}({(object) r[0]},{(object) r[1]},{(object) r[2]}),"));
    return $"{base.ToString()},qId={(object) this.qId},rewards={(object) this.rewards.Count}/{reward_str}";
  }
}
