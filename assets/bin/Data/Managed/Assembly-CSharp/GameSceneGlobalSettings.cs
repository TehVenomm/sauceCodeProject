using Network;
using System;
using UnityEngine;

public class GameSceneGlobalSettings
{
	private const string INGAME_PRESET = "InGame";

	public string externalStageName;

	private string stageName;

	private bool stageForceLoad;

	private int stageImageID;

	private Animation cameraAnim;

	private AnimationClip cameraAnimClip;

	private bool needHomeManager;

	private bool needLoungeManager;

	private bool needGuildManager;

	private bool needStatusStageManager;

	private float saveCameraNear = -3.40282347E+38f;

	private float saveCameraFar = -3.40282347E+38f;

	private int mainCameraCullingMask;

	public bool isInitialized
	{
		get
		{
			if (MonoBehaviourSingleton<StageManager>.IsValid() && MonoBehaviourSingleton<StageManager>.I.isLoading)
			{
				return false;
			}
			if (MonoBehaviourSingleton<HomeManager>.IsValid())
			{
				if (!MonoBehaviourSingleton<HomeManager>.I.IsInitialized)
				{
					return false;
				}
				if (MonoBehaviourSingleton<HomeManager>.I.HomeCamera.isChanging)
				{
					return false;
				}
			}
			if (MonoBehaviourSingleton<LoungeManager>.IsValid())
			{
				if (!MonoBehaviourSingleton<LoungeManager>.I.IsInitialized)
				{
					return false;
				}
				if (MonoBehaviourSingleton<LoungeManager>.I.HomeCamera.isChanging)
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

	public GameSceneGlobalSettings()
	{
		mainCameraCullingMask = GetDefaultMainCameraCullingMask();
	}

	public void ChangeSection(GameSceneTables.SceneData scene_data, GameSceneTables.SectionData section_data)
	{
		if (MonoBehaviourSingleton<UIManager>.I.npcMessage != null)
		{
			MonoBehaviourSingleton<UIManager>.I.npcMessage.UpdateMessage(section_data, false);
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
		bool flag = next_scene_name == "HomeScene" || next_scene_name == "LoungeScene";
		if (prev_scene_name == "InGameScene" && prev_section_name == "InGameQuestAcceptInvitation" && flag)
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
		bool result = true;
		if (prev_scene_name == "TitleScene" && next_scene_name == "InGameScene")
		{
			result = false;
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
			case "SmithScene":
			case "StatusScene":
			case "ShopScene":
			case "GachaScene":
			case "FriendScene":
			case "ProfileScene":
				MonoBehaviourSingleton<GameSceneManager>.I.ClearHistory();
				break;
			}
			if ((next_scene_name == "StatusScene" && prev_scene_name == "SmithScene") || (next_scene_name == "SmithScene" && prev_scene_name == "StatusScene"))
			{
				result = false;
			}
			if (MonoBehaviourSingleton<UIManager>.I.mainMenu != null)
			{
				MonoBehaviourSingleton<UIManager>.I.mainMenu.UpdateSceneButtons(next_scene_name);
			}
		}
		return result;
	}

	public void SceneInitialize(string prev_scene_name, string next_scene_name)
	{
		if (next_scene_name != "InGameScene")
		{
			bool need_tutorial = UserInfoManager.IsNeedsTutorialMessage();
			MonoBehaviourSingleton<UIManager>.I.LoadUI(true, true, need_tutorial);
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
		else if (!(section_data != (GameSceneTables.SectionData)null) || !section_data.type.IsDialog())
		{
			string text = stageName;
			stageName = null;
			stageImageID = -1;
			needHomeManager = false;
			needLoungeManager = false;
			needGuildManager = false;
			needStatusStageManager = false;
			if (!(section_name == "InGameMain") && !(section_name == "MenuReset"))
			{
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
				if (stageName == null && scene_name == "GuildScene")
				{
					stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.guildScene.mainStage;
					needGuildManager = true;
				}
				if (stageName == null && (scene_name == "StatusScene" || scene_name == "ProfileScene"))
				{
					stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage;
					if (scene_name == "StatusScene")
					{
						needStatusStageManager = true;
					}
				}
				if (stageName == null && scene_name == "SmithScene")
				{
					if (section_name == "SmithGrowSkillSelect" || section_name == "SmithGrowSkillPerformance" || section_name == "SmithGrowSkillSecond")
					{
						stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillStage;
					}
					else if (section_name == "SmithCreateTypeSelect" || section_name == "SmithGrowItemSelect" || section_name.EndsWith("Performance"))
					{
						stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage;
					}
					else if (prev_scene_name == "HomeScene" || prev_scene_name == "LoungeScene")
					{
						stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage;
					}
					else
					{
						stageName = text;
					}
					if (stageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage)
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
							stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gachaScene.QuestReamGachaStage;
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
				if (stageName == null && stageImageID == -1)
				{
					if (scene_name == "StatusScene" || scene_name == "ItemStorageScene")
					{
						stageImageID = 10000000;
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
			MonoBehaviourSingleton<UIManager>.I.npcMessage.UpdateMessage(section_data, true);
		}
	}

	public void SectionStart(string scene_name, string section_name, bool is_new_section)
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.tutorialMessage != null)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.Run(scene_name, section_name, is_new_section, false, null);
		}
	}

	private void InitGlobal(string scene_name, string section_name, GameSceneTables.SectionData section_data)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (scene_name != "InGameScene" && !ExistSection("QuestAcceptRoom") && !ExistSection("WorldMapOpenNewField") && MonoBehaviourSingleton<CoopApp>.IsValid())
		{
			MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty(null, false, false);
		}
		Protocol.strict = (scene_name != "InGameScene");
		if (scene_name != "InGameScene" && scene_name != "DebugScene")
		{
			if (!MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
			{
				MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<OutGameEffectManager>();
			}
		}
		else if (MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<OutGameEffectManager>.I);
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
			MonoBehaviourSingleton<UIManager>.I.LoadUI(true, false, false);
		}
	}

	private void InitGlobalStage(string scene_name, string section_name, GameSceneTables.SectionData section_data)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		if (needHomeManager)
		{
			if (!MonoBehaviourSingleton<HomeManager>.IsValid())
			{
				Utility.CreateGameObjectAndComponent("HomeManager", MonoBehaviourSingleton<AppMain>.I._transform, -1);
			}
		}
		else if (MonoBehaviourSingleton<HomeManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<HomeManager>.I.get_gameObject());
		}
		if (needLoungeManager)
		{
			if (!MonoBehaviourSingleton<LoungeManager>.IsValid())
			{
				Utility.CreateGameObjectAndComponent("LoungeManager", MonoBehaviourSingleton<AppMain>.I._transform, -1);
			}
		}
		else if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<LoungeManager>.I.get_gameObject());
		}
		if (needGuildManager)
		{
			if (!MonoBehaviourSingleton<GuildStageManager>.IsValid())
			{
				Utility.CreateGameObjectAndComponent("GuildStageManager", MonoBehaviourSingleton<AppMain>.I._transform, -1);
			}
		}
		else if (MonoBehaviourSingleton<GuildStageManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<GuildStageManager>.I.get_gameObject());
		}
		if (needStatusStageManager)
		{
			if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
			{
				Utility.CreateGameObjectAndComponent("StatusStageManager", MonoBehaviourSingleton<AppMain>.I._transform, -1);
			}
			MonoBehaviourSingleton<StatusStageManager>.I.UpdateCamera(scene_name, section_name, section_data);
		}
		else if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<StatusStageManager>.I.get_gameObject());
		}
		if (MonoBehaviourSingleton<StageManager>.IsValid())
		{
			if (!string.IsNullOrEmpty(stageName))
			{
				if (stageForceLoad)
				{
					MonoBehaviourSingleton<StageManager>.I.UnloadStage();
					stageForceLoad = false;
				}
				if (MonoBehaviourSingleton<StageManager>.I.LoadStage(stageName) && cameraAnim != null)
				{
					cameraAnim.set_enabled(false);
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
	}

	private void InitOrientation(string scene_name, string section_name)
	{
		bool orientation = isAvailableScreenRotation(scene_name, section_name);
		SetOrientation(orientation);
	}

	public bool isAvailableScreenRotation(string scene_name, string section_name)
	{
		return scene_name == "InGameScene" && (section_name.StartsWith("InGame") || section_name == "WorldMap" || section_name == "RegionMap" || section_name == "RegionMapDescriptionList" || section_name == "RegionMapDescriptionDetailDelivery" || section_name == "WorldMapOpenNewField" || section_name == "WorldMapOpenNewRegion" || section_name.StartsWith("ExploreMap") || section_name == "InformationDialog") && section_name != "InGameStoryMain";
	}

	public unsafe static void SetOrientation(bool ingame)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Invalid comparison between Unknown and I4
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Invalid comparison between Unknown and I4
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Invalid comparison between Unknown and I4
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
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
			Screen.set_autorotateToLandscapeLeft(true);
			Screen.set_autorotateToLandscapeRight(true);
			Screen.set_autorotateToPortrait(true);
			Screen.set_autorotateToPortraitUpsideDown(true);
			Screen.set_orientation(5);
		}
		else
		{
			Screen.set_autorotateToLandscapeLeft(false);
			Screen.set_autorotateToLandscapeRight(false);
			Screen.set_autorotateToPortrait(true);
			Screen.set_autorotateToPortraitUpsideDown(true);
			if ((int)Screen.get_orientation() == 0 || (int)Screen.get_orientation() == 3 || (int)Screen.get_orientation() == 3 || (int)Screen.get_orientation() == 4)
			{
				Screen.set_orientation(1);
				MonoBehaviourSingleton<AppMain>.I.UpdateResolution(true);
				AppMain i = MonoBehaviourSingleton<AppMain>.I;
				Action onDelayCall = i.onDelayCall;
				if (_003C_003Ef__am_0024cacheE == null)
				{
					_003C_003Ef__am_0024cacheE = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				i.onDelayCall = Delegate.Combine((Delegate)onDelayCall, (Delegate)_003C_003Ef__am_0024cacheE);
			}
			else
			{
				Screen.set_orientation(5);
			}
		}
	}

	private void InitCamera(string scene_name, string section_name)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Expected O, but got Unknown
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Expected O, but got Unknown
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Expected O, but got Unknown
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		if (mainCamera == null || !mainCamera.get_enabled() || !MonoBehaviourSingleton<StageManager>.IsValid())
		{
			return;
		}
		mainCamera.set_cullingMask(mainCameraCullingMask);
		if (saveCameraNear == -3.40282347E+38f)
		{
			saveCameraNear = mainCamera.get_nearClipPlane();
			saveCameraFar = mainCamera.get_farClipPlane();
		}
		float nearClipPlane = saveCameraNear;
		float farClipPlane = saveCameraFar;
		Color black = Color.get_black();
		CameraClearFlags clearFlags = 2;
		if (MonoBehaviourSingleton<StageManager>.I.stageObject == null)
		{
			nearClipPlane = 0.01f;
		}
		mainCamera.set_nearClipPlane(nearClipPlane);
		mainCamera.set_farClipPlane(farClipPlane);
		mainCamera.set_clearFlags(clearFlags);
		mainCamera.set_backgroundColor(black);
		if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			if (ExistSection("CharaMake"))
			{
				Vector3 pos = default(Vector3);
				Vector3 rot = default(Vector3);
				CharaMake.GetCameraPosRot(out pos, out rot, ExistHistorySection("StatusTop") || ExistHistorySection("ProfileTop"));
				mainCamera.get_transform().Set(pos, rot);
			}
			else if (scene_name == "StoryScene" || section_name == "InGameStoryMain")
			{
				Utility.Set(mainCamera.get_transform(), new Vector3(0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraHeight, 0f), Vector3.get_zero());
			}
			else if (scene_name == "ProfileScene")
			{
				mainCamera.get_transform().Set(MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.friendPos, MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.friendRot);
			}
			else if (MonoBehaviourSingleton<StageManager>.I.currentStageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage)
			{
				mainCamera.get_transform().Set(MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createCameraPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createCameraRot);
			}
			else if (MonoBehaviourSingleton<StageManager>.I.currentStageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillStage && section_name != "SmithGrowSkillResult")
			{
				mainCamera.get_transform().Set(MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillCameraPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillCameraRot);
			}
			else if (scene_name == "StatusScene")
			{
				mainCamera.get_transform().Set(MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.myhousePos, MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.myhouseRot);
			}
			else if (section_name == "GachaPerformanceSkill" || section_name == "GachaPerformanceQuest")
			{
				if (AnimationDirector.I != null)
				{
					AnimationDirector.I.SetLinkCamera(true);
				}
			}
			else if (section_name == "ShopTop")
			{
				if (AnimationDirector.I != null)
				{
					AnimationDirector.I.SetLinkCamera(false);
				}
				mainCamera.get_transform().Set(MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.cameraPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.cameraRot);
			}
			else if (!(section_name == "QuestResultFriend"))
			{
				goto IL_0332;
			}
		}
		goto IL_0332;
		IL_0332:
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
						cameraAnim = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.get_gameObject().AddComponent<Animation>();
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
					cameraAnim.set_enabled(true);
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
				Object.Destroy(cameraAnim);
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
			mainCamera.set_cullingMask(mask);
		}
	}

	public static int GetDefaultMainCameraCullingMask()
	{
		return 1620966657;
	}

	private void UpdateCameraFieldOfView(string scene_name, string section_name, Camera camera, bool is_portrait)
	{
		if (!(camera == null) && camera.get_enabled() && MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
		{
			float num = -1f;
			string a = null;
			if (MonoBehaviourSingleton<StageManager>.IsValid())
			{
				a = MonoBehaviourSingleton<StageManager>.I.currentStageName;
			}
			if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
			{
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
					switch (scene_name)
					{
					case "InGameScene":
					{
						if (!section_name.Contains("InGame"))
						{
							return;
						}
						GlobalSettingsManager.CameraParam cameraParam2 = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam;
						num = ((!is_portrait) ? cameraParam2.inGameLandscapeFieldOfView : cameraParam2.inGamePortraitFieldOfView);
						break;
					}
					case "StatusScene":
					{
						GlobalSettingsManager.CameraParam cameraParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam;
						num = cameraParam.myhouseFieldOfView;
						break;
					}
					}
				}
				if (num == -1f)
				{
					GlobalSettingsManager.CameraParam cameraParam3 = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam;
					num = cameraParam3.outGameFieldOfView;
				}
				camera.set_fieldOfView(num);
			}
		}
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
		if (!(model is QuestCompleteModel))
		{
			goto IL_0110;
		}
		goto IL_0110;
		IL_0110:
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
		if (!check_tutorial || TutorialStep.HasFirstDeliveryCompleted())
		{
			switch (scene_name)
			{
			case "TitleScene":
			case "InGameScene":
			case "GachaScene":
			case "StoryScene":
			case "QuestResultScene":
			case "DebugScene":
				return false;
			default:
				if (scene_name == "HomeScene" || scene_name == "LoungeScene")
				{
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
				return true;
			}
		}
		return false;
	}

	public static bool IsDisplayMainStatusUI(string scene_name, string section_name)
	{
		if (scene_name == "StatusScene")
		{
			string text = section_name;
			GameSection lastSectionExcludeDialog = MonoBehaviourSingleton<GameSceneManager>.I.GetLastSectionExcludeDialog();
			if (lastSectionExcludeDialog != null && lastSectionExcludeDialog.sectionData != (GameSceneTables.SectionData)null)
			{
				text = lastSectionExcludeDialog.sectionData.sectionName;
			}
			switch (text)
			{
			case "StatusAvatarEquipSelect":
			case "StatusEquip":
			case "StatusEquipSort":
			case "StatusEquipList":
			case "StatusEnemyList":
			case "StatusEquipListAchievement":
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
		return IsDisplayMainUI(scene_name, section_name, false);
	}

	public static bool IsActiveMainUI()
	{
		string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
		if (!(currentSection != null) || !(currentSection.sectionData != (GameSceneTables.SectionData)null) || !currentSection.sectionData.type.IsDialog())
		{
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
		return false;
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
		{
			HomeThemeTable.HomeThemeData homeThemeData = Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow());
			num = homeThemeData.bgmId;
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
		}
		case "LoungeScene":
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
		}
		if (num != -1)
		{
			i.requestBGMID = num;
		}
		if (text != "InGame")
		{
			i.TransitionTo(text, 1f);
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
			SoundManager.RequestBGM(num, true);
		}
	}

	private void UpdateIngamePreset(string section_name)
	{
		switch (section_name)
		{
		case "QuestResultDirection":
		case "QuestResultFriend":
			MonoBehaviourSingleton<SoundManager>.I.TransitionTo("QuestResult", 1f);
			break;
		case "InGameMain":
		{
			SoundID.AudioSettinID presetId = QuestManager.IsValidInGame() ? SoundID.AudioSettinID.IngameBoss : SoundID.AudioSettinID.IngameField;
			MonoBehaviourSingleton<SoundManager>.I.TransitionPreset((uint)presetId);
			break;
		}
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
			}
			else
			{
				num = -1;
				SoundManager.RequestBGM(10, false);
			}
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
			else if (QuestManager.IsValidInGame())
			{
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
			SoundManager.RequestBGM(num, true);
		}
	}
}
