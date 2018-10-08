using System;
using UnityEngine;

public class QuestRoomObserver : MonoBehaviour
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

	public static void OffObserve()
	{
		isObserve = false;
	}

	public QuestRoomObserver Initialize(bool from_search_section, bool is_entry_pass, Action<string> _dispatch_callback, Action<string> _change_event_callback, Action _stay_event_callback, Action<bool> _resume_event_callback, bool? is_update_observe = default(bool?))
	{
		fromSearchSection = from_search_section;
		isEntryPass = is_entry_pass;
		dispatchCallBack = _dispatch_callback;
		changeEventCallBack = _change_event_callback;
		stayEventCallBack = _stay_event_callback;
		resumeEventCallBack = _resume_event_callback;
		isObserve = ((!is_update_observe.HasValue) ? isObserve : is_update_observe.Value);
		section = base.gameObject.GetComponent<GameSection>();
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

	private void Update()
	{
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
									GameSceneEvent.Resume(null);
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
					if ((UnityEngine.Object)section != (UnityEngine.Object)null && section.sectionData != (GameSceneTables.SectionData)null && section.sectionData.sectionName == currentSectionName && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
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
				Protocol.Try(delegate
				{
					MonoBehaviourSingleton<PartyManager>.I.SendInfo(delegate
					{
					});
				});
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
				stayEventCallBack();
				resumeEventCallBack(true);
			}
		}
	}
}
