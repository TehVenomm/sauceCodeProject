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
		BTN_WAVEMATCH_AUTO,
		OBJ_CARNIVAL_ROOT,
		LBL_POINT_CARNIVAL,
		LBL_LIMIT_TIME,
		OBJ_CLEAR_ICON_ROOT,
		OBJ_CLEAR_REWARD,
		BTN_CREATE_OFF,
		SPR_TYPE_DIFFICULTY,
		BTN_CHANGE_EQUIP
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
		seriesRoom,
		orderRoom,
		eventRoom
	}

	private enum SMITH_SECTION
	{
		INVALID,
		EQUIP_GROW,
		EQUIP_EXCEED,
		EQUIP_EVOLVE,
		EQUIP_CREATE,
		MAGI_GROW,
		CHANGE_ABILITY
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
		case QUEST_TYPE.WAVE_STRATEGY:
			return JumpButtonType.WaveRoom;
		case QUEST_TYPE.SERIES:
			return JumpButtonType.seriesRoom;
		case QUEST_TYPE.ORDER:
			return JumpButtonType.orderRoom;
		case QUEST_TYPE.EVENT:
			return JumpButtonType.eventRoom;
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
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep2_2");
		}
	}

	private void CompleteTutorial()
	{
		if (!TutorialStep.HasAllTutorialCompleted() && !isInGameScene)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep5_1");
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

	protected virtual Enum GetBtnChangeEquipValue()
	{
		return UI.BTN_CHANGE_EQUIP;
	}

	protected virtual bool IsChangableEquip()
	{
		return false;
	}

	protected void AdjustBtnPosToChangableEquipUI1Btn()
	{
		Transform transform = FindCtrl(baseRoot, GetBtnChangeEquipValue());
		if (!(transform == null))
		{
			transform.localPosition = GetEquipBtnPos();
			SetActive(baseRoot, GetBtnChangeEquipValue(), is_visible: true);
		}
	}

	protected virtual Vector3 GetEquipBtnPos()
	{
		return new Vector3(170f, -340f, 0f);
	}

	private void AdjustBtnPositionToChangableEquipUI(JumpButtonType type)
	{
		if (isInGameScene || !IsChangableEquip())
		{
			SetActive(baseRoot, GetBtnChangeEquipValue(), is_visible: false);
			return;
		}
		switch (type)
		{
		case JumpButtonType.Invalid:
		case JumpButtonType.Complete:
		case JumpButtonType.Gacha:
		case JumpButtonType.Smith:
		case JumpButtonType.Status:
		case JumpButtonType.Storage:
		case JumpButtonType.PointShop:
		case JumpButtonType.WaveRoom:
		case JumpButtonType.seriesRoom:
		case JumpButtonType.orderRoom:
		case JumpButtonType.eventRoom:
			break;
		case JumpButtonType.Map:
		case JumpButtonType.Quest:
		case JumpButtonType.WorldMap:
			AdjustBtnPosToChangableEquipUI1Btn();
			break;
		}
	}

	protected void UpdateSubMissionButton()
	{
		uint questId = info.needs[0].questId;
		if (questId == 0)
		{
			SetActive(UI.BTN_SUBMISSION, is_visible: false);
			return;
		}
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questId);
		SetActive(UI.BTN_SUBMISSION, questData.IsMissionExist());
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
		if (info.needs.Length == 0)
		{
			return;
		}
		uint questId = info.needs[0].questId;
		if (questId == 0)
		{
			return;
		}
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questId);
		if (!questData.IsMissionExist())
		{
			SetActive(UI.OBJ_SUBMISSION_ROOT, is_visible: false);
			return;
		}
		ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(questId);
		if (clearStatusQuestData == null)
		{
			SetActive(UI.OBJ_SUBMISSION_ROOT, is_visible: true);
			int i = 0;
			for (int num = questData.missionID.Length; i < num; i++)
			{
				uint num2 = questData.missionID[i];
				SetActive(submissionFrame, array[i], num2 != 0);
				SetActive(submissionFrame, array2[i], num2 != 0);
				SetActive(submissionFrame, array3[i], num2 != 0);
				if (num2 != 0)
				{
					SetActive(submissionFrame, array4[i], is_visible: false);
					SetActive(submissionFrame, array5[i], is_visible: false);
					QuestTable.MissionTableData missionData = Singleton<QuestTable>.I.GetMissionData(num2);
					SetLabelText(submissionFrame, array3[i], missionData.missionText);
				}
			}
		}
		else
		{
			SetActive(UI.OBJ_SUBMISSION_ROOT, is_visible: true);
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

	protected void UpdateHappenTarget()
	{
		QuestTable.QuestTableData questData = info.GetQuestData();
		if (questData != null)
		{
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
			if (enemyData != null)
			{
				ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, null, FindCtrl(targetFrame, UI.OBJ_ENEMY), enemyData.element);
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
		SetLabelText(root, UI.LBL_HAVE, isComplete ? need.ToString() : have.ToString());
		SetColor(root, UI.LBL_HAVE, isComplete ? Color.white : Color.red);
		SetLabelText(root, UI.LBL_NEED, need.ToString());
		SetLabelText(root, UI.LBL_NEED_ITEM_NAME, MonoBehaviourSingleton<DeliveryManager>.I.GetTargetItemName(deliveryID));
		if (info.IsDefeatCondition())
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
			SetLabelText(root, UI.LBL_GET_PLACE, StringTable.Get(STRING_CATEGORY.DELIVERY_CONDITION_PLACE, (uint)info.GetConditionType()));
			SetLabelText(root, UI.LBL_ENEMY_NAME, enemy_name);
		}
		SetActive(root, UI.OBJ_DIFFICULTY_ROOT, isQuestEnemy);
		SetActive(root, UI.OBJ_ENEMY_NAME_ROOT, !isQuestEnemy);
		UpdateNPC(map_name, enemy_name);
		if ((isComplete || isNotice) && !isCompletedEventDelivery)
		{
			SetActive(UI.OBJ_BACK, is_visible: false);
			SetActive(UI.BTN_CREATE, is_visible: false);
			SetActive(UI.BTN_JOIN, is_visible: false);
			SetActive(UI.BTN_MATCHING, is_visible: false);
			SetActive(GetBtnChangeEquipValue(), is_visible: false);
			if (isNotice)
			{
				UpdateUIJumpButton(JumpButtonType.Complete);
			}
		}
		else
		{
			SetActive(UI.OBJ_BACK, is_visible: true);
			bool flag2 = true;
			bool flag3 = false;
			if (info == null || info.IsDefeatCondition() || targetMapID != 0)
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
					if (jumpButtonType != JumpButtonType.WaveRoom && jumpButtonType != JumpButtonType.seriesRoom && jumpButtonType != JumpButtonType.orderRoom && jumpButtonType != JumpButtonType.eventRoom)
					{
						jumpButtonType = (flag3 ? JumpButtonType.Map : JumpButtonType.Quest);
					}
				}
				UpdateUIJumpButton(jumpButtonType);
			}
			else
			{
				SetActive(baseRoot, UI.BTN_JUMP_QUEST, is_visible: false);
				SetActive(baseRoot, UI.BTN_JUMP_MAP, is_visible: false);
				SetActive(baseRoot, UI.BTN_JUMP_GACHATOP, is_visible: false);
				SetActive(baseRoot, UI.BTN_JUMP_INVALID, is_visible: false);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_NEW, is_visible: false);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_PASS, is_visible: false);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_AUTO, is_visible: false);
				SetActive(baseRoot, UI.BTN_COMPLETE, is_visible: false);
				SetActive(GetBtnChangeEquipValue(), is_visible: false);
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
		int carnivalPoint = 0;
		if (rewardData != null)
		{
			SetGrid(baseRoot, UI.GRD_REWARD, "", rewardData.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
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
				else if (reward.type == REWARD_TYPE.RANKING_POINT)
				{
					carnivalPoint += reward.num;
				}
				else
				{
					is_visible = true;
					ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(reward.type, reward.item_id, t, reward.num, string.Empty, 0, is_new: false, -1, is_select: false, null, is_equipping: false, disable_rarity_text: false, ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_DETAIL);
					SetMaterialInfo(itemIcon.transform, reward.type, reward.item_id);
					itemIcon.SetRewardBG(is_visible: true);
				}
				SetActive(t, is_visible);
			});
		}
		SetLabelText(baseRoot, UI.LBL_MONEY, money.ToString());
		SetLabelText(baseRoot, UI.LBL_EXP, exp.ToString());
		SetActive(UI.OBJ_CARNIVAL_ROOT, carnivalPoint > 0);
		if (carnivalPoint > 0)
		{
			SetLabelText(baseRoot, UI.LBL_POINT_CARNIVAL, carnivalPoint.ToString());
		}
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
				SkipTween(baseRoot, UI.OBJ_COMPLETE_ROOT);
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
			yield return null;
		}
		string effectName = "ef_ui_portal_unlock_01";
		if (is_unlock_portal)
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, effectName);
			yield return loadingQueue.Wait();
			ResetTween(baseRoot, UI.OBJ_UNLOCK_PORTAL_ROOT);
		}
		PlayCompleteTween(delegate
		{
			OnEndCompletetween(is_unlock_portal, effectName);
		});
		CompleteTutorial();
	}

	private void PlayCompleteTween(EventDelegate.Callback callback)
	{
		ResetTween(baseRoot, UI.OBJ_COMPLETE_ROOT);
		PlayTween(baseRoot, UI.OBJ_COMPLETE_ROOT, forward: true, delegate
		{
			PlayAudio(AUDIO.REQUEST_COMPLETE);
			callback();
		}, is_input_block: false);
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
			return;
		}
		UpdateUIJumpButton(JumpButtonType.Complete);
		DispMessageEventOpen();
	}

	protected void PlayUnlockPortalTween(string effectName, Action callback)
	{
		Transform ctrl = GetCtrl(UI.OBJ_UNLOCK_PORTAL_ROOT);
		if (ctrl != null)
		{
			Transform uIEffect = EffectManager.GetUIEffect(effectName, ctrl, -0.2f);
			if (uIEffect != null)
			{
				rymFX component = uIEffect.GetComponent<rymFX>();
				if (component != null)
				{
					component.ChangeRenderQueue = 3999;
				}
			}
		}
		PlayTween(baseRoot, UI.OBJ_UNLOCK_PORTAL_ROOT, forward: true, delegate
		{
			PlayAudio(AUDIO.UNLOCKE_PORTAL, 1f, as_jingle: true);
			callback();
		}, is_input_block: false);
	}

	protected void DispMessageEventOpen()
	{
		if (MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds != null && MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds.Count != 0)
		{
			hasDispedMessage = true;
			int releasedEventId = MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds[0];
			MonoBehaviourSingleton<DeliveryManager>.I.releasedEventIds.RemoveAt(0);
			Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I.eventList.Where((Network.EventData e) => e.eventId == releasedEventId).First();
			if (eventData == null)
			{
				Debug.LogError("イベント開放に関して、指定されたIDのイベントが存在しません");
				DispMessageEventOpen();
			}
			else
			{
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 4u), eventData.name)), delegate
				{
					DispMessageEventOpen();
				});
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
		string text = isComplete ? info.npcClearComment : info.npcComment;
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
			DispatchEvent("TUTORIAL_TO_FIELD");
			return;
		}
		PlayAudio(AUDIO.GO_TO_FIELD);
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
			return;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(targetMapID);
		if (fieldMapData == null || fieldMapData.jumpPortalID == 0)
		{
			Log.Error("QuestDeliveryDetail.JumpQuest() jumpPortalID is not found.");
		}
		else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
		{
			MonoBehaviourSingleton<WorldMapManager>.I.SetJumpPortalID(fieldMapData.jumpPortalID);
			GameSection.StopEvent();
			DispatchEvent("QUEST_TO_FIELD");
		}
		else if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.currentMapID != targetMapID)
		{
			MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
		}
	}

	protected void OnQuery_TO_SMITH()
	{
		XorUInt targetId;
		SMITH_SECTION smithSection = GetSmithSection(info, out targetId);
		MonoBehaviourSingleton<StatusManager>.I.InitUniqueEquip();
		if (smithSection == SMITH_SECTION.INVALID)
		{
			ToSmith();
		}
		else
		{
			ToSmith(smithSection, targetId);
		}
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

	public void OnQuery_PORTAL_RELEASE()
	{
		object eventData = GameSection.GetEventData();
		if (eventData is List<uint>)
		{
			GameSaveData.instance.newReleasePortals = (eventData as List<uint>);
		}
		if (MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
		{
			MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame = new List<int>(MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene);
			MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Clear();
			MonoBehaviourSingleton<InGameProgress>.I.DeliveryAddCheck();
		}
		else
		{
			MonoBehaviourSingleton<DeliveryManager>.I.CheckAnnouncePortalOpen();
		}
	}

	protected virtual void UpdateUIJumpButton(JumpButtonType type)
	{
		SetActive(baseRoot, UI.BTN_JUMP_QUEST, is_visible: false);
		SetActive(baseRoot, UI.BTN_JUMP_MAP, is_visible: false);
		SetActive(baseRoot, UI.BTN_JUMP_GACHATOP, is_visible: false);
		SetActive(baseRoot, UI.BTN_JUMP_INVALID, is_visible: false);
		SetActive(baseRoot, UI.BTN_COMPLETE, is_visible: false);
		SetActive(baseRoot, UI.BTN_JUMP_POINT_SHOP, is_visible: false);
		SetActive(baseRoot, UI.BTN_JUMP_WORLDMAP, is_visible: false);
		SetActive(baseRoot, UI.BTN_JUMP_SMITH, is_visible: false);
		SetActive(baseRoot, UI.BTN_JUMP_STATUS, is_visible: false);
		SetActive(baseRoot, UI.BTN_JUMP_STORAGE, is_visible: false);
		SetActive(baseRoot, UI.BTN_WAVEMATCH_NEW, is_visible: false);
		SetActive(baseRoot, UI.BTN_WAVEMATCH_PASS, is_visible: false);
		SetActive(baseRoot, UI.BTN_WAVEMATCH_AUTO, is_visible: false);
		SetActive(baseRoot, GetBtnChangeEquipValue(), is_visible: false);
		AdjustBtnPositionToChangableEquipUI(type);
		switch (type)
		{
		default:
			SetActive(baseRoot, UI.BTN_JUMP_INVALID, is_visible: true);
			break;
		case JumpButtonType.Complete:
			SetActive(baseRoot, UI.BTN_COMPLETE, is_visible: true);
			break;
		case JumpButtonType.Map:
			SetActive(baseRoot, UI.BTN_JUMP_MAP, is_visible: true);
			break;
		case JumpButtonType.Quest:
			SetActive(baseRoot, UI.BTN_JUMP_QUEST, is_visible: true);
			break;
		case JumpButtonType.Gacha:
			SetActive(baseRoot, UI.BTN_JUMP_GACHATOP, is_visible: true);
			break;
		case JumpButtonType.Smith:
			SetActive(baseRoot, UI.BTN_JUMP_SMITH, is_visible: true);
			break;
		case JumpButtonType.Status:
			SetActive(baseRoot, UI.BTN_JUMP_STATUS, is_visible: true);
			break;
		case JumpButtonType.Storage:
			SetActive(baseRoot, UI.BTN_JUMP_STORAGE, is_visible: true);
			break;
		case JumpButtonType.PointShop:
			SetActive(baseRoot, UI.BTN_JUMP_POINT_SHOP, is_visible: true);
			break;
		case JumpButtonType.WorldMap:
			SetActive(baseRoot, UI.BTN_JUMP_WORLDMAP, is_visible: true);
			break;
		case JumpButtonType.WaveRoom:
		case JumpButtonType.seriesRoom:
		case JumpButtonType.orderRoom:
		case JumpButtonType.eventRoom:
			if (!isInGameScene)
			{
				SetActive(baseRoot, UI.BTN_WAVEMATCH_NEW, is_visible: true);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_PASS, is_visible: true);
				SetActive(baseRoot, UI.BTN_WAVEMATCH_AUTO, is_visible: true);
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
			flag = Singleton<TutorialMessageTable>.I.ReadData.HasRead(10003);
		}
		bool flag2 = false;
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		if (deliveryTableData != null && deliveryTableData.type == DELIVERY_TYPE.STORY && 10100011 >= deliveryID && !flag)
		{
			flag2 = true;
			if (Singleton<TutorialMessageTable>.IsValid())
			{
				TutorialReadData readData = Singleton<TutorialMessageTable>.I.ReadData;
				readData.SetReadId(10003, hasRead: true);
				readData.Save();
			}
		}
		if (flag2)
		{
			RequestEvent("DIRECT_REGION_TUTORIAL");
		}
		else
		{
			RequestEvent("DIRECT_REGION_QUEST");
		}
	}

	private IEnumerator SetPointShopGetPointUI()
	{
		if (pointShopGetPointData == null || pointShopGetPointData.Count <= 0)
		{
			yield break;
		}
		LoadingQueue queue = new LoadingQueue(this);
		queue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName((int)pointShopGetPointData[0].pointShopId));
		if (queue.IsLoading())
		{
			yield return queue.Wait();
		}
		SetActive(UI.OBJ_NORMAL_ROOT, is_visible: true);
		SetLabelText(UI.LBL_POINT_NORMAL, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopGetPointData[0].basePoint));
		ResourceLoad.LoadPointIconImageTexture(GetCtrl(UI.TEX_NORMAL_ICON).GetComponent<UITexture>(), pointShopGetPointData[0].pointShopId);
		if (pointShopGetPointData.Count >= 2)
		{
			queue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName((int)pointShopGetPointData[1].pointShopId));
			if (queue.IsLoading())
			{
				yield return queue.Wait();
			}
			SetActive(UI.OBJ_EVENT_ROOT, is_visible: true);
			SetLabelText(UI.LBL_POINT_EVENT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopGetPointData[1].basePoint));
			ResourceLoad.LoadPointIconImageTexture(GetCtrl(UI.TEX_EVENT_ICON).GetComponent<UITexture>(), pointShopGetPointData[1].pointShopId);
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
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID);
		GameSection.SetEventData(new object[1]
		{
			questData.questType
		});
	}

	protected void WaveMatchPass()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(info.needs[0].questId);
	}

	protected void WaveMatchAuto()
	{
		GameSection.SetEventData(new object[1]
		{
			false
		});
		GameSection.StayEvent();
		int retryCount = 0;
		PartyManager.PartySetting setting = new PartyManager.PartySetting(is_lock: false, 0, 0);
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int)info.GetQuestData().questID, retryCount, isExplore: false, delegate(bool is_success, int maxRetryCount, bool isJoined, float waitTime)
		{
			if (!is_success)
			{
				GameSection.ResumeEvent(is_resume: false);
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
				GameSection.ResumeEvent(is_resume: true);
			}
		});
	}

	private IEnumerator MatchAtRandom(PartyManager.PartySetting setting, int retryCount, float time)
	{
		yield return new WaitForSeconds(time);
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
					WaveMatchCreate();
				}
				else
				{
					retryCount++;
					StartCoroutine(MatchAtRandom(setting, retryCount, waitTime));
				}
			}
			else if (!isJoined)
			{
				WaveMatchCreate();
			}
			else
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
				GameSection.ResumeEvent(is_resume: true);
			}
		});
	}

	protected void WaveMatchCreate()
	{
		QuestTable.QuestTableData questData = info.GetQuestData();
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID);
		GameSection.SetEventData(new object[1]
		{
			questData.questType
		});
		PartyManager.PartySetting setting = new PartyManager.PartySetting(is_lock: false, 0, 0);
		MonoBehaviourSingleton<PartyManager>.I.SendCreate((int)questData.questID, setting, delegate(bool is_success)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
			}
			GameSection.ResumeEvent(is_success);
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
					GameSection.ResumeEvent(is_resume: true);
				});
			}
			else if (info.DeliveryTypeIndex() != 1)
			{
				GameSection.StayEvent();
				MonoBehaviourSingleton<DeliveryManager>.I.SendEventNormalList(delegate
				{
					GameSection.ResumeEvent(is_resume: true);
				});
			}
		}
	}

	private SMITH_SECTION GetSmithSection(DeliveryTable.DeliveryData _info, out XorUInt targetId)
	{
		targetId = 0u;
		if (_info == null)
		{
			return SMITH_SECTION.INVALID;
		}
		SMITH_SECTION sMITH_SECTION = SMITH_SECTION.INVALID;
		DeliveryTable.DeliveryData.NeedData[] needs = _info.needs;
		int i = 0;
		for (int num = needs.Length; i < num; i++)
		{
			DeliveryTable.DeliveryData.NeedData needData = needs[i];
			switch (needData.conditionType)
			{
			case DELIVERY_CONDITION_TYPE.CHANGE_ABILITY:
				targetId = needData.needId;
				if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)_info.id))
				{
					return SMITH_SECTION.CHANGE_ABILITY;
				}
				break;
			case DELIVERY_CONDITION_TYPE.WEAPON_PROTECTOR_GROW:
			case DELIVERY_CONDITION_TYPE.WEAPON_GROW:
			case DELIVERY_CONDITION_TYPE.PROTECTOR_GROW:
			case DELIVERY_CONDITION_TYPE.EQUIP_GROW_MAX_EQUIP_ID_OR:
				targetId = needData.needId;
				if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)_info.id) && MonoBehaviourSingleton<InventoryManager>.I.GetEquipItemNumWithShadow(targetId) > 0)
				{
					return SMITH_SECTION.EQUIP_GROW;
				}
				break;
			case DELIVERY_CONDITION_TYPE.EQUIP_EXCEED:
				targetId = needData.needId;
				if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)_info.id) && MonoBehaviourSingleton<InventoryManager>.I.GetEquipItemNumWithShadow(targetId) > 0)
				{
					return SMITH_SECTION.EQUIP_EXCEED;
				}
				break;
			case DELIVERY_CONDITION_TYPE.WEAPON_PROTECTOR_CREATE:
			case DELIVERY_CONDITION_TYPE.WEAPON_CREATE:
			case DELIVERY_CONDITION_TYPE.PROTECTOR_CREATE:
				targetId = needData.needId;
				if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)_info.id))
				{
					return SMITH_SECTION.EQUIP_CREATE;
				}
				break;
			case DELIVERY_CONDITION_TYPE.WEAPON_PROTECTOR_EVOLVE:
			case DELIVERY_CONDITION_TYPE.WEAPON_EVOLVE:
			case DELIVERY_CONDITION_TYPE.PROTECTOR_EVOLVE:
				targetId = needData.needId;
				if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)_info.id) && MonoBehaviourSingleton<InventoryManager>.I.GetEquipItemNumWithShadow(targetId) > 0)
				{
					return SMITH_SECTION.EQUIP_EVOLVE;
				}
				break;
			case DELIVERY_CONDITION_TYPE.MAGI_GROW:
				targetId = needData.needId;
				if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)_info.id))
				{
					return SMITH_SECTION.MAGI_GROW;
				}
				break;
			case DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID:
				if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)needData.needId.value))
				{
					DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(needData.needId.value);
					sMITH_SECTION = GetSmithSection(deliveryTableData, out targetId);
					if (sMITH_SECTION != 0)
					{
						return sMITH_SECTION;
					}
				}
				break;
			default:
				sMITH_SECTION = SMITH_SECTION.INVALID;
				break;
			}
		}
		return sMITH_SECTION;
	}

	private void ToSmith(SMITH_SECTION section, XorUInt targetId)
	{
		if ((uint)targetId == 0)
		{
			ToSmith();
			return;
		}
		if ((uint)(section - 1) <= 2u)
		{
			EquipItemInfo growEquipItem = GetGrowEquipItem(section, targetId);
			if (growEquipItem != null)
			{
				GetOrCreateSmithData<SmithManager.SmithGrowData>().selectEquipData = growEquipItem;
				if (growEquipItem.IsLevelMax() && growEquipItem.tableData.IsEvolve())
				{
					ChangeSmithScene("SmithEvolve");
				}
				else
				{
					ChangeSmithScene("SmithGrow");
				}
				return;
			}
		}
		ToSmith();
	}

	private EquipItemInfo GetGrowEquipItem(SMITH_SECTION section, XorUInt targetId)
	{
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.ALL_EQUIP;
		EquipItemInfo[] array = Array.FindAll(MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone(), (EquipItemInfo e) => e.tableID == (uint)targetId || e.tableData.shadowEvolveEquipItemId == (uint)targetId);
		Array.Sort(array, (EquipItemInfo a, EquipItemInfo b) => (b.uniqueID - a.uniqueID < 0) ? 1 : ((b.uniqueID - a.uniqueID != 0) ? (-1) : 0));
		EquipItemInfo equipItemInfo = null;
		switch (section)
		{
		case SMITH_SECTION.EQUIP_GROW:
			foreach (EquipItemInfo equipItemInfo3 in array)
			{
				if (equipItemInfo == null || (equipItemInfo.IsLevelMax() && !equipItemInfo3.IsLevelMax()))
				{
					equipItemInfo = equipItemInfo3;
				}
			}
			break;
		case SMITH_SECTION.EQUIP_EXCEED:
			foreach (EquipItemInfo equipItemInfo4 in array)
			{
				if (equipItemInfo == null || (equipItemInfo.IsExceedMax() && !equipItemInfo4.IsExceedMax()))
				{
					equipItemInfo = equipItemInfo4;
				}
			}
			break;
		case SMITH_SECTION.EQUIP_EVOLVE:
			foreach (EquipItemInfo equipItemInfo2 in array)
			{
				if (equipItemInfo == null || (equipItemInfo.IsLevelAndEvolveMax() && !equipItemInfo2.IsLevelAndEvolveMax()))
				{
					equipItemInfo = equipItemInfo2;
				}
			}
			break;
		}
		if (equipItemInfo != null && equipItemInfo.IsLevelAndEvolveMax() && equipItemInfo.IsExceedMax())
		{
			equipItemInfo = null;
		}
		return equipItemInfo;
	}

	private SortSettings CreateSortSettings()
	{
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		if (smithData.selectCreateEquipItemType < SortBase.TYPE.ARMOR || smithData.selectCreateEquipItemType == SortBase.TYPE.WEAPON_ALL)
		{
			if (smithData.selectCreateEquipItemType == SortBase.TYPE.WEAPON_ALL)
			{
				return SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
			}
			return SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
		}
		if (smithData.selectCreateEquipItemType == SortBase.TYPE.ARMOR_ALL)
		{
			return SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
		}
		return SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
	}

	private T GetOrCreateSmithData<T>() where T : SmithManager.SmithDataBase, new()
	{
		T val = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<T>();
		if (val == null)
		{
			val = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<T>();
		}
		return val;
	}

	private void ChangeSmithScene(string to_section)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Smith", to_section);
	}
}
