// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerApplyChangeWeapon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class Coop_Model_PlayerApplyChangeWeapon : Coop_Model_ObjectBase
{
  public CharaInfo.EquipItem item;
  public int index;

  public Coop_Model_PlayerApplyChangeWeapon()
  {
    this.packetType = PACKET_TYPE.PLAYER_APPLY_CHANGE_WEAPON;
  }

  public override bool IsHandleable(StageObject owner)
  {
    Player player = owner as Player;
    return (player.actionID != (Character.ACTION_ID) 27 || player.IsValidWaitingPacket(StageObject.WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON)) && base.IsHandleable(owner);
  }
}
