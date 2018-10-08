using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptRushDetail : QuestDeliveryDetail
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
		LBL_LIMIT_TIME,
		OBJ_CHANGE_INFO,
		BTN_CHANGE_INFO,
		OBJ_DROP_ICON_ROOT,
		OBJ_CLEAR_ICON_ROOT,
		OBJ_DROP_REWARD,
		OBJ_CLEAR_REWARD,
		GRD_DROP_REWARD,
		LBL_RUSH_LEVEL,
		TEX_RUSH_IMAGE,
		BTN_AUTO_MATCHING,
		BTN_CREATE_OFF,
		BTN_JOIN_OFF,
		BTN_AUTO_MATCHING_OFF,
		SPR_TYPE_DIFFICULTY
	}

	protected class ResourceInfo
	{
		public RESOURCE_CATEGORY category;

		public string packageName;

		public ResourceInfo(RESOURCE_CATEGORY category, string packageName)
		{
			this.category = category;
			this.packageName = packageName;
		}
	}

	private const int PREDOWNLOAD_MESSAGE_KEY = 3000;

	private const string WINDOW_SPRITE = "RequestWindowBase_Rush";

	private const string MESSAGE_SPRITE = "Checkhukidashi_Rush";

	private bool isShowDropInfo;

	private QuestTable.QuestTableData questTableData;

	public override void Initialize()
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize();
		UITexture component = GetCtrl(UI.TEX_RUSH_IMAGE).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.RUSH_QUEST_ICON, ResourceName.GetRushQuestIconName((int)info.GetQuestData().rushIconId));
		if ((base.isComplete || isNotice) && !isCompletedEventDelivery)
		{
			SetActive((Enum)UI.BTN_JOIN, false);
			SetActive((Enum)UI.BTN_CREATE, false);
			SetActive((Enum)UI.BTN_AUTO_MATCHING, false);
			SetActive((Enum)UI.BTN_JOIN_OFF, false);
			SetActive((Enum)UI.BTN_CREATE_OFF, false);
			SetActive((Enum)UI.BTN_AUTO_MATCHING_OFF, false);
		}
		else
		{
			this.StartCoroutine(StartPredownload());
		}
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_DROP_REWARD, true);
		SetActive((Enum)UI.OBJ_CLEAR_REWARD, true);
		base.UpdateUI();
		questTableData = info.GetQuestData();
		if (questTableData != null)
		{
			int num = (int)questTableData.limitTime;
			SetLabelText((Enum)UI.LBL_LIMIT_TIME, $"{num / 60:D2}:{num % 60:D2}");
			SetLabelText((Enum)UI.LBL_RUSH_LEVEL, string.Empty);
			SetSprite(baseRoot, UI.SPR_WINDOW, "RequestWindowBase_Rush");
			SetSprite(baseRoot, UI.SPR_MESSAGE_BG, "Checkhukidashi_Rush");
			SetDifficultySprite();
			UpdateRewardInfo();
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

	private void CreateRoom()
	{
		GameSection.StayEvent();
		PartyManager.PartySetting setting = new PartyManager.PartySetting(true, 0, 0, 0, 0);
		MonoBehaviourSingleton<PartyManager>.I.SendCreate((int)info.needs[0].questId, setting, delegate(bool is_success)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
			}
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void UpdateRewardInfo()
	{
		SetActive((Enum)UI.OBJ_CLEAR_ICON_ROOT, !isShowDropInfo);
		SetActive((Enum)UI.OBJ_DROP_ICON_ROOT, isShowDropInfo);
		SetActive((Enum)UI.OBJ_CLEAR_REWARD, !isShowDropInfo);
		SetActive((Enum)UI.OBJ_DROP_REWARD, isShowDropInfo);
		SetActive((Enum)UI.OBJ_COMPLETE_ROOT, !isShowDropInfo);
	}

	private void OnQuery_SWITCH_SUBMISSION()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit(targetFrame) && Object.op_Implicit(submissionFrame))
		{
			bool activeSelf = targetFrame.get_gameObject().get_activeSelf();
			targetFrame.get_gameObject().SetActive(!activeSelf);
			submissionFrame.get_gameObject().SetActive(activeSelf);
			isCompletedEventDelivery = true;
			RefreshUI();
		}
	}

	private void OnQuery_CHANGE_INFO()
	{
		isShowDropInfo = !isShowDropInfo;
		UpdateRewardInfo();
	}

	private void OnQuery_CREATE()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(info.needs[0].questId, true);
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
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(info.needs[0].questId, true);
	}

	protected IEnumerator StartPredownload()
	{
		SetActive((Enum)UI.BTN_JOIN, false);
		SetActive((Enum)UI.BTN_CREATE, false);
		SetActive((Enum)UI.BTN_AUTO_MATCHING, false);
		SetActive((Enum)UI.BTN_JOIN_OFF, false);
		SetActive((Enum)UI.BTN_CREATE_OFF, false);
		SetActive((Enum)UI.BTN_AUTO_MATCHING_OFF, false);
		List<ResourceInfo> list = new List<ResourceInfo>();
		List<QuestTable.QuestTableData> targetQuest = QuestTable.GetSameRushQuestData(info.GetQuestData().rushId);
		targetQuest.Remove(info.GetQuestData());
		foreach (QuestTable.QuestTableData item2 in targetQuest)
		{
			uint mapId = item2.mapId;
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
			if (fieldMapData == null)
			{
				yield break;
			}
			string stageName = fieldMapData.stageName;
			if (string.IsNullOrEmpty(stageName))
			{
				stageName = "ST011D_01";
			}
			StageTable.StageData stageData = Singleton<StageTable>.I.GetData(stageName);
			if (stageData == null)
			{
				yield break;
			}
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SCENE, stageData.scene));
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SKY, stageData.sky));
			if (!string.IsNullOrEmpty(stageData.cameraLinkEffect))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.cameraLinkEffect));
			}
			if (!string.IsNullOrEmpty(stageData.cameraLinkEffectY0))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.cameraLinkEffectY0));
			}
			if (!string.IsNullOrEmpty(stageData.rootEffect))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.rootEffect));
			}
			for (int i = 0; i < 8; i++)
			{
				if (!string.IsNullOrEmpty(stageData.useEffects[i]))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.useEffects[i]));
				}
			}
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)item2.enemyID[0]);
			int bodyId = enemyData.modelId;
			string bodyName = ResourceName.GetEnemyBody(bodyId);
			string mateName = ResourceName.GetEnemyMaterial(bodyId);
			string animName = ResourceName.GetEnemyAnim(enemyData.animId);
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, bodyName));
			if (!string.IsNullOrEmpty(mateName))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, bodyName));
			}
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, animName));
			if (!string.IsNullOrEmpty(enemyData.baseEffectName))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, enemyData.baseEffectName));
			}
		}
		if (list.Find((ResourceInfo x) => !MonoBehaviourSingleton<ResourceManager>.I.IsCached(x.category, x.packageName)) != null)
		{
			RequestEvent("ASSET_DOWNLOAD", StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 3000u));
			SetActive((Enum)UI.BTN_JOIN_OFF, true);
			SetActive((Enum)UI.BTN_CREATE_OFF, true);
			SetActive((Enum)UI.BTN_AUTO_MATCHING_OFF, true);
			LoadingQueue load_queue = new LoadingQueue(this);
			foreach (ResourceInfo item3 in list)
			{
				if (!string.IsNullOrEmpty(item3.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(item3.category, item3.packageName))
				{
					ResourceManager.downloadOnly = true;
					load_queue.Load(item3.category, item3.packageName, null, false);
					ResourceManager.downloadOnly = false;
					yield return (object)load_queue.Wait();
				}
			}
		}
		bool pushEnable = true;
		SetActive((Enum)UI.BTN_JOIN, pushEnable);
		SetActive((Enum)UI.BTN_CREATE, pushEnable);
		SetActive((Enum)UI.BTN_AUTO_MATCHING, pushEnable);
		SetActive((Enum)UI.BTN_JOIN_OFF, !pushEnable);
		SetActive((Enum)UI.BTN_CREATE_OFF, !pushEnable);
		SetActive((Enum)UI.BTN_AUTO_MATCHING_OFF, !pushEnable);
	}

	private unsafe void OnQuery_AUTO_MATCHING()
	{
		GameSection.SetEventData(new object[1]
		{
			false
		});
		GameSection.StayEvent();
		int retryCount = 0;
		PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0, 0, 0);
		_003COnQuery_AUTO_MATCHING_003Ec__AnonStorey403 _003COnQuery_AUTO_MATCHING_003Ec__AnonStorey;
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int)info.GetQuestData().questID, retryCount, false, new Action<bool, int, bool, float>((object)_003COnQuery_AUTO_MATCHING_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private unsafe IEnumerator MatchAtRandom(PartyManager.PartySetting setting, int retryCount, float time)
	{
		yield return (object)new WaitForSeconds(time);
		MonoBehaviourSingleton<PartyManager>.I.SendRandomMatching((int)info.needs[0].questId, retryCount, false, new Action<bool, int, bool, float>((object)/*Error near IL_0061: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void OnQuery_AUTO_CREATE_ROOM()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(info.GetQuestData().questID, true);
		if (questTableData != null)
		{
			GameSection.SetEventData(new object[1]
			{
				questTableData.questType
			});
		}
		PartyManager.PartySetting setting = new PartyManager.PartySetting(false, 0, 0, 0, 0);
		MonoBehaviourSingleton<PartyManager>.I.SendCreate((int)info.GetQuestData().questID, setting, delegate(bool is_success)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
			}
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void SetDifficultySprite()
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		SetActive((Enum)UI.SPR_TYPE_DIFFICULTY, (deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}
}
