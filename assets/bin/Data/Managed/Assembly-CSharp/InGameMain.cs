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
			if (MonoBehaviourSingleton<UIInGameMenu>.IsValid() && MonoBehaviourSingleton<UIInGameMenu>.I.gameObject.activeInHierarchy)
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
		if (MonoBehaviourSingleton<InGameManager>.I.selfCacheObject != null)
		{
			MonoBehaviourSingleton<InGameManager>.I.DestroySelfCache();
		}
		StartCoroutine(DoInitialize_GG_Optimization());
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
				SoundManager.RequestBGM(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID());
			}
			MonoBehaviourSingleton<InGameManager>.I.isRetry = false;
		}
		SetActive(UI.BTN_QUEST_MENU, QuestManager.IsValidInGame());
		InitializeChatUI();
		if (MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL).Length >= 2)
		{
			SetEvent(UI.BTN_REQUEST, "QUEST_SELECT_WINDOW", (int)MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId);
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
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<InGameRecorder>();
		}
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<AttackColliderManager>();
		}
		inGameMembers = new GameObject("InGameMembers").transform;
		Transform _transform = inGameMembers;
		_transform.transform.parent = MonoBehaviourSingleton<AppMain>.I._transform;
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
				GameObject gameObject = new GameObject("VorgonQuestController");
				vorgonPreEventController = gameObject.AddComponent<VorgonPreEventController>();
				vorgonPreEventController.enabled = false;
				gameObject.transform.parent = _transform;
				if (vorgonQuestType == QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN)
				{
					string cutSceneDataPath = "ev001_CutScene";
					bool wait = true;
					cutScenePlayer = gameObject.AddComponent<CutScenePlayer>();
					cutScenePlayer.Init(cutSceneDataPath, delegate
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
		bool isExploreMiniMapActive = MonoBehaviourSingleton<ExploreMiniMap>.IsValid() && MonoBehaviourSingleton<ExploreMiniMap>.I.gameObject.activeSelf;
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
		Transform transform = null;
		InGameSettingsManager.Player player = null;
		ResourceObject[] loadedObjects = lo_prefabs.loadedObjects;
		foreach (ResourceObject resourceObject in loadedObjects)
		{
			string name = resourceObject.obj.name;
			if (name.Contains("PlayerAttackInfo"))
			{
				if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					if (transform == null)
					{
						transform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
					}
					if (player == null)
					{
						player = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
						player.attackInfosAll = new AttackInfo[0];
					}
					AttackInfos component = ResourceUtility.Realizes(resourceObject.obj, transform).gameObject.GetComponent<AttackInfos>();
					player.weaponAttackInfoList.Add(component);
					InGameSettingsManager.Player player2 = player;
					AttackInfo[] attackInfosAll = player.attackInfosAll;
					AttackInfo[] attackHitInfos = component.attackHitInfos;
					player2.attackInfosAll = Utility.CreateMergedArray(attackInfosAll, attackHitInfos);
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
		loadedObjects = lo_link_resources.loadedObjects;
		for (int j = 0; j < loadedObjects.Length; j++)
		{
			ResourceUtility.Realizes(loadedObjects[j].obj, MonoBehaviourSingleton<InGameSettingsManager>.I._transform);
		}
		Utility.CreateGameObjectAndComponent("StageObjectManager", _transform);
		Utility.CreateGameObjectAndComponent("TargetMarkerManager", _transform);
		Utility.CreateGameObjectAndComponent("InGameProgress", _transform);
		Utility.CreateGameObjectAndComponent("AIManager", _transform);
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != null)
		{
			MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.gameObject.SetActive(value: true);
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
			{
				MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
			}
		}
		if (lo_tutorial_prefab != null)
		{
			Transform transform2 = ResourceUtility.Realizes(lo_tutorial_prefab.loadedObject, inGameUIMembers);
			if (transform2 != null)
			{
				UITutorialFieldHelper component2 = transform2.GetComponent<UITutorialFieldHelper>();
				if (component2 != null)
				{
					component2.Setup(this);
				}
			}
		}
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(isOnlyEvent: true))
		{
			GameObject obj = UnityEngine.Object.Instantiate(lo_ingame_rush.loadedObject) as GameObject;
			obj.transform.parent = MonoBehaviourSingleton<UIContinueButton>.I._transform.parent;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localRotation = Quaternion.identity;
			obj.transform.localScale = Vector3.one;
			UIStaticPanelChanger componentInParent = obj.GetComponentInParent<UIStaticPanelChanger>();
			MonoBehaviourSingleton<UISpectatorButton>.I.Initialize(componentInParent);
		}
		if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
		{
			MonoBehaviourSingleton<UIDamageManager>.I.RegisterDamageNumResources(loDamageNum.loadedObject, loPlayerDamageNum.loadedObject, loAdditionalDamageNum.loadedObject);
		}
		string text = MonoBehaviourSingleton<InGameManager>.IsValid() ? MonoBehaviourSingleton<InGameManager>.I.GetCurrentStageName() : "ST011D_01";
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetExternalStageName(text);
		}
		MonoBehaviourSingleton<StageManager>.I.LoadStage(text);
		while (MonoBehaviourSingleton<StageManager>.I.isLoading)
		{
			yield return null;
		}
		_ = MonoBehaviourSingleton<StageManager>.I.isValidInside;
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(40);
		}
		InGameSettingsManager i2 = MonoBehaviourSingleton<InGameSettingsManager>.I;
		GlobalSettingsManager.LinkResources linkResources = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources;
		List<string> loadEffectNames = new List<string>();
		string[] effects;
		for (int k = 0; k < 3; k++)
		{
			InGameSettingsManager.UseResources useResources = null;
			switch (k)
			{
			case 0:
				useResources = linkResources.inGameCommonResources;
				break;
			case 1:
				if (FieldManager.IsValidInGameNoQuest() || FieldManager.IsValidInGameNoBoss())
				{
					useResources = i2.useResourcesField;
				}
				break;
			case 2:
				if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest() || FieldManager.IsValidInTutorial())
				{
					useResources = linkResources.inGameQuestResources;
				}
				break;
			}
			if (useResources != null)
			{
				effects = useResources.effects;
				foreach (string text2 in effects)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text2);
					loadEffectNames.Add(text2);
				}
				effects = useResources.uiEffects;
				foreach (string name2 in effects)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, name2);
				}
			}
		}
		_ = i2.player;
		effects = linkResources.stunnedEffectList;
		foreach (string text3 in effects)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text3);
			loadEffectNames.Add(text3);
		}
		effects = linkResources.charmEffectList;
		foreach (string text4 in effects)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text4);
			loadEffectNames.Add(text4);
		}
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.battleStartEffectName);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.changeWeaponEffectName);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.spActionStartEffectName);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.arrowBleedOtherEffectName);
		loadEffectNames.Add(linkResources.battleStartEffectName);
		loadEffectNames.Add(linkResources.changeWeaponEffectName);
		loadEffectNames.Add(linkResources.spActionStartEffectName);
		loadEffectNames.Add(linkResources.arrowBleedOtherEffectName);
		int[] elementHitSEIDs = i2.enemy.elementHitSEIDs;
		foreach (int se_id in elementHitSEIDs)
		{
			load_queue.CacheSE(se_id);
		}
		if (FieldManager.IsValidInGameNoBoss())
		{
			effects = i2.portal.effectNames;
			foreach (string text5 in effects)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text5);
				loadEffectNames.Add(text5);
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i2.fieldDrop.tresureBoxOpenEffect);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i2.portal.pointGetEffectName);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i2.portal.pointEffect.normalEffectName);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i2.portal.pointEffect.largeEffectName);
			loadEffectNames.Add(i2.fieldDrop.tresureBoxOpenEffect);
			loadEffectNames.Add(i2.portal.pointGetEffectName);
			loadEffectNames.Add(i2.portal.pointEffect.normalEffectName);
			loadEffectNames.Add(i2.portal.pointEffect.largeEffectName);
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(load_queue);
			}
			load_queue.CacheAnimDataUseResource(MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop.animEventData);
		}
		else
		{
			InGameSettingsManager.ShadowSealingParam shadowSealingParam = i2.debuff.shadowSealingParam;
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
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.shadowSealingEffectName);
			loadEffectNames.Add(linkResources.shadowSealingEffectName);
		}
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.enemyOtherSimpleHitEffectName);
		loadEffectNames.Add(linkResources.enemyOtherSimpleHitEffectName);
		string text6 = "ef_btl_pl_frozen_01";
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text6);
		loadEffectNames.Add(text6);
		string text7 = "ef_btl_wsk_bow_01_04";
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text7);
		loadEffectNames.Add(text7);
		string text8 = "ef_btl_enm_flinch_01";
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text8);
		loadEffectNames.Add(text8);
		InGameSettingsManager.Concussion concussion = i2.debuff.concussion;
		if (concussion.startSeId != 0)
		{
			load_queue.CacheSE(concussion.startSeId);
		}
		if (concussion.loopSeId != 0)
		{
			load_queue.CacheSE(concussion.loopSeId);
		}
		if (concussion.endSeId != 0)
		{
			load_queue.CacheSE(concussion.endSeId);
		}
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.effectName);
		MonoBehaviourSingleton<InGameProgress>.I.CacheUseResources(load_queue, ref loadEffectNames);
		MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(loadEffectNames);
		if (QuestManager.IsValidInGameWaveMatch())
		{
			InGameSettingsManager.WaveMatchParam waveMatchParam = i2.GetWaveMatchParam();
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
		yield return StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPop());
		InGameManager.IntervalTransferInfo intervalTransferInfo = null;
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			intervalTransferInfo = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.InitForArena(intervalTransferInfo);
		}
		else
		{
			MonoBehaviourSingleton<StageObjectManager>.I.Init(intervalTransferInfo);
		}
		List<Character> load_check_list = new List<Character>();
		int l = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; l < count; l++)
		{
			Character character2 = MonoBehaviourSingleton<StageObjectManager>.I.characterList[l] as Character;
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
			yield return StartCoroutine(MonoBehaviourSingleton<CoopManager>.I.coopStage.DoActivate());
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
			vorgonPreEventController.enabled = true;
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
		_ = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
		_ = 3;
		if (MonoBehaviourSingleton<UIQuestRepeat>.IsValid())
		{
			MonoBehaviourSingleton<UIQuestRepeat>.I.InitData();
		}
		Debug.Log("STEP FINISH");
		base.Initialize();
	}

	private IEnumerator DoInitialize_GG_Optimization()
	{
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
		{
			UILabel.OutlineLimit = true;
		}
		else
		{
			UILabel.OutlineLimit = false;
		}
		Application.backgroundLoadingPriority = ThreadPriority.High;
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_MAIN, is_stop: true);
		while (MonoBehaviourSingleton<DataTableManager>.I.IsLoading())
		{
			yield return null;
		}
		bool isWaitQuestLoad = true;
		StartCoroutine(DoInitializeQuest(delegate
		{
			isWaitQuestLoad = false;
		}));
		bool isWaitChatUILoad = true;
		StartCoroutine(DoInitializeChatUI(delegate
		{
			isWaitChatUILoad = false;
		}));
		if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<AppMain>.IsValid() && !MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<InGameRecorder>();
		}
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<AttackColliderManager>();
		}
		inGameMembers = new GameObject("InGameMembers").transform;
		Transform _transform = inGameMembers;
		_transform.transform.parent = MonoBehaviourSingleton<AppMain>.I._transform;
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
		bool isWaitCutScreen = true;
		StartCoroutine(DoInitializeCutScreen(delegate(VorgonPreEventController o)
		{
			vorgonPreEventController = o;
			isWaitCutScreen = false;
		}));
		bool isExploreMiniMapActive = MonoBehaviourSingleton<ExploreMiniMap>.IsValid() && MonoBehaviourSingleton<ExploreMiniMap>.I.gameObject.activeSelf;
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
		while ((isWaitQuestLoad | isWaitChatUILoad | isWaitCutScreen) || load_queue.IsLoading())
		{
			if (load_queue.IsStop())
			{
				yield return null;
			}
			else
			{
				yield return load_queue.Wait();
			}
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(20);
		}
		Transform transform = null;
		InGameSettingsManager.Player player = null;
		ResourceObject[] loadedObjects = lo_prefabs.loadedObjects;
		foreach (ResourceObject resourceObject in loadedObjects)
		{
			string name = resourceObject.obj.name;
			if (name.Contains("PlayerAttackInfo"))
			{
				if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					if (transform == null)
					{
						transform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
					}
					if (player == null)
					{
						player = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
						player.attackInfosAll = new AttackInfo[0];
					}
					AttackInfos component = ResourceUtility.Realizes(resourceObject.obj, transform).gameObject.GetComponent<AttackInfos>();
					player.weaponAttackInfoList.Add(component);
					InGameSettingsManager.Player player2 = player;
					AttackInfo[] attackInfosAll = player.attackInfosAll;
					AttackInfo[] attackHitInfos = component.attackHitInfos;
					player2.attackInfosAll = Utility.CreateMergedArray(attackInfosAll, attackHitInfos);
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
		loadedObjects = lo_link_resources.loadedObjects;
		for (int i = 0; i < loadedObjects.Length; i++)
		{
			ResourceUtility.Realizes(loadedObjects[i].obj, MonoBehaviourSingleton<InGameSettingsManager>.I._transform);
		}
		Utility.CreateGameObjectAndComponent("StageObjectManager", _transform);
		Utility.CreateGameObjectAndComponent("TargetMarkerManager", _transform);
		Utility.CreateGameObjectAndComponent("InGameProgress", _transform);
		Utility.CreateGameObjectAndComponent("AIManager", _transform);
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != null)
		{
			MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.gameObject.SetActive(value: true);
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
			{
				MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
			}
		}
		if (lo_tutorial_prefab != null)
		{
			Transform transform2 = ResourceUtility.Realizes(lo_tutorial_prefab.loadedObject, inGameUIMembers);
			if (transform2 != null)
			{
				UITutorialFieldHelper component2 = transform2.GetComponent<UITutorialFieldHelper>();
				if (component2 != null)
				{
					component2.Setup(this);
				}
			}
		}
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(isOnlyEvent: true))
		{
			GameObject obj = UnityEngine.Object.Instantiate(lo_ingame_rush.loadedObject) as GameObject;
			obj.transform.parent = MonoBehaviourSingleton<UIContinueButton>.I._transform.parent;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localRotation = Quaternion.identity;
			obj.transform.localScale = Vector3.one;
			UIStaticPanelChanger componentInParent = obj.GetComponentInParent<UIStaticPanelChanger>();
			MonoBehaviourSingleton<UISpectatorButton>.I.Initialize(componentInParent);
		}
		if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
		{
			MonoBehaviourSingleton<UIDamageManager>.I.RegisterDamageNumResources(loDamageNum.loadedObject, loPlayerDamageNum.loadedObject, loAdditionalDamageNum.loadedObject);
		}
		bool isWaitLoadScene = true;
		bool isWaitLoadCharacter = true;
		bool isWaitLoadEnemy = true;
		StartCoroutine(DoInitializeSceneLoad(delegate
		{
			isWaitLoadScene = false;
			InGameManager.IntervalTransferInfo intervalTransferInfo = null;
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				intervalTransferInfo = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo;
			}
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				MonoBehaviourSingleton<StageObjectManager>.I.InitForArena(intervalTransferInfo);
			}
			else
			{
				MonoBehaviourSingleton<StageObjectManager>.I.Init(intervalTransferInfo);
			}
			StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPop_GG_Optimize(delegate
			{
				isWaitLoadEnemy = false;
			}, delegate
			{
			}));
			StartCoroutine(DoInitializeCharacter(delegate
			{
				isWaitLoadCharacter = false;
			}));
		}));
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(40);
		}
		InGameSettingsManager i2 = MonoBehaviourSingleton<InGameSettingsManager>.I;
		GlobalSettingsManager.LinkResources linkResources = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources;
		List<string> loadEffectNames = new List<string>();
		string[] effects;
		for (int j = 0; j < 3; j++)
		{
			InGameSettingsManager.UseResources useResources = null;
			switch (j)
			{
			case 0:
				useResources = linkResources.inGameCommonResources;
				break;
			case 1:
				if (FieldManager.IsValidInGameNoQuest() || FieldManager.IsValidInGameNoBoss())
				{
					useResources = i2.useResourcesField;
				}
				break;
			case 2:
				if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest() || FieldManager.IsValidInTutorial())
				{
					useResources = linkResources.inGameQuestResources;
				}
				break;
			}
			if (useResources != null)
			{
				effects = useResources.effects;
				foreach (string text in effects)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text);
					loadEffectNames.Add(text);
				}
				effects = useResources.uiEffects;
				foreach (string name2 in effects)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, name2);
				}
			}
		}
		_ = i2.player;
		effects = linkResources.stunnedEffectList;
		foreach (string text2 in effects)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text2);
			loadEffectNames.Add(text2);
		}
		effects = linkResources.charmEffectList;
		foreach (string text3 in effects)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text3);
			loadEffectNames.Add(text3);
		}
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.battleStartEffectName);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.changeWeaponEffectName);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.spActionStartEffectName);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.arrowBleedOtherEffectName);
		loadEffectNames.Add(linkResources.battleStartEffectName);
		loadEffectNames.Add(linkResources.changeWeaponEffectName);
		loadEffectNames.Add(linkResources.spActionStartEffectName);
		loadEffectNames.Add(linkResources.arrowBleedOtherEffectName);
		int[] elementHitSEIDs = i2.enemy.elementHitSEIDs;
		foreach (int se_id in elementHitSEIDs)
		{
			load_queue.CacheSE(se_id);
		}
		if (FieldManager.IsValidInGameNoBoss())
		{
			effects = i2.portal.effectNames;
			foreach (string text4 in effects)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text4);
				loadEffectNames.Add(text4);
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i2.fieldDrop.tresureBoxOpenEffect);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i2.portal.pointGetEffectName);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i2.portal.pointEffect.normalEffectName);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i2.portal.pointEffect.largeEffectName);
			loadEffectNames.Add(i2.fieldDrop.tresureBoxOpenEffect);
			loadEffectNames.Add(i2.portal.pointGetEffectName);
			loadEffectNames.Add(i2.portal.pointEffect.normalEffectName);
			loadEffectNames.Add(i2.portal.pointEffect.largeEffectName);
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(load_queue);
			}
			load_queue.CacheAnimDataUseResource(MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop.animEventData);
		}
		else
		{
			InGameSettingsManager.ShadowSealingParam shadowSealingParam = i2.debuff.shadowSealingParam;
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
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.shadowSealingEffectName);
			loadEffectNames.Add(linkResources.shadowSealingEffectName);
		}
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.enemyOtherSimpleHitEffectName);
		loadEffectNames.Add(linkResources.enemyOtherSimpleHitEffectName);
		string text5 = "ef_btl_pl_frozen_01";
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text5);
		loadEffectNames.Add(text5);
		string text6 = "ef_btl_wsk_bow_01_04";
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text6);
		loadEffectNames.Add(text6);
		string text7 = "ef_btl_enm_flinch_01";
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text7);
		loadEffectNames.Add(text7);
		InGameSettingsManager.Concussion concussion = i2.debuff.concussion;
		if (concussion.startSeId != 0)
		{
			load_queue.CacheSE(concussion.startSeId);
		}
		if (concussion.loopSeId != 0)
		{
			load_queue.CacheSE(concussion.loopSeId);
		}
		if (concussion.endSeId != 0)
		{
			load_queue.CacheSE(concussion.endSeId);
		}
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.effectName);
		MonoBehaviourSingleton<InGameProgress>.I.CacheUseResources(load_queue, ref loadEffectNames);
		MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(loadEffectNames);
		if (QuestManager.IsValidInGameWaveMatch())
		{
			InGameSettingsManager.WaveMatchParam waveMatchParam = i2.GetWaveMatchParam();
			load_queue.CacheSE(waveMatchParam.waveJingleId);
			load_queue.CacheSE(waveMatchParam.targetHitSeId);
			load_queue.CacheSE(waveMatchParam.targetBreakSeId);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchParam.targetChangeAnimEffect);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchParam.targetHitEffect);
		}
		bool isNotInitCharacter = true;
		while ((isWaitLoadScene | isWaitLoadEnemy | isWaitLoadCharacter) || load_queue.IsLoading())
		{
			if (!isWaitLoadScene && isNotInitCharacter)
			{
				isNotInitCharacter = false;
				CacheAudio(load_queue);
				if (MonoBehaviourSingleton<CoopManager>.IsValid())
				{
					MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(70);
				}
			}
			if (load_queue.IsStop())
			{
				yield return null;
			}
			else
			{
				yield return load_queue.Wait();
			}
		}
		_ = MonoBehaviourSingleton<StageManager>.I.isValidInside;
		Application.backgroundLoadingPriority = ThreadPriority.Low;
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
			yield return StartCoroutine(MonoBehaviourSingleton<CoopManager>.I.coopStage.DoActivate());
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
			vorgonPreEventController.enabled = true;
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
		_ = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
		_ = 3;
		if (MonoBehaviourSingleton<UIQuestRepeat>.IsValid())
		{
			MonoBehaviourSingleton<UIQuestRepeat>.I.InitData();
		}
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		base.Initialize();
	}

	private IEnumerator DoInitializeQuest(Action callBack)
	{
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
				SoundManager.RequestBGM(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID());
			}
			MonoBehaviourSingleton<InGameManager>.I.isRetry = false;
		}
		callBack();
	}

	private IEnumerator DoInitializeChatUI(Action callBack)
	{
		SetActive(UI.BTN_QUEST_MENU, QuestManager.IsValidInGame());
		InitializeChatUI();
		if (MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL).Length >= 2)
		{
			SetEvent(UI.BTN_REQUEST, "QUEST_SELECT_WINDOW", (int)MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId);
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
		callBack();
	}

	private IEnumerator DoInitializeCutScreen(Action<VorgonPreEventController> callBack)
	{
		VorgonPreEventController vorgonPreEventController = null;
		if (MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			QuestManager.VorgonQuetType vorgonQuestType = MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType();
			if (vorgonQuestType != 0)
			{
				GameObject gameObject = new GameObject("VorgonQuestController");
				vorgonPreEventController = gameObject.AddComponent<VorgonPreEventController>();
				vorgonPreEventController.enabled = false;
				gameObject.transform.parent = base._transform;
				if (vorgonQuestType == QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN)
				{
					string cutSceneDataPath = "ev001_CutScene";
					bool wait = true;
					cutScenePlayer = gameObject.AddComponent<CutScenePlayer>();
					cutScenePlayer.Init(cutSceneDataPath, delegate
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
		callBack(vorgonPreEventController);
	}

	private IEnumerator DoInitializeSceneLoad(Action callBack)
	{
		string text = MonoBehaviourSingleton<InGameManager>.IsValid() ? MonoBehaviourSingleton<InGameManager>.I.GetCurrentStageName() : "ST011D_01";
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetExternalStageName(text);
		}
		MonoBehaviourSingleton<StageManager>.I.LoadStage(text);
		while (MonoBehaviourSingleton<StageManager>.I.isLoading)
		{
			yield return null;
		}
		callBack();
	}

	private IEnumerator DoInitializeCharacter(Action callBack)
	{
		List<Character> load_check_list = new List<Character>();
		int j = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; j < count; j++)
		{
			Character character2 = MonoBehaviourSingleton<StageObjectManager>.I.characterList[j] as Character;
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
		callBack();
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
		UIExplorePlayerStatusList componentInChildren = GetComponentInChildren<UIExplorePlayerStatusList>(includeInactive: true);
		if ((bool)componentInChildren)
		{
			componentInChildren.gameObject.SetActive(value: false);
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
						portalUnlockEvent = MonoBehaviourSingleton<InGameManager>.I.gameObject.AddComponent<PortalUnlockEvent>();
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
		SetBadge(UI.BTN_REQUEST, num, SpriteAlignment.TopRight, -5, -5);
		SetBadge(UI.BTN_EVENT, completableEventDeliveryNum, SpriteAlignment.TopRight, -5, -5);
		if (LoungeMatchingManager.IsValidInLounge())
		{
			int num2 = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetMemberCount() - 1;
			SetBadge(UI.BTN_LOUNGE_MEMBER, num2, SpriteAlignment.TopRight, -5, -5);
		}
		if (UIInGameFieldMenu.IsValid() && !UIInGameFieldMenu.I.IsPopMenu())
		{
			SetBadge(UI.BTN_INGAME_MENU, completableDeliveryNum, SpriteAlignment.TopRight, -5, -5);
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
				SetBadge(UI.BTN_INGAME_MENU, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(), SpriteAlignment.TopRight, -5, -5);
			}
			else
			{
				SetBadge(UI.BTN_INGAME_MENU, 0, SpriteAlignment.TopRight, -5, -5);
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
			UnityEngine.Object.DestroyImmediate(inGameMembers.gameObject);
			inGameMembers = null;
		}
		if (inGameUIMembers != null)
		{
			UnityEngine.Object.DestroyImmediate(inGameUIMembers.gameObject);
			inGameUIMembers = null;
		}
		if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<DropTargetMarkerManeger>.I);
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
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<AttackColliderManager>.I);
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
					int num = deliveryTableDataList.Where((DeliveryTable.DeliveryData d) => d.IsEvent() && d.eventID == firstEvent.eventId).Count();
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
		List<EventData> list = new List<EventData>
		{
			new EventData("QUEST_WINDOW", null),
			new EventData("SELECT_NORMAL", null)
		};
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
			if (MonoBehaviourSingleton<DeliveryManager>.I.GetEventDeliveryList(fieldMapData.eventId).Count > 0)
			{
				foreach (Network.EventData @event in MonoBehaviourSingleton<QuestManager>.I.eventList)
				{
					if (@event.eventId == fieldMapData.eventId)
					{
						EventData[] autoEvents = new EventData[1]
						{
							new EventData("SELECT", @event)
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
		else if ((flags & NOTIFY_FLAG.RESET_DARK_MARKET) != (NOTIFY_FLAG)0L && (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds > 0)
		{
			GameSaveData.instance.canShowNoteDarkMarket = true;
			MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 37u), "");
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
		UIExplorePlayerStatusList componentInChildren = GetComponentInChildren<UIExplorePlayerStatusList>(includeInactive: true);
		if ((bool)componentInChildren)
		{
			componentInChildren.gameObject.SetActive(value: true);
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
				component.gameObject.SetActive(value: false);
			}
			else if (QuestManager.IsValidInGameSeriesArena())
			{
				component.gameObject.SetActive(value: false);
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
				component.gameObject.SetActive(value: false);
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
				SetBadge(UI.BTN_CHAT, MonoBehaviourSingleton<UIManager>.I.mainChat.GetPendingQueueNum(), SpriteAlignment.TopLeft, -5, -5);
			}
			if ((flag & MainChat.NOTIFY_FLAG.CLOSE_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).gameObject.SetActive(value: true);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).gameObject.SetActive(value: false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY) != 0)
			{
				GetCtrl(UI.BTN_CHAT).gameObject.SetActive(value: false);
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
		if (!ctrl)
		{
			return;
		}
		UIWidget component = ctrl.gameObject.GetComponent<UIWidget>();
		if (!component)
		{
			return;
		}
		UIStaticPanelChanger staticPanelChanger = ctrl.GetComponentInParent<UIStaticPanelChanger>();
		if (!staticPanelChanger)
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
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && (object)MonoBehaviourSingleton<StageObjectManager>.I.self != null && MonoBehaviourSingleton<UIPlayerStatus>.I.IsEnableWeaponChangeButton() && MonoBehaviourSingleton<StageObjectManager>.I.self.ExecEvolve())
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
