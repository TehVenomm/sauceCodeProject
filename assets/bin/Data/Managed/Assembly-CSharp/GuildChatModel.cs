// Decompiled with JetBrains decompiler
// Type: GuildChatModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class GuildChatModel : BaseModel
{
  public static string URL = "clan/ChatLog.go";
  public GuildChatModel.Param result = new GuildChatModel.Param();

  [Serializable]
  public class Param
  {
    public List<ClanChatLogMessageData> array = new List<ClanChatLogMessageData>();
    public GuildChatModel.PinData pin;
    public ClanAdvisaryData advisory;
  }

  public class PinData
  {
    public int fromUserId;
    public int id;
    public int type;
    public string message;
    public string uuid;
    public CharaInfo charInfo;
  }
}
