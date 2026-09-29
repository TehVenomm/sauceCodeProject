// Decompiled with JetBrains decompiler
// Type: QuestChallengeSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestChallengeSelect : GameSection
{
  protected QuestData[] challengeData;
  private QuestChallengeSelect.UI[] difficult = new QuestChallengeSelect.UI[10]
  {
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_1,
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_2,
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_3,
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_4,
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_5,
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_6,
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_7,
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_8,
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_9,
    QuestChallengeSelect.UI.OBJ_DIFFICULT_STAR_10
  };
  protected bool isScrollViewReady;
  private System.Action onScrollViewReady;
  private bool isTransitionFinished;
  private System.Action onOpen;
  private bool isQuestItemDirty;
  private bool isResetUI;
  private int nowPage = 1;
  private int pageMax = 1;
  private QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam param = new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam();

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    bool is_recv_quest = false;
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
    this.InitSearchParam();
    MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeList(new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam()
    {
      enemyLevel = this.GetEnemyLevelFromUserLevel()
    }, (Action<bool, Error>) ((is_success, err) => is_recv_quest = true), false);
    while (!is_recv_quest)
      yield return (object) null;
    this.StartCoroutine(this.CheckLimitQuestItem());
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    base.Initialize();
  }

  protected override void OnOpen()
  {
    GameSaveData.instance.recommendedChallengeCheck = 0;
    GameSaveData.Save();
    base.OnOpen();
  }

  public override void UpdateUI()
  {
    this.ShowChallenge();
    this.isResetUI = false;
  }

  protected void ShowChallenge()
  {
    List<QuestData> challengeList = MonoBehaviourSingleton<QuestManager>.I.challengeList;
    if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount != null)
    {
      this.SetActive((Enum) QuestChallengeSelect.UI.STR_CHALLENGE_BONUS_MESSAGE, true);
      this.SetActive((Enum) QuestChallengeSelect.UI.BTN_DETAIL, true);
      ((Component) this.GetCtrl((Enum) QuestChallengeSelect.UI.SCR_ORDER_QUEST)).GetComponent<UIPanel>().baseClipRegion = new Vector4(0.0f, -110f, 440f, 549f);
      this.SetLabelText((Enum) QuestChallengeSelect.UI.STR_CHALLENGE_BONUS_MESSAGE, StringTable.Format(STRING_CATEGORY.SHADOW_COUNT, 3U, (object) MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount.num));
      this.GetComponent<UILabel>((Enum) QuestChallengeSelect.UI.STR_CHALLENGE_BONUS_MESSAGE).supportEncoding = true;
    }
    else
    {
      this.SetActive((Enum) QuestChallengeSelect.UI.STR_CHALLENGE_BONUS_MESSAGE, false);
      this.SetActive((Enum) QuestChallengeSelect.UI.BTN_DETAIL, false);
    }
    this.SetLabelText((Enum) QuestChallengeSelect.UI.STR_CHALLENGE_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
    this.SetSupportEncoding((Enum) QuestChallengeSelect.UI.STR_CHALLENGE_MESSAGE, true);
    if (challengeList == null || challengeList.Count == 0)
    {
      this.SetActive((Enum) QuestChallengeSelect.UI.GRD_ORDER_QUEST, false);
      this.SetActive((Enum) QuestChallengeSelect.UI.STR_ORDER_NON_LIST, true);
      this.SetActive((Enum) QuestChallengeSelect.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) QuestChallengeSelect.UI.OBJ_INACTIVE_ROOT, true);
      this.SetLabelText((Enum) QuestChallengeSelect.UI.LBL_MAX, "0");
      this.SetLabelText((Enum) QuestChallengeSelect.UI.LBL_NOW, "0");
      UIScrollView component = ((Component) this.GetCtrl((Enum) QuestChallengeSelect.UI.SCR_ORDER_QUEST)).GetComponent<UIScrollView>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Behaviour) component).enabled = false;
      component.verticalScrollBar.alpha = 0.0f;
    }
    else
    {
      this.SetActive((Enum) QuestChallengeSelect.UI.GRD_ORDER_QUEST, true);
      this.SetActive((Enum) QuestChallengeSelect.UI.STR_ORDER_NON_LIST, false);
      this.pageMax = 1 + (challengeList.Count - 1) / 10;
      bool is_visible = this.pageMax > 1;
      this.SetActive((Enum) QuestChallengeSelect.UI.OBJ_ACTIVE_ROOT, is_visible);
      this.SetActive((Enum) QuestChallengeSelect.UI.OBJ_INACTIVE_ROOT, !is_visible);
      this.SetLabelText((Enum) QuestChallengeSelect.UI.LBL_MAX, this.pageMax.ToString());
      this.SetLabelText((Enum) QuestChallengeSelect.UI.LBL_NOW, this.nowPage.ToString());
      UITweener[] transitions = ((Component) this.GetCtrl((Enum) QuestChallengeSelect.UI.OBJ_FRAME)).GetComponents<UITweener>();
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
      int length = this.nowPage == this.pageMax ? challengeList.Count - sourceIndex : 10;
      this.challengeData = new QuestData[length];
      Array.Copy((Array) challengeList.ToArray(), sourceIndex, (Array) this.challengeData, 0, length);
      bool isGuildRequest = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("GuildRequest");
      UIScrollView scrollView = ((Component) this.GetCtrl((Enum) QuestChallengeSelect.UI.SCR_ORDER_QUEST)).GetComponent<UIScrollView>();
      this.SetGrid((Enum) QuestChallengeSelect.UI.GRD_ORDER_QUEST, "QuestListChallengeItem", this.challengeData.Length, this.isResetUI, (Func<int, Transform, Transform>) ((i, t) => this.Realizes("QuestListChallengeItem", t)), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetActive(t, true);
        this.SetEvent(t, "SELECT_ORDER", i);
        QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) this.challengeData[i].questId);
        if (isGuildRequest)
        {
          this.SetActive(t, (Enum) QuestChallengeSelect.UI.TWN_DIFFICULT_STAR, false);
          this.SetActive(t, (Enum) QuestChallengeSelect.UI.TXT_NEED_POINT, true);
          string text = string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 6U), (object) MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(questData.rarity), (object) MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(questData.rarity));
          this.SetLabelText(t, (Enum) QuestChallengeSelect.UI.TXT_NEED_POINT, text);
        }
        else
        {
          this.SetActive(t, (Enum) QuestChallengeSelect.UI.TWN_DIFFICULT_STAR, false);
          this.SetActive(t, (Enum) QuestChallengeSelect.UI.TXT_NEED_POINT, false);
          Debug.Log((object) "2");
        }
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
        ItemIcon icon = ItemIcon.Create(ItemIcon.GetItemIconType(questData.questType), enemyData.iconId, new RARITY_TYPE?(questData.rarity), this.FindCtrl(t, (Enum) QuestChallengeSelect.UI.OBJ_ENEMY), enemyData.element);
        icon.SetEnableCollider(false);
        this.SetActive(t, (Enum) QuestChallengeSelect.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
        this.SetElementSprite(t, (Enum) QuestChallengeSelect.UI.SPR_ELEMENT, (int) enemyData.element);
        this.SetElementSprite(t, (Enum) QuestChallengeSelect.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
        this.SetActive(t, (Enum) QuestChallengeSelect.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
        this.SetLabelText(t, (Enum) QuestChallengeSelect.UI.LBL_QUEST_NAME, questData.questText);
        int num1 = 1;
        ClearStatusQuestEnemySpecies questEnemySpecies = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestEnemySpecies(questData.questID);
        if (questEnemySpecies != null)
          num1 = questEnemySpecies.questStatus;
        int num2 = i + 100;
        this.SetToggleGroup(t, (Enum) QuestChallengeSelect.UI.OBJ_ICON_NEW, num2);
        if (num1 != 1)
        {
          this.SetToggle(t, (Enum) QuestChallengeSelect.UI.OBJ_ICON_NEW, false);
          this.SetActive(t, (Enum) QuestChallengeSelect.UI.OBJ_ICON_ROOT, false);
        }
        else
        {
          this.SetActive(t, (Enum) QuestChallengeSelect.UI.OBJ_ICON_ROOT, true);
          this.SetToggle(t, (Enum) QuestChallengeSelect.UI.OBJ_ICON_NEW, true);
          this.SetVisibleWidgetEffect((Enum) QuestChallengeSelect.UI.SCR_ORDER_QUEST, t, (Enum) QuestChallengeSelect.UI.SPR_ICON_NEW, "ef_ui_questselect_new");
        }
        Transform ctrl = this.FindCtrl(t, (Enum) QuestChallengeSelect.UI.OBJ_FRAME);
        if (!Object.op_Inequality((Object) ctrl, (Object) null))
          return;
        UIPanel uiPanel = ((Component) ctrl).gameObject.GetComponent<UIPanel>();
        if (Object.op_Equality((Object) uiPanel, (Object) null))
        {
          uiPanel = ((Component) ctrl).gameObject.AddComponent<UIPanel>();
          uiPanel.depth = scrollView.panel.depth + 1;
        }
        uiPanel.widgetsAreStatic = false;
        if (this.isScrollViewReady)
          this.PanelToStatic(icon, uiPanel);
        else
          this.onScrollViewReady += (System.Action) (() => this.PanelToStatic(icon, uiPanel));
      }));
    }
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
    if (eventData < 0 || eventData >= this.challengeData.Length)
      GameSection.StopEvent();
    else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint) this.challengeData[eventData].questId))
    {
      GameSection.StopEvent();
    }
    else
    {
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint) this.challengeData[eventData].questId);
      GameSection.SetEventData((object) MonoBehaviourSingleton<QuestManager>.I.GetQuestChallengeInfoData((uint) this.challengeData[eventData].questId));
      this.isScrollViewReady = false;
    }
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
      this.SetDirty((Enum) QuestChallengeSelect.UI.GRD_ORDER_QUEST);
    if (false)
      return;
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY;
  }

  private void Update()
  {
    if (!this.isScrollViewReady)
      this.TryScrollViewToReady();
    if (!this.isQuestItemDirty)
      return;
    this.RefreshUI();
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeList(this.param, (Action<bool, Error>) ((is_success, err) => this.StartCoroutine(this.CheckLimitQuestItem())), true)));
    this.isQuestItemDirty = false;
  }

  private IEnumerator CheckLimitQuestItem()
  {
    if (!this.isQuestItemDirty)
    {
      List<QuestData> challengeList = MonoBehaviourSingleton<QuestManager>.I.challengeList;
      if (challengeList != null && challengeList.Count > 0)
      {
        float minRemainingSec = float.MaxValue;
        QuestData questData = (QuestData) null;
        float parseRemainingSec;
        challengeList.ForEach((Action<QuestData>) (challengeQuest =>
        {
          for (int index = 0; index < challengeQuest.remainTimes.Count; ++index)
          {
            parseRemainingSec = challengeQuest.remainTimes[index];
            if ((double) parseRemainingSec > 0.0 && (double) minRemainingSec > (double) parseRemainingSec)
            {
              minRemainingSec = parseRemainingSec;
              questData = challengeQuest;
            }
          }
        }));
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
    this.ShowChallenge();
  }

  private void OnQuery_PAGE_NEXT()
  {
    this.isResetUI = true;
    this.nowPage = this.nowPage < this.pageMax ? this.nowPage + 1 : 1;
    this.ShowChallenge();
  }

  private void InitSearchParam()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetChallengeSearchRequestFromPrefs(this.GetEnemyLevelFromUserLevel(), this.param);
  }

  private void OnQuery_CONDITION() => GameSection.SetEventData((object) this.param);

  protected void OnCloseDialog_QuestAcceptChallengeRoomCondition()
  {
    if (!(GameSection.GetEventData() is QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam eventData) || eventData.order != 1)
      return;
    this.param = eventData;
    this.nowPage = 1;
    this.isResetUI = true;
    this.RefreshUI();
  }

  private int GetEnemyLevelFromUserLevel()
  {
    return this.GetEnemyLevelFromUserLevel((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level);
  }

  private int GetEnemyLevelFromUserLevel(int userLevel)
  {
    int questItemLevelMax = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_ITEM_LEVEL_MAX;
    return (Mathf.Clamp(userLevel, 10, questItemLevelMax) + 9) / 10 * 10;
  }

  protected enum UI
  {
    SPR_BG_BTN_CLOSE,
    BTN_INPUT_CLOSE,
    BTN_INPUT_CLOSE_BG,
    BG,
    OBJ_ORDER_ROOT,
    STR_ORDER_NON_LIST,
    GRD_ORDER_QUEST,
    OBJ_ICON_ROOT,
    SCR_ORDER_QUEST,
    OBJ_FRAME,
    LBL_QUEST_NAME,
    OBJ_ENEMY,
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
    BTN_PARTY,
    BTN_GUILD_REQUEST,
    TXT_NEED_POINT,
    OBJ_ICON_NEW,
    SPR_ICON_NEW,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    LBL_MAX,
    LBL_NOW,
    OBJ_CHALLENGE_MSG_ROOT,
    STR_CHALLENGE_MESSAGE,
    STR_CHALLENGE_BONUS_MESSAGE,
    BTN_DETAIL,
  }
}
