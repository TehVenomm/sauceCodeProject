// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerGetHeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class Coop_Model_PlayerGetHeal : Coop_Model_ObjectBase
{
  public int heal_hp;
  public int heal_type;
  public int effect_type;
  public List<int> applyAbilityTypeList;
  public bool receive;

  public Coop_Model_PlayerGetHeal() => this.packetType = PACKET_TYPE.PLAYER_GET_HEAL;

  public void Serialize(int ownerId, Character.HealData healData, bool isReceive)
  {
    this.id = ownerId;
    this.heal_hp = healData.healHp;
    this.heal_type = (int) healData.healType;
    this.effect_type = (int) healData.effectType;
    this.applyAbilityTypeList = healData.applyAbilityTypeList;
    this.receive = isReceive;
  }

  public Character.HealData Deserialize()
  {
    return new Character.HealData(this.heal_hp, (HEAL_TYPE) this.heal_type, (HEAL_EFFECT_TYPE) this.effect_type, this.applyAbilityTypeList);
  }
}
