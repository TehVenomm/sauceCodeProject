// Decompiled with JetBrains decompiler
// Type: Network.FriendCharaInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class FriendCharaInfo : CharaInfo
{
  public bool following;
  public bool follower;
  public int requestId;
  public int playedCount;
  public int following_id;
  public int follower_id;
  public FriendCharaInfo.JoinInfo joinStatus;

  [Serializable]
  public class JoinInfo
  {
    public int joinType;
    public int targetParam;
    public string conditionParam = "";
  }
}
