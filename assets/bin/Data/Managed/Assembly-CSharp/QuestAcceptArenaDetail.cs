using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptArenaDetail : QuestDeliveryDetail
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
		BTN_CREATE_OFF,
		OBJ_RANK_UP_ROOT,
		OBJ_RANK_UP,
		TEX_RANK_PRE,
		TEX_RANK_NEW,
		OBJ_PARTICLE,
		BTN_COMPLETE_RANK_UP,
		LBL_LIMIT_TIME_NAME,
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

	private ArenaTable.ArenaData arenaData;

	private const string WINDOW_SPRITE = "RequestWindowBase_Arena";

	private const string MESSAGE_SPRITE = "Checkhukidashi_Rush";

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "PointShopGetPointTable";
			yield return "DeliveryRewardTable";
			yield return "FieldMapTable";
			yield return "ArenaTable";
		}
	}

	public override void Initialize()
	{
		base.Initialize();
		arenaData = info.GetArenaData();
		UITexture component = GetCtrl(UI.TEX_RUSH_IMAGE).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(arenaData.rank));
		if ((base.isComplete || isNotice) && !isCompletedEventDelivery)
		{
			SetActive((Enum)UI.BTN_CREATE, is_visible: false);
			SetActive((Enum)UI.BTN_CREATE_OFF, is_visible: false);
		}
		else if (info.GetConditionType() != DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
		{
			this.StartCoroutine(StartPredownload());
		}
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_DROP_REWARD, is_visible: true);
		SetActive((Enum)UI.OBJ_CLEAR_REWARD, is_visible: true);
		SetActive((Enum)UI.OBJ_RANK_UP_ROOT, is_visible: false);
		SetActive((Enum)UI.BTN_COMPLETE_RANK_UP, is_visible: false);
		base.UpdateUI();
		SetActive((Enum)UI.BTN_CREATE_OFF, is_visible: false);
		UpdateTime();
		SetSprite(baseRoot, UI.SPR_WINDOW, "RequestWindowBase_Arena");
		SetDifficultySprite();
		UpdateRewardInfo();
		if (info.GetConditionType() == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
		{
			SetActive((Enum)UI.BTN_CREATE, is_visible: false);
			SetActive((Enum)UI.BTN_CREATE_OFF, is_visible: false);
		}
	}

	private void UpdateTime()
	{
		if (info.GetConditionType() == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
		{
			MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryDataAllNeeds((int)info.id, out int have, out int need, out string _, out string _);
			if (base.isComplete)
			{
				have = need;
			}
			SetLabelText((Enum)UI.LBL_LIMIT_TIME, $"{have}/{need}");
			SetLabelText((Enum)UI.LBL_LIMIT_TIME_NAME, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 17u));
		}
		else
		{
			int num = QuestUtility.ToSecByMilliSec(arenaData.timeLimit);
			SetLabelText((Enum)UI.LBL_LIMIT_TIME, $"{num / 60}:{num % 60:D2}");
		}
		SetLabelText((Enum)UI.LBL_RUSH_LEVEL, string.Empty);
	}

	protected override void UpdateNPC(string map_name, string enemy_name)
	{
		SetActive((Enum)UI.CHARA_ALL, is_visible: false);
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
		submissionFrame = null;
	}

	protected override void OnEndCompletetween(bool is_unlock_portal, string effectName)
	{
		base.OnEndCompletetween(is_unlock_portal, effectName);
	}

	private IEnumerator PlayRankUpEffect(bool is_unlock_portal, string effectName)
	{
		ParticleSystem particle = GetCtrl(UI.OBJ_PARTICLE).GetComponent<ParticleSystem>();
		particle.GetComponent<ParticleSystemRenderer>().get_sharedMaterial().set_renderQueue(4000);
		yield return (object)new WaitForSeconds(1f);
		SetActive((Enum)UI.OBJ_BASE_FRAME, is_visible: false);
		SetActive((Enum)UI.BTN_COMPLETE_RANK_UP, is_visible: true);
		PlayTween((Enum)UI.OBJ_RANK_UP, forward: true, (EventDelegate.Callback)delegate
		{
			if (is_unlock_portal)
			{
				PlayUnlockPortalTween(effectName, delegate
				{
				});
			}
		}, is_input_block: false, 0);
	}

	private void UpdateRewardInfo()
	{
		SetActive((Enum)UI.OBJ_CLEAR_ICON_ROOT, !isShowDropInfo);
		SetActive((Enum)UI.OBJ_DROP_ICON_ROOT, isShowDropInfo);
		SetActive((Enum)UI.OBJ_CLEAR_REWARD, !isShowDropInfo);
		SetActive((Enum)UI.OBJ_DROP_REWARD, isShowDropInfo);
		SetActive((Enum)UI.OBJ_COMPLETE_ROOT, !isShowDropInfo);
	}

	private void OnQuery_CREATE()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)arenaData.questIds[0]);
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentArenaId(arenaData.id);
		GameSection.SetEventData(info);
	}

	private IEnumerator StartPredownload()
	{
		SetActive((Enum)UI.BTN_CREATE, is_visible: false);
		SetActive((Enum)UI.BTN_CREATE_OFF, is_visible: false);
		List<ResourceInfo> list = new List<ResourceInfo>();
		List<QuestTable.QuestTableData> questDataArray = arenaData.GetQuestDataArray();
		if (questDataArray.IsNullOrEmpty())
		{
			yield break;
		}
		for (int i = 0; i < questDataArray.Count; i++)
		{
			uint mapId = questDataArray[i].mapId;
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
			for (int j = 0; j < 8; j++)
			{
				if (!string.IsNullOrEmpty(data.useEffects[j]))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[j]));
				}
			}
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questDataArray[i].enemyID[0]);
			int modelId = enemyData.modelId;
			string enemyBody = ResourceName.GetEnemyBody(modelId);
			string enemyMaterial = ResourceName.GetEnemyMaterial(modelId);
			string enemyAnim = ResourceName.GetEnemyAnim(enemyData.animId);
			if (!string.IsNullOrEmpty(enemyBody))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody));
			}
			if (!string.IsNullOrEmpty(enemyMaterial))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody));
			}
			if (!string.IsNullOrEmpty(enemyAnim))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim));
			}
			if (!string.IsNullOrEmpty(enemyData.baseEffectName))
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, enemyData.baseEffectName));
			}
		}
		if (list.Find((ResourceInfo x) => !MonoBehaviourSingleton<ResourceManager>.I.IsCached(x.category, x.packageName)) != null)
		{
			List<string> assetNames = new List<string>();
			foreach (ResourceInfo item2 in list)
			{
				if (!string.IsNullOrEmpty(item2.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(item2.category, item2.packageName))
				{
					assetNames.Add(item2.category.ToAssetBundleName(item2.packageName));
				}
			}
			SetActive((Enum)UI.BTN_CREATE_OFF, is_visible: true);
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
			LoadingQueue loadQueue = new LoadingQueue(this);
			foreach (ResourceInfo item in list)
			{
				if (!string.IsNullOrEmpty(item.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(item.category, item.packageName))
				{
					ResourceManager.downloadOnly = true;
					loadQueue.Load(item.category, item.packageName, null);
					ResourceManager.downloadOnly = false;
					yield return loadQueue.Wait();
				}
			}
		}
		bool pushEnable = true;
		SetActive((Enum)UI.BTN_CREATE, pushEnable);
		SetActive((Enum)UI.BTN_CREATE_OFF, !pushEnable);
	}

	private void SetDifficultySprite()
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		SetActive((Enum)UI.SPR_TYPE_DIFFICULTY, (deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}
}
