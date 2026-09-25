// Decompiled with JetBrains decompiler
// Type: Coop_Model_CharacterUpdateDirection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_CharacterUpdateDirection : Coop_Model_ObjectBase
{
  public string trigger;
  public float dir;
  public float lerp_dir;

  public Coop_Model_CharacterUpdateDirection()
  {
    this.packetType = PACKET_TYPE.CHARACTER_UPDATE_DIRECTION;
  }

  public override bool IsHandleable(StageObject owner)
  {
    Character character = owner as Character;
    return (character.actionID != Character.ACTION_ID.ATTACK && character.actionID != (Character.ACTION_ID) 22 || character.directionWaitSync && !(character.directionWaitTrigger != this.trigger)) && base.IsHandleable(owner);
  }
}
