// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerWarp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_PlayerWarp : Coop_Model_ObjectSyncPositionBase
{
  public Coop_Model_PlayerWarp() => this.packetType = PACKET_TYPE.PLAYER_WARP;

  public override bool IsHandleable(StageObject owner)
  {
    return (owner as Character).IsChangeableAction((Character.ACTION_ID) 36) && base.IsHandleable(owner);
  }
}
