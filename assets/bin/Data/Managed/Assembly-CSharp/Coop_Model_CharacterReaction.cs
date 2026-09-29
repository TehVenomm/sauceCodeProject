// Decompiled with JetBrains decompiler
// Type: Coop_Model_CharacterReaction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_CharacterReaction : Coop_Model_ObjectSyncPositionBase
{
  public int reactionType;
  public Vector3 blowForce = Vector3.zero;
  public float loopTime;
  public int targetId;
  public int deadReviveCount;

  public Coop_Model_CharacterReaction() => this.packetType = PACKET_TYPE.CHARACTER_REACTION;

  public override bool IsForceHandleBefore(StageObject owner) => true;
}
