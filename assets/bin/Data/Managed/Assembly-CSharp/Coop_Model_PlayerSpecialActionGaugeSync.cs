// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerSpecialActionGaugeSync
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_PlayerSpecialActionGaugeSync : Coop_Model_ObjectBase
{
  public int weaponIndex;
  public float currentSpActionGauge;
  public int comboLv;

  public Coop_Model_PlayerSpecialActionGaugeSync()
  {
    this.packetType = PACKET_TYPE.PLAYER_SPECIAL_ACTION_GAUGE_SYNC;
  }
}
