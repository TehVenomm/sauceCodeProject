using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptExploreDetail : QuestDeliveryDetail
{
	protected new enum UI
	{
		OBJ_BASE_ROOT,
		OBJ_BACK,
		OBJ_COMPLETE_ROOT,
		BTN_COMPLETE,
		CHARA_ALL,
		OBJ_UNLOCK_PORTAL_ROOT,
		LBL_UNLOCK_PORTAL,
		LBL_QUEST_TITLE,
		LBL_CHARA_MESSAGE,
		LBL_PERSON_NAME,
		TEX_NPC,
		BTN_JUMP_QUEST,
		BTN_JUMP_INVALID,
		BTN_JUMP_MAP,
		BTN_JUMP_GACHATOP,
		GRD_REWARD,
		LBL_MONEY,
		LBL_EXP,
		SPR_WINDOW,
		SPR_MESSAGE_BG,
		OBJ_NEED_ITEM_ROOT,
		LBL_NEED_ITEM_NAME,
		LBL_NEED,
		LBL_HAVE,
		LBL_PLACE_NAME,
		LBL_ENEMY_NAME,
		OBJ_DIFFICULTY_ROOT,
		OBJ_ENEMY_NAME_ROOT,
		LBL_GET_PLACE,
		OBJ_ENEMY,
		SPR_ELEMENT_ROOT,
		SPR_ENM_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		BTN_SUBMISSION,
		STR_BTN_SUBMISSION,
		STR_BTN_SUBMISSION_BACK,
		OBJ_TOP_CROWN_ROOT,
		OBJ_TOP_CROWN_1,
		OBJ_TOP_CROWN_2,
		OBJ_TOP_CROWN_3,
		STR_MISSION_EMPTY,
		SPR_CROWN_1,
		SPR_CROWN_2,
		SPR_CROWN_3,
		OBJ_SUBMISSION_ROOT,
		OBJ_MISSION_INFO,
		OBJ_MISSION_INFO_1,
		OBJ_MISSION_INFO_2,
		OBJ_MISSION_INFO_3,
		LBL_MISSION_INFO_1,
		LBL_MISSION_INFO_2,
		LBL_MISSION_INFO_3,
		SPR_MISSION_INFO_CROWN_1,
		SPR_MISSION_INFO_CROWN_2,
		SPR_MISSION_INFO_CROWN_3,
		STR_MISSION,
		OBJ_BASE_FRAME,
		OBJ_TARGET_FRAME,
		OBJ_SUBMISSION_FRAME,
		OBJ_NORMAL_ROOT,
		OBJ_EVENT_ROOT,
		LBL_POINT_NORMAL,
		TEX_NORMAL_ICON,
		LBL_POINT_EVENT,
		TEX_EVENT_ICON,
		BTN_CREATE,
		BTN_JOIN,
		BTN_MATCHING,
		BTN_JUMP_SMITH,
		BTN_JUMP_STATUS,
		BTN_JUMP_STORAGE,
		BTN_JUMP_POINT_SHOP,
		BTN_JUMP_WORLDMAP,
		BTN_WAVEMATCH_NEW,
		BTN_WAVEMATCH_PASS,
		BTN_WAVEMATCH_AUTO,
		OBJ_CARNIVAL_ROOT,
		LBL_POINT_CARNIVAL,
		LBL_LIMIT_TIME,
		OBJ_CLEAR_ICON_ROOT,
		OBJ_CLEAR_REWARD,
		BTN_CREATE_OFF,
		SPR_TYPE_DIFFICULTY
	}

	private Network.EventData eventData;

	private QuestTable.QuestTableData questTableData;

	private const string WINDOW_SPRITE = "RequestWindowBase_Explorer";

	private const string MESSAGE_SPRITE = "Checkhukidashi_Explorer";

	public override void Initialize()
	{
		base.Initialize();
		List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
		foreach (Network.EventData item in eventList)
		{
			if (item.eventId == info.eventID)
			{
				eventData = item;
				break;
			}
		}
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_CLEAR_REWARD, is_visible: true);
		base.UpdateUI();
		questTableData = info.GetQuestData();
		if (questTableData == null)
		{
			return;
		}
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questTableData.GetMainEnemyID());
		if (enemyData != null)
		{
			ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, null, GetCtrl(UI.OBJ_ENEMY));
			itemIcon.SetDepth(7);
			SetElementSprite((Enum)UI.SPR_ENM_ELEMENT, (int)enemyData.element);
			SetElementSprite((Enum)UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive((Enum)UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
			int num = (int)questTableData.limitTime;
			SetLabelText((Enum)UI.LBL_LIMIT_TIME, $"{num / 60:D2}:{num % 60:D2}");
			if ((base.isComplete || isNotice) && !isCompletedEventDelivery)
			{
				SetActive((Enum)UI.BTN_CREATE_OFF, is_visible: false);
			}
			else
			{
				SetActive((Enum)UI.BTN_CREATE, IsCreatableRoom());
				SetActive((Enum)UI.BTN_CREATE_OFF, !IsCreatableRoom());
			}
			SetDifficultySprite();
			SetSprite(baseRoot, UI.SPR_WINDOW, "RequestWindowBase_Explorer");
			SetSprite(baseRoot, UI.SPR_MESSAGE_BG, "Checkhukidashi_Explorer");
			SetActive(baseRoot, UI.OBJ_COMPLETE_ROOT, base.isComplete);
		}
	}

	protected override void SetBaseFrame()
	{
		baseRoot = GetCtrl(UI.OBJ_BASE_FRAME);
	}

	protected override void SetTargetFrame()
	{
		targetFrame = GetCtrl(UI.OBJ_TARGET_FRAME);
	}

	protected override void SetSubmissionFrame()
	{
		submissionFrame = GetCtrl(UI.OBJ_SUBMISSION_FRAME);
	}

	private void OnQuery_SWITCH_SUBMISSION()
	{
		if (Object.op_Implicit(targetFrame) && Object.op_Implicit(submissionFrame))
		{
			bool activeSelf = targetFrame.get_gameObject().get_activeSelf();
			targetFrame.get_gameObject().SetActive(!activeSelf);
			submissionFrame.get_gameObject().SetActive(activeSelf);
			isCompletedEventDelivery = true;
			RefreshUI();
		}
	}

	private void OnQuery_CREATE()
	{
		if (!IsCreatableRoom())
		{
			string text = StringTable.Get(STRING_CATEGORY.MATCHING, 0u);
			object[] array = new object[1]
			{
				text
			};
			GameSection.ChangeEvent("HOST_LIMIT", text);
			return;
		}
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(info.needs[0].questId);
		if (questTableData != null)
		{
			GameSection.SetEventData(new object[1]
			{
				questTableData.questType
			});
		}
	}

	private void OnQuery_JOIN()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(info.needs[0].questId);
	}

	private void OnQuery_MATCHING()
	{
		GameSection.SetEventData(new object[1]
		{
			false
		});
		GameSection.StayEvent();
		int retryCount = 0;
		PartyManager.PartySetting setting = new PartyManager.PartySetting(is_lock: false, 0, 0, 0, 1);
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int)info.needs[0].questId, retryCount, isExplore: true, delegate(bool is_success, int maxRetryCount, bool isJoined, float waitTime)
		{
			if (!is_success)
			{
				GameSection.ResumeEvent(is_resume: false);
			}
			else if (maxRetryCount > 0)
			{
				retryCount++;
				this.StartCoroutine(MatchAtRandom(setting, retryCount, waitTime));
			}
			else if (!isJoined)
			{
				OnQuery_AUTO_CREATE_ROOM();
			}
			else
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
				GameSection.ResumeEvent(is_resume: true);
			}
		});
	}

	private IEnumerator MatchAtRandom(PartyManager.PartySetting setting, int retryCount, float time)
	{
		yield return (object)new WaitForSeconds(time);
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int)info.needs[0].questId, retryCount, isExplore: true, delegate(bool is_success, int maxRetryCount, bool isJoined, float waitTime)
		{
			if (!is_success)
			{
				GameSection.ResumeEvent(is_resume: false);
			}
			else if (maxRetryCount > 0)
			{
				if (retryCount >= maxRetryCount)
				{
					OnQuery_AUTO_CREATE_ROOM();
				}
				else
				{
					retryCount++;
					this.StartCoroutine(MatchAtRandom(setting, retryCount, waitTime));
				}
			}
			else if (!isJoined)
			{
				OnQuery_AUTO_CREATE_ROOM();
			}
			else
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
				GameSection.ResumeEvent(is_resume: true);
			}
		});
	}

	private void OnQuery_AUTO_CREATE_ROOM()
	{
		GameSection.ResumeEvent(is_resume: false);
		string text = StringTable.Get(STRING_CATEGORY.MATCHING, 1u);
		object[] array = new object[1]
		{
			text
		};
		DispatchEvent("HOST_LIMIT", text);
	}

	private bool IsCreatableRoom()
	{
		return eventData.hostCountLimit > 0;
	}

	private void SetDifficultySprite()
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		SetActive((Enum)UI.SPR_TYPE_DIFFICULTY, (deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}
}
