// Decompiled with JetBrains decompiler
// Type: Coop_Model_CharacterBuffRoutine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_CharacterBuffRoutine : Coop_Model_ObjectBase
{
  public int type;
  public int value;
  public int valueType;
  public int fromObjectID;
  public int fromEquipIndex;
  public int fromSkillIndex;

  public Coop_Model_CharacterBuffRoutine() => this.packetType = PACKET_TYPE.CHARACTER_BUFFROUTINE;

  public BuffParam.BuffData Deserialize()
  {
    return new BuffParam.BuffData()
    {
      type = (BuffParam.BUFFTYPE) this.type,
      value = this.value,
      valueType = (BuffParam.VALUE_TYPE) this.valueType,
      fromObjectID = this.fromObjectID,
      fromEquipIndex = this.fromEquipIndex,
      fromSkillIndex = this.fromSkillIndex
    };
  }
}
