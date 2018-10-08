using System;

public class QuestRoomObserver
{
	public bool fromSearchSection;

	public bool isEntryPass;

	private Action<string> dispatchCallBack;

	private Action<string> changeEventCallBack;

	private Action stayEventCallBack;

	private Action<bool> resumeEventCallBack;

	private bool queryInvalidRoom;

	private bool checkInviteListDone;

	private bool isSendingInviteList;

	private bool isStayEvent;

	private static bool isObserve;

	private GameSection section;

	private SpanTimer sendInfoSpan = new SpanTimer(5f);

	public QuestRoomObserver()
		: this()
	{
	}

	public static void OffObserve()
	{
		isObserve = false;
	}

	public QuestRoomObserver Initialize(bool from_search_section, bool is_entry_pass, Action<string> _dispatch_callback, Action<string> _change_event_callback, Action _stay_event_callback, Action<bool> _resume_event_callback, bool? is_update_observe = default(bool?))
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		fromSearchSection = from_search_section;
		isEntryPass = is_entry_pass;
		dispatchCallBack = _dispatch_callback;
		changeEventCallBack = _change_event_callback;
		stayEventCallBack = _stay_event_callback;
		resumeEventCallBack = _resume_event_callback;
		isObserve = ((!is_update_observe.HasValue) ? isObserve : is_update_observe.Value);
		section = this.get_gameObject().GetComponent<GameSection>();
		return this;
	}

	public bool IsValidParty()
	{
		return PartyManager.IsValidInParty();
	}

	public bool IsConnect()
	{
		return true;
	}

	public bool IsQueryInvalidRoomEvent()
	{
		return queryInvalidRoom;
	}

	private unsafe void Update()
	{
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		if (isObserve)
		{
			if ((!IsValidParty() || !IsConnect()) && !queryInvalidRoom)
			{
				if (!checkInviteListDone)
				{
					if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
					{
						if (!isSendingInviteList)
						{
							if (!GameSceneEvent.IsStay())
							{
								GameSceneEvent.Stay();
								isStayEvent = true;
							}
							MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty(delegate
							{
								if (isStayEvent)
								{
									GameSceneEvent.Resume(null, false);
								}
								checkInviteListDone = true;
							}, false);
							isSendingInviteList = true;
						}
					}
					else
					{
						checkInviteListDone = true;
					}
				}
				else
				{
					string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
					if (section != null && section.sectionData != (GameSceneTables.SectionData)null && section.sectionData.sectionName == currentSectionName && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
					{
						queryInvalidRoom = true;
						if (dispatchCallBack != null)
						{
							dispatchCallBack("INVALID_ROOM");
							OffObserve();
						}
					}
				}
			}
			else if (sendInfoSpan.IsReady())
			{
				if (_003C_003Ef__am_0024cacheD == null)
				{
					_003C_003Ef__am_0024cacheD = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				Protocol.Try(_003C_003Ef__am_0024cacheD);
			}
		}
	}

	public void SetupBackSectionEvent()
	{
		if (changeEventCallBack != null && stayEventCallBack != null && resumeEventCallBack != null && fromSearchSection)
		{
			changeEventCallBack((!isEntryPass) ? "BACK_ROOM_SEARCH" : "BACK_INPUT_PASS");
			if (!isEntryPass)
			{
				stayEventCallBack.Invoke();
				resumeEventCallBack(true);
			}
		}
	}
}
