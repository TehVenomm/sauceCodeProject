// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerChargeRelease
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_PlayerChargeRelease : Coop_Model_ObjectSyncPositionBase
{
  public float lerp_dir;
  public float charge_rate;
  public Vector3 act_pos = Vector3.zero;
  public bool act_pos_f;
  public bool isExRushCharge;

  public Coop_Model_PlayerChargeRelease() => this.packetType = PACKET_TYPE.PLAYER_CHARGE_RELEASE;

  public override bool IsHandleable(StageObject owner)
  {
    Player player = owner as Player;
    return (player.actionID != Character.ACTION_ID.ATTACK && player.actionID != (Character.ACTION_ID) 22 || player.enableInputCharge) && base.IsHandleable(owner);
  }
}
