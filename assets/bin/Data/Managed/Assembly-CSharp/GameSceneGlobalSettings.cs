using Network;
using System;
using UnityEngine;

public class GameSceneGlobalSettings
{
	public string externalStageName;

	private string stageName;

	private bool stageForceLoad;

	private int stageImageID;

	private Animation cameraAnim;

	private AnimationClip cameraAnimClip;

	private bool needHomeManager;

	private bool needLoungeManager;

	private bool needClanManager;

	private bool needGuildManager;

	private bool needStatusStageManager;

	private float saveCameraNear = float.MinValue;

	private float saveCameraFar = float.MinValue;

	private int mainCameraCullingMask;

	private const string INGAME_PRESET = "InGame";

	public bool isInitialized
	{
		get
		{
			if (MonoBehaviourSingleton<StageManager>.IsValid() && MonoBehaviourSingleton<StageManager>.I.isLoading)
			{
				return false;
			}
			IHomeManager currentIHomeManager = GetCurrentIHomeManager();
			if (currentIHomeManager != null)
			{
				if (!currentIHomeManager.IsInitialized)
				{
					return false;
				}
				if (currentIHomeManager.HomeCamera.isChanging)
				{
					return false;
				}
			}
			if (MonoBehaviourSingleton<UIManager>.I.isLoading)
			{
				return false;
			}
			if (MonoBehaviourSingleton<GameSceneManager>.I.isOpenCommonDialog)
			{
				return false;
			}
			if (MonoBehaviourSingleton<StatusStageManager>.IsValid() && MonoBehaviourSingleton<StatusStageManager>.I.isBusy)
			{
				return false;
			}
			return true;
		}
	}

	public static bool forceIgnoreMainUI
	{
		get;
		set;
	}

	public GameSceneGlobalSettings()
	{
		mainCameraCullingMask = GetDefaultMainCameraCullingMask();
	}

	public void ChangeSection(GameSceneTables.SceneData scene_data, GameSceneTables.SectionData section_data)
	{
		if (MonoBehaviourSingleton<UIManager>.I.npcMessage != null)
		{
			MonoBehaviourSingleton<UIManager>.I.npcMessage.UpdateMessage(section_data, is_open: false);
		}
	}

	public TransitionManager.TYPE GetTransitionType(string prev_scene_name, string prev_section_name, string next_scene_name, string next_section_name)
	{
		if (prev_scene_name == null)
		{
			prev_scene_name = string.Empty;
		}
		if (prev_section_name == null)
		{
			prev_section_name = string.Empty;
		}
		if (next_scene_name == null)
		{
			next_scene_name = string.Empty;
		}
		if (next_section_name == null)
		{
			next_section_name = string.Empty;
		}
		if ((prev_section_name == "InGameMain" || prev_section_name == "QuestResultTop" || prev_section_name == "QuestResultDirection" || prev_section_name == "CarnivalResultPoint") && (next_section_name == "QuestResultDirection" || next_section_name == "QuestResultFriend" || next_section_name == "QuestResultTrialEnd"))
		{
			return TransitionManager.TYPE.WHITE;
		}
		if (prev_section_name == "InGameFieldQuestConfirm" || next_section_name == "InGameFieldQuestConfirm")
		{
			return TransitionManager.TYPE.NONE;
		}
		if (prev_scene_name != "InGameScene" && next_scene_name == "InGameScene")
		{
			return TransitionManager.TYPE.LOADING;
		}
		if ((prev_section_name == "WorldMapOpenNewField" || prev_section_name == "StoryMain") && (next_section_name == "InGameMain" || next_section_name == "InGameInterval"))
		{
			return TransitionManager.TYPE.BLACK;
		}
		if (next_section_name == "InGameMain" || next_section_name == "InGameInterval")
		{
			return TransitionManager.TYPE.LOADING;
		}
		if (prev_scene_name == "TitleScene" && next_scene_name == "HomeScene")
		{
			return TransitionManager.TYPE.LOADING;
		}
		if (prev_scene_name == "TitleScene" && next_scene_name == "LoungeScene")
		{
			return TransitionManager.TYPE.LOADING;
		}
		if (prev_scene_name == "TitleScene" && next_scene_name == "ClanScene")
		{
			return TransitionManager.TYPE.LOADING;
		}
		bool flag = next_scene_name == "HomeScene" || next_scene_name == "LoungeScene" || next_scene_name == "ClanScene";
		if ((prev_scene_name == "InGameScene" && prev_section_name == "InGameQuestAcceptInvitation") & flag)
		{
			return TransitionManager.TYPE.LOADING;
		}
		if (prev_scene_name != next_scene_name)
		{
			if (prev_scene_name == "InGameScene")
			{
				return TransitionManager.TYPE.BLACK;
			}
			if (next_scene_name == "ShopScene" || next_scene_name == "GachaScene")
			{
				return TransitionManager.TYPE.BLACK;
			}
		}
		if (next_section_name == "GachaPerformanceSkill" || next_section_name == "GachaPerformanceQuest")
		{
			return TransitionManager.TYPE.BLACK;
		}
		if (next_section_name == "SmithCreatePerformance" || next_section_name == "SmithGrowPerformance" || next_section_name == "SmithEvolvePerformance" || next_section_name == "SmithAbilityChangePerformance" || next_section_name == "SmithGrowSkillPerformance" || next_section_name == "SmithAbilityItemPerformance" || next_section_name == "SmithShadowEvolvePerformance" || next_section_name == "SmithExceedPerformance")
		{
			return TransitionManager.TYPE.BLACK;
		}
		if (prev_scene_name == "StoryScene" || next_scene_name == "StoryScene")
		{
			return TransitionManager.TYPE.BLACK;
		}
		if ((next_scene_name == "FriendScene" && prev_scene_name != "FriendScene") || (next_scene_name != "FriendScene" && prev_scene_name == "FriendScene"))
		{
			return TransitionManager.TYPE.BLACK;
		}
		if ((next_scene_name == "ProfileScene" && prev_scene_name != "ProfileScene") || (next_scene_name != "ProfileScene" && prev_scene_name == "ProfileScene"))
		{
			return TransitionManager.TYPE.BLACK;
		}
		if ((prev_scene_name == "TitleScene" && next_scene_name == "StatusScene") || (prev_scene_name == "StatusScene" && next_scene_name == "TitleScene"))
		{
			return TransitionManager.TYPE.BLACK;
		}
		if (MonoBehaviourSingleton<StageManager>.IsValid() && (MonoBehaviourSingleton<StageManager>.I.currentStageName == stageName || MonoBehaviourSingleton<StageManager>.I.backgroundImageID == stageImageID))
		{
			return TransitionManager.TYPE.NONE;
		}
		return TransitionManager.TYPE.BLACK;
	}

	public bool IsTransitionEnd(string prev_scene_name, string prev_section_name, string next_scene_name, string next_section_name)
	{
		if (next_section_name.StartsWith("InGameInterval"))
		{
			return false;
		}
		return true;
	}

	public bool SceneClear(string prev_scene_name, string prev_section_name, string next_scene_name)
	{
		bool flag = true;
		if (prev_scene_name == "TitleScene" && next_scene_name == "InGameScene")
		{
			flag = false;
		}
		if (next_scene_name == "InGameScene")
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ClearHistory();
			MonoBehaviourSingleton<UIManager>.I.DeleteUI();
		}
		else
		{
			switch (next_scene_name)
			{
			case "HomeScene":
			case "LoungeScene":
			case "ClanScene":
			case "SmithScene":
			case "StatusScene":
			case "UniqueStatusScene":
			case "ShopScene":
			case "GachaScene":
			case "FriendScene":
			case "ProfileScene":
				MonoBehaviourSingleton<GameSceneManager>.I.ClearHistory();
				break;
			}
			if ((next_scene_name == "StatusScene" && prev_scene_name == "SmithScene") || (next_scene_name == "SmithScene" && prev_scene_name == "StatusScene"))
			{
				flag = false;
			}
			if ((next_scene_name == "UniqueStatusScene" && prev_scene_name == "SmithScene") || (next_scene_name == "SmithScene" && prev_scene_name == "UniqueStatusScene"))
			{
				flag = false;
			}
			if (MonoBehaviourSingleton<UIManager>.I.mainMenu != null)
			{
				MonoBehaviourSingleton<UIManager>.I.mainMenu.UpdateSceneButtons(next_scene_name);
			}
		}
		if (flag)
		{
			return AppMain.needClearMemory;
		}
		return false;
	}

	public void SceneInitialize(string prev_scene_name, string next_scene_name, bool skipChat = false)
	{
		if (next_scene_name != "InGameScene")
		{
			bool need_tutorial = UserInfoManager.IsNeedsTutorialMessage();
			MonoBehaviourSingleton<UIManager>.I.LoadUI(need_common: true, need_outgame: true, need_tutorial, skipChat);
		}
		if (next_scene_name != "GachaScene" && next_scene_name != "ShopScene" && MonoBehaviourSingleton<GachaManager>.IsValid())
		{
			MonoBehaviourSingleton<GachaManager>.I.ResetGachaType();
		}
		ResetAudioRestener();
		UpdateBGM(next_scene_name, null);
	}

	public void StageSetup(string prev_scene_name, string scene_name, string section_name, GameSceneTables.SectionData section_data)
	{
		stageForceLoad = false;
		if (!string.IsNullOrEmpty(externalStageName))
		{
			stageName = externalStageName;
			stageImageID = -1;
		}
		else
		{
			if (section_data != null && section_data.type.IsDialog())
			{
				return;
			}
			string text = stageName;
			stageName = null;
			stageImageID = -1;
			needHomeManager = false;
			needLoungeManager = false;
			needClanManager = false;
			needGuildManager = false;
			needStatusStageManager = false;
			if (section_name == "InGameMain" || section_name == "MenuReset")
			{
				return;
			}
			if (scene_name == "HomeScene")
			{
				HomeThemeTable.HomeThemeData homeThemeData = Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow());
				stageName = homeThemeData.sceneName;
				if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("HomeTop") && prev_scene_name == "HomeScene")
				{
					stageName = text;
				}
				if (string.IsNullOrEmpty(stageName))
				{
					stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.mainStage;
				}
				if (stageName == homeThemeData.sceneName)
				{
					Singleton<HomeThemeTable>.I.SetCurrentHomeThemeName(homeThemeData.name);
				}
				needHomeManager = true;
			}
			if (stageName == null && scene_name == "LoungeScene")
			{
				stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.mainStage;
				needLoungeManager = true;
			}
			if (stageName == null && scene_name == "ClanScene")
			{
				if (prev_scene_name == "ClanScene")
				{
					stageName = text;
				}
				else
				{
					int level = MonoBehaviourSingleton<UserInfoManager>.I.userClan.level;
					stageName = ClanLvUnlockManager.CallGetLoadStageName(level);
				}
				needClanManager = true;
			}
			if (stageName == null && (scene_name == "StatusScene" || scene_name == "ProfileScene"))
			{
				stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage;
				if (scene_name == "StatusScene")
				{
					needStatusStageManager = true;
				}
			}
			if (stageName == null && scene_name == "UniqueStatusScene")
			{
				stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createUniqueStage;
				needStatusStageManager = true;
			}
			if (stageName == null && scene_name == "SmithScene")
			{
				if (section_name == "SmithGrowSkillSelect" || section_name == "SmithGrowSkillPerformance" || section_name == "SmithGrowSkillSecond")
				{
					stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillStage;
				}
				else if (section_name == "SmithCreateTypeSelect" || section_name == "SmithGrowItemSelect" || section_name.EndsWith("Performance"))
				{
					if (StatusManager.IsUnique())
					{
						stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createUniqueStage;
					}
					else
					{
						stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage;
					}
				}
				else if (prev_scene_name == "HomeScene" || prev_scene_name == "LoungeScene" || prev_scene_name == "ClanScene")
				{
					stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage;
				}
				else
				{
					stageName = text;
				}
				if (stageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage || stageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createUniqueStage)
				{
					needStatusStageManager = true;
				}
			}
			if (stageName == null && scene_name == "GatherScene")
			{
				stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gatherScene.mainStage;
			}
			if (stageName == null && scene_name == "ForestScene")
			{
				stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gatherScene.mainStage;
			}
			if (stageName == null && (scene_name == "ShopScene" || scene_name == "GachaScene"))
			{
				if (section_name == "ShopTop")
				{
					stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.mainStage;
				}
				else if (section_name == "GachaPerformanceSkill")
				{
					stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gachaScene.SkillGachaStage;
				}
				else if (section_name == "GachaPerformanceQuest")
				{
					if (MonoBehaviourSingleton<GachaManager>.IsValid() && MonoBehaviourSingleton<GachaManager>.I.IsReam())
					{
						if (MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector)
						{
							stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gachaScene.QuestFeverGachaStage;
							stageForceLoad = true;
						}
						else
						{
							stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gachaScene.QuestReamGachaStage;
						}
					}
					else
					{
						stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gachaScene.QuestSingleGachaStage;
					}
				}
				else
				{
					stageName = text;
				}
			}
			if (stageName == null && scene_name == "TitleScene")
			{
				if (section_name == "CharaMake")
				{
					if (ExistHistorySection("StatusTop") || ExistHistorySection("ProfileTop"))
					{
						stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.stage;
					}
					else
					{
						stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.stage;
					}
				}
				else
				{
					stageName = string.Empty;
				}
			}
			if (scene_name == "EnemyDownloadScene")
			{
				stageName = string.Empty;
			}
			if (stageName == null && stageImageID == -1)
			{
				if (scene_name == "StatusScene" || scene_name == "ItemStorageScene")
				{
					stageImageID = 10000000;
				}
				else if (scene_name.Contains("WeaponSelect"))
				{
					stageImageID = 99999999;
				}
				else
				{
					stageImageID = 10000001;
				}
			}
			if ((prev_scene_name == "ShopScene" && scene_name == "GachaScene") || (prev_scene_name == "GachaScene" && scene_name == "ShopScene"))
			{
				stageForceLoad = true;
			}
		}
	}

	public void SectionInitialize(string scene_name, string section_name, GameSceneTables.SectionData section_data)
	{
		InitGlobal(scene_name, section_name, section_data);
		InitGlobalUI(scene_name, section_name);
		InitGlobalStage(scene_name, section_name, section_data);
		InitOrientation(scene_name, section_name);
		UpdateBGM(scene_name, section_name);
		if (MonoBehaviourSingleton<SmithManager>.IsValid())
		{
			MonoBehaviourSingleton<SmithManager>.I.CheckSmithSectionBlur(scene_name, section_name, section_data);
		}
	}

	public void SectionSetup(string scene_name, string section_name, GameSceneTables.SectionData section_data)
	{
		InitCamera(scene_name, section_name);
		if (MonoBehaviourSingleton<UIManager>.I.npcMessage != null)
		{
			MonoBehaviourSingleton<UIManager>.I.npcMessage.UpdateMessage(section_data, is_open: true);
		}
	}

	public void SectionStart(string scene_name, string section_name, bool is_new_section)
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.tutorialMessage != null)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.Run(scene_name, section_name, is_new_section, hide_cursol: false);
		}
	}

	private void InitGlobal(string scene_name, string section_name, GameSceneTables.SectionData section_data)
	{
		if (scene_name != "InGameScene" && !ExistSection("QuestAcceptRoom") && !ExistSection("WorldMapOpenNewField") && MonoBehaviourSingleton<CoopApp>.IsValid())
		{
			MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty();
		}
		Protocol.strict = (scene_name != "InGameScene");
		if (scene_name != "InGameScene" && scene_name != "DebugScene")
		{
			if (!MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
			{
				MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<OutGameEffectManager>();
			}
		}
		else if (MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<OutGameEffectManager>.I);
		}
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.UpdateCamera(scene_name, section_name, section_data);
		}
	}

	private void InitGlobalUI(string scene_name, string section_name)
	{
		if (section_name == "QuestResultFriend")
		{
			MonoBehaviourSingleton<UIManager>.I.LoadUI(need_common: true, need_outgame: false, need_tutorial: false);
		}
	}

	public static IHomeManager GetCurrentIHomeManager()
	{
		if (MonoBehaviourSingleton<HomeManager>.IsValid())
		{
			return MonoBehaviourSingleton<HomeManager>.I;
		}
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			return MonoBehaviourSingleton<LoungeManager>.I;
		}
		if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			return MonoBehaviourSingleton<ClanManager>.I;
		}
		return null;
	}

	private void InitGlobalStage(string scene_name, string section_name, GameSceneTables.SectionData section_data)
	{
		if (needHomeManager)
		{
			if (!MonoBehaviourSingleton<HomeManager>.IsValid())
			{
				Utility.CreateGameObjectAndComponent("HomeManager", MonoBehaviourSingleton<AppMain>.I._transform);
			}
		}
		else if (MonoBehaviourSingleton<HomeManager>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<HomeManager>.I.gameObject);
		}
		if (needLoungeManager)
		{
			if (!MonoBehaviourSingleton<LoungeManager>.IsValid())
			{
				Utility.CreateGameObjectAndComponent("LoungeManager", MonoBehaviourSingleton<AppMain>.I._transform);
			}
		}
		else if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<LoungeManager>.I.gameObject);
		}
		if (needClanManager)
		{
			if (!MonoBehaviourSingleton<ClanManager>.IsValid())
			{
				Utility.CreateGameObjectAndComponent("ClanManager", MonoBehaviourSingleton<AppMain>.I._transform);
			}
		}
		else if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<ClanManager>.I.gameObject);
		}
		if (needGuildManager)
		{
			if (!MonoBehaviourSingleton<GuildStageManager>.IsValid())
			{
				Utility.CreateGameObjectAndComponent("GuildStageManager", MonoBehaviourSingleton<AppMain>.I._transform);
			}
		}
		else if (MonoBehaviourSingleton<GuildStageManager>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<GuildStageManager>.I.gameObject);
		}
		if (needStatusStageManager)
		{
			if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
			{
				Utility.CreateGameObjectAndComponent("StatusStageManager", MonoBehaviourSingleton<AppMain>.I._transform);
			}
			MonoBehaviourSingleton<StatusStageManager>.I.UpdateCamera(scene_name, section_name, section_data);
		}
		else if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<StatusStageManager>.I.gameObject);
		}
		if (!MonoBehaviourSingleton<StageManager>.IsValid())
		{
			return;
		}
		if (!string.IsNullOrEmpty(stageName))
		{
			if (stageForceLoad)
			{
				MonoBehaviourSingleton<StageManager>.I.UnloadStage();
				stageForceLoad = false;
			}
			if (MonoBehaviourSingleton<StageManager>.I.LoadStage(stageName) && cameraAnim != null)
			{
				cameraAnim.enabled = false;
			}
		}
		else if (stageImageID > 0)
		{
			MonoBehaviourSingleton<StageManager>.I.LoadBackgoundImage(stageImageID);
		}
		else
		{
			MonoBehaviourSingleton<StageManager>.I.UnloadStage();
		}
	}

	private void InitOrientation(string scene_name, string section_name)
	{
		SetOrientation(isAvailableScreenRotation(scene_name, section_name));
	}

	public bool isAvailableScreenRotation(string scene_name, string section_name)
	{
		if (scene_name == "InGameScene" && (section_name.StartsWith("InGame") || section_name == "WorldMap" || section_name == "RegionMap" || section_name == "RegionMapDescriptionList" || section_name == "RegionMapDescriptionDetailDelivery" || section_name == "WorldMapOpenNewField" || section_name == "WorldMapOpenNewRegion" || section_name.StartsWith("ExploreMap") || section_name == "InformationDialog"))
		{
			return section_name != "InGameStoryMain";
		}
		return false;
	}

	public static void SetOrientation(bool ingame)
	{
		bool flag = ingame;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name == "/colopl_rob")
		{
			flag = false;
		}
		if (!TutorialStep.HasFirstDeliveryCompleted())
		{
			flag = false;
		}
		if (GameSaveData.instance != null && !GameSaveData.instance.enableLandscape)
		{
			flag = false;
		}
		if (!Native.GetDeviceAutoRotateSetting())
		{
			flag = false;
		}
		if (flag)
		{
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			Screen.orientation = ScreenOrientation.AutoRotation;
			return;
		}
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = true;
		if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
		{
			Screen.orientation = ScreenOrientation.Portrait;
			MonoBehaviourSingleton<AppMain>.I.UpdateResolution(is_portrait: true);
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
			{
				Screen.orientation = ScreenOrientation.AutoRotation;
			});
		}
		else
		{
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
	}

	private void InitCamera(string scene_name, string section_name)
	{
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		if (mainCamera == null || !mainCamera.enabled || !MonoBehaviourSingleton<StageManager>.IsValid())
		{
			return;
		}
		mainCamera.cullingMask = mainCameraCullingMask;
		if (saveCameraNear == float.MinValue)
		{
			saveCameraNear = mainCamera.nearClipPlane;
			saveCameraFar = mainCamera.farClipPlane;
		}
		float nearClipPlane = saveCameraNear;
		float farClipPlane = saveCameraFar;
		Color black = Color.black;
		CameraClearFlags clearFlags = CameraClearFlags.Color;
		if (MonoBehaviourSingleton<StageManager>.I.stageObject == null)
		{
			nearClipPlane = 0.01f;
		}
		mainCamera.nearClipPlane = nearClipPlane;
		mainCamera.farClipPlane = farClipPlane;
		mainCamera.clearFlags = clearFlags;
		mainCamera.backgroundColor = black;
		if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			if (ExistSection("CharaMake"))
			{
				CharaMake.GetCameraPosRot(out Vector3 pos, out Vector3 rot, ExistHistorySection("StatusTop") || ExistHistorySection("ProfileTop"));
				mainCamera.transform.Set(pos, rot);
			}
			else if ((scene_name == "StoryScene" || section_name == "InGameStoryMain") && MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
			{
				mainCamera.transform.Set(new Vector3(0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraHeight, 0f), Vector3.zero);
			}
			else if (scene_name == "ProfileScene")
			{
				mainCamera.transform.Set(MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.friendPos, MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.friendRot);
			}
			else if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<StageManager>.I.currentStageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage)
			{
				mainCamera.transform.Set(MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createCameraPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createCameraRot);
			}
			else if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene != null && MonoBehaviourSingleton<StageManager>.I.currentStageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillStage && section_name != "SmithGrowSkillResult")
			{
				mainCamera.transform.Set(MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillCameraPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillCameraRot);
			}
			else if (scene_name == "StatusScene" || scene_name == "UniqueStatusScene")
			{
				mainCamera.transform.Set(MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.myhousePos, MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.myhouseRot);
			}
			else if (section_name == "GachaPerformanceSkill" || section_name == "GachaPerformanceQuest")
			{
				if (AnimationDirector.I != null)
				{
					AnimationDirector.I.SetLinkCamera(is_link: true);
				}
			}
			else if (section_name == "ShopTop")
			{
				if (AnimationDirector.I != null)
				{
					AnimationDirector.I.SetLinkCamera(is_link: false);
				}
				mainCamera.transform.Set(MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.cameraPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.cameraRot);
			}
			else if (!(section_name == "QuestResultFriend"))
			{
				if (section_name == "HomeTop")
				{
					float num = 0f;
					float num2 = 0f;
					if (MonoBehaviourSingleton<HomeManager>.IsValid())
					{
						num2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.GetSelfCameraHeight();
						num = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.selfCameraTagetHeight;
					}
					else
					{
						num2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.GetSelfCameraHeight();
						num = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.selfCameraTagetHeight;
					}
					Vector3 defaultCameraPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.defaultCameraPos;
					defaultCameraPos.y += num2;
					Vector3 defaultTargetPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.defaultTargetPos;
					defaultTargetPos.y += num;
					mainCamera.transform.position = defaultCameraPos;
					mainCamera.transform.LookAt(defaultTargetPos);
					mainCamera.fieldOfView = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
				}
				else if (section_name == "LoungeTop")
				{
					float num3 = 0f;
					float num4 = 0f;
					if (MonoBehaviourSingleton<HomeManager>.IsValid())
					{
						num4 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.GetSelfCameraHeight();
						num3 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.selfCameraTagetHeight;
					}
					else
					{
						num4 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.GetSelfCameraHeight();
						num3 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.selfCameraTagetHeight;
					}
					Vector3 defaultCameraPos2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.defaultCameraPos;
					defaultCameraPos2.y += num4;
					Vector3 defaultTargetPos2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.defaultTargetPos;
					defaultTargetPos2.y += num3;
					mainCamera.transform.position = defaultCameraPos2;
					mainCamera.transform.LookAt(defaultTargetPos2);
					mainCamera.fieldOfView = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
				}
			}
		}
		UpdateCameraFieldOfView(scene_name, section_name, mainCamera, MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		string text = null;
		if (scene_name == "ShopScene")
		{
			text = "CameraAnim_01";
		}
		if (text != null)
		{
			AnimationClip linkResource = SceneSettingsManager.GetLinkResource<AnimationClip>(text);
			if (linkResource != null)
			{
				if (cameraAnimClip != linkResource)
				{
					if (cameraAnim == null)
					{
						cameraAnim = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.gameObject.AddComponent<Animation>();
					}
					if (cameraAnimClip != null)
					{
						cameraAnim.RemoveClip(cameraAnimClip);
					}
					cameraAnimClip = linkResource;
					cameraAnim.AddClip(cameraAnimClip, text);
					cameraAnim.Stop();
					cameraAnim.Play(text);
				}
				if (cameraAnim != null)
				{
					cameraAnim.enabled = true;
				}
			}
			else
			{
				text = null;
			}
		}
		if (text == null)
		{
			cameraAnimClip = null;
			if (cameraAnim != null)
			{
				UnityEngine.Object.Destroy(cameraAnim);
				cameraAnim = null;
			}
		}
	}

	public void SetMainCameraCullingMask(int mask)
	{
		mainCameraCullingMask = mask;
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		if (!(mainCamera == null))
		{
			mainCamera.cullingMask = mask;
		}
	}

	public static int GetDefaultMainCameraCullingMask()
	{
		return 1620966657;
	}

	private void UpdateCameraFieldOfView(string scene_name, string section_name, Camera camera, bool is_portrait)
	{
		if (camera == null || !camera.enabled || !MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
		{
			return;
		}
		float num = -1f;
		string a = null;
		if (MonoBehaviourSingleton<StageManager>.IsValid())
		{
			a = MonoBehaviourSingleton<StageManager>.I.currentStageName;
		}
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			return;
		}
		if (ExistSection("CharaMake"))
		{
			num = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.cameraFieldOfView;
		}
		else if (scene_name == "StoryScene" || section_name == "InGameStoryMain")
		{
			num = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraFieldOfView;
		}
		else if (scene_name == "ProfileScene")
		{
			num = MonoBehaviourSingleton<OutGameSettingsManager>.I.profileScene.cameraFieldOfView;
		}
		else if (a == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage)
		{
			num = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createCameraFieldOfView;
		}
		else if (a == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillStage)
		{
			num = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillCameraFieldOfView;
		}
		else if (a == MonoBehaviourSingleton<OutGameSettingsManager>.I.gatherScene.mainStage)
		{
			num = MonoBehaviourSingleton<OutGameSettingsManager>.I.gatherScene.cameraFieldOfView;
		}
		else
		{
			if (section_name == "HomeLoginBonusTheater" || section_name == "HomeLoginBonus" || section_name == "HomeLoginBonusNoticeTwo" || section_name == "HomeLoginBonusNoticeOne")
			{
				return;
			}
			if (!(scene_name == "InGameScene"))
			{
				if (scene_name == "UniqueStatusScene" || scene_name == "StatusScene")
				{
					num = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.myhouseFieldOfView;
				}
			}
			else
			{
				if (!section_name.Contains("InGame"))
				{
					return;
				}
				GlobalSettingsManager.CameraParam cameraParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam;
				num = ((!is_portrait) ? cameraParam.inGameLandscapeFieldOfView : cameraParam.inGamePortraitFieldOfView);
			}
		}
		if (num == -1f)
		{
			num = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
		}
		camera.fieldOfView = num;
	}

	public void OnScreenRotate(bool is_portrait)
	{
		UpdateCameraFieldOfView(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName(), MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName(), MonoBehaviourSingleton<AppMain>.I.mainCamera, is_portrait);
	}

	public static bool IsGlobalEvent(string event_name)
	{
		switch (event_name)
		{
		case "MAIN_MENU_HOME":
		case "MAIN_MENU_LOUNGE":
		case "MAIN_MENU_CLAN":
		case "MAIN_MENU_MY_HOUSE":
		case "MAIN_MENU_QUEST":
		case "MAIN_MENU_SHOP":
		case "MAIN_MENU_GACHA":
		case "MAIN_MENU_MENU":
		case "MAIN_MENU_GATHER":
		case "QUEST_ROOM_IN_GAME":
		case "APP_VERSION_RESTRICTION":
		case "EXPAND_STORAGE":
		case "TUTORIAL_NEXT":
		case "TUTORIL_TO_FIELD":
		case "OPINIONBOX":
		case "CHAT_AGE_CONFIRM":
			return true;
		default:
			return false;
		}
	}

	public static bool IsNonPopupError(BaseModel model)
	{
		if (model is CheckRegisterModel)
		{
			return true;
		}
		if (model is RegistCreateModel)
		{
			return true;
		}
		if (model is OptionBirthdayModel)
		{
			return true;
		}
		if (model is LinkRobModel && model.Error == Error.WRN_LINK_ROB_LINKED_WITH_ROB)
		{
			return true;
		}
		if (model is RegistLinkFacebookModel && model.Error == Error.WRN_REGISTER_FACEBOOK_ACCOUNT_LINKED)
		{
			return true;
		}
		if (model is GoPayDepositModel && model.Error == Error.WRN_PAYMENT_GOPAY_PENDING)
		{
			return true;
		}
		if (model is ScreenshotSharingModel && model.Error == Error.WRN_USER_HAD_SCREEN_SHOT)
		{
			return true;
		}
		if (model is OptionSetParentPassModel || model is OptionResetParentPassModel)
		{
			return true;
		}
		if (model is GoldCanPurchaseModel && model.Error == Error.WRN_GOLD_OVER_LIMITTER_OVERUSE)
		{
			return true;
		}
		if (model is FriendFollowModel)
		{
			return true;
		}
		if (model is FriendSearchByCodeModel && model.Error == Error.WRN_FRIEND_CODE_NOT_FOUND)
		{
			return true;
		}
		if ((model is ShopBuyModel || model is GachaGachaModel) && model.Error == Error.ERR_CRYSTAL_NOT_ENOUGH)
		{
			return true;
		}
		_ = (model is QuestCompleteModel);
		if (model is QuestContinueModel)
		{
			return true;
		}
		if (model is PartySearchModel && model.Error == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
		{
			return true;
		}
		if (model is PartyModel)
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "QuestAcceptEntryPassRoom" && model.Error == Error.WRN_PARTY_SEARCH_NOT_FOUND_PARTY)
			{
				return true;
			}
			if (model.Error == Error.WRN_PARTY_OWNER_REJOIN)
			{
				return true;
			}
			if (model.Error == Error.WRN_QUEST_IS_ORDER)
			{
				return true;
			}
			if (model.Error == Error.WRN_PARTY_TOO_MANY_PARTIES)
			{
				return true;
			}
			if (model.Error == Error.WRN_PARTY_ALREADY_FINISH)
			{
				return true;
			}
			if (model.Error == Error.WRN_PARTY_EXPIRED_OVER)
			{
				return true;
			}
		}
		if (model is PartyLeaveModel && model.Error == Error.ERR_PARTY_NOT_FOUND_PARTY)
		{
			return true;
		}
		if (model is FieldModel && model.Error == Error.WRN_QUEST_IS_ORDER)
		{
			return true;
		}
		if (model is PresentReceiveModel)
		{
			if (model.Error == Error.WRN_PRESENT_OVER_MONEY)
			{
				return true;
			}
			if (model.Error == Error.WRN_PRESENT_OVER_ITEM)
			{
				return true;
			}
			if (model.Error == Error.WRN_PRESENT_OVER_EQUIP_ITEM)
			{
				return true;
			}
			if (model.Error == Error.WRN_PRESENT_OVER_SKILL_ITEM)
			{
				return true;
			}
			if (model.Error == Error.WRN_PRESENT_OVER_QUEST_ITEM)
			{
				return true;
			}
			if (model.Error == Error.WRN_PRESENT_OVER_EQUIP_AND_SKILL)
			{
				return true;
			}
			if (model.Error == Error.WRN_PRESENT_OVER_ETC)
			{
				return true;
			}
		}
		if (model is SmithCreateModel && model.Error == Error.WRN_SMITH_OVER_EQUIP_ITEM_NUM)
		{
			return true;
		}
		if (model is ClanRoomQuestModel && model.Error == Error.WRN_CLAN_NOT_JOINED)
		{
			return true;
		}
		if (model is ClanAcceptInviteModel)
		{
			if (model.Error == Error.WRN_CLAN_NOT_EXISTS_INVITE)
			{
				return true;
			}
			if (model.Error == Error.WRN_CLAN_NOT_EXISTS_CLAN)
			{
				return true;
			}
		}
		if (model is GuildStatisticModel && model.Error == Error.WRN_GUILD_DELETE_GET_DATA)
		{
			return true;
		}
		if (model is DarkMarketBuyModel)
		{
			if (model.Error == Error.ERR_BM_NOT_ENOUGH_GOLD)
			{
				return true;
			}
			if (model.Error == Error.ERR_BM_NOT_ENOUGH_GEM)
			{
				return true;
			}
			if (model.Error == Error.ERR_BM_ITEM_UNAVAILABLE)
			{
				return true;
			}
			if (model.Error == Error.ERR_BLACK_MARKET_BUY)
			{
				return true;
			}
			if (model.Error == Error.ERR_BM_ITEM_SOLD_OUT)
			{
				return true;
			}
		}
		return false;
	}

	private static bool ExistSection(string section_name)
	{
		return MonoBehaviourSingleton<GameSceneManager>.I.FindSection(section_name) != null;
	}

	private static bool ExistHistorySection(string section_name)
	{
		return MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory(section_name);
	}

	public static bool IsDisplayMainUI(string scene_name, string section_name, bool check_tutorial = true)
	{
		if (check_tutorial && !TutorialStep.HasFirstDeliveryCompleted())
		{
			return false;
		}
		switch (scene_name)
		{
		case "TitleScene":
		case "InGameScene":
		case "GachaScene":
		case "StoryScene":
		case "QuestResultScene":
		case "DebugScene":
		case "UniqueStatusScene":
		case "EnemyDownloadScene":
			return false;
		default:
			if (scene_name.Contains("TutorialWeaponSelect"))
			{
				return false;
			}
			if (scene_name == "HomeScene" || scene_name == "LoungeScene" || scene_name == "ClanScene")
			{
				if (section_name == "MenuReset")
				{
					return false;
				}
				if (ExistSection("QuestAcceptRoom") || ExistSection("HomeLoginBonusTheater"))
				{
					return false;
				}
				if (section_name.Contains("GuildDonateMaterialSelectDialog"))
				{
					return false;
				}
				if (section_name.Contains("GuildSmithGrowItemSelect"))
				{
					return false;
				}
				if (section_name.Contains("GuildSmithGrow"))
				{
					return false;
				}
				string prevSectionNameFromHistory = MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory();
				if (prevSectionNameFromHistory != null)
				{
					if (prevSectionNameFromHistory.Contains("GuildDonateMaterialSelectDialog"))
					{
						return false;
					}
					if (prevSectionNameFromHistory.Contains("GuildSmithGrow"))
					{
						return false;
					}
					if (prevSectionNameFromHistory.Contains("GuildSmithGrowItemSelect"))
					{
						return false;
					}
				}
			}
			if (scene_name == "SmithScene" && section_name.Contains("Performance"))
			{
				return false;
			}
			if (scene_name == "StatusScene" && ExistSection("StatusScreenShot"))
			{
				return false;
			}
			if (scene_name == "TitleScene" && section_name == "MenuReset")
			{
				return false;
			}
			if (forceIgnoreMainUI)
			{
				return false;
			}
			return true;
		}
	}

	public static bool IsDisplayMainStatusUI(string scene_name, string section_name)
	{
		if (scene_name == "StatusScene")
		{
			string a = section_name;
			GameSection lastSectionExcludeDialog = MonoBehaviourSingleton<GameSceneManager>.I.GetLastSectionExcludeDialog();
			if (lastSectionExcludeDialog != null && lastSectionExcludeDialog.sectionData != null)
			{
				a = lastSectionExcludeDialog.sectionData.sectionName;
			}
			if (a == "StatusAvatarEquipSelect" || a == "StatusEquip" || a == "StatusEquipSort" || a == "StatusEquipList" || a == "StatusEnemyList" || a == "StatusEquipListAchievement")
			{
				return false;
			}
		}
		if (scene_name == "HomeScene" || scene_name == "LoungeScene")
		{
			if (section_name.Contains("GuildDonateMaterialSelectDialog"))
			{
				return true;
			}
			if (section_name.Contains("GuildSmithGrowItemSelect"))
			{
				return true;
			}
			if (section_name.Contains("GuildSmithGrow"))
			{
				return true;
			}
			string prevSectionNameFromHistory = MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory();
			if (prevSectionNameFromHistory != null)
			{
				if (prevSectionNameFromHistory.Contains("GuildDonateMaterialSelectDialog"))
				{
					return true;
				}
				if (prevSectionNameFromHistory.Contains("GuildSmithGrowItemSelect"))
				{
					return true;
				}
				if (prevSectionNameFromHistory.Contains("GuildSmithGrow"))
				{
					return true;
				}
			}
		}
		return IsDisplayMainUI(scene_name, section_name, check_tutorial: false);
	}

	public static bool IsActiveMainUI()
	{
		string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
		if (currentSection != null && currentSection.sectionData != null && currentSection.sectionData.type.IsDialog())
		{
			return false;
		}
		switch (currentSectionName)
		{
		case "QuestStartChangeEquipSet":
		case "QuestStorageOverflowEquip":
		case "QuestStorageOverflowSkill":
		case "QuestRoomInvalid":
		case "QuestRoomInvalid_EquipChange":
		case "QuestRoomInvalid_UserDetail":
		case "QuestRoomInvalid_UserDetailItem":
		case "PresentTop":
			return false;
		default:
			return true;
		}
	}

	private void ResetAudioRestener()
	{
		if (MonoBehaviourSingleton<AudioListenerManager>.IsValid())
		{
			MonoBehaviourSingleton<AudioListenerManager>.I.ReSetTargetObject();
		}
	}

	private void UpdateBGM(string scene_name, string section_name)
	{
		SoundManager i = MonoBehaviourSingleton<SoundManager>.I;
		i.fadeOutTime = 1f;
		int playingBGMID = MonoBehaviourSingleton<SoundManager>.I.playingBGMID;
		int num = -1;
		string text = "Default";
		switch (scene_name)
		{
		case "TitleScene":
			if (section_name == "Opening")
			{
				num = 13;
			}
			if (section_name == "Opening")
			{
				text = "Opening";
			}
			break;
		case "QuestResultScene":
			num = 0;
			text = "QuestResult";
			break;
		case "HomeScene":
			num = Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow()).bgmId;
			if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("HomeTop") && MonoBehaviourSingleton<GameSceneManager>.I.prev_scene_name == "HomeScene")
			{
				num = playingBGMID;
			}
			if (num <= 0)
			{
				num = 2;
			}
			text = "Home";
			break;
		case "LoungeScene":
			num = 153;
			text = "Lounge";
			MonoBehaviourSingleton<SoundManager>.I.TransitionPreset(3u);
			break;
		case "ClanScene":
			num = 153;
			text = "Lounge";
			MonoBehaviourSingleton<SoundManager>.I.TransitionPreset(3u);
			break;
		case "StoryScene":
			text = "Story";
			break;
		case "GatherScene":
			num = 108;
			break;
		case "InGameScene":
			UpdateIngameBGM(section_name);
			UpdateIngamePreset(section_name);
			text = "InGame";
			break;
		case "StatusScene":
			num = 6;
			text = "Home";
			break;
		case "ShopScene":
			num = 7;
			text = "Gacha";
			break;
		case "GachaScene":
			num = ((section_name == "GachaPerformanceQuest") ? 8 : ((!(section_name == "GachaPerformanceSkill")) ? 7 : 9));
			text = "Gacha";
			break;
		case "UniqueStatusScene":
			num = 6;
			text = "Home";
			break;
		}
		if (num != -1)
		{
			i.requestBGMID = num;
		}
		if (text != "InGame")
		{
			i.TransitionTo(text);
		}
	}

	public static void RequestSoundSettingIngameField()
	{
		int num = -1;
		if (MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			num = MonoBehaviourSingleton<FieldManager>.I.GetCurrentMapBGMID();
		}
		if (num > 0)
		{
			SoundManager.RequestBGM(num);
		}
	}

	private void UpdateIngamePreset(string section_name)
	{
		if (!(section_name == "QuestResultDirection") && !(section_name == "QuestResultFriend"))
		{
			if (section_name == "InGameMain")
			{
				SoundID.AudioSettinID presetId = QuestManager.IsValidInGame() ? SoundID.AudioSettinID.IngameBoss : SoundID.AudioSettinID.IngameField;
				MonoBehaviourSingleton<SoundManager>.I.TransitionPreset((uint)presetId);
			}
		}
		else
		{
			MonoBehaviourSingleton<SoundManager>.I.TransitionTo("QuestResult");
		}
	}

	private void UpdateIngameBGM(string section_name)
	{
		int num = -1;
		switch (section_name)
		{
		case "QuestResultDirection":
		case "QuestResultFriend":
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
			{
				num = 4;
				break;
			}
			num = -1;
			SoundManager.RequestBGM(10, isLoop: false);
			break;
		case "InGameFieldQuestConfirm":
			num = 12;
			break;
		case "InGameMain":
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isEnding)
			{
				num = -1;
			}
			else if (QuestManager.IsValidInGameExplore())
			{
				if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
				{
					int currentQuestBGMID = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID();
					if (currentQuestBGMID > 0)
					{
						num = currentQuestBGMID;
					}
				}
				if (num < 0 && MonoBehaviourSingleton<FieldManager>.IsValid())
				{
					int currentMapBGMID = MonoBehaviourSingleton<FieldManager>.I.GetCurrentMapBGMID();
					if (currentMapBGMID > 0)
					{
						num = currentMapBGMID;
					}
				}
			}
			else
			{
				if (!QuestManager.IsValidInGame())
				{
					break;
				}
				if (MonoBehaviourSingleton<QuestManager>.IsValid())
				{
					num = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID();
				}
				if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestHappen && MonoBehaviourSingleton<FieldManager>.IsValid())
				{
					int happenMapBGMID = MonoBehaviourSingleton<FieldManager>.I.GetHappenMapBGMID();
					if (happenMapBGMID > 0)
					{
						num = happenMapBGMID;
					}
				}
			}
			break;
		}
		if (num > 0)
		{
			SoundManager.RequestBGM(num);
		}
	}
}
