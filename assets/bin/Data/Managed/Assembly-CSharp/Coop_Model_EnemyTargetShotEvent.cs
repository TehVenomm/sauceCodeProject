// Decompiled with JetBrains decompiler
// Type: Coop_Model_EnemyTargetShotEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class Coop_Model_EnemyTargetShotEvent : Coop_Model_ObjectBase
{
  public List<Enemy.RandomShotInfo.TargetInfo> targets = new List<Enemy.RandomShotInfo.TargetInfo>();

  public Coop_Model_EnemyTargetShotEvent() => this.packetType = PACKET_TYPE.ENEMY_TARGRTSHOT_EVENT;
}
