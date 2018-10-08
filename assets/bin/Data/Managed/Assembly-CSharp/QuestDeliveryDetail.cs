using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestDeliveryDetail : GameSection
{
	protected enum UI
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
		SPR_ELEMENT,
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
		BTN_WAVEMATCH_AUTO
	}

	private enum AUDIO
	{
		REQUEST_COMPLETE = 40000029,
		GO_TO_FIELD = 40000124,
		UNLOCKE_PORTAL = 40000161
	}

	public enum JumpButtonType
	{
		Invalid,
		Complete,
		Map,
		Quest,
		Gacha,
		Smith,
		Status,
		Storage,
		PointShop,
		WorldMap,
		WaveRoom,
		seriesRoom
	}

	private readonly string[] SPR_WINDOW_TYPE = new string[5]
	{
		"RequestWindowBase",
		"RequestWindowBase_Event",
		"RequestWindowBase_Story",
		"RequestWindowBase_Hard",
		"RequestWindowBase_Event"
	};

	private readonly string[] SPR_MESSAGE_BG_TYPE = new string[5]
	{
		"CheckHukidashi",
		"Checkhukidashi_Event",
		"Checkhukidashi_Story",
		"Checkhukidashi_Hard",
		"Checkhukidashi_Event"
	};

	protected Transform baseRoot;

	protected Transform targetFrame;

	protected Transform submissionFrame;

	protected int deliveryID;

	protected DeliveryTable.DeliveryData info;

	private DeliveryRewardTable.DeliveryRewardData[] rewardData;

	private DeliveryRewardList competeReward;

	public List<PointShopGetPointTable.Data> pointShopGetPointData;

	private bool isInGameScene;

	protected bool isCompletedEventDelivery;

	protected bool isNotice;

	protected bool completeJumpButton;

	private bool isQuestEnemy;

	private uint targetQuestID;

	private uint targetMapID;

	private int[] targetPortalID;

	private bool hasDispedMessage;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "PointShopGetPointTable";
			yield return "DeliveryRewardTable";
			yield return "FieldMapTable";
		}
	}

	protected bool isComplete => competeReward != null;

	private JumpButtonType GetJumpButtonTypeByQuestType(QUEST_TYPE questType)
	{
		switch (questType)
		{
		case QUEST_TYPE.WAVE:
			return JumpButtonType.WaveRoom;
		case QUEST_TYPE.SERIES:
			return JumpButtonType.seriesRoom;
		default:
			return JumpButtonType.Invalid;
		}
	}

	public override void Initialize()
	{
		isInGameScene = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene");
		object[] array = GameSection.GetEventData() as object[];
		deliveryID = (int)array[0];
		competeReward = (array[1] as DeliveryRewardList);
		if (array.Length >= 3)
		{
			isCompletedEventDelivery = (bool)array[2];
		}
		info = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		rewardData = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData((uint)deliveryID);
		pointShopGetPointData = Singleton<PointShopGetPointTable>.I.GetFromDeiliveryId((uint)deliveryID);
		SetBaseFrame();
		SetTargetFrame();
		SetSubmissionFrame();
		completeJumpButton = false;
		base.Initialize();
	}

	private void OpenTutorial()
	{
		if (HomeTutorialManager.DoesTutorial() && !isInGameScene)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep2_2", null);
		}
	}

	private void CompleteTutorial()
	{
		if (!TutorialStep.HasAllTutorialCompleted() && !isInGameScene)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep5_1", null);
		}
	}

	protected virtual void SetBaseFrame()
	{
		baseRoot = SetPrefab(UI.OBJ_BASE_ROOT, "QuestRequestCheckBase");
	}

	protected virtual void SetTargetFrame()
	{
		targetFrame = SetPrefab(UI.OBJ_NEED_ITEM_ROOT, "QuestRequestCheckItem");
	}

	protected virtual void SetSubmissionFrame()
	{
	}

	protected void UpdateSubMissionButton()
	{
		uint questId = info.needs[0].questId;
		if (questId == 0)
		{
			SetActive(UI.BTN_SUBMISSION, false);
		}
		else
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questId);
			SetActive(UI.BTN_SUBMISSION, questData.IsMissionExist());
		}
	}

	protected void UpdateSubMission()
	{
		UI[] array = new UI[3]
		{
			UI.OBJ_MISSION_INFO_1,
			UI.OBJ_MISSION_INFO_2,
			UI.OBJ_MISSION_INFO_3
		};
		UI[] array2 = new UI[3]
		{
			UI.OBJ_TOP_CROWN_1,
			UI.OBJ_TOP_CROWN_2,
			UI.OBJ_TOP_CROWN_3
		};
		UI[] array3 = new UI[3]
		{
			UI.LBL_MISSION_INFO_1,
			UI.LBL_MISSION_INFO_2,
			UI.LBL_MISSION_INFO_3
		};
		UI[] array4 = new UI[3]
		{
			UI.SPR_MISSION_INFO_CROWN_1,
			UI.SPR_MISSION_INFO_CROWN_2,
			UI.SPR_MISSION_INFO_CROWN_3
		};
		UI[] array5 = new UI[3]
		{
			UI.SPR_CROWN_1,
			UI.SPR_CROWN_2,
			UI.SPR_CROWN_3
		};
		if (info.needs.Length != 0)
		{
			uint questId = info.needs[0].questId;
			if (questId != 0)
			{
				QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questId);
				if (!questData.IsMissionExist())
				{
					SetActive(UI.OBJ_SUBMISSION_ROOT, false);
				}
				else
				{
					ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(questId);
					if (clearStatusQuestData == null)
					{
						SetActive(UI.OBJ_SUBMISSION_ROOT, true);
						int i = 0;
						for (int num = questData.missionID.Length; i < num; i++)
						{
							uint num2 = questData.missionID[i];
							SetActive(submissionFrame, array[i], num2 != 0);
							SetActive(submissionFrame, array2[i], num2 != 0);
							SetActive(submissionFrame, array3[i], num2 != 0);
							if (num2 != 0)
							{
								SetActive(submissionFrame, array4[i], false);
								SetActive(submissionFrame, array5[i], false);
								QuestTable.MissionTableData missionData = Singleton<QuestTable>.I.GetMissionData(num2);
								SetLabelText(submissionFrame, array3[i], missionData.missionText);
							}
						}
					}
					else
					{
						SetActive(UI.OBJ_SUBMISSION_ROOT, true);
						int j = 0;
						for (int count = clearStatusQuestData.missionStatus.Count; j < count; j++)
						{
							CLEAR_STATUS cLEAR_STATUS = (CLEAR_STATUS)clearStatusQuestData.missionStatus[j];
							SetActive(submissionFrame, array[j], questData.missionID[j] != 0);
							SetActive(submissionFrame, array2[j], questData.missionID[j] != 0);
							SetActive(submissionFrame, array4[j], cLEAR_STATUS >= CLEAR_STATUS.CLEAR);
							SetActive(submissionFrame, array5[j], cLEAR_STATUS >= CLEAR_STATUS.CLEAR);
							QuestTable.MissionTableData missionData2 = Singleton<QuestTable>.I.GetMissionData(questData.missionID[j]);
							SetLabelText(submissionFrame, array3[j], missionData2.missionText);
						}
					}
				}
			}
		}
	}

	protected void UpdateHappenTarget()
	{
		QuestTable.QuestTableData questData = info.GetQuestData();
		if (questData != null)
		{
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
			if (enemyData != null)
			{
				ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, null, FindCtrl(targetFrame, UI.OBJ_ENEMY), enemyData.element, null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
			}
		}
	}

	public override void UpdateUI()
	{
		OpenTutorial();
		UpdateTitle();
		SetSprite(baseRoot, UI.SPR_WINDOW, SPR_WINDOW_TYPE[info.DeliveryTypeIndex()]);
		SetSprite(baseRoot, UI.SPR_MESSAGE_BG, SPR_MESSAGE_BG_TYPE[info.DeliveryTypeIndex()]);
		bool flag = false;
		if ((bool)submissionFrame)
		{
			UpdateSubMissionButton();
			UpdateSubMission();
			flag = submissionFrame.gameObject.activeSelf;
			SetActive(UI.STR_BTN_SUBMISSION, !flag);
			SetActive(UI.STR_BTN_SUBMISSION_BACK, flag);
		}
		Transform root = targetFrame;
		MonoBehaviourSingleton<DeliveryManager>.I.GetTargetEnemyData(deliveryID, out targetQuestID, out targetMapID, out string map_name, out string enemy_name, out DIFFICULTY_TYPE? difficulty, out targetPortalID);
		SetLabelText(root, UI.LBL_PLACE_NAME, map_name);
		MonoBehaviourSingleton<DeliveryManager>.I.GetAllProgressDelivery(deliveryID, out int have, out int need);
		SetLabelText(root, UI.LBL_HAVE, (!isComplete) ? have.ToString() : need.ToString());
		SetColor(root, UI.LBL_HAVE, (!isComplete) ? Color.red : Color.white);
		SetLabelText(root, UI.LBL_NEED, need.ToString());
		SetLabelText(root, UI.LBL_NEED_ITEM_NAME, MonoBehaviourSingleton<DeliveryManager>.I.GetTargetItemName(deliveryID, 0u));
		if (info.IsDefeatCondition(0u))
		{
			if (targetQuestID != 0)
			{
				isQuestEnemy = true;
				Transform transform = FindCtrl(root, UI.OBJ_DIFFICULTY_ROOT);
				int value = (int)difficulty.Value;
				int j = 0;
				for (int childCount = transform.childCount; j < childCount; j++)
				{
					Transform child = transform.GetChild(j);
					SetActive(child, j <= value);
				}
				SetLabelText(root, UI.LBL_GET_PLACE, base.sectionData.GetText("GET_QUEST"));
			}
			else
			{
				isQuestEnemy = false;
				SetLabelText(root, UI.LBL_GET_PLACE, base.sectionData.GetText("GET_AREA"));
			}
			SetLabelText(root, UI.LBL_ENEMY_NAME, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 3u), enemy_name));
		}
		else
		{
			isQuestEnemy = false;
			SetLabelText(root, UI.LBL_GET_PLACE, StringTable.Get(STRING_CATEGORY.DELIVERY_CONDITION_PLACE, (uint)info.GetConditionType(0u)));
			SetLabelText(root, UI.LBL_ENEMY_NAME, enemy_name);
		}
		SetActive(root, UI.OBJ_DIFFICULTY_ROOT, isQuestEnemy);
		SetActive(root, UI.OBJ_ENEMY_NAME_ROOT, !isQuestEnemy);
		UpdateNPC(map_name, enemy_name);
		if ((isComplete || isNotice) && !isCompletedEventDelivery)
		{
			SetActive(UI.OBJ_BACK, false);
			SetActive(UI.BTN_CREATE, false);
			SetActive(UI.BTN_JOIN, false);
			SetActive(UI.BTN_MATCHING, false);
			if (isNotice)
			{
				UpdateUIJumpButton(JumpButtonType.Complete);
			}
		}
		else
		{
			SetActive(UI.OBJ_BACK, true);
			bool flag2 = true;
			bool flag3 = false;
			if (info == null || info.IsDefeatCondition(0u) || targetMapID != 0)
			{
				if (isQuestEnemy)
				{
					if (isInGameScene)
					{
						flag2 = false;
					}
				}
				else
				{
					bool flag4 = FieldManager.HasWorldMap(targetMapID);
					if (isInGameScene)
					{
						if (MonoBehaviourSingleton<FieldManager>.I.currentMapID == targetMapID)
						{
							if (flag4)
							{
								flag3 = true;
							}
							else
							{
								flag2 = false;
							}
						}
						else if (flag4)
						{
							if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(targetMapID) || WorldMapManager.IsValidPortalIDs(targetPortalID))
							{
								flag3 = true;
							}
						}
						else if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(targetMapID))
						{
							flag2 = false;
						}
					}
					else if (flag4)
					{
						if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(targetMapID) || WorldMapManager.IsValidPortalIDs(targetPortalID))
						{
							flag3 = true;
						}
					}
					else if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(targetMapID))
					{
						flag2 = false;
					}
				}
			}
			else
			{
				flag2 = (info.GetDeliveryJumpType() != DeliveryTable.DELIVERY_JUMPTYPE.UNDEFINED);
			}
			if (info != null && info.subType == DELIVERY_SUB_TYPE.READ_STORY)
			{
				flag2 = false;
			}
			JumpButtonType jumpButtonType = JumpButtonType.Invalid;
			if (flag2)
			{
				if (info != null && info.GetDeliveryJumpType() != 0)
				{
					jumpButtonType = ConvertDeliveryJumpType();
				}
				else
				{
					if (info != null)
					{
						QuestTable.QuestTableData questData = info.GetQuestData();
						if (questData != null)
						{
							jumpButtonType = GetJumpButtonTypeByQuestType(questData.questType);
						}
					}
					if (jumpButtonType != JumpButtonType.WaveRoom && jumpButtonType != JumpButtonType.seriesRoom)
					{
						jumpButtonType = ((!flag3) ? JumpButtonType.Quest : JumpButtonType.Map);
					}
				}
				UpdateUIJumpButton(jumpButtonType);
			}
			else
			{
				SetActive(baseRoot, UI.BTN_JUMP_QUEST, false);
				SetActive(baseRoot, UI.BTN_JUMP_MAP, false);
				SetActive(baseRoot, UI.BTN_JUMP_GACHATOP, false);
				SetActive(baseRoot, UI.BTN_JUMP_INVALID, false);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_NEW, false);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_PASS, false);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_AUTO, false);
				SetActive(baseRoot, UI.BTN_COMPLETE, false);
			}
			if (flag3 && MonoBehaviourSingleton<FieldManager>.I.currentMapID != targetMapID)
			{
				SetColor(baseRoot, UI.LBL_PLACE_NAME, Color.red);
			}
			else
			{
				SetColor(baseRoot, UI.LBL_PLACE_NAME, Color.white);
			}
		}
		int money = 0;
		int exp = 0;
		if (rewardData != null)
		{
			SetGrid(baseRoot, UI.GRD_REWARD, string.Empty, rewardData.Length, false, delegate(int i, Transform t, bool is_recycle)
			{
				DeliveryRewardTable.DeliveryRewardData.Reward reward = rewardData[i].reward;
				bool is_visible = false;
				if (reward.type == REWARD_TYPE.MONEY)
				{
					money += reward.num;
				}
				else if (reward.type == REWARD_TYPE.EXP)
				{
					exp += reward.num;
				}
				else
				{
					is_visible = true;
					ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(reward.type, reward.item_id, t, reward.num, string.Empty, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_DETAIL);
					SetMaterialInfo(itemIcon.transform, reward.type, reward.item_id, null);
					itemIcon.SetRewardBG(true);
				}
				SetActive(t, is_visible);
			});
		}
		SetLabelText(baseRoot, UI.LBL_MONEY, money.ToString());
		SetLabelText(baseRoot, UI.LBL_EXP, exp.ToString());
		SetActive(baseRoot, UI.OBJ_COMPLETE_ROOT, isComplete && !flag);
		SetActive(baseRoot, UI.OBJ_UNLOCK_PORTAL_ROOT, isComplete);
		if (isComplete)
		{
			string text = string.Empty;
			List<FieldMapTable.PortalTableData> deliveryRelationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData(info.id);
			switch (deliveryRelationPortalData.Count)
			{
			case 1:
			{
				FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(deliveryRelationPortalData[0].srcMapID);
				if (fieldMapData != null)
				{
					text = fieldMapData.mapName;
				}
				break;
			}
			default:
				text = base.sectionData.GetText("MULTI_UNLOCK");
				break;
			case 0:
				break;
			}
			bool flag5 = !string.IsNullOrEmpty(text);
			if (!TutorialStep.HasFirstDeliveryCompleted())
			{
				flag5 = false;
			}
			SetActive(baseRoot, UI.OBJ_UNLOCK_PORTAL_ROOT, flag5 && !isCompletedEventDelivery);
			SetLabelText(baseRoot, UI.LBL_UNLOCK_PORTAL, text);
			if (isCompletedEventDelivery)
			{
				SkipTween(baseRoot, UI.OBJ_COMPLETE_ROOT, true, 0);
			}
			else
			{
				StartCoroutine(StartTweenCoroutine(flag5));
			}
		}
		StartCoroutine(SetPointShopGetPointUI());
	}

	private IEnumerator StartTweenCoroutine(bool is_unlock_portal)
	{
		while (GameSceneManager.isAutoEventSkip)
		{
			yield return (object)null;
		}
		string effectName = "ef_ui_portal_unlock_01";
		if (is_unlock_portal)
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, effectName);
			yield return (object)load_queue.Wait();
			ResetTween(baseRoot, UI.OBJ_UNLOCK_PORTAL_ROOT, 0);
		}
		PlayCompleteTween(delegate
		{
			((_003CStartTweenCoroutine_003Ec__Iterator96)/*Error near IL_00c1: stateMachine*/)._003C_003Ef__this.OnEndCompletetween(((_003CStartTweenCoroutine_003Ec__Iterator96)/*Error near IL_00c1: stateMachine*/).is_unlock_portal, ((_003CStartTweenCoroutine_003Ec__Iterator96)/*Error near IL_00c1: stateMachine*/)._003CeffectName_003E__0);
		});
		CompleteTutorial();
	}

	private void PlayCompleteTween(EventDelegate.Callback callback)
	{
		ResetTween(baseRoot, UI.OBJ_COMPLETE_ROOT, 0);
		PlayTween(baseRoot, UI.OBJ_COMPLETE_ROOT, true, delegate
		{
			PlayAudio(AUDIO.REQUEST_COMPLETE, 1f, false);
			callback();
		}, false, 0);
	}

	protected virtual void OnEndCompletetween(bool is_unlock_portal, string effectName)
	{
		completeJumpButton = true;
		if (is_unlock_portal)
		{
			PlayUnlockPortalTween(effectName, delegate
			{
				UpdateUIJumpButton(JumpButtonType.Complete);
				DispMessageEventOpen();
			});
		}
		else
		{
			UpdateUIJumpButton(JumpButtonType.Complete);
			DispMessageEventOpen();
		}
	}

	protected void PlayUnlockPortalTween(string effectName, Action callback)
	{
		Transform ctrl = GetCtrl(UI.OBJ_UNLOCK_PORTAL_ROOT);
		if ((UnityEngine.Object)ctrl != (UnityEngine.Object)null)
		{
			Transform uIEffect = EffectManager.GetUIEffect(effectName, ctrl, -0.2f, 0, null);
			if ((UnityEngine.Object)uIEffect != (UnityEngine.Object)null)
			{
				rymFX component = uIEffect.GetComponent<rymFX>();
				if ((UnityEngine.Object)component != (UnityEngine.Object)null)
				{
					component.ChangeRenderQueue = 3999;
				}
			}
		}
		PlayTween(baseRoot, UI.OBJ_UNLOCK_PORTAL_ROOT, true, delegate
		{
			PlayAudio(AUDIO.UNLOCKE_PORTAL, 1f, true);
			callback();
		}, false, 0);
	}

	protected void DispMessageEventOpen()
	{
		if (MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds != null && MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds.Count != 0)
		{
			hasDispedMessage = true;
			int releasedEventId = MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds[0];
			MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds.RemoveAt(0);
			Network.EventData eventData = (from e in MonoBehaviourSingleton<QuestManager>.I.eventList
			where e.eventId == releasedEventId
			select e).First();
			if (eventData == null)
			{
				Debug.LogError("イベント開放に関して、指定されたIDのイベントが存在しません");
				DispMessageEventOpen();
			}
			else
			{
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 4u), eventData.name), null, null, null, null), delegate
				{
					DispMessageEventOpen();
				}, false, 0);
			}
		}
	}

	protected virtual void UpdateTitle()
	{
		SetLabelText(baseRoot, UI.LBL_QUEST_TITLE, info.name);
	}

	protected virtual void UpdateNPC(string map_name, string enemy_name)
	{
		NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData((int)info.npcID);
		SetNPCIcon(baseRoot, UI.TEX_NPC, nPCData.npcModelID, isComplete);
		SetLabelText(baseRoot, UI.LBL_PERSON_NAME, nPCData.displayName);
		string text = (!isComplete) ? info.npcComment : info.npcClearComment;
		text = text.Replace("{MAP_NAME}", map_name);
		text = text.Replace("{USER_NAME}", MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
		text = text.Replace("{ENEMY_NAME}", enemy_name);
		SetLabelText(baseRoot, UI.LBL_CHARA_MESSAGE, text);
	}

	protected void JumpQuest()
	{
		if (!TutorialStep.HasFirstDeliveryCompleted())
		{
			GameSection.StopEvent();
			DispatchEvent("TUTORIAL_TO_FIELD", null);
		}
		else
		{
			PlayAudio(AUDIO.GO_TO_FIELD, 1f, false);
			if (isQuestEnemy)
			{
				if (!isInGameScene)
				{
					EventData[] autoEvents = new EventData[3]
					{
						new EventData("[BACK]", null),
						new EventData("TAB_QUEST", (uint)deliveryID),
						new EventData("SELECT_QUEST", targetQuestID)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				}
			}
			else
			{
				FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(targetMapID);
				if (fieldMapData == null || fieldMapData.jumpPortalID == 0)
				{
					Log.Error("QuestDeliveryDetail.JumpQuest() jumpPortalID is not found.");
				}
				else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
				{
					MonoBehaviourSingleton<WorldMapManager>.I.SetJumpPortalID(fieldMapData.jumpPortalID);
					GameSection.StopEvent();
					DispatchEvent("QUEST_TO_FIELD", null);
				}
				else if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.currentMapID != targetMapID)
				{
					MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
				}
			}
		}
	}

	protected void OnQuery_TO_SMITH()
	{
		ToSmith();
	}

	protected void OnQuery_TO_STATUS()
	{
		OnQuery_MAIN_MENU_STATUS();
	}

	protected void OnQuery_TO_STORAGE()
	{
		OpenStorage();
	}

	protected void OnQuery_TO_POINT_SHOP()
	{
		ToPointShop();
	}

	protected void OnQuery_TO_WORLDMAP()
	{
		OnQuery_MAIN_MENU_QUEST();
	}

	protected virtual void UpdateUIJumpButton(JumpButtonType type)
	{
		SetActive(baseRoot, UI.BTN_JUMP_QUEST, false);
		SetActive(baseRoot, UI.BTN_JUMP_MAP, false);
		SetActive(baseRoot, UI.BTN_JUMP_GACHATOP, false);
		SetActive(baseRoot, UI.BTN_JUMP_INVALID, false);
		SetActive(baseRoot, UI.BTN_COMPLETE, false);
		SetActive(baseRoot, UI.BTN_JUMP_POINT_SHOP, false);
		SetActive(baseRoot, UI.BTN_JUMP_WORLDMAP, false);
		SetActive(baseRoot, UI.BTN_JUMP_SMITH, false);
		SetActive(baseRoot, UI.BTN_JUMP_STATUS, false);
		SetActive(baseRoot, UI.BTN_JUMP_STORAGE, false);
		SetActive(baseRoot, UI.BTN_WAVEMATCH_NEW, false);
		SetActive(baseRoot, UI.BTN_WAVEMATCH_PASS, false);
		SetActive(baseRoot, UI.BTN_WAVEMATCH_AUTO, false);
		switch (type)
		{
		default:
			SetActive(baseRoot, UI.BTN_JUMP_INVALID, true);
			break;
		case JumpButtonType.Complete:
			SetActive(baseRoot, UI.BTN_COMPLETE, true);
			break;
		case JumpButtonType.Map:
			SetActive(baseRoot, UI.BTN_JUMP_MAP, true);
			break;
		case JumpButtonType.Quest:
			SetActive(baseRoot, UI.BTN_JUMP_QUEST, true);
			break;
		case JumpButtonType.Gacha:
			SetActive(baseRoot, UI.BTN_JUMP_GACHATOP, true);
			break;
		case JumpButtonType.Smith:
			SetActive(baseRoot, UI.BTN_JUMP_SMITH, true);
			break;
		case JumpButtonType.Status:
			SetActive(baseRoot, UI.BTN_JUMP_STATUS, true);
			break;
		case JumpButtonType.Storage:
			SetActive(baseRoot, UI.BTN_JUMP_STORAGE, true);
			break;
		case JumpButtonType.PointShop:
			SetActive(baseRoot, UI.BTN_JUMP_POINT_SHOP, true);
			break;
		case JumpButtonType.WorldMap:
			SetActive(baseRoot, UI.BTN_JUMP_WORLDMAP, true);
			break;
		case JumpButtonType.WaveRoom:
		case JumpButtonType.seriesRoom:
			if (!isInGameScene)
			{
				SetActive(baseRoot, UI.BTN_WAVEMATCH_NEW, true);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_PASS, true);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_AUTO, true);
			}
			break;
		}
	}

	protected void JumpMap()
	{
		if (FieldManager.HasWorldMap(targetMapID))
		{
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(targetMapID);
			if (Array.IndexOf(MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdList(), fieldMapData.regionId) < 0)
			{
				RegionTable.Data data = Singleton<RegionTable>.I.GetData(fieldMapData.regionId);
				GameSection.ChangeEvent("NOT_OPEN", new object[1]
				{
					data.regionName
				});
				return;
			}
		}
		MonoBehaviourSingleton<WorldMapManager>.I.PushDisplayQuestTarget((int)targetMapID, targetPortalID);
		MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = true;
		bool flag = true;
		if (Singleton<TutorialMessageTable>.IsValid())
		{
			TutorialReadData readData = Singleton<TutorialMessageTable>.I.ReadData;
			flag = readData.HasRead(10003);
		}
		bool flag2 = false;
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		if (deliveryTableData != null && deliveryTableData.type == DELIVERY_TYPE.STORY && 10100011 >= deliveryID && !flag)
		{
			flag2 = true;
			if (Singleton<TutorialMessageTable>.IsValid())
			{
				TutorialReadData readData2 = Singleton<TutorialMessageTable>.I.ReadData;
				readData2.SetReadId(10003, true);
				readData2.Save();
			}
		}
		if (flag2)
		{
			RequestEvent("DIRECT_REGION_TUTORIAL", null);
		}
		else
		{
			RequestEvent("DIRECT_REGION_QUEST", null);
		}
	}

	private IEnumerator SetPointShopGetPointUI()
	{
		if (pointShopGetPointData != null && pointShopGetPointData.Count > 0)
		{
			LoadingQueue queue = new LoadingQueue(this);
			queue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName((int)pointShopGetPointData[0].pointShopId), false);
			if (queue.IsLoading())
			{
				yield return (object)queue.Wait();
			}
			SetActive(UI.OBJ_NORMAL_ROOT, true);
			SetLabelText(UI.LBL_POINT_NORMAL, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopGetPointData[0].basePoint));
			UITexture normalTex = GetCtrl(UI.TEX_NORMAL_ICON).GetComponent<UITexture>();
			ResourceLoad.LoadPointIconImageTexture(normalTex, pointShopGetPointData[0].pointShopId);
			if (pointShopGetPointData.Count >= 2)
			{
				queue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName((int)pointShopGetPointData[1].pointShopId), false);
				if (queue.IsLoading())
				{
					yield return (object)queue.Wait();
				}
				SetActive(UI.OBJ_EVENT_ROOT, true);
				SetLabelText(UI.LBL_POINT_EVENT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopGetPointData[1].basePoint));
				UITexture eventTex = GetCtrl(UI.TEX_EVENT_ICON).GetComponent<UITexture>();
				ResourceLoad.LoadPointIconImageTexture(eventTex, pointShopGetPointData[1].pointShopId);
			}
		}
	}

	private JumpButtonType ConvertDeliveryJumpType()
	{
		switch (info.GetDeliveryJumpType())
		{
		case DeliveryTable.DELIVERY_JUMPTYPE.TO_GACHA:
			return JumpButtonType.Gacha;
		case DeliveryTable.DELIVERY_JUMPTYPE.TO_SMITH:
			return JumpButtonType.Smith;
		case DeliveryTable.DELIVERY_JUMPTYPE.TO_STATUS:
			return JumpButtonType.Status;
		case DeliveryTable.DELIVERY_JUMPTYPE.TO_STORAGE:
			return JumpButtonType.Storage;
		case DeliveryTable.DELIVERY_JUMPTYPE.TO_POINT_SHOP:
			return JumpButtonType.PointShop;
		case DeliveryTable.DELIVERY_JUMPTYPE.TO_WORLD_MAP:
			return JumpButtonType.WorldMap;
		default:
			return JumpButtonType.Invalid;
		}
	}

	protected void WaveMatchNew()
	{
		QuestTable.QuestTableData questData = info.GetQuestData();
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID, true);
		GameSection.SetEventData(new object[1]
		{
			questData.questType
		});
	}

	protected void WaveMatchPass()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(info.needs[0].questId, true);
	}

	protected void WaveMatchAuto()
	{
		GameSection.SetEventData(new object[1]
		{
			false
		});
		GameSection.StayEvent();
		int retryCount = 0;
		PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0, 0, 0);
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int)info.GetQuestData().questID, retryCount, false, delegate(bool is_success, int maxRetryCount, bool isJoined, float waitTime)
		{
			if (!is_success)
			{
				GameSection.ResumeEvent(false, null);
			}
			else if (maxRetryCount > 0)
			{
				retryCount++;
				StartCoroutine(MatchAtRandom(setting, retryCount, waitTime));
			}
			else if (!isJoined)
			{
				WaveMatchCreate();
			}
			else
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
				GameSection.ResumeEvent(true, null);
			}
		});
	}

	private IEnumerator MatchAtRandom(PartyManager.PartySetting setting, int retryCount, float time)
	{
		yield return (object)new WaitForSeconds(time);
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int)info.needs[0].questId, retryCount, false, delegate(bool is_success, int maxRetryCount, bool isJoined, float waitTime)
		{
			if (!is_success)
			{
				GameSection.ResumeEvent(false, null);
			}
			else if (maxRetryCount > 0)
			{
				if (((_003CMatchAtRandom_003Ec__Iterator98)/*Error near IL_0061: stateMachine*/).retryCount >= maxRetryCount)
				{
					((_003CMatchAtRandom_003Ec__Iterator98)/*Error near IL_0061: stateMachine*/)._003C_003Ef__this.WaveMatchCreate();
				}
				else
				{
					((_003CMatchAtRandom_003Ec__Iterator98)/*Error near IL_0061: stateMachine*/).retryCount++;
					((_003CMatchAtRandom_003Ec__Iterator98)/*Error near IL_0061: stateMachine*/)._003C_003Ef__this.StartCoroutine(((_003CMatchAtRandom_003Ec__Iterator98)/*Error near IL_0061: stateMachine*/)._003C_003Ef__this.MatchAtRandom(((_003CMatchAtRandom_003Ec__Iterator98)/*Error near IL_0061: stateMachine*/).setting, ((_003CMatchAtRandom_003Ec__Iterator98)/*Error near IL_0061: stateMachine*/).retryCount, waitTime));
				}
			}
			else if (!isJoined)
			{
				((_003CMatchAtRandom_003Ec__Iterator98)/*Error near IL_0061: stateMachine*/)._003C_003Ef__this.WaveMatchCreate();
			}
			else
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(((_003CMatchAtRandom_003Ec__Iterator98)/*Error near IL_0061: stateMachine*/).setting);
				GameSection.ResumeEvent(true, null);
			}
		});
	}

	protected void WaveMatchCreate()
	{
		QuestTable.QuestTableData questData = info.GetQuestData();
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID, true);
		GameSection.SetEventData(new object[1]
		{
			questData.questType
		});
		PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0, 0, 0);
		MonoBehaviourSingleton<PartyManager>.I.SendCreate((int)questData.questID, setting, delegate(bool is_success)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
			}
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void OnQuery_SECTION_BACK()
	{
		if (info != null && completeJumpButton)
		{
			if (info.GetUIType() == DeliveryTable.UIType.EVENT || info.GetUIType() == DeliveryTable.UIType.SUB_EVENT)
			{
				GameSection.StayEvent();
				MonoBehaviourSingleton<DeliveryManager>.I.SendEventList(delegate
				{
					GameSection.ResumeEvent(true, null);
				});
			}
			else if (info.DeliveryTypeIndex() != 1)
			{
				GameSection.StayEvent();
				MonoBehaviourSingleton<DeliveryManager>.I.SendEventNormalList(delegate
				{
					GameSection.ResumeEvent(true, null);
				});
			}
		}
	}
}
