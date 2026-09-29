// Decompiled with JetBrains decompiler
// Type: Coop_Model_EnemyPop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_EnemyPop : Coop_Model_Base
{
  public int sid;
  public int ownerClientId;
  public int popIndex;
  public int seriesIdx;
  public bool setPos;
  public float x;
  public float z;

  public Coop_Model_EnemyPop() => this.packetType = PACKET_TYPE.ENEMY_POP;

  public override string ToString()
  {
    return base.ToString() + $",sid={this.sid},ownerClientId={this.ownerClientId},popIndex={this.popIndex},seriesIdx={this.seriesIdx}";
  }
}
