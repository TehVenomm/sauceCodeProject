using System;

public class ChatState_ClanTab : ChatState
{
	private float updateTimer;

	public override void Enter(MainChat _manager)
	{
		base.Enter(_manager);
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			m_manager.UseNoClanBlock = false;
			updateTimer = MonoBehaviourSingleton<ClanMatchingManager>.I.chatUpdateInterval;
			string cId = MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
			if (MonoBehaviourSingleton<ClanMatchingManager>.I.CachedMessageClanId != cId)
			{
				reloadChat();
			}
		}
		else
		{
			updateTimer = 0f;
			m_manager.UseNoClanBlock = true;
			m_manager.CurrentData.Reset();
		}
		m_manager.IsDraging = false;
		m_manager.UpdateSendBlock();
		EndInitialize();
	}

	public override void Update(float _deltaTime)
	{
		if (!MonoBehaviourSingleton<ClanMatchingManager>.IsValid() || !MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			return;
		}
		if (m_manager.UseNoClanBlock)
		{
			m_manager.UseNoClanBlock = false;
			m_manager.UpdateSendBlock();
		}
		updateTimer -= _deltaTime;
		if (!(updateTimer <= 0f))
		{
			return;
		}
		if (m_manager.CurrentData.itemList.Count == 0)
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100);
		}
		else if (m_manager.CurrentData.newestIndex >= 0)
		{
			ChatItem chatItem = m_manager.CurrentData.itemList[m_manager.CurrentData.newestIndex];
			if (chatItem.gameObject.activeSelf && chatItem.chatItemId == MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetLatestCacheId())
			{
				MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100, chatItem.chatItemId);
			}
		}
		int num = MonoBehaviourSingleton<ClanMatchingManager>.I.chatUpdateInterval;
		if (num < 1)
		{
			num = 1;
		}
		updateTimer = num;
	}

	public override void Exit()
	{
		m_manager.UseNoClanBlock = false;
		m_manager.IsDraging = false;
		m_manager.UpdateSendBlock();
		base.Exit();
	}

	public override void OnDragAtTop(string chatItemId, float dragpower)
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			if (m_manager.CurrentData.itemList.Count == 0)
			{
				MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100, chatItemId);
			}
			else
			{
				MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetOldMessage(getDispatchNum(dragpower), chatItemId);
			}
		}
	}

	public override void OnDragAtBottom(string chatItemId, float dragpower)
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			if (m_manager.CurrentData.itemList.Count == 0)
			{
				MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100, chatItemId);
			}
			else
			{
				MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(getDispatchNum(dragpower), chatItemId);
			}
		}
	}

	public override void OnTapHeaderTab(MainChat.CHAT_TYPE chatType)
	{
		if (base.IsInitialized && chatType == MainChat.CHAT_TYPE.CLAN)
		{
			reloadChat();
		}
	}

	private int getDispatchNum(float dragpower)
	{
		int num = 1;
		if (dragpower > 0.3f)
		{
			num += 4;
		}
		if (dragpower > 1.2f)
		{
			num += 5;
		}
		return num;
	}

	public override Type GetNextState()
	{
		if (m_manager == null || !base.IsInitialized)
		{
			return base.GetNextState();
		}
		return m_manager.GetTopState();
	}

	public override void OnShowMessageOnDisplay(string chatItemId)
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.OnReadMessage(chatItemId);
		}
	}

	private void reloadChat()
	{
		m_manager.ResetCacheData(MainChat.CHAT_TYPE.CLAN);
		MonoBehaviourSingleton<ClanMatchingManager>.I.ChatResetCache();
		MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100);
	}
}
