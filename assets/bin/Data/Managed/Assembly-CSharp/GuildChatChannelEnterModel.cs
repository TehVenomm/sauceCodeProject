// Decompiled with JetBrains decompiler
// Type: GuildChatChannelEnterModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class GuildChatChannelEnterModel : BaseModel
{
  public static string URL = "clan/ChatChannelEnter.go";
  public GuildChatChannelEnterModel.Param result = new GuildChatChannelEnterModel.Param();

  [Serializable]
  public class Param
  {
    public ChatChannel channel;
  }

  public class RequestSendForm
  {
    public int channel;
  }
}
