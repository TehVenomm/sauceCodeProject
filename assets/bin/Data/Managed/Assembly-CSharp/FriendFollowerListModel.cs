// Decompiled with JetBrains decompiler
// Type: FriendFollowerListModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class FriendFollowerListModel : BaseModel
{
  public static string URL = "ajax/friend/followerlist";
  public FriendFollowerListModel.Param result = new FriendFollowerListModel.Param();

  public class Param : FriendFollowListModel.Param
  {
    public int chunkSize = 100;
    public int totalFollowers;
  }

  public class RequestSendForm
  {
    public int page;
    public int sortType;
  }
}
