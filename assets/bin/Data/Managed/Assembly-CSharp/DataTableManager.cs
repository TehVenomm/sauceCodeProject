using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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

	public event Action<DataTableLoadError, Action> onError
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.onError = Delegate.Combine((Delegate)this.onError, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.onError = Delegate.Remove((Delegate)this.onError, (Delegate)value);
		}
	}

	protected override void Awake()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		cache = new DataTableCache(null);
		dataLoader = this.get_gameObject().AddComponent<DataLoader>();
		dataLoader.SetCache(new DataTableCache(null));
		forceLoadCSV = false;
		loadStatus = LoadStatus.NotInitialize;
	}

	public void OnReceiveTableManifestVersion(int version)
	{
		if (lastReceiveManifestVersion == version)
		{
			goto IL_000c;
		}
		goto IL_000c;
		IL_000c:
		lastReceiveManifestVersion = version;
	}

	public void OnReceiveVM(XorInt vm)
	{
		this.vm = vm;
	}

	public void UpdateManifest(Action onComplete)
	{
		int version = lastReceiveManifestVersion;
		DataLoadRequest dataLoadRequest = CreateRequest(MANIFEST_NAME, new ManifestVersion(version), DATA_TABLE_DIRECTORY, false);
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
				IL_0040:;
			}
		};
		dataLoadRequest.onComplete += onComplete;
		dataLoader.RequestManifest(dataLoadRequest);
	}

	public unsafe List<DataLoadRequest> LoadInitialTable(Action onComplete, bool downloadOnly = false)
	{
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		if (!downloadOnly)
		{
			loadStatus = LoadStatus.LoadingInitialTable;
		}
		RequestParam[] array = new RequestParam[23]
		{
			new RequestParam("AvatarTable", null),
			new RequestParam("AccessoryTable", null),
			new RequestParam("AccessoryInfoTable", null),
			new RequestParam("CreateEquipItemTable", null),
			new RequestParam("CreatePickupItemTable", null),
			new RequestParam("DeliveryTable", null),
			new RequestParam("EquipItemTable", null),
			new RequestParam("EquipModelTable", null),
			new RequestParam("GrowSkillItemTable", null),
			new RequestParam("HomeThemeTable", null),
			new RequestParam("CountdownTable", null),
			new RequestParam("NPCMessageTable", null),
			new RequestParam("NPCTable", null),
			new RequestParam("QuestTable", null),
			new RequestParam("SkillItemTable", null),
			new RequestParam("ExceedSkillItemTable", null),
			new RequestParam("StageTable", null),
			new RequestParam("TutorialMessageTable", null),
			new RequestParam("StampTypeTable", null),
			new RequestParam("EquipItemExceedParamTable", null),
			new RequestParam("RegionTable", null),
			new RequestParam("FieldMapTable", null),
			new RequestParam("FieldMapPortalTable", null)
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
			_003CLoadInitialTable_003Ec__AnonStorey6EF _003CLoadInitialTable_003Ec__AnonStorey6EF;
			item2.onComplete += new Action((object)_003CLoadInitialTable_003Ec__AnonStorey6EF, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		Request(list);
		return list;
	}

	public unsafe List<DataLoadRequest> LoadAllTable(Action onComplete, bool downloadOnly = false)
	{
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Expected O, but got Unknown
		RequestParam[] array = new RequestParam[48]
		{
			new RequestParam("AbilityDataTable", null),
			new RequestParam("AbilityTable", null),
			new RequestParam("AbilityItemLotTable", null),
			new RequestParam("AudioSettingTable", null),
			new RequestParam("DeliveryRewardTable", null),
			new RequestParam("EnemyTable", null),
			new RequestParam("EquipItemExceedTable", null),
			new RequestParam("EvolveEquipItemTable", null),
			new RequestParam("GrowEnemyTable", null),
			new RequestParam("ItemTable", null),
			new RequestParam("SETable", null),
			new RequestParam("StringTable", null),
			new RequestParam("TaskTable", null),
			new RequestParam("UserLevelTable", null),
			new RequestParam("GrowEquipItemTable", null),
			new RequestParam("GrowEquipItemNeedItemTable", null),
			new RequestParam("GrowEquipItemNeedUniqueItemTable", null),
			new RequestParam("MissionTable", null),
			new RequestParam("RegionTable", null),
			new RequestParam("FieldMapTable", null),
			new RequestParam("FieldMapPortalTable", null),
			new RequestParam("FieldMapEnemyPopTable", null),
			new RequestParam("FieldMapGatherPointTable", null),
			new RequestParam("FieldMapGatherPointViewTable", null),
			new RequestParam("FieldMapGimmickPointTable", null),
			new RequestParam("FieldMapGimmickActionTable", null),
			new RequestParam("QuestToFieldTable", null),
			new RequestParam("ItemToFieldTable", null),
			new RequestParam("EnemyHitTypeTable", null),
			new RequestParam("EnemyHitMaterialTable", null),
			new RequestParam("EnemyPersonalityTable", null),
			new RequestParam("PointShopGetPointTable", null),
			new RequestParam("DegreeTable", null),
			new RequestParam("DamageDistanceTable", null),
			new RequestParam("GachaSearchEnemyTable", null),
			new RequestParam("BuffTable", null),
			new RequestParam("FieldBuffTable", null),
			new RequestParam("WaveMatchDropTable", null),
			new RequestParam("LimitedEquipItemExceedTable", null),
			new RequestParam("PlayDataTable", null),
			new RequestParam("ArenaTable", null),
			new RequestParam("EnemyAngryTable", null),
			new RequestParam("EnemyActionTable", null),
			new RequestParam("NpcLevelTable", null),
			new RequestParam("NpcLevelSpecialTable", null),
			new RequestParam("FieldMapEnemyPopTimeZoneTable", null),
			new RequestParam("GatherItemTable", null),
			new RequestParam("AssignedEquipmentTable", null)
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
			_003CLoadAllTable_003Ec__AnonStorey6F0 _003CLoadAllTable_003Ec__AnonStorey6F;
			item2.onComplete += new Action((object)_003CLoadAllTable_003Ec__AnonStorey6F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
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
			if (tables.TryGetValue(dataLoadRequest.name, out value))
			{
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
	}

	public DataLoadRequest RequestLoadTable(string name, IDataTable table, Action onComplete, bool downloadOnly = false)
	{
		DataLoadRequest dataLoadRequest = CreateRequestLoadTable(name, table, downloadOnly, null);
		dataLoadRequest.onComplete += onComplete;
		Request(dataLoadRequest);
		return dataLoadRequest;
	}

	public DataLoadRequest RequestLoadTable(string name, Action onComplete, bool downloadOnly = false)
	{
		tables.TryGetValue(name, out DataTableContainer value);
		DataLoadRequest dataLoadRequest = CreateRequestLoadTable(name, value, downloadOnly, null);
		dataLoadRequest.onComplete += onComplete;
		Request(dataLoadRequest);
		return dataLoadRequest;
	}

	public DataLoadRequest RequestLoadTable(string name, Action<byte[]> processBinaryData, Action onComplete, bool downloadOnly = false)
	{
		tables.TryGetValue(name, out DataTableContainer value);
		DataLoadRequest dataLoadRequest = CreateRequestLoadTable(name, value, downloadOnly, null);
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

	private unsafe DataLoadRequest CreateRequest(string name, IDataTableRequestHash hash, string directory, bool downloadOnly = false)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		DataLoadRequest req = new DataLoadRequest(name, hash, directory, downloadOnly);
		_003CCreateRequest_003Ec__AnonStorey6F3 _003CCreateRequest_003Ec__AnonStorey6F;
		req.onVerifyError += new Func<string, bool>((object)_003CCreateRequest_003Ec__AnonStorey6F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		req.onError += delegate(DataTableLoadError error)
		{
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Expected O, but got Unknown
			Log.Error(LOG.DATA_TABLE, "load error ({1}): {0}", req.name, error.ToString());
			erroredRequests.Add(req);
			cache.Remove(req);
			if (this.onError != null)
			{
				this.onError.Invoke(error, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		};
		req.onComplete += new Action((object)_003CCreateRequest_003Ec__AnonStorey6F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
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
			afterProcess.Invoke();
		}
	}

	public void Clear()
	{
		this.StopAllCoroutines();
		tables.Clear();
		erroredRequests.Clear();
	}

	public unsafe void Initialize()
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
		RegisterTable("AbilityDataTable", Singleton<AbilityDataTable>.I, null);
		RegisterTable("AbilityTable", Singleton<AbilityTable>.I, null);
		RegisterTable("AbilityItemLotTable", Singleton<AbilityItemLotTable>.I, null);
		AccessoryTable i = Singleton<AccessoryTable>.I;
		RegisterTable("AccessoryTable", new DataTableInterfaceProxy(i.CreateTable), null);
		RegisterTable("AccessoryInfoTable", new DataTableInterfaceProxy(Singleton<AccessoryTable>.I.CreateInfoTable), null);
		RegisterTable("AudioSettingTable", Singleton<AudioSettingTable>.I, null);
		RegisterTable("AvatarTable", Singleton<AvatarTable>.I, null);
		RegisterTable("CreateEquipItemTable", Singleton<CreateEquipItemTable>.I, null);
		RegisterTable("CreatePickupItemTable", Singleton<CreatePickupItemTable>.I, null);
		RegisterTable("DeliveryRewardTable", Singleton<DeliveryRewardTable>.I, null);
		RegisterTable("DeliveryTable", Singleton<DeliveryTable>.I, null);
		EnemyTable i2 = Singleton<EnemyTable>.I;
		RegisterTable("EnemyTable", new DataTableInterfaceProxy(i2.CreateTable), null);
		RegisterTable("EquipItemExceedParamTable", Singleton<EquipItemExceedParamTable>.I, null);
		RegisterTable("EquipItemExceedTable", Singleton<EquipItemExceedTable>.I, null);
		EquipItemTable i3 = Singleton<EquipItemTable>.I;
		RegisterTable("EquipItemTable", new DataTableInterfaceProxy(i3.CreateTable), null);
		RegisterTable("EquipModelTable", Singleton<EquipModelTable>.I, null);
		RegisterTable("EvolveEquipItemTable", Singleton<EvolveEquipItemTable>.I, null);
		RegisterTable("GrowEnemyTable", Singleton<GrowEnemyTable>.I, null);
		GrowSkillItemTable i4 = Singleton<GrowSkillItemTable>.I;
		RegisterTable("GrowSkillItemTable", new DataTableInterfaceProxy(i4.CreateTable), null);
		RegisterTable("ItemTable", Singleton<ItemTable>.I, null);
		RegisterTable("NPCMessageTable", Singleton<NPCMessageTable>.I, null);
		RegisterTable("NPCTable", Singleton<NPCTable>.I, null);
		RegisterTable("SETable", Singleton<SETable>.I, null);
		RegisterTable("SkillItemTable", Singleton<SkillItemTable>.I, null);
		RegisterTable("ExceedSkillItemTable", Singleton<ExceedSkillItemTable>.I, null);
		RegisterTable("StageTable", Singleton<StageTable>.I, null);
		RegisterTable("StampTypeTable", Singleton<StampTable>.I, null);
		RegisterTable("StringTable", Singleton<StringTable>.I, null);
		RegisterTable("TaskTable", Singleton<TaskTable>.I, null);
		RegisterTable("TutorialMessageTable", Singleton<TutorialMessageTable>.I, null);
		RegisterTable("UserLevelTable", Singleton<UserLevelTable>.I, null);
		RegisterTable("GachaSearchEnemyTable", Singleton<GachaSearchEnemyTable>.I, null);
		RegisterTable("HomeThemeTable", Singleton<HomeThemeTable>.I, null);
		RegisterTable("CountdownTable", Singleton<CountdownTable>.I, null);
		RegisterTable("LimitedEquipItemExceedTable", Singleton<LimitedEquipItemExceedTable>.I, "ItemTable");
		RegisterTable("PlayDataTable", Singleton<PlayDataTable>.I, null);
		RegisterTable("ArenaTable", Singleton<ArenaTable>.I, null);
		RegisterTable("EnemyAngryTable", Singleton<EnemyAngryTable>.I, null);
		RegisterTable("EnemyActionTable", Singleton<EnemyActionTable>.I, null);
		RegisterTable("NpcLevelTable", Singleton<NpcLevelTable>.I, null);
		RegisterTable("NpcLevelSpecialTable", Singleton<NpcLevelSpecialTable>.I, null);
		RegisterTable("AssignedEquipmentTable", Singleton<AssignedEquipmentTable>.I, null);
		RegisterTable("GrowEquipItemTable", new DataTableInterfaceProxy(Singleton<GrowEquipItemTable>.I.CreateGrowTable), "ItemTable");
		RegisterTable("GrowEquipItemNeedItemTable", new DataTableInterfaceProxy(Singleton<GrowEquipItemTable>.I.CreateNeedTable), "ItemTable");
		RegisterTable("GrowEquipItemNeedUniqueItemTable", new DataTableInterfaceProxy(Singleton<GrowEquipItemTable>.I.CreateNeedUniqueTable), "ItemTable");
		RegisterTable("QuestTable", new DataTableInterfaceProxy(delegate(string csv)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			Singleton<QuestTable>.I.CreateQuestTable(csv);
			List<Action> list3 = afterProcesses;
			if (_003C_003Ef__am_0024cache11 == null)
			{
				_003C_003Ef__am_0024cache11 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			list3.Add(_003C_003Ef__am_0024cache11);
		}), null);
		RegisterTable("MissionTable", new DataTableInterfaceProxy(Singleton<QuestTable>.I.CreateMissionTable), null);
		RegisterTable("RegionTable", Singleton<RegionTable>.I, null);
		RegisterTable("FieldMapTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateFieldMapTable), null);
		RegisterTable("FieldMapPortalTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreatePortalTable), null);
		RegisterTable("FieldMapEnemyPopTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateEnemyPopTable), null);
		RegisterTable("FieldMapGatherPointTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateGatherPointTable), null);
		RegisterTable("FieldMapGatherPointViewTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateGatherPointViewTable), null);
		RegisterTable("FieldMapGimmickPointTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateGimmickPointTable), null);
		RegisterTable("FieldMapGimmickActionTable", new DataTableInterfaceProxy(Singleton<FieldMapTable>.I.CreateGimmickActionTable), null);
		RegisterTable("QuestToFieldTable", new DataTableInterfaceProxy(delegate(string csv)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			Singleton<QuestToFieldTable>.I.CreateTable(csv);
			List<Action> list2 = afterProcesses;
			if (_003C_003Ef__am_0024cache12 == null)
			{
				_003C_003Ef__am_0024cache12 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			list2.Add(_003C_003Ef__am_0024cache12);
		}), null);
		RegisterTable("ItemToFieldTable", new DataTableInterfaceProxy(delegate(string csv)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			Singleton<ItemToFieldTable>.I.CreateTable(csv);
			List<Action> list = afterProcesses;
			if (_003C_003Ef__am_0024cache13 == null)
			{
				_003C_003Ef__am_0024cache13 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			list.Add(_003C_003Ef__am_0024cache13);
		}), null);
		RegisterTable("EnemyHitTypeTable", Singleton<EnemyHitTypeTable>.I, null);
		RegisterTable("EnemyHitMaterialTable", Singleton<EnemyHitMaterialTable>.I, "EnemyHitTypeTable");
		RegisterTable("EnemyPersonalityTable", Singleton<EnemyPersonalityTable>.I, null);
		RegisterTable("PointShopGetPointTable", Singleton<PointShopGetPointTable>.I, null);
		RegisterTable("DegreeTable", Singleton<DegreeTable>.I, null);
		RegisterTable("DamageDistanceTable", Singleton<DamageDistanceTable>.I, null);
		RegisterTable("BuffTable", Singleton<BuffTable>.I, null);
		RegisterTable("FieldBuffTable", Singleton<FieldBuffTable>.I, null);
		RegisterTable("WaveMatchDropTable", Singleton<WaveMatchDropTable>.I, null);
		RegisterTable("FieldMapEnemyPopTimeZoneTable", Singleton<FieldMapEnemyPopTimeZoneTable>.I, null);
		RegisterTable("GatherItemTable", Singleton<GatherItemTable>.I, null);
		UpdateDependency();
	}

	public void InitializeForDownload()
	{
		Clear();
		DataTableInterfaceProxy table = new DataTableInterfaceProxy(delegate
		{
		});
		RegisterTable("AvatarTable", table, null);
		RegisterTable("AccessoryTable", table, null);
		RegisterTable("AccessoryInfoTable", table, null);
		RegisterTable("AccessoryDataTable", table, null);
		RegisterTable("CreateEquipItemTable", table, null);
		RegisterTable("CreatePickupItemTable", table, null);
		RegisterTable("DeliveryTable", table, null);
		RegisterTable("EquipItemTable", table, null);
		RegisterTable("EquipModelTable", table, null);
		RegisterTable("GrowSkillItemTable", table, null);
		RegisterTable("HomeThemeTable", table, null);
		RegisterTable("CountdownTable", table, null);
		RegisterTable("NPCMessageTable", table, null);
		RegisterTable("NPCTable", table, null);
		RegisterTable("QuestTable", table, null);
		RegisterTable("SkillItemTable", table, null);
		RegisterTable("ExceedSkillItemTable", table, null);
		RegisterTable("StageTable", table, null);
		RegisterTable("TutorialMessageTable", table, null);
		RegisterTable("StampTypeTable", table, null);
		RegisterTable("EquipItemExceedParamTable", table, null);
		RegisterTable("AbilityDataTable", table, null);
		RegisterTable("AbilityTable", table, null);
		RegisterTable("AbilityItemLotTable", table, null);
		RegisterTable("AudioSettingTable", table, null);
		RegisterTable("DeliveryRewardTable", table, null);
		RegisterTable("EnemyTable", table, null);
		RegisterTable("EquipItemExceedTable", table, null);
		RegisterTable("EvolveEquipItemTable", table, null);
		RegisterTable("GrowEnemyTable", table, null);
		RegisterTable("ItemTable", table, null);
		RegisterTable("SETable", table, null);
		RegisterTable("StringTable", table, null);
		RegisterTable("TaskTable", table, null);
		RegisterTable("UserLevelTable", table, null);
		RegisterTable("GrowEquipItemTable", table, null);
		RegisterTable("GrowEquipItemNeedItemTable", table, null);
		RegisterTable("GrowEquipItemNeedUniqueItemTable", table, null);
		RegisterTable("MissionTable", table, null);
		RegisterTable("RegionTable", table, null);
		RegisterTable("FieldMapTable", table, null);
		RegisterTable("FieldMapPortalTable", table, null);
		RegisterTable("FieldMapEnemyPopTable", table, null);
		RegisterTable("FieldMapGatherPointTable", table, null);
		RegisterTable("FieldMapGatherPointViewTable", table, null);
		RegisterTable("FieldMapGimmickPointTable", table, null);
		RegisterTable("FieldMapGimmickActionTable", table, null);
		RegisterTable("QuestToFieldTable", table, null);
		RegisterTable("ItemToFieldTable", table, null);
		RegisterTable("EnemyHitTypeTable", table, null);
		RegisterTable("EnemyHitMaterialTable", table, null);
		RegisterTable("EnemyPersonalityTable", table, null);
		RegisterTable("PointShopGetPointTable", table, null);
		RegisterTable("DegreeTable", table, null);
		RegisterTable("DamageDistanceTable", table, null);
		RegisterTable("GachaSearchEnemyTable", table, null);
		RegisterTable("BuffTable", table, null);
		RegisterTable("FieldBuffTable", table, null);
		RegisterTable("WaveMatchDropTable", table, null);
		RegisterTable("LimitedEquipItemExceedTable", table, null);
		RegisterTable("PlayDataTable", table, null);
		RegisterTable("ArenaTable", table, null);
		RegisterTable("EnemyAngryTable", table, null);
		RegisterTable("EnemyActionTable", table, null);
		RegisterTable("NpcLevelTable", table, null);
		RegisterTable("NpcLevelSpecialTable", table, null);
		RegisterTable("FieldMapEnemyPopTimeZoneTable", table, null);
		RegisterTable("GatherItemTable", table, null);
		RegisterTable("AssignedEquipmentTable", table, null);
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
		//IL_001e: Expected O, but got Unknown
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
						IL_0035:
						return text;
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
			//IL_001f: Expected O, but got Unknown
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
						while ((count = val.Read(array, 0, array.Length)) != 0)
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
		DataLoadRequest req = CreateRequestLoadTable(storyName, table, false, null);
		Request(req);
	}
}