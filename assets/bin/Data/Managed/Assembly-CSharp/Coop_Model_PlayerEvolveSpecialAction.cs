// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerEvolveSpecialAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_PlayerEvolveSpecialAction : Coop_Model_ObjectSyncPositionBase
{
  public Coop_Model_PlayerEvolveSpecialAction()
  {
    this.packetType = PACKET_TYPE.PLAYER_EVOLVE_SPECIAL_ACTION;
  }

  public override bool IsHandleable(StageObject owner)
  {
    if (!(owner is Player player))
      return true;
    return player.IsChangeableAction((Character.ACTION_ID) 38) && base.IsHandleable(owner);
  }
}
