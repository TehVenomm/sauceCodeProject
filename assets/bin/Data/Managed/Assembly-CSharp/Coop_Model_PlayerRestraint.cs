// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerRestraint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_PlayerRestraint : Coop_Model_ObjectSyncPositionBase
{
  public float duration;
  public float damageInterval;
  public int damageRate;
  public float reduceTimeByFlick;
  public string effectName = string.Empty;
  public bool isStopMotion;
  public bool isDisableRemoveByPlayerAttack = true;

  public Coop_Model_PlayerRestraint() => this.packetType = PACKET_TYPE.PLAYER_RESTRAINT;
}
