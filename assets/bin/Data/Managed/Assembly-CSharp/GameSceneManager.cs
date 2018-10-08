using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviourSingleton<GameSceneManager>
{
	public const string STR_EVENT_BACK = "[BACK]";

	private const string STR_EVENT_VERSION_RESTRICTION = "APP_VERSION_RESTRICTION";

	private const string STR_EVENT_VERSION_RESTRICTION_AUTO = "APP_VERSION_RESTRICTION_AUTO";

	private const string STR_RECOMMENDED_VERSION_CEHCK_EVENT = "RecommendedVersionCheck";

	private static bool isAutoEventTeleportMode = true;

	private GameSceneTables tables;

	private GameSectionHistory history;

	private GameSectionHierarchy hierarchy;

	private GameSceneGlobalSettings global;

	private GameSection.NOTIFY_FLAG notifyFlags;

	private IEnumerator notifyCoroutine;

	private int downloadErrorResult;

	private static readonly List<GameSceneTables.TextData> emptyTextList = new List<GameSceneTables.TextData>();

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
		Object.DontDestroyOnLoad(this);
		GameSceneEvent.Initialize();
		MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += global.OnScreenRotate;
	}

	public void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		isInitialized = false;
		tables = new GameSceneTables();
		LoadingQueue load_queue = new LoadingQueue(this);
		ResourceManager.enableCache = false;
		LoadObject lo_common_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "CommonDialogTable", false);
		ResourceManager.enableCache = true;
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		tables.CreateCommonResourceTable(lo_common_table.loadedObject as TextAsset);
		isInitialized = true;
	}

	private unsafe void OnEnable()
	{
		MonoBehaviourSingleton<ResourceManager>.I.onDownloadErrorQuery = new Func<bool, Error, int>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
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
			OpenCommonDialog_(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage((uint)error_code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111u), null, null), delegate(string btn)
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
			}, true, true, 0);
		}
		return downloadErrorResult;
	}

	public virtual void SetNotify(GameSection.NOTIFY_FLAG flag)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		notifyFlags |= flag;
		if (notifyCoroutine == null)
		{
			this.StartCoroutine(notifyCoroutine = DoNotifyUpdate());
		}
	}

	private IEnumerator DoNotifyUpdate()
	{
		while (notifyFlags != (GameSection.NOTIFY_FLAG)0L)
		{
			yield return (object)null;
			if (notifyFlags != (GameSection.NOTIFY_FLAG)0L && !GameSceneEvent.IsStay() && !Protocol.isBusy && !isWaiting)
			{
				bool save_isWaiting = isWaiting;
				isWaiting = true;
				MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.NOTIFY, true);
				try
				{
					DoNotify(notifyFlags);
				}
				catch (Exception ex)
				{
					Exception e = ex;
					Log.Exception(e);
				}
				notifyFlags = (GameSection.NOTIFY_FLAG)0L;
				while (Protocol.isBusy)
				{
					yield return (object)null;
				}
				MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.NOTIFY, false);
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
				target.SendMessage(func, 1);
			}
			else
			{
				target.SendMessage(func, param, 1);
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
		return history.GetLast(2, false)?.sectionName;
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

	public void ChangeScene(string scene_name, string section_name = null, UITransition.TYPE close_type = UITransition.TYPE.CLOSE, UITransition.TYPE open_type = UITransition.TYPE.OPEN, bool error = false)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (!AppMain.isApplicationQuit)
		{
			this.StartCoroutine(DoChangeScene(scene_name, section_name, error, ResourceManager.internalMode, close_type, open_type, false));
		}
	}

	private void ChangeCommonDialog(string scene_name, string section_name, bool error, bool internal_res)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoChangeScene(scene_name, section_name, error, internal_res, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false));
	}

	public void ReloadScene(UITransition.TYPE close_type = UITransition.TYPE.CLOSE, UITransition.TYPE open_type = UITransition.TYPE.OPEN, bool error = false)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		string scene_name = GetCurrentSceneName().Replace("Scene", string.Empty);
		string section_name = null;
		this.StartCoroutine(DoChangeScene(scene_name, section_name, error, ResourceManager.internalMode, close_type, open_type, true));
	}

	public void ChangeSectionBack()
	{
		ChangeScene("[BACK]", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}

	private IEnumerator DoChangeScene(string scene_name, string section_name, bool error, bool internal_res, UITransition.TYPE close_type, UITransition.TYPE open_type, bool reloadSceneFlag)
	{
		if (isChangeing && !error && (!isWaiting || commonDialogCallback == null))
		{
			Log.Error("Error DoChangeScene : scene={0} ,section={1}", scene_name, section_name);
			GameSceneEvent.request = null;
		}
		else
		{
			bool save_isChangeing = isChangeing;
			isChangeing = true;
			CrashlyticsReporter.SetSceneInfo(scene_name, section_name);
			CrashlyticsReporter.SetSceneStatus(isChangeing);
			MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.SCENE_CHANGE, true);
			if (commonDialogResult != null)
			{
				GameSectionHierarchy.HierarchyData dialog_hierarchy_data = hierarchy.GetLast();
				dialog_hierarchy_data.section.Close(close_type);
				while (dialog_hierarchy_data.section.state != 0)
				{
					yield return (object)null;
				}
				hierarchy.DestroyHierarchy(dialog_hierarchy_data);
				Action<string> dialog_callback = commonDialogCallback;
				string dialog_result = commonDialogResult;
				commonDialogResult = null;
				commonDialogCallback = null;
				isOpenImportantDialog = false;
				bool save_isWaiting2 = isWaiting;
				isWaiting = true;
				dialog_callback?.Invoke(dialog_result);
				isWaiting = save_isWaiting2;
				GameSceneEvent.current = commonDialogSaveCurrentEvent;
				commonDialogSaveCurrentEvent = null;
				MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, null);
				if (!save_isChangeing)
				{
					MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.SCENE_CHANGE, false);
				}
				isChangeing = save_isChangeing;
				CrashlyticsReporter.SetSceneStatus(isChangeing);
			}
			else
			{
				prev_scene_name = GetCurrentSceneName();
				string prev_section_name = GetCurrentSectionName();
				GameSectionHistory.HistoryData back_history_data = null;
				if (scene_name == "[BACK]")
				{
					history.PopSection();
					history.CutSingleDialog();
					back_history_data = history.GetLast();
				}
				if (back_history_data != null)
				{
					scene_name = back_history_data.sceneName;
					section_name = back_history_data.sectionName;
				}
				string scene_section_name;
				if (string.IsNullOrEmpty(scene_name))
				{
					scene_section_name = GetCurrentSceneName();
					GameSceneTables.SceneData scene_data = tables.GetSceneDataFromSectionName(section_name);
					if (scene_data != (GameSceneTables.SceneData)null)
					{
						scene_name = scene_data.sceneName;
					}
					else
					{
						GameSectionHistory.HistoryData last_history_data = history.GetLast();
						scene_name = ((last_history_data == null) ? scene_section_name.Replace("Scene", string.Empty) : last_history_data.sceneName);
					}
				}
				else
				{
					scene_section_name = scene_name + "Scene";
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
				if (new_scene_data == (GameSceneTables.SceneData)null)
				{
					string load_scene_table_name = scene_section_name + "Table";
					bool save_enable_cache = ResourceManager.enableCache;
					bool save_internal_mode5 = ResourceManager.internalMode;
					bool internal_mode = internal_res;
					if (!internal_mode && MonoBehaviourSingleton<ResourceManager>.I.manifest != null)
					{
						internal_mode = true;
						if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && !AppMain.CheckApplicationVersion(MonoBehaviourSingleton<GlobalSettingsManager>.I.ignoreExternalSceneTableNamesAppVer))
						{
							List<string> table_names = MonoBehaviourSingleton<GlobalSettingsManager>.I.useExternalSceneTableNames;
							if (table_names != null)
							{
								int j = 0;
								for (int i = table_names.Count; j < i; j++)
								{
									if (table_names[j] == load_scene_table_name)
									{
										internal_mode = false;
										break;
									}
								}
							}
						}
					}
					ResourceManager.enableCache = false;
					ResourceManager.internalMode = internal_mode;
					LoadObject lo_scene_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, load_scene_table_name, false);
					ResourceManager.enableCache = save_enable_cache;
					ResourceManager.internalMode = save_internal_mode5;
					yield return (object)load_queue.Wait();
					new_scene_data = tables.CreateSceneData(scene_name, lo_scene_table.loadedObject as TextAsset);
				}
				GameSceneTables.SectionData new_scene_section_data = new_scene_data.GetSectionData(scene_section_name);
				GameSectionHierarchy.HierarchyData now_scene_hierarchy_data = hierarchy.GetTyped(GAME_SECTION_TYPE.SCENE);
				if (new_scene_section_data != (GameSceneTables.SectionData)null && (now_scene_hierarchy_data == null || now_scene_hierarchy_data.data != new_scene_section_data || reloadSceneFlag))
				{
					global.ChangeSection(new_scene_data, null);
					List<GameSectionHierarchy.HierarchyData> exclusive_list2 = isOpenImportantDialog ? new List<GameSectionHierarchy.HierarchyData>() : hierarchy.GetExclusiveList(GAME_SECTION_TYPE.SCENE);
					exclusive_list2.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
					{
						o.section.Close(((_003CDoChangeScene_003Ec__Iterator261)/*Error near IL_0732: stateMachine*/).close_type);
					});
					while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
					{
						yield return (object)null;
					}
					if (!isAutoEventSkip)
					{
						if (MonoBehaviourSingleton<TransitionManager>.I.isTransing || isOpenCommonDialog)
						{
							while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
							{
								yield return (object)null;
							}
						}
						else
						{
							TransitionManager.TYPE transition_type3 = global.GetTransitionType(prev_scene_name, prev_section_name, scene_section_name, section_name);
							if (transition_type3 != 0)
							{
								yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(transition_type3);
							}
						}
					}
					bool save_isWaiting10 = isWaiting;
					isWaiting = true;
					exclusive_list2.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
					{
						o.section.Exit();
					});
					while (exclusive_list2.Find((GameSectionHierarchy.HierarchyData o) => !o.section.isExited) != null)
					{
						yield return (object)null;
					}
					isWaiting = save_isWaiting10;
					hierarchy.DestroyHierarchy(exclusive_list2);
					if (global.SceneClear(prev_scene_name, prev_section_name, scene_section_name))
					{
						if (MonoBehaviourSingleton<InstantiateManager>.IsValid())
						{
							MonoBehaviourSingleton<InstantiateManager>.I.ClearStocks();
						}
						bool flag = new_scene_section_data.sectionName == "InGameScene";
						yield return (object)MonoBehaviourSingleton<AppMain>.I.ClearMemory(true, true);
					}
					bool save_isWaiting9 = isWaiting;
					isWaiting = true;
					global.SceneInitialize(prev_scene_name, scene_section_name);
					while (!global.isInitialized || IsBusy(error))
					{
						yield return (object)null;
					}
					isWaiting = save_isWaiting9;
					bool save_internal_mode4 = ResourceManager.internalMode;
					ResourceManager.internalMode = internal_res;
					LoadObject[] load_objs2 = new_scene_section_data.LoadUseResources(load_queue);
					ResourceManager.internalMode = save_internal_mode4;
					if (load_queue.IsLoading())
					{
						yield return (object)load_queue.Wait();
					}
					new_scene_section = hierarchy.CreateSection(new_scene_section_data, load_objs2);
					if (string.IsNullOrEmpty(section_name))
					{
						GameSceneTables.EventData event_data = new_scene_section_data.GetEventData(string.Empty);
						if (event_data != null)
						{
							section_name = event_data.toSectionName;
						}
					}
				}
				else
				{
					scene_section_name = GetCurrentSceneName();
					if (string.IsNullOrEmpty(section_name))
					{
						if (new_scene_section_data == (GameSceneTables.SectionData)null)
						{
							section_name = scene_name + "Top";
						}
						if (now_scene_hierarchy_data.data == new_scene_section_data)
						{
							GameSectionHierarchy.HierarchyData last_section2 = hierarchy.GetLast();
							if (last_section2 != null && last_section2.data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
							{
								last_section2 = hierarchy.GetLastExcludeCommonDialog();
								if (last_section2 != null && last_section2.data.type != 0)
								{
									section_name = last_section2.data.sectionName;
								}
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(section_name))
				{
					new_section_data = new_scene_data.GetSectionData(section_name);
					if (new_section_data == (GameSceneTables.SectionData)null)
					{
						Log.Error(LOG.GAMESCENE, "[ {0} ] is not found, in {1}", section_name, new_scene_data.sceneName);
						yield break;
					}
					global.ChangeSection(null, new_section_data);
					global.StageSetup(prev_scene_name, scene_section_name, section_name, new_section_data);
					GameSectionHierarchy.HierarchyData last_hierarchy_data = hierarchy.GetLast();
					GameSectionHierarchy.HierarchyData now_hierarchy_data = hierarchy.FindIgnoreSingle(new_section_data);
					if (now_hierarchy_data == null)
					{
						MonoBehaviourSingleton<UIManager>.I.UpdateMainUI(scene_section_name, section_name);
						List<GameSectionHierarchy.HierarchyData> exclusive_list = isOpenImportantDialog ? new List<GameSectionHierarchy.HierarchyData>() : hierarchy.GetExclusiveList(new_section_data.type);
						exclusive_list.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
						{
							o.section.Close(((_003CDoChangeScene_003Ec__Iterator261)/*Error near IL_0ce0: stateMachine*/).close_type);
						});
						MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
						while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
						{
							yield return (object)null;
						}
						if (!isAutoEventSkip)
						{
							if (MonoBehaviourSingleton<TransitionManager>.I.isTransing || new_section_data.type.IsDialog())
							{
								while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
								{
									yield return (object)null;
								}
							}
							else
							{
								TransitionManager.TYPE transition_type2 = global.GetTransitionType(prev_scene_name, prev_section_name, scene_section_name, section_name);
								if (transition_type2 != 0)
								{
									yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(transition_type2);
								}
							}
						}
						if (last_hierarchy_data != null)
						{
							Send(true, last_hierarchy_data.section, "OnChangePretreat", section_name + "@" + scene_name);
						}
						if (new_section_data.type == GAME_SECTION_TYPE.PAGE && !new_section_data.isTop)
						{
							List<GameSectionHierarchy.HierarchyData> list2 = new List<GameSectionHierarchy.HierarchyData>();
							exclusive_list.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
							{
								if (o.data.type != GAME_SECTION_TYPE.PAGE)
								{
									((_003CDoChangeScene_003Ec__Iterator261)/*Error near IL_0e5f: stateMachine*/)._003Clist_003E__41.Add(o);
								}
							});
							exclusive_list = list2;
						}
						else if (new_section_data.type == GAME_SECTION_TYPE.PAGE_DIALOG)
						{
							List<GameSectionHierarchy.HierarchyData> list = new List<GameSectionHierarchy.HierarchyData>();
							exclusive_list.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
							{
								if (o.data.type != GAME_SECTION_TYPE.PAGE_DIALOG)
								{
									((_003CDoChangeScene_003Ec__Iterator261)/*Error near IL_0ea3: stateMachine*/)._003Clist_003E__42.Add(o);
								}
							});
							exclusive_list = list;
						}
						bool save_isWaiting8 = isWaiting;
						isWaiting = true;
						exclusive_list.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
						{
							o.section.Exit();
						});
						while (exclusive_list.Find((GameSectionHierarchy.HierarchyData o) => !o.section.isExited) != null)
						{
							yield return (object)null;
						}
						isWaiting = save_isWaiting8;
						hierarchy.DestroyHierarchy(exclusive_list);
						MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
						bool save_internal_mode3 = ResourceManager.internalMode;
						ResourceManager.internalMode = internal_res;
						LoadObject[] load_objs;
						if (new_section_data.type != GAME_SECTION_TYPE.COMMON_DIALOG)
						{
							load_objs = new_section_data.LoadUseResources(load_queue);
						}
						else
						{
							string common_res_name = tables.GetCommonResourceName(new_section_data.typeParams[0]);
							load_objs = new LoadObject[1]
							{
								load_queue.Load(RESOURCE_CATEGORY.UI, common_res_name, false)
							};
						}
						ResourceManager.internalMode = save_internal_mode3;
						if (load_queue.IsLoading())
						{
							yield return (object)load_queue.Wait();
						}
						while (IsBusy(error))
						{
							yield return (object)null;
						}
						new_section = hierarchy.CreateSection(new_section_data, load_objs);
					}
					else if (last_hierarchy_data != now_hierarchy_data)
					{
						List<GameSectionHierarchy.HierarchyData> cut_list = isOpenImportantDialog ? new List<GameSectionHierarchy.HierarchyData>() : hierarchy.GetCutList(now_hierarchy_data);
						cut_list.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
						{
							o.section.Close(((_003CDoChangeScene_003Ec__Iterator261)/*Error near IL_1104: stateMachine*/).close_type);
						});
						MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
						while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
						{
							yield return (object)null;
						}
						if (!isAutoEventSkip)
						{
							if (MonoBehaviourSingleton<TransitionManager>.I.isTransing || new_section_data.type.IsDialog())
							{
								while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
								{
									yield return (object)null;
								}
							}
							else
							{
								GameSectionHierarchy.HierarchyData last_exculude_dialog_data = hierarchy.GetLastExcludeDialog();
								if ((last_hierarchy_data.data.type == GAME_SECTION_TYPE.PAGE || (last_hierarchy_data.data.type.IsDialog() && last_exculude_dialog_data != null && last_exculude_dialog_data.data.type == GAME_SECTION_TYPE.PAGE && last_exculude_dialog_data != now_hierarchy_data)) && (!last_hierarchy_data.data.type.IsDialog() || !new_section_data.type.IsDialog()))
								{
									TransitionManager.TYPE transition_type = global.GetTransitionType(prev_scene_name, prev_section_name, scene_section_name, section_name);
									if (transition_type != 0)
									{
										yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(transition_type);
									}
								}
							}
						}
						if (!global_init_section)
						{
							bool save_isWaiting7 = isWaiting;
							isWaiting = true;
							global_init_section = true;
							global.SectionInitialize(scene_section_name, section_name, new_section_data);
							while (!global.isInitialized || IsBusy(error))
							{
								yield return (object)null;
							}
							isWaiting = save_isWaiting7;
						}
						MonoBehaviourSingleton<UIManager>.I.UpdateMainUI(scene_section_name, section_name);
						if (now_hierarchy_data.section.state != UIBehaviour.STATE.OPEN)
						{
							bool save_isWaiting6 = isWaiting;
							isWaiting = true;
							now_hierarchy_data.section.isReOpenInitialized = false;
							now_hierarchy_data.section.InitializeReopen();
							while (!now_hierarchy_data.section.isReOpenInitialized || IsBusy(error))
							{
								yield return (object)null;
							}
							isWaiting = save_isWaiting6;
							now_hierarchy_data.section.Open(open_type);
						}
						while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
						{
							yield return (object)null;
						}
						bool save_isWaiting5 = isWaiting;
						isWaiting = true;
						cut_list.ForEach(delegate(GameSectionHierarchy.HierarchyData o)
						{
							o.section.Exit();
						});
						while (cut_list.Find((GameSectionHierarchy.HierarchyData o) => !o.section.isExited) != null)
						{
							yield return (object)null;
						}
						isWaiting = save_isWaiting5;
						hierarchy.DestroyHierarchy(cut_list);
						MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
						if (last_hierarchy_data != null && last_hierarchy_data.data.type.IsDialog())
						{
							Send(true, now_hierarchy_data.section, "OnCloseDialog", last_hierarchy_data.data.sectionName);
							Send(true, now_hierarchy_data.section, "OnCloseDialog_" + last_hierarchy_data.data.sectionName, null);
							while (IsBusy(error))
							{
								yield return (object)null;
							}
						}
					}
				}
				if (new_section_data != (GameSceneTables.SectionData)null && !isOpenCommonDialog)
				{
					history.Push(scene_name, section_name, new_section_data.type);
				}
				if (!global_init_section)
				{
					bool save_isWaiting4 = isWaiting;
					isWaiting = true;
					global.SectionInitialize(scene_section_name, section_name, new_section_data);
					while (!global.isInitialized || IsBusy(error))
					{
						yield return (object)null;
					}
					isWaiting = save_isWaiting4;
				}
				if (new_scene_section != null)
				{
					bool save_isWaiting3 = isWaiting;
					isWaiting = true;
					new_section.LoadRequireDataTable();
					while (!new_section.isLoadedRequireDataTable)
					{
						yield return (object)null;
					}
					new_scene_section.Initialize();
					while (!new_scene_section.isInitialized || IsBusy(error))
					{
						yield return (object)null;
					}
					isWaiting = save_isWaiting3;
					new_scene_section.Open(open_type);
					if (new_scene_section_data != (GameSceneTables.SectionData)null)
					{
						bool save_internal_mode2 = ResourceManager.internalMode;
						ResourceManager.internalMode = internal_res;
						new_scene_section_data.LoadPreloadResources(load_queue);
						ResourceManager.internalMode = save_internal_mode2;
					}
				}
				if (new_section != null)
				{
					bool save_isWaiting = isWaiting;
					isWaiting = true;
					new_section.LoadRequireDataTable();
					while (!new_section.isLoadedRequireDataTable)
					{
						yield return (object)null;
					}
					new_section.Initialize();
					while (!new_section.isInitialized || IsBusy(error))
					{
						yield return (object)null;
					}
					isWaiting = save_isWaiting;
					MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(hierarchy, new_section_data);
					new_section.Open(open_type);
					if (new_section_data != (GameSceneTables.SectionData)null)
					{
						bool save_internal_mode = ResourceManager.internalMode;
						ResourceManager.internalMode = internal_res;
						new_section_data.LoadPreloadResources(load_queue);
						ResourceManager.internalMode = save_internal_mode;
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
							yield return (object)null;
						}
					}
					global.SectionSetup(scene_section_name, section_name, new_section_data);
					if (!isAutoEventSkip && !isOpenCommonDialog && (save_isChangeing || !skipTrantisionEnd) && global.IsTransitionEnd(prev_scene_name, prev_section_name, scene_section_name, section_name))
					{
						yield return (object)MonoBehaviourSingleton<TransitionManager>.I.In();
					}
					while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
					{
						yield return (object)null;
					}
					MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.SCENE_CHANGE, false);
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
					ExecuteSceneEvent("REQUEST", this.get_gameObject(), GameSceneEvent.request.eventName, GameSceneEvent.request.userData, null, true);
					GameSceneEvent.request = null;
				}
			}
		}
	}

	private bool IsBusy()
	{
		return Protocol.isBusy;
	}

	private bool IsBusy(bool important)
	{
		return !important && IsBusy();
	}

	public void ExecuteSceneEvent(string caller, GameObject sender, string event_name, object user_data = null, string check_app_ver = null, bool is_send_query = true)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		GameSectionHierarchy.HierarchyData last = hierarchy.GetLast();
		if (last != null)
		{
			UIBehaviour uIBehaviour = null;
			string text;
			bool flag;
			if (sender == this.get_gameObject())
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
				text = ((!(uIBehaviour != null)) ? string.Empty : uIBehaviour.get_name());
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
			}
			else if (isChangeing && !isOpenImportantDialog)
			{
				if (sender != null)
				{
					Log.Warning(LOG.GAMESCENE, "during scene change, so an event is ignored. {0}", event_name);
				}
			}
			else if (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
			{
				Log.Warning(LOG.GAMESCENE, "during UI transitioning, so an event is ignored. {0}", event_name);
			}
			else if (IsBusy(isOpenImportantDialog))
			{
				if (!Protocol.strict && doWaitEventCount == 0)
				{
					this.StartCoroutine(DoWaitEvent(caller, sender, event_name, user_data, check_app_ver, is_send_query));
				}
				else
				{
					Log.Warning(LOG.GAMESCENE, "protocol is busy, so an event is ignored. {0}", event_name);
				}
			}
			else if (GameSceneEvent.IsStay())
			{
				Log.Warning(LOG.GAMESCENE, "now staying, so an event is ignored. {0}", event_name);
			}
			else
			{
				GameSection gameSection = null;
				if (sender != null)
				{
					gameSection = sender.GetComponentInParent<GameSection>();
				}
				if (gameSection != null && !gameSection.isInitialized)
				{
					Log.Warning(LOG.GAMESCENE, "It's initialized, so an event is ignored. {0}", event_name);
				}
				else if (!(gameSection != null) || !last.data.type.IsDialog() || !(gameSection != last.section) || GameSceneGlobalSettings.IsGlobalEvent(event_name))
				{
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
										Send(false, lastExcludeCommonDialog.section, $"OnQuery_{last.data.sectionName}_{event_name}", null);
									}
								}
							}
							else
							{
								Send(false, last.section, $"OnQuery_{event_name}", null);
							}
						}
						else if (last.data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
						{
							if (commonDialogCallback == null)
							{
								GameSectionHierarchy.HierarchyData lastExcludeCommonDialog2 = hierarchy.GetLastExcludeCommonDialog();
								if (lastExcludeCommonDialog2 != null)
								{
									Send(false, lastExcludeCommonDialog2.section, $"OnQuery_{last.data.sectionName}_SECTION_BACK", null);
								}
							}
						}
						else
						{
							Send(false, last.section, "OnQuery_SECTION_BACK", null);
						}
						this.isCallingOnQuery = isCallingOnQuery;
					}
					if (GameSceneEvent.current.isExecute)
					{
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
									GameSectionHistory.HistoryData last2 = history.GetLast(i, true);
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
			yield return (object)null;
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
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
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
		}
		else
		{
			autoEvents = event_datas;
			if (MonoBehaviourSingleton<UIManager>.I.tutorialMessage != null)
			{
				MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SetSkipSectionRunCount(autoEvents.Length - 1);
			}
			this.StartCoroutine(DoAutoEvent());
		}
	}

	public void StopAutoEvent(Action on_finished = null)
	{
		if (autoEvents == null)
		{
			if (on_finished != null)
			{
				on_finished.Invoke();
			}
		}
		else
		{
			autoEvents = null;
			onAutoEventFinished = on_finished;
		}
	}

	private IEnumerator DoAutoEvent()
	{
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.AUTO_EVENT, true);
		if (!isAutoEventSkip)
		{
			MonoBehaviourSingleton<OutGameEffectManager>.I.ShowAutoEventEffect();
		}
		int index = 0;
		bool is_change_version_check_section = false;
		while (autoEvents != null)
		{
			yield return (object)null;
			if (notifyFlags == (GameSection.NOTIFY_FLAG)0L && !isChangeing && !isOpenCommonDialog && !GameSceneEvent.IsStay() && (MonoBehaviourSingleton<UIManager>.I.disableFlags & UIManager.DISABLE_FACTOR.AUTO_EVENT) == UIManager.DISABLE_FACTOR.AUTO_EVENT)
			{
				if (autoEvents != null && index >= autoEvents.Length)
				{
					break;
				}
				if (isAutoEventSkip && !MonoBehaviourSingleton<TransitionManager>.I.isTransing)
				{
					yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.AUTO_EVENT);
				}
				EventData event_data = (autoEvents == null) ? null : autoEvents[index];
				if (event_data != null)
				{
					EventData ret = GetCurrentSection().CheckAutoEvent(event_data.name, event_data.data);
					if (ret != null)
					{
						event_data = ret;
					}
				}
				bool is_execute_event = event_data != null && !string.IsNullOrEmpty(event_data.name);
				if (!isAutoEventSkip && is_execute_event && MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
				{
					UIGameSceneEventSender sender = null;
					Utility.ForEach(MonoBehaviourSingleton<UIManager>.I._transform, delegate(Transform t)
					{
						UIGameSceneEventSender component = t.GetComponent<UIGameSceneEventSender>();
						if (component == null)
						{
							return false;
						}
						if (component.eventName != ((_003CDoAutoEvent_003Ec__Iterator263)/*Error near IL_0205: stateMachine*/)._003Cevent_data_003E__2.name)
						{
							return false;
						}
						if (component.eventData != null && ((_003CDoAutoEvent_003Ec__Iterator263)/*Error near IL_0205: stateMachine*/)._003Cevent_data_003E__2.data != null && !component.eventData.Equals(((_003CDoAutoEvent_003Ec__Iterator263)/*Error near IL_0205: stateMachine*/)._003Cevent_data_003E__2.data))
						{
							return false;
						}
						((_003CDoAutoEvent_003Ec__Iterator263)/*Error near IL_0205: stateMachine*/)._003Csender_003E__5 = component;
						return true;
					});
					if (sender != null)
					{
						UIButton button = sender.GetComponent<UIButton>();
						if (button != null)
						{
							UIScrollView scroll_view = button.GetComponentInParent<UIScrollView>();
							if (scroll_view != null && scroll_view.get_enabled())
							{
								UIPanel panel = scroll_view.GetComponent<UIPanel>();
								if (!panel.IsVisible(button.GetComponent<UIWidget>()))
								{
									Vector3 offset = -panel.cachedTransform.InverseTransformPoint(button.get_transform().get_position());
									if (!scroll_view.canMoveHorizontally)
									{
										Vector3 localPosition = panel.cachedTransform.get_localPosition();
										offset.x = localPosition.x;
									}
									if (!scroll_view.canMoveVertically)
									{
										Vector3 localPosition2 = panel.cachedTransform.get_localPosition();
										offset.y = localPosition2.y;
									}
									SpringPanel sp = SpringPanel.Begin(panel.cachedGameObject, offset, 16f);
									bool wait = true;
									SpringPanel.OnFinished func = delegate
									{
										((_003CDoAutoEvent_003Ec__Iterator263)/*Error near IL_0362: stateMachine*/)._003Cwait_003E__11 = false;
									};
									SpringPanel springPanel = sp;
									springPanel.onFinished = (SpringPanel.OnFinished)Delegate.Combine(springPanel.onFinished, func);
									while (wait)
									{
										yield return (object)null;
									}
									SpringPanel springPanel2 = sp;
									springPanel2.onFinished = (SpringPanel.OnFinished)Delegate.Remove(springPanel2.onFinished, func);
								}
							}
							Vector3 pos = button.get_transform().get_position();
							yield return (object)MonoBehaviourSingleton<OutGameEffectManager>.I.MoveAutoEventEffect(pos);
							button.SetState(UIButtonColor.State.Pressed, false);
							MonoBehaviourSingleton<OutGameEffectManager>.I.PopTouchEffect(pos);
							yield return (object)new WaitForSeconds(0.2f);
							button.SetState(UIButtonColor.State.Normal, false);
						}
					}
				}
				if (is_execute_event)
				{
					if (event_data.data != null && event_data.data.GetType() == typeof(EventListData))
					{
						Network.EventData ev = (Network.EventData)event_data.data;
						if (ev.eventType == 15)
						{
							if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 50)
							{
								MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
								break;
							}
							ExecuteSceneEvent("AUTO", this.get_gameObject(), event_data.name + "_ARENA", event_data.data, null, true);
						}
						else
						{
							ExecuteSceneEvent("AUTO", this.get_gameObject(), event_data.name, event_data.data, null, true);
						}
					}
					else
					{
						ExecuteSceneEvent("AUTO", this.get_gameObject(), event_data.name, event_data.data, null, true);
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
		}
		if (!is_change_version_check_section && MonoBehaviourSingleton<TransitionManager>.I.isTransing)
		{
			yield return (object)MonoBehaviourSingleton<TransitionManager>.I.In();
		}
		if (!isAutoEventSkip)
		{
			MonoBehaviourSingleton<OutGameEffectManager>.I.HideAutoEventEffect();
		}
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.AUTO_EVENT, false);
		autoEvents = null;
		if (onAutoEventFinished != null)
		{
			onAutoEventFinished.Invoke();
			onAutoEventFinished = null;
		}
	}

	public void OpenCommonDialog(CommonDialog.Desc desc, Action<string> callback, bool error = false, int errorCode = 0)
	{
		OpenCommonDialog_(desc, callback, error, true, errorCode);
	}

	private void OpenCommonDialog_(CommonDialog.Desc desc, Action<string> callback, bool error, bool internal_res, int errorCode = 0)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (callback != null)
		{
			if (isOpenCommonDialog)
			{
				this.StartCoroutine(DoWaitOpenCommonDialog(desc, callback, error, internal_res, errorCode));
			}
			else if (isChangeing && !error)
			{
				Log.Error(LOG.GAMESCENE, "during scene change. error={0} isWaiting={1}", error, isWaiting);
			}
			else
			{
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
					ChangeCommonDialog("CommonDialog", (!error) ? "CommonDialogTop" : "CustomDialogError", error, internal_res);
				}
				else if (errorCode > 600000 && errorCode < 700000)
				{
					ChangeCommonDialog("CommonDialog", (!error) ? "CommonDialogTop" : "CustomDialogError", error, internal_res);
				}
				else
				{
					ChangeCommonDialog("CommonDialog", (!error) ? "CommonDialogTop" : "CommonDialogError", error, internal_res);
				}
			}
		}
	}

	private IEnumerator DoWaitOpenCommonDialog(CommonDialog.Desc desc, Action<string> callback, bool error, bool internal_res, int errorCode = 0)
	{
		while (isOpenCommonDialog)
		{
			yield return (object)null;
		}
		OpenCommonDialog_(desc, callback, error, internal_res, errorCode);
	}

	public void OpenInfoDialog(Action<string> callback, bool error = false)
	{
		commonDialogSaveCurrentEvent = GameSceneEvent.current;
		commonDialogCallback = callback;
		isOpenImportantDialog = error;
		ChangeCommonDialog("CommonDialog", "InformationDialog", true, ResourceManager.internalMode);
	}

	public void OpenUpdateAppDialog(uint msg_id, bool is_yes_no, Action onCancel = null)
	{
		GameSceneEvent.PushStay();
		OpenCommonDialog(new CommonDialog.Desc(is_yes_no ? CommonDialog.TYPE.YES_NO : CommonDialog.TYPE.OK, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, msg_id), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102u), null, null), delegate(string ret)
		{
			GameSceneEvent.PopStay();
			if (ret == "YES" || ret == "OK")
			{
				Native.launchMyselfMarket();
				MonoBehaviourSingleton<AppMain>.I.Reset();
			}
			else if (onCancel != null)
			{
				onCancel.Invoke();
			}
		}, true, 0);
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
		if (portal_data.dstMapID != 0 && !CheckMapAndOpenUpdateAppDialog(portal_data.dstMapID, true))
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
						OpenUpdateAppDialog(2003u, is_yes_no, null);
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
		return CheckQuestAndOpenUpdateAppDialog(Singleton<QuestTable>.I.GetQuestData(quest_id), is_yes_no, false);
	}

	public unsafe bool CheckQuestAndOpenUpdateAppDialog(QuestTable.QuestTableData quest_data, bool is_yes_no = true, bool is_happen_quest = false)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
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
						int msg_id = (!is_happen_quest) ? 2003 : 2002;
						if (_003C_003Ef__am_0024cache16 == null)
						{
							_003C_003Ef__am_0024cache16 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
						}
						OpenUpdateAppDialog((uint)msg_id, is_yes_no, _003C_003Ef__am_0024cache16);
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
		return CheckEquipItemAndOpenUpdateAppDialog(Singleton<EquipItemTable>.I.GetEquipItemData(equip_item_id), null);
	}

	public bool CheckEquipItemAndOpenUpdateAppDialog(EquipItemTable.EquipItemData equip_item_data, Action onCancel = null)
	{
		if (equip_item_data == null)
		{
			return true;
		}
		if (!equip_item_data.IsEnableNowApplicationVersion())
		{
			OpenUpdateAppDialog(2000u, true, onCancel);
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
		return CheckSkillItemAndOpenUpdateAppDialog(Singleton<SkillItemTable>.I.GetSkillItemData(skill_item_id), null);
	}

	public bool CheckSkillItemAndOpenUpdateAppDialog(SkillItemTable.SkillItemData skill_item_data, Action onCancel = null)
	{
		if (skill_item_data == null)
		{
			return true;
		}
		if (!skill_item_data.IsEnableNowApplicationVersion())
		{
			OpenUpdateAppDialog(2001u, true, onCancel);
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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoOpenOpinionBox());
	}

	private IEnumerator DoOpenOpinionBox()
	{
		while (notifyFlags != (GameSection.NOTIFY_FLAG)0L || isChangeing || isOpenCommonDialog || GameSceneEvent.IsStay())
		{
			yield return (object)null;
		}
		ChangeScene("OpinionBox", "OpinionTop", UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}

	public bool IsCurrentSceneHomeOrLounge()
	{
		return GetCurrentSceneName() == "HomeScene" || GetCurrentSceneName() == "LoungeScene" || GetCurrentSceneName() == "GuildScene";
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
