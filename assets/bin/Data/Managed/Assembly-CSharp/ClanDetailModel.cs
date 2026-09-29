// Decompiled with JetBrains decompiler
// Type: ClanDetailModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class ClanDetailModel : BaseModel
{
  public static string URL = "ajax/clan/detail";
  public ClanDetailModel.Param result = new ClanDetailModel.Param();

  public class Param
  {
    public ClanData clan = new ClanData();
    public List<FriendCharaInfo> memberList = new List<FriendCharaInfo>();
    public bool isRequested;
    public bool isInvited;
  }

  public class RequestSendForm
  {
    public string cId;
  }
}
