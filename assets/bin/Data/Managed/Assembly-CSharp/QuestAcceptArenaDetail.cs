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

	private const string WINDOW_SPRITE = "RequestWindowBase_Arena";

	private const string MESSAGE_SPRITE = "Checkhukidashi_Rush";

	private bool isShowDropInfo;

	private ArenaTable.ArenaData arenaData;

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
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize();
		arenaData = info.GetArenaData();
		UITexture component = GetCtrl(UI.TEX_RUSH_IMAGE).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(arenaData.rank));
		if ((base.isComplete || isNotice) && !isCompletedEventDelivery)
		{
			SetActive((Enum)UI.BTN_CREATE, false);
			SetActive((Enum)UI.BTN_CREATE_OFF, false);
		}
		else if (info.GetConditionType(0u) != DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
		{
			this.StartCoroutine(StartPredownload());
		}
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_DROP_REWARD, true);
		SetActive((Enum)UI.OBJ_CLEAR_REWARD, true);
		SetActive((Enum)UI.OBJ_RANK_UP_ROOT, false);
		SetActive((Enum)UI.BTN_COMPLETE_RANK_UP, false);
		base.UpdateUI();
		SetActive((Enum)UI.BTN_CREATE_OFF, false);
		UpdateTime();
		SetSprite(baseRoot, UI.SPR_WINDOW, "RequestWindowBase_Arena");
		SetDifficultySprite();
		UpdateRewardInfo();
		if (info.GetConditionType(0u) == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
		{
			SetActive((Enum)UI.BTN_CREATE, false);
			SetActive((Enum)UI.BTN_CREATE_OFF, false);
		}
	}

	private void UpdateTime()
	{
		if (info.GetConditionType(0u) == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
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
		SetActive((Enum)UI.CHARA_ALL, false);
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

	private unsafe IEnumerator PlayRankUpEffect(bool is_unlock_portal, string effectName)
	{
		ParticleSystem particle = GetCtrl(UI.OBJ_PARTICLE).GetComponent<ParticleSystem>();
		particle.GetComponent<ParticleSystemRenderer>().get_sharedMaterial().set_renderQueue(4000);
		yield return (object)new WaitForSeconds(1f);
		SetActive((Enum)UI.OBJ_BASE_FRAME, false);
		SetActive((Enum)UI.BTN_COMPLETE_RANK_UP, true);
		PlayTween((Enum)UI.OBJ_RANK_UP, true, (EventDelegate.Callback)delegate
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			if (((_003CPlayRankUpEffect_003Ec__Iterator12A)/*Error near IL_00a8: stateMachine*/).is_unlock_portal)
			{
				QuestAcceptArenaDetail _003C_003Ef__this = ((_003CPlayRankUpEffect_003Ec__Iterator12A)/*Error near IL_00a8: stateMachine*/)._003C_003Ef__this;
				string effectName2 = ((_003CPlayRankUpEffect_003Ec__Iterator12A)/*Error near IL_00a8: stateMachine*/).effectName;
				if (_003CPlayRankUpEffect_003Ec__Iterator12A._003C_003Ef__am_0024cache8 == null)
				{
					_003CPlayRankUpEffect_003Ec__Iterator12A._003C_003Ef__am_0024cache8 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				_003C_003Ef__this.PlayUnlockPortalTween(effectName2, _003CPlayRankUpEffect_003Ec__Iterator12A._003C_003Ef__am_0024cache8);
			}
		}, false, 0);
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
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)arenaData.questIds[0], true);
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentArenaId(arenaData.id);
		GameSection.SetEventData(info);
	}

	private IEnumerator StartPredownload()
	{
		SetActive((Enum)UI.BTN_CREATE, false);
		SetActive((Enum)UI.BTN_CREATE_OFF, false);
		List<ResourceInfo> list = new List<ResourceInfo>();
		List<QuestTable.QuestTableData> questDataArray = arenaData.GetQuestDataArray();
		if (!questDataArray.IsNullOrEmpty())
		{
			for (int j = 0; j < questDataArray.Count; j++)
			{
				uint mapId = questDataArray[j].mapId;
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
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questDataArray[j].enemyID[0]);
				int bodyId = enemyData.modelId;
				string bodyName = ResourceName.GetEnemyBody(bodyId);
				string matName = ResourceName.GetEnemyMaterial(bodyId);
				string animName = ResourceName.GetEnemyAnim(enemyData.animId);
				if (!string.IsNullOrEmpty(bodyName))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, bodyName));
				}
				if (!string.IsNullOrEmpty(matName))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, bodyName));
				}
				if (!string.IsNullOrEmpty(animName))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, animName));
				}
				if (!string.IsNullOrEmpty(enemyData.baseEffectName))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, enemyData.baseEffectName));
				}
			}
			if (list.Find((ResourceInfo x) => !MonoBehaviourSingleton<ResourceManager>.I.IsCached(x.category, x.packageName)) != null)
			{
				RequestEvent("ASSET_DOWNLOAD", StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 3000u));
				SetActive((Enum)UI.BTN_CREATE_OFF, true);
				LoadingQueue loadQueue = new LoadingQueue(this);
				foreach (ResourceInfo item in list)
				{
					if (!string.IsNullOrEmpty(item.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(item.category, item.packageName))
					{
						ResourceManager.downloadOnly = true;
						loadQueue.Load(item.category, item.packageName, null, false);
						ResourceManager.downloadOnly = false;
						yield return (object)loadQueue.Wait();
					}
				}
			}
			bool pushEnable = true;
			SetActive((Enum)UI.BTN_CREATE, pushEnable);
			SetActive((Enum)UI.BTN_CREATE_OFF, !pushEnable);
		}
	}

	private void SetDifficultySprite()
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryID);
		SetActive((Enum)UI.SPR_TYPE_DIFFICULTY, (deliveryTableData != null && deliveryTableData.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}
}
