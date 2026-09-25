// Decompiled with JetBrains decompiler
// Type: FriendUnfollowModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class FriendUnfollowModel : BaseModel
{
  public static string URL = "ajax/friend/unfollow";
  public FriendUnfollowModel.Param result = new FriendUnfollowModel.Param();

  [Serializable]
  public class Param
  {
    public int success;
  }

  public class RequestSendForm
  {
    public int followUserId;
  }
}
