// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerPrayerStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_PlayerPrayerStart : Coop_Model_ObjectBase
{
  public int sid;
  public int reason;

  public Coop_Model_PlayerPrayerStart() => this.packetType = PACKET_TYPE.PLAYER_PRAYER_START;

  public override bool IsForceHandleBefore(StageObject owner) => true;
}
