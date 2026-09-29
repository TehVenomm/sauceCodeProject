// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerShotArrow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_PlayerShotArrow : Coop_Model_ObjectSyncPositionBase
{
  public Vector3 shot_pos = Vector3.zero;
  public Quaternion shot_rot;
  public string attack_name;
  public float attack_rate;
  public int shot_count;
  public bool is_sit_shot;
  public bool is_aim_end = true;

  public Coop_Model_PlayerShotArrow() => this.packetType = PACKET_TYPE.PLAYER_SHOT_ARROW;

  public override bool IsHandleable(StageObject owner)
  {
    Player player = owner as Player;
    return (player.actionID != Character.ACTION_ID.ATTACK || player.shotArrowCount >= this.shot_count) && base.IsHandleable(owner);
  }
}
