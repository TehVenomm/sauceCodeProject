// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerStoneCount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_PlayerStoneCount : Coop_Model_ObjectBase
{
  public float remaind_time;
  public bool stop;
  public bool requested;

  public Coop_Model_PlayerStoneCount() => this.packetType = PACKET_TYPE.PLAYER_STONE_COUNT;

  public override bool IsForceHandleBefore(StageObject owner) => true;
}
