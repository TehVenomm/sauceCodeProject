// Decompiled with JetBrains decompiler
// Type: FriendMessageUserListModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class FriendMessageUserListModel : BaseModel
{
  public static string URL = "ajax/friend/messageuserlist";
  public FriendMessageUserListModel.Param result = new FriendMessageUserListModel.Param();

  [Serializable]
  public class Param
  {
    public int pageNumMax;
    public List<FriendMessageUserListModel.MessageUserInfo> messageUser = new List<FriendMessageUserListModel.MessageUserInfo>();
  }

  public class MessageUserInfo : FriendCharaInfo
  {
    public int noReadNum;
    public bool isPermitted;
  }

  public class RequestSendForm
  {
    public int page;
  }
}
