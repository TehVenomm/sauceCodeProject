// Decompiled with JetBrains decompiler
// Type: Coop_Model_EnemyReleasedGrabbedPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_EnemyReleasedGrabbedPlayer : Coop_Model_ObjectSyncPositionBase
{
  public float angle;
  public float power;

  public Coop_Model_EnemyReleasedGrabbedPlayer()
  {
    this.packetType = PACKET_TYPE.ENEMY_RELEASE_GRABBED_PLAYER;
  }

  public override bool IsHandleable(StageObject owner) => base.IsHandleable(owner);
}
