// Decompiled with JetBrains decompiler
// Type: FriendMessagedMutualFollowerListModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class FriendMessagedMutualFollowerListModel : BaseModel
{
  public static string URL = "ajax/friend/followmessagelist";
  public FriendMessagedMutualFollowerListModel.Param result = new FriendMessagedMutualFollowerListModel.Param();

  [Serializable]
  public class Param
  {
    public List<FriendMessageUserListModel.MessageUserInfo> messageFollowList = new List<FriendMessageUserListModel.MessageUserInfo>();
  }

  public class RequestSendForm
  {
  }
}
