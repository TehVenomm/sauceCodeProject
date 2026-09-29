// Decompiled with JetBrains decompiler
// Type: Coop_Model_CharacterMoveSideways
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_CharacterMoveSideways : Coop_Model_ObjectSyncPositionBase
{
  public int moveAngleSign;
  public Vector3 actionPos = Vector3.zero;
  public bool actionPosFlag;

  public Coop_Model_CharacterMoveSideways()
  {
    this.packetType = PACKET_TYPE.CHARACTER_MOVE_SIDEWAYS;
  }

  public override bool IsHandleable(StageObject owner)
  {
    return (owner as Character).IsChangeableAction(Character.ACTION_ID.MOVE) && base.IsHandleable(owner);
  }
}
