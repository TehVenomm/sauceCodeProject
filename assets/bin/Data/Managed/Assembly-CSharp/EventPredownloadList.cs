// Decompiled with JetBrains decompiler
// Type: EventPredownloadList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class EventPredownloadList : AssetPreDownloadList
{
  private const string SAVE_KEY = "EVENT_ASSET_CHECK";
  private const int THRESHOLE_SLEEP = 10;
  private List<EventListData> eventList;
  private List<Delivery> deliveryList;
  private EventPredownloadList.SaveData savedata;
  public List<int> newEventIds = new List<int>();
  private List<string> loading_packages_name;

  public override void Check(MonoBehaviour mono)
  {
    this.SetStopFlag(false);
    mono.StartCoroutine(this.CheckDownloadFiles());
  }

  public override void FinishDownload() => this.SaveEventData();

  public override void Init()
  {
    this.newEventIds.Clear();
    this.eventList = (List<EventListData>) null;
    this.deliveryList = (List<Delivery>) null;
    this.isAvailable = false;
    this.loading_packages_name = (List<string>) null;
    this.checkedCount = 0;
    this.totalCheckCount = 0;
    this.totalCount = 0;
  }

  public override IEnumerator Setup()
  {
    while (!Singleton<EnemyTable>.IsValid() || !Singleton<EnemyTable>.I.IsAvailable() || !Singleton<DeliveryTable>.IsValid() || !MonoBehaviourSingleton<DeliveryManager>.IsValid() || !Singleton<StageTable>.IsValid() || !Singleton<FieldMapTable>.IsValid())
      yield return (object) null;
    this.isAvailable = true;
  }

  private IEnumerator CheckDownloadFiles()
  {
    bool checkLoadData = false;
    MonoBehaviourSingleton<QuestManager>.I.SendGetEventList((Action<bool>) (b => checkLoadData = true));
    while (!checkLoadData)
      yield return (object) null;
    checkLoadData = false;
    if (!MonoBehaviourSingleton<DeliveryManager>.I.HasEventListData())
    {
      bool isFN = false;
      MonoBehaviourSingleton<DeliveryManager>.I.SendEventList((Action<bool>) (b =>
      {
        checkLoadData = b;
        isFN = true;
      }));
      while (!isFN)
        yield return (object) null;
      MonoBehaviourSingleton<DeliveryManager>.I.isUpdateEventListData = false;
    }
    else
      checkLoadData = true;
    if (checkLoadData)
    {
      this.eventList = new List<EventListData>((IEnumerable<EventListData>) MonoBehaviourSingleton<DeliveryManager>.I.eventListData);
      string str1 = PlayerPrefs.GetString("EVENT_ASSET_CHECK", "");
      this.savedata = !string.IsNullOrEmpty(str1) ? JsonUtility.FromJson<EventPredownloadList.SaveData>(str1) : new EventPredownloadList.SaveData();
      this.eventList.ForEach((Action<EventListData>) (o =>
      {
        if (this.savedata.ListEventID.Contains(o.eventId))
          return;
        this.newEventIds.Add(o.eventId);
      }));
      if (this.loading_packages == null)
        this.loading_packages = new List<AssetPreDownloadList.LoadPackage>();
      else
        this.loading_packages.Clear();
      if (this.loading_packages_name == null)
        this.loading_packages_name = new List<string>();
      else
        this.loading_packages_name.Clear();
      if (this.newEventIds.Count > 0)
      {
        if (this.deliveryList == null)
          this.deliveryList = new List<Delivery>();
        else
          this.deliveryList.Clear();
        for (int index = 0; index < this.newEventIds.Count; ++index)
          this.deliveryList.AddRange((IEnumerable<Delivery>) MonoBehaviourSingleton<DeliveryManager>.I.GetEventDeliveryList(this.newEventIds[index]));
        this.totalCheckCount = this.deliveryList.Count;
      }
      this.checkedCount = 0;
      this.SetWaitFlag(true);
      AssetPreDownloadList.OnStartCheckDownload startCheckDownload = this.m_OnStartCheckDownload;
      if (startCheckDownload != null)
        startCheckDownload((AssetPreDownloadList) this, this.totalCheckCount > 0);
      while (this.waitFlag)
        yield return (object) null;
      if (this.newEventIds.Count > 0)
      {
        while (this.checkedCount < this.totalCheckCount)
        {
          Delivery delivery = this.deliveryList[this.checkedCount];
          DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery.dId);
          List<uint> mapIdList = deliveryTableData.GetMapIdList();
          List<uint> enemyIdList = deliveryTableData.GetEnemyIdList();
          QuestTable.QuestTableData questData = deliveryTableData.GetQuestData();
          int jumpMapId = deliveryTableData.jumpMapID;
          if (mapIdList != null)
          {
            for (int index = 0; index < mapIdList.Count; ++index)
              this.LoadMap(Singleton<FieldMapTable>.I.GetFieldMapData(mapIdList[index]));
          }
          if (enemyIdList != null)
          {
            for (int index = 0; index < enemyIdList.Count; ++index)
              this.LoadEnemy(Singleton<EnemyTable>.I.GetEnemyData(enemyIdList[index]));
          }
          if (questData != null)
          {
            string[] stageName = questData.stageName;
            if (stageName != null && stageName.Length != 0)
            {
              for (int index = 0; index < stageName.Length; ++index)
              {
                string str2 = stageName[index];
                this.LoadState(Singleton<StageTable>.I.GetData(str2), str2);
              }
            }
            int[] enemyId = questData.enemyID;
            if (enemyId != null && enemyId.Length != 0)
            {
              for (int index = 0; index < enemyId.Length; ++index)
                this.LoadEnemy(Singleton<EnemyTable>.I.GetEnemyData((uint) enemyId[index]));
            }
          }
          if (jumpMapId > 0)
          {
            this.LoadMap(Singleton<FieldMapTable>.I.GetFieldMapData((uint) jumpMapId));
            List<int> jumpPortIds = this.GetJumpPortIDs(jumpMapId);
            if (jumpPortIds.Count > 1)
            {
              for (int index = 0; index < jumpPortIds.Count; ++index)
              {
                int id = jumpPortIds[index];
                if (id != jumpMapId)
                  this.LoadMap(Singleton<FieldMapTable>.I.GetFieldMapData((uint) id));
              }
            }
          }
          ++this.checkedCount;
          AssetPreDownloadList.OnUpdateCheckDownload updateCheckDownload = this.m_OnUpdateCheckDownload;
          if (updateCheckDownload != null)
            updateCheckDownload((AssetPreDownloadList) this);
          if (this.checkedCount % 10 == 0)
            yield return (object) null;
          if (this.stopFlag)
            yield break;
        }
        if (this.loading_packages != null)
          this.totalCount = this.loading_packages.Count;
        else
          this.totalCount = 0;
      }
    }
    else
    {
      this.totalCount = 0;
      AssetPreDownloadList.OnStartCheckDownload startCheckDownload = this.m_OnStartCheckDownload;
      if (startCheckDownload != null)
        startCheckDownload((AssetPreDownloadList) this, false);
    }
    AssetPreDownloadList.OnFinishCheckDownload finishCheckDownLoad = this.m_OnFinishCheckDownLoad;
    if (finishCheckDownLoad != null)
      finishCheckDownLoad((AssetPreDownloadList) this, this.totalCount > 0);
  }

  private void SaveEventData()
  {
    if (this.newEventIds == null || this.newEventIds.Count == 0)
      return;
    if (this.savedata == null)
      this.savedata = JsonUtility.FromJson<EventPredownloadList.SaveData>(PlayerPrefs.GetString("EVENT_ASSET_CHECK"));
    this.savedata.ListEventID.AddRange((IEnumerable<int>) this.newEventIds);
    PlayerPrefs.SetString("EVENT_ASSET_CHECK", JsonUtility.ToJson((object) this.savedata));
  }

  private void LoadMap(FieldMapTable.FieldMapTableData map_table)
  {
    if (this.loading_packages_name == null || this.loading_packages == null || map_table == null)
      return;
    this.LoadState(Singleton<StageTable>.I.GetData(map_table.stageName), map_table.stageName);
  }

  private void LoadState(StageTable.StageData map_data, string map_name)
  {
    if (this.loading_packages_name == null || this.loading_packages == null || map_data == null)
      return;
    string assetBundleName1 = RESOURCE_CATEGORY.STAGE_SCENE.ToAssetBundleName(map_name);
    if (!this.loading_packages_name.Contains(assetBundleName1))
    {
      this.loading_packages_name.Add(assetBundleName1);
      if (!MonoBehaviourSingleton<ResourceManager>.I.IsCached(RESOURCE_CATEGORY.STAGE_SCENE, map_name) && this.IsValidAssetURL(RESOURCE_CATEGORY.STAGE_SCENE, map_name))
        this.loading_packages.Add(new AssetPreDownloadList.LoadPackage()
        {
          category = RESOURCE_CATEGORY.STAGE_SCENE,
          packageName = map_name
        });
    }
    string sky = map_data.sky;
    string assetBundleName2 = RESOURCE_CATEGORY.STAGE_SKY.ToAssetBundleName(sky);
    if (this.loading_packages_name.Contains(assetBundleName2))
      return;
    this.loading_packages_name.Add(assetBundleName2);
    if (MonoBehaviourSingleton<ResourceManager>.I.IsCached(RESOURCE_CATEGORY.STAGE_SKY, sky) || !this.IsValidAssetURL(RESOURCE_CATEGORY.STAGE_SKY, sky))
      return;
    this.loading_packages.Add(new AssetPreDownloadList.LoadPackage()
    {
      category = RESOURCE_CATEGORY.STAGE_SKY,
      packageName = sky
    });
  }

  private void LoadEnemy(EnemyTable.EnemyData enemy_data)
  {
    if (this.loading_packages_name == null || this.loading_packages == null || enemy_data == null || !(enemy_data.name != Log.NON_DATA_NAME))
      return;
    string enemyBody = ResourceName.GetEnemyBody(enemy_data.modelId);
    string enemyMaterial = ResourceName.GetEnemyMaterial(enemy_data.modelId);
    string enemyAnim = ResourceName.GetEnemyAnim(enemy_data.animId);
    if (!string.IsNullOrEmpty(enemyBody))
    {
      string assetBundleName = RESOURCE_CATEGORY.ENEMY_MODEL.ToAssetBundleName(enemyBody);
      if (!this.loading_packages_name.Contains(assetBundleName))
      {
        this.loading_packages_name.Add(assetBundleName);
        if (!MonoBehaviourSingleton<ResourceManager>.I.IsCached(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody) && this.IsValidAssetURL(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody))
          this.loading_packages.Add(new AssetPreDownloadList.LoadPackage()
          {
            category = RESOURCE_CATEGORY.ENEMY_MODEL,
            packageName = enemyBody
          });
      }
    }
    if (!string.IsNullOrEmpty(enemyMaterial))
    {
      string assetBundleName = RESOURCE_CATEGORY.ENEMY_MATERIAL.ToAssetBundleName(enemyBody);
      if (!this.loading_packages_name.Contains(assetBundleName))
      {
        this.loading_packages_name.Add(assetBundleName);
        if (!MonoBehaviourSingleton<ResourceManager>.I.IsCached(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody) && this.IsValidAssetURL(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody))
          this.loading_packages.Add(new AssetPreDownloadList.LoadPackage()
          {
            category = RESOURCE_CATEGORY.ENEMY_MATERIAL,
            packageName = enemyBody
          });
      }
    }
    if (string.IsNullOrEmpty(enemyAnim))
      return;
    string assetBundleName1 = RESOURCE_CATEGORY.ENEMY_ANIM.ToAssetBundleName(enemyAnim);
    if (this.loading_packages_name.Contains(assetBundleName1))
      return;
    this.loading_packages_name.Add(assetBundleName1);
    if (MonoBehaviourSingleton<ResourceManager>.I.IsCached(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim) || !this.IsValidAssetURL(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim))
      return;
    this.loading_packages.Add(new AssetPreDownloadList.LoadPackage()
    {
      category = RESOURCE_CATEGORY.ENEMY_ANIM,
      packageName = enemyAnim
    });
  }

  private List<int> GetJumpPortIDs(int ID)
  {
    HashSet<int> ids = new HashSet<int>();
    ids.Add(ID);
    Singleton<FieldMapTable>.I.GetPortalListByMapID((uint) ID)?.ForEach((Action<FieldMapTable.PortalTableData>) (o =>
    {
      if (o.dstMapID == 0U || ids.Contains((int) o.dstMapID))
        return;
      ids.Add((int) o.dstMapID);
    }));
    return ids.ToList<int>();
  }

  public class SaveData
  {
    public List<int> ListEventID = new List<int>();
  }
}
