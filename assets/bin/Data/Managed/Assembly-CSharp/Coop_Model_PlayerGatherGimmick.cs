// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerGatherGimmick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_PlayerGatherGimmick : Coop_Model_ObjectSyncPositionBase
{
  public Vector3 act_pos = Vector3.zero;
  public bool act_pos_f;
  public int gimmickId;

  public Coop_Model_PlayerGatherGimmick() => this.packetType = PACKET_TYPE.PLAYER_GATHER_GIMMICK;

  public override bool IsHandleable(StageObject owner)
  {
    return (owner as Character).IsChangeableAction((Character.ACTION_ID) 40) && base.IsHandleable(owner);
  }
}
