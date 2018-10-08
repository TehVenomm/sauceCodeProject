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
				DispatchEvent("RETIRE", null);
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
				DispatchEvent("RETURN", null);
			}
		}
	}

	public override void Initialize()
	{
		if (MonoBehaviourSingleton<ShopManager>.IsValid())
		{
			MonoBehaviourSingleton<ShopManager>.I.trackPlayerDie = false;
		}
		StartCoroutine(DoInitialize());
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
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_MAIN, true);
		while (MonoBehaviourSingleton<DataTableManager>.I.IsLoading())
		{
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isRetry)
		{
			bool isWaitQuestLoad = true;
			if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				CoopApp.EnterArenaQuestOffline(delegate(bool isMatching, bool isConnect, bool isRegist, bool isStart)
				{
					((_003CDoInitialize_003Ec__IteratorC3)/*Error near IL_00d3: stateMachine*/)._003CisWaitQuestLoad_003E__0 = !isStart;
				});
			}
			else if (MonoBehaviourSingleton<StatusManager>.IsValid() && MonoBehaviourSingleton<StatusManager>.I.assignedCharaInfo != null && MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData != null)
			{
				CoopApp.EnterQuestOfflineAssignedEquipment(null, null, delegate(bool isMatching, bool isConnect, bool isRegist, bool isStart)
				{
					((_003CDoInitialize_003Ec__IteratorC3)/*Error near IL_0113: stateMachine*/)._003CisWaitQuestLoad_003E__0 = !isStart;
				});
			}
			while (isWaitQuestLoad)
			{
				yield return (object)null;
			}
			if (MonoBehaviourSingleton<QuestManager>.IsValid())
			{
				int bgm_id = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID();
				SoundManager.RequestBGM(bgm_id, true);
			}
			MonoBehaviourSingleton<InGameManager>.I.isRetry = false;
		}
		SetActive(UI.BTN_QUEST_MENU, QuestManager.IsValidInGame());
		InitializeChatUI();
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
			yield return (object)null;
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
		}, false);
		LoadObject lo_tutorial_prefab = null;
		if (!TutorialStep.HasFirstDeliveryCompleted() && TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02))
		{
			lo_tutorial_prefab = load_queue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialFieldHelper", false);
		}
		LoadObject lo_link_resources;
		if (FieldManager.IsValidInTutorial())
		{
			lo_link_resources = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", new string[3]
			{
				"InGameLinkResourcesCommon",
				"InGameLinkResourcesQuest",
				"InGameLinkResourcesField"
			}, false);
		}
		else
		{
			List<string> linkResources = new List<string>(3)
			{
				"InGameLinkResourcesCommon"
			};
			if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest())
			{
				linkResources.Add("InGameLinkResourcesQuest");
			}
			if (FieldManager.IsValidInGameNoBoss() || InGameManager.IsValidInGameTest())
			{
				linkResources.Add("InGameLinkResourcesField");
			}
			lo_link_resources = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", linkResources.ToArray(), false);
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
				vorgonPreEventController.enabled = false;
				questControllerObj.transform.parent = _transform;
				if (vorgonQuestType == QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN)
				{
					string path = "ev001_CutScene";
					bool wait = true;
					cutScenePlayer = questControllerObj.AddComponent<CutScenePlayer>();
					cutScenePlayer.Init(path, delegate
					{
						((_003CDoInitialize_003Ec__IteratorC3)/*Error near IL_04e0: stateMachine*/)._003Cwait_003E__12 = false;
					});
					while (wait)
					{
						yield return (object)null;
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
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true))
		{
			lo_ingame_rush = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameRush", false);
		}
		LoadObject loAdditionalDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameAdditionalDamageNum", false);
		LoadObject loPlayerDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGamePlayerDamageNum", false);
		LoadObject loDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameDamageNum", false);
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(20);
		}
		Transform _setting_transform = null;
		InGameSettingsManager.Player player_setting = null;
		ResourceObject[] loadedObjects = lo_prefabs.loadedObjects;
		foreach (ResourceObject prefab in loadedObjects)
		{
			string name = prefab.obj.name;
			if (name.Contains("PlayerAttackInfo"))
			{
				if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					if ((UnityEngine.Object)_setting_transform == (UnityEngine.Object)null)
					{
						_setting_transform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
					}
					if (player_setting == null)
					{
						player_setting = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
						player_setting.attackInfosAll = new AttackInfo[0];
					}
					GameObject obj = ResourceUtility.Realizes(prefab.obj, _setting_transform, -1).gameObject;
					AttackInfos attack_infos = obj.GetComponent<AttackInfos>();
					player_setting.weaponAttackInfoList.Add(attack_infos);
					player_setting.attackInfosAll = Utility.CreateMergedArray(player_setting.attackInfosAll, attack_infos.attackHitInfos);
				}
			}
			else if (name.Contains("PuniConManager"))
			{
				ResourceUtility.Realizes(prefab.obj, inGameUIMembers, 5);
				MonoBehaviourSingleton<PuniConManager>.I.enableMultiTouch = true;
			}
			else
			{
				ResourceUtility.Realizes(prefab.obj, _transform, -1);
			}
		}
		ResourceObject[] loadedObjects2 = lo_link_resources.loadedObjects;
		foreach (ResourceObject prefab2 in loadedObjects2)
		{
			ResourceUtility.Realizes(prefab2.obj, MonoBehaviourSingleton<InGameSettingsManager>.I._transform, -1);
		}
		Utility.CreateGameObjectAndComponent("StageObjectManager", _transform, -1);
		Utility.CreateGameObjectAndComponent("TargetMarkerManager", _transform, -1);
		Utility.CreateGameObjectAndComponent("InGameProgress", _transform, -1);
		Utility.CreateGameObjectAndComponent("AIManager", _transform, -1);
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.gameObject.SetActive(true);
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
			{
				MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open(UITransition.TYPE.OPEN);
			}
		}
		if (lo_tutorial_prefab != null)
		{
			Transform t = ResourceUtility.Realizes(lo_tutorial_prefab.loadedObject, inGameUIMembers, -1);
			if ((UnityEngine.Object)t != (UnityEngine.Object)null)
			{
				UITutorialFieldHelper h = t.GetComponent<UITutorialFieldHelper>();
				if ((UnityEngine.Object)h != (UnityEngine.Object)null)
				{
					h.Setup(this);
				}
			}
		}
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true))
		{
			GameObject inGameRushUI = UnityEngine.Object.Instantiate(lo_ingame_rush.loadedObject) as GameObject;
			inGameRushUI.transform.parent = MonoBehaviourSingleton<UIContinueButton>.I._transform.parent;
			inGameRushUI.transform.localPosition = Vector3.zero;
			inGameRushUI.transform.localRotation = Quaternion.identity;
			inGameRushUI.transform.localScale = Vector3.one;
			UIStaticPanelChanger panelChanger = inGameRushUI.GetComponentInParent<UIStaticPanelChanger>();
			MonoBehaviourSingleton<UISpectatorButton>.I.Initialize(panelChanger);
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
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<StageManager>.I.isValidInside)
		{
			goto IL_0abe;
		}
		goto IL_0abe;
		IL_0abe:
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(40);
		}
		InGameSettingsManager inGameSettings = MonoBehaviourSingleton<InGameSettingsManager>.I;
		GlobalSettingsManager.LinkResources globalResources = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources;
		List<string> loadEffectNames = new List<string>();
		for (int k = 0; k < 3; k++)
		{
			InGameSettingsManager.UseResources use_res = null;
			switch (k)
			{
			case 0:
				use_res = globalResources.inGameCommonResources;
				break;
			case 1:
				if (FieldManager.IsValidInGameNoQuest() || FieldManager.IsValidInGameNoBoss())
				{
					use_res = inGameSettings.useResourcesField;
				}
				break;
			case 2:
				if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest() || FieldManager.IsValidInTutorial())
				{
					use_res = globalResources.inGameQuestResources;
				}
				break;
			}
			if (use_res != null)
			{
				string[] effects = use_res.effects;
				foreach (string effect_name in effects)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name);
					loadEffectNames.Add(effect_name);
				}
				string[] uiEffects = use_res.uiEffects;
				foreach (string effect_name2 in uiEffects)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, effect_name2);
				}
			}
		}
		InGameSettingsManager.Player player = inGameSettings.player;
		string[] stunnedEffectList = globalResources.stunnedEffectList;
		foreach (string effect_name3 in stunnedEffectList)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name3);
			loadEffectNames.Add(effect_name3);
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
			load_queue.CacheSE(se_id, null);
		}
		if (FieldManager.IsValidInGameNoBoss())
		{
			string[] effectNames = inGameSettings.portal.effectNames;
			foreach (string effect_name4 in effectNames)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name4);
				loadEffectNames.Add(effect_name4);
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
			load_queue.CacheAnimDataUseResource(MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop.animEventData, null, null);
		}
		else
		{
			InGameSettingsManager.ShadowSealingParam ssp = inGameSettings.debuff.shadowSealingParam;
			if (ssp.startSeId != 0)
			{
				load_queue.CacheSE(ssp.startSeId, null);
			}
			if (ssp.loopSeId != 0)
			{
				load_queue.CacheSE(ssp.loopSeId, null);
			}
			if (ssp.endSeId != 0)
			{
				load_queue.CacheSE(ssp.endSeId, null);
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
		MonoBehaviourSingleton<InGameProgress>.I.CacheUseResources(load_queue, ref loadEffectNames);
		MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(loadEffectNames);
		if (QuestManager.IsValidInGameWaveMatch(false))
		{
			InGameSettingsManager.WaveMatchParam wmParam = inGameSettings.GetWaveMatchParam();
			load_queue.CacheSE(wmParam.waveJingleId, null);
			load_queue.CacheSE(wmParam.targetHitSeId, null);
			load_queue.CacheSE(wmParam.targetBreakSeId, null);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, wmParam.targetChangeAnimEffect);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, wmParam.targetHitEffect);
		}
		CacheAudio(load_queue);
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(70);
		}
		yield return (object)StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPop());
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
		int j = 0;
		for (int len2 = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; j < len2; j++)
		{
			Character character = MonoBehaviourSingleton<StageObjectManager>.I.characterList[j] as Character;
			if (character.IsCoopNone() && character.isLoading && (UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.FindNonPlayer(character.id) == (UnityEngine.Object)null)
			{
				load_check_list.Add(character);
			}
		}
		int i = 0;
		for (int len = load_check_list.Count; i < len; i++)
		{
			Character character2 = load_check_list[i];
			while (character2.isLoading)
			{
				yield return (object)null;
			}
		}
		if ((UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (UnityEngine.Object)null)
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
			yield return (object)MonoBehaviourSingleton<AppMain>.I.ClearMemory(false, false);
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			float limit_time = MonoBehaviourSingleton<InGameManager>.I.GetCurrentLimitTime();
			MonoBehaviourSingleton<InGameProgress>.I.SetLimitTime(limit_time);
			if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
			{
				MonoBehaviourSingleton<InGameProgress>.I.SetRushRemainTime((int)limit_time);
			}
			if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
			{
				MonoBehaviourSingleton<InGameProgress>.I.SetArenaRemainTime(Mathf.FloorToInt(limit_time));
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
				MonoBehaviourSingleton<InGameProgress>.I.StartTimer(0f);
			}
			if (FieldManager.IsValidInGameNoQuest())
			{
				MonoBehaviourSingleton<InGameProgress>.I.SetAfkLimitTime();
			}
		}
		MonoBehaviourSingleton<InGameManager>.I.CheckStageInitialState();
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			yield return (object)StartCoroutine(MonoBehaviourSingleton<CoopManager>.I.coopStage.DoActivate());
		}
		MonoBehaviourSingleton<StageObjectManager>.I.objectList.ForEach(delegate(StageObject o)
		{
			if ((UnityEngine.Object)o.controller != (UnityEngine.Object)null)
			{
				o.controller.SetEnableControll(true, ControllerBase.DISABLE_FLAG.BATTLE_START);
			}
		});
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if ((UnityEngine.Object)self != (UnityEngine.Object)null)
		{
			self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.INITIALIZE;
			if ((UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.boss != (UnityEngine.Object)null)
			{
				self.SetActionTarget(MonoBehaviourSingleton<StageObjectManager>.I.boss, true);
			}
		}
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			prevLevel = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
			if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.levelUp != (UnityEngine.Object)null)
			{
				MonoBehaviourSingleton<UIManager>.I.levelUp.GetNowStatus();
			}
		}
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			if ((UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (UnityEngine.Object)null)
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
		yield return (object)0;
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_MAIN, false);
		if ((UnityEngine.Object)vorgonPreEventController != (UnityEngine.Object)null)
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
		if ((UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.ResetShadowSealingUI();
		}
		if (UIInGameFieldMenu.IsValid() && !UIInGameFieldMenu.I.IsPopMenu())
		{
			FieldMapTable.FieldMapTableData field = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			if (field != null)
			{
				if (!FieldManager.HasWorldMap(field.mapID))
				{
					UIInGameFieldMenu.I.SetDisableMapButton(true);
				}
			}
			else
			{
				UIInGameFieldMenu.I.SetDisableMapButton(true);
			}
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
			{
				UIInGameFieldMenu.I.SetDisableEventButton(true);
			}
		}
		bool waitGetAutoTime = true;
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.autoBattleButton.GetAutoPlayTime(delegate
			{
				((_003CDoInitialize_003Ec__IteratorC3)/*Error near IL_18ec: stateMachine*/)._003CwaitGetAutoTime_003E__70 = false;
			});
		}
		while (waitGetAutoTime)
		{
			yield return (object)null;
		}
		while (!MonoBehaviourSingleton<UIManager>.I.canHideGGTutorialMessage(5f))
		{
			yield return (object)null;
		}
		MonoBehaviourSingleton<UIManager>.I.HideGGTutorialMessage();
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep == 3)
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_mission_start, "Tutorial");
		}
		if (MonoBehaviourSingleton<UIQuestRepeat>.IsValid())
		{
			MonoBehaviourSingleton<UIQuestRepeat>.I.InitData();
		}
		base.Initialize();
	}

	public void SetMapButtonState()
	{
		if (UIInGameFieldMenu.IsValid())
		{
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			if (fieldMapData != null)
			{
				if (!FieldManager.HasWorldMap(fieldMapData.mapID))
				{
					UIInGameFieldMenu.I.SetDisableMapButton(true);
				}
			}
			else
			{
				UIInGameFieldMenu.I.SetDisableMapButton(true);
			}
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
			{
				UIInGameFieldMenu.I.SetDisableEventButton(true);
			}
			else
			{
				UIInGameFieldMenu.I.SetDisableEventButton(false);
			}
		}
	}

	protected override void OnOpen()
	{
		UIExplorePlayerStatusList componentInChildren = GetComponentInChildren<UIExplorePlayerStatusList>(true);
		if ((bool)componentInChildren)
		{
			componentInChildren.gameObject.SetActive(false);
		}
		OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		base.OnOpen();
	}

	public override void StartSection()
	{
		if (UITutorialFieldHelper.IsValid())
		{
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
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
					if ((UnityEngine.Object)portalUnlockEvent == (UnityEngine.Object)null)
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
		if ((UnityEngine.Object)portalUnlockEvent == (UnityEngine.Object)null)
		{
			SetCoopStageReady();
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
		SetBadge(UI.BTN_REQUEST, num, SpriteAlignment.TopRight, -5, -5, false);
		SetBadge(UI.BTN_EVENT, completableEventDeliveryNum, SpriteAlignment.TopRight, -5, -5, false);
		if (LoungeMatchingManager.IsValidInLounge())
		{
			int num2 = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetMemberCount() - 1;
			SetBadge(UI.BTN_LOUNGE_MEMBER, num2, SpriteAlignment.TopRight, -5, -5, false);
		}
		if (UIInGameFieldMenu.IsValid() && !UIInGameFieldMenu.I.IsPopMenu())
		{
			SetBadge(UI.BTN_INGAME_MENU, completableDeliveryNum, SpriteAlignment.TopRight, -5, -5, false);
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
				SetBadge(UI.BTN_INGAME_MENU, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(), SpriteAlignment.TopRight, -5, -5, false);
			}
			else
			{
				SetBadge(UI.BTN_INGAME_MENU, 0, SpriteAlignment.TopRight, -5, -5, false);
			}
		}
	}

	private void TutorialStep4_1()
	{
		if (is_collect_tutorial)
		{
			execTutorial4_1 = true;
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("InGameScene", "TutorialStep4_1_1", null);
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (UnityEngine.Object)null)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.TUTORIAL;
			}
		}
	}

	private void TutorialStep4_2()
	{
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
		if (is_collect_tutorial)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("InGameScene", "TutorialStep4_1_2", null);
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
		if ((UnityEngine.Object)inGameMembers != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<InGameManager>.I.SetIntervalTransferSelf();
			UnityEngine.Object.DestroyImmediate(inGameMembers.gameObject);
			inGameMembers = null;
		}
		if ((UnityEngine.Object)inGameUIMembers != (UnityEngine.Object)null)
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
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.RemoveObserver(this);
		}
		if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<AttackColliderManager>.I);
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close(UITransition.TYPE.CLOSE);
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
					List<DeliveryTable.DeliveryData> deliveryTableDataList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryTableDataList(false);
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
						GameSection.ChangeStayEvent("MINI_BINGO", null);
					}
				}
				else
				{
					GameSection.ChangeStayEvent("MINI_BINGO", null);
				}
			}
			GameSection.ResumeEvent(true, null);
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
			GameSection.ResumeEvent(isSuccess, null);
		}, false);
	}

	private void OnQuery_CONTINUE()
	{
		if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsTutorialCurrentQuest())
		{
			GameSection.SetEventData(StringTable.Get(STRING_CATEGORY.IN_GAME, 1007u));
		}
		else
		{
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
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_guide_movement, "Tutorial");
			MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(MonoBehaviourSingleton<InGameProgress>.I.checkPortalObject.portalID);
			MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
			GameSceneEvent.Cancel();
			MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "CharaMake", UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
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
			}, false);
		});
	}

	private void OnQuery_QUEST_WINDOW()
	{
		CloseChatWindow();
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
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		int completableEventDeliveryNum = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(fieldMapData.eventId);
		if (completableEventDeliveryNum > 0)
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

	private void OnQuery_RALLY_INVITE()
	{
		MonoBehaviourSingleton<UIPlayerStatus>.I.SetDisableRalltBtn(true);
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendRally(delegate
		{
			UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 35u), false, 3f);
		});
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((NOTIFY_FLAG.UPDATE_USER_STATUS & flags) != (NOTIFY_FLAG)0L && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level != prevLevel)
		{
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (UnityEngine.Object)null)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.OnSetPlayerStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp, true, null);
			}
			if (MonoBehaviourSingleton<UIManager>.IsValid())
			{
				MonoBehaviourSingleton<UIManager>.I.levelUp.PlayLevelUp();
			}
			prevLevel = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
			{
				UIInGameFieldMenu.I.SetDisableEventButton(true);
			}
			else
			{
				UIInGameFieldMenu.I.SetDisableEventButton(false);
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
			if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != (UnityEngine.Object)null && TutorialStep.HasAllTutorialCompleted())
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "InGameMain")
				{
					MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open(UITransition.TYPE.OPEN);
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close(UITransition.TYPE.CLOSE);
				}
			}
		}
		else if ((flags & NOTIFY_FLAG.UPDATE_RALLY_INVITE) != (NOTIFY_FLAG)0L)
		{
			if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.invitationInGameButton != (UnityEngine.Object)null && TutorialStep.HasAllTutorialCompleted())
			{
				if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "InGameMain")
				{
					MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open(UITransition.TYPE.OPEN);
				}
				else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen)
				{
					MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close(UITransition.TYPE.CLOSE);
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
			DispatchEvent("LOUNGE_KICKED", null);
		}
		else
		{
			if ((flags & NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE) != (NOTIFY_FLAG)0L && MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
			{
				RefreshUI();
			}
			if ((flags & NOTIFY_FLAG.TRANSITION_END) != (NOTIFY_FLAG)0L)
			{
				if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss != (UnityEngine.Object)null)
				{
					MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.ClearAnnounce();
					if (MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.IsKnockDownRaidBossByRaidBossHp())
					{
						MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.PlayKnockDown(false, null);
					}
				}
				if (MonoBehaviourSingleton<FieldManager>.IsValid())
				{
					MonoBehaviourSingleton<FieldManager>.I.MatchingNotice();
				}
			}
			base.OnNotify(flags);
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_QUEST_CLEAR_STATUS | base.GetUpdateUINotifyFlags();
	}

	private void InitializeExplorePlayerStatuses()
	{
		UIExplorePlayerStatusList componentInChildren = GetComponentInChildren<UIExplorePlayerStatusList>(true);
		if ((bool)componentInChildren)
		{
			componentInChildren.gameObject.SetActive(true);
			ExploreStatus exploreStatus = MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus();
			componentInChildren.Initialize(exploreStatus);
		}
	}

	private void InitializeChatUI()
	{
		MonoBehaviourSingleton<ChatManager>.I.CreateRoomChatWithCoopIfNeeded();
		MonoBehaviourSingleton<ChatManager>.I.roomChat.JoinRoom(0);
		UIButton component = GetCtrl(UI.BTN_CHAT).GetComponent<UIButton>();
		if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
		{
			if (!TutorialStep.HasAllTutorialCompleted())
			{
				component.gameObject.SetActive(false);
			}
			else if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
			{
				MonoBehaviourSingleton<UIManager>.I.mainChat.addObserver(this);
				component.onClick.Clear();
				component.onClick.Add(new EventDelegate(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly));
				MonoBehaviourSingleton<UIManager>.I.mainChat.SetRoomChatNameType(!QuestManager.IsValidInGame());
			}
			else
			{
				component.gameObject.SetActive(false);
			}
		}
	}

	private void CloseChatWindow()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
	}

	public override void OnModifyChat(MainChat.NOTIFY_FLAG flag)
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
		{
			if ((flag & MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE) != 0)
			{
				SpriteAlignment align = LoungeMatchingManager.IsValidInLounge() ? SpriteAlignment.TopLeft : SpriteAlignment.TopRight;
				SetBadge(UI.BTN_CHAT, MonoBehaviourSingleton<UIManager>.I.mainChat.GetPendingQueueNumWithoutRoom(), align, -5, -5, false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.CLOSE_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).gameObject.SetActive(true);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).gameObject.SetActive(false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY) != 0)
			{
				GetCtrl(UI.BTN_CHAT).gameObject.SetActive(false);
			}
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		float num = (!is_portrait) ? 10f : 323f;
		UIWidget component = GetCtrl(UI.WGT_CHAT_PARENT).GetComponent<UIWidget>();
		if ((bool)component)
		{
			UIStaticPanelChanger staticPanelChanger = component.GetComponentInParent<UIStaticPanelChanger>();
			if ((bool)staticPanelChanger)
			{
				staticPanelChanger.UnLock();
				component.topAnchor.Set(0f, num + (float)component.height);
				component.bottomAnchor.Set(0f, num);
				component.UpdateAnchors();
				AppMain i = MonoBehaviourSingleton<AppMain>.I;
				i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
				{
					staticPanelChanger.Lock();
				});
			}
		}
	}

	private void OnQuery_EVOLVE()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && !object.ReferenceEquals(MonoBehaviourSingleton<StageObjectManager>.I.self, null) && MonoBehaviourSingleton<StageObjectManager>.I.self.ExecEvolve())
		{
			if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.RestrictPopMenu(true);
			}
			if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnduranceStatus>.I.RestrictPopMenu(true);
			}
		}
	}

	private void OnQuery_EVOLVE_TOUCH()
	{
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.RestrictPopMenu(false);
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.RestrictPopMenu(false);
		}
	}
}
