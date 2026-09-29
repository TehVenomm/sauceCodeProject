// Decompiled with JetBrains decompiler
// Type: FriendFollowListModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class FriendFollowListModel : BaseModel
{
  public static string URL = "ajax/friend/followlist";
  public FriendFollowListModel.Param result = new FriendFollowListModel.Param();

  [Serializable]
  public class Param
  {
    public int pageNumMax;
    public List<FriendCharaInfo> follow = new List<FriendCharaInfo>();
  }

  public class RequestSendForm
  {
    public int page;
  }
}
