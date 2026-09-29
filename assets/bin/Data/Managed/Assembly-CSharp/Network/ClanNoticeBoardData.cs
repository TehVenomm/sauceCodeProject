// Decompiled with JetBrains decompiler
// Type: Network.ClanNoticeBoardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Network;

public class ClanNoticeBoardData
{
  public string clanId = "";
  public string contributorUserId = "";
  public string contributorUserName = "";
  public int version = -1;
  public string body = "";
  public EndDate updatedAt;
  public UserClanData contributorUserClan;
}
