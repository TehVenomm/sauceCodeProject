// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerGetChargeSkillGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_PlayerGetChargeSkillGauge : Coop_Model_ObjectBase
{
  public int buffType;
  public int buffValue;
  public int useSkillIndex;
  public bool receive;
  public bool isCorrectWaveMatch;

  public Coop_Model_PlayerGetChargeSkillGauge()
  {
    this.packetType = PACKET_TYPE.PLAYER_GET_CHARGE_SKILLGAUGE;
  }
}
