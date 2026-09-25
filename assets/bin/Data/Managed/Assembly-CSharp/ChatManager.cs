// Decompiled with JetBrains decompiler
// Type: ChatManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ChatManager : MonoBehaviourSingleton<ChatManager>
{
  private const int CLAN_CHANNEL = 1;
  private ChatChannel offlineChannel = new ChatChannel()
  {
    host = "offline",
    channel = -1
  };
  private ChatChannel invalidChannel = new ChatChannel()
  {
    host = "invalid",
    channel = -1
  };

  public ChatRoom homeChat { get; private set; }

  public ChatRoom roomChat { get; private set; }

  public ChatRoom loungeChat { get; private set; }

  public ChatRoom clanChat { get; private set; }

  public event System.Action OnCreateRoomChat;

  public event Action<ChatRoom> OnDestroyRoomChat;

  public event Action<ChatRoom> OnCreateLoungeChat;

  public event Action<ChatRoom> OnDestroyLoungeChat;

  public event Action<ChatRoom> OnCreateClanChat;

  public event Action<ChatRoom> OnDestroyClanChat;

  public ChatChannelInfo chatChannelInfo { get; private set; }

  public ChatChannel currentChannel { get; private set; }

  public void OnNotifyUpdateChannnelInfo(ChatChannelInfo info)
  {
    this.chatChannelInfo = info;
    if (this.currentChannel != null && this.currentChannel != this.invalidChannel)
      return;
    this.SelectChannel(this.chatChannelInfo.recommend);
  }

  public List<int> GetChannels()
  {
    return this.chatChannelInfo != null ? this.chatChannelInfo.channels : (List<int>) null;
  }

  public ChatChannel GetCurrentChannel()
  {
    return this.currentChannel == null ? this.invalidChannel : this.currentChannel;
  }

  public int GetHotChannel()
  {
    return this.chatChannelInfo != null ? this.chatChannelInfo.hot : this.invalidChannel.channel;
  }

  public int GetColdChannel()
  {
    return this.chatChannelInfo != null ? this.chatChannelInfo.cold : this.invalidChannel.channel;
  }

  public int GetRecommendedChannel()
  {
    return this.chatChannelInfo != null ? this.chatChannelInfo.recommend : this.invalidChannel.channel;
  }

  public void SelectChannel(int channel)
  {
    if (channel <= 0 || this.currentChannel != null && this.currentChannel.channel == channel && this.homeChat.HasConnect)
      return;
    Protocol.Force((System.Action) (() => this.SendChannelEnter(channel, (Action<ChatChannel>) (chatChannel =>
    {
      if (chatChannel == null)
        return;
      if (this.homeChat != null && this.homeChat.connection != null)
        this.homeChat.Disconnect((System.Action) (() =>
        {
          ChatWebSocketConnection connection = this.homeChat.connection as ChatWebSocketConnection;
          if (Object.op_Implicit((Object) connection))
            Object.Destroy((Object) connection);
          this.ConnectHomeChat(chatChannel);
        }));
      else
        this.ConnectHomeChat(chatChannel);
    }))));
  }

  private void ConnectHomeChat(ChatChannel channel)
  {
    this.currentChannel = channel;
    if (channel == this.invalidChannel || channel == this.offlineChannel)
    {
      this.homeChat.SetConnection((IChatConnection) new ChatOfflineConnection());
    }
    else
    {
      ChatWebSocketConnection connection = ((Component) this).gameObject.AddComponent<ChatWebSocketConnection>();
      connection.Setup(channel.host, channel.port, channel.path);
      this.homeChat.SetConnection((IChatConnection) connection);
      this.homeChat.JoinRoom(1);
    }
  }

  public void CreateHomeChat()
  {
    if (this.homeChat != null)
    {
      if (this.currentChannel != this.invalidChannel)
        return;
      this.SelectChannel(this.GetRecommendedChannel());
    }
    else
    {
      this.homeChat = new ChatRoom();
      this.SelectChannel(this.GetRecommendedChannel());
    }
  }

  public void CreateRoomChatWithCoop()
  {
    this.CreateRoomChat((IChatConnection) MonoBehaviourSingleton<CoopManager>.I.CreateChatConnection());
  }

  public void CreateRoomChatWithCoopIfNeeded()
  {
    if (this.roomChat != null && !(this.roomChat.connection is ChatCoopConnection))
      return;
    this.CreateRoomChat((IChatConnection) MonoBehaviourSingleton<CoopManager>.I.CreateChatConnection());
  }

  public void CreateRoomChatWithParty()
  {
    this.CreateRoomChat((IChatConnection) MonoBehaviourSingleton<PartyNetworkManager>.I.CreateChatConnection());
  }

  public void SwitchRoomChatConnectionToCoopConnection()
  {
    this.SwitchRoomChatConnection((IChatConnection) MonoBehaviourSingleton<CoopManager>.I.CreateChatConnection());
  }

  public void SwitchRoomChatConnectionToPartyConnection()
  {
    this.SwitchRoomChatConnection((IChatConnection) MonoBehaviourSingleton<PartyNetworkManager>.I.CreateChatConnection());
  }

  private void SwitchRoomChatConnection(IChatConnection connection)
  {
    if (this.roomChat == null)
      return;
    IChatConnection conn = this.roomChat.connection;
    this.roomChat.Disconnect((System.Action) (() =>
    {
      ChatWebSocketConnection socketConnection = conn as ChatWebSocketConnection;
      if (Object.op_Inequality((Object) socketConnection, (Object) null))
        Object.Destroy((Object) socketConnection);
      this.roomChat.SetConnection(connection);
      this.roomChat.JoinRoom(0);
    }));
  }

  private void CreateRoomChat(IChatConnection conn)
  {
    if (this.roomChat != null)
    {
      this.roomChat.Disconnect();
      if (this.OnDestroyRoomChat != null)
        this.OnDestroyRoomChat(this.roomChat);
      this.roomChat = (ChatRoom) null;
    }
    this.roomChat = new ChatRoom();
    this.roomChat.SetConnection(conn);
    if (this.OnCreateRoomChat == null)
      return;
    this.OnCreateRoomChat();
  }

  public void DestroyRoomChat()
  {
    if (this.roomChat == null)
      return;
    IChatConnection conn = this.roomChat.connection;
    this.roomChat.Disconnect((System.Action) (() =>
    {
      ChatWebSocketConnection socketConnection = conn as ChatWebSocketConnection;
      if (!Object.op_Inequality((Object) socketConnection, (Object) null))
        return;
      Object.Destroy((Object) socketConnection);
    }));
    if (this.OnDestroyRoomChat != null)
      this.OnDestroyRoomChat(this.roomChat);
    this.roomChat = (ChatRoom) null;
  }

  public void CreateLoungeChat(IChatConnection conn)
  {
    if (this.loungeChat != null)
    {
      this.loungeChat.Disconnect();
      if (this.OnDestroyLoungeChat != null)
        this.OnDestroyLoungeChat(this.loungeChat);
      this.loungeChat = (ChatRoom) null;
    }
    this.loungeChat = new ChatRoom();
    this.loungeChat.SetConnection(conn);
    if (this.OnCreateLoungeChat == null)
      return;
    this.OnCreateLoungeChat(this.loungeChat);
  }

  public void DestroyLoungeChat()
  {
    if (this.loungeChat == null)
      return;
    IChatConnection connection = this.loungeChat.connection;
    this.loungeChat.Disconnect();
    if (this.OnDestroyLoungeChat != null)
      this.OnDestroyLoungeChat(this.loungeChat);
    this.loungeChat = (ChatRoom) null;
  }

  public void CreateClanChat(ChatChannelInfo info, int clanId, Action<bool> callback = null)
  {
  }

  public void CreateClanChat(IChatConnection conn)
  {
    if (this.clanChat != null)
      return;
    this.clanChat = new ChatRoom();
    this.clanChat.SetConnection(conn);
    if (this.OnCreateClanChat == null)
      return;
    this.OnCreateClanChat(this.clanChat);
  }

  public void DestroyClanChat()
  {
    if (this.clanChat == null)
      return;
    IChatConnection connection = this.clanChat.connection;
    this.clanChat.Disconnect();
    if (this.OnDestroyClanChat != null)
      this.OnDestroyClanChat(this.clanChat);
    this.clanChat = (ChatRoom) null;
  }

  public void SendChannelList(Action<bool> call_back)
  {
    Protocol.Send<ChatServerChannelListModel>(ChatServerChannelListModel.URL, (Action<ChatServerChannelListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.OnNotifyUpdateChannnelInfo(ret.result.chat);
      }
      call_back(flag);
    }));
  }

  public void SendChannelEnter(int channel, Action<ChatChannel> call_back)
  {
    Protocol.Send<ChatServerChannelEnterModel.RequestSendForm, ChatServerChannelEnterModel>(ChatServerChannelEnterModel.URL, new ChatServerChannelEnterModel.RequestSendForm()
    {
      channel = channel
    }, (Action<ChatServerChannelEnterModel>) (ret =>
    {
      ChatChannel chatChannel = (ChatChannel) null;
      if (ret.Error == Error.None)
        chatChannel = ret.result.channel;
      call_back(chatChannel);
    }));
  }

  public void SendClanChannelEnter(int channel, Action<ChatChannel> call_back)
  {
    Protocol.Send<GuildChatChannelEnterModel.RequestSendForm, GuildChatChannelEnterModel>(GuildChatChannelEnterModel.URL, new GuildChatChannelEnterModel.RequestSendForm()
    {
      channel = channel
    }, (Action<GuildChatChannelEnterModel>) (ret =>
    {
      ChatChannel chatChannel = (ChatChannel) null;
      if (ret.Error == Error.None)
        chatChannel = ret.result.channel;
      call_back(chatChannel);
    }));
  }
}
