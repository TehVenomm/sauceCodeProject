// Decompiled with JetBrains decompiler
// Type: Coop_Model_CharacterAttack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_CharacterAttack : Coop_Model_ObjectSyncPositionBase
{
  public int attack_id;
  public string motionLayerName = "";
  public string motionStateName = "";
  public Vector3 act_pos = Vector3.zero;
  public bool act_pos_f;
  public bool sync_immediately;
  public int syncRandomSeed;

  public Coop_Model_CharacterAttack() => this.packetType = PACKET_TYPE.CHARACTER_ATTACK;

  public override bool IsHandleable(StageObject owner)
  {
    Character character = owner as Character;
    return (this.sync_immediately || character.IsChangeableAction(Character.ACTION_ID.ATTACK)) && base.IsHandleable(owner);
  }
}
