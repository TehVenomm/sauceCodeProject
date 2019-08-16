using System;

public class ChatCoopConnection : IChatConnection
{
	private bool established;

	public bool isEstablished => established;

	public event ChatRoom.OnJoin onJoin;

	public event ChatRoom.OnJoinClan onJoinClan;

	public event ChatRoom.OnReceiveText onReceiveText;

	public event ChatRoom.OnReceiveStamp onReceiveStamp;

	public event ChatRoom.OnReceiveNotification onReceiveNotification;

	public event ChatRoom.OnAfterSendUserMessage onAfterSendUserMessage;

	public event ChatRoom.OnDisconnect onDisconnect;

	public event ChatRoom.OnReceiveText onReceivePrivateText;

	public event ChatRoom.OnReceiveStamp onReceivePrivateStamp;

	public void Connect()
	{
		established = true;
	}

	public void Disconnect(Action onFinished = null)
	{
		established = false;
		onFinished?.Invoke();
	}

	public void Join(int roomNo, string userName)
	{
		established = true;
		if (this.onJoin != null)
		{
			this.onJoin(CHAT_ERROR_TYPE.NO_ERROR);
		}
	}

	public void SendText(string message)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatMessage(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), message);
		}
		else
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (self == null || !self.get_gameObject().get_activeInHierarchy() || !self.uiPlayerStatusGizmo.get_isActiveAndEnabled())
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatMessage(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), message);
			}
			else
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.ChatSay(message);
				if (MonoBehaviourSingleton<StageObjectManager>.I.self.IsCoopNone() && QuestManager.IsValidInGameExplore())
				{
					MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatMessage(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), message);
				}
			}
		}
		OnReceiveMessage(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, message, string.Empty);
	}

	public void SendStamp(int stampId)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			if (QuestManager.IsValidInGameExplore())
			{
				MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendChatStamp(stampId);
			}
			else
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatStamp(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), stampId);
			}
		}
		else
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (self == null || !self.get_gameObject().get_activeInHierarchy() || !self.uiPlayerStatusGizmo.get_isActiveAndEnabled())
			{
				if (QuestManager.IsValidInGameExplore())
				{
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendChatStamp(stampId);
				}
				else
				{
					MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatStamp(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), stampId);
				}
			}
			else
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.ChatSayStamp(stampId);
				if (MonoBehaviourSingleton<StageObjectManager>.I.self.IsCoopNone() && QuestManager.IsValidInGameExplore())
				{
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendChatStamp(stampId);
				}
			}
		}
		OnReceiveStamp(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, stampId, string.Empty);
	}

	public void SendPrivateText(string target_id, string message)
	{
	}

	public void SendPrivateStamp(string target_id, int stampId)
	{
	}

	public void OnReceiveMessage(int userId, string userName, string message, string chatItemId = "")
	{
		if (isEstablished && this.onReceiveText != null)
		{
			this.onReceiveText(userId, userName, message, chatItemId);
		}
	}

	public void OnReceiveStamp(int userId, string userName, int stampId, string chatItemId = "")
	{
		if (isEstablished && this.onReceiveStamp != null)
		{
			this.onReceiveStamp(userId, userName, stampId, chatItemId);
		}
	}

	public void OnReceiveNotification(string message, string chatItemId = "")
	{
		if (isEstablished && this.onReceiveNotification != null)
		{
			this.onReceiveNotification(message, chatItemId);
		}
	}
}
