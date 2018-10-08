using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviourSingleton<ChatManager>
{
	private const int CLAN_CHANNEL = 1;

	private ChatChannel offlineChannel = new ChatChannel
	{
		host = "offline",
		channel = -1
	};

	private ChatChannel invalidChannel = new ChatChannel
	{
		host = "invalid",
		channel = -1
	};

	public ChatRoom homeChat
	{
		get;
		private set;
	}

	public ChatRoom roomChat
	{
		get;
		private set;
	}

	public ChatRoom loungeChat
	{
		get;
		private set;
	}

	public ClanChatRoom clanChat
	{
		get;
		private set;
	}

	public ChatRoom ClanChat
	{
		get;
		private set;
	}

	public ChatChannelInfo chatChannelInfo
	{
		get;
		private set;
	}

	public ChatChannel currentChannel
	{
		get;
		private set;
	}

	public event Action OnCreateRoomChat;

	public event Action<ChatRoom> OnDestroyRoomChat;

	public event Action<ChatRoom> OnCreateLoungeChat;

	public event Action<ChatRoom> OnDestroyLoungeChat;

	public void OnNotifyUpdateChannnelInfo(ChatChannelInfo info)
	{
		chatChannelInfo = info;
		if (currentChannel == null || currentChannel == invalidChannel)
		{
			int recommend = chatChannelInfo.recommend;
			SelectChannel(recommend);
		}
	}

	public List<int> GetChannels()
	{
		if (chatChannelInfo != null)
		{
			return chatChannelInfo.channels;
		}
		return null;
	}

	public ChatChannel GetCurrentChannel()
	{
		if (currentChannel == null)
		{
			return invalidChannel;
		}
		return currentChannel;
	}

	public int GetHotChannel()
	{
		if (chatChannelInfo != null)
		{
			return chatChannelInfo.hot;
		}
		return invalidChannel.channel;
	}

	public int GetColdChannel()
	{
		if (chatChannelInfo != null)
		{
			return chatChannelInfo.cold;
		}
		return invalidChannel.channel;
	}

	public int GetRecommendedChannel()
	{
		if (chatChannelInfo != null)
		{
			return chatChannelInfo.recommend;
		}
		return invalidChannel.channel;
	}

	public void SelectChannel(int channel)
	{
		if (channel > 0 && (currentChannel == null || currentChannel.channel != channel || !homeChat.HasConnect))
		{
			Protocol.Force(delegate
			{
				SendChannelEnter(channel, delegate(ChatChannel chatChannel)
				{
					if (chatChannel != null)
					{
						if (homeChat != null && homeChat.connection != null)
						{
							homeChat.Disconnect(delegate
							{
								ChatWebSocketConnection chatWebSocketConnection = homeChat.connection as ChatWebSocketConnection;
								if ((bool)chatWebSocketConnection)
								{
									UnityEngine.Object.Destroy(chatWebSocketConnection);
								}
								ConnectHomeChat(chatChannel);
							});
						}
						else
						{
							ConnectHomeChat(chatChannel);
						}
					}
				});
			});
		}
	}

	private void ConnectHomeChat(ChatChannel channel)
	{
		currentChannel = channel;
		if (channel == invalidChannel || channel == offlineChannel)
		{
			homeChat.SetConnection(new ChatOfflineConnection());
		}
		else
		{
			ChatWebSocketConnection chatWebSocketConnection = base.gameObject.AddComponent<ChatWebSocketConnection>();
			chatWebSocketConnection.Setup(channel.host, channel.port, channel.path, true);
			homeChat.SetConnection(chatWebSocketConnection);
			int roomNo = 1;
			homeChat.JoinRoom(roomNo);
		}
	}

	public void CreateHomeChat()
	{
		if (homeChat != null)
		{
			if (currentChannel == invalidChannel)
			{
				int recommendedChannel = GetRecommendedChannel();
				SelectChannel(recommendedChannel);
			}
		}
		else
		{
			homeChat = new ChatRoom();
			int recommendedChannel2 = GetRecommendedChannel();
			SelectChannel(recommendedChannel2);
		}
	}

	public void CreateRoomChatWithCoop()
	{
		ChatCoopConnection conn = MonoBehaviourSingleton<CoopManager>.I.CreateChatConnection();
		CreateRoomChat(conn);
	}

	public void CreateRoomChatWithCoopIfNeeded()
	{
		if (roomChat == null || roomChat.connection is ChatCoopConnection)
		{
			ChatCoopConnection conn = MonoBehaviourSingleton<CoopManager>.I.CreateChatConnection();
			CreateRoomChat(conn);
		}
	}

	public void CreateRoomChatWithParty()
	{
		ChatPartyConnection conn = MonoBehaviourSingleton<PartyNetworkManager>.I.CreateChatConnection();
		CreateRoomChat(conn);
	}

	public void SwitchRoomChatConnectionToCoopConnection()
	{
		ChatCoopConnection connection = MonoBehaviourSingleton<CoopManager>.I.CreateChatConnection();
		SwitchRoomChatConnection(connection);
	}

	public void SwitchRoomChatConnectionToPartyConnection()
	{
		ChatPartyConnection connection = MonoBehaviourSingleton<PartyNetworkManager>.I.CreateChatConnection();
		SwitchRoomChatConnection(connection);
	}

	private void SwitchRoomChatConnection(IChatConnection connection)
	{
		if (roomChat != null)
		{
			IChatConnection conn = roomChat.connection;
			roomChat.Disconnect(delegate
			{
				ChatWebSocketConnection chatWebSocketConnection = conn as ChatWebSocketConnection;
				if ((UnityEngine.Object)chatWebSocketConnection != (UnityEngine.Object)null)
				{
					UnityEngine.Object.Destroy(chatWebSocketConnection);
				}
				roomChat.SetConnection(connection);
				roomChat.JoinRoom(0);
			});
		}
	}

	private void CreateRoomChat(IChatConnection conn)
	{
		if (roomChat != null)
		{
			roomChat.Disconnect(null);
			if (this.OnDestroyRoomChat != null)
			{
				this.OnDestroyRoomChat(roomChat);
			}
			roomChat = null;
		}
		roomChat = new ChatRoom();
		roomChat.SetConnection(conn);
		if (this.OnCreateRoomChat != null)
		{
			this.OnCreateRoomChat();
		}
	}

	public void DestroyRoomChat()
	{
		if (roomChat != null)
		{
			IChatConnection conn = roomChat.connection;
			roomChat.Disconnect(delegate
			{
				ChatWebSocketConnection chatWebSocketConnection = conn as ChatWebSocketConnection;
				if ((UnityEngine.Object)chatWebSocketConnection != (UnityEngine.Object)null)
				{
					UnityEngine.Object.Destroy(chatWebSocketConnection);
				}
			});
			if (this.OnDestroyRoomChat != null)
			{
				this.OnDestroyRoomChat(roomChat);
			}
			roomChat = null;
		}
	}

	public void CreateLoungeChat(IChatConnection conn)
	{
		if (loungeChat != null)
		{
			loungeChat.Disconnect(null);
			if (this.OnDestroyLoungeChat != null)
			{
				this.OnDestroyLoungeChat(loungeChat);
			}
			loungeChat = null;
		}
		loungeChat = new ChatRoom();
		loungeChat.SetConnection(conn);
		if (this.OnCreateLoungeChat != null)
		{
			this.OnCreateLoungeChat(loungeChat);
		}
	}

	public void DestroyLoungeChat()
	{
		if (loungeChat != null)
		{
			IChatConnection connection = loungeChat.connection;
			loungeChat.Disconnect(null);
			if (this.OnDestroyLoungeChat != null)
			{
				this.OnDestroyLoungeChat(loungeChat);
			}
			loungeChat = null;
		}
	}

	public void CreateClanChat(ChatChannelInfo info, int clanId, Action<bool> callback = null)
	{
		if (info == null)
		{
			Log.Error(LOG.NETWORK, "clanChat info is null!!");
			if (callback != null)
			{
				callback(false);
			}
		}
		else if (clanChat != null && clanChat.HasConnect)
		{
			Log.Error(LOG.NETWORK, "ClanChat has already connected!");
			if (callback != null)
			{
				callback(true);
			}
		}
		else if (clanChat != null)
		{
			Log.Warning(LOG.NETWORK, "ClanChat reconnect!");
			clanChat.JoinRoom(clanId);
			if (callback != null)
			{
				callback(true);
			}
		}
		else
		{
			clanChat = new ClanChatRoom();
			Protocol.Force(delegate
			{
				SendClanChannelEnter(info.recommend, delegate(ChatChannel chatChannel)
				{
					if (chatChannel == null)
					{
						Log.Error(LOG.NETWORK, "Clan Chat channel is null");
						clanChat.LeaveRoom(clanId);
						if (callback != null)
						{
							callback(false);
						}
					}
					else if (chatChannel == invalidChannel || chatChannel == offlineChannel)
					{
						clanChat.SetConnection(new ClanChatOfflineConnection());
						Log.Error(LOG.NETWORK, "Use Clan chat Offline Connection");
						if (callback != null)
						{
							callback(true);
						}
					}
					else
					{
						ClanChatWebSocketConnection clanChatWebSocketConnection = base.gameObject.AddComponent<ClanChatWebSocketConnection>();
						clanChatWebSocketConnection.Setup(chatChannel.host, chatChannel.port, chatChannel.path, true);
						clanChat.SetConnection(clanChatWebSocketConnection);
						clanChat.JoinRoom(clanId);
						Log.Error(LOG.NETWORK, "Create Clan Chat Successful !");
						if (callback != null)
						{
							callback(true);
						}
					}
				});
			});
		}
	}

	public void DestroyClanChat()
	{
		if (clanChat != null)
		{
			Log.Error(LOG.NETWORK, "Destroy clan chat successful!");
			clanChat.LeaveRoom(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId);
			clanChat.Disconnect(null);
		}
	}

	public void SendChannelList(Action<bool> call_back)
	{
		Protocol.Send(ChatServerChannelListModel.URL, delegate(ChatServerChannelListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				OnNotifyUpdateChannnelInfo(ret.result.chat);
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendChannelEnter(int channel, Action<ChatChannel> call_back)
	{
		ChatServerChannelEnterModel.RequestSendForm requestSendForm = new ChatServerChannelEnterModel.RequestSendForm();
		requestSendForm.channel = channel;
		Protocol.Send(ChatServerChannelEnterModel.URL, requestSendForm, delegate(ChatServerChannelEnterModel ret)
		{
			ChatChannel obj = null;
			if (ret.Error == Error.None)
			{
				obj = ret.result.channel;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendClanChannelEnter(int channel, Action<ChatChannel> call_back)
	{
		GuildChatChannelEnterModel.RequestSendForm requestSendForm = new GuildChatChannelEnterModel.RequestSendForm();
		requestSendForm.channel = channel;
		Protocol.Send(GuildChatChannelEnterModel.URL, requestSendForm, delegate(GuildChatChannelEnterModel ret)
		{
			ChatChannel obj = null;
			if (ret.Error == Error.None)
			{
				obj = ret.result.channel;
			}
			call_back(obj);
		}, string.Empty);
	}
}
