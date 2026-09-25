// Decompiled with JetBrains decompiler
// Type: Network.ClanMemberInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Network;

public class ClanMemberInfo : FriendCharaInfo
{
  public ClanMemberInfo.ClanInfo myClanInfo;
  public ClanMemberInfo.CurrentActivityInfo myActivityInfo;

  public bool IsJoinClan() => this.myClanInfo != null;

  public bool IsJoinClan(int _alianceId)
  {
    return this.IsJoinClan() && _alianceId == this.myClanInfo.ClanId;
  }

  public new class ClanInfo
  {
    public int ClanId = -1;
    public string ClanName = "";
    public int ClanMemberId = -1;
  }

  public class CurrentActivityInfo
  {
    public int LoungeNo = -1;
    public int RoomNo = -1;
    public int DeliveryId = -1;
  }
}
