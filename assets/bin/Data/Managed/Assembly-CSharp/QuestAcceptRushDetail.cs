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

	private bool isShowDropInfo;

	private QuestTable.QuestTableData questTableData;

	private const string WINDOW_SPRITE = "RequestWindowBase_Rush";

	private const string MESSAGE_SPRITE = "Checkhukidashi_Rush";

	public override void Initialize()
	{
		base.Initialize();
		UITexture component = GetCtrl(UI.TEX_RUSH_IMAGE).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.RUSH_QUEST_ICON, ResourceName.GetRushQuestIconName((int)info.GetQuestData().rushIconId));
		if ((base.isComplete || isNotice) && !isCompletedEventDelivery)
		{
			SetActive((Enum)UI.BTN_JOIN, is_visible: false);
			SetActive((Enum)UI.BTN_CREATE, is_visible: false);
			SetActive((Enum)UI.BTN_AUTO_MATCHING, is_visible: false);
			SetActive((Enum)UI.BTN_JOIN_OFF, is_visible: false);
			SetActive((Enum)UI.BTN_CREATE_OFF, is_visible: false);
			SetActive((Enum)UI.BTN_AUTO_MATCHING_OFF, is_visible: false);
		}
		else
		{
			this.StartCoroutine(StartPredownload());
		}
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_DROP_REWARD, is_visible: true);
		SetActive((Enum)UI.OBJ_CLEAR_REWARD, is_visible: true);
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
		PartyManager.PartySetting setting = new PartyManager.PartySetting(is_lock: true, 0, 0);
		MonoBehaviourSingleton<PartyManager>.I.SendCreate((int)info.needs[0].questId, setting, delegate(bool is_success)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
			}
			GameSection.ResumeEvent(is_success);
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

	protected IEnumerator StartPredownload()
	{
		SetActive((Enum)UI.BTN_JOIN, is_visible: false);
		SetActive((Enum)UI.BTN_CREATE, is_visible: false);
		SetActive((Enum)UI.BTN_AUTO_MATCHING, is_visible: false);
		SetActive((Enum)UI.BTN_JOIN_OFF, is_visible: false);
		SetActive((Enum)UI.BTN_CREATE_OFF, is_visible: false);
		SetActive((Enum)UI.BTN_AUTO_MATCHING_OFF, is_visible: false);
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
			string text = fieldMapData.stageName;
			if (string.IsNullOrEmpty(text))
			{
				text = "ST011D_01";
			}
			StageTable.StageData data = Singleton<StageTable>.I.GetData(text);
			if (data == null)
			{
				yield break;
			}
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SCENE, data.scene));
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SKY, data.sky));
			if (!string.IsNullOrEmpty(data.cameraLinkEffect))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffect));
			}
			if (!string.IsNullOrEmpty(data.cameraLinkEffectY0))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffectY0));
			}
			if (!string.IsNullOrEmpty(data.rootEffect))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.rootEffect));
			}
			for (int i = 0; i < 8; i++)
			{
				if (!string.IsNullOrEmpty(data.useEffects[i]))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[i]));
				}
			}
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)item2.enemyID[0]);
			int modelId = enemyData.modelId;
			string enemyBody = ResourceName.GetEnemyBody(modelId);
			string enemyMaterial = ResourceName.GetEnemyMaterial(modelId);
			string enemyAnim = ResourceName.GetEnemyAnim(enemyData.animId);
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody));
			if (!string.IsNullOrEmpty(enemyMaterial))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody));
			}
			list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim));
			if (!string.IsNullOrEmpty(enemyData.baseEffectName))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, enemyData.baseEffectName));
			}
		}
		if (list.Find((ResourceInfo x) => !MonoBehaviourSingleton<ResourceManager>.I.IsCached(x.category, x.packageName)) != null)
		{
			List<string> assetNames = new List<string>();
			foreach (ResourceInfo item3 in list)
			{
				if (!string.IsNullOrEmpty(item3.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(item3.category, item3.packageName))
				{
					assetNames.Add(item3.category.ToAssetBundleName(item3.packageName));
				}
			}
			SetActive((Enum)UI.BTN_JOIN_OFF, is_visible: true);
			SetActive((Enum)UI.BTN_CREATE_OFF, is_visible: true);
			SetActive((Enum)UI.BTN_AUTO_MATCHING_OFF, is_visible: true);
			yield return ResourceSizeInfo.Init();
			string act = null;
			yield return ResourceSizeInfo.OpenConfirmDialog(ResourceSizeInfo.GetAssetsSizeMB(assetNames.ToArray()), 3002u, CommonDialog.TYPE.YES_NO, delegate(string str)
			{
				act = str;
			});
			if (act == "NO")
			{
				while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
				{
					yield return null;
				}
				DispatchEvent("[BACK]");
				yield break;
			}
			LoadingQueue load_queue = new LoadingQueue(this);
			foreach (ResourceInfo item in list)
			{
				if (!string.IsNullOrEmpty(item.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(item.category, item.packageName))
				{
					ResourceManager.downloadOnly = true;
					load_queue.Load(item.category, item.packageName, null);
					ResourceManager.downloadOnly = false;
					yield return load_queue.Wait();
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

	private void OnQuery_AUTO_MATCHING()
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
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(info.GetQuestData().questID);
		if (questTableData != null)
		{
			GameSection.SetEventData(new object[1]
			{
				questTableData.questType
			});
		}
		PartyManager.PartySetting setting = new PartyManager.PartySetting(is_lock: false, 0, 0);
		MonoBehaviourSingleton<PartyManager>.I.SendCreate((int)info.GetQuestData().questID, setting, delegate(bool is_success)
		{
			if (is_success)
			{
				MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	private void SetDifficultySprite()
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		SetActive((Enum)UI.SPR_TYPE_DIFFICULTY, (deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}
}
