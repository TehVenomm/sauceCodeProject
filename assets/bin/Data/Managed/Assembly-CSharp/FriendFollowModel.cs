// Decompiled with JetBrains decompiler
// Type: FriendFollowModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class FriendFollowModel : BaseModel
{
  public static string URL = "ajax/friend/follow";
  public FriendFollowModel.Param result = new FriendFollowModel.Param();

  [Serializable]
  public class Param
  {
    public List<int> success = new List<int>();
    public List<int> err = new List<int>();
  }

  public class RequestSendForm
  {
    public List<int> ids = new List<int>();
  }
}
