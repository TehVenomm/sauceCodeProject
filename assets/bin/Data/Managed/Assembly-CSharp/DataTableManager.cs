using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataTableManager : MonoBehaviourSingleton<DataTableManager>
{
	private enum LoadStatus
	{
		NotInitialize,
		LoadingInitialTable,
		LoadingAllTable,
		LoadComplete
	}

	private class RequestParam
	{
		public string tableName;

		public Action<byte[]> processBinary;

		public RequestParam(string tableName, Action<byte[]> processBinary = null)
		{
			this.tableName = tableName;
			this.processBinary = processBinary;
		}
	}

	private class DataTableInterfaceProxy : IDataTable
	{
		private Action<string> create;

		public DataTableInterfaceProxy(Action<string> create)
		{
			this.create = create;
		}

		public void CreateTable(string csv)
		{
			create(csv);
		}
	}

	private class DataTableContainer : IDataTable
	{
		private IDataTable table;

		private DataTableContainer dependencyTable;

		public bool isInitialized
		{
			get;
			private set;
		}

		public string name
		{
			get;
			private set;
		}

		public DataTableContainer(string name, IDataTable table)
		{
			this.name = name;
			this.table = table;
		}

		public void SetDependency(DataTableContainer table)
		{
			dependencyTable = table;
		}

		public DataTableContainer GetDependency()
		{
			return dependencyTable;
		}

		public bool CanLoad()
		{
			if (dependencyTable == null)
			{
				return true;
			}
			return dependencyTable.isInitialized;
		}

		public void CreateTable(string csv)
		{
			table.CreateTable(csv);
			isInitialized = true;
		}
	}

	private static string MANIFEST_NAME = "manifest";

	private DataTableManifest manifest;

	public DataTableCache cache;

	private Dictionary<string, DataTableContainer> tables = new Dictionary<string, DataTableContainer>();

	private List<DataLoadRequest> erroredRequests = new List<DataLoadRequest>();

	private LoadStatus loadStatus;

	public DataLoader dataLoader;

	private XorInt vm = new XorInt(1);

	private int lastReceiveManifestVersion = -1;

	private static readonly string DATA_TABLE_DIRECTORY = "tables";

	private List<DataLoadRequest> verifyErroredRequest = new List<DataLoadRequest>();

	private List<Action> afterProcesses = new List<Action>();

	private List<KeyValuePair<string, string>> unresolvedDependencies = new List<KeyValuePair<string, string>>();

	public bool shouldUpdateManifest => manifest == null || lastReceiveManifestVersion != manifest.version;

	public int manifestVersion => (manifest == null) ? (-1) : manifest.version;

	public bool hasManifest => manifest != null;

	public bool reportOnly => (int)vm == 1;

	public bool forceLoadCSV
	{
		get;
		set;
	}

	public event Action<DataTableLoadError, Action> onError;

	protected override void Awake()
	{
		base.Awake();
		cache = new DataTableCache();
		dataLoader = this.get_gameObject().AddComponent<DataLoader>();
		dataLoader.SetCache(new DataTableCache());
		forceLoadCSV = false;
		loadStatus = LoadStatus.NotInitialize;
	}

	public void OnReceiveTableManifestVersion(int version)
	{
		if (lastReceiveManifestVersion != version)
		{
		}
		lastReceiveManifestVersion = version;
	}

	public void OnReceiveVM(XorInt vm)
	{
		this.vm = vm;
	}

	public void UpdateManifest(Action onComplete)
	{
		int version = lastReceiveManifestVersion;
		DataLoadRequest dataLoadRequest = CreateRequest(MANIFEST_NAME, new ManifestVersion(version), DATA_TABLE_DIRECTORY);
		dataLoadRequest.processCompressedTextData = delegate(byte[] bytes)
		{
			try
			{
				string csv = DecompressToString(bytes);
				manifest = DataTableManifest.Create(csv, version);
			}
			catch (Exception ex)
			{
				Log.Error(LOG.DATA_TABLE, "manifest load error: {0}", ex.ToString());
				throw;
			}
		};
		dataLoadRequest.onComplete += onComplete;
		dataLoader.RequestManifest(dataLoadRequest);
	}

	public List<DataLoadRequest> LoadInitialTable(Action onComplete, bool downloadOnly = false)
	{
		if (!downloadOnly)
		{
			loadStatus = LoadStatus.LoadingInitialTable;
		}
		RequestParam[] array = new RequestParam[23]
		{
			new RequestParam("AvatarTable"),
			new RequestParam("AccessoryTable"),
			new RequestParam("AccessoryInfoTable"),
			new RequestParam("CreateEquipItemTable"),
			new RequestParam("CreatePickupItemTable"),
			new RequestParam("DeliveryTable"),
			new RequestParam("EquipItemTable"),
			new RequestParam("EquipModelTable"),
			new RequestParam("GrowSkillItemTable"),
			new RequestParam("HomeThemeTable"),
			new RequestParam("CountdownTable"),
			new RequestParam("NPCMessageTable"),
			new RequestParam("NPCTable"),
			new RequestParam("QuestTable"),
			new RequestParam("SkillItemTable"),
			new RequestParam("ExceedSkillItemTable"),
			new RequestParam("StageTable"),
			new RequestParam("TutorialMessageTable"),
			new RequestParam("StampTypeTable"),
			new RequestParam("EquipItemExceedParamTable"),
			new RequestParam("RegionTable"),
			new RequestParam("FieldMapTable"),
			new RequestParam("FieldMapPortalTable")
		};
		List<DataLoadRequest> list = new List<DataLoadRequest>();
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			RequestParam requestParam = array[i];
			DataLoadRequest item = CreateRequestLoadTable(requestParam.tableName, downloadOnly, requestParam.processBinary);
			list.Add(item);
		}
		SetDepends(list);
		int reqCount = list.Count;
		foreach (DataLoadRequest item2 in list)
		{
			item2.onComplete += delegate
			{
				reqCount--;
				if (reqCount <= 0)
				{
					onComplete();
					if (!downloadOnly)
					{
						loadStatus = LoadStatus.LoadingAllTable;
					}
				}
			};
		}
		Request(list);
		return list;
	}

	public List<DataLoadRequest> LoadAllTable(Action onComplete, bool downloadOnly = false)
	{
		RequestParam[] array = new RequestParam[52]
		{
			new RequestParam("AbilityDataTable"),
			new RequestParam("AbilityTable"),
			new RequestParam("AbilityItemLotTable"),
			new RequestParam("AudioSettingTable"),
			new RequestParam("DeliveryRewardTable"),
			new RequestParam("EnemyTable"),
			new RequestParam("EquipItemExceedTable"),
			new RequestParam("EvolveEquipItemTable"),
			new RequestParam("GrowEnemyTable"),
			new RequestParam("ItemTable"),
			new RequestParam("TutorialGearSetTable"),
			new RequestParam("TradingPostTable"),
			new RequestParam("SETable"),
			new RequestParam("StringTable"),
			new RequestParam("TaskTable"),
			new RequestParam("UserLevelTable"),
			new RequestParam("GrowEquipItemTable"),
			new RequestParam("GrowEquipItemNeedItemTable"),
			new RequestParam("GrowEquipItemNeedUniqueItemTable"),
			new RequestParam("MissionTable"),
			new RequestParam("RegionTable"),
			new RequestParam("FieldMapTable"),
			new RequestParam("FieldMapPortalTable"),
			new RequestParam("FieldMapEnemyPopTable"),
			new RequestParam("FieldMapGatherPointTable"),
			new RequestParam("FieldMapGatherPointViewTable"),
			new RequestParam("FieldMapGimmickPointTable"),
			new RequestParam("FieldMapGimmickActionTable"),
			new RequestParam("QuestToFieldTable"),
			new RequestParam("ItemToFieldTable"),
			new RequestParam("EnemyHitTypeTable"),
			new RequestParam("EnemyHitMaterialTable"),
			new RequestParam("EnemyPersonalityTable"),
			new RequestParam("PointShopGetPointTable"),
			new RequestParam("DegreeTable"),
			new RequestParam("DamageDistanceTable"),
			new RequestParam("GachaSearchEnemyTable"),
			new RequestParam("BuffTable"),
			new RequestParam("FieldBuffTable"),
			new RequestParam("WaveMatchDropTable"),
			new RequestParam("LimitedEquipItemExceedTable"),
			new RequestParam("PlayDataTable"),
			new RequestParam("ArenaTable"),
			new RequestParam("EnemyAngryTable"),
			new RequestParam("EnemyActionTable"),
			new RequestParam("NpcLevelTable"),
			new RequestParam("NpcLevelSpecialTable"),
			new RequestParam("FieldMapEnemyPopTimeZoneTable"),
			new RequestParam("GatherItemTable"),
			new RequestParam("AssignedEquipmentTable"),
			new RequestParam("SymbolTable"),
			new RequestParam("ProductDataTable")
		};
		List<DataLoadRequest> list = new List<DataLoadRequest>();
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			RequestParam requestParam = array[i];
			DataLoadRequest item = CreateRequestLoadTable(requestParam.tableName, downloadOnly, requestParam.processBinary);
			list.Add(item);
		}
		SetDepends(list);
		int reqCount = list.Count;
		foreach (DataLoadRequest item2 in list)
		{
			item2.onComplete += delegate
			{
				reqCount--;
				if (reqCount <= 0)
				{
					onComplete();
					if (!downloadOnly)
					{
						loadStatus = LoadStatus.LoadComplete;
					}
				}
			};
		}
		Request(list);
		return list;
	}

	private void SetDepends(List<DataLoadRequest> reqs)
	{
		int i = 0;
		for (int count = reqs.Count; i < count; i++)
		{
			DataLoadRequest dataLoadRequest = reqs[i];
			DataTableContainer value = null;
			if (!tables.TryGetValue(dataLoadRequest.name, out value))
			{
				continue;
			}
			DataTableContainer dependency = value.GetDependency();
			if (dependency != null)
			{
				DataLoadRequest dataLoadRequest2 = reqs.Find((DataLoadRequest o) => o.name == dependency.name);
				if (dataLoadRequest2 != null)
				{
					dataLoadRequest.DependsOn(dataLoadRequest2);
				}
			}
		}
	}

	public DataLoadRequest RequestLoadTable(string name, IDataTable table, Action onComplete, bool downloadOnly = false)
	{
		DataLoadRequest dataLoadRequest = CreateRequestLoadTable(name, table, downloadOnly);
		dataLoadRequest.onComplete += onComplete;
		Request(dataLoadRequest);
		return dataLoadRequest;
	}

	public DataLoadRequest RequestLoadTable(string name, Action onComplete, bool downloadOnly = false)
	{
		tables.TryGetValue(name, out DataTableContainer value);
		DataLoadRequest dataLoadRequest = CreateRequestLoadTable(name, value, downloadOnly);
		dataLoadRequest.onComplete += onComplete;
		Request(dataLoadRequest);
		return dataLoadRequest;
	}

	public DataLoadRequest RequestLoadTable(string name, Action<byte[]> processBinaryData, Action onComplete, bool downloadOnly = false)
	{
		tables.TryGetValue(name, out DataTableContainer value);
		DataLoadRequest dataLoadRequest = CreateRequestLoadTable(name, value, downloadOnly);
		dataLoadRequest.onComplete += onComplete;
		if (processBinaryData != null)
		{
			dataLoadRequest.processCompressedBinaryData = processBinaryData;
		}
		Request(dataLoadRequest);
		return dataLoadRequest;
	}

	private DataLoadRequest CreateRequestLoadTable(string name, bool downloadOnly = false, Action<byte[]> processBinary = null)
	{
		tables.TryGetValue(name, out DataTableContainer value);
		return CreateRequestLoadTable(name, value, downloadOnly, processBinary);
	}

	private DataLoadRequest CreateRequestLoadTable(string name, IDataTable table, bool downloadOnly = false, Action<byte[]> processBinary = null)
	{
		if (downloadOnly || forceLoadCSV)
		{
			processBinary = null;
		}
		DataLoadRequest dataLoadRequest = CreateRequest(name, manifest.GetTableHash(name), DATA_TABLE_DIRECTORY, downloadOnly);
		dataLoadRequest.processCompressedTextData = delegate(byte[] bytes)
		{
			if (table != null)
			{
				if (bytes.Length < 256)
				{
					throw new ApplicationException("seek error");
				}
				string text = DecompressToString(bytes);
				if (!string.IsNullOrEmpty(text))
				{
					table.CreateTable(text);
				}
				else if (text == null)
				{
					throw new ApplicationException();
				}
			}
		};
		if (processBinary != null)
		{
			dataLoadRequest.SetupLoadBinary(manifest, processBinary);
		}
		return dataLoadRequest;
	}

	private DataLoadRequest CreateRequest(string name, IDataTableRequestHash hash, string directory, bool downloadOnly = false)
	{
		DataLoadRequest req = new DataLoadRequest(name, hash, directory, downloadOnly);
		req.onVerifyError += delegate(string filehash)
		{
			ReportVerifyError(name, filehash);
			if (reportOnly)
			{
				Log.Error(LOG.DATA_TABLE, "VerifyError(report-only): {0}", req.name);
				return true;
			}
			if (!verifyErroredRequest.Contains(req))
			{
				Log.Error(LOG.DATA_TABLE, "VerifyError(auto-retry): {0}", req.name);
				cache.Remove(req);
				verifyErroredRequest.Add(req);
			}
			return false;
		};
		req.onError += delegate(DataTableLoadError error)
		{
			Log.Error(LOG.DATA_TABLE, "load error ({1}): {0}", req.name, error.ToString());
			erroredRequests.Add(req);
			cache.Remove(req);
			if (this.onError != null)
			{
				this.onError(error, Retry);
			}
		};
		req.onComplete += delegate
		{
			verifyErroredRequest.Remove(req);
		};
		return req;
	}

	private void Request(List<DataLoadRequest> reqs)
	{
		dataLoader.Request(reqs);
	}

	private void Request(DataLoadRequest req)
	{
		dataLoader.Request(req);
	}

	private void Retry()
	{
		int i = 0;
		for (int count = erroredRequests.Count; i < count; i++)
		{
			DataLoadRequest dataLoadRequest = erroredRequests[i];
			dataLoadRequest.Reset();
			dataLoader.Request(dataLoadRequest);
		}
		erroredRequests.Clear();
	}

	private void ReportVerifyError(string filename, string filehash)
	{
		ReportVerifyModel.RequestSendForm requestSendForm = new ReportVerifyModel.RequestSendForm();
		requestSendForm.fileName = filename.ToLower();
		requestSendForm.fileHash = filehash;
		Protocol.Send<ReportVerifyModel.RequestSendForm, ReportVerifyModel>(ReportVerifyModel.URL, requestSendForm, delegate
		{
		}, string.Empty);
	}

	private void AfterAllLoad()
	{
		foreach (Action afterProcess in afterProcesses)
		{
			afterProcess();
		}
	}

	public void Clear()
	{
		this.StopAllCoroutines();
		tables.Clear();
		erroredRequests.Clear();
	}

	public void Initialize()
	{
		Clear();
		Singleton<AbilityDataTable>.Create();
		Singleton<AbilityTable>.Create();
		Singleton<AbilityItemLotTable>.Create();
		Singleton<AccessoryTable>.Create();
		Singleton<AudioSettingTable>.Create();
		Singleton<AvatarTable>.Create();
		Singleton<CreateEquipItemTable>.Create();
		Singleton<CreatePickupItemTable>.Create();
		Singleton<DeliveryRewardTable>.Create();
		Singleton<DeliveryTable>.Create();
		Singleton<EnemyHitMaterialTable>.Create();
		Singleton<EnemyHitTypeTable>.Create();
		Singleton<EnemyPersonalityTable>.Create();
		Singleton<EnemyTable>.Create();
		Singleton<EquipItemExceedParamTable>.Create();
		Singleton<EquipItemExceedTable>.Create();
		Singleton<EquipItemTable>.Create();
		Singleton<EquipModelTable>.Create();
		Singleton<EvolveEquipItemTable>.Create();
		Singleton<FieldMapTable>.Create();
		Singleton<GrowEnemyTable>.Create();
		Singleton<GrowEquipItemTable>.Create();
		Singleton<GrowSkillItemTable>.Create();
		Singleton<ItemTable>.Create();
		Singleton<TutorialGearSetTable>.Create();
		Singleton<TradingPostTable>.Create();
		Singleton<ItemToFieldTable>.Create();
		Singleton<ItemToQuestTable>.Create();
		Singleton<NPCMessageTable>.Create();
		Singleton<NPCTable>.Create();
		Singleton<QuestTable>.Create();
		Singleton<QuestToFieldTable>.Create();
		Singleton<RegionTable>.Create();
		Singleton<SETable>.Create();
		Singleton<SkillItemTable>.Create();
		Singleton<ExceedSkillItemTable>.Create();
		Singleton<StageTable>.Create();
		Singleton<StampTable>.Create();
		Singleton<StringTable>.Create();
		Singleton<TaskTable>.Create();
		Singleton<TutorialMessageTable>.Create();
		Singleton<UserLevelTable>.Create();
		Singleton<PointShopGetPointTable>.Create();
		Singleton<DegreeTable>.Create();
		Singleton<DamageDistanceTable>.Create();
		Singleton<GachaSearchEnemyTable>.Create();
		Singleton<HomeThemeTable>.Create();
		Singleton<CountdownTable>.Create();
		Singleton<BuffTable>.Create();
		Singleton<FieldBuffTable>.Create();
		Singleton<WaveMatchDropTable>.Create();
		Singleton<LimitedEquipItemExceedTable>.Create();
		Singleton<PlayDataTable>.Create();
		Singleton<ArenaTable>.Create();
		Singleton<EnemyAngryTable>.Create();
		Singleton<EnemyActionTable>.Create();
		Singleton<NpcLevelTable>.Create();
		Singleton<NpcLevelSpecialTable>.Create();
		Singleton<FieldMapEnemyPopTimeZoneTable>.Create();
		Singleton<GatherItemTable>.Create();
		Singleton<AssignedEquipmentTable>.Create();
		Singleton<SymbolTable>.Create();
		Singleton<ProductDataTable>.Create();
		RegisterTable("AbilityDataTable", Singleton<AbilityDataTable>.I);
		RegisterTable("AbilityTable", Singleton<AbilityTable>.I);
		RegisterTable("AbilityItemLotTable", Singleton<AbilityItemLotTable>.I);
		RegisterTable("AccessoryTable", new DataTableInterfaceProxy(Singleton<AccessoryTable>.I.CreateTable));
		RegisterTable("AccessoryInfoTable", new DataTableInterfaceProxy(Singleton<AccessoryTable>.I.CreateInfoTable));
		RegisterTable("AudioSettingTable", Singleton<AudioSettingTable>.I);
		RegisterTable("AvatarTable", Singleton<AvatarTable>.I);
		RegisterTable("CreateEquipItemTable", Singleton<CreateEquipItemTable>.I);
		RegisterTable("CreatePickupItemTable", Singleton<CreatePickupItemTable>.I);
		RegisterTable("DeliveryRewardTable", Singleton<DeliveryRewardTable>.I);
		RegisterTable("DeliveryTable", Singleton<DeliveryTable>.I);
		RegisterTable("EnemyTable", new DataTableInterfaceProxy(Singleton<EnemyTable>.I.CreateTable));
		RegisterTable("EquipItemExceedParamTable", Singleton<EquipItemExceedParamTable>.I);
		RegisterTable("EquipItemExceedTable", Singleton<EquipItemExceedTable>.I);
		RegisterTable("EquipItemTable", new DataTableInterfaceProxy(Singleton<EquipItemTable>.I.CreateTable));
		RegisterTable("EquipModelTable", Singleton<EquipModelTable>.I);
		RegisterTable("EvolveEquipItemTable", Singleton<EvolveEquipItemTable>.I);
		RegisterTable("GrowEnemyTable", Singleton<GrowEnemyTable>.I);
		RegisterTable("GrowSkillItemTable", new DataTableInterfaceProxy(Singleton<GrowSkillItemTable>.I.CreateTable));
		RegisterTable("ItemTable", Singleton<ItemTable>.I);
		RegisterTable("TutorialGearSetTable", new DataTableInterfaceProxy(Singleton<TutorialGearSetTable>.I.CreateTable));
		RegisterTable("TradingPostTable", new DataTableInterfaceProxy(Singleton<TradingPostTable>.I.CreateTable));
		RegisterTable("NPCMessageTable", Singleton<NPCMessageTable>.I);
		RegisterTable("NPCTable", Singleton<NPCTable>.I);
		RegisterTable("SETable", Singleton<SETable>.I);
		RegisterTable("SkillItemTable", Singleton<SkillItemTable>.I);
		RegisterTable("ExceedSkillItemTable", Singleton<ExceedSkillItemTable>.I);
		RegisterTable("StageTable", Singleton<StageTable>.I);
		RegisterTable("StampTypeTable", Singleton<StampTable>.I);
		RegisterTable("StringTable", Singleton<StringTable>.I);
		RegisterTable("TaskTable", Singleton<TaskTable>.I);
		RegisterTable("TutorialMessageTable", Singleton<TutorialMessageTable>.I);
		RegisterTable("UserLevelTable", Singleton<UserLevelTable>.I);
		RegisterTable("GachaSearchEnemyTable", Singleton<GachaSearchEnemyTable>.I);
		RegisterTable("HomeThemeTable", Singleton<HomeThemeTable>.I);
		RegisterTable("CountdownTable", Singleton<CountdownTable>.I);
		RegisterTable("LimitedEquipItemExceedTable", Singleton<LimitedEquipItemExceedTable>.I, "ItemTable");
		RegisterTable("PlayDataTable", Singleton<PlayDataTable>.I);
		RegisterTable("ArenaTable", Singleton<ArenaTable>.I);
		RegisterTable("EnemyAngryTable", Singleton<EnemyAngryTable>.I);
		RegisterTable("EnemyActionTable", Singleton<EnemyActionTable>.I);
		RegisterTable("NpcLevelTable", Singleton<NpcLevelTable>.I);
		RegisterTable("NpcLevelSpecialTable", Singleton<NpcLevelSpecialTable>.I);
		RegisterTable("AssignedEquipmentTable", Singleton<AssignedEquipmentTable>.I);
		RegisterTable("SymbolTable", Singleton<SymbolTable>.I);
		RegisterTable("GrowEquipItemTable", new DataTableInterfaceProxy(Singleton<GrowEquipItemTable>.I.CreateGrowTable), "ItemTable");
		RegisterTable("GrowEquipItemNeedItemTable", new DataTableInterfaceProxy(Singleton<GrowEquipItemTable>.I.CreateNeedTable), "ItemTable");
		RegisterTable("GrowEquipItemNeedUniqueItemTable", new DataTableInterfaceProxy(Singleton<GrowEquipItemTable>.I.CreateNeedUniqueTable), "ItemTable");
		RegisterTable("QuestTable", new DataTableInterfaceProxy(delegate(string csv)
		{
			Singleton<QuestTable>.I.CreateQuestTable(csv);
			afterProcesses.Add(delegate
			{
				Singleton<QuestTable>.I.InitQuestDependencyData();
			});
		}));
		RegisterTable("MissionTable", new DataTableInterfaceProxy(Singleton<QuestTable>.I.CreateMissionTable));
		RegisterTable("RegionTable", Singleton<RegionTable>.I);
		RegisterTable("FieldMapTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateFieldMapTable));
		RegisterTable("FieldMapPortalTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreatePortalTable));
		RegisterTable("FieldMapEnemyPopTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateEnemyPopTable));
		RegisterTable("FieldMapGatherPointTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateGatherPointTable));
		RegisterTable("FieldMapGatherPointViewTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateGatherPointViewTable));
		RegisterTable("FieldMapGimmickPointTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateGimmickPointTable));
		RegisterTable("FieldMapGimmickActionTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateGimmickActionTable));
		RegisterTable("QuestToFieldTable", new DataTableInterfaceProxy(delegate(string csv)
		{
			Singleton<QuestToFieldTable>.I.CreateTable(csv);
			afterProcesses.Add(delegate
			{
				Singleton<QuestToFieldTable>.I.InitDependencyData();
			});
		}));
		RegisterTable("ItemToFieldTable", new DataTableInterfaceProxy(delegate(string csv)
		{
			Singleton<ItemToFieldTable>.I.CreateTable(csv);
			afterProcesses.Add(delegate
			{
				Singleton<ItemToFieldTable>.I.InitDependencyData();
			});
		}));
		RegisterTable("EnemyHitTypeTable", Singleton<EnemyHitTypeTable>.I);
		RegisterTable("EnemyHitMaterialTable", Singleton<EnemyHitMaterialTable>.I, "EnemyHitTypeTable");
		RegisterTable("EnemyPersonalityTable", Singleton<EnemyPersonalityTable>.I);
		RegisterTable("PointShopGetPointTable", Singleton<PointShopGetPointTable>.I);
		RegisterTable("DegreeTable", Singleton<DegreeTable>.I);
		RegisterTable("DamageDistanceTable", Singleton<DamageDistanceTable>.I);
		RegisterTable("BuffTable", Singleton<BuffTable>.I);
		RegisterTable("FieldBuffTable", Singleton<FieldBuffTable>.I);
		RegisterTable("WaveMatchDropTable", Singleton<WaveMatchDropTable>.I);
		RegisterTable("FieldMapEnemyPopTimeZoneTable", Singleton<FieldMapEnemyPopTimeZoneTable>.I);
		RegisterTable("GatherItemTable", Singleton<GatherItemTable>.I);
		RegisterTable("ProductDataTable", Singleton<ProductDataTable>.I);
		UpdateDependency();
	}

	public void InitializeForDownload()
	{
		Clear();
		DataTableInterfaceProxy table = new DataTableInterfaceProxy(delegate
		{
		});
		RegisterTable("AvatarTable", table);
		RegisterTable("AccessoryTable", table);
		RegisterTable("AccessoryInfoTable", table);
		RegisterTable("AccessoryDataTable", table);
		RegisterTable("CreateEquipItemTable", table);
		RegisterTable("CreatePickupItemTable", table);
		RegisterTable("DeliveryTable", table);
		RegisterTable("EquipItemTable", table);
		RegisterTable("EquipModelTable", table);
		RegisterTable("GrowSkillItemTable", table);
		RegisterTable("HomeThemeTable", table);
		RegisterTable("CountdownTable", table);
		RegisterTable("NPCMessageTable", table);
		RegisterTable("NPCTable", table);
		RegisterTable("QuestTable", table);
		RegisterTable("SkillItemTable", table);
		RegisterTable("ExceedSkillItemTable", table);
		RegisterTable("StageTable", table);
		RegisterTable("TutorialMessageTable", table);
		RegisterTable("StampTypeTable", table);
		RegisterTable("EquipItemExceedParamTable", table);
		RegisterTable("AbilityDataTable", table);
		RegisterTable("AbilityTable", table);
		RegisterTable("AbilityItemLotTable", table);
		RegisterTable("AudioSettingTable", table);
		RegisterTable("DeliveryRewardTable", table);
		RegisterTable("EnemyTable", table);
		RegisterTable("EquipItemExceedTable", table);
		RegisterTable("EvolveEquipItemTable", table);
		RegisterTable("GrowEnemyTable", table);
		RegisterTable("ItemTable", table);
		RegisterTable("TutorialGearSetTable", table);
		RegisterTable("TradingPostTable", table);
		RegisterTable("SETable", table);
		RegisterTable("StringTable", table);
		RegisterTable("TaskTable", table);
		RegisterTable("UserLevelTable", table);
		RegisterTable("GrowEquipItemTable", table);
		RegisterTable("GrowEquipItemNeedItemTable", table);
		RegisterTable("GrowEquipItemNeedUniqueItemTable", table);
		RegisterTable("MissionTable", table);
		RegisterTable("RegionTable", table);
		RegisterTable("FieldMapTable", table);
		RegisterTable("FieldMapPortalTable", table);
		RegisterTable("FieldMapEnemyPopTable", table);
		RegisterTable("FieldMapGatherPointTable", table);
		RegisterTable("FieldMapGatherPointViewTable", table);
		RegisterTable("FieldMapGimmickPointTable", table);
		RegisterTable("FieldMapGimmickActionTable", table);
		RegisterTable("QuestToFieldTable", table);
		RegisterTable("ItemToFieldTable", table);
		RegisterTable("EnemyHitTypeTable", table);
		RegisterTable("EnemyHitMaterialTable", table);
		RegisterTable("EnemyPersonalityTable", table);
		RegisterTable("PointShopGetPointTable", table);
		RegisterTable("DegreeTable", table);
		RegisterTable("DamageDistanceTable", table);
		RegisterTable("GachaSearchEnemyTable", table);
		RegisterTable("BuffTable", table);
		RegisterTable("FieldBuffTable", table);
		RegisterTable("WaveMatchDropTable", table);
		RegisterTable("LimitedEquipItemExceedTable", table);
		RegisterTable("PlayDataTable", table);
		RegisterTable("ArenaTable", table);
		RegisterTable("EnemyAngryTable", table);
		RegisterTable("EnemyActionTable", table);
		RegisterTable("NpcLevelTable", table);
		RegisterTable("NpcLevelSpecialTable", table);
		RegisterTable("FieldMapEnemyPopTimeZoneTable", table);
		RegisterTable("GatherItemTable", table);
		RegisterTable("AssignedEquipmentTable", table);
		RegisterTable("SymbolTable", table);
		RegisterTable("ProductDataTable", table);
	}

	public void RegisterTable(string name, IDataTable table, string dependencyTableName = null)
	{
		tables.Add(name, new DataTableContainer(name, table));
		if (!string.IsNullOrEmpty(dependencyTableName))
		{
			unresolvedDependencies.Add(new KeyValuePair<string, string>(name, dependencyTableName));
		}
	}

	public void UnregisterTable(string name)
	{
		tables.Remove(name);
	}

	private void UpdateDependency()
	{
		int i = 0;
		for (int count = unresolvedDependencies.Count; i < count; i++)
		{
			KeyValuePair<string, string> keyValuePair = unresolvedDependencies[i];
			if (tables.TryGetValue(keyValuePair.Key, out DataTableContainer value))
			{
				if (tables.TryGetValue(keyValuePair.Value, out DataTableContainer value2))
				{
					value.SetDependency(value2);
				}
				else
				{
					Log.Error(LOG.DATA_TABLE, "Not found dependency table: {0} ---> {1}", keyValuePair.Key, keyValuePair.Value);
				}
			}
			else
			{
				Log.Error(LOG.DATA_TABLE, "Not found table: {0}", keyValuePair.Key);
			}
		}
	}

	public static string Decrypt(string encrypted_csv_text)
	{
		return Cipher.DecryptRJ128("Auto_XlS_To_CSV.", "yCNBH$$rCNGvC+#f", encrypted_csv_text);
	}

	public static byte[] DecryptToBytes(string encrypted_csv_text)
	{
		return Cipher.DecryptRJ128Byte("Auto_XlS_To_CSV.", "yCNBH$$rCNGvC+#f", encrypted_csv_text);
	}

	public static string DecompressToString(byte[] bytes)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		string text = null;
		using (MemoryStream memoryStream = new MemoryStream(bytes))
		{
			memoryStream.Seek(256L, SeekOrigin.Begin);
			ZlibStream val = new ZlibStream((Stream)memoryStream, 1);
			try
			{
				using (StreamReader streamReader = new StreamReader((Stream)val))
				{
					try
					{
						return streamReader.ReadToEnd();
					}
					catch (Exception)
					{
						throw;
					}
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
	}

	public static Action<byte[]> CreateCompressedBinaryTableProcess(Action<MemoryStream> create)
	{
		return delegate(byte[] bytes)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			MemoryStream memoryStream = new MemoryStream();
			using (MemoryStream memoryStream2 = new MemoryStream(bytes))
			{
				byte[] array = new byte[1024];
				ZlibStream val = new ZlibStream((Stream)memoryStream2, 1);
				try
				{
					try
					{
						int count;
						while ((count = ((Stream)val).Read(array, 0, array.Length)) != 0)
						{
							memoryStream.Write(array, 0, count);
						}
					}
					finally
					{
						((IDisposable)val)?.Dispose();
					}
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
			}
			memoryStream.Seek(0L, SeekOrigin.Begin);
			create(memoryStream);
		};
	}

	public void DumpManifest()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string allFileName in manifest.GetAllFileNames())
		{
			MD5Hash tableHash = manifest.GetTableHash(allFileName);
			stringBuilder.AppendLine($"{allFileName} : {tableHash.ToString()}");
		}
		Debug.Log((object)stringBuilder.ToString());
	}

	public void ChangePriorityTop(string tableName)
	{
		if (null != dataLoader)
		{
			dataLoader.ChangePriorityTop(tableName);
		}
	}

	public bool IsLoading()
	{
		return loadStatus != LoadStatus.LoadComplete;
	}

	public bool IsLoading(string tableName)
	{
		if (loadStatus == LoadStatus.LoadComplete)
		{
			return false;
		}
		if (loadStatus != LoadStatus.LoadingAllTable)
		{
			return true;
		}
		if (null != dataLoader)
		{
			return dataLoader.IsLoading(tableName);
		}
		return false;
	}

	public void LoadStory(string storyName, Action<string> onComplete)
	{
		DataTableInterfaceProxy table = new DataTableInterfaceProxy(onComplete);
		DataLoadRequest req = CreateRequestLoadTable(storyName, table);
		Request(req);
	}
}
