// Decompiled with JetBrains decompiler
// Type: QuestOrderSelect
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
public class QuestOrderSelect : GameSection
{
  private SortSettings sortSettings;
  private QuestItemInfo[] questItemAry;
  protected QuestOrderSelect.QuestGridData[] questGridDatas;
  private QuestOrderSelect.UI[] difficult = new QuestOrderSelect.UI[10]
  {
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_1,
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_2,
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_3,
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_4,
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_5,
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_6,
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_7,
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_8,
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_9,
    QuestOrderSelect.UI.OBJ_DIFFICULT_STAR_10
  };
  private string npcText;
  protected bool isScrollViewReady;
  private System.Action onScrollViewReady;
  private bool isTransitionFinished;
  private System.Action onOpen;
  private bool isQuestItemDirty;
  private float remainingTime;
  private float SHOW_QUEST_REMAIN_LIMIT_SECOND;
  private bool isResetUI;
  protected int nowPage = 1;
  private int pageMax = 1;
  protected QuestSearchRoomCondition.SearchRequestParam param = new QuestSearchRoomCondition.SearchRequestParam();

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    bool is_recv_quest = false;
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
    if (MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList)
    {
      MonoBehaviourSingleton<QuestManager>.I.SendGetQuestList((Action<bool>) (b =>
      {
        MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList = false;
        is_recv_quest = true;
      }));
      while (!is_recv_quest)
        yield return (object) null;
    }
    this.StartCoroutine(this.CheckLimitQuestItem());
    this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.QUEST, SortSettings.SETTINGS_TYPE.ORDER_QUEST);
    this.SHOW_QUEST_REMAIN_LIMIT_SECOND = (float) TimeSpan.FromDays(5.0).TotalSeconds;
    this.sortSettings.indivComparison = new Comparison<SortCompareData>(this.Compare);
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    base.Initialize();
  }

  protected override void OnOpen()
  {
    GameSaveData.instance.recommendedOrderCheck = 0;
    GameSaveData.Save();
    base.OnOpen();
  }

  public override void UpdateUI()
  {
    this.npcText = Singleton<NPCMessageTable>.I.GetNPCMessageBySectionData(this.sectionData);
    this.SetRenderNPCModel((Enum) QuestOrderSelect.UI.TEX_NPCMODEL, 2, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCFOV);
    this.SetLabelText((Enum) QuestOrderSelect.UI.LBL_NPC_MESSAGE, this.npcText);
    if (MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.challengeInfo != null && MonoBehaviourSingleton<PartyManager>.I.challengeInfo.currentShadowCount != null)
      this.SetActive((Enum) QuestOrderSelect.UI.BTN_SHADOW_COUNT, true);
    else
      this.SetActive((Enum) QuestOrderSelect.UI.BTN_SHADOW_COUNT, false);
    this.ShowOrder();
    this.isResetUI = false;
  }

  protected void ShowOrder()
  {
    if (MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() > 0)
    {
      List<QuestItemInfo> list = new List<QuestItemInfo>();
      MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory((Action<QuestItemInfo>) (item =>
      {
        if (!this.IsSetQuestItemInfoByOrderQuest(item))
          return;
        if (this.isQuestItemDirty)
        {
          float remainTime;
          int expiredNum;
          this.GetRemainingTimeAndExpiredNum(item, out remainTime, out expiredNum);
          if ((double) remainTime <= 0.0)
            return;
          item.infoData.questData.num -= expiredNum;
          if (item.infoData.questData.num <= 0)
            return;
          list.Add(item);
        }
        else
        {
          if (item.infoData.questData.num <= 0)
            return;
          list.Add(item);
        }
      }));
      this.Search(ref list);
      this.questItemAry = list.ToArray();
    }
    List<QuestOrderSelect.QuestGridData> questGridDataList = new List<QuestOrderSelect.QuestGridData>();
    if (MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsEnable())
      questGridDataList.Add(new QuestOrderSelect.QuestGridData(QuestOrderSelect.QuestGridData.ORDER_QUEST_TYPE.Challenge));
    if (this.questItemAry != null && this.questItemAry.Length != 0)
    {
      QuestSortData[] sortAry = this.sortSettings.CreateSortAry<QuestItemInfo, QuestSortData>(this.questItemAry);
      if (sortAry != null)
      {
        int index = 0;
        for (int length = sortAry.Length; index < length; ++index)
        {
          QuestInfoData info = sortAry[index].itemData.infoData;
          int num1 = info.questData.num;
          int num2 = 0;
          if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
            num2 = MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Where<GuildRequestItem>((Func<GuildRequestItem, bool>) (g => g.questId == (int) info.questData.tableData.questID)).Count<GuildRequestItem>();
          int num3 = num2;
          if (num1 - num3 > 0)
            questGridDataList.Add(new QuestOrderSelect.QuestGridData(data: sortAry[index]));
        }
      }
    }
    this.questGridDatas = questGridDataList.ToArray();
    if (this.questGridDatas == null || this.questGridDatas.Length == 0)
    {
      this.SetActive((Enum) QuestOrderSelect.UI.GRD_ORDER_QUEST, false);
      this.SetActive((Enum) QuestOrderSelect.UI.STR_ORDER_NON_LIST, true);
      this.SetActive((Enum) QuestOrderSelect.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) QuestOrderSelect.UI.OBJ_INACTIVE_ROOT, true);
      this.SetLabelText((Enum) QuestOrderSelect.UI.LBL_MAX, "0");
      this.SetLabelText((Enum) QuestOrderSelect.UI.LBL_NOW, "0");
      UIScrollView component = ((Component) this.GetCtrl((Enum) QuestOrderSelect.UI.SCR_ORDER_QUEST)).GetComponent<UIScrollView>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Behaviour) component).enabled = false;
      component.verticalScrollBar.alpha = 0.0f;
    }
    else
    {
      if (this.questGridDatas.Length == 1 && this.questGridDatas[0].orderQuestType == QuestOrderSelect.QuestGridData.ORDER_QUEST_TYPE.Challenge)
        this.SetActive((Enum) QuestOrderSelect.UI.STR_ORDER_NON_LIST, true);
      else
        this.SetActive((Enum) QuestOrderSelect.UI.STR_ORDER_NON_LIST, false);
      this.SetActive((Enum) QuestOrderSelect.UI.GRD_ORDER_QUEST, true);
      this.SetLabelText((Enum) QuestOrderSelect.UI.LBL_SORT, this.sortSettings.GetSortLabel());
      this.SetToggle((Enum) QuestOrderSelect.UI.TGL_ICON_ASC, this.sortSettings.orderTypeAsc);
      this.pageMax = 1 + (this.questGridDatas.Length - 1) / 10;
      bool is_visible = this.pageMax > 1;
      this.SetActive((Enum) QuestOrderSelect.UI.OBJ_ACTIVE_ROOT, is_visible);
      this.SetActive((Enum) QuestOrderSelect.UI.OBJ_INACTIVE_ROOT, !is_visible);
      this.SetLabelText((Enum) QuestOrderSelect.UI.LBL_MAX, this.pageMax.ToString());
      this.SetLabelText((Enum) QuestOrderSelect.UI.LBL_NOW, this.nowPage.ToString());
      UITweener[] transitions = ((Component) this.GetCtrl((Enum) QuestOrderSelect.UI.OBJ_FRAME)).GetComponents<UITweener>();
      int finishCount = 0;
      foreach (UITweener uiTweener in transitions)
        uiTweener.AddOnFinished((EventDelegate.Callback) (() =>
        {
          ++finishCount;
          if (finishCount < transitions.Length)
            return;
          this.isTransitionFinished = true;
        }));
      int sourceIndex = 10 * (this.nowPage - 1);
      int length = this.nowPage == this.pageMax ? this.questGridDatas.Length - sourceIndex : 10;
      QuestOrderSelect.QuestGridData[] destinationArray = new QuestOrderSelect.QuestGridData[length];
      Array.Copy((Array) this.questGridDatas, sourceIndex, (Array) destinationArray, 0, length);
      this.questGridDatas = destinationArray;
      this.SetGrid((Enum) QuestOrderSelect.UI.GRD_ORDER_QUEST, "", 0, true, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) null);
      this.SetGrid((Enum) QuestOrderSelect.UI.GRD_ORDER_QUEST, "QuestListOrderItem", this.questGridDatas.Length, true, new Func<int, Transform, Transform>(this.CreateGridListItem), new Action<int, Transform, bool>(this.UpdateGridListItem));
    }
  }

  private Transform CreateGridListItem(int index, Transform t)
  {
    if (index < 0 || this.questGridDatas.Length <= index)
      return (Transform) null;
    string prefab_name = "QuestListOrderItem";
    if (this.questGridDatas[index].orderQuestType == QuestOrderSelect.QuestGridData.ORDER_QUEST_TYPE.Challenge)
      prefab_name = "QuestListChallengeGotoItem";
    return this.Realizes(prefab_name, t);
  }

  private void UpdateGridListItem(int i, Transform t, bool is_recycle)
  {
    if (i < 0 || this.questGridDatas.Length <= i)
      return;
    switch (this.questGridDatas[i].orderQuestType)
    {
      case QuestOrderSelect.QuestGridData.ORDER_QUEST_TYPE.Quest:
        this.UpdateGirdListItemQuest(i, t, is_recycle);
        break;
      case QuestOrderSelect.QuestGridData.ORDER_QUEST_TYPE.Challenge:
        this.UpdateGridListItemChallenge(i, t, is_recycle);
        break;
    }
  }

  private void UpdateGirdListItemQuest(int i, Transform t, bool is_recycle)
  {
    int num1 = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Any<GameSectionHistory.HistoryData>((Func<GameSectionHistory.HistoryData, bool>) (h => h.sectionName.StartsWith("GuildRequest"))) ? 1 : 0;
    this.SetActive(t, true);
    this.SetEvent(t, "SELECT_ORDER", i);
    QuestSortData questSortData1 = this.questGridDatas[i].questSortData;
    UIScrollView component = ((Component) this.GetCtrl((Enum) QuestOrderSelect.UI.SCR_ORDER_QUEST)).GetComponent<UIScrollView>();
    QuestInfoData info = questSortData1.itemData.infoData;
    if (num1 != 0)
    {
      this.SetActive(t, (Enum) QuestOrderSelect.UI.TWN_DIFFICULT_STAR, false);
      this.SetActive(t, (Enum) QuestOrderSelect.UI.TXT_NEED_POINT, true);
      string text = string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 6U), (object) MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(info.questData.tableData.rarity), (object) MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(info.questData.tableData.rarity));
      this.SetLabelText(t, (Enum) QuestOrderSelect.UI.TXT_NEED_POINT, text);
    }
    else
    {
      this.SetActive(t, (Enum) QuestOrderSelect.UI.TWN_DIFFICULT_STAR, false);
      this.SetActive(t, (Enum) QuestOrderSelect.UI.TXT_NEED_POINT, false);
    }
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) info.questData.tableData.GetMainEnemyID());
    QuestSortData questSortData2 = questSortData1;
    ItemIcon icon = ItemIcon.Create(questSortData2.GetIconType(), questSortData2.GetIconID(), new RARITY_TYPE?(questSortData2.GetRarity()), this.FindCtrl(t, (Enum) QuestOrderSelect.UI.OBJ_ENEMY), questSortData2.GetIconElement());
    icon.SetEnableCollider(false);
    this.SetActive(t, (Enum) QuestOrderSelect.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
    this.SetElementSprite(t, (Enum) QuestOrderSelect.UI.SPR_ELEMENT, (int) enemyData.element);
    this.SetElementSprite(t, (Enum) QuestOrderSelect.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
    this.SetActive(t, (Enum) QuestOrderSelect.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
    this.SetLabelText(t, (Enum) QuestOrderSelect.UI.LBL_QUEST_NAME, info.questData.tableData.questText);
    int num2 = 1;
    ClearStatusQuestEnemySpecies questEnemySpecies = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestEnemySpecies(info.questData.tableData.questID);
    if (questEnemySpecies != null)
      num2 = questEnemySpecies.questStatus;
    int num3 = i + 100;
    this.SetToggleGroup(t, (Enum) QuestOrderSelect.UI.OBJ_ICON_NEW, num3);
    this.SetToggleGroup(t, (Enum) QuestOrderSelect.UI.OBJ_ICON_CLEARED, num3);
    this.SetToggleGroup(t, (Enum) QuestOrderSelect.UI.OBJ_ICON_COMPLETE, num3);
    if (num2 != 1)
    {
      this.SetToggle(t, (Enum) QuestOrderSelect.UI.OBJ_ICON_NEW, false);
      this.SetToggle(t, (Enum) QuestOrderSelect.UI.OBJ_ICON_CLEARED, false);
      this.SetToggle(t, (Enum) QuestOrderSelect.UI.OBJ_ICON_COMPLETE, false);
      this.SetActive(t, (Enum) QuestOrderSelect.UI.OBJ_ICON_ROOT, false);
      this.SetVisibleWidgetEffect((Enum) QuestOrderSelect.UI.SCR_ORDER_QUEST, t, (Enum) QuestOrderSelect.UI.SPR_ICON_NEW, (string) null);
      this.SetVisibleWidgetEffect((Enum) QuestOrderSelect.UI.SCR_ORDER_QUEST, t, (Enum) QuestOrderSelect.UI.SPR_ICON_COMPLETE, (string) null);
    }
    else
    {
      this.SetActive(t, (Enum) QuestOrderSelect.UI.OBJ_ICON_ROOT, true);
      this.SetToggle(t, (Enum) QuestOrderSelect.UI.OBJ_ICON_NEW, true);
      this.SetVisibleWidgetEffect((Enum) QuestOrderSelect.UI.SCR_ORDER_QUEST, t, (Enum) QuestOrderSelect.UI.SPR_ICON_COMPLETE, (string) null);
      this.SetVisibleWidgetEffect((Enum) QuestOrderSelect.UI.SCR_ORDER_QUEST, t, (Enum) QuestOrderSelect.UI.SPR_ICON_NEW, "ef_ui_questselect_new");
    }
    int num4 = info.questData.num;
    int num5 = 0;
    if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
      num5 = MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Where<GuildRequestItem>((Func<GuildRequestItem, bool>) (g => g.questId == (int) info.questData.tableData.questID)).Count<GuildRequestItem>();
    int num6 = num5;
    int num7 = num4 - num6;
    this.SetLabelText(t, (Enum) QuestOrderSelect.UI.LBL_ORDER_NUM, num7.ToString());
    if (num7 <= 0)
      ((Component) t).GetComponent<UIButton>().isEnabled = false;
    Transform ctrl = this.FindCtrl(t, (Enum) QuestOrderSelect.UI.OBJ_FRAME);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      UIPanel uiPanel = ((Component) ctrl).gameObject.GetComponent<UIPanel>();
      if (Object.op_Equality((Object) uiPanel, (Object) null))
      {
        uiPanel = ((Component) ctrl).gameObject.AddComponent<UIPanel>();
        uiPanel.depth = component.panel.depth + 1;
      }
      uiPanel.widgetsAreStatic = false;
      if (this.isScrollViewReady)
        this.PanelToStatic(icon, uiPanel);
      else
        this.onScrollViewReady += (System.Action) (() => this.PanelToStatic(icon, uiPanel));
    }
    QuestItemInfo itemData = questSortData1.itemData;
    bool is_visible = false;
    foreach (double remainTime in itemData.remainTimes)
    {
      if (remainTime < (double) this.SHOW_QUEST_REMAIN_LIMIT_SECOND)
      {
        is_visible = true;
        break;
      }
    }
    this.SetLabelText(t, (Enum) QuestOrderSelect.UI.LBL_REMAIN, StringTable.Get(STRING_CATEGORY.GATE_QUEST_NAME, 1U));
    this.SetActive(t, (Enum) QuestOrderSelect.UI.LBL_REMAIN, is_visible);
  }

  private void UpdateGridListItemChallenge(int i, Transform t, bool is_recycle)
  {
    this.SetActive(t, true);
    this.SetEvent(t, "SELECT_CHALLENGE", i);
    this.SetActive(t, (Enum) QuestOrderSelect.UI.OBJ_CHALLENGE_ON, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy());
    this.SetActive(t, (Enum) QuestOrderSelect.UI.OBJ_CHALLENGE_OFF, !MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy());
    this.SetLabelText(t, (Enum) QuestOrderSelect.UI.LBL_CHALLENGE_ON_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
    this.SetLabelText(t, (Enum) QuestOrderSelect.UI.LBL_CHALLENGE_OFF_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
    this.SetSupportEncoding((Enum) QuestOrderSelect.UI.LBL_CHALLENGE_ON_MESSAGE, true);
    this.SetSupportEncoding((Enum) QuestOrderSelect.UI.LBL_CHALLENGE_OFF_MESSAGE, true);
  }

  private void TryScrollViewToReady()
  {
    if (!this.isOpen || !this.isTransitionFinished || this.onScrollViewReady == null)
      return;
    this.isScrollViewReady = true;
    this.onScrollViewReady();
    this.onScrollViewReady = (System.Action) null;
  }

  private void PanelToStatic(ItemIcon icon, UIPanel uiPanel)
  {
    if (icon.isIconLoaded)
    {
      uiPanel.widgetsAreStatic = false;
      MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => uiPanel.widgetsAreStatic = true);
    }
    else
      icon.onIconLoaded = (System.Action) (() =>
      {
        uiPanel.widgetsAreStatic = false;
        MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => uiPanel.widgetsAreStatic = true);
      });
  }

  public virtual void OnQuery_SELECT_ORDER()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData < 0 || eventData >= this.questGridDatas.Length)
      GameSection.StopEvent();
    else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(this.questGridDatas[eventData].questSortData.GetTableID()))
    {
      GameSection.StopEvent();
    }
    else
    {
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.questGridDatas[eventData].questSortData.GetTableID());
      GameSection.SetEventData((object) this.questGridDatas[eventData].questSortData.itemData.infoData);
      this.isScrollViewReady = false;
    }
  }

  public void OnQuery_SELECT_ORDER_FROM_ITEM_DETAIL()
  {
    uint eventData = (uint) GameSection.GetEventData();
    if (eventData <= 0U)
      GameSection.StopEvent();
    else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(eventData))
    {
      GameSection.StopEvent();
    }
    else
    {
      QuestInfoData questInfoData = MonoBehaviourSingleton<QuestManager>.I.GetQuestInfoData(eventData);
      if (questInfoData == null)
      {
        GameSection.StopEvent();
      }
      else
      {
        MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(eventData);
        GameSection.SetEventData((object) questInfoData);
        this.isScrollViewReady = false;
      }
    }
  }

  private void OnQuery_SORT() => GameSection.SetEventData((object) this.sortSettings.Clone());

  protected void _OnCloseDialogSort()
  {
    SortSettings eventData = (SortSettings) GameSection.GetEventData();
    if (eventData == null)
      return;
    this.sortSettings = eventData;
    this.SetDirty((Enum) QuestOrderSelect.UI.GRD_ORDER_QUEST);
    this.RefreshUI();
  }

  private void OnCloseDialog_QuestSort() => this._OnCloseDialogSort();

  private void OnQuery_CAUTION()
  {
    GameSection.SetEventData((object) WebViewManager.GachaQuestList);
  }

  public virtual void OnQuery_SELECT_CHALLENGE()
  {
    if (!MonoBehaviourSingleton<PartyManager>.IsValid())
      return;
    if (!MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy())
    {
      GameSection.ChangeEvent("NO_SATISFY");
    }
    else
    {
      if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.num != 0)
        return;
      GameSection.ChangeEvent("NUM_ZERO");
    }
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.SetDirty((Enum) QuestOrderSelect.UI.GRD_ORDER_QUEST);
      this.questItemAry = (QuestItemInfo[]) null;
    }
    if (false)
      return;
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY;
  }

  public override EventData CheckAutoEvent(string event_name, object event_data)
  {
    if (!(event_name == "SELECT_ORDER"))
      return base.CheckAutoEvent(event_name, event_data);
    uint table_id = (uint) event_data;
    int index = Array.FindIndex<QuestOrderSelect.QuestGridData>(this.questGridDatas, (Predicate<QuestOrderSelect.QuestGridData>) (data => data.questSortData != null && (long) (int) data.questSortData.GetTableID() == (long) table_id));
    return index != -1 ? new EventData(event_name, (object) index) : new EventData(event_name, (object) -1);
  }

  private void Update()
  {
    if (!this.isScrollViewReady)
      this.TryScrollViewToReady();
    if (!this.isQuestItemDirty)
      return;
    this.RefreshUI();
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<QuestManager>.I.SendGetQuestList((Action<bool>) (b => this.StartCoroutine(this.CheckLimitQuestItem())))));
    this.isQuestItemDirty = false;
  }

  private bool IsSetQuestItemInfoByOrderQuest(QuestItemInfo questItemInfo)
  {
    bool isMatch = false;
    List<QuestData> orderQuestList = MonoBehaviourSingleton<QuestManager>.I.orderQuestList;
    if (orderQuestList == null || orderQuestList.Count <= 0)
      return false;
    orderQuestList.ForEach((Action<QuestData>) (orderQuest =>
    {
      if ((long) orderQuest.questId != (long) questItemInfo.infoData.questData.tableData.questID)
        return;
      isMatch = true;
      questItemInfo.remainTimes = orderQuest.remainTimes;
      questItemInfo.infoData.questData.num = orderQuest.order.num;
    }));
    return isMatch;
  }

  private void GetRemainingTimeAndExpiredNum(
    QuestItemInfo questItemInfo,
    out float remainTime,
    out int expiredNum)
  {
    remainTime = 0.0f;
    expiredNum = 0;
    for (int index = 0; index < questItemInfo.remainTimes.Count; ++index)
    {
      float num = questItemInfo.remainTimes[index] - this.remainingTime;
      if ((double) remainTime < (double) num)
        remainTime = num;
      if ((double) num <= 0.0)
        ++expiredNum;
    }
  }

  private IEnumerator CheckLimitQuestItem()
  {
    if (!this.isQuestItemDirty)
    {
      List<QuestData> orderQuestList = MonoBehaviourSingleton<QuestManager>.I.orderQuestList;
      if (orderQuestList != null && orderQuestList.Count > 0)
      {
        float minRemainingSec = float.MaxValue;
        QuestData questData = (QuestData) null;
        float parseRemainingSec;
        orderQuestList.ForEach((Action<QuestData>) (orderQuest =>
        {
          for (int index = 0; index < orderQuest.remainTimes.Count; ++index)
          {
            parseRemainingSec = orderQuest.remainTimes[index];
            if ((double) parseRemainingSec > 0.0 && (double) minRemainingSec > (double) parseRemainingSec)
            {
              minRemainingSec = parseRemainingSec;
              questData = orderQuest;
            }
          }
        }));
        this.remainingTime = minRemainingSec;
        yield return (object) new WaitForSeconds(minRemainingSec);
        if (questData != null)
          this.isQuestItemDirty = true;
      }
    }
  }

  private void OnQuery_PAGE_PREV()
  {
    this.isResetUI = true;
    this.nowPage = this.nowPage > 1 ? this.nowPage - 1 : this.pageMax;
    this.ShowOrder();
  }

  private void OnQuery_PAGE_NEXT()
  {
    this.isResetUI = true;
    this.nowPage = this.nowPage < this.pageMax ? this.nowPage + 1 : 1;
    this.ShowOrder();
  }

  private void OnQuery_CONDITION() => GameSection.SetEventData((object) this.param);

  private void Search(ref List<QuestItemInfo> list)
  {
    if (this.param == null || this.param.order == 0)
      return;
    list = list.Where<QuestItemInfo>((Func<QuestItemInfo, bool>) (item => this.param.IsMatchRarity(item))).Where<QuestItemInfo>((Func<QuestItemInfo, bool>) (item => this.param.IsMatchLevel(item))).Where<QuestItemInfo>((Func<QuestItemInfo, bool>) (item => this.param.IsMatchElement(item))).Where<QuestItemInfo>((Func<QuestItemInfo, bool>) (item => this.param.IsMatchEnemySpecies(item))).ToList<QuestItemInfo>();
  }

  private int Compare(SortCompareData lp, SortCompareData rp)
  {
    QuestSortData questSortData1 = lp as QuestSortData;
    QuestSortData questSortData2 = rp as QuestSortData;
    if (questSortData1 == null || questSortData2 == null)
      return 0;
    float num1 = questSortData1.itemData.remainTimes.Min();
    double num2 = (double) questSortData2.itemData.remainTimes.Min();
    bool flag1 = (double) num1 < (double) this.SHOW_QUEST_REMAIN_LIMIT_SECOND;
    double remainLimitSecond = (double) this.SHOW_QUEST_REMAIN_LIMIT_SECOND;
    bool flag2 = num2 < remainLimitSecond;
    if (lp.IsAbsFirst() != rp.IsAbsFirst())
      return !lp.IsAbsFirst() ? 1 : -1;
    if (flag1 != flag2)
      return !flag1 ? 1 : -1;
    int num3 = questSortData2.GetRarity() - questSortData1.GetRarity();
    return num3 == 0 ? (int) ((long) questSortData2.GetUniqID() - (long) questSortData1.GetUniqID()) : num3;
  }

  protected enum UI
  {
    TEX_NPCMODEL,
    OBJ_NPC_MESSAGE,
    LBL_NPC_MESSAGE,
    BTN_SEARCH,
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
    STR_ORDER_NON_LIST,
    GRD_ORDER_QUEST,
    SCR_ORDER_QUEST2,
    OBJ_ICON_ROOT,
    OBJ_BUTTON_ROOT,
    SCR_ORDER_QUEST,
    SPR_ORDER_RARITY_FRAME,
    LBL_ORDER_NUM,
    OBJ_FRAME,
    LBL_QUEST_NAME,
    LBL_QUEST_NUM,
    LBL_REMAIN,
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
    TXT_NEED_POINT,
    OBJ_ICON,
    OBJ_ICON_NEW,
    OBJ_ICON_CLEARED,
    OBJ_ICON_COMPLETE,
    SPR_ICON_NEW,
    SPR_ICON_CLEARED,
    SPR_ICON_COMPLETE,
    OBJ_BANNER_ROOT,
    SPR_BANNER,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    LBL_MAX,
    LBL_NOW,
    OBJ_CHALLENGE_ON,
    OBJ_CHALLENGE_OFF,
    LBL_CHALLENGE_ON_MESSAGE,
    LBL_CHALLENGE_OFF_MESSAGE,
    BTN_SHADOW_COUNT,
  }

  public class QuestGridData
  {
    public QuestOrderSelect.QuestGridData.ORDER_QUEST_TYPE orderQuestType;
    public QuestSortData questSortData;

    public QuestGridData(
      QuestOrderSelect.QuestGridData.ORDER_QUEST_TYPE type = QuestOrderSelect.QuestGridData.ORDER_QUEST_TYPE.Quest,
      QuestSortData data = null)
    {
      this.orderQuestType = type;
      this.questSortData = data;
    }

    public enum ORDER_QUEST_TYPE
    {
      Quest,
      Challenge,
    }
  }
}
