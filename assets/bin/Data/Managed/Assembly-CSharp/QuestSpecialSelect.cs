// Decompiled with JetBrains decompiler
// Type: QuestSpecialSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestSpecialSelect : GameSection
{
  protected QuestSpecialSelect.SHOW_MODE showMode;
  private const string SPR_QUEST_TAB_TEXT = "QuestTabBtnText";
  private const string SPR_QUEST_TAB_ICON = "QuestTabBtnIcon";
  private const string SPR_QUEST_TAB_BASE = "QuestTabBtnBase";
  private readonly string[] SPR_INDEX = new string[3]
  {
    "01",
    "02",
    "03"
  };
  private readonly string[] SPR_ON_OFF = new string[3]
  {
    "_on",
    "_off",
    "_Gray"
  };
  private SortSettings sortSettings;
  private QuestSortData[] questSortData;
  private QuestItemInfo[] questItemAry;
  protected Delivery[] deliveryInfo;
  protected Delivery[] normalDeliveryInfo;
  private Delivery[] dailyDeliveryInfo;
  private Delivery[] weeklyDeliveryInfo;
  private readonly string[] SPR_FRAME_TYPE = new string[5]
  {
    "RequestPlate_Base",
    "RequestPlate_Event",
    "RequestPlate_Story",
    "RequestPlate_Hard",
    "RequestPlate_SubEvent"
  };
  private const string spriteTabName_ON = "PickeShopBtn_Green_on";
  private const string spriteTabName_OFF = "PickeShopBtn_Normal_off";
  private QuestInfoData[] questInfo;
  private QuestSpecialSelect.UI[] ui_top_crown = new QuestSpecialSelect.UI[3]
  {
    QuestSpecialSelect.UI.OBJ_TOP_CROWN_1,
    QuestSpecialSelect.UI.OBJ_TOP_CROWN_2,
    QuestSpecialSelect.UI.OBJ_TOP_CROWN_3
  };
  private QuestSpecialSelect.UI[] ui_crown = new QuestSpecialSelect.UI[3]
  {
    QuestSpecialSelect.UI.SPR_CROWN_1,
    QuestSpecialSelect.UI.SPR_CROWN_2,
    QuestSpecialSelect.UI.SPR_CROWN_3
  };
  protected bool isInGameScene;
  private string npcText = string.Empty;
  private QuestSpecialSelect.UI[] difficult = new QuestSpecialSelect.UI[10]
  {
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_1,
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_2,
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_3,
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_4,
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_5,
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_6,
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_7,
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_8,
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_9,
    QuestSpecialSelect.UI.OBJ_DIFFICULT_STAR_10
  };
  private DELIVERY_TYPE[][] TAB_TYPES = new DELIVERY_TYPE[3][]
  {
    new DELIVERY_TYPE[2]
    {
      DELIVERY_TYPE.STORY,
      DELIVERY_TYPE.ONCE
    },
    new DELIVERY_TYPE[1],
    new DELIVERY_TYPE[1]{ DELIVERY_TYPE.WEEKLY }
  };
  protected QuestSpecialSelect.UI selectedTab = QuestSpecialSelect.UI.BTN_TAB_NORMAL;
  private bool isDeliveryGridReset = true;
  private List<Texture2D> areaBanners = new List<Texture2D>();
  private uint[] openRegionIds;
  private uint[] validRegionIds;
  protected int releaseRegionId = -1;
  protected bool changeToDeliveryClearEvent;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "DeliveryRewardTable";
      yield return "FieldMapTable";
    }
  }

  public override void Initialize()
  {
    this.isInGameScene = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene";
    this.StartCoroutine(this.DoInitialize());
  }

  protected virtual IEnumerator DoInitialize()
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    if (!this.isInGameScene)
    {
      bool is_recv_delivery = false;
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
      MonoBehaviourSingleton<QuestManager>.I.SendGetDeliveryList((Action<bool>) (b => is_recv_delivery = true));
      while (!is_recv_delivery)
        yield return (object) null;
      this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.QUEST, SortSettings.SETTINGS_TYPE.ORDER_QUEST);
      this.InitQuestInfoData();
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
    }
    this.GetDeliveryList();
    bool is_recv_delivery1 = false;
    MonoBehaviourSingleton<DeliveryManager>.I.SendEventNormalList((Action<bool>) (is_success => is_recv_delivery1 = true));
    while (!is_recv_delivery1)
      yield return (object) null;
    switch (GameSection.GetEventData() as string)
    {
      case "DAILY":
        this.selectedTab = QuestSpecialSelect.UI.BTN_TAB_DAILY;
        break;
      case "WEEKLY":
        this.selectedTab = QuestSpecialSelect.UI.BTN_TAB_WEEKLY;
        break;
      case "NORMAL":
        this.selectedTab = QuestSpecialSelect.UI.BTN_TAB_NORMAL;
        break;
      default:
        this.selectedTab = this.SelectTabByPriority();
        break;
    }
    this.openRegionIds = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL);
    this.validRegionIds = MonoBehaviourSingleton<WorldMapManager>.I.GetValidRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL);
    Array.Reverse((Array) this.validRegionIds);
    for (int i = 0; i < this.validRegionIds.Length; ++i)
    {
      int validRegionId = (int) this.validRegionIds[i];
      int num = MonoBehaviourSingleton<WorldMapManager>.I.IsOpenRegion((uint) validRegionId) ? 1 : 0;
      LoadObject bannerObj = (LoadObject) null;
      bannerObj = num == 0 ? load_queue.Load(RESOURCE_CATEGORY.AREA_BANNER_CLOSE, ResourceName.GetCloseAreaBanner(validRegionId)) : load_queue.Load(RESOURCE_CATEGORY.AREA_BANNER, ResourceName.GetAreaBanner(validRegionId));
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      this.areaBanners.Add(bannerObj.loadedObject as Texture2D);
      bannerObj = (LoadObject) null;
    }
    this.EndInitialize();
  }

  private QuestSpecialSelect.UI SelectTabByPriority()
  {
    if (this.normalDeliveryInfo != null)
    {
      int index = 0;
      for (int length = this.normalDeliveryInfo.Length; index < length; ++index)
      {
        if (Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.normalDeliveryInfo[index].dId).type == DELIVERY_TYPE.STORY)
          return QuestSpecialSelect.UI.BTN_TAB_NORMAL;
      }
    }
    if (this.dailyDeliveryInfo != null && this.dailyDeliveryInfo.Length >= 1)
      return QuestSpecialSelect.UI.BTN_TAB_DAILY;
    if (this.normalDeliveryInfo != null && this.normalDeliveryInfo.Length >= 1)
      return QuestSpecialSelect.UI.BTN_TAB_NORMAL;
    return this.weeklyDeliveryInfo != null && this.weeklyDeliveryInfo.Length >= 1 ? QuestSpecialSelect.UI.BTN_TAB_WEEKLY : QuestSpecialSelect.UI.BTN_TAB_DAILY;
  }

  protected void EndInitialize() => base.Initialize();

  private void InitQuestInfoData()
  {
    this.questInfo = MonoBehaviourSingleton<QuestManager>.I.GetQuestInfoData();
    if (this.questInfo == null)
      return;
    Array.Sort<QuestInfoData>(this.questInfo, (Comparison<QuestInfoData>) ((l, r) => (int) r.questData.tableData.questID - (int) l.questData.tableData.questID));
  }

  private void Update()
  {
    if ((double) MonoBehaviourSingleton<DeliveryManager>.I.dailyUpdateRemainTime <= 0.0 || (double) MonoBehaviourSingleton<DeliveryManager>.I.weeklyUpdateRemainTime <= 0.0)
      return;
    MonoBehaviourSingleton<DeliveryManager>.I.dailyUpdateRemainTime -= Time.deltaTime;
    MonoBehaviourSingleton<DeliveryManager>.I.weeklyUpdateRemainTime -= Time.deltaTime;
    this.ShowNonDeliveryList();
  }

  public override void UpdateUI()
  {
    if (this.changeToDeliveryClearEvent)
      return;
    this.ShowSelectUI();
    this.UpdateAnchors();
    this.SetBadge((Enum) QuestSpecialSelect.UI.BTN_EVENT, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(), (SpriteAlignment) 1, 24, -10, true);
    this.SetBadge((Enum) QuestSpecialSelect.UI.BTN_TAB_NORMAL, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(this.TAB_TYPES[0]), (SpriteAlignment) 3, -10, -10, true);
    this.SetBadge((Enum) QuestSpecialSelect.UI.BTN_TAB_DAILY, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(this.TAB_TYPES[1]), (SpriteAlignment) 3, -10, -10, true);
    this.SetBadge((Enum) QuestSpecialSelect.UI.BTN_TAB_WEEKLY, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(this.TAB_TYPES[2]), (SpriteAlignment) 3, -10, -10, true);
    this.SetNew(QuestSpecialSelect.UI.BTN_TAB_DAILY, GameSaveData.instance.IsRecommendedDailyDeliveryCheck());
    this.SetNew(QuestSpecialSelect.UI.BTN_TAB_WEEKLY, GameSaveData.instance.IsRecommendedWeeklyDeliveryCheck());
  }

  private void SetNew(QuestSpecialSelect.UI btn, bool is_visible)
  {
    this.SetActive(this.GetCtrl((Enum) btn), (Enum) QuestSpecialSelect.UI.SPR_NEW, is_visible);
  }

  private void OpenTutorial()
  {
    if (this.isInGameScene || !HomeTutorialManager.DoesTutorial())
      return;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep2_1");
  }

  protected void SetupDeliveryListItem(Transform t, DeliveryTable.DeliveryData info)
  {
    QuestRequestItem questRequestItem = ((Component) t).GetComponent<QuestRequestItem>();
    if (Object.op_Equality((Object) questRequestItem, (Object) null))
      questRequestItem = ((Component) t).gameObject.AddComponent<QuestRequestItem>();
    questRequestItem.InitUI();
    questRequestItem.Setup(t, info);
  }

  protected void SetCompletedHaveCount(Transform t, DeliveryTable.DeliveryData info)
  {
    int need;
    MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryDataAllNeeds((int) info.id, out int _, out need, out string _, out string _);
    this.SetLabelText(t, (Enum) QuestSpecialSelect.UI.LBL_HAVE, need.ToString());
  }

  protected virtual void ShowSelectUI()
  {
    int num1 = (this.isInGameScene ? 1 : (!TutorialStep.HasQuestSpecialUnlocked() ? 1 : 0)) != 0 ? 2 : 1;
    int index1 = this.showMode == QuestSpecialSelect.SHOW_MODE.DELIVERY ? 0 : num1;
    int index2 = this.showMode == QuestSpecialSelect.SHOW_MODE.QUEST ? 0 : num1;
    int index3 = this.showMode == QuestSpecialSelect.SHOW_MODE.ORDER ? 0 : num1;
    this.SetActive((Enum) QuestSpecialSelect.UI.OBJ_DELIVERY_ROOT, this.showMode == QuestSpecialSelect.SHOW_MODE.DELIVERY);
    this.SetActive((Enum) QuestSpecialSelect.UI.OBJ_QUEST_ROOT, this.showMode == QuestSpecialSelect.SHOW_MODE.QUEST);
    this.SetActive((Enum) QuestSpecialSelect.UI.OBJ_ORDER_ROOT, this.showMode == QuestSpecialSelect.SHOW_MODE.ORDER);
    this.SetButtonSprite((Enum) QuestSpecialSelect.UI.BTN_DELIVERY, "QuestTabBtnBase" + this.SPR_ON_OFF[index1], true);
    this.SetSprite((Enum) QuestSpecialSelect.UI.SPR_DELIVERY_TEXT, $"QuestTabBtnText{this.SPR_INDEX[0]}{this.SPR_ON_OFF[index1]}");
    this.SetSprite((Enum) QuestSpecialSelect.UI.SPR_DELIVERY_ICON, $"QuestTabBtnIcon{this.SPR_INDEX[0]}{this.SPR_ON_OFF[index1]}");
    this.SetButtonSprite((Enum) QuestSpecialSelect.UI.BTN_QUEST, "QuestTabBtnBase" + this.SPR_ON_OFF[index2], true);
    this.SetSprite((Enum) QuestSpecialSelect.UI.SPR_QUEST_TEXT, $"QuestTabBtnText{this.SPR_INDEX[1]}{this.SPR_ON_OFF[index2]}");
    this.SetSprite((Enum) QuestSpecialSelect.UI.SPR_QUEST_ICON, $"QuestTabBtnIcon{this.SPR_INDEX[1]}{this.SPR_ON_OFF[index2]}");
    this.SetButtonSprite((Enum) QuestSpecialSelect.UI.BTN_ORDER, "QuestTabBtnBase" + this.SPR_ON_OFF[index3], true);
    this.SetSprite((Enum) QuestSpecialSelect.UI.SPR_ORDER_TEXT, $"QuestTabBtnText{this.SPR_INDEX[2]}{this.SPR_ON_OFF[index3]}");
    this.SetSprite((Enum) QuestSpecialSelect.UI.SPR_ORDER_ICON, $"QuestTabBtnIcon{this.SPR_INDEX[2]}{this.SPR_ON_OFF[index3]}");
    if (!TutorialStep.HasQuestSpecialUnlocked())
    {
      this.SetButtonEnabled((Enum) QuestSpecialSelect.UI.BTN_QUEST, false);
      this.SetButtonEnabled((Enum) QuestSpecialSelect.UI.BTN_ORDER, false);
      this.SetActive((Enum) QuestSpecialSelect.UI.BTN_EVENT, false);
    }
    if (!this.isInGameScene)
    {
      this.SetRenderNPCModel((Enum) QuestSpecialSelect.UI.TEX_NPCMODEL, 0, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV);
      this.SetLabelText((Enum) QuestSpecialSelect.UI.LBL_NPC_MESSAGE, this.npcText);
    }
    this.SetActive((Enum) QuestSpecialSelect.UI.OBJ_NPC_MESSAGE, !this.isInGameScene);
    if (this.showMode == QuestSpecialSelect.SHOW_MODE.DELIVERY)
    {
      this.SetActive((Enum) QuestSpecialSelect.UI.SPR_TAB_NORMAL, QuestSpecialSelect.UI.BTN_TAB_NORMAL == this.selectedTab);
      this.SetActive((Enum) QuestSpecialSelect.UI.SPR_TAB_DAILY, QuestSpecialSelect.UI.BTN_TAB_DAILY == this.selectedTab);
      this.SetActive((Enum) QuestSpecialSelect.UI.SPR_TAB_WEEKLY, QuestSpecialSelect.UI.BTN_TAB_WEEKLY == this.selectedTab);
      this.SetButtonSprite((Enum) QuestSpecialSelect.UI.BTN_TAB_NORMAL, QuestSpecialSelect.UI.BTN_TAB_NORMAL == this.selectedTab ? "PickeShopBtn_Green_on" : "PickeShopBtn_Normal_off");
      this.SetButtonSprite((Enum) QuestSpecialSelect.UI.BTN_TAB_DAILY, QuestSpecialSelect.UI.BTN_TAB_DAILY == this.selectedTab ? "PickeShopBtn_Green_on" : "PickeShopBtn_Normal_off");
      this.SetButtonSprite((Enum) QuestSpecialSelect.UI.BTN_TAB_WEEKLY, QuestSpecialSelect.UI.BTN_TAB_WEEKLY == this.selectedTab ? "PickeShopBtn_Green_on" : "PickeShopBtn_Normal_off");
      ((Collider) ((Component) this.GetCtrl((Enum) QuestSpecialSelect.UI.BTN_TAB_NORMAL)).gameObject.GetComponent<BoxCollider>()).enabled = QuestSpecialSelect.UI.BTN_TAB_NORMAL != this.selectedTab;
      ((Collider) ((Component) this.GetCtrl((Enum) QuestSpecialSelect.UI.BTN_TAB_DAILY)).gameObject.GetComponent<BoxCollider>()).enabled = QuestSpecialSelect.UI.BTN_TAB_DAILY != this.selectedTab;
      ((Collider) ((Component) this.GetCtrl((Enum) QuestSpecialSelect.UI.BTN_TAB_WEEKLY)).gameObject.GetComponent<BoxCollider>()).enabled = QuestSpecialSelect.UI.BTN_TAB_WEEKLY != this.selectedTab;
      this.SetNPCMessage(this.selectedTab);
      switch (this.selectedTab)
      {
        case QuestSpecialSelect.UI.BTN_TAB_NORMAL:
          this.SetDeliveryList(this.normalDeliveryInfo);
          break;
        case QuestSpecialSelect.UI.BTN_TAB_DAILY:
          this.SetDeliveryList(this.dailyDeliveryInfo);
          break;
        case QuestSpecialSelect.UI.BTN_TAB_WEEKLY:
          this.SetDeliveryList(this.weeklyDeliveryInfo);
          break;
      }
    }
    else if (this.showMode == QuestSpecialSelect.SHOW_MODE.ORDER)
    {
      if (this.questItemAry == null && MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() > 0)
      {
        List<QuestItemInfo> list = new List<QuestItemInfo>();
        MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory((Action<QuestItemInfo>) (item =>
        {
          if (item.infoData.questData.num <= 0)
            return;
          list.Add(item);
        }));
        this.questItemAry = list.ToArray();
        this.GetCtrl((Enum) QuestSpecialSelect.UI.GRD_ORDER_QUEST).DestroyChildren();
      }
      if (this.questItemAry == null || this.questItemAry.Length == 0)
      {
        this.SetActive((Enum) QuestSpecialSelect.UI.BTN_SORT, false);
        this.SetActive((Enum) QuestSpecialSelect.UI.GRD_ORDER_QUEST, false);
        this.SetActive((Enum) QuestSpecialSelect.UI.STR_ORDER_NON_LIST, true);
      }
      else
      {
        this.questSortData = this.sortSettings.CreateSortAry<QuestItemInfo, QuestSortData>(this.questItemAry);
        this.SetActive((Enum) QuestSpecialSelect.UI.GRD_ORDER_QUEST, true);
        this.SetActive((Enum) QuestSpecialSelect.UI.STR_ORDER_NON_LIST, false);
        this.SetActive((Enum) QuestSpecialSelect.UI.BTN_SORT, true);
        this.SetLabelText((Enum) QuestSpecialSelect.UI.LBL_SORT, this.sortSettings.GetSortLabel());
        this.SetToggle((Enum) QuestSpecialSelect.UI.TGL_ICON_ASC, this.sortSettings.orderTypeAsc);
        this.SetDynamicList((Enum) QuestSpecialSelect.UI.GRD_ORDER_QUEST, "QuestListOrderItem", this.questSortData.Length, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
        {
          this.SetActive(t, true);
          this.SetEvent(t, "SELECT_ORDER", i);
          QuestInfoData info = this.questSortData[i].itemData.infoData;
          int num2 = (int) (info.questData.tableData.difficulty + 1);
          int index4 = 0;
          for (int length = this.difficult.Length; index4 < length; ++index4)
            this.SetActive(t, (Enum) this.difficult[index4], index4 < num2);
          if (!is_recycle)
          {
            this.ResetTween(t, (Enum) QuestSpecialSelect.UI.TWN_DIFFICULT_STAR);
            this.PlayTween(t, (Enum) QuestSpecialSelect.UI.TWN_DIFFICULT_STAR, is_input_block: false);
          }
          EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) info.questData.tableData.GetMainEnemyID());
          QuestSortData questSortData = this.questSortData[i];
          ItemIcon.Create(questSortData.GetIconType(), questSortData.GetIconID(), new RARITY_TYPE?(questSortData.GetRarity()), this.FindCtrl(t, (Enum) QuestSpecialSelect.UI.OBJ_ENEMY), questSortData.GetIconElement()).SetEnableCollider(false);
          this.SetActive(t, (Enum) QuestSpecialSelect.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
          this.SetElementSprite(t, (Enum) QuestSpecialSelect.UI.SPR_ELEMENT, (int) enemyData.element);
          this.SetElementSprite(t, (Enum) QuestSpecialSelect.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
          this.SetActive(t, (Enum) QuestSpecialSelect.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
          this.SetLabelText(t, (Enum) QuestSpecialSelect.UI.LBL_QUEST_NAME, info.questData.tableData.questText);
          int num3 = 1;
          ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) info.questData.tableData.questID == (long) data.questId));
          if (clearStatusQuest != null)
            num3 = clearStatusQuest.questStatus;
          int num4 = i + 100;
          this.SetToggleGroup(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_NEW, num4);
          this.SetToggleGroup(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_CLEARED, num4);
          this.SetToggleGroup(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_COMPLETE, num4);
          switch (num3)
          {
            case 1:
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_NEW, true);
              this.SetVisibleWidgetEffect((Enum) QuestSpecialSelect.UI.SCR_ORDER_QUEST, t, (Enum) QuestSpecialSelect.UI.SPR_ICON_NEW, "ef_ui_questselect_new");
              break;
            case 3:
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_CLEARED, true);
              break;
            case 4:
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_COMPLETE, true);
              this.SetVisibleWidgetEffect((Enum) QuestSpecialSelect.UI.SCR_ORDER_QUEST, t, (Enum) QuestSpecialSelect.UI.SPR_ICON_COMPLETE, "ef_ui_questselect_complete");
              break;
            default:
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_NEW, false);
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_CLEARED, false);
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_COMPLETE, false);
              break;
          }
          this.SetLabelText(t, (Enum) QuestSpecialSelect.UI.LBL_ORDER_NUM, info.questData.num.ToString());
          this.SetActive(t, (Enum) QuestSpecialSelect.UI.LBL_REMAIN, false);
        }));
      }
    }
    else
    {
      if (this.showMode != QuestSpecialSelect.SHOW_MODE.QUEST)
        return;
      if (this.questInfo == null || this.questInfo.Length == 0)
      {
        this.SetActive((Enum) QuestSpecialSelect.UI.GRD_QUEST, false);
        this.SetActive((Enum) QuestSpecialSelect.UI.STR_QUEST_NON_LIST, true);
      }
      else
      {
        this.SetActive((Enum) QuestSpecialSelect.UI.STR_QUEST_NON_LIST, false);
        this.SetActive((Enum) QuestSpecialSelect.UI.GRD_QUEST, true);
        this.SetDynamicList((Enum) QuestSpecialSelect.UI.GRD_QUEST, "QuestListItem", this.questInfo.Length, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
        {
          this.SetEvent(t, "SELECT_QUEST", i);
          QuestInfoData info = this.questInfo[i];
          int num5 = (int) (info.questData.tableData.difficulty + 1);
          int index5 = 0;
          for (int length = this.difficult.Length; index5 < length; ++index5)
            this.SetActive(t, (Enum) this.difficult[index5], index5 < num5);
          if (!is_recycle)
          {
            this.ResetTween(t, (Enum) QuestSpecialSelect.UI.TWN_DIFFICULT_STAR);
            this.PlayTween(t, (Enum) QuestSpecialSelect.UI.TWN_DIFFICULT_STAR, is_input_block: false);
          }
          EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) info.questData.tableData.GetMainEnemyID());
          if (enemyData != null)
          {
            this.SetActive(t, (Enum) QuestSpecialSelect.UI.OBJ_ENEMY, true);
            ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, info.questData.tableData.questType == QUEST_TYPE.ORDER ? new RARITY_TYPE?(info.questData.tableData.rarity) : new RARITY_TYPE?(), this.FindCtrl(t, (Enum) QuestSpecialSelect.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
            this.SetActive(t, (Enum) QuestSpecialSelect.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
            this.SetElementSprite(t, (Enum) QuestSpecialSelect.UI.SPR_ELEMENT, (int) enemyData.element);
            this.SetElementSprite(t, (Enum) QuestSpecialSelect.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
            this.SetActive(t, (Enum) QuestSpecialSelect.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
          }
          else
          {
            this.SetActive(t, (Enum) QuestSpecialSelect.UI.OBJ_ENEMY, false);
            this.SetElementSprite(t, (Enum) QuestSpecialSelect.UI.SPR_WEAK_ELEMENT, 6);
            this.SetActive(t, (Enum) QuestSpecialSelect.UI.STR_NON_WEAK_ELEMENT, true);
          }
          this.SetLabelText(t, (Enum) QuestSpecialSelect.UI.LBL_QUEST_NUM, string.Empty);
          this.SetLabelText(t, (Enum) QuestSpecialSelect.UI.LBL_QUEST_NAME, info.questData.tableData.questText);
          if (!info.isExistMission)
          {
            this.SetActive(t, (Enum) QuestSpecialSelect.UI.OBJ_MISSION_INFO_ROOT, false);
          }
          else
          {
            this.SetActive(t, (Enum) QuestSpecialSelect.UI.OBJ_MISSION_INFO_ROOT, true);
            int index6 = 0;
            for (int length = info.missionData.Length; index6 < length; ++index6)
            {
              this.SetActive(t, (Enum) this.ui_top_crown[index6], info.missionData[index6] != null);
              if (info.missionData[index6] != null)
                this.SetActive(t, (Enum) this.ui_crown[index6], info.missionData[index6].state >= CLEAR_STATUS.CLEAR);
            }
          }
          int num6 = 1;
          ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) info.questData.tableData.questID == (long) data.questId));
          if (clearStatusQuest != null)
            num6 = clearStatusQuest.questStatus;
          int num7 = i + 100;
          this.SetToggleGroup(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_NEW, num7);
          this.SetToggleGroup(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_CLEARED, num7);
          this.SetToggleGroup(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_COMPLETE, num7);
          switch (num6)
          {
            case 1:
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_NEW, true);
              this.SetVisibleWidgetEffect((Enum) QuestSpecialSelect.UI.SCR_NORMAL_QUEST, t, (Enum) QuestSpecialSelect.UI.SPR_ICON_NEW, "ef_ui_questselect_new");
              break;
            case 3:
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_CLEARED, true);
              break;
            case 4:
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_COMPLETE, true);
              this.SetVisibleWidgetEffect((Enum) QuestSpecialSelect.UI.SCR_NORMAL_QUEST, t, (Enum) QuestSpecialSelect.UI.SPR_ICON_COMPLETE, "ef_ui_questselect_complete");
              break;
            default:
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_NEW, false);
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_CLEARED, false);
              this.SetToggle(t, (Enum) QuestSpecialSelect.UI.OBJ_ICON_COMPLETE, false);
              break;
          }
        }));
      }
    }
  }

  protected virtual void SetDeliveryList(Delivery[] list)
  {
    if (this.selectedTab == QuestSpecialSelect.UI.BTN_TAB_NORMAL && this.openRegionIds.Length >= 2)
    {
      this.SetActive((Enum) QuestSpecialSelect.UI.OBJ_DELIVERY_BAR, false);
      this.SetActive((Enum) QuestSpecialSelect.UI.GRD_DELIVERY, false);
      this.SetActive((Enum) QuestSpecialSelect.UI.LBL_DELIVERY_NON_LIST, false);
      this.SetActive((Enum) QuestSpecialSelect.UI.GRD_AREA, true);
      this.SetActive((Enum) QuestSpecialSelect.UI.OBJ_AREA_BAR, true);
      List<QuestSpecialSelect.AreaQuestInfo> areaInfos = new List<QuestSpecialSelect.AreaQuestInfo>();
      for (int index = 0; index < this.validRegionIds.Length; ++index)
      {
        bool opened = MonoBehaviourSingleton<WorldMapManager>.I.IsOpenRegion(this.validRegionIds[index]);
        RegionTable.Data data = Singleton<RegionTable>.I.GetData(this.validRegionIds[index]);
        if (!opened)
        {
          areaInfos.Add(new QuestSpecialSelect.AreaQuestInfo(data, this.areaBanners[index], false, opened));
        }
        else
        {
          bool cleared = this.IsCleardRegion(data);
          areaInfos.Add(new QuestSpecialSelect.AreaQuestInfo(data, this.areaBanners[index], cleared, opened));
        }
      }
      this.SetDynamicList((Enum) QuestSpecialSelect.UI.GRD_AREA, "QuestAreaListItem", areaInfos.Count, true, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        QuestSpecialSelect.AreaQuestInfo areaQuestInfo = areaInfos[i];
        Texture2D bannerTex = areaQuestInfo.bannerTex;
        if (Object.op_Inequality((Object) bannerTex, (Object) null))
        {
          Transform ctrl = this.FindCtrl(t, (Enum) QuestSpecialSelect.UI.TEX_AREA_BANNER);
          this.SetActive(ctrl, true);
          this.SetTexture(ctrl, (Texture) bannerTex);
        }
        RegionTable.Data regionData = areaQuestInfo.regionData;
        if (areaQuestInfo.opened)
          this.SetEvent(t, "SELECT_AREA", (int) regionData.regionId);
        else
          this.SetEvent(t, "SELECT_CLOSE_AREA", (int) regionData.regionId);
        int regionDeliveryNum = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableRegionDeliveryNum((int) regionData.regionId, regionData.groupId);
        this.SetBadge(this.FindCtrl(t, (Enum) QuestSpecialSelect.UI.TEX_AREA_BANNER), regionDeliveryNum, (SpriteAlignment) 3, -16, -3);
        this.SetActive(t, (Enum) QuestSpecialSelect.UI.SPR_CLEARED_AREA, areaQuestInfo.cleared);
        bool is_visible = false;
        if (areaQuestInfo.opened)
        {
          RegionTable.Data data = Singleton<RegionTable>.I.GetData(regionData.groupId, REGION_DIFFICULTY_TYPE.HARD);
          EventNormalListData eventNormalListData = data == null || !MonoBehaviourSingleton<WorldMapManager>.I.IsOpenRegion(data.regionId) ? MonoBehaviourSingleton<DeliveryManager>.I.GetEventNormalListData((int) regionData.regionId) : MonoBehaviourSingleton<DeliveryManager>.I.GetEventNormalListData((int) data.regionId);
          if (eventNormalListData != null)
          {
            if (eventNormalListData.numerator >= eventNormalListData.denominator)
              this.SetLabelText(t, (Enum) QuestSpecialSelect.UI.LBL_DELIVERY_NUM, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 43U)));
            else
              this.SetLabelText(t, (Enum) QuestSpecialSelect.UI.LBL_DELIVERY_NUM, $"{eventNormalListData.denominator - eventNormalListData.numerator} Missions Left");
            is_visible = true;
          }
        }
        this.SetActive(t, (Enum) QuestSpecialSelect.UI.LBL_DELIVERY_NUM, is_visible);
      }));
      this.isDeliveryGridReset = false;
      this.RepositionAreaGrid();
    }
    else
    {
      if (list == null)
        return;
      this.SetActive((Enum) QuestSpecialSelect.UI.OBJ_AREA_BAR, false);
      this.SetActive((Enum) QuestSpecialSelect.UI.GRD_AREA, false);
      this.SetActive((Enum) QuestSpecialSelect.UI.GRD_DELIVERY, list.Length != 0);
      this.SetActive((Enum) QuestSpecialSelect.UI.LBL_DELIVERY_NON_LIST, list.Length == 0);
      this.SetActive((Enum) QuestSpecialSelect.UI.OBJ_DELIVERY_BAR, true);
      this.SetDynamicList((Enum) QuestSpecialSelect.UI.GRD_DELIVERY, "QuestRequestItem", list.Length, this.isDeliveryGridReset, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetActive(t, true);
        int index = Array.FindIndex<Delivery>(this.deliveryInfo, (Predicate<Delivery>) (d => d.uId == list[i].uId));
        DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[index].dId);
        if (deliveryTableData.subType == DELIVERY_SUB_TYPE.READ_STORY)
          this.SetEvent(t, "READ_STORY", index);
        else
          this.SetEvent(t, "SELECT_DELIVERY", index);
        this.SetupDeliveryListItem(t, deliveryTableData);
      }));
      this.isDeliveryGridReset = false;
      this.ShowNonDeliveryList();
    }
  }

  private void RepositionAreaGrid()
  {
    UIScrollView component1 = ((Component) this.GetCtrl((Enum) QuestSpecialSelect.UI.SCR_AREA)).GetComponent<UIScrollView>();
    UIPanel component2 = ((Component) this.GetCtrl((Enum) QuestSpecialSelect.UI.SCR_AREA)).GetComponent<UIPanel>();
    uint openRegionId = this.openRegionIds[this.openRegionIds.Length - 1];
    int num = -1;
    int index = 0;
    for (int length = this.validRegionIds.Length; index < length; ++index)
    {
      if ((int) openRegionId == (int) this.validRegionIds[index])
      {
        num = index;
        break;
      }
    }
    if (num <= -1 || num <= 3)
      return;
    int length1 = this.validRegionIds.Length;
    float y = (float) num / (float) length1;
    if ((double) y >= 0.60000002384185791)
      y = (float) (num + 2) / (float) length1;
    component1.SetDragAmount(0.5f, y, true);
    Vector2 clipOffset = component2.clipOffset;
    ((Component) component1).transform.localPosition = Vector2.op_Implicit(Vector2.op_UnaryNegation(clipOffset));
  }

  private bool IsCleardRegion(RegionTable.Data data)
  {
    int index = 0;
    for (int length = this.normalDeliveryInfo.Length; index < length; ++index)
    {
      Delivery delivery = this.normalDeliveryInfo[index];
      if (delivery.dId != 0)
      {
        DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery.dId);
        if ((long) deliveryTableData.regionId == (long) data.regionId || data.groupId > 0 && deliveryTableData.regionId == data.groupId)
          return false;
      }
    }
    return true;
  }

  private void ShowNonDeliveryList()
  {
    switch (this.selectedTab)
    {
      case QuestSpecialSelect.UI.BTN_TAB_NORMAL:
        if (this.normalDeliveryInfo == null || this.normalDeliveryInfo.Length != 0)
          break;
        this.SetLabelText((Enum) QuestSpecialSelect.UI.LBL_DELIVERY_NON_LIST, StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 100U));
        break;
      case QuestSpecialSelect.UI.BTN_TAB_DAILY:
        if (this.dailyDeliveryInfo == null || this.dailyDeliveryInfo.Length != 0)
          break;
        TimeSpan span1 = TimeSpan.FromSeconds((double) MonoBehaviourSingleton<DeliveryManager>.I.dailyUpdateRemainTime);
        this.SetLabelText((Enum) QuestSpecialSelect.UI.LBL_DELIVERY_NON_LIST, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 101U), (object) QuestSpecialSelect.GetRemainTimeText(span1)));
        break;
      case QuestSpecialSelect.UI.BTN_TAB_WEEKLY:
        if (this.weeklyDeliveryInfo == null || this.weeklyDeliveryInfo.Length != 0)
          break;
        TimeSpan span2 = TimeSpan.FromSeconds((double) MonoBehaviourSingleton<DeliveryManager>.I.weeklyUpdateRemainTime);
        this.SetLabelText((Enum) QuestSpecialSelect.UI.LBL_DELIVERY_NON_LIST, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 102U), (object) QuestSpecialSelect.GetRemainTimeText(span2)));
        break;
    }
  }

  public static string GetRemainTimeText(TimeSpan span)
  {
    string str = "";
    if (span.Seconds > 0)
      span = span.Add(TimeSpan.FromMinutes(1.0));
    if (span.Days > 0)
      str += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 0U), (object) span.Days);
    if (span.Hours > 0)
      str += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 1U), (object) span.Hours);
    if (span.Minutes > 0)
      str += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2U), (object) span.Minutes);
    return str == "" ? string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2U), (object) 0) : str;
  }

  protected void OnQuery_TAB_DELIVERY()
  {
    if (this.showMode == QuestSpecialSelect.SHOW_MODE.DELIVERY)
      return;
    this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_DELIVERY);
    this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_AREA);
    this.ChangeToggle(QuestSpecialSelect.SHOW_MODE.DELIVERY);
    this.ResetTween((Enum) QuestSpecialSelect.UI.BTN_DELIVERY);
    this.PlayTween((Enum) QuestSpecialSelect.UI.BTN_DELIVERY, is_input_block: false);
  }

  protected void OnQuery_TAB_ORDER()
  {
    if (this.showMode == QuestSpecialSelect.SHOW_MODE.ORDER)
      return;
    this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_ORDER_QUEST);
    this.ChangeToggle(QuestSpecialSelect.SHOW_MODE.ORDER);
    this.ResetTween((Enum) QuestSpecialSelect.UI.BTN_ORDER);
    this.PlayTween((Enum) QuestSpecialSelect.UI.BTN_ORDER, is_input_block: false);
  }

  protected void OnQuery_TAB_QUEST()
  {
    if (this.showMode == QuestSpecialSelect.SHOW_MODE.QUEST)
      return;
    this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_QUEST);
    this.ChangeToggle(QuestSpecialSelect.SHOW_MODE.QUEST);
    this.ResetTween((Enum) QuestSpecialSelect.UI.BTN_QUEST);
    this.PlayTween((Enum) QuestSpecialSelect.UI.BTN_QUEST, is_input_block: false);
    if (GameSection.GetEventData() == null)
      return;
    MonoBehaviourSingleton<QuestManager>.I.currentDeliveryId = (uint) GameSection.GetEventData();
  }

  public void ChangeToggle(QuestSpecialSelect.SHOW_MODE show_mode)
  {
    this.showMode = show_mode;
    this.ShowSelectUI();
  }

  protected override void OnOpen()
  {
    this.SetNPCMessage(this.selectedTab);
    this.RemoveRecommend();
    this.changeToDeliveryClearEvent = false;
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(0U);
    MonoBehaviourSingleton<QuestManager>.I.currentDeliveryId = 0U;
    base.OnOpen();
  }

  protected override void OnClose()
  {
    this.RemoveNew(this.selectedTab);
    base.OnClose();
  }

  private void SetNPCMessage(QuestSpecialSelect.UI tab)
  {
    if (this.isInGameScene)
      return;
    string str = "";
    switch (tab)
    {
      case QuestSpecialSelect.UI.BTN_TAB_NORMAL:
        str = !MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(this.TAB_TYPES[0]) ? "_COMPLETE" : "";
        break;
      case QuestSpecialSelect.UI.BTN_TAB_DAILY:
        str = !MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(this.TAB_TYPES[1]) ? "_COMPLETE" : "_DAILY";
        break;
      case QuestSpecialSelect.UI.BTN_TAB_WEEKLY:
        str = !MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(this.TAB_TYPES[2]) ? "_COMPLETE" : "_WEEKLY";
        break;
    }
    NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection($"{this.sectionData.sectionName}{str}_TEXT");
    if (section != null)
    {
      NPCMessageTable.Message npcMessage = section.GetNPCMessage();
      if (npcMessage != null)
        this.npcText = npcMessage.GetReplaceText();
    }
    else
      this.npcText = this.sectionData.GetText("NPC_MESSAGE_" + (object) Random.Range(0, 3));
    this.SetLabelText((Enum) QuestSpecialSelect.UI.LBL_NPC_MESSAGE, this.npcText);
  }

  protected virtual void RemoveRecommend()
  {
    if (!GameSaveData.instance.IsRecommendedDeliveryCheck())
      return;
    GameSaveData.instance.recommendedDeliveryCheck = 0;
    GameSaveData.Save();
  }

  public override void StartSection()
  {
    UITweenAddToChildrenCtrl component = this.GetComponent<UITweenAddToChildrenCtrl>((Enum) new QuestSpecialSelect.UI[3]
    {
      QuestSpecialSelect.UI.GRD_DELIVERY,
      QuestSpecialSelect.UI.GRD_ORDER_QUEST,
      QuestSpecialSelect.UI.GRD_QUEST
    }[(int) this.showMode]);
    if (Object.op_Inequality((Object) component, (Object) null))
      component.InitTween();
    this.OpenTutorial();
    base.StartSection();
  }

  public void OnQuery_SELECT_ORDER()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData < 0 || eventData >= this.questSortData.Length)
    {
      GameSection.StopEvent();
    }
    else
    {
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.questSortData[eventData].GetTableID());
      GameSection.SetEventData((object) this.questSortData[eventData].itemData.infoData);
    }
  }

  public void OnQuery_SELECT_DELIVERY()
  {
    int index = (int) GameSection.GetEventData();
    bool is_enough_material = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(this.deliveryInfo[index].dId);
    int delivery_id = this.deliveryInfo[index].dId;
    bool is_happen_quest = false;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery_id);
    if (deliveryTableData != null)
    {
      QuestTable.QuestTableData questData = deliveryTableData.GetQuestData();
      if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
        is_happen_quest = true;
    }
    if (is_enough_material)
    {
      if (!TutorialStep.HasFirstDeliveryCompleted() && this.isInGameScene)
        GameSection.ChangeEvent("DELIVERY_ITEM_COMPLETE");
      else if (this.isInGameScene)
      {
        GameSection.StayEvent();
        MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop((Action<bool>) (b =>
        {
          if (!b)
            return;
          this.SendDeliveryComplete(index, delivery_id, is_enough_material, is_happen_quest);
          if (Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint) delivery_id) == null)
            return;
          MonoBehaviourSingleton<DeliveryManager>.I.CheckAnnouncePortalOpen();
        }));
      }
      else
      {
        GameSection.StayEvent();
        this.SendDeliveryComplete(index, delivery_id, is_enough_material, is_happen_quest);
      }
    }
    else if (is_happen_quest)
      GameSection.ChangeEvent("SELECT_DELIVERY_HAPPEN", (object) new object[2]
      {
        (object) delivery_id,
        null
      });
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) delivery_id,
        null
      });
  }

  public void OnQuery_READ_STORY()
  {
    int eventData = (int) GameSection.GetEventData();
    bool is_enough_material = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(this.deliveryInfo[eventData].dId);
    int dId = this.deliveryInfo[eventData].dId;
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) dId);
    if (is_enough_material)
    {
      GameSection.StayEvent();
      this.SendDeliveryComplete(eventData, dId, is_enough_material, false);
    }
    else
    {
      EventData[] eventDataArray = new EventData[2]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("COMPLETE_READ_STORY", (object) (int) deliveryTableData.readScriptId)
      };
      GameSection.SetEventData((object) new object[4]
      {
        (object) (int) deliveryTableData.readScriptId,
        (object) "",
        (object) "",
        (object) eventDataArray
      });
    }
  }

  protected void SendDeliveryComplete(
    int index,
    int delivery_id,
    bool is_enough_material,
    bool is_happen_quest)
  {
    DeliveryTable.DeliveryData table = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) this.deliveryInfo[index].dId);
    this.changeToDeliveryClearEvent = true;
    bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
    bool enable_clear_event = table.clearEventID > 0U;
    MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(this.deliveryInfo[index].uId, enable_clear_event, (Action<bool, DeliveryRewardList>) ((is_success, recv_reward) =>
    {
      if (is_success)
      {
        List<FieldMapTable.PortalTableData> relationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint) delivery_id);
        for (int index1 = 0; index1 < relationPortalData.Count; ++index1)
          GameSaveData.instance.newReleasePortals.Add(relationPortalData[index1].portalID);
        if (is_tutorial)
          TutorialStep.isSendFirstRewardComplete = true;
        if (!enable_clear_event)
        {
          MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
          if (is_happen_quest)
            GameSection.ChangeStayEvent("DELIVERY_REWARD_HAPPEN", (object) new object[2]
            {
              (object) delivery_id,
              (object) recv_reward
            });
          else
            GameSection.ChangeStayEvent("DELIVERY_REWARD", (object) new object[2]
            {
              (object) delivery_id,
              (object) recv_reward
            });
        }
        else
        {
          GameSection.ChangeStayEvent("CLEAR_EVENT", (object) new object[3]
          {
            (object) (int) table.clearEventID,
            (object) delivery_id,
            (object) recv_reward
          });
          if (this.isInGameScene)
          {
            is_success = false;
            List<int> intList = new List<int>((IEnumerable<int>) MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame);
            for (int index2 = 0; index2 < intList.Count; ++index2)
            {
              int num = intList[index2];
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
            MonoBehaviourSingleton<InGameProgress>.I.FieldReadStory((int) table.clearEventID, true, requestEventData);
            MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame.Clear();
          }
        }
      }
      else
        this.changeToDeliveryClearEvent = false;
      GameSection.ResumeEvent(is_success);
    }));
  }

  public void OnQuery_SELECT_QUEST()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData < 0 || eventData >= this.questInfo.Length)
    {
      GameSection.StopEvent();
    }
    else
    {
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.questInfo[eventData].questData.tableData.questID);
      GameSection.SetEventData((object) this.questInfo[eventData]);
    }
  }

  private void OnQuery_QuestAcceptDeliveryItemComplete_YES() => this.BackHome();

  private void OnQuery_InGameQuestAcceptDeliveryItemComplete_YES() => this.BackHome();

  private void OnQuery_InGameQuestBackHome_YES() => this.BackHome();

  private void BackHome()
  {
    if (!this.isInGameScene || !MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
  }

  private void OnQuery_SORT() => GameSection.SetEventData((object) this.sortSettings.Clone());

  protected void _OnCloseDialogSort()
  {
    SortSettings eventData = (SortSettings) GameSection.GetEventData();
    if (eventData == null)
      return;
    this.sortSettings = eventData;
    this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_ORDER_QUEST);
    this.RefreshUI();
  }

  private void OnCloseDialog_QuestSort() => this._OnCloseDialogSort();

  protected virtual bool IsVisibleDelivery(Delivery delivery, DeliveryTable.DeliveryData tableData)
  {
    return !tableData.IsEvent();
  }

  protected virtual void GetDeliveryList()
  {
    this.deliveryInfo = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList();
    List<Delivery> deliveryList1 = new List<Delivery>();
    int index = 0;
    for (int length = this.deliveryInfo.Length; index < length; ++index)
    {
      Delivery delivery = this.deliveryInfo[index];
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) delivery.dId);
      if (deliveryTableData == null)
        Log.Warning("DeliveryTable Not Found : dId " + (object) delivery.dId);
      else if (this.IsVisibleDelivery(delivery, deliveryTableData))
      {
        if (Singleton<NPCTable>.I.GetNPCData((int) deliveryTableData.npcID) == null)
          Log.Error($"DeliveryTable NPC ID Found  : dId {(object) delivery.dId} : npcID {(object) deliveryTableData.npcID}");
        else
          deliveryList1.Add(this.deliveryInfo[index]);
      }
    }
    this.deliveryInfo = deliveryList1.ToArray();
    List<Delivery> deliveryList2 = new List<Delivery>();
    List<Delivery> deliveryList3 = new List<Delivery>();
    List<Delivery> deliveryList4 = new List<Delivery>();
    foreach (Delivery delivery in this.deliveryInfo)
    {
      switch ((DELIVERY_TYPE) delivery.type)
      {
        case DELIVERY_TYPE.DAILY:
          deliveryList3.Add(delivery);
          break;
        case DELIVERY_TYPE.WEEKLY:
          deliveryList4.Add(delivery);
          break;
        default:
          deliveryList2.Add(delivery);
          break;
      }
    }
    this.normalDeliveryInfo = deliveryList2.ToArray();
    this.dailyDeliveryInfo = deliveryList3.ToArray();
    this.weeklyDeliveryInfo = deliveryList4.ToArray();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    bool flag = false;
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_ORDER_QUEST);
      this.questItemAry = (QuestItemInfo[]) null;
    }
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_OVER) != (GameSection.NOTIFY_FLAG) 0)
    {
      flag = true;
      MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryUpdate((Action<bool>) (b =>
      {
        this.GetDeliveryList();
        this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_AREA);
        this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_DELIVERY);
        base.OnNotify(flags);
      }));
    }
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.GetDeliveryList();
      this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_AREA);
      this.SetDirty((Enum) QuestSpecialSelect.UI.GRD_DELIVERY);
    }
    if (flag)
      return;
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE | GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_OVER;
  }

  public override EventData CheckAutoEvent(string event_name, object event_data)
  {
    switch (event_name)
    {
      case "SELECT_ORDER":
        uint table_id1 = (uint) event_data;
        int index1 = Array.FindIndex<QuestSortData>(this.questSortData, (Predicate<QuestSortData>) (data => (long) (int) data.GetTableID() == (long) table_id1));
        return index1 != -1 ? new EventData(event_name, (object) index1) : new EventData(event_name, (object) -1);
      case "SELECT_QUEST":
        uint table_id2 = (uint) event_data;
        int index2 = Array.FindIndex<QuestInfoData>(this.questInfo, (Predicate<QuestInfoData>) (data => (long) (int) data.questData.tableData.questID == (long) table_id2));
        return index2 != -1 ? new EventData(event_name, (object) index2) : new EventData(event_name, (object) -1);
      case "SELECT_DELIVERY":
        uint table_id3 = (uint) event_data;
        int index3 = Array.FindIndex<Delivery>(this.deliveryInfo, (Predicate<Delivery>) (data => (long) data.dId == (long) table_id3));
        return index3 != -1 ? new EventData(event_name, (object) index3) : new EventData(event_name, (object) -1);
      default:
        return base.CheckAutoEvent(event_name, event_data);
    }
  }

  private void OnQuery_SELECT_NORMAL()
  {
    this.SelectTab(QuestSpecialSelect.UI.BTN_TAB_NORMAL);
    this.isDeliveryGridReset = true;
    this.RefreshUI();
  }

  private void OnQuery_SELECT_DAILY()
  {
    this.SelectTab(QuestSpecialSelect.UI.BTN_TAB_DAILY);
    this.isDeliveryGridReset = true;
    this.RefreshUI();
  }

  private void OnQuery_SELECT_WEEKLY()
  {
    this.SelectTab(QuestSpecialSelect.UI.BTN_TAB_WEEKLY);
    this.isDeliveryGridReset = true;
    this.RefreshUI();
  }

  private void SelectTab(QuestSpecialSelect.UI tab)
  {
    if (this.selectedTab != tab)
      this.RemoveNew(this.selectedTab);
    this.selectedTab = tab;
  }

  private void RemoveNew(QuestSpecialSelect.UI old_select)
  {
    switch (old_select)
    {
      case QuestSpecialSelect.UI.BTN_TAB_DAILY:
        this.SetNew(QuestSpecialSelect.UI.BTN_TAB_DAILY, false);
        GameSaveData.instance.recommendedDailyDeliveryCheck = 0;
        GameSaveData.Save();
        break;
      case QuestSpecialSelect.UI.BTN_TAB_WEEKLY:
        this.SetNew(QuestSpecialSelect.UI.BTN_TAB_WEEKLY, false);
        GameSaveData.instance.recommendedWeeklyDeliveryCheck = 0;
        GameSaveData.Save();
        break;
    }
  }

  private void OnQuery_SELECT_CLOSE_AREA()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.WORLD_MAP))
    {
      GameSection.ChangeEvent("WORLD_MAP_TUTORIAL");
    }
    else
    {
      this.releaseRegionId = (int) GameSection.GetEventData();
      GameSection.StayEvent();
      MonoBehaviourSingleton<WorldMapManager>.I.SendRegionCrystalNum(this.releaseRegionId, (Action<bool, string>) ((isSuccess, campainText) => GameSection.ResumeEvent((isSuccess ? 1 : 0) != 0, (object) new object[2]
      {
        (object) MonoBehaviourSingleton<WorldMapManager>.I.releaseCrystalNum.ToString(),
        (object) campainText
      })));
    }
  }

  private void OnQuery_QuestAcceptReleaseRegionDialog_YES()
  {
    if (this.releaseRegionId < 0)
      return;
    EventData[] auto_event_data = new EventData[2]
    {
      new EventData(GameSection.GetGoingHomeEvent(), (object) null),
      new EventData("QUEST", (object) null)
    };
    GameSection.StayEvent();
    MonoBehaviourSingleton<WorldMapManager>.I.SendRegionOpen(this.releaseRegionId, (Action<bool>) (isSuccess =>
    {
      GameSection.ResumeEvent(isSuccess);
      if (!isSuccess)
        return;
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(auto_event_data);
      MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard = this.releaseRegionId;
    }));
  }

  protected enum UI
  {
    TEX_NPCMODEL,
    OBJ_NPC_MESSAGE,
    LBL_NPC_MESSAGE,
    BTN_SEARCH,
    BTN_EVENT,
    BTN_SORT,
    LBL_SORT,
    TGL_ICON_ASC,
    SPR_FRAME,
    SPR_BG_BTN_CLOSE,
    BTN_INPUT_CLOSE,
    BTN_INPUT_CLOSE_BG,
    BG,
    TIELEBAR,
    FRAMEDOWN,
    FRAMEUP,
    LIST_BASE,
    DELIVERY_SCROLLBAR_OVER,
    DELIVERY_SCROLLBAR_BASE,
    OBJ_ORDER_ROOT,
    BTN_ORDER,
    SPR_ORDER_TEXT,
    SPR_ORDER_ICON,
    OBJ_DELIVERY_ROOT,
    BTN_DELIVERY,
    SPR_DELIVERY_TEXT,
    SPR_DELIVERY_ICON,
    OBJ_QUEST_ROOT,
    BTN_QUEST,
    SPR_QUEST_TEXT,
    SPR_QUEST_ICON,
    STR_ORDER_NON_LIST,
    GRD_ORDER_QUEST,
    SCR_ORDER_QUEST2,
    LBL_DELIVERY_NON_LIST,
    SCR_DELIVERY_QUEST,
    GRD_DELIVERY,
    BTN_TAB_NORMAL,
    BTN_TAB_DAILY,
    BTN_TAB_WEEKLY,
    SPR_NEW,
    SPR_TAB_NORMAL,
    SPR_TAB_DAILY,
    SPR_TAB_WEEKLY,
    GRD_QUEST,
    STR_QUEST_NON_LIST,
    GRD_ICON_ROOT,
    OBJ_ICON_ROOT_1,
    OBJ_ICON_ROOT_2,
    OBJ_BUTTON_ROOT,
    SCR_ORDER_QUEST,
    SPR_ORDER_RARITY_FRAME,
    LBL_ORDER_NUM,
    LBL_REMAIN,
    TEX_NPC,
    LBL_DELIVERY_COMMENT,
    LBL_NEED_ITEM_NAME,
    LBL_HAVE,
    LBL_NEED,
    LBL_LIMIT,
    OBJ_REQUEST_OK,
    OBJ_REQUEST_COMPLETED,
    SPR_TYPE_STORY,
    SPR_TYPE_EVENT,
    SPR_TYPE_EVENT_TEXT,
    SPR_TYPE_DAILY_TEXT,
    SPR_TYPE_WEEKLY_TEXT,
    SPR_TYPE_HARD,
    SPR_TYPE_NORMAL,
    SPR_TYPE_SUB_EVENT,
    SPR_DIFFICULTY_EASY,
    SPR_DIFFICULTY_NORMAL,
    SPR_DIFFICULTY_HARD,
    SPR_DROP_DIFFICULTY_RARE,
    SPR_DROP_DIFFICULTY_SUPER_RARE,
    SCR_NORMAL_QUEST,
    OBJ_ROT_SPRITE,
    LBL_QUEST_NAME,
    LBL_QUEST_NUM,
    OBJ_MISSION_INFO_ROOT,
    OBJ_TOP_CROWN_1,
    OBJ_TOP_CROWN_2,
    OBJ_TOP_CROWN_3,
    SPR_CROWN_1,
    SPR_CROWN_2,
    SPR_CROWN_3,
    OBJ_ENEMY,
    SPR_MONSTER_ICON,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    TWN_DIFFICULT_STAR,
    OBJ_DIFFICULT_STAR_1,
    OBJ_DIFFICULT_STAR_2,
    OBJ_DIFFICULT_STAR_3,
    OBJ_DIFFICULT_STAR_4,
    OBJ_DIFFICULT_STAR_5,
    OBJ_DIFFICULT_STAR_6,
    OBJ_DIFFICULT_STAR_7,
    OBJ_DIFFICULT_STAR_8,
    OBJ_DIFFICULT_STAR_9,
    OBJ_DIFFICULT_STAR_10,
    OBJ_ICON,
    OBJ_ICON_NEW,
    OBJ_ICON_CLEARED,
    OBJ_ICON_COMPLETE,
    SPR_ICON_NEW,
    SPR_ICON_CLEARED,
    SPR_ICON_COMPLETE,
    OBJ_BANNER_ROOT,
    SPR_BANNER,
    PORTRAIT_SPR_BG_BTN_CLOSE,
    PORTRAIT_BTN_INPUT_CLOSE,
    PORTRAIT_BTN_INPUT_CLOSE_BG,
    PORTRAIT_OBJ_BUTTON_ROOT,
    PORTRAIT_BG,
    PORTRAIT_TIELEBAR,
    PORTRAIT_FRAMEDOWN,
    PORTRAIT_FRAMEUP,
    PORTRAIT_OBJ_DELIVERY_ROOT,
    PORTRAIT_LIST_BASE,
    PORTRAIT_STR_DELIVERY_NON_LIST,
    PORTRAIT_SCR_DELIVERY_QUEST,
    PORTRAIT_DELIVERY_SCROLLBAR,
    LANDSCAPE_SPR_BG_BTN_CLOSE,
    LANDSCAPE_BTN_INPUT_CLOSE,
    LANDSCAPE_BTN_INPUT_CLOSE_BG,
    LANDSCAPE_OBJ_BUTTON_ROOT,
    LANDSCAPE_BG,
    LANDSCAPE_TIELEBAR,
    LANDSCAPE_FRAMEDOWN,
    LANDSCAPE_FRAMEUP,
    LANDSCAPE_OBJ_DELIVERY_ROOT,
    LANDSCAPE_LIST_BASE,
    LANDSCAPE_STR_DELIVERY_NON_LIST,
    LANDSCAPE_SCR_DELIVERY_QUEST,
    LANDSCAPE_DELIVERY_SCROLLBAR,
    TEX_AREA_BANNER,
    SPR_CLEARED_AREA,
    SPR_NEW_AREA,
    GRD_AREA,
    OBJ_DELIVERY_BAR,
    OBJ_AREA_BAR,
    LBL_DELIVERY_NUM,
    LBL_DELIVERY_REST,
    SCR_AREA,
  }

  public enum SHOW_MODE
  {
    DELIVERY,
    ORDER,
    QUEST,
  }

  private struct AreaQuestInfo(RegionTable.Data data, Texture2D tex, bool cleared, bool opened)
  {
    public RegionTable.Data regionData = data;
    public Texture2D bannerTex = tex;
    public bool cleared = cleared;
    public bool opened = opened;
  }
}
