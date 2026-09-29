// Decompiled with JetBrains decompiler
// Type: Network.ChatChannel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class ChatChannel
{
  public int channel;
  public string host;
  public string path;
  public int port;
  public int connections;
  public int messages;

  public string CreateUrl()
  {
    return new UriBuilder($"ws://{this.host}/{this.path}")
    {
      Port = this.port
    }.Uri.ToString();
  }

  public override bool Equals(object obj)
  {
    return obj is ChatChannel chatChannel && this.channel == chatChannel.channel && this.host == chatChannel.host && this.path == chatChannel.path && this.port == chatChannel.port;
  }

  public override int GetHashCode() => base.GetHashCode();
}
