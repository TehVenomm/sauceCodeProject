using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptWaveDetail : QuestDeliveryDetail
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

	private const string WINDOW_SPRITE = "RequestWindowBase";

	private const string MESSAGE_SPRITE = "RequestFukidashi";

	private Network.EventData eventData;

	private QuestTable.QuestTableData questTableData;

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
		if (questTableData != null)
		{
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questTableData.GetMainEnemyID());
			if (enemyData != null)
			{
				ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, null, GetCtrl(UI.OBJ_ENEMY));
				itemIcon.SetDepth(7);
				SetElementSprite((Enum)UI.SPR_ENM_ELEMENT, (int)enemyData.element);
				int num = (int)questTableData.limitTime;
				SetLabelText((Enum)UI.LBL_LIMIT_TIME, $"{num / 60:D2}:{num % 60:D2}");
				SetDifficultySprite();
				SetSprite(baseRoot, UI.SPR_WINDOW, "RequestWindowBase");
				SetSprite(baseRoot, UI.SPR_MESSAGE_BG, "RequestFukidashi");
				SetActive(baseRoot, UI.OBJ_COMPLETE_ROOT, base.isComplete);
			}
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

	private void SetDifficultySprite()
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		SetActive((Enum)UI.SPR_TYPE_DIFFICULTY, (deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}

	private void OnQuery_CREATE()
	{
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
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int)info.needs[0].questId, retryCount, isExplore: false, delegate(bool is_success, int maxRetryCount, bool isJoined, float waitTime)
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
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int)info.needs[0].questId, retryCount, isExplore: false, delegate(bool is_success, int maxRetryCount, bool isJoined, float waitTime)
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
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(info.needs[0].questId);
		if (questTableData != null)
		{
			GameSection.SetEventData(new object[1]
			{
				questTableData.questType
			});
		}
		PartyManager.PartySetting setting = new PartyManager.PartySetting(is_lock: false, 0, 0);
		MonoBehaviourSingleton<PartyManager>.I.SendCreate((int)info.needs[0].questId, setting, delegate(bool is_success)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
			}
			GameSection.ResumeEvent(is_success);
		});
	}
}
