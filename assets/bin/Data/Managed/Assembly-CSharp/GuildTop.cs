using System.Collections;

public class GuildTop : HomeBase
{
	private enum UI
	{
		OBJ_NOTICE,
		LBL_NOTICE,
		BTN_STORAGE,
		BTN_MISSION,
		BTN_TICKET,
		BTN_GIFTBOX,
		BTN_CHAT,
		OBJ_BALOON_ROOT,
		OBJ_EXPLORE_BALLOON_POS,
		BTN_CHAIR,
		OBJ_NORMAL_NOTICE,
		OBJ_BUTTON_NOTICE,
		OBJ_NOTICE_LOCK,
		LBL_NOTICE_LOCK
	}

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DDoInitialize());
	}

	private IEnumerator DDoInitialize()
	{
		bool is_finish = false;
		MonoBehaviourSingleton<GuildManager>.I.SendClanInfo(delegate(bool success)
		{
			((_003CDDoInitialize_003Ec__Iterator7C)/*Error near IL_002d: stateMachine*/)._003Cis_finish_003E__0 = true;
			if (success)
			{
				MonoBehaviourSingleton<ChatManager>.I.clanChat.onLeave += ((_003CDDoInitialize_003Ec__Iterator7C)/*Error near IL_002d: stateMachine*/)._003C_003Ef__this._OnLeave;
			}
		});
		while (!is_finish)
		{
			yield return (object)null;
		}
		MonoBehaviourSingleton<GuildManager>.I.GetClanStat(null);
		base.Initialize();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		MonoBehaviourSingleton<ChatManager>.I.clanChat.onLeave -= _OnLeave;
	}

	protected override void CreateSelfCharacter()
	{
		MonoBehaviourSingleton<GuildStageManager>.I.HomePeople.CreateSelfCharacter(base.OnNoticeAreaEvent);
	}

	protected override void SetupLoginBonus()
	{
	}

	protected override void SetupPointShop()
	{
	}

	protected override bool CheckInvitedLoungeBySNS()
	{
		return false;
	}

	protected override IEnumerator SendHomeInfo()
	{
		bool is_finish = false;
		MonoBehaviourSingleton<GuildManager>.I.SendClanInfo(delegate
		{
			((_003CSendHomeInfo_003Ec__Iterator7D)/*Error near IL_002d: stateMachine*/)._003Cis_finish_003E__0 = true;
		});
		while (!is_finish)
		{
			yield return (object)null;
		}
		MonoBehaviourSingleton<GuildManager>.I.GetClanStat(null);
	}

	protected override IEnumerator WaitLoadHomeCharacters()
	{
		while (MonoBehaviourSingleton<GuildStageManager>.I.HomePeople.selfChara.isLoading || !MonoBehaviourSingleton<GuildStageManager>.I.HomePeople.isPeopleInitialized)
		{
			yield return (object)null;
		}
	}

	protected override IEnumerator WaitInitializeManager()
	{
		while (!MonoBehaviourSingleton<GuildStageManager>.I.IsInitialized)
		{
			yield return (object)null;
		}
	}

	protected override bool CheckNeededOpenQuest()
	{
		return false;
	}

	protected override void AddChatClickDelegate(UIButton btnChat)
	{
	}

	protected override void InitializeChat()
	{
	}

	private void OnQuery_EXIT()
	{
		if (MonoBehaviourSingleton<ChatManager>.I.clanChat != null && MonoBehaviourSingleton<ChatManager>.I.clanChat.HasConnect)
		{
			MonoBehaviourSingleton<ChatManager>.I.DestroyClanChat();
		}
	}

	private void _OnLeave(CHAT_ERROR_TYPE errorType, string userId)
	{
	}

	protected override bool CheckNeededGotoGacha()
	{
		if (!string.IsNullOrEmpty(MonoBehaviourSingleton<UserInfoManager>.I.oncePurchaseGachaProductId))
		{
			EventData[] autoEvents = new EventData[2]
			{
				new EventData("MAIN_MENU_SHOP", null),
				new EventData("FORCE_ONCE_PURCHASE_GACHA", MonoBehaviourSingleton<UserInfoManager>.I.oncePurchaseGachaProductId)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			return true;
		}
		return false;
	}

	protected override void CheckEventLock()
	{
	}
}
