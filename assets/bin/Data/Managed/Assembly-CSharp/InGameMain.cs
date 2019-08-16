using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InGameMain : GameSection
{
	public enum UI
	{
		BTN_INGAME_MENU,
		BTN_QUEST_MENU,
		BTN_REQUEST,
		BTN_EVENT,
		BTN_CHAT,
		WGT_CHAT_PARENT,
		BTN_LOUNGE_MEMBER,
		SPR_BTN_2
	}

	public enum AUDIO
	{
		DROP_ITEM_HEAVY = 10000061,
		DROP_ITEM_LITE = 10000062,
		GET_NORMAL_ITEM = 10000063,
		GET_RARE_ITEM = 10000064,
		GOLD_DROP = 10000065,
		OPEN_TREASURE = 10000071,
		GET_ITEM_NORMAL = 40000153,
		GET_ITEM_RARE = 40000154,
		GET_ITEM_DELIVERY = 40000155,
		ANNOUNCE_CLEAR = 40000030,
		CHANGE_WEAPON = 40000025,
		PORTAL_CHARGE = 40000068,
		PORTAL_FULL_INFO = 40000069,
		PORTAL_CHARGE_FIRE = 40000070,
		ANNOUNCE_NEW_PORTAL = 40000071,
		SKILL_MAX_ATTACK = 40000120,
		BREAK_REGION = 40000156,
		PORTAL_UNLOCK = 40000159,
		HALF_CHARGE = 40000358,
		FULL_CHARGE = 40000359
	}

	private Transform inGameMembers;

	private Transform inGameUIMembers;

	private int prevLevel = -1;

	private bool is_collect_tutorial;

	private bool execTutorial4_1;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "EnemyAngryTable";
			yield return "EnemyActionTable";
			yield return "NpcLevelTable";
			yield return "NpcLevelSpecialTable";
		}
	}

	public CutScenePlayer cutScenePlayer
	{
		get;
		private set;
	}

	public override bool useOnPressBackKey => true;

	public override void OnPressBackKey()
	{
		if (QuestManager.IsValidInGame())
		{
			if (MonoBehaviourSingleton<UIInGameMenu>.IsValid() && MonoBehaviourSingleton<UIInGameMenu>.I.get_gameObject().get_activeInHierarchy())
			{
				MonoBehaviourSingleton<UIInGameMenu>.I.Close();
			}
			else
			{
				DispatchEvent("RETIRE");
			}
		}
		else if (UIInGameFieldMenu.IsValid())
		{
			if (UIInGameFieldMenu.I.IsPopMenu())
			{
				UIInGameFieldMenu.I.OnClickPopMenu();
			}
			else
			{
				DispatchEvent("RETURN");
			}
		}
	}

	public override void Initialize()
	{
		if (MonoBehaviourSingleton<ShopManager>.IsValid())
		{
			MonoBehaviourSingleton<ShopManager>.I.trackPlayerDie = false;
		}
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
		{
			UILabel.OutlineLimit = true;
		}
		else
		{
			UILabel.OutlineLimit = false;
		}
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_MAIN, is_stop: true);
		while (MonoBehaviourSingleton<DataTableManager>.I.IsLoading())
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isRetry)
		{
			bool isWaitQuestLoad = true;
			if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				CoopApp.EnterArenaQuestOffline(delegate(bool isMatching, bool isConnect, bool isRegist, bool isStart)
				{
					isWaitQuestLoad = !isStart;
				});
			}
			else if (MonoBehaviourSingleton<StatusManager>.IsValid() && MonoBehaviourSingleton<StatusManager>.I.assignedCharaInfo != null && MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData != null)
			{
				CoopApp.EnterQuestOfflineAssignedEquipment(null, null, delegate(bool isMatching, bool isConnect, bool isRegist, bool isStart)
				{
					isWaitQuestLoad = !isStart;
				});
			}
			else
			{
				MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)GameSaveData.instance.lastQusetID);
				if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
				{
					CoopApp.EnterSeriesArenaQuestOffline(delegate(bool isMatching, bool isConnect, bool isRegist, bool isStart)
					{
						isWaitQuestLoad = !isStart;
					});
				}
			}
			while (isWaitQuestLoad)
			{
				yield return null;
			}
			if (MonoBehaviourSingleton<QuestManager>.IsValid())
			{
				int currentQuestBGMID = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID();
				SoundManager.RequestBGM(currentQuestBGMID);
			}
			MonoBehaviourSingleton<InGameManager>.I.isRetry = false;
		}
		SetActive((Enum)UI.BTN_QUEST_MENU, QuestManager.IsValidInGame());
		InitializeChatUI();
		if (MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL).Length >= 2)
		{
			SetEvent((Enum)UI.BTN_REQUEST, "QUEST_SELECT_WINDOW", (int)MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId);
		}
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.LoadingStart();
		}
		while (MonoBehaviourSingleton<LoadingProcess>.IsValid())
		{
			yield return null;
		}
		if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<AppMain>.IsValid() && !MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<InGameRecorder>();
		}
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<AttackColliderManager>();
		}
		inGameMembers = new GameObject("InGameMembers").get_transform();
		Transform _transform = inGameMembers;
		_transform.get_transform().set_parent(MonoBehaviourSingleton<AppMain>.I._transform);
		inGameUIMembers = Utility.CreateGameObject("InGameUIMembers", MonoBehaviourSingleton<UIManager>.I._transform, 5);
		LoadingQueue load_queue = new LoadingQueue(this);
		ResourceManager.enableCache = false;
		LoadObject lo_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGame", new string[8]
		{
			"InGameSettingsManager",
			"InGameCameraManager",
			"PuniConManager",
			"PlayerAttackInfo_00",
			"PlayerAttackInfo_01",
			"PlayerAttackInfo_02",
			"PlayerAttackInfo_04",
			"PlayerAttackInfo_05"
		});
		LoadObject lo_tutorial_prefab = null;
		if (!TutorialStep.HasFirstDeliveryCompleted() && TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02))
		{
			lo_tutorial_prefab = load_queue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialFieldHelper");
		}
		LoadObject lo_link_resources;
		if (FieldManager.IsValidInTutorial())
		{
			lo_link_resources = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", new string[3]
			{
				"InGameLinkResourcesCommon",
				"InGameLinkResourcesQuest",
				"InGameLinkResourcesField"
			});
		}
		else
		{
			List<string> list = new List<string>(3);
			list.Add("InGameLinkResourcesCommon");
			if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest())
			{
				list.Add("InGameLinkResourcesQuest");
			}
			if (FieldManager.IsValidInGameNoBoss() || InGameManager.IsValidInGameTest())
			{
				list.Add("InGameLinkResourcesField");
			}
			lo_link_resources = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", list.ToArray());
		}
		ResourceManager.enableCache = true;
		VorgonPreEventController vorgonPreEventController = null;
		if (MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			QuestManager.VorgonQuetType vorgonQuestType = MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType();
			if (vorgonQuestType != 0)
			{
				GameObject questControllerObj = new GameObject("VorgonQuestController");
				vorgonPreEventController = questControllerObj.AddComponent<VorgonPreEventController>();
				vorgonPreEventController.set_enabled(false);
				questControllerObj.get_transform().set_parent(_transform);
				if (vorgonQuestType == QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN)
				{
					string path = "ev001_CutScene";
					bool wait = true;
					cutScenePlayer = questControllerObj.AddComponent<CutScenePlayer>();
					cutScenePlayer.Init(path, delegate
					{
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
				}
			}
		}
		bool isExploreMiniMapActive = MonoBehaviourSingleton<ExploreMiniMap>.IsValid() && MonoBehaviourSingleton<ExploreMiniMap>.I.get_gameObject().get_activeSelf();
		if (isExploreMiniMapActive)
		{
			MonoBehaviourSingleton<ExploreMiniMap>.I.Preload(load_queue);
		}
		LoadObject lo_ingame_rush = null;
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(isOnlyEvent: true))
		{
			lo_ingame_rush = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameRush");
		}
		LoadObject loAdditionalDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameAdditionalDamageNum");
		LoadObject loPlayerDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGamePlayerDamageNum");
		LoadObject loDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameDamageNum");
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(20);
		}
		Transform _setting_transform = null;
		InGameSettingsManager.Player player_setting = null;
		ResourceObject[] loadedObjects = lo_prefabs.loadedObjects;
		foreach (ResourceObject resourceObject in loadedObjects)
		{
			string name = resourceObject.obj.get_name();
			if (name.Contains("PlayerAttackInfo"))
			{
				if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					if (_setting_transform == null)
					{
						_setting_transform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
					}
					if (player_setting == null)
					{
						player_setting = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
						player_setting.attackInfosAll = new AttackInfo[0];
					}
					GameObject gameObject = ResourceUtility.Realizes(resourceObject.obj, _setting_transform).get_gameObject();
					AttackInfos component = gameObject.GetComponent<AttackInfos>();
					player_setting.weaponAttackInfoList.Add(component);
					player_setting.attackInfosAll = Utility.CreateMergedArray(player_setting.attackInfosAll, component.attackHitInfos);
				}
			}
			else if (name.Contains("PuniConManager"))
			{
				ResourceUtility.Realizes(resourceObject.obj, inGameUIMembers, 5);
				MonoBehaviourSingleton<PuniConManager>.I.enableMultiTouch = true;
			}
			else
			{
				ResourceUtility.Realizes(resourceObject.obj, _transform);
			}
		}
		ResourceObject[] loadedObjects2 = lo_link_resources.loadedObjects;
		foreach (ResourceObject resourceObject2 in loadedObjects2)
		{
			ResourceUtility.Realizes(resourceObject2.obj, MonoBehaviourSingleton<InGameSettingsManager>.I._transform);
		}
		Utility.CreateGameObjectAndComponent("StageObjectManager", _transform);
		Utility.CreateGameObjectAndComponent("TargetMarkerManager", _transform);
		Utility.CreateGameObjectAndComponent("InGameProgress", _transform);
		Utility.CreateGameObjectAndComponent("AIManager", _transform);
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != null)
		{
			MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.get_gameObject().SetActive(true);
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
			{
				MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
			}
		}
		if (lo_tutorial_prefab != null)
		{
			Transform val = ResourceUtility.Realizes(lo_tutorial_prefab.loadedObject, inGameUIMembers);
			if (val != null)
			{
				UITutorialFieldHelper component2 = val.GetComponent<UITutorialFieldHelper>();
				if (component2 != null)
				{
					component2.Setup(this);
				}
			}
		}
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(isOnlyEvent: true))
		{
			GameObject val2 = Object.Instantiate(lo_ingame_rush.loadedObject) as GameObject;
			val2.get_transform().set_parent(MonoBehaviourSingleton<UIContinueButton>.I._transform.get_parent());
			val2.get_transform().set_localPosition(Vector3.get_zero());
			val2.get_transform().set_localRotation(Quaternion.get_identity());
			val2.get_transform().set_localScale(Vector3.get_one());
			UIStaticPanelChanger componentInParent = val2.GetComponentInParent<UIStaticPanelChanger>();
			MonoBehaviourSingleton<UISpectatorButton>.I.Initialize(componentInParent);
		}
		if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
		{
			MonoBehaviourSingleton<UIDamageManager>.I.RegisterDamageNumResources(loDamageNum.loadedObject, loPlayerDamageNum.loadedObject, loAdditionalDamageNum.loadedObject);
		}
		string stage_name = (!MonoBehaviourSingleton<InGameManager>.IsValid()) ? "ST011D_01" : MonoBehaviourSingleton<InGameManager>.I.GetCurrentStageName();
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetExternalStageName(stage_name);
		}
		MonoBehaviourSingleton<StageManager>.I.LoadStage(stage_name);
		while (MonoBehaviourSingleton<StageManager>.I.isLoading)
		{
			yield return null;
		}
		if (!MonoBehaviourSingleton<StageManager>.I.isValidInside)
		{
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(40);
		}
		InGameSettingsManager inGameSettings = MonoBehaviourSingleton<InGameSettingsManager>.I;
		GlobalSettingsManager.LinkResources globalResources = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources;
		List<string> loadEffectNames = new List<string>();
		for (int l = 0; l < 3; l++)
		{
			InGameSettingsManager.UseResources useResources = null;
			switch (l)
			{
			case 0:
				useResources = globalResources.inGameCommonResources;
				break;
			case 1:
				if (FieldManager.IsValidInGameNoQuest() || FieldManager.IsValidInGameNoBoss())
				{
					useResources = inGameSettings.useResourcesField;
				}
				break;
			case 2:
				if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest() || FieldManager.IsValidInTutorial())
				{
					useResources = globalResources.inGameQuestResources;
				}
				break;
			}
			if (useResources != null)
			{
				string[] effects = useResources.effects;
				foreach (string text in effects)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text);
					loadEffectNames.Add(text);
				}
				string[] uiEffects = useResources.uiEffects;
				foreach (string name2 in uiEffects)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, name2);
				}
			}
		}
		InGameSettingsManager.Player player = inGameSettings.player;
		string[] stunnedEffectList = globalResources.stunnedEffectList;
		foreach (string text2 in stunnedEffectList)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text2);
			loadEffectNames.Add(text2);
		}
		string[] charmEffectList = globalResources.charmEffectList;
		foreach (string text3 in charmEffectList)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text3);
			loadEffectNames.Add(text3);
		}
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.battleStartEffectName);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.changeWeaponEffectName);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.spActionStartEffectName);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.arrowBleedOtherEffectName);
		loadEffectNames.Add(globalResources.battleStartEffectName);
		loadEffectNames.Add(globalResources.changeWeaponEffectName);
		loadEffectNames.Add(globalResources.spActionStartEffectName);
		loadEffectNames.Add(globalResources.arrowBleedOtherEffectName);
		int[] elementHitSEIDs = inGameSettings.enemy.elementHitSEIDs;
		foreach (int se_id in elementHitSEIDs)
		{
			load_queue.CacheSE(se_id);
		}
		if (FieldManager.IsValidInGameNoBoss())
		{
			string[] effectNames = inGameSettings.portal.effectNames;
			foreach (string text4 in effectNames)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text4);
				loadEffectNames.Add(text4);
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, inGameSettings.fieldDrop.tresureBoxOpenEffect);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, inGameSettings.portal.pointGetEffectName);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, inGameSettings.portal.pointEffect.normalEffectName);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, inGameSettings.portal.pointEffect.largeEffectName);
			loadEffectNames.Add(inGameSettings.fieldDrop.tresureBoxOpenEffect);
			loadEffectNames.Add(inGameSettings.portal.pointGetEffectName);
			loadEffectNames.Add(inGameSettings.portal.pointEffect.normalEffectName);
			loadEffectNames.Add(inGameSettings.portal.pointEffect.largeEffectName);
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(load_queue);
			}
			load_queue.CacheAnimDataUseResource(MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop.animEventData);
		}
		else
		{
			InGameSettingsManager.ShadowSealingParam shadowSealingParam = inGameSettings.debuff.shadowSealingParam;
			if (shadowSealingParam.startSeId != 0)
			{
				load_queue.CacheSE(shadowSealingParam.startSeId);
			}
			if (shadowSealingParam.loopSeId != 0)
			{
				load_queue.CacheSE(shadowSealingParam.loopSeId);
			}
			if (shadowSealingParam.endSeId != 0)
			{
				load_queue.CacheSE(shadowSealingParam.endSeId);
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.shadowSealingEffectName);
			loadEffectNames.Add(globalResources.shadowSealingEffectName);
		}
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.enemyOtherSimpleHitEffectName);
		loadEffectNames.Add(globalResources.enemyOtherSimpleHitEffectName);
		string freezeEffect = "ef_btl_pl_frozen_01";
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, freezeEffect);
		loadEffectNames.Add(freezeEffect);
		string shadowSealingEffect = "ef_btl_wsk_bow_01_04";
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, shadowSealingEffect);
		loadEffectNames.Add(shadowSealingEffect);
		string concussionEffect = "ef_btl_enm_flinch_01";
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, concussionEffect);
		loadEffectNames.Add(concussionEffect);
		InGameSettingsManager.Concussion concussionParam = inGameSettings.debuff.concussion;
		if (concussionParam.startSeId != 0)
		{
			load_queue.CacheSE(concussionParam.startSeId);
		}
		if (concussionParam.loopSeId != 0)
		{
			load_queue.CacheSE(concussionParam.loopSeId);
		}
		if (concussionParam.endSeId != 0)
		{
			load_queue.CacheSE(concussionParam.endSeId);
		}
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.effectName);
		MonoBehaviourSingleton<InGameProgress>.I.CacheUseResources(load_queue, ref loadEffectNames);
		MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(loadEffectNames);
		if (QuestManager.IsValidInGameWaveMatch())
		{
			InGameSettingsManager.WaveMatchParam waveMatchParam = inGameSettings.GetWaveMatchParam();
			load_queue.CacheSE(waveMatchParam.waveJingleId);
			load_queue.CacheSE(waveMatchParam.targetHitSeId);
			load_queue.CacheSE(waveMatchParam.targetBreakSeId);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchParam.targetChangeAnimEffect);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchParam.targetHitEffect);
		}
		CacheAudio(load_queue);
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(70);
		}
		yield return this.StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPop());
		InGameManager.IntervalTransferInfo interval_transfer_info = null;
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			interval_transfer_info = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.InitForArena(interval_transfer_info);
		}
		else
		{
			MonoBehaviourSingleton<StageObjectManager>.I.Init(interval_transfer_info);
		}
		List<Character> load_check_list = new List<Character>();
		int num5 = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; num5 < count; num5++)
		{
			Character character2 = MonoBehaviourSingleton<StageObjectManager>.I.characterList[num5] as Character;
			if (character2.IsCoopNone() && character2.isLoading && MonoBehaviourSingleton<StageObjectManager>.I.FindNonPlayer(character2.id) == null)
			{
				load_check_list.Add(character2);
			}
		}
		int i = 0;
		for (int len = load_check_list.Count; i < len; i++)
		{
			Character character = load_check_list[i];
			while (character.isLoading)
			{
				yield return null;
			}
		}
		if (MonoBehaviourSingleton<StageObjectManager>.I.self != null)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.INITIALIZE;
		}
		if (isExploreMiniMapActive)
		{
			MonoBehaviourSingleton<ExploreMiniMap>.I.Initialize();
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.LoadingFinish();
		}
		if (!FieldManager.IsValidInTutorial())
		{
			yield return MonoBehaviourSingleton<AppMain>.I.ClearMemory(clearObjCaches: false, clearPreloaded: false);
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			float currentLimitTime = MonoBehaviourSingleton<InGameManager>.I.GetCurrentLimitTime();
			MonoBehaviourSingleton<InGameProgress>.I.SetLimitTime(currentLimitTime);
			if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
			{
				MonoBehaviourSingleton<InGameProgress>.I.SetRushRemainTime((int)currentLimitTime);
			}
			if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				MonoBehaviourSingleton<InGameProgress>.I.SetArenaRemainTime(Mathf.FloorToInt(currentLimitTime));
			}
			if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
			{
				MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEnduranceMax(MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.defenseEndurance);
				if (MonoBehaviourSingleton<QuestManager>.I.GetRemainEndurance() > 0f)
				{
					MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEndurance(MonoBehaviourSingleton<QuestManager>.I.GetRemainEndurance());
				}
				else
				{
					MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEndurance(MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.defenseEndurance);
				}
			}
			if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameManager>.I.isAlreadyBattleStarted && MonoBehaviourSingleton<InGameProgress>.I.enableLimitTime)
			{
				MonoBehaviourSingleton<InGameProgress>.I.StartTimer();
			}
			if (FieldManager.IsValidInGameNoQuest())
			{
				MonoBehaviourSingleton<InGameProgress>.I.SetAfkLimitTime();
			}
		}
		MonoBehaviourSingleton<InGameManager>.I.CheckStageInitialState();
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			yield return this.StartCoroutine(MonoBehaviourSingleton<CoopManager>.I.coopStage.DoActivate());
		}
		MonoBehaviourSingleton<StageObjectManager>.I.objectList.ForEach(delegate(StageObject o)
		{
			if (o.controller != null)
			{
				o.controller.SetEnableControll(enable: true, ControllerBase.DISABLE_FLAG.BATTLE_START);
			}
		});
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (self != null)
		{
			self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.INITIALIZE;
			if (MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
			{
				self.SetActionTarget(MonoBehaviourSingleton<StageObjectManager>.I.boss);
			}
		}
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			prevLevel = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.levelUp != null)
			{
				MonoBehaviourSingleton<UIManager>.I.levelUp.GetNowStatus();
			}
		}
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			if (MonoBehaviourSingleton<StageObjectManager>.I.self != null)
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.target = MonoBehaviourSingleton<StageObjectManager>.I.self._transform;
			}
			MonoBehaviourSingleton<InGameCameraManager>.I.AdjustCameraPosition();
		}
		if (FieldManager.IsValidInGameNoQuest() && MonoBehaviourSingleton<DeliveryManager>.IsValid())
		{
			DropTargetMarkerManeger.Create();
		}
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			MonoBehaviourSingleton<EffectManager>.I.enableStock = true;
		}
		if (QuestManager.IsValidInGameDefenseBattle())
		{
			if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.DoDisable();
			}
		}
		else if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.DoDisable();
		}
		yield return 0;
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_MAIN, is_stop: false);
		if (vorgonPreEventController != null)
		{
			vorgonPreEventController.set_enabled(true);
		}
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.ResetSpActionGaugeState();
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.ResetSpActionGaugeState();
		}
		if (MonoBehaviourSingleton<StageObjectManager>.I.self != null)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.ResetShadowSealingUI();
			MonoBehaviourSingleton<StageObjectManager>.I.self.ResetConcussionUI();
		}
		SyncRotatePosition();
		if (UIInGameFieldMenu.IsValid() && !UIInGameFieldMenu.I.IsPopMenu())
		{
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			if (fieldMapData != null)
			{
				if (!FieldManager.HasWorldMap(fieldMapData.mapID))
				{
					UIInGameFieldMenu.I.SetDisableMapButton(disable: true);
				}
			}
			else
			{
				UIInGameFieldMenu.I.SetDisableMapButton(disable: true);
			}
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
			{
				UIInGameFieldMenu.I.SetDisableEventButton(disable: true);
			}
		}
		bool waitGetAutoTime = true;
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.autoBattleButton.GetAutoPlayTime(delegate
			{
				waitGetAutoTime = false;
			});
		}
		while (waitGetAutoTime)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 3)
		{
		}
		if (MonoBehaviourSingleton<UIQuestRepeat>.IsValid())
		{
			MonoBehaviourSingleton<UIQuestRepeat>.I.InitData();
		}
		base.Initialize();
	}

	public void SetMapButtonState()
	{
		if (!UIInGameFieldMenu.IsValid())
		{
			return;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		if (fieldMapData != null)
		{
			if (!FieldManager.HasWorldMap(fieldMapData.mapID))
			{
				UIInGameFieldMenu.I.SetDisableMapButton(disable: true);
			}
		}
		else
		{
			UIInGameFieldMenu.I.SetDisableMapButton(disable: true);
		}
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
		{
			UIInGameFieldMenu.I.SetDisableEventButton(disable: true);
		}
		else
		{
			UIInGameFieldMenu.I.SetDisableEventButton(disable: false);
		}
	}

	protected override void OnOpen()
	{
		UIExplorePlayerStatusList componentInChildren = this.GetComponentInChildren<UIExplorePlayerStatusList>(true);
		if (Object.op_Implicit(componentInChildren))
		{
			componentInChildren.get_gameObject().SetActive(false);
		}
		OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		base.OnOpen();
	}

	public override void StartSection()
	{
		if (UITutorialFieldHelper.IsValid())
		{
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
			switch (UITutorialFieldHelper.m_State)
			{
			case UITutorialFieldHelper.MessageState.CollectItemImg:
			case UITutorialFieldHelper.MessageState.CollectItem:
				UITutorialFieldHelper.I.OpenTutorialFirstDelivery();
				break;
			case UITutorialFieldHelper.MessageState.BackHome:
				NoticeTutorialOnCollectItem();
				TutorialStep4_1();
				break;
			}
		}
		PortalUnlockEvent portalUnlockEvent = null;
		List<PortalObject> portalObjectList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
		if (portalObjectList != null)
		{
			for (int i = 0; i < portalObjectList.Count; i++)
			{
				if (portalObjectList[i].isClearOrder && GameSaveData.instance.isNewReleasePortal(portalObjectList[i].portalID))
				{
					if (portalUnlockEvent == null)
					{
						portalUnlockEvent = MonoBehaviourSingleton<InGameManager>.I.get_gameObject().AddComponent<PortalUnlockEvent>();
						portalUnlockEvent.SetOnEndAllEvent(SetCoopStageReady);
					}
					portalUnlockEvent.AddPortal(portalObjectList[i]);
					GameSaveData.instance.newReleasePortals.Remove(portalObjectList[i].portalID);
				}
			}
		}
		if (QuestManager.IsValidInGameExplore())
		{
			InitializeExplorePlayerStatuses();
		}
		if (portalUnlockEvent == null)
		{
			SetCoopStageReady();
		}
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.UpdateUnreadMessage();
		}
		base.StartSection();
	}

	private void SetCoopStageReady()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SetReadyToPlay();
		}
	}

	public override void UpdateUI()
	{
		int completableDeliveryNum = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum();
		int completableEventDeliveryNum = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum();
		int num = completableDeliveryNum - completableEventDeliveryNum;
		SetBadge((Enum)UI.BTN_REQUEST, num, 3, -5, -5, is_scale_normalize: false);
		SetBadge((Enum)UI.BTN_EVENT, completableEventDeliveryNum, 3, -5, -5, is_scale_normalize: false);
		if (LoungeMatchingManager.IsValidInLounge())
		{
			int num2 = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetMemberCount() - 1;
			SetBadge((Enum)UI.BTN_LOUNGE_MEMBER, num2, 3, -5, -5, is_scale_normalize: false);
		}
		if (UIInGameFieldMenu.IsValid() && !UIInGameFieldMenu.I.IsPopMenu())
		{
			SetBadge((Enum)UI.BTN_INGAME_MENU, completableDeliveryNum, 3, -5, -5, is_scale_normalize: false);
		}
	}

	public void NoticeTutorialOnCollectItem()
	{
		is_collect_tutorial = true;
	}

	public void OnQuery_MENU_POP_INGAME()
	{
		if (UIInGameFieldMenu.IsValid())
		{
			TutorialStep4_2();
			if (!UIInGameFieldMenu.I.IsPopMenu())
			{
				SetBadge((Enum)UI.BTN_INGAME_MENU, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(), 3, -5, -5, is_scale_normalize: false);
			}
			else
			{
				SetBadge((Enum)UI.BTN_INGAME_MENU, 0, 3, -5, -5, is_scale_normalize: false);
			}
		}
	}

	private void TutorialStep4_1()
	{
		if (is_collect_tutorial)
		{
			execTutorial4_1 = true;
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: true);
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("InGameScene", "TutorialStep4_1_1");
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.TUTORIAL;
			}
		}
	}

	private void TutorialStep4_2()
	{
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
		if (is_collect_tutorial)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("InGameScene", "TutorialStep4_1_2");
		}
	}

	private void OnCloseDialog()
	{
		if (is_collect_tutorial && !execTutorial4_1)
		{
			execTutorial4_1 = true;
			TutorialStep4_1();
		}
	}

	public void OnNoticeCompletedDelivery()
	{
		RefreshUI();
		TutorialStep4_1();
	}

	private void OnDisable()
	{
		if (inGameMembers != null)
		{
			MonoBehaviourSingleton<InGameManager>.I.SetIntervalTransferSelf();
			Object.DestroyImmediate(inGameMembers.get_gameObject());
			inGameMembers = null;
		}
		if (inGameUIMembers != null)
		{
			Object.DestroyImmediate(inGameUIMembers.get_gameObject());
			inGameUIMembers = null;
		}
		if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<DropTargetMarkerManeger>.I);
		}
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			MonoBehaviourSingleton<EffectManager>.I.enableStock = false;
		}
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.RemoveObserver(this);
		}
		if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<AttackColliderManager>.I);
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != null)
		{
			MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
		}
	}

	private void OnQuery_BINGO()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<QuestManager>.I.SendGetBingoEventList(delegate
		{
			List<Network.EventData> validBingoDataListInSection = MonoBehaviourSingleton<QuestManager>.I.GetValidBingoDataListInSection();
			if (validBingoDataListInSection != null && validBingoDataListInSection.Count > 0)
			{
				if (validBingoDataListInSection.Count == 1)
				{
					Network.EventData firstEvent = validBingoDataListInSection[0];
					List<DeliveryTable.DeliveryData> deliveryTableDataList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryTableDataList(do_sort: false);
					List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
					int num = (from d in deliveryTableDataList
					where d.IsEvent() && d.eventID == firstEvent.eventId
					select d).Count();
					int num2 = 0;
					for (int i = 0; i < clearStatusDelivery.Count; i++)
					{
						ClearStatusDelivery clearStatusDelivery2 = clearStatusDelivery[i];
						DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)clearStatusDelivery2.deliveryId);
						if (deliveryTableData == null)
						{
							Log.Warning("DeliveryTable Not Found : dId " + clearStatusDelivery2.deliveryId);
						}
						else if (deliveryTableData.IsEvent() && deliveryTableData.eventID == firstEvent.eventId && clearStatusDelivery2.deliveryStatus == 3)
						{
							num2++;
						}
					}
					if (num + num2 == 18)
					{
						GameSection.ChangeStayEvent("MINI_BINGO");
					}
				}
				else
				{
					GameSection.ChangeStayEvent("MINI_BINGO");
				}
			}
			GameSection.ResumeEvent(is_resume: true);
		});
	}

	private void OnQuery_RETIRE()
	{
	}

	private void OnQuery_LOUNGE_MEMBER_LIST()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate(bool isSuccess)
		{
			GameSection.ResumeEvent(isSuccess);
		});
	}

	private void OnQuery_CONTINUE()
	{
		if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsTutorialCurrentQuest())
		{
			GameSection.SetEventData(StringTable.Get(STRING_CATEGORY.IN_GAME, 1007u));
			return;
		}
		int num = 0;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_CONTINUE_USE_CRYSTAL;
		}
		GameSection.SetEventData(new object[1]
		{
			num
		});
	}

	private void OnQuery_WORLDMAP()
	{
		int num = 0;
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		if (fieldMapData != null)
		{
			num = (int)fieldMapData.regionId;
		}
		MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = true;
		if (FieldManager.HasWorldMap(fieldMapData.mapID))
		{
			RequestEvent("DIRECT_REGION", num);
		}
		else
		{
			GameSceneEvent.Cancel();
		}
		CloseChatWindow();
	}

	private void OnQuery_InGameMainRetireConfirm_YES()
	{
		if (MonoBehaviourSingleton<UIContinueButton>.IsValid())
		{
			MonoBehaviourSingleton<UIContinueButton>.I.DoRetire();
		}
	}

	private void OnQuery_InGameMainContinueConfirm_YES()
	{
		if (MonoBehaviourSingleton<UIContinueButton>.IsValid())
		{
			MonoBehaviourSingleton<UIContinueButton>.I.DoContinue();
		}
	}

	private void OnQuery_InGameMainRestartConfirm_YES()
	{
		if (MonoBehaviourSingleton<UIContinueButton>.IsValid())
		{
			MonoBehaviourSingleton<UIContinueButton>.I.DoRestart();
		}
	}

	private void OnQuery_InGameMainRetryConfirm_YES()
	{
		if (MonoBehaviourSingleton<UIContinueButton>.IsValid())
		{
			MonoBehaviourSingleton<UIContinueButton>.I.DoRetry();
		}
	}

	private void OnQuery_InGameMainPortalNextConfirm_YES()
	{
		if (FieldManager.IsValidInTutorial())
		{
			MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(MonoBehaviourSingleton<InGameProgress>.I.checkPortalObject.portalID);
			MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
			GameSceneEvent.Cancel();
			MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "CharaMake");
		}
		else
		{
			PortalNext();
		}
	}

	private void OnQuery_InGameMainPortalHomeConfirm_YES()
	{
		PortalNext();
	}

	private void OnQuery_InGameMainPortalLoungeConfirm_YES()
	{
		PortalNext();
	}

	private void OnQuery_InGameMainReturnHomeConfirm_YES()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
		}
	}

	private void OnQuery_InGameReturnLoungeConfirm_YES()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
		}
	}

	private void OnQuery_InGameMainPortalQuestConfirm_YES()
	{
		PortalNext();
	}

	private void OnQuery_InGameMainPortalQuestLockConfirm_YES()
	{
		PortalNext();
	}

	private void OnQuery_InGameClearedMainStory_YES()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
		}
	}

	private void OnQuery_InGameClearedMainStory_NO()
	{
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid())
		{
			MonoBehaviourSingleton<DeliveryManager>.I.DeleteCleardDeliveryId();
		}
	}

	private void PortalNext()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			PortalObject checkPortalObject = MonoBehaviourSingleton<InGameProgress>.I.checkPortalObject;
			MonoBehaviourSingleton<InGameProgress>.I.PortalNext(checkPortalObject.portalID);
		}
	}

	private void OnCloseDialog_InGameLoungeKickedDialog()
	{
		if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.KickRoomLeave();
		}
	}

	private void OnCloseDialog_InGameKickedMessage()
	{
		Protocol.Force(delegate
		{
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate
			{
			});
		});
		Protocol.Force(delegate
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.SendInfo(delegate
			{
			});
		});
	}

	private void OnQuery_QUEST_WINDOW()
	{
		CloseChatWindow();
	}

	private void OnQuery_QUEST_SELECT_WINDOW()
	{
		CloseChatWindow();
		int num = (int)GameSection.GetEventData();
		int count = MonoBehaviourSingleton<DeliveryManager>.I.GetNormalDeliveryList(num).Count;
		List<EventData> list = new List<EventData>();
		list.Add(new EventData("QUEST_WINDOW", null));
		list.Add(new EventData("SELECT_NORMAL", null));
		if (count > 0)
		{
			list.Add(new EventData("SELECT_AREA", num));
		}
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(list.ToArray());
	}

	private void OnQuery_RETURN()
	{
		CloseChatWindow();
	}

	private void OnQuery_PLAYER_LIST()
	{
		CloseChatWindow();
	}

	private void OnQuery_OPTION()
	{
		CloseChatWindow();
	}

	private void OnQuery_EVENT_WINDOW()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
		{
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			int count = MonoBehaviourSingleton<DeliveryManager>.I.GetEventDeliveryList(fieldMapData.eventId).Count;
			if (count > 0)
			{
				List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
				foreach (Network.EventData item in eventList)
				{
					if (item.eventId == fieldMapData.eventId)
					{
						EventData[] autoEvents = new EventData[1]
						{
							new EventData("SELECT", item)
						};
						MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
						break;
					}
				}
			}
		}
	}

	private void OnQuery_RALLY_INVITE()
	{
		MonoBehaviourSingleton<UIPlayerStatus>.I.SetDisableRalltBtn(isDisable: true);
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendRally(delegate
		{
			UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 35u), is_important: false, 3f);
		});
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((NOTIFY_FLAG.UPDATE_USER_STATUS & flags) != (NOTIFY_FLAG)0L && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level != prevLevel)
		{
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.OnSetPlayerStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp);
			}
			if (MonoBehaviourSingleton<UIManager>.IsValid())
			{
				MonoBehaviourSingleton<UIManager>.I.levelUp.PlayLevelUp();
			}
			prevLevel = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
			{
				UIInGameFieldMenu.I.SetDisableEventButton(disable: true);
			}
			else
			{
				UIInGameFieldMenu.I.SetDisableEventButton(disable: false);
			}
		}
		if ((NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE & flags) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
			{
				MonoBehaviourSingleton<DropTargetMarkerManeger>.I.UpdateList();
			}
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.DeliveryAddCheck();
			}
		}
		if ((flags & NOTIFY_FLAG.UPDATE_PARTY_INVITE) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != null && TutorialStep.HasAllTutorialCompleted())
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "InGameMain")
				{
					MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
				}
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_RALLY_INVITE) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != null && TutorialStep.HasAllTutorialCompleted())
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "InGameMain")
				{
					MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
				}
			}
		}
		else if ((flags & NOTIFY_FLAG.RESET_DARK_MARKET) != (NOTIFY_FLAG)0L)
		{
			int num = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
			if (num > 0)
			{
				GameSaveData.instance.canShowNoteDarkMarket = true;
				MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 37u), string.Empty);
			}
		}
		if ((flags & NOTIFY_FLAG.LOUNGE_KICKED) != (NOTIFY_FLAG)0L)
		{
			DispatchEvent("LOUNGE_KICKED");
			return;
		}
		if ((flags & NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE) != (NOTIFY_FLAG)0L && MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
		{
			RefreshUI();
		}
		if ((flags & NOTIFY_FLAG.TRANSITION_END) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss != null)
			{
				MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.ClearAnnounce();
				if (MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.IsKnockDownRaidBossByRaidBossHp())
				{
					MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.PlayKnockDown();
				}
			}
			if (MonoBehaviourSingleton<FieldManager>.IsValid())
			{
				MonoBehaviourSingleton<FieldManager>.I.MatchingNotice();
			}
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_QUEST_CLEAR_STATUS | base.GetUpdateUINotifyFlags();
	}

	private void InitializeExplorePlayerStatuses()
	{
		UIExplorePlayerStatusList componentInChildren = this.GetComponentInChildren<UIExplorePlayerStatusList>(true);
		if (Object.op_Implicit(componentInChildren))
		{
			componentInChildren.get_gameObject().SetActive(true);
			ExploreStatus exploreStatus = MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus();
			componentInChildren.Initialize(exploreStatus);
		}
	}

	private void InitializeChatUI()
	{
		MonoBehaviourSingleton<ChatManager>.I.CreateRoomChatWithCoopIfNeeded();
		MonoBehaviourSingleton<ChatManager>.I.roomChat.JoinRoom(0);
		UIButton component = GetCtrl(UI.BTN_CHAT).GetComponent<UIButton>();
		if (!(component == null))
		{
			if (!TutorialStep.HasAllTutorialCompleted())
			{
				component.get_gameObject().SetActive(false);
			}
			else if (QuestManager.IsValidInGameSeriesArena())
			{
				component.get_gameObject().SetActive(false);
			}
			else if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
			{
				MonoBehaviourSingleton<UIManager>.I.mainChat.addObserver(this);
				component.onClick.Clear();
				component.onClick.Add(new EventDelegate(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly));
				MonoBehaviourSingleton<UIManager>.I.mainChat.SetRoomChatNameType(!QuestManager.IsValidInGame());
			}
			else
			{
				component.get_gameObject().SetActive(false);
			}
		}
	}

	private void CloseChatWindow()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
	}

	public override void OnModifyChat(MainChat.NOTIFY_FLAG flag)
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			if ((flag & MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE) != 0)
			{
				SetBadge((Enum)UI.BTN_CHAT, MonoBehaviourSingleton<UIManager>.I.mainChat.GetPendingQueueNum(), 1, -5, -5, is_scale_normalize: false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.CLOSE_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).get_gameObject().SetActive(true);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).get_gameObject().SetActive(false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY) != 0)
			{
				GetCtrl(UI.BTN_CHAT).get_gameObject().SetActive(false);
			}
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		SyncRotatePosition();
	}

	private void SyncRotatePosition()
	{
		bool isPortrait = SpecialDeviceManager.IsPortrait;
		Transform ctrl = GetCtrl(UI.WGT_CHAT_PARENT);
		if (!Object.op_Implicit(ctrl))
		{
			return;
		}
		UIWidget component = ctrl.get_gameObject().GetComponent<UIWidget>();
		if (!Object.op_Implicit(component))
		{
			return;
		}
		UIStaticPanelChanger staticPanelChanger = ctrl.GetComponentInParent<UIStaticPanelChanger>();
		if (!Object.op_Implicit(staticPanelChanger))
		{
			return;
		}
		staticPanelChanger.UnLock();
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyInGameChatOpenPosition)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			if (SpecialDeviceManager.IsPortrait)
			{
				component.leftAnchor.Set(0f, specialDeviceInfo.ChatButtonAnchorPortrait.left);
				component.rightAnchor.Set(0f, specialDeviceInfo.ChatButtonAnchorPortrait.right);
				component.bottomAnchor.Set(0f, specialDeviceInfo.ChatButtonAnchorPortrait.bottom);
				component.topAnchor.Set(0f, specialDeviceInfo.ChatButtonAnchorPortrait.top);
			}
			else
			{
				component.leftAnchor.Set(0f, specialDeviceInfo.ChatButtonAnchorLandscape.left);
				component.rightAnchor.Set(0f, specialDeviceInfo.ChatButtonAnchorLandscape.right);
				component.bottomAnchor.Set(0f, specialDeviceInfo.ChatButtonAnchorLandscape.bottom);
				component.topAnchor.Set(0f, specialDeviceInfo.ChatButtonAnchorLandscape.top);
			}
		}
		else
		{
			float num = (!isPortrait) ? 10f : 323f;
			component.topAnchor.Set(0f, num + (float)component.height);
			component.bottomAnchor.Set(0f, num);
		}
		component.UpdateAnchors();
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			staticPanelChanger.Lock();
		});
	}

	private void OnQuery_EVOLVE()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && !object.ReferenceEquals(MonoBehaviourSingleton<StageObjectManager>.I.self, null) && MonoBehaviourSingleton<UIPlayerStatus>.I.IsEnableWeaponChangeButton() && MonoBehaviourSingleton<StageObjectManager>.I.self.ExecEvolve())
		{
			if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.RestrictPopMenu(isRestrict: true);
			}
			if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnduranceStatus>.I.RestrictPopMenu(isRestrict: true);
			}
		}
	}

	private void OnQuery_EVOLVE_TOUCH()
	{
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.RestrictPopMenu(isRestrict: false);
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.RestrictPopMenu(isRestrict: false);
		}
	}
}
