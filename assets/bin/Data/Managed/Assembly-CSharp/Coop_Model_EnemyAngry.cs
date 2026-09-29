// Decompiled with JetBrains decompiler
// Type: Coop_Model_EnemyAngry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class Coop_Model_EnemyAngry : Coop_Model_ObjectSyncPositionBase
{
  public int angryActionId;
  public uint angryId;
  public List<uint> execAngryIds = new List<uint>();

  public Coop_Model_EnemyAngry() => this.packetType = PACKET_TYPE.ENEMY_ANGRY;

  public override bool IsHandleable(StageObject owner)
  {
    return (owner as Character).IsChangeableAction(Character.ACTION_ID.PARALYZE | Character.ACTION_ID.FREEZE) && base.IsHandleable(owner);
  }
}
