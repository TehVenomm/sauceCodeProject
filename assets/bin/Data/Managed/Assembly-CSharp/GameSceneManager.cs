using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviourSingleton<GameSceneManager>
{
	private class GameSceneTask : IComparable<GameSceneTask>
	{
		public int Priority;

		public string SceneName;

		public string SectionName;

		public bool Error;

		public bool InternalRes;

		public UITransition.TYPE CloseType;

		public UITransition.TYPE OpenType;

		public bool ReloadSceneFlag;

		public GameSceneTask(int priority)
		{
			Priority = priority;
		}

		public int CompareTo(GameSceneTask other)
		{
			if (this == null && other == null)
			{
				return 0;
			}
			if (this == null)
			{
				return -1;
			}
			if (other == null)
			{
				return 1;
			}
			return Priority.CompareTo(other.Priority);
		}
	}

	public const string STR_EVENT_BACK = "[BACK]";

	private const string STR_EVENT_VERSION_RESTRICTION = "APP_VERSION_RESTRICTION";

	private const string STR_EVENT_VERSION_RESTRICTION_AUTO = "APP_VERSION_RESTRICTION_AUTO";

	private const string STR_RECOMMENDED_VERSION_CEHCK_EVENT = "RecommendedVersionCheck";

	private static bool isAutoEventTeleportMode = true;

	private GameSceneTables tables;

	private GameSectionHistory history;

	private GameSectionHierarchy hierarchy;

	private GameSceneGlobalSettings global;

	private long notifyFlags;

	private IEnumerator notifyCoroutine;

	private int downloadErrorResult;

	private static readonly List<GameSceneTables.TextData> emptyTextList = new List<GameSceneTables.TextData>();

	private static bool Use_Force = true;

	private PriorityQueue<GameSceneTask> q_ForceTask = new PriorityQueue<GameSceneTask>();

	private int doWaitEventCount;

	private EventData[] autoEvents;

	private Action onAutoEventFinished;

	private Action<string> commonDialogCallback;

	private GameSceneEvent commonDialogSaveCurrentEvent;

	private string commonDialogResult;

	public static bool isAutoEventSkip
	{
		get
		{
			if (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
			{
				return false;
			}
			if (MonoBehaviourSingleton<GameSceneManager>.I.autoEvents == null)
			{
				return false;
			}
			return isAutoEventTeleportMode;
		}
	}

	public bool isInitialized
	{
		get;
		private set;
	}

	public bool isChangeing
	{
		get;
		private set;
	}

	public bool isWaiting
	{
		get;
		private set;
	}

	public bool isCallingOnQuery
	{
		get;
		private set;
	}

	public bool skipTrantisionEnd
	{
		get;
		set;
	}

	public string prev_scene_name
	{
		get;
		private set;
	}

	public bool isOpenImportantDialog
	{
		get;
		private set;
	}

	public bool isOpenCommonDialog => commonDialogCallback != null;

	protected override void Awake()
	{
		base.Awake();
		global = new GameSceneGlobalSettings();
		history = new GameSectionHistory();
		hierarchy = new GameSectionHierarchy();
		UnityEngine.Object.DontDestroyOnLoad(this);
		GameSceneEvent.Initialize();
		MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += global.OnScreenRotate;
	}

	public void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		isInitialized = false;
		tables = new GameSceneTables();
		LoadingQueue loadingQueue = new LoadingQueue(this);
		ResourceManager.enableCache = false;
		LoadObject lo_common_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "CommonDialogTable");
		ResourceManager.enableCache = true;
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		tables.CreateCommonResourceTable(lo_common_table.loadedObject as TextAsset);
		isInitialized = true;
	}

	private void OnEnable()
	{
		MonoBehaviourSingleton<ResourceManager>.I.onDownloadErrorQuery = OnDownloadErrorQuery;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			MonoBehaviourSingleton<ResourceManager>.I.onDownloadErrorQuery = null;
		}
	}

	private int OnDownloadErrorQuery(bool is_init, Error error_code)
	{
		if (is_init)
		{
			if (isOpenCommonDialog)
			{
				return -1;
			}
			downloadErrorResult = 0;
			OpenCommonDialog_(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage((uint)error_code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111u)), delegate(string btn)
			{
				if (btn == "YES")
				{
					downloadErrorResult = 1;
				}
				else
				{
					MonoBehaviourSingleton<AppMain>.I.Reset();
					downloadErrorResult = -1;
				}
			}, error: true, internal_res: true);
		}
		return downloadErrorResult;
	}

	public virtual void SetNotify(GameSection.NOTIFY_FLAG flag)
	{
		notifyFlags |= (long)flag;
		if (notifyCoroutine == null)
		{
			StartCoroutine(notifyCoroutine = DoNotifyUpdate());
		}
	}

	private IEnumerator DoNotifyUpdate()
	{
		while (notifyFlags != 0L)
		{
			yield return null;
			if (notifyFlags != 0L && !GameSceneEvent.IsStay() && !Protocol.isBusy && !isWaiting)
			{
				bool save_isWaiting = isWaiting;
				isWaiting = true;
				MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.NOTIFY, is_disable: true);
				try
				{
					DoNotify((GameSection.NOTIFY_FLAG)notifyFlags);
				}
				catch (Exception exc)
				{
					Log.Exception(exc);
				}
				notifyFlags = 0L;
				while (Protocol.isBusy)
				{
					yield return null;
				}
				MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.NOTIFY, is_disable: false);
				isWaiting = save_isWaiting;
			}
		}
		notifyCoroutine = null;
	}

	private void DoNotify(GameSection.NOTIFY_FLAG flags)
	{
		hierarchy.DoNotify(flags);
		MonoBehaviourSingleton<UIManager>.I.OnNotify(flags);
	}

	private void Send(bool set_wait_flag, MonoBehaviour target, string func, object param = null)
	{
		if (!isOpenCommonDialog)
		{
			bool isWaiting = this.isWaiting;
			if (set_wait_flag)
			{
				this.isWaiting = true;
			}
			if (param == null)
			{
				target.SendMessage(func, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				target.SendMessage(func, param, SendMessageOptions.DontRequireReceiver);
			}
			this.isWaiting = isWaiting;
		}
	}

	public GameSection GetCurrentScene()
	{
		return hierarchy.GetTyped(GAME_SECTION_TYPE.SCENE)?.section;
	}

	public GameSection GetCurrentScreen()
	{
		return hierarchy.GetTyped(GAME_SECTION_TYPE.SCREEN)?.section;
	}

	public GameSection GetCurrentSection()
	{
		return hierarchy.GetLast()?.section;
	}

	public GameSection GetLastSectionExcludeDialog()
	{
		return hierarchy.GetLastExcludeDialog()?.section;
	}

	public GameSection GetLastSectionExcludeCommonDialog()
	{
		return hierarchy.GetLastExcludeCommonDialog()?.section;
	}

	public GameSection FindSection(string section_name)
	{
		return hierarchy.Find(section_name)?.section;
	}

	public bool ExistHistory(string section_name)
	{
		return history.Exist(section_name);
	}

	public void RemoveHistory(string section_name)
	{
		history.RemoveSection(section_name);
	}

	public string GetCurrentSceneName()
	{
		GameSectionHierarchy.HierarchyData typed = hierarchy.GetTyped(GAME_SECTION_TYPE.SCENE);
		if (typed == null)
		{
			return string.Empty;
		}
		return typed.data.sectionName;
	}

	public string GetCurrentScreenName()
	{
		GameSectionHierarchy.HierarchyData typed = hierarchy.GetTyped(GAME_SECTION_TYPE.SCREEN);
		if (typed == null)
		{
			return string.Empty;
		}
		return typed.data.sectionName;
	}

	public string GetCurrentSectionName()
	{
		return hierarchy.GetLast()?.data.sectionName;
	}

	public string GetPrevSectionNameFromHistory()
	{
		return history.GetLast(2)?.sectionName;
	}

	public GAME_SECTION_TYPE GetCurrentSectionType()
	{
		return hierarchy.GetLast()?.data.type ?? GAME_SECTION_TYPE.NONE;
	}

	public string[] GetCurrentSectionTypeParams()
	{
		return hierarchy.GetLast()?.data.typeParams;
	}

	public List<GameSceneTables.TextData> GetCurrentSectionTextList()
	{
		GameSectionHierarchy.HierarchyData last = hierarchy.GetLast();
		if (last == null)
		{
			return null;
		}
		if (last.data.textList == null)
		{
			return emptyTextList;
		}
		return last.data.textList;
	}

	public void ClearHistory()
	{
		history.Clear();
	}

	public List<GameSectionHistory.HistoryData> GetHistoryList()
	{
		return history.GetHistoryList();
	}

	public List<GameSectionHierarchy.HierarchyData> GetHierarchyList()
	{
		return hierarchy.GetHierarchyList();
	}

	public static void StopForce()
	{
		Use_Force = !Use_Force;
	}

	public void AddHighForceChangeScene(string scene_name, string section_name = null, bool internal_res = false, UITransition.TYPE close_type = UITransition.TYPE.CLOSE, UITransition.TYPE open_type = UITransition.TYPE.OPEN, bool error = false, bool reloadSceneFlag = false)
	{
		AddForceScene(0, scene_name, section_name, internal_res, close_type, open_type, error, reloadSceneFlag);
	}

	public void AddNormalForceChangeScene(string scene_name, string section_name = null, bool internal_res = false, UITransition.TYPE close_type = UITransition.TYPE.CLOSE, UITransition.TYPE open_type = UITransition.TYPE.OPEN, bool error = false, bool reloadSceneFlag = false)
	{
		AddForceScene(10000, scene_name, section_name, internal_res, close_type, open_type, error, reloadSceneFlag);
	}

	public void AddLowForceChangeScene(string scene_name, string section_name = null, bool internal_res = false, UITransition.TYPE close_type = UITransition.TYPE.CLOSE, UITransition.TYPE open_type = UITransition.TYPE.OPEN, bool error = false, bool reloadSceneFlag = false)
	{
		AddForceScene(100000, scene_name, section_name, internal_res, close_type, open_type, error, reloadSceneFlag);
	}

	private void AddForceScene(int piority, string scene_name, string section_name = null, bool internal_res = false, UITransition.TYPE close_type = UITransition.TYPE.CLOSE, UITransition.TYPE open_type = UITransition.TYPE.OPEN, bool error = false, bool reloadSceneFlag = false)
	{
		if (Use_Force)
		{
			GameSceneTask item = new GameSceneTask(piority)
			{
				SceneName = scene_name,
				SectionName = section_name,
				Error = error,
				InternalRes = internal_res,
				CloseType = close_type,
				OpenType = open_type,
				ReloadSceneFlag = reloadSceneFlag
			};
			q_ForceTask.Enqueue(item);
		}
	}

	public void ChangeScene(string scene_name, string section_name = null, UITransition.TYPE close_type = UITransition.TYPE.CLOSE, UITransition.TYPE open_type = UITransition.TYPE.OPEN, bool error = false)
	{
		if (!AppMain.isApplicationQuit)
		{
			StartCoroutine(DoChangeScene(scene_name, section_name, error, ResourceManager.internalMode, close_type, open_type, reloadSceneFlag: false));
		}
	}

	private void ChangeCommonDialog(string scene_name, string section_name, bool error, bool internal_res)
	{
		StartCoroutine(DoChangeScene(scene_name, section_name, error, internal_res, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, reloadSceneFlag: false));
	}

	public void ReloadScene(UITransition.TYPE close_type = UITransition.TYPE.CLOSE, UITransition.TYPE open_type = UITransition.TYPE.OPEN, bool error = false)
	{
		string scene_name = GetCurrentSceneName().Replace("Scene", "");
		string section_name = null;
		StartCoroutine(DoChangeScene(scene_name, section_name, error, ResourceManager.internalMode, close_type, open_type, reloadSceneFlag: true));
	}

	public void ChangeSectionBack()
	{
		ChangeScene("[BACK]");
	}

	private IEnumerator DoChangeScene(string scene_name, string section_name, bool error, bool internal_res, UITransition.TYPE close_type, UITransition.TYPE open_type, bool reloadSceneFlag)
	{
		if (isChangeing && !error && (!this.isWaiting || commonDialogCallback == null))
		{
			Log.Error("Error DoChangeScene : scene={0} ,section={1}", scene_name, section_name);
			GameSceneEvent.request = null;
			yield break;
		}
		if (q_ForceTask.Count() > 0 && GetCurrentScreenName() != "InGameScene" && Use_Force)
		{
			GameSceneTask gameSceneTask = q_ForceTask.Dequeue();
			yield return DoChangeScene(gameSceneTask.SceneName, gameSceneTask.SectionName, gameSceneTask.Error, gameSceneTask.InternalRes, gameSceneTask.CloseType, gameSceneTask.OpenType, gameSceneTask.ReloadSceneFlag);
			yield break;
		}
		bool save_isChangeing = isChangeing;
		isChangeing = true;
		CrashlyticsReporter.SetSceneInfo(scene_name, section_name);
		CrashlyticsReporter.SetSceneStatus(isChangeing);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.SCENE_CHANGE, is_disable: true);
		if (commonDialogResult != null)
		{
			GameSectionHierarchy.HierarchyData dialog_hierarchy_data2 = hierarchy.GetLast();
			dialog_hierarchy_data2.section.Close(close_type);
			while (dialog_hierarchy_data2.section.state != 0)
			{
				yield return null;
			}
			hierarchy.DestroyHierarchy(dialog_hierarchy_data2);
			Action<string> action = commonDialogCallback;
			string obj = commonDialogResult;
			commonDialogResult = null;
			commonDialogCallback = null;
			isOpenImportantDialog = false;
			bool isWaiting = this.isWaiting;
			this.isWaiting = true;
			action?.Invoke(obj);
			this.isWaiting = isWaiting;
			GameSceneEvent.current = commonDialogSaveCurrentEvent;
			commonDialogSaveCurrentEvent = null;
			MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, null);
			if (!save_isChangeing)
			{
				MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.SCENE_CHANGE, is_disable: false);
			}
			isChangeing = save_isChangeing;
			CrashlyticsReporter.SetSceneStatus(isChangeing);
			yield break;
		}
		prev_scene_name = GetCurrentSceneName();
		string prev_section_name = GetCurrentSectionName();
		GameSectionHistory.HistoryData historyData = null;
		if (scene_name == "[BACK]")
		{
			history.PopSection();
			history.CutSingleDialog();
			historyData = history.GetLast();
		}
		if (historyData != null)
		{
			scene_name = historyData.sceneName;
			section_name = historyData.sectionName;
		}
		string scene_section_name;
		if (string.IsNullOrEmpty(scene_name))
		{
			scene_section_name = GetCurrentSceneName();
			GameSceneTables.SceneData sceneDataFromSectionName = tables.GetSceneDataFromSectionName(section_name);
			if (sceneDataFromSectionName != null)
			{
				scene_name = sceneDataFromSectionName.sceneName;
			}
			else
			{
				GameSectionHistory.HistoryData last = history.GetLast();
				scene_name = ((last == null) ? scene_section_name.Replace("Scene", "") : last.sceneName);
			}
		}
		else
		{
			scene_section_name = scene_name + "Scene";
		}
		if (scene_name != prev_scene_name)
		{
			bool isWait = false;
			if ((scene_name == "Home" || scene_name == "Clan") && MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
			{
				isWait = true;
				Protocol.Force(delegate
				{
					MonoBehaviourSingleton<LoungeMatchingManager>.I.SendLeave(delegate
					{
						isWait = false;
					});
				});
			}
			if ((scene_name == "Home" || scene_name == "Lounge") && MonoBehaviourSingleton<ClanMatchingManager>.I.IsInClan())
			{
				isWait = true;
				Protocol.Force(delegate
				{
					MonoBehaviourSingleton<ClanMatchingManager>.I.SendLeaveFromClanBase(delegate
					{
						isWait = false;
					});
				});
			}
			while (isWait)
			{
				yield return null;
			}
		}
		DoNotify(GameSection.NOTIFY_FLAG.PRETREAT_SCENE);
		GameSection new_scene_section = null;
		GameSection new_section = null;
		GameSceneTables.SectionData new_section_data = null;
		bool global_init_section = false;
		if (isOpenCommonDialog)
		{
			global_init_section = true;
		}
		LoadingQueue load_queue = new LoadingQueue(this);
		GameSceneTables.SceneData new_scene_data = tables.GetSceneData(scene_name, section_name);
		if (new_scene_data == null)
		{
			string text = scene_section_name + "Table";
			bool enableCache = ResourceManager.enableCache;
			bool internalMode = ResourceManager.internalMode;
			bool flag = internal_res;
			if (!flag && MonoBehaviourSingleton<ResourceManager>.I.manifest != null)
			{
				flag = true;
				if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && !AppMain.CheckApplicationVersion(MonoBehaviourSingleton<GlobalSettingsManager>.I.ignoreExternalSceneTableNamesAppVer))
				{
					List<string> useExternalSceneTableNames = MonoBehaviourSingleton<GlobalSettingsManager>.I.useExternalSceneTableNames;
					if (useExternalSceneTableNames != null)
					{
						int i = 0;
						for (int count = useExternalSceneTableNames.Count; i < count; i++)
						{
							if (useExternalSceneTableNames[i] == text)
							{
								flag = false;
								break;
							}
						}
					}
				}
			}
			ResourceManager.enableCache = false;
			ResourceManager.internalMode = flag;
			LoadObject lo_scene_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, text);
			ResourceManager.enableCache = enableCache;
			ResourceManager.internalMode = internalMode;
			while (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			if (lo_scene_table.loadedObject == null)
			{
				yield break;
			}
			new_scene_data = tables.CreateSceneData(scene_name, lo_scene_table.loadedObject as TextAsset);
		}
		GameSceneTables.SectionData new_scene_section_data = new_scene_data.GetSectionData(scene_section_name);
		GameSectionHierarchy.HierarchyData typed = hierarchy.GetTyped(GAME_SECTION_TYPE.SCENE);
		if (new_scene_section_data != null && ((typed == null || typed.data != new_scene_section_data) | reloadSceneFlag))
		{
			global.ChangeSection(new_scene_data, null);
			List<GameSectionHierarchy.HierarchyData> exclusive_list3 = isOpenImportantDialog ? new List<GameSectionHierarchy.HierarchyData>() : hierarchy.GetExclusiveList(GAME_SECTION_TYPE.SCENE);
			exclusive_list3.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
			{
				o.section.Close(close_type);
			});
			while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
			{
				yield return null;
			}
			if (!isAutoEventSkip)
			{
				if (!MonoBehaviourSingleton<TransitionManager>.I.isTransing && !isOpenCommonDialog)
				{
					TransitionManager.TYPE transitionType = global.GetTransitionType(prev_scene_name, prev_section_name, scene_section_name, section_name);
					if (transitionType != 0)
					{
						yield return MonoBehaviourSingleton<TransitionManager>.I.Out(transitionType);
					}
				}
				else
				{
					while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
					{
						yield return null;
					}
				}
			}
			bool save_isWaiting9 = this.isWaiting;
			this.isWaiting = true;
			exclusive_list3.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
			{
				o.section.Exit();
			});
			while (exclusive_list3.Find((GameSectionHierarchy.HierarchyData o) => !o.section.isExited) != null)
			{
				yield return null;
			}
			this.isWaiting = save_isWaiting9;
			hierarchy.DestroyHierarchy(exclusive_list3);
			if (global.SceneClear(prev_scene_name, prev_section_name, scene_section_name))
			{
				if (MonoBehaviourSingleton<InstantiateManager>.IsValid())
				{
					MonoBehaviourSingleton<InstantiateManager>.I.ClearStocks();
				}
				_ = (new_scene_section_data.sectionName == "InGameScene");
				yield return MonoBehaviourSingleton<AppMain>.I.ClearMemory(clearObjCaches: true, clearPreloaded: true);
			}
			save_isWaiting9 = this.isWaiting;
			this.isWaiting = true;
			global.SceneInitialize(prev_scene_name, scene_section_name, scene_name.Contains("TutorialWeaponSelect"));
			while (!global.isInitialized || IsBusy(error))
			{
				yield return null;
			}
			this.isWaiting = save_isWaiting9;
			bool internalMode2 = ResourceManager.internalMode;
			ResourceManager.internalMode = internal_res;
			LoadObject[] load_objs2 = new_scene_section_data.LoadUseResources(load_queue);
			ResourceManager.internalMode = internalMode2;
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			new_scene_section = hierarchy.CreateSection(new_scene_section_data, load_objs2);
			if (string.IsNullOrEmpty(section_name))
			{
				GameSceneTables.EventData eventData = new_scene_section_data.GetEventData("");
				if (eventData != null)
				{
					section_name = eventData.toSectionName;
				}
			}
		}
		else
		{
			scene_section_name = GetCurrentSceneName();
			if (string.IsNullOrEmpty(section_name))
			{
				if (new_scene_section_data == null)
				{
					section_name = scene_name + "Top";
				}
				if (typed.data == new_scene_section_data)
				{
					GameSectionHierarchy.HierarchyData last2 = hierarchy.GetLast();
					if (last2 != null && last2.data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
					{
						last2 = hierarchy.GetLastExcludeCommonDialog();
						if (last2 != null && last2.data.type != 0)
						{
							section_name = last2.data.sectionName;
						}
					}
				}
			}
		}
		if (!string.IsNullOrEmpty(section_name))
		{
			new_section_data = new_scene_data.GetSectionData(section_name);
			if (new_section_data == null)
			{
				Log.Error(LOG.GAMESCENE, "[ {0} ] is not found, in {1}", section_name, new_scene_data.sceneName);
				yield break;
			}
			global.ChangeSection(null, new_section_data);
			global.StageSetup(prev_scene_name, scene_section_name, section_name, new_section_data);
			GameSectionHierarchy.HierarchyData dialog_hierarchy_data2 = hierarchy.GetLast();
			GameSectionHierarchy.HierarchyData now_hierarchy_data = hierarchy.FindIgnoreSingle(new_section_data);
			if (now_hierarchy_data == null)
			{
				MonoBehaviourSingleton<UIManager>.I.UpdateMainUI(scene_section_name, section_name);
				List<GameSectionHierarchy.HierarchyData> exclusive_list3 = isOpenImportantDialog ? new List<GameSectionHierarchy.HierarchyData>() : hierarchy.GetExclusiveList(new_section_data.type);
				exclusive_list3.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
				{
					o.section.Close(close_type);
				});
				MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
				while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
				{
					yield return null;
				}
				if (!isAutoEventSkip)
				{
					if (!MonoBehaviourSingleton<TransitionManager>.I.isTransing && !new_section_data.type.IsDialog())
					{
						TransitionManager.TYPE transitionType2 = global.GetTransitionType(prev_scene_name, prev_section_name, scene_section_name, section_name);
						if (transitionType2 != 0)
						{
							yield return MonoBehaviourSingleton<TransitionManager>.I.Out(transitionType2);
						}
					}
					else
					{
						while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
						{
							yield return null;
						}
					}
				}
				if (dialog_hierarchy_data2 != null)
				{
					Send(set_wait_flag: true, dialog_hierarchy_data2.section, "OnChangePretreat", section_name + "@" + scene_name);
				}
				if (new_section_data.type == GAME_SECTION_TYPE.PAGE && !new_section_data.isTop)
				{
					List<GameSectionHierarchy.HierarchyData> list2 = new List<GameSectionHierarchy.HierarchyData>();
					exclusive_list3.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
					{
						if (o.data.type != GAME_SECTION_TYPE.PAGE)
						{
							list2.Add(o);
						}
					});
					exclusive_list3 = list2;
				}
				else if (new_section_data.type == GAME_SECTION_TYPE.PAGE_DIALOG)
				{
					List<GameSectionHierarchy.HierarchyData> list = new List<GameSectionHierarchy.HierarchyData>();
					exclusive_list3.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
					{
						if (o.data.type != GAME_SECTION_TYPE.PAGE_DIALOG)
						{
							list.Add(o);
						}
					});
					exclusive_list3 = list;
				}
				bool save_isWaiting9 = this.isWaiting;
				this.isWaiting = true;
				exclusive_list3.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
				{
					o.section.Exit();
				});
				while (exclusive_list3.Find((GameSectionHierarchy.HierarchyData o) => !o.section.isExited) != null)
				{
					yield return null;
				}
				this.isWaiting = save_isWaiting9;
				hierarchy.DestroyHierarchy(exclusive_list3);
				MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
				bool internalMode3 = ResourceManager.internalMode;
				ResourceManager.internalMode = internal_res;
				LoadObject[] load_objs2;
				if (new_section_data.type != GAME_SECTION_TYPE.COMMON_DIALOG)
				{
					load_objs2 = new_section_data.LoadUseResources(load_queue);
				}
				else
				{
					string commonResourceName = tables.GetCommonResourceName(new_section_data.typeParams[0]);
					load_objs2 = new LoadObject[1]
					{
						load_queue.Load(RESOURCE_CATEGORY.UI, commonResourceName)
					};
				}
				ResourceManager.internalMode = internalMode3;
				if (load_queue.IsLoading())
				{
					yield return load_queue.Wait();
				}
				while (IsBusy(error))
				{
					yield return null;
				}
				if (scene_name.Contains("TutorialWeaponSelect"))
				{
					while (MonoBehaviourSingleton<LoadingProcess>.IsValid())
					{
						yield return null;
					}
				}
				new_section = hierarchy.CreateSection(new_section_data, load_objs2);
			}
			else if (dialog_hierarchy_data2 != now_hierarchy_data)
			{
				List<GameSectionHierarchy.HierarchyData> exclusive_list3 = isOpenImportantDialog ? new List<GameSectionHierarchy.HierarchyData>() : hierarchy.GetCutList(now_hierarchy_data);
				exclusive_list3.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
				{
					o.section.Close(close_type);
				});
				MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
				while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
				{
					yield return null;
				}
				if (!isAutoEventSkip)
				{
					if (!MonoBehaviourSingleton<TransitionManager>.I.isTransing && !new_section_data.type.IsDialog())
					{
						GameSectionHierarchy.HierarchyData lastExcludeDialog = hierarchy.GetLastExcludeDialog();
						if ((dialog_hierarchy_data2.data.type == GAME_SECTION_TYPE.PAGE || (dialog_hierarchy_data2.data.type.IsDialog() && lastExcludeDialog != null && lastExcludeDialog.data.type == GAME_SECTION_TYPE.PAGE && lastExcludeDialog != now_hierarchy_data)) && (!dialog_hierarchy_data2.data.type.IsDialog() || !new_section_data.type.IsDialog()))
						{
							TransitionManager.TYPE transitionType3 = global.GetTransitionType(prev_scene_name, prev_section_name, scene_section_name, section_name);
							if (transitionType3 != 0)
							{
								yield return MonoBehaviourSingleton<TransitionManager>.I.Out(transitionType3);
							}
						}
					}
					else
					{
						while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
						{
							yield return null;
						}
					}
				}
				bool save_isWaiting9;
				if (!global_init_section)
				{
					save_isWaiting9 = this.isWaiting;
					this.isWaiting = true;
					global_init_section = true;
					global.SectionInitialize(scene_section_name, section_name, new_section_data);
					while (!global.isInitialized || IsBusy(error))
					{
						yield return null;
					}
					this.isWaiting = save_isWaiting9;
				}
				MonoBehaviourSingleton<UIManager>.I.UpdateMainUI(scene_section_name, section_name);
				if (now_hierarchy_data.section.state != UIBehaviour.STATE.OPEN)
				{
					save_isWaiting9 = this.isWaiting;
					this.isWaiting = true;
					now_hierarchy_data.section.isReOpenInitialized = false;
					now_hierarchy_data.section.InitializeReopen();
					while (!now_hierarchy_data.section.isReOpenInitialized || IsBusy(error))
					{
						yield return null;
					}
					this.isWaiting = save_isWaiting9;
					now_hierarchy_data.section.Open(open_type);
				}
				while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
				{
					yield return null;
				}
				save_isWaiting9 = this.isWaiting;
				this.isWaiting = true;
				exclusive_list3.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
				{
					o.section.Exit();
				});
				while (exclusive_list3.Find((GameSectionHierarchy.HierarchyData o) => !o.section.isExited) != null)
				{
					yield return null;
				}
				this.isWaiting = save_isWaiting9;
				hierarchy.DestroyHierarchy(exclusive_list3);
				MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
				if (dialog_hierarchy_data2 != null && dialog_hierarchy_data2.data.type.IsDialog())
				{
					Send(set_wait_flag: true, now_hierarchy_data.section, "OnCloseDialog", dialog_hierarchy_data2.data.sectionName);
					Send(set_wait_flag: true, now_hierarchy_data.section, "OnCloseDialog_" + dialog_hierarchy_data2.data.sectionName);
					while (IsBusy(error))
					{
						yield return null;
					}
				}
			}
		}
		if (new_section_data != null && !isOpenCommonDialog)
		{
			history.Push(scene_name, section_name, new_section_data.type);
		}
		if (!global_init_section)
		{
			bool save_isWaiting9 = this.isWaiting;
			this.isWaiting = true;
			global.SectionInitialize(scene_section_name, section_name, new_section_data);
			while (!global.isInitialized || IsBusy(error))
			{
				yield return null;
			}
			this.isWaiting = save_isWaiting9;
		}
		if (new_scene_section != null)
		{
			bool save_isWaiting9 = this.isWaiting;
			this.isWaiting = true;
			new_section.LoadRequireDataTable();
			while (!new_section.isLoadedRequireDataTable)
			{
				yield return null;
			}
			new_scene_section.Initialize();
			while (!new_scene_section.isInitialized || IsBusy(error))
			{
				yield return null;
			}
			this.isWaiting = save_isWaiting9;
			new_scene_section.Open(open_type);
			if (new_scene_section_data != null)
			{
				bool internalMode4 = ResourceManager.internalMode;
				ResourceManager.internalMode = internal_res;
				new_scene_section_data.LoadPreloadResources(load_queue);
				ResourceManager.internalMode = internalMode4;
			}
		}
		bool shouldPreOpenCamera = false;
		if (!save_isChangeing && new_scene_section != null && MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && MonoBehaviourSingleton<GlobalSettingsManager>.I.stageShouldPreOpenCamera.Contains(section_name))
		{
			shouldPreOpenCamera = true;
			global.SectionSetup(scene_section_name, section_name, new_section_data);
			if (!isAutoEventSkip && !isOpenCommonDialog && (save_isChangeing || !skipTrantisionEnd) && global.IsTransitionEnd(prev_scene_name, prev_section_name, scene_section_name, section_name))
			{
				MonoBehaviourSingleton<TransitionManager>.I.In();
			}
		}
		if (new_section != null)
		{
			bool save_isWaiting9 = this.isWaiting;
			this.isWaiting = true;
			new_section.LoadRequireDataTable();
			while (!new_section.isLoadedRequireDataTable)
			{
				yield return null;
			}
			new_section.Initialize();
			while (!new_section.isInitialized)
			{
				yield return null;
			}
			while (IsBusy(error))
			{
				yield return null;
			}
			this.isWaiting = save_isWaiting9;
			MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
			new_section.Open(open_type);
			if (new_section_data != null)
			{
				bool internalMode5 = ResourceManager.internalMode;
				ResourceManager.internalMode = internal_res;
				new_section_data.LoadPreloadResources(load_queue);
				ResourceManager.internalMode = internalMode5;
			}
		}
		if (!save_isChangeing)
		{
			DoNotify(GameSection.NOTIFY_FLAG.CHANGED_SCENE);
			MonoBehaviourSingleton<UIManager>.I.UpdateMainUI();
			if (MonoBehaviourSingleton<TransitionManager>.I.isTransing && MonoBehaviourSingleton<UIManager>.I.mainMenu != null)
			{
				while (MonoBehaviourSingleton<UIManager>.I.mainMenu.state == UIBehaviour.STATE.TO_CLOSE)
				{
					yield return null;
				}
			}
			if (!shouldPreOpenCamera)
			{
				global.SectionSetup(scene_section_name, section_name, new_section_data);
				if (!isAutoEventSkip && !isOpenCommonDialog && (save_isChangeing || !skipTrantisionEnd) && global.IsTransitionEnd(prev_scene_name, prev_section_name, scene_section_name, section_name))
				{
					yield return MonoBehaviourSingleton<TransitionManager>.I.In();
				}
			}
			while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
			{
				yield return null;
			}
			MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.SCENE_CHANGE, is_disable: false);
		}
		global.SectionStart(scene_section_name, section_name, new_section != null);
		MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
		isChangeing = save_isChangeing;
		CrashlyticsReporter.SetSceneStatus(isChangeing);
		if (new_section != null)
		{
			new_section.StartSection();
		}
		if (!save_isChangeing && skipTrantisionEnd)
		{
			skipTrantisionEnd = false;
			if (!isChangeing)
			{
				MonoBehaviourSingleton<TransitionManager>.I.In();
			}
		}
		if (GameSceneEvent.request != null)
		{
			ExecuteSceneEvent("REQUEST", base.gameObject, GameSceneEvent.request.eventName, GameSceneEvent.request.userData);
			GameSceneEvent.request = null;
		}
	}

	private bool IsBusy()
	{
		return Protocol.isBusy;
	}

	private bool IsBusy(bool important)
	{
		if (!important)
		{
			return IsBusy();
		}
		return false;
	}

	public void ExecuteSceneEvent(string caller, GameObject sender, string event_name, object user_data = null, string check_app_ver = null, bool is_send_query = true)
	{
		GameSectionHierarchy.HierarchyData last = hierarchy.GetLast();
		if (last == null)
		{
			return;
		}
		UIBehaviour uIBehaviour = null;
		string text;
		bool flag;
		if (sender == base.gameObject)
		{
			uIBehaviour = null;
			text = string.Empty;
			flag = true;
		}
		else
		{
			if (sender != null)
			{
				try
				{
					uIBehaviour = sender.GetComponentInParent<UIBehaviour>();
				}
				catch (Exception ex)
				{
					Log.Warning(LOG.SYSTEM, ex.ToString());
					uIBehaviour = null;
				}
			}
			text = ((uIBehaviour != null) ? uIBehaviour.name : string.Empty);
			flag = false;
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.tutorialMessage != null)
		{
			if (MonoBehaviourSingleton<UIManager>.I.tutorialMessage.IsEnableMessage())
			{
				MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SubmitCursor(text, event_name);
			}
			string currentSceneName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName();
			string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.TriggerRun(currentSceneName, currentSectionName, event_name);
		}
		if (string.IsNullOrEmpty(caller))
		{
			Log.Error(LOG.GAMESCENE, "caller is empty.");
			return;
		}
		if (isChangeing && !isOpenImportantDialog)
		{
			if (sender != null)
			{
				Log.Warning(LOG.GAMESCENE, "during scene change, so an event is ignored. {0}", event_name);
			}
			return;
		}
		if (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
		{
			Log.Warning(LOG.GAMESCENE, "during UI transitioning, so an event is ignored. {0}", event_name);
			return;
		}
		if (IsBusy(isOpenImportantDialog))
		{
			if (!Protocol.strict && doWaitEventCount == 0)
			{
				StartCoroutine(DoWaitEvent(caller, sender, event_name, user_data, check_app_ver, is_send_query));
			}
			else
			{
				Log.Warning(LOG.GAMESCENE, "protocol is busy, so an event is ignored. {0}", event_name);
			}
			return;
		}
		if (GameSceneEvent.IsStay())
		{
			Log.Warning(LOG.GAMESCENE, "now staying, so an event is ignored. {0}", event_name);
			return;
		}
		GameSection gameSection = null;
		if (sender != null)
		{
			gameSection = sender.GetComponentInParent<GameSection>();
		}
		if (gameSection != null && !gameSection.isInitialized)
		{
			Log.Warning(LOG.GAMESCENE, "It's initialized, so an event is ignored. {0}", event_name);
		}
		else
		{
			if (gameSection != null && last.data.type.IsDialog() && gameSection != last.section && !GameSceneGlobalSettings.IsGlobalEvent(event_name))
			{
				return;
			}
			GameSceneEvent.request = null;
			GameSceneEvent.current.eventName = event_name;
			GameSceneEvent.current.isExecute = true;
			GameSceneEvent.current.sender = sender;
			GameSceneEvent.current.userData = user_data;
			bool flag2 = event_name == "[BACK]";
			GameSceneTables.EventData eventData = null;
			eventData = ((!flag2) ? last.data.GetEventData(GameSceneEvent.current.eventName) : last.data.GetEventData("SECTION_BACK"));
			if (eventData != null)
			{
				bool flag3 = true;
				if (!AppMain.CheckApplicationVersion(eventData.appVer))
				{
					flag3 = false;
				}
				if (flag3 && !string.IsNullOrEmpty(check_app_ver) && !AppMain.CheckApplicationVersion(check_app_ver))
				{
					flag3 = false;
				}
				if (!flag3)
				{
					if (IsExecutionAutoEvent())
					{
						event_name = (GameSceneEvent.current.eventName = "RecommendedVersionCheck");
						isOpenImportantDialog = true;
					}
					else
					{
						event_name = (GameSceneEvent.current.eventName = "APP_VERSION_RESTRICTION");
					}
					GameSceneEvent.current.isExecute = false;
				}
			}
			if (is_send_query)
			{
				bool isCallingOnQuery = this.isCallingOnQuery;
				this.isCallingOnQuery = true;
				if (!flag2)
				{
					if (last.data.type == GAME_SECTION_TYPE.COMMON_DIALOG && (text == last.data.sectionName || flag))
					{
						if (commonDialogCallback == null)
						{
							GameSectionHierarchy.HierarchyData lastExcludeCommonDialog = hierarchy.GetLastExcludeCommonDialog();
							if (lastExcludeCommonDialog != null)
							{
								Send(set_wait_flag: false, lastExcludeCommonDialog.section, $"OnQuery_{last.data.sectionName}_{event_name}");
							}
						}
					}
					else
					{
						Send(set_wait_flag: false, last.section, $"OnQuery_{event_name}");
					}
				}
				else if (last.data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
				{
					if (commonDialogCallback == null)
					{
						GameSectionHierarchy.HierarchyData lastExcludeCommonDialog2 = hierarchy.GetLastExcludeCommonDialog();
						if (lastExcludeCommonDialog2 != null)
						{
							Send(set_wait_flag: false, lastExcludeCommonDialog2.section, $"OnQuery_{last.data.sectionName}_SECTION_BACK");
						}
					}
				}
				else
				{
					Send(set_wait_flag: false, last.section, "OnQuery_SECTION_BACK");
				}
				this.isCallingOnQuery = isCallingOnQuery;
			}
			if (!GameSceneEvent.current.isExecute)
			{
				return;
			}
			GameSceneEvent.current.isExecute = false;
			flag2 = (GameSceneEvent.current.eventName == "[BACK]");
			bool error = false;
			if (commonDialogCallback != null)
			{
				commonDialogResult = GameSceneEvent.current.eventName;
				if (isOpenImportantDialog)
				{
					error = true;
				}
			}
			string text2 = null;
			string text3 = null;
			UITransition.TYPE close_type = UITransition.TYPE.CLOSE;
			UITransition.TYPE open_type = UITransition.TYPE.OPEN;
			if (flag2)
			{
				eventData = last.data.GetEventData("SECTION_BACK");
				if (eventData == null)
				{
					text2 = GameSceneEvent.current.eventName;
				}
			}
			else
			{
				eventData = last.data.GetEventData(GameSceneEvent.current.eventName);
			}
			if (eventData != null)
			{
				close_type = eventData.closeType;
				open_type = eventData.openType;
				string toSectionName = eventData.toSectionName;
				if (toSectionName.Length > 0)
				{
					if (toSectionName.StartsWith("[HISTORY_"))
					{
						int i = int.Parse(toSectionName.Substring(9, toSectionName.IndexOf(']') - 9));
						GameSectionHistory.HistoryData last2 = history.GetLast(i, ignore_common_dialog: true);
						if (last2 != null)
						{
							text2 = last2.sceneName;
							text3 = last2.sectionName;
						}
					}
					else
					{
						int num = toSectionName.IndexOf("@");
						switch (num)
						{
						case -1:
							text3 = toSectionName;
							break;
						case 0:
							text2 = toSectionName.Substring(1);
							break;
						default:
							text3 = toSectionName.Substring(0, num);
							text2 = toSectionName.Substring(num + 1);
							break;
						}
						if (LoungeMatchingManager.IsValidInLounge())
						{
							if (text2 == "Home")
							{
								text2 = "Lounge";
							}
							if (text3 == "HomeTop")
							{
								text3 = "LoungeTop";
							}
						}
						else if (ClanMatchingManager.IsValidInClan() || MonoBehaviourSingleton<ClanManager>.IsValid())
						{
							if (text2 == "Home")
							{
								text2 = "Clan";
							}
							if (text3 == "HomeTop")
							{
								text3 = "ClanTop";
							}
						}
					}
				}
			}
			if (text2 == null && text3 == null && (commonDialogCallback != null || (last.data.type == GAME_SECTION_TYPE.COMMON_DIALOG && (text == last.data.sectionName || flag))))
			{
				text2 = "[BACK]";
			}
			if (text2 != null || text3 != null)
			{
				ChangeScene(text2, text3, close_type, open_type, error);
			}
			else
			{
				GameSceneEvent.request = null;
			}
		}
	}

	public bool IsEventExecutionPossible()
	{
		if (MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			return false;
		}
		if (GameSceneEvent.IsStay())
		{
			return false;
		}
		if (Protocol.isBusy)
		{
			return false;
		}
		if (isOpenCommonDialog)
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
		{
			return false;
		}
		return true;
	}

	public bool IsBackKeyEventExecutionPossible()
	{
		if (isOpenCommonDialog)
		{
			return true;
		}
		return IsEventExecutionPossible();
	}

	private IEnumerator DoWaitEvent(string caller, GameObject sender, string event_name, object user_data, string check_app_ver, bool is_send_query)
	{
		doWaitEventCount++;
		while (true)
		{
			yield return null;
			if (Protocol.strict)
			{
				break;
			}
			if (IsEventExecutionPossible())
			{
				doWaitEventCount--;
				ExecuteSceneEvent(caller, sender, event_name, user_data, check_app_ver, is_send_query);
				yield break;
			}
		}
		doWaitEventCount--;
	}

	public bool IsExecutionAutoEvent()
	{
		return autoEvents != null;
	}

	public void SetAutoEvents(EventData[] event_datas)
	{
		if (event_datas == null || autoEvents != null)
		{
			if (event_datas == null)
			{
				Log.Error(LOG.GAMESCENE, "event_datas == null");
			}
			else
			{
				Log.Error(LOG.GAMESCENE, "autoEvents != null");
			}
			return;
		}
		autoEvents = event_datas;
		if (MonoBehaviourSingleton<UIManager>.I.tutorialMessage != null)
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SetSkipSectionRunCount(autoEvents.Length - 1);
		}
		StartCoroutine(DoAutoEvent());
	}

	public void StopAutoEvent(Action on_finished = null)
	{
		if (autoEvents == null)
		{
			on_finished?.Invoke();
			return;
		}
		autoEvents = null;
		onAutoEventFinished = on_finished;
	}

	private IEnumerator DoAutoEvent()
	{
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.AUTO_EVENT, is_disable: true);
		if (!isAutoEventSkip)
		{
			MonoBehaviourSingleton<OutGameEffectManager>.I.ShowAutoEventEffect();
		}
		int index = 0;
		bool is_change_version_check_section = false;
		while (autoEvents != null)
		{
			yield return null;
			if (notifyFlags != 0L || isChangeing || isOpenCommonDialog || GameSceneEvent.IsStay() || (MonoBehaviourSingleton<UIManager>.I.disableFlags & UIManager.DISABLE_FACTOR.AUTO_EVENT) != UIManager.DISABLE_FACTOR.AUTO_EVENT)
			{
				continue;
			}
			if (autoEvents != null && index >= autoEvents.Length)
			{
				break;
			}
			if (isAutoEventSkip && !MonoBehaviourSingleton<TransitionManager>.I.isTransing)
			{
				yield return MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.AUTO_EVENT);
			}
			EventData event_data = (autoEvents != null) ? autoEvents[index] : null;
			if (event_data != null)
			{
				EventData eventData = GetCurrentSection().CheckAutoEvent(event_data.name, event_data.data);
				if (eventData != null)
				{
					event_data = eventData;
				}
			}
			bool is_execute_event = event_data != null && !string.IsNullOrEmpty(event_data.name);
			if (!isAutoEventSkip && is_execute_event && MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
			{
				UIGameSceneEventSender sender = null;
				Utility.ForEach(MonoBehaviourSingleton<UIManager>.I._transform, delegate(Transform t)
				{
					UIGameSceneEventSender component2 = t.GetComponent<UIGameSceneEventSender>();
					if (component2 == null)
					{
						return false;
					}
					if (component2.eventName != event_data.name)
					{
						return false;
					}
					if (component2.eventData != null && event_data.data != null && !component2.eventData.Equals(event_data.data))
					{
						return false;
					}
					sender = component2;
					return true;
				});
				if (sender != null)
				{
					UIButton button = sender.GetComponent<UIButton>();
					if (button != null)
					{
						UIScrollView componentInParent = button.GetComponentInParent<UIScrollView>();
						if (componentInParent != null && componentInParent.enabled)
						{
							UIPanel component = componentInParent.GetComponent<UIPanel>();
							if (!component.IsVisible(button.GetComponent<UIWidget>()))
							{
								Vector3 pos2 = -component.cachedTransform.InverseTransformPoint(button.transform.position);
								if (!componentInParent.canMoveHorizontally)
								{
									pos2.x = component.cachedTransform.localPosition.x;
								}
								if (!componentInParent.canMoveVertically)
								{
									pos2.y = component.cachedTransform.localPosition.y;
								}
								SpringPanel sp = SpringPanel.Begin(component.cachedGameObject, pos2, 16f);
								bool wait = true;
								SpringPanel.OnFinished func = delegate
								{
									wait = false;
								};
								sp.onFinished = (SpringPanel.OnFinished)Delegate.Combine(sp.onFinished, func);
								while (wait)
								{
									yield return null;
								}
								sp.onFinished = (SpringPanel.OnFinished)Delegate.Remove(sp.onFinished, func);
							}
						}
						Vector3 pos = button.transform.position;
						yield return MonoBehaviourSingleton<OutGameEffectManager>.I.MoveAutoEventEffect(pos);
						button.SetState(UIButtonColor.State.Pressed, immediate: false);
						MonoBehaviourSingleton<OutGameEffectManager>.I.PopTouchEffect(pos);
						yield return new WaitForSeconds(0.2f);
						button.SetState(UIButtonColor.State.Normal, immediate: false);
					}
				}
			}
			if (is_execute_event)
			{
				if (event_data.data != null && event_data.data.GetType() == typeof(EventListData))
				{
					if (((Network.EventData)event_data.data).eventType == 15)
					{
						if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 50)
						{
							MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
							break;
						}
						ExecuteSceneEvent("AUTO", base.gameObject, event_data.name + "_ARENA", event_data.data);
					}
					else
					{
						ExecuteSceneEvent("AUTO", base.gameObject, event_data.name, event_data.data);
					}
				}
				else
				{
					ExecuteSceneEvent("AUTO", base.gameObject, event_data.name, event_data.data);
				}
				if (GameSceneEvent.current.eventName == "RecommendedVersionCheck")
				{
					Array.Resize(ref autoEvents, 1);
					autoEvents[0].name = "APP_VERSION_RESTRICTION_AUTO";
					autoEvents[0].data = 0;
					index = -1;
					is_change_version_check_section = true;
				}
			}
			index++;
			if ((autoEvents == null || index >= autoEvents.Length) && MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
			{
				MonoBehaviourSingleton<OutGameEffectManager>.I.HideAutoEventEffect();
			}
		}
		if (!is_change_version_check_section && MonoBehaviourSingleton<TransitionManager>.I.isTransing)
		{
			yield return MonoBehaviourSingleton<TransitionManager>.I.In();
		}
		if (!isAutoEventSkip)
		{
			MonoBehaviourSingleton<OutGameEffectManager>.I.HideAutoEventEffect();
		}
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.AUTO_EVENT, is_disable: false);
		autoEvents = null;
		if (onAutoEventFinished != null)
		{
			onAutoEventFinished();
			onAutoEventFinished = null;
		}
	}

	public void OpenCommonDialog(CommonDialog.Desc desc, Action<string> callback, bool error = false, int errorCode = 0)
	{
		OpenCommonDialog_(desc, callback, error, internal_res: true, errorCode);
	}

	private void OpenCommonDialog_(CommonDialog.Desc desc, Action<string> callback, bool error, bool internal_res, int errorCode = 0)
	{
		if (callback == null)
		{
			return;
		}
		if (isOpenCommonDialog)
		{
			StartCoroutine(DoWaitOpenCommonDialog(desc, callback, error, internal_res, errorCode));
			return;
		}
		if (isChangeing && !error)
		{
			Log.Error(LOG.GAMESCENE, "during scene change. error={0} isWaiting={1}", error, isWaiting);
			return;
		}
		commonDialogSaveCurrentEvent = GameSceneEvent.current;
		commonDialogCallback = callback;
		GameSceneEvent.current = new GameSceneEvent();
		GameSceneEvent.current.userData = desc;
		isOpenImportantDialog = error;
		if (errorCode == 1020 || errorCode == 1023)
		{
			ChangeCommonDialog("CommonDialog", "CommonDialogTop", error, internal_res);
		}
		else if (errorCode == 1002)
		{
			ChangeCommonDialog("CommonDialog", "CommonDialogMaintenanceError", error, internal_res);
		}
		else if (errorCode == 70800)
		{
			ChangeCommonDialog("CommonDialog", "YesNoDialogImportant", error, internal_res);
		}
		else if (errorCode > 500000 && errorCode < 600000)
		{
			ChangeCommonDialog("CommonDialog", error ? "CustomDialogError" : "CommonDialogTop", error, internal_res);
		}
		else if (errorCode > 600000 && errorCode < 700000)
		{
			ChangeCommonDialog("CommonDialog", error ? "CustomDialogError" : "CommonDialogTop", error, internal_res);
		}
		else
		{
			ChangeCommonDialog("CommonDialog", error ? "CommonDialogError" : "CommonDialogTop", error, internal_res);
		}
	}

	private IEnumerator DoWaitOpenCommonDialog(CommonDialog.Desc desc, Action<string> callback, bool error, bool internal_res, int errorCode = 0)
	{
		while (isOpenCommonDialog)
		{
			yield return null;
		}
		OpenCommonDialog_(desc, callback, error, internal_res, errorCode);
	}

	public void OpenInfoDialog(Action<string> callback, bool error = false)
	{
		commonDialogSaveCurrentEvent = GameSceneEvent.current;
		commonDialogCallback = callback;
		isOpenImportantDialog = error;
		ChangeCommonDialog("CommonDialog", "InformationDialog", error: true, ResourceManager.internalMode);
	}

	public void OpenUpdateAppDialog(uint msg_id, bool is_yes_no, Action onCancel = null)
	{
		GameSceneEvent.PushStay();
		OpenCommonDialog(new CommonDialog.Desc(is_yes_no ? CommonDialog.TYPE.YES_NO : CommonDialog.TYPE.OK, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, msg_id), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102u)), delegate(string ret)
		{
			GameSceneEvent.PopStay();
			if (ret == "YES" || ret == "OK")
			{
				Native.launchMyselfMarket();
				MonoBehaviourSingleton<AppMain>.I.Reset();
			}
			else if (onCancel != null)
			{
				onCancel();
			}
		}, error: true);
	}

	public bool CheckPortalAndOpenUpdateAppDialog(uint portal_id, bool check_dst_quest, bool is_yes_no = true)
	{
		if (portal_id == 0)
		{
			return true;
		}
		return CheckPortalAndOpenUpdateAppDialog(Singleton<FieldMapTable>.I.GetPortalData(portal_id), check_dst_quest, is_yes_no);
	}

	public bool CheckPortalAndOpenUpdateAppDialog(FieldMapTable.PortalTableData portal_data, bool check_dst_quest, bool is_yes_no = true)
	{
		if (portal_data == null)
		{
			return true;
		}
		if (check_dst_quest && portal_data.dstQuestID != 0 && !CheckQuestAndOpenUpdateAppDialog(portal_data.dstQuestID, is_yes_no))
		{
			return false;
		}
		if (portal_data.dstMapID != 0 && !CheckMapAndOpenUpdateAppDialog(portal_data.dstMapID))
		{
			return false;
		}
		return true;
	}

	public bool CheckMapAndOpenUpdateAppDialog(uint map_id, bool is_yes_no = true)
	{
		if (map_id == 0)
		{
			return true;
		}
		List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(map_id);
		if (enemyPopList != null)
		{
			int i = 0;
			for (int count = enemyPopList.Count; i < count; i++)
			{
				if (enemyPopList[i] != null && enemyPopList[i].enemyID != 0)
				{
					EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemyPopList[i].enemyID);
					if (enemyData != null && !enemyData.IsEnableNowApplicationVersion())
					{
						OpenUpdateAppDialog(2003u, is_yes_no);
						return false;
					}
				}
			}
		}
		return true;
	}

	public bool CheckQuestAndOpenUpdateAppDialog(uint quest_id, bool is_yes_no = true)
	{
		if (quest_id == 0)
		{
			return true;
		}
		return CheckQuestAndOpenUpdateAppDialog(Singleton<QuestTable>.I.GetQuestData(quest_id), is_yes_no);
	}

	public bool CheckQuestAndOpenUpdateAppDialog(QuestTable.QuestTableData quest_data, bool is_yes_no = true, bool is_happen_quest = false)
	{
		if (quest_data != null)
		{
			int i = 0;
			for (int num = quest_data.enemyID.Length; i < num; i++)
			{
				if (quest_data.enemyID[i] != 0)
				{
					EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)quest_data.enemyID[i]);
					if (enemyData != null && !enemyData.IsEnableNowApplicationVersion())
					{
						OpenUpdateAppDialog(is_happen_quest ? 2002u : 2003u, is_yes_no, delegate
						{
							GameSceneEvent.Cancel();
						});
						return false;
					}
				}
			}
		}
		return true;
	}

	public bool CheckEquipItemAndOpenUpdateAppDialog(uint equip_item_id, Action onCancel = null)
	{
		if (equip_item_id == 0)
		{
			return true;
		}
		return CheckEquipItemAndOpenUpdateAppDialog(Singleton<EquipItemTable>.I.GetEquipItemData(equip_item_id));
	}

	public bool CheckEquipItemAndOpenUpdateAppDialog(EquipItemTable.EquipItemData equip_item_data, Action onCancel = null)
	{
		if (equip_item_data == null)
		{
			return true;
		}
		if (!equip_item_data.IsEnableNowApplicationVersion())
		{
			OpenUpdateAppDialog(2000u, is_yes_no: true, onCancel);
			return false;
		}
		return true;
	}

	public bool CheckSkillItemAndOpenUpdateAppDialog(uint skill_item_id, Action onCancel = null)
	{
		if (skill_item_id == 0)
		{
			return true;
		}
		return CheckSkillItemAndOpenUpdateAppDialog(Singleton<SkillItemTable>.I.GetSkillItemData(skill_item_id));
	}

	public bool CheckSkillItemAndOpenUpdateAppDialog(SkillItemTable.SkillItemData skill_item_data, Action onCancel = null)
	{
		if (skill_item_data == null)
		{
			return true;
		}
		if (!skill_item_data.IsEnableNowApplicationVersion())
		{
			OpenUpdateAppDialog(2001u, is_yes_no: true, onCancel);
			return false;
		}
		return true;
	}

	[Obsolete]
	public bool CheckEquipAbilityAndOpenUpdateAppDialog(EquipItemInfo equipItemInfo, Action onCancel = null)
	{
		return true;
	}

	public void OpinionBox()
	{
		StartCoroutine(DoOpenOpinionBox());
	}

	private IEnumerator DoOpenOpinionBox()
	{
		while (notifyFlags != 0L || isChangeing || isOpenCommonDialog || GameSceneEvent.IsStay())
		{
			yield return null;
		}
		ChangeScene("OpinionBox", "OpinionTop");
	}

	public bool IsCurrentSceneHomeOrLounge()
	{
		if (!(GetCurrentSceneName() == "HomeScene") && !(GetCurrentSceneName() == "LoungeScene") && !(GetCurrentSceneName() == "ClanScene"))
		{
			return GetCurrentSceneName() == "GuildScene";
		}
		return true;
	}

	public bool IsCurrentSceneMejorOutGameScene()
	{
		if (!(GetCurrentSceneName() == "HomeScene") && !(GetCurrentSceneName() == "LoungeScene") && !(GetCurrentSceneName() == "ClanScene") && !(GetCurrentSceneName() == "StatusScene"))
		{
			return GetCurrentSceneName() == "ShopScene";
		}
		return true;
	}

	public void SetExternalStageName(string stage_name)
	{
		global.externalStageName = stage_name;
	}

	public void SetMainCameraCullingMask(int mask)
	{
		global.SetMainCameraCullingMask(mask);
	}

	public bool isAvailableScreenRotationScene()
	{
		if (global == null)
		{
			return false;
		}
		return global.isAvailableScreenRotation(GetCurrentSceneName(), GetCurrentSectionName());
	}
}
