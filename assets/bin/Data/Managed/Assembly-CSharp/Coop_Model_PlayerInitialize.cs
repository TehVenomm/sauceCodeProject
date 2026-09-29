// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerInitialize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class Coop_Model_PlayerInitialize : Coop_Model_ObjectSyncPositionBase
{
  public int sid;
  public int hp;
  public int healHp;
  public int target_id;
  public bool stopcounter;
  public bool act_battle_start;
  public CharaInfo.EquipItem weapon_item;
  public int weapon_index;
  public BuffParam.BuffSyncParam buff_sync_param;
  public int cannonId;
  public int bulletIndex;
  public int fishingState;
  public int gatherGimmickId;
  public int carryingGimmickId;

  public Coop_Model_PlayerInitialize() => this.packetType = PACKET_TYPE.PLAYER_INITIALIZE;

  public override bool IsPromiseOverAgainCheck() => true;

  public override bool IsForceHandleBefore(StageObject owner) => true;
}
