// Decompiled with JetBrains decompiler
// Type: DataTableManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
public class DataTableManager : MonoBehaviourSingleton<DataTableManager>
{
  private static string MANIFEST_NAME = nameof (manifest);
  private DataTableManifest manifest;
  public DataTableCache cache;
  private Dictionary<string, DataTableManager.DataTableContainer> tables = new Dictionary<string, DataTableManager.DataTableContainer>();
  private List<DataLoadRequest> erroredRequests = new List<DataLoadRequest>();
  private DataTableManager.LoadStatus loadStatus;
  public DataLoader dataLoader;
  private XorInt vm = new XorInt(1);
  private int lastReceiveManifestVersion = -1;
  private static readonly string DATA_TABLE_DIRECTORY = nameof (tables);
  private List<DataLoadRequest> verifyErroredRequest = new List<DataLoadRequest>();
  private List<System.Action> afterProcesses = new List<System.Action>();
  private List<KeyValuePair<string, string>> unresolvedDependencies = new List<KeyValuePair<string, string>>();

  public event Action<DataTableLoadError, System.Action> onError;

  public bool shouldUpdateManifest
  {
    get => this.manifest == null || this.lastReceiveManifestVersion != this.manifest.version;
  }

  public int manifestVersion => this.manifest == null ? -1 : this.manifest.version;

  public bool hasManifest => this.manifest != null;

  public bool reportOnly => (int) this.vm == 1;

  public bool forceLoadCSV { get; set; }

  protected override void Awake()
  {
    base.Awake();
    this.cache = new DataTableCache();
    this.dataLoader = ((Component) this).gameObject.AddComponent<DataLoader>();
    this.dataLoader.SetCache((DataCache) new DataTableCache());
    this.forceLoadCSV = false;
    this.loadStatus = DataTableManager.LoadStatus.NotInitialize;
  }

  public void OnReceiveTableManifestVersion(int version)
  {
    int receiveManifestVersion = this.lastReceiveManifestVersion;
    this.lastReceiveManifestVersion = version;
  }

  public void OnReceiveVM(XorInt vm) => this.vm = vm;

  public void UpdateManifest(System.Action onComplete)
  {
    int version = this.lastReceiveManifestVersion;
    DataLoadRequest request = this.CreateRequest(DataTableManager.MANIFEST_NAME, (IDataTableRequestHash) new ManifestVersion(version), DataTableManager.DATA_TABLE_DIRECTORY);
    request.processCompressedTextData = (Action<byte[]>) (bytes =>
    {
      try
      {
        this.manifest = DataTableManifest.Create(DataTableManager.DecompressToString(bytes), version);
      }
      catch (Exception ex)
      {
        Log.Error(LOG.DATA_TABLE, "manifest load error: {0}", (object) ex.ToString());
        throw;
      }
    });
    request.onComplete += onComplete;
    this.dataLoader.RequestManifest(request);
  }

  public List<DataLoadRequest> LoadInitialTable(System.Action onComplete, bool downloadOnly = false)
  {
    if (!downloadOnly)
      this.loadStatus = DataTableManager.LoadStatus.LoadingInitialTable;
    DataTableManager.RequestParam[] requestParamArray = new DataTableManager.RequestParam[23]
    {
      new DataTableManager.RequestParam("AvatarTable"),
      new DataTableManager.RequestParam("AccessoryTable"),
      new DataTableManager.RequestParam("AccessoryInfoTable"),
      new DataTableManager.RequestParam("CreateEquipItemTable"),
      new DataTableManager.RequestParam("CreatePickupItemTable"),
      new DataTableManager.RequestParam("DeliveryTable"),
      new DataTableManager.RequestParam("EquipItemTable"),
      new DataTableManager.RequestParam("EquipModelTable"),
      new DataTableManager.RequestParam("GrowSkillItemTable"),
      new DataTableManager.RequestParam("HomeThemeTable"),
      new DataTableManager.RequestParam("CountdownTable"),
      new DataTableManager.RequestParam("NPCMessageTable"),
      new DataTableManager.RequestParam("NPCTable"),
      new DataTableManager.RequestParam("QuestTable"),
      new DataTableManager.RequestParam("SkillItemTable"),
      new DataTableManager.RequestParam("ExceedSkillItemTable"),
      new DataTableManager.RequestParam("StageTable"),
      new DataTableManager.RequestParam("TutorialMessageTable"),
      new DataTableManager.RequestParam("StampTypeTable"),
      new DataTableManager.RequestParam("EquipItemExceedParamTable"),
      new DataTableManager.RequestParam("RegionTable"),
      new DataTableManager.RequestParam("FieldMapTable"),
      new DataTableManager.RequestParam("FieldMapPortalTable")
    };
    List<DataLoadRequest> reqs = new List<DataLoadRequest>();
    int index = 0;
    for (int length = requestParamArray.Length; index < length; ++index)
    {
      DataTableManager.RequestParam requestParam = requestParamArray[index];
      DataLoadRequest requestLoadTable = this.CreateRequestLoadTable(requestParam.tableName, downloadOnly, requestParam.processBinary);
      reqs.Add(requestLoadTable);
    }
    this.SetDepends(reqs);
    int reqCount = reqs.Count;
    foreach (DataLoadRequest dataLoadRequest in reqs)
      dataLoadRequest.onComplete += (System.Action) (() =>
      {
        --reqCount;
        if (reqCount > 0)
          return;
        onComplete();
        if (downloadOnly)
          return;
        this.loadStatus = DataTableManager.LoadStatus.LoadingAllTable;
      });
    this.Request(reqs);
    return reqs;
  }

  public List<DataLoadRequest> LoadAllTable(System.Action onComplete, bool downloadOnly = false)
  {
    DataTableManager.RequestParam[] requestParamArray = new DataTableManager.RequestParam[52]
    {
      new DataTableManager.RequestParam("AbilityDataTable"),
      new DataTableManager.RequestParam("AbilityTable"),
      new DataTableManager.RequestParam("AbilityItemLotTable"),
      new DataTableManager.RequestParam("AudioSettingTable"),
      new DataTableManager.RequestParam("DeliveryRewardTable"),
      new DataTableManager.RequestParam("EnemyTable"),
      new DataTableManager.RequestParam("EquipItemExceedTable"),
      new DataTableManager.RequestParam("EvolveEquipItemTable"),
      new DataTableManager.RequestParam("GrowEnemyTable"),
      new DataTableManager.RequestParam("ItemTable"),
      new DataTableManager.RequestParam("TutorialGearSetTable"),
      new DataTableManager.RequestParam("TradingPostTable"),
      new DataTableManager.RequestParam("SETable"),
      new DataTableManager.RequestParam("StringTable"),
      new DataTableManager.RequestParam("TaskTable"),
      new DataTableManager.RequestParam("UserLevelTable"),
      new DataTableManager.RequestParam("GrowEquipItemTable"),
      new DataTableManager.RequestParam("GrowEquipItemNeedItemTable"),
      new DataTableManager.RequestParam("GrowEquipItemNeedUniqueItemTable"),
      new DataTableManager.RequestParam("MissionTable"),
      new DataTableManager.RequestParam("RegionTable"),
      new DataTableManager.RequestParam("FieldMapTable"),
      new DataTableManager.RequestParam("FieldMapPortalTable"),
      new DataTableManager.RequestParam("FieldMapEnemyPopTable"),
      new DataTableManager.RequestParam("FieldMapGatherPointTable"),
      new DataTableManager.RequestParam("FieldMapGatherPointViewTable"),
      new DataTableManager.RequestParam("FieldMapGimmickPointTable"),
      new DataTableManager.RequestParam("FieldMapGimmickActionTable"),
      new DataTableManager.RequestParam("QuestToFieldTable"),
      new DataTableManager.RequestParam("ItemToFieldTable"),
      new DataTableManager.RequestParam("EnemyHitTypeTable"),
      new DataTableManager.RequestParam("EnemyHitMaterialTable"),
      new DataTableManager.RequestParam("EnemyPersonalityTable"),
      new DataTableManager.RequestParam("PointShopGetPointTable"),
      new DataTableManager.RequestParam("DegreeTable"),
      new DataTableManager.RequestParam("DamageDistanceTable"),
      new DataTableManager.RequestParam("GachaSearchEnemyTable"),
      new DataTableManager.RequestParam("BuffTable"),
      new DataTableManager.RequestParam("FieldBuffTable"),
      new DataTableManager.RequestParam("WaveMatchDropTable"),
      new DataTableManager.RequestParam("LimitedEquipItemExceedTable"),
      new DataTableManager.RequestParam("PlayDataTable"),
      new DataTableManager.RequestParam("ArenaTable"),
      new DataTableManager.RequestParam("EnemyAngryTable"),
      new DataTableManager.RequestParam("EnemyActionTable"),
      new DataTableManager.RequestParam("NpcLevelTable"),
      new DataTableManager.RequestParam("NpcLevelSpecialTable"),
      new DataTableManager.RequestParam("FieldMapEnemyPopTimeZoneTable"),
      new DataTableManager.RequestParam("GatherItemTable"),
      new DataTableManager.RequestParam("AssignedEquipmentTable"),
      new DataTableManager.RequestParam("SymbolTable"),
      new DataTableManager.RequestParam("ProductDataTable")
    };
    List<DataLoadRequest> reqs = new List<DataLoadRequest>();
    int index = 0;
    for (int length = requestParamArray.Length; index < length; ++index)
    {
      DataTableManager.RequestParam requestParam = requestParamArray[index];
      DataLoadRequest requestLoadTable = this.CreateRequestLoadTable(requestParam.tableName, downloadOnly, requestParam.processBinary);
      reqs.Add(requestLoadTable);
    }
    this.SetDepends(reqs);
    int reqCount = reqs.Count;
    foreach (DataLoadRequest dataLoadRequest in reqs)
      dataLoadRequest.onComplete += (System.Action) (() =>
      {
        --reqCount;
        if (reqCount > 0)
          return;
        onComplete();
        if (downloadOnly)
          return;
        this.loadStatus = DataTableManager.LoadStatus.LoadComplete;
      });
    this.Request(reqs);
    return reqs;
  }

  private void SetDepends(List<DataLoadRequest> reqs)
  {
    int index = 0;
    for (int count = reqs.Count; index < count; ++index)
    {
      DataLoadRequest req = reqs[index];
      DataTableManager.DataTableContainer dataTableContainer = (DataTableManager.DataTableContainer) null;
      if (this.tables.TryGetValue(req.name, out dataTableContainer))
      {
        DataTableManager.DataTableContainer dependency = dataTableContainer.GetDependency();
        if (dependency != null)
        {
          DataLoadRequest depReq = reqs.Find((Predicate<DataLoadRequest>) (o => o.name == dependency.name));
          if (depReq != null)
            req.DependsOn(depReq);
        }
      }
    }
  }

  public DataLoadRequest RequestLoadTable(
    string name,
    IDataTable table,
    System.Action onComplete,
    bool downloadOnly = false)
  {
    DataLoadRequest requestLoadTable = this.CreateRequestLoadTable(name, table, downloadOnly);
    requestLoadTable.onComplete += onComplete;
    this.Request(requestLoadTable);
    return requestLoadTable;
  }

  public DataLoadRequest RequestLoadTable(string name, System.Action onComplete, bool downloadOnly = false)
  {
    DataTableManager.DataTableContainer table;
    this.tables.TryGetValue(name, out table);
    DataLoadRequest requestLoadTable = this.CreateRequestLoadTable(name, (IDataTable) table, downloadOnly);
    requestLoadTable.onComplete += onComplete;
    this.Request(requestLoadTable);
    return requestLoadTable;
  }

  public DataLoadRequest RequestLoadTable(
    string name,
    Action<byte[]> processBinaryData,
    System.Action onComplete,
    bool downloadOnly = false)
  {
    DataTableManager.DataTableContainer table;
    this.tables.TryGetValue(name, out table);
    DataLoadRequest requestLoadTable = this.CreateRequestLoadTable(name, (IDataTable) table, downloadOnly);
    requestLoadTable.onComplete += onComplete;
    if (processBinaryData != null)
      requestLoadTable.processCompressedBinaryData = processBinaryData;
    this.Request(requestLoadTable);
    return requestLoadTable;
  }

  private DataLoadRequest CreateRequestLoadTable(
    string name,
    bool downloadOnly = false,
    Action<byte[]> processBinary = null)
  {
    DataTableManager.DataTableContainer table;
    this.tables.TryGetValue(name, out table);
    return this.CreateRequestLoadTable(name, (IDataTable) table, downloadOnly, processBinary);
  }

  private DataLoadRequest CreateRequestLoadTable(
    string name,
    IDataTable table,
    bool downloadOnly = false,
    Action<byte[]> processBinary = null)
  {
    if (downloadOnly || this.forceLoadCSV)
      processBinary = (Action<byte[]>) null;
    DataLoadRequest request = this.CreateRequest(name, (IDataTableRequestHash) this.manifest.GetTableHash(name), DataTableManager.DATA_TABLE_DIRECTORY, downloadOnly);
    request.processCompressedTextData = (Action<byte[]>) (bytes =>
    {
      if (table == null)
        return;
      string csv = bytes.Length >= 256 /*0x0100*/ ? DataTableManager.DecompressToString(bytes) : throw new ApplicationException("seek error");
      if (!string.IsNullOrEmpty(csv))
        table.CreateTable(csv);
      else if (csv == null)
        throw new ApplicationException();
    });
    if (processBinary != null)
      request.SetupLoadBinary(this.manifest, processBinary);
    return request;
  }

  private DataLoadRequest CreateRequest(
    string name,
    IDataTableRequestHash hash,
    string directory,
    bool downloadOnly = false)
  {
    DataLoadRequest req = new DataLoadRequest(name, hash, directory, downloadOnly);
    req.onVerifyError += (Func<string, bool>) (filehash =>
    {
      this.ReportVerifyError(name, filehash);
      if (this.reportOnly)
      {
        Log.Error(LOG.DATA_TABLE, "VerifyError(report-only): {0}", (object) req.name);
        return true;
      }
      if (!this.verifyErroredRequest.Contains(req))
      {
        Log.Error(LOG.DATA_TABLE, "VerifyError(auto-retry): {0}", (object) req.name);
        this.cache.Remove(req);
        this.verifyErroredRequest.Add(req);
      }
      return false;
    });
    req.onError += (Action<DataTableLoadError>) (error =>
    {
      Log.Error(LOG.DATA_TABLE, "load error ({1}): {0}", (object) req.name, (object) error.ToString());
      this.erroredRequests.Add(req);
      this.cache.Remove(req);
      if (this.onError == null)
        return;
      this.onError(error, new System.Action(this.Retry));
    });
    req.onComplete += (System.Action) (() => this.verifyErroredRequest.Remove(req));
    return req;
  }

  private void Request(List<DataLoadRequest> reqs) => this.dataLoader.Request(reqs);

  private void Request(DataLoadRequest req) => this.dataLoader.Request(req);

  private void Retry()
  {
    int index = 0;
    for (int count = this.erroredRequests.Count; index < count; ++index)
    {
      DataLoadRequest erroredRequest = this.erroredRequests[index];
      erroredRequest.Reset();
      this.dataLoader.Request(erroredRequest);
    }
    this.erroredRequests.Clear();
  }

  private void ReportVerifyError(string filename, string filehash)
  {
    Protocol.Send<ReportVerifyModel.RequestSendForm, ReportVerifyModel>(ReportVerifyModel.URL, new ReportVerifyModel.RequestSendForm()
    {
      fileName = filename.ToLower(),
      fileHash = filehash
    }, (Action<ReportVerifyModel>) (model => { }));
  }

  private void AfterAllLoad()
  {
    foreach (System.Action afterProcess in this.afterProcesses)
      afterProcess();
  }

  public void Clear()
  {
    this.StopAllCoroutines();
    this.tables.Clear();
    this.erroredRequests.Clear();
  }

  public void Initialize()
  {
    this.Clear();
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
    this.RegisterTable("AbilityDataTable", (IDataTable) Singleton<AbilityDataTable>.I);
    this.RegisterTable("AbilityTable", (IDataTable) Singleton<AbilityTable>.I);
    this.RegisterTable("AbilityItemLotTable", (IDataTable) Singleton<AbilityItemLotTable>.I);
    this.RegisterTable("AccessoryTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<AccessoryTable>.I.CreateTable)));
    this.RegisterTable("AccessoryInfoTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<AccessoryTable>.I.CreateInfoTable)));
    this.RegisterTable("AudioSettingTable", (IDataTable) Singleton<AudioSettingTable>.I);
    this.RegisterTable("AvatarTable", (IDataTable) Singleton<AvatarTable>.I);
    this.RegisterTable("CreateEquipItemTable", (IDataTable) Singleton<CreateEquipItemTable>.I);
    this.RegisterTable("CreatePickupItemTable", (IDataTable) Singleton<CreatePickupItemTable>.I);
    this.RegisterTable("DeliveryRewardTable", (IDataTable) Singleton<DeliveryRewardTable>.I);
    this.RegisterTable("DeliveryTable", (IDataTable) Singleton<DeliveryTable>.I);
    this.RegisterTable("EnemyTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<EnemyTable>.I.CreateTable)));
    this.RegisterTable("EquipItemExceedParamTable", (IDataTable) Singleton<EquipItemExceedParamTable>.I);
    this.RegisterTable("EquipItemExceedTable", (IDataTable) Singleton<EquipItemExceedTable>.I);
    this.RegisterTable("EquipItemTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<EquipItemTable>.I.CreateTable)));
    this.RegisterTable("EquipModelTable", (IDataTable) Singleton<EquipModelTable>.I);
    this.RegisterTable("EvolveEquipItemTable", (IDataTable) Singleton<EvolveEquipItemTable>.I);
    this.RegisterTable("GrowEnemyTable", (IDataTable) Singleton<GrowEnemyTable>.I);
    this.RegisterTable("GrowSkillItemTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<GrowSkillItemTable>.I.CreateTable)));
    this.RegisterTable("ItemTable", (IDataTable) Singleton<ItemTable>.I);
    this.RegisterTable("TutorialGearSetTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<TutorialGearSetTable>.I.CreateTable)));
    this.RegisterTable("TradingPostTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<TradingPostTable>.I.CreateTable)));
    this.RegisterTable("NPCMessageTable", (IDataTable) Singleton<NPCMessageTable>.I);
    this.RegisterTable("NPCTable", (IDataTable) Singleton<NPCTable>.I);
    this.RegisterTable("SETable", (IDataTable) Singleton<SETable>.I);
    this.RegisterTable("SkillItemTable", (IDataTable) Singleton<SkillItemTable>.I);
    this.RegisterTable("ExceedSkillItemTable", (IDataTable) Singleton<ExceedSkillItemTable>.I);
    this.RegisterTable("StageTable", (IDataTable) Singleton<StageTable>.I);
    this.RegisterTable("StampTypeTable", (IDataTable) Singleton<StampTable>.I);
    this.RegisterTable("StringTable", (IDataTable) Singleton<StringTable>.I);
    this.RegisterTable("TaskTable", (IDataTable) Singleton<TaskTable>.I);
    this.RegisterTable("TutorialMessageTable", (IDataTable) Singleton<TutorialMessageTable>.I);
    this.RegisterTable("UserLevelTable", (IDataTable) Singleton<UserLevelTable>.I);
    this.RegisterTable("GachaSearchEnemyTable", (IDataTable) Singleton<GachaSearchEnemyTable>.I);
    this.RegisterTable("HomeThemeTable", (IDataTable) Singleton<HomeThemeTable>.I);
    this.RegisterTable("CountdownTable", (IDataTable) Singleton<CountdownTable>.I);
    this.RegisterTable("LimitedEquipItemExceedTable", (IDataTable) Singleton<LimitedEquipItemExceedTable>.I, "ItemTable");
    this.RegisterTable("PlayDataTable", (IDataTable) Singleton<PlayDataTable>.I);
    this.RegisterTable("ArenaTable", (IDataTable) Singleton<ArenaTable>.I);
    this.RegisterTable("EnemyAngryTable", (IDataTable) Singleton<EnemyAngryTable>.I);
    this.RegisterTable("EnemyActionTable", (IDataTable) Singleton<EnemyActionTable>.I);
    this.RegisterTable("NpcLevelTable", (IDataTable) Singleton<NpcLevelTable>.I);
    this.RegisterTable("NpcLevelSpecialTable", (IDataTable) Singleton<NpcLevelSpecialTable>.I);
    this.RegisterTable("AssignedEquipmentTable", (IDataTable) Singleton<AssignedEquipmentTable>.I);
    this.RegisterTable("SymbolTable", (IDataTable) Singleton<SymbolTable>.I);
    this.RegisterTable("GrowEquipItemTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<GrowEquipItemTable>.I.CreateGrowTable)), "ItemTable");
    this.RegisterTable("GrowEquipItemNeedItemTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<GrowEquipItemTable>.I.CreateNeedTable)), "ItemTable");
    this.RegisterTable("GrowEquipItemNeedUniqueItemTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<GrowEquipItemTable>.I.CreateNeedUniqueTable)), "ItemTable");
    this.RegisterTable("QuestTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy((Action<string>) (csv =>
    {
      Singleton<QuestTable>.I.CreateQuestTable(csv);
      this.afterProcesses.Add((System.Action) (() => Singleton<QuestTable>.I.InitQuestDependencyData()));
    })));
    this.RegisterTable("MissionTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<QuestTable>.I.CreateMissionTable)));
    this.RegisterTable("RegionTable", (IDataTable) Singleton<RegionTable>.I);
    this.RegisterTable("FieldMapTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<FieldMapTable>.I.CreateFieldMapTable)));
    this.RegisterTable("FieldMapPortalTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<FieldMapTable>.I.CreatePortalTable)));
    this.RegisterTable("FieldMapEnemyPopTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<FieldMapTable>.I.CreateEnemyPopTable)));
    this.RegisterTable("FieldMapGatherPointTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<FieldMapTable>.I.CreateGatherPointTable)));
    this.RegisterTable("FieldMapGatherPointViewTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<FieldMapTable>.I.CreateGatherPointViewTable)));
    this.RegisterTable("FieldMapGimmickPointTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<FieldMapTable>.I.CreateGimmickPointTable)));
    this.RegisterTable("FieldMapGimmickActionTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy(new Action<string>(Singleton<FieldMapTable>.I.CreateGimmickActionTable)));
    this.RegisterTable("QuestToFieldTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy((Action<string>) (csv =>
    {
      Singleton<QuestToFieldTable>.I.CreateTable(csv);
      this.afterProcesses.Add((System.Action) (() => Singleton<QuestToFieldTable>.I.InitDependencyData()));
    })));
    this.RegisterTable("ItemToFieldTable", (IDataTable) new DataTableManager.DataTableInterfaceProxy((Action<string>) (csv =>
    {
      Singleton<ItemToFieldTable>.I.CreateTable(csv);
      this.afterProcesses.Add((System.Action) (() => Singleton<ItemToFieldTable>.I.InitDependencyData()));
    })));
    this.RegisterTable("EnemyHitTypeTable", (IDataTable) Singleton<EnemyHitTypeTable>.I);
    this.RegisterTable("EnemyHitMaterialTable", (IDataTable) Singleton<EnemyHitMaterialTable>.I, "EnemyHitTypeTable");
    this.RegisterTable("EnemyPersonalityTable", (IDataTable) Singleton<EnemyPersonalityTable>.I);
    this.RegisterTable("PointShopGetPointTable", (IDataTable) Singleton<PointShopGetPointTable>.I);
    this.RegisterTable("DegreeTable", (IDataTable) Singleton<DegreeTable>.I);
    this.RegisterTable("DamageDistanceTable", (IDataTable) Singleton<DamageDistanceTable>.I);
    this.RegisterTable("BuffTable", (IDataTable) Singleton<BuffTable>.I);
    this.RegisterTable("FieldBuffTable", (IDataTable) Singleton<FieldBuffTable>.I);
    this.RegisterTable("WaveMatchDropTable", (IDataTable) Singleton<WaveMatchDropTable>.I);
    this.RegisterTable("FieldMapEnemyPopTimeZoneTable", (IDataTable) Singleton<FieldMapEnemyPopTimeZoneTable>.I);
    this.RegisterTable("GatherItemTable", (IDataTable) Singleton<GatherItemTable>.I);
    this.RegisterTable("ProductDataTable", (IDataTable) Singleton<ProductDataTable>.I);
    this.UpdateDependency();
  }

  public void InitializeForDownload()
  {
    this.Clear();
    DataTableManager.DataTableInterfaceProxy table = new DataTableManager.DataTableInterfaceProxy((Action<string>) (csv => { }));
    this.RegisterTable("AvatarTable", (IDataTable) table);
    this.RegisterTable("AccessoryTable", (IDataTable) table);
    this.RegisterTable("AccessoryInfoTable", (IDataTable) table);
    this.RegisterTable("AccessoryDataTable", (IDataTable) table);
    this.RegisterTable("CreateEquipItemTable", (IDataTable) table);
    this.RegisterTable("CreatePickupItemTable", (IDataTable) table);
    this.RegisterTable("DeliveryTable", (IDataTable) table);
    this.RegisterTable("EquipItemTable", (IDataTable) table);
    this.RegisterTable("EquipModelTable", (IDataTable) table);
    this.RegisterTable("GrowSkillItemTable", (IDataTable) table);
    this.RegisterTable("HomeThemeTable", (IDataTable) table);
    this.RegisterTable("CountdownTable", (IDataTable) table);
    this.RegisterTable("NPCMessageTable", (IDataTable) table);
    this.RegisterTable("NPCTable", (IDataTable) table);
    this.RegisterTable("QuestTable", (IDataTable) table);
    this.RegisterTable("SkillItemTable", (IDataTable) table);
    this.RegisterTable("ExceedSkillItemTable", (IDataTable) table);
    this.RegisterTable("StageTable", (IDataTable) table);
    this.RegisterTable("TutorialMessageTable", (IDataTable) table);
    this.RegisterTable("StampTypeTable", (IDataTable) table);
    this.RegisterTable("EquipItemExceedParamTable", (IDataTable) table);
    this.RegisterTable("AbilityDataTable", (IDataTable) table);
    this.RegisterTable("AbilityTable", (IDataTable) table);
    this.RegisterTable("AbilityItemLotTable", (IDataTable) table);
    this.RegisterTable("AudioSettingTable", (IDataTable) table);
    this.RegisterTable("DeliveryRewardTable", (IDataTable) table);
    this.RegisterTable("EnemyTable", (IDataTable) table);
    this.RegisterTable("EquipItemExceedTable", (IDataTable) table);
    this.RegisterTable("EvolveEquipItemTable", (IDataTable) table);
    this.RegisterTable("GrowEnemyTable", (IDataTable) table);
    this.RegisterTable("ItemTable", (IDataTable) table);
    this.RegisterTable("TutorialGearSetTable", (IDataTable) table);
    this.RegisterTable("TradingPostTable", (IDataTable) table);
    this.RegisterTable("SETable", (IDataTable) table);
    this.RegisterTable("StringTable", (IDataTable) table);
    this.RegisterTable("TaskTable", (IDataTable) table);
    this.RegisterTable("UserLevelTable", (IDataTable) table);
    this.RegisterTable("GrowEquipItemTable", (IDataTable) table);
    this.RegisterTable("GrowEquipItemNeedItemTable", (IDataTable) table);
    this.RegisterTable("GrowEquipItemNeedUniqueItemTable", (IDataTable) table);
    this.RegisterTable("MissionTable", (IDataTable) table);
    this.RegisterTable("RegionTable", (IDataTable) table);
    this.RegisterTable("FieldMapTable", (IDataTable) table);
    this.RegisterTable("FieldMapPortalTable", (IDataTable) table);
    this.RegisterTable("FieldMapEnemyPopTable", (IDataTable) table);
    this.RegisterTable("FieldMapGatherPointTable", (IDataTable) table);
    this.RegisterTable("FieldMapGatherPointViewTable", (IDataTable) table);
    this.RegisterTable("FieldMapGimmickPointTable", (IDataTable) table);
    this.RegisterTable("FieldMapGimmickActionTable", (IDataTable) table);
    this.RegisterTable("QuestToFieldTable", (IDataTable) table);
    this.RegisterTable("ItemToFieldTable", (IDataTable) table);
    this.RegisterTable("EnemyHitTypeTable", (IDataTable) table);
    this.RegisterTable("EnemyHitMaterialTable", (IDataTable) table);
    this.RegisterTable("EnemyPersonalityTable", (IDataTable) table);
    this.RegisterTable("PointShopGetPointTable", (IDataTable) table);
    this.RegisterTable("DegreeTable", (IDataTable) table);
    this.RegisterTable("DamageDistanceTable", (IDataTable) table);
    this.RegisterTable("GachaSearchEnemyTable", (IDataTable) table);
    this.RegisterTable("BuffTable", (IDataTable) table);
    this.RegisterTable("FieldBuffTable", (IDataTable) table);
    this.RegisterTable("WaveMatchDropTable", (IDataTable) table);
    this.RegisterTable("LimitedEquipItemExceedTable", (IDataTable) table);
    this.RegisterTable("PlayDataTable", (IDataTable) table);
    this.RegisterTable("ArenaTable", (IDataTable) table);
    this.RegisterTable("EnemyAngryTable", (IDataTable) table);
    this.RegisterTable("EnemyActionTable", (IDataTable) table);
    this.RegisterTable("NpcLevelTable", (IDataTable) table);
    this.RegisterTable("NpcLevelSpecialTable", (IDataTable) table);
    this.RegisterTable("FieldMapEnemyPopTimeZoneTable", (IDataTable) table);
    this.RegisterTable("GatherItemTable", (IDataTable) table);
    this.RegisterTable("AssignedEquipmentTable", (IDataTable) table);
    this.RegisterTable("SymbolTable", (IDataTable) table);
    this.RegisterTable("ProductDataTable", (IDataTable) table);
  }

  public void RegisterTable(string name, IDataTable table, string dependencyTableName = null)
  {
    this.tables.Add(name, new DataTableManager.DataTableContainer(name, table));
    if (string.IsNullOrEmpty(dependencyTableName))
      return;
    this.unresolvedDependencies.Add(new KeyValuePair<string, string>(name, dependencyTableName));
  }

  public void UnregisterTable(string name) => this.tables.Remove(name);

  private void UpdateDependency()
  {
    int index = 0;
    for (int count = this.unresolvedDependencies.Count; index < count; ++index)
    {
      KeyValuePair<string, string> unresolvedDependency = this.unresolvedDependencies[index];
      DataTableManager.DataTableContainer dataTableContainer;
      if (this.tables.TryGetValue(unresolvedDependency.Key, out dataTableContainer))
      {
        DataTableManager.DataTableContainer table;
        if (this.tables.TryGetValue(unresolvedDependency.Value, out table))
          dataTableContainer.SetDependency(table);
        else
          Log.Error(LOG.DATA_TABLE, "Not found dependency table: {0} ---> {1}", (object) unresolvedDependency.Key, (object) unresolvedDependency.Value);
      }
      else
        Log.Error(LOG.DATA_TABLE, "Not found table: {0}", (object) unresolvedDependency.Key);
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
    using (MemoryStream memoryStream = new MemoryStream(bytes))
    {
      memoryStream.Seek(256L /*0x0100*/, SeekOrigin.Begin);
      using (ZlibStream zlibStream = new ZlibStream((Stream) memoryStream, (CompressionMode) 1))
      {
        using (StreamReader streamReader = new StreamReader((Stream) zlibStream))
        {
          try
          {
            return streamReader.ReadToEnd();
          }
          catch (Exception ex)
          {
            throw;
          }
        }
      }
    }
  }

  public static Action<byte[]> CreateCompressedBinaryTableProcess(Action<MemoryStream> create)
  {
    return (Action<byte[]>) (bytes =>
    {
      MemoryStream memoryStream1 = new MemoryStream();
      using (MemoryStream memoryStream2 = new MemoryStream(bytes))
      {
        byte[] buffer = new byte[1024 /*0x0400*/];
        using (ZlibStream zlibStream = new ZlibStream((Stream) memoryStream2, (CompressionMode) 1))
        {
          try
          {
            int count;
            while ((count = ((Stream) zlibStream).Read(buffer, 0, buffer.Length)) != 0)
              memoryStream1.Write(buffer, 0, count);
          }
          finally
          {
            ((IDisposable) zlibStream)?.Dispose();
          }
        }
      }
      memoryStream1.Seek(0L, SeekOrigin.Begin);
      create(memoryStream1);
    });
  }

  public void DumpManifest()
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string allFileName in this.manifest.GetAllFileNames())
    {
      MD5Hash tableHash = this.manifest.GetTableHash(allFileName);
      stringBuilder.AppendLine($"{allFileName} : {tableHash.ToString()}");
    }
    Debug.Log((object) stringBuilder.ToString());
  }

  public void ChangePriorityTop(string tableName)
  {
    if (!Object.op_Inequality((Object) null, (Object) this.dataLoader))
      return;
    this.dataLoader.ChangePriorityTop(tableName);
  }

  public bool IsLoading() => this.loadStatus != DataTableManager.LoadStatus.LoadComplete;

  public bool IsLoading(string tableName)
  {
    if (this.loadStatus == DataTableManager.LoadStatus.LoadComplete)
      return false;
    if (this.loadStatus != DataTableManager.LoadStatus.LoadingAllTable)
      return true;
    return Object.op_Inequality((Object) null, (Object) this.dataLoader) && this.dataLoader.IsLoading(tableName);
  }

  public void LoadStory(string storyName, Action<string> onComplete)
  {
    DataTableManager.DataTableInterfaceProxy table = new DataTableManager.DataTableInterfaceProxy(onComplete);
    this.Request(this.CreateRequestLoadTable(storyName, (IDataTable) table));
  }

  private enum LoadStatus
  {
    NotInitialize,
    LoadingInitialTable,
    LoadingAllTable,
    LoadComplete,
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

    public DataTableInterfaceProxy(Action<string> create) => this.create = create;

    public void CreateTable(string csv) => this.create(csv);
  }

  private class DataTableContainer : IDataTable
  {
    private IDataTable table;
    private DataTableManager.DataTableContainer dependencyTable;

    public bool isInitialized { get; private set; }

    public string name { get; private set; }

    public DataTableContainer(string name, IDataTable table)
    {
      this.name = name;
      this.table = table;
    }

    public void SetDependency(DataTableManager.DataTableContainer table)
    {
      this.dependencyTable = table;
    }

    public DataTableManager.DataTableContainer GetDependency() => this.dependencyTable;

    public bool CanLoad() => this.dependencyTable == null || this.dependencyTable.isInitialized;

    public void CreateTable(string csv)
    {
      this.table.CreateTable(csv);
      this.isInitialized = true;
    }
  }
}
