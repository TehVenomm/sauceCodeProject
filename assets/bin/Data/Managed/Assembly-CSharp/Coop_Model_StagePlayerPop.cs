// Decompiled with JetBrains decompiler
// Type: Coop_Model_StagePlayerPop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class Coop_Model_StagePlayerPop : Coop_Model_Base
{
  public int sid;
  public bool isSelf;
  public CharaInfo charaInfo;
  public StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo;
  public StageObjectManager.PlayerTransferInfo transferInfo;

  public Coop_Model_StagePlayerPop() => this.packetType = PACKET_TYPE.STAGE_PLAYER_POP;

  public override string ToString()
  {
    string str = $"{$"{$"{$"{$",sid={(object) this.sid}"},isSelf={this.isSelf.ToString()}"},charaInfo={(object) this.charaInfo}"},extentionInfo={(object) this.extentionInfo}"},transferInfo={(object) this.transferInfo}";
    return base.ToString() + str;
  }
}
