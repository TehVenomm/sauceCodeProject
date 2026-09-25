// Decompiled with JetBrains decompiler
// Type: FriendMessageDetailListModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class FriendMessageDetailListModel : BaseModel
{
  public static string URL = "ajax/friend/messagedetaillist";
  public FriendMessageDetailListModel.Param result = new FriendMessageDetailListModel.Param();

  [Serializable]
  public class Param
  {
    public int pageNumMax;
    public List<FriendMessageData> message = new List<FriendMessageData>();
  }

  public class RequestSendForm
  {
    public int userId;
    public int page;
  }
}
