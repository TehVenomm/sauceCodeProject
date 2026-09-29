// Decompiled with JetBrains decompiler
// Type: Coop_Model_EnemyDefeat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class Coop_Model_EnemyDefeat : Coop_Model_Base
{
  public int sid;
  public int eid;
  public int exp;
  public int money;
  public int ppt;
  public int defeatKeyId;
  public string sig;
  public int x;
  public int z;
  public int rewardId;
  public int rewardId2;
  public List<int> dropIds;
  public List<int> dropTypes;
  public List<int> dropItemIds;
  public List<int> dropNums;
  public List<int> dropParam_0s;
  public int deliver;
  public int boostBit;
  public int boostNum;
  public bool dropLoungeShare;
  public int boxType;

  public Coop_Model_EnemyDefeat() => this.packetType = PACKET_TYPE.ENEMY_DEFEAT;

  public override string ToString()
  {
    string str_dropIds = "";
    string str_dropItemIds = "";
    string str_dropNums = "";
    if (this.dropIds != null)
      this.dropIds.ForEach((Action<int>) (id => str_dropIds = $"{str_dropIds}{(object) id},"));
    if (this.dropItemIds != null)
      this.dropItemIds.ForEach((Action<int>) (id => str_dropItemIds = $"{str_dropItemIds}{(object) id},"));
    if (this.dropParam_0s != null)
      this.dropNums.ForEach((Action<int>) (id => str_dropNums = $"{str_dropNums}{(object) id},"));
    string str = $"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$",sid={(object) this.sid},eid={(object) this.eid}"},exp={(object) this.exp},money={(object) this.money},portalPoint={(object) this.ppt}"},keyid={(object) this.defeatKeyId},sig={this.sig}"},rewardId={(object) this.rewardId}"},rewardId2={(object) this.rewardId2}"},dropIds={str_dropIds.Trim(',')}"},dropItemIds={str_dropItemIds.Trim(',')}"},dropNums={str_dropNums.Trim(',')}"},deliverBitFlag={(object) this.deliver}"},deliverBoostBitFlag={(object) this.boostBit}"},deliverBoostNum={(object) this.boostNum}"},boxType={(object) this.boxType}";
    return base.ToString() + str;
  }
}
