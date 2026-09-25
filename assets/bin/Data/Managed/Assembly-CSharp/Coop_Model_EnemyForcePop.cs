// Decompiled with JetBrains decompiler
// Type: Coop_Model_EnemyForcePop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_EnemyForcePop : Coop_Model_Base
{
  public string psig;
  public int keyId;
  public int eid;
  public int lv;
  public int popType;
  public float x;
  public float z;

  public Coop_Model_EnemyForcePop() => this.packetType = PACKET_TYPE.ENEMY_FORCE_POP;
}
