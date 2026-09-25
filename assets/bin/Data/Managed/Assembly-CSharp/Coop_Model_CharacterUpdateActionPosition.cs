// Decompiled with JetBrains decompiler
// Type: Coop_Model_CharacterUpdateActionPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_CharacterUpdateActionPosition : Coop_Model_ObjectBase
{
  public string trigger;
  public Vector3 act_pos = Vector3.zero;
  public bool act_pos_f;

  public Coop_Model_CharacterUpdateActionPosition()
  {
    this.packetType = PACKET_TYPE.CHARACTER_UPDATE_ACTION_POSITION;
  }

  public override bool IsHandleable(StageObject owner)
  {
    Character character = owner as Character;
    return (character.actionID != Character.ACTION_ID.ATTACK && character.actionID != (Character.ACTION_ID) 22 || character.actionPositionWaitSync && !(character.actionPositionWaitTrigger != this.trigger)) && base.IsHandleable(owner);
  }
}
