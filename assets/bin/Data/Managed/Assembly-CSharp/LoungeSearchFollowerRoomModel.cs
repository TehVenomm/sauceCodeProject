// Decompiled with JetBrains decompiler
// Type: LoungeSearchFollowerRoomModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class LoungeSearchFollowerRoomModel : BaseModel
{
  public static string URL = "ajax/lounge/followerlounge";
  public LoungeSearchFollowerRoomModel.Param result = new LoungeSearchFollowerRoomModel.Param();

  public class Param
  {
    public List<LoungeSearchFollowerRoomModel.LoungeFollowerModel> lounges = new List<LoungeSearchFollowerRoomModel.LoungeFollowerModel>();
    public List<int> firstMetUserIds = new List<int>();
  }

  public class LoungeFollowerModel : LoungeModel.Lounge
  {
    public int followerUserId;
  }
}
