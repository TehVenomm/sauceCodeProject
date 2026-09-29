// Decompiled with JetBrains decompiler
// Type: Coop_Model_CharacterMoveVelocityEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_CharacterMoveVelocityEnd : Coop_Model_ObjectBase
{
  public float time;
  public Vector3 pos = Vector3.zero;
  public float direction;
  public float sync_speed;
  public int motion_id;

  public Coop_Model_CharacterMoveVelocityEnd()
  {
    this.packetType = PACKET_TYPE.CHARACTER_MOVE_VELOCITY_END;
  }

  public override Vector3 GetObjectPosition() => this.pos;

  public override bool IsHaveObjectPosition() => true;

  public override bool IsHandleable(StageObject owner)
  {
    Character character = owner as Character;
    bool flag = false;
    if (character.actionID == Character.ACTION_ID.MOVE && character.moveType == Character.MOVE_TYPE.SYNC_VELOCITY)
      flag = true;
    return (character.IsChangeableAction(Character.ACTION_ID.MOVE) || flag) && base.IsHandleable(owner);
  }
}
