// Decompiled with JetBrains decompiler
// Type: RegionMapDescriptionList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class RegionMapDescriptionList : GameSection
{
  private const float ANCHOR_LEFT = 0.0f;
  private const float ANCHOR_CENTER = 0.5f;
  private const float ANCHOR_RIGHT = 1f;
  private const float ANCHOR_BOT = 0.0f;
  private const float ANCHOR_TOP = 1f;
  private bool isInGame;
  private RegionMap.SpotEventData mapData;
  private List<RegionMapDescriptionList.DeliveryDataAndUId> deliveryDataAndUIdList = new List<RegionMapDescriptionList.DeliveryDataAndUId>();
  private List<QuestTable.QuestTableData> happenDataList;
  private List<RegionMapDescriptionList.EnemyDataForDisplay> enemyDataList;

  public override void Initialize()
  {
    object eventData = GameSection.GetEventData();
    if (eventData != null && eventData is RegionMap.SpotEventData)
      this.mapData = (RegionMap.SpotEventData) eventData;
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && FieldManager.IsValidInGame())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.InitDataLists();
    this.isInGame = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene";
    if (this.isInGame)
    {
      InGameMain currentScreen = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreen() as InGameMain;
      if (Object.op_Inequality((Object) currentScreen, (Object) null))
        ((Component) this).transform.parent = ((Component) currentScreen).transform;
    }
    else
    {
      HomeTop currentScreen = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreen() as HomeTop;
      if (Object.op_Inequality((Object) currentScreen, (Object) null))
        ((Component) this).transform.parent = ((Component) currentScreen).transform;
    }
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      this.Reposition();
    this.UpdateTable();
    this.SetActive((Enum) RegionMapDescriptionList.UI.BTN_TO_FIELD, (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID != (int) this.mapData.mapId);
    base.UpdateUI();
  }

  protected void UpdateTable()
  {
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(this.mapData.mapId);
    string mapName = fieldMapData.mapName;
    this.SetLabelText((Enum) RegionMapDescriptionList.UI.LBL_MAP_NAME, mapName);
    this.SetLabelText((Enum) RegionMapDescriptionList.UI.LBL_MAP_NAME_D, mapName);
    ResourceLoad.LoadFieldIconTexture(((Component) this.GetCtrl((Enum) RegionMapDescriptionList.UI.TEX_FIELD)).GetComponent<UITexture>(), fieldMapData);
    Dictionary<int, string> borderIndexTitleDic = new Dictionary<int, string>(3);
    int count1 = this.deliveryDataAndUIdList.Count;
    int count2 = this.enemyDataList.Count;
    int count3 = this.happenDataList.Count;
    int num = 0;
    if (count1 >= 1)
    {
      borderIndexTitleDic[0] = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 0U);
      ++num;
    }
    int deliveryStartIndex = num;
    if (count3 >= 1)
    {
      borderIndexTitleDic[count1 + deliveryStartIndex] = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 1U);
      ++num;
    }
    int happenStartIndex = count1 + num;
    if (count2 >= 1)
    {
      borderIndexTitleDic[happenStartIndex + count3] = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 2U);
      ++num;
    }
    int enemyStartIndex = count1 + count3 + num;
    this.ClearTable();
    int item_num = count1 + count3 + count2 + num;
    this.SetActive((Enum) RegionMapDescriptionList.UI.LBL_NON_LIST, item_num <= 0);
    this.SetTable((Enum) RegionMapDescriptionList.UI.TBL_ALL, "", item_num, true, (Func<int, Transform, Transform>) ((i, parent) =>
    {
      Transform transform = (Transform) null;
      if (borderIndexTitleDic.ContainsKey(i))
        return this.Realizes("RegionMapDescriptionBorderItem", parent);
      if (i >= enemyStartIndex)
        return this.Realizes("RegionMapDescriptionEnemyItem", parent);
      if (i >= happenStartIndex)
        return this.Realizes("RegionMapDescriptionHappenItem", parent);
      return i >= deliveryStartIndex ? this.Realizes("RegionMapDescriptionDeliveryItem", parent) : transform;
    }), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      string text = "";
      if (borderIndexTitleDic.TryGetValue(i, out text))
      {
        this.SetLabelText(t, (Enum) RegionMapDescriptionList.UI.LBL_BORDER_TITLE, text);
        this.SetActive(t, true);
      }
      else if (i >= enemyStartIndex && i - enemyStartIndex < this.enemyDataList.Count)
      {
        this.SetupEnemyListItem(t, this.enemyDataList[i - enemyStartIndex]);
        this.SetActive(t, true);
      }
      else if (i >= happenStartIndex && i - happenStartIndex < this.happenDataList.Count)
      {
        this.SetupHappenListItem(t, this.happenDataList[i - happenStartIndex]);
        this.SetActive(t, true);
      }
      else if (i >= deliveryStartIndex && i - deliveryStartIndex < this.deliveryDataAndUIdList.Count)
      {
        this.SetupDeliveryListItem(t, this.deliveryDataAndUIdList[i - deliveryStartIndex]);
        this.SetActive(t, true);
      }
      else
        this.SetActive(t, true);
    }));
  }

  private void InitDataLists()
  {
    this.enemyDataList = this.CreateEnemyDataList(this.mapData.mapId);
    this.deliveryDataAndUIdList = this.CreateDeliveryList(this.mapData.mapId);
    this.happenDataList = this.CreateHappneList(this.mapData.mapId);
  }

  private List<QuestTable.QuestTableData> CreateHappneList(uint mapId)
  {
    List<QuestTable.QuestTableData> happneList = new List<QuestTable.QuestTableData>();
    Dictionary<uint, uint> questIdEventIdDic = Singleton<QuestToFieldTable>.I.GetQuestIdEventIdDic(mapId);
    if (questIdEventIdDic == null || questIdEventIdDic.Count <= 0)
      return happneList;
    foreach (KeyValuePair<uint, uint> keyValuePair in questIdEventIdDic)
    {
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(keyValuePair.Key);
      if (questData != null && (keyValuePair.Value < 1U || MonoBehaviourSingleton<QuestManager>.I.IsEventPlayableWith((int) keyValuePair.Value, NetworkNative.getNativeVersionFromName())))
        happneList.Add(questData);
    }
    return happneList;
  }

  private List<RegionMapDescriptionList.DeliveryDataAndUId> CreateDeliveryList(uint mapId)
  {
    Delivery[] deliveryList1 = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
    List<RegionMapDescriptionList.DeliveryDataAndUId> deliveryList2 = new List<RegionMapDescriptionList.DeliveryDataAndUId>();
    if (deliveryList1 == null)
      return (List<RegionMapDescriptionList.DeliveryDataAndUId>) null;
    int index = 0;
    for (int length = deliveryList1.Length; index < length; ++index)
    {
      Delivery delivery = deliveryList1[index];
      int dId = delivery.dId;
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) dId);
      if (this.IsExistTargetEnemy(deliveryTableData, mapId) && (!deliveryTableData.IsEvent() || !MonoBehaviourSingleton<QuestManager>.I.bingoEventList.Any<Network.EventData>((Func<Network.EventData, bool>) (e => e.eventId == deliveryTableData.eventID))))
        deliveryList2.Add(new RegionMapDescriptionList.DeliveryDataAndUId(deliveryTableData, delivery.uId));
    }
    return deliveryList2;
  }

  private bool IsExistTargetEnemy(DeliveryTable.DeliveryData deliveryData, uint mapId)
  {
    if (deliveryData == null)
      return false;
    bool flag = false;
    List<uint> mapIdList = deliveryData.GetMapIdList();
    if (mapIdList != null)
    {
      flag = true;
      if (mapIdList.Contains(mapId))
        return true;
    }
    if (flag)
      return false;
    List<uint> enemyIdList = deliveryData.GetEnemyIdList();
    if (enemyIdList == null)
      return false;
    int index = 0;
    for (int count = this.enemyDataList.Count; index < count; ++index)
    {
      if (enemyIdList.Contains(this.enemyDataList[index].data.id))
        return true;
    }
    return false;
  }

  private List<RegionMapDescriptionList.EnemyDataForDisplay> CreateEnemyDataList(uint mapId)
  {
    List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(mapId);
    List<RegionMapDescriptionList.EnemyDataForDisplay> enemyDataList = new List<RegionMapDescriptionList.EnemyDataForDisplay>(enemyPopList.Count);
    Dictionary<uint, HashSet<uint>> dictionary = new Dictionary<uint, HashSet<uint>>(enemyPopList.Count);
    int index = 0;
    for (int count = enemyPopList.Count; index < count; ++index)
    {
      if (enemyPopList[index].enemyPopType == ENEMY_POP_TYPE.NONE || enemyPopList[index].enemyPopType == ENEMY_POP_TYPE.FIELD_BOSS)
      {
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemyPopList[index].enemyID);
        uint level = enemyPopList[index].enemyLv;
        if (level == 0U)
          level = (uint) (int) enemyData.level;
        HashSet<uint> uintSet;
        if (dictionary.TryGetValue(enemyData.id, out uintSet))
        {
          if (!uintSet.Add(level))
            continue;
        }
        else
        {
          dictionary[enemyData.id] = new HashSet<uint>();
          dictionary[enemyData.id].Add(level);
        }
        enemyDataList.Add(new RegionMapDescriptionList.EnemyDataForDisplay(enemyData, level));
      }
    }
    return enemyDataList;
  }

  private void SetupDeliveryListItem(
    Transform t,
    RegionMapDescriptionList.DeliveryDataAndUId deliveryDataAndUId)
  {
    RegionMapDescriptionDeliveryItem descriptionDeliveryItem = ((Component) t).GetComponent<RegionMapDescriptionDeliveryItem>();
    if (Object.op_Equality((Object) descriptionDeliveryItem, (Object) null))
      descriptionDeliveryItem = ((Component) t).gameObject.AddComponent<RegionMapDescriptionDeliveryItem>();
    descriptionDeliveryItem.InitUI();
    descriptionDeliveryItem.Setup(t, deliveryDataAndUId.data);
    this.SetEvent(t, "SELECT_DELIVERY", (object) deliveryDataAndUId);
  }

  public void OnQuery_SELECT_DELIVERY()
  {
    RegionMapDescriptionList.DeliveryDataAndUId dataAndUId = (RegionMapDescriptionList.DeliveryDataAndUId) GameSection.GetEventData();
    DeliveryTable.DeliveryData data = dataAndUId.data;
    int deliveryId = (int) dataAndUId.data.id;
    bool is_enough_material = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryId);
    if (!is_enough_material)
    {
      GameSection.SetEventData((object) new object[4]
      {
        (object) deliveryId,
        null,
        (object) false,
        (object) this.mapData
      });
    }
    else
    {
      int num = FieldManager.IsValidInGame() ? 1 : 0;
      int clearEventId = (int) data.clearEventID;
      if (num != 0)
      {
        GameSection.StayEvent();
        MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop((Action<bool>) (b =>
        {
          if (!b)
            return;
          this.SendDeliveryComplete(data, dataAndUId.uId, is_enough_material);
          if (Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint) deliveryId) == null)
            return;
          MonoBehaviourSingleton<DeliveryManager>.I.CheckAnnouncePortalOpen();
        }));
      }
      else
      {
        GameSection.StayEvent();
        this.SendDeliveryComplete(data, dataAndUId.uId, is_enough_material);
      }
    }
  }

  private void SendDeliveryComplete(
    DeliveryTable.DeliveryData deliveryData,
    string deliveryUniqueId,
    bool is_enough_material)
  {
    bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
    int delivery_id = (int) deliveryData.id;
    bool enable_clear_event = deliveryData.clearEventID > 0U;
    MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryUniqueId, enable_clear_event, (Action<bool, DeliveryRewardList>) ((is_success, recv_reward) =>
    {
      if (is_success)
      {
        List<FieldMapTable.PortalTableData> relationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint) delivery_id);
        for (int index = 0; index < relationPortalData.Count; ++index)
          GameSaveData.instance.newReleasePortals.Add(relationPortalData[index].portalID);
        if (is_tutorial)
          TutorialStep.isSendFirstRewardComplete = true;
        if (!enable_clear_event)
        {
          MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
          GameSection.ChangeStayEvent("DELIVERY_REWARD", (object) new object[4]
          {
            (object) delivery_id,
            (object) recv_reward,
            (object) false,
            (object) this.mapData
          });
        }
        else
        {
          GameSection.ChangeStayEvent("CLEAR_EVENT", (object) new object[3]
          {
            (object) (int) deliveryData.clearEventID,
            (object) delivery_id,
            (object) recv_reward
          });
          if (FieldManager.IsValidInGame())
          {
            is_success = false;
            List<int> intList = new List<int>((IEnumerable<int>) MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame);
            for (int index = 0; index < intList.Count; ++index)
            {
              int num = intList[index];
              if (!MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Contains(num))
                MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Add(num);
            }
            EventData[] requestEventData = new EventData[2]
            {
              new EventData("STORY_DELIVERY_REWARD", (object) new object[2]
              {
                (object) delivery_id,
                (object) recv_reward
              }),
              new EventData("PORTAL_RELEASE", (object) GameSaveData.instance.newReleasePortals)
            };
            GameSaveData.instance.newReleasePortals = new List<uint>();
            MonoBehaviourSingleton<InGameProgress>.I.FieldReadStory((int) deliveryData.clearEventID, true, requestEventData);
            MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame.Clear();
          }
        }
        this.deliveryDataAndUIdList = this.CreateDeliveryList(this.mapData.mapId);
      }
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void SetupHappenListItem(Transform t, QuestTable.QuestTableData happenData)
  {
    RegionMapDescriptionHappenItem descriptionHappenItem = ((Component) t).GetComponent<RegionMapDescriptionHappenItem>();
    if (Object.op_Equality((Object) descriptionHappenItem, (Object) null))
      descriptionHappenItem = ((Component) t).gameObject.AddComponent<RegionMapDescriptionHappenItem>();
    descriptionHappenItem.InitUI();
    descriptionHappenItem.SetUp(happenData);
    this.SetEvent(t, "SELECT_HAPPEN", (object) new object[4]
    {
      (object) (int) happenData.questID,
      (object) "",
      (object) false,
      (object) this.mapData
    });
  }

  private void SetupEnemyListItem(
    Transform t,
    RegionMapDescriptionList.EnemyDataForDisplay enemyData)
  {
    RegionMapDescriptionEnemyItem descriptionEnemyItem = ((Component) t).GetComponent<RegionMapDescriptionEnemyItem>();
    if (Object.op_Equality((Object) descriptionEnemyItem, (Object) null))
      descriptionEnemyItem = ((Component) t).gameObject.AddComponent<RegionMapDescriptionEnemyItem>();
    descriptionEnemyItem.InitUI();
    descriptionEnemyItem.SetUpEnemyOnly(enemyData.data, enemyData.level);
    this.SetEvent(t, "SELECT_HAPPEN", (object) new object[2]
    {
      (object) (int) enemyData.data.id,
      (object) ""
    });
  }

  private void ClearTable()
  {
    Transform ctrl = this.GetCtrl((Enum) RegionMapDescriptionList.UI.TBL_ALL);
    if (!Object.op_Implicit((Object) ctrl))
      return;
    int num = 0;
    for (int childCount = ctrl.childCount; num < childCount; ++num)
    {
      Transform child = ctrl.GetChild(0);
      child.parent = (Transform) null;
      Object.Destroy((Object) ((Component) child).gameObject);
    }
  }

  public void OnQuery_TO_FIELD()
  {
    GameSection.StopEvent();
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("TO_REGION_MAP", (object) null),
      new EventData("TO_FIELD_OR_HOME", (object) this.mapData)
    });
  }

  public void OnQuery_TO_REGION_MAP()
  {
    GameSection.SetEventData((object) Singleton<FieldMapTable>.I.GetFieldMapData(this.mapData.mapId).regionId);
  }

  private void OnScreenRotate(bool isPortrait) => this.Reposition();

  private void Reposition()
  {
    if (Vector3.op_Inequality(((Component) this).transform.localScale, Vector3.one))
      ((Component) this).transform.localScale = Vector3.one;
    foreach (UIScreenRotationHandler componentsInChild in ((Component) this._transform).GetComponentsInChildren<UIScreenRotationHandler>())
      componentsInChild.InvokeRotate();
    if (SpecialDeviceManager.HasSpecialDeviceInfo)
    {
      DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
      if (specialDeviceInfo.NeedModifyRegionMapDescriptionList)
      {
        UIWidget component1 = ((Component) this.GetCtrl((Enum) RegionMapDescriptionList.UI.OBJ_BACK)).GetComponent<UIWidget>();
        UISprite component2 = ((Component) this.GetCtrl((Enum) RegionMapDescriptionList.UI.BTN_TO_FIELD)).GetComponent<UISprite>();
        if (SpecialDeviceManager.IsPortrait)
        {
          component1.leftAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBACKAnchorPortrait.left);
          component1.rightAnchor.Set(0.5f, (float) specialDeviceInfo.RegionMapDescriptionListBACKAnchorPortrait.right);
          component1.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBACKAnchorPortrait.bottom);
          component1.topAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBACKAnchorPortrait.top);
          component1.UpdateAnchors();
          component2.leftAnchor.Set(0.5f, (float) specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.left);
          component2.rightAnchor.Set(0.5f, (float) specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.right);
          component2.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.bottom);
          component2.topAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.top);
          component2.UpdateAnchors();
        }
        else
        {
          component1.leftAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBACKAnchorLandscape.left);
          component1.rightAnchor.Set(0.5f, (float) specialDeviceInfo.RegionMapDescriptionListBACKAnchorLandscape.right);
          component1.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBACKAnchorLandscape.bottom);
          component1.topAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBACKAnchorLandscape.top);
          component1.UpdateAnchors();
          component2.leftAnchor.Set(0.5f, (float) specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.left);
          component2.rightAnchor.Set(0.5f, (float) specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.right);
          component2.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.bottom);
          component2.topAnchor.Set(0.0f, (float) specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.top);
          component2.UpdateAnchors();
        }
      }
    }
    ((Component) this.GetCtrl((Enum) RegionMapDescriptionList.UI.SPR_BG_FRAME)).GetComponent<UIRect>().UpdateAnchors();
    this.UpdateAnchors();
    if (!((Component) this.GetCtrl((Enum) RegionMapDescriptionList.UI.SCR_ALL)).gameObject.activeInHierarchy)
      return;
    this.ScrollViewResetPosition((Enum) RegionMapDescriptionList.UI.SCR_ALL);
  }

  public override void Exit()
  {
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && FieldManager.IsValidInGame())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    base.Exit();
  }

  private void OnQuery_InGameQuestAcceptDeliveryItemComplete_YES() => this.BackHome();

  private void BackHome()
  {
    if (!FieldManager.IsValidInGame() || !MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
  }

  protected enum UI
  {
    SCR_ALL,
    TBL_ALL,
    LBL_MAP_NAME,
    LBL_MAP_NAME_D,
    LBL_BORDER_TITLE,
    BTN_TO_FIELD,
    LBL_NON_LIST,
    SPR_BG_FRAME,
    TEX_FIELD,
    OBJ_BACK,
  }

  private struct EnemyDataForDisplay(EnemyTable.EnemyData data, uint level)
  {
    public EnemyTable.EnemyData data = data;
    public int level = (int) level;
  }

  private struct DeliveryDataAndUId(DeliveryTable.DeliveryData data, string uId)
  {
    public DeliveryTable.DeliveryData data = data;
    public string uId = uId;
  }
}
