// Decompiled with JetBrains decompiler
// Type: ChatServerChannelEnterModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class ChatServerChannelEnterModel : BaseModel
{
  public static string URL = "ajax/chat-server/channel-enter";
  public ChatServerChannelEnterModel.Param result = new ChatServerChannelEnterModel.Param();

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
