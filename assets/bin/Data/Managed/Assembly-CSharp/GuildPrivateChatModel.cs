// Decompiled with JetBrains decompiler
// Type: GuildPrivateChatModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class GuildPrivateChatModel : BaseModel
{
  public static string URL = "clan/ChatPrivateLog.go";
  public GuildPrivateChatModel.Param result = new GuildPrivateChatModel.Param();

  [Serializable]
  public class Param
  {
    public List<ClanChatLogMessageData> array = new List<ClanChatLogMessageData>();
  }

  public class SendForm
  {
    public int toUserId;
  }
}
