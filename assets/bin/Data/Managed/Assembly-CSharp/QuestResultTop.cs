// Decompiled with JetBrains decompiler
// Type: QuestResultTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestResultTop : GameSection
{
  protected SortCompareData[] dropItemIconData;
  private QuestResultTop.UI dropItemGRD = QuestResultTop.UI.GRD_DROP_ITEM;
  private QuestResultTop.UI dropItemSCR = QuestResultTop.UI.OBJ_SCROLL_VIEW;
  protected bool isVictory;
  protected List<PointShopResultData> pointShopResultData;
  protected PointShopResultData missionPointData;
  protected bool is_open_get_rare_item;
  protected int dropItemNum;
  protected int dropLineNum;
  protected float animTimer;
  private int animIndex = -1;
  protected bool animationEnd;
  private bool isValidMission;
  protected bool startDropDirection;
  private string lvupTextFormat = string.Empty;
  protected int[] missionNewClear;
  protected bool isValidMissionNewClearAnim;
  protected const float ANIM_INDEX_STEP_TIME = 0.4f;
  private const float ICON_SCROLL_VALUE = -1.38f;
  private const float ICON_SCROLL_VALUE_2 = -1.5f;
  protected float animScrollValue = -1.38f;
  protected const int DROP_ITEM_LIST_NUM_X = 5;
  protected const int DROP_ITEM_LIST_MIN_Y = 1;
  protected const int DROP_ITEM_LIST_MIN = 5;
  protected const string NORMAL_DROP_EFF_NAME = "ef_ui_dropitem_silver_01";
  protected const string RARE_DROP_EFF_NAME = "ef_ui_dropitem_gold_01";
  protected const string BREAK_DROP_EFF_NAME = "ef_ui_dropitem_red_01";
  protected QuestCompleteReward dropReward;
  protected QuestCompleteReward eventReward;
  protected List<string> eventRewardTitles;
  private PointEventCurrentData exploreResultData;
  private int guildPoint;
  protected QuestResultTop.RESULT_ANIM_STATE animState;
  private ResultExpGaugeCtrl expGauge;
  protected System.Action dropSellCallback;
  protected System.Action pointEventCallback;
  protected QuestCompleteReward missionReward;
  protected QuestCompleteReward missionCompleteReward;
  protected bool isOpenedMissionClearDialog;
  protected System.Action missionClearRewardCallback;
  protected QuestCompleteReward followReward;
  protected System.Action followBonusCallback;
  protected System.Action eventRewardCallback;
  protected int eventRewardIndex;
  protected List<QuestCompleteReward> eventRewardList;
  protected System.Action firstClearRewardCallback;
  private bool canEnterParty;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    yield return (object) new WaitForEndOfFrame();
    yield return (object) MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    this.InitReward();
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_dropitem_silver_01");
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_dropitem_gold_01");
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_dropitem_red_01");
    this.CacheAudio(load_queue);
    if (this.pointShopResultData.Count > 0)
    {
      foreach (PointShopResultData pointShopResultData in this.pointShopResultData)
        load_queue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName(pointShopResultData.pointShopId));
    }
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    GC.Collect();
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
    base.Initialize();
    if (MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id != MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
      this.StartCoroutine("AutoJoinParty");
  }

  protected virtual void InitReward()
  {
    if (MonoBehaviourSingleton<QuestManager>.I.compData != null)
    {
      this.isVictory = true;
      int start_ary_index1 = 0;
      QuestCompleteRewardList reward = MonoBehaviourSingleton<QuestManager>.I.compData.reward;
      this.dropReward = new QuestCompleteReward();
      this.eventReward = new QuestCompleteReward();
      ResultUtility.DevideRewardDropAndEvent(reward.drop, ref this.dropReward, ref this.eventReward, ref this.eventRewardTitles);
      QuestCompleteReward breakPartsReward = reward.breakPartsReward;
      QuestCompleteReward breakReward = reward.breakReward;
      QuestCompleteReward order = reward.order;
      this.exploreResultData = MonoBehaviourSingleton<QuestManager>.I.compData.pointExplore;
      if (this.exploreResultData != null)
      {
        QuestCompleteReward exploreReward = this.CreateExploreReward();
        if (exploreReward != null)
          ResultUtility.DevideRewardDropAndEvent(exploreReward, ref this.dropReward, ref this.eventReward, ref this.eventRewardTitles);
      }
      this.guildPoint = MonoBehaviourSingleton<QuestManager>.I.compData.guildPoint;
      this.missionReward = reward.mission;
      this.missionCompleteReward = reward.missionComplete;
      this.followReward = reward.followReward;
      this.pointShopResultData = MonoBehaviourSingleton<QuestManager>.I.compData.pointShop ?? new List<PointShopResultData>();
      for (int index = 0; index < this.pointShopResultData.Count; ++index)
      {
        if (this.pointShopResultData[index].missionPoint > 0)
        {
          this.missionPointData = this.pointShopResultData[index];
          break;
        }
      }
      List<SortCompareData> drop_ary = new List<SortCompareData>();
      int start_ary_index2 = ResultUtility.SetDropData(drop_ary, start_ary_index1, order.item);
      int start_ary_index3 = ResultUtility.SetDropData(drop_ary, start_ary_index2, order.equipItem);
      int start_ary_index4 = ResultUtility.SetDropData(drop_ary, start_ary_index3, order.skillItem);
      int start_ary_index5 = ResultUtility.SetDropData(drop_ary, start_ary_index4, order.accessoryItem);
      int start_ary_index6 = ResultUtility.SetDropData(drop_ary, start_ary_index5, this.dropReward.item);
      int start_ary_index7 = ResultUtility.SetDropData(drop_ary, start_ary_index6, this.dropReward.equipItem);
      int start_ary_index8 = ResultUtility.SetDropData(drop_ary, start_ary_index7, this.dropReward.skillItem);
      int start_ary_index9 = ResultUtility.SetDropData(drop_ary, start_ary_index8, this.dropReward.questItem);
      int start_ary_index10 = ResultUtility.SetDropData(drop_ary, start_ary_index9, this.dropReward.accessoryItem);
      int start_ary_index11 = ResultUtility.SetDropData(drop_ary, start_ary_index10, breakReward.item);
      int start_ary_index12 = ResultUtility.SetDropData(drop_ary, start_ary_index11, breakReward.equipItem);
      int start_ary_index13 = ResultUtility.SetDropData(drop_ary, start_ary_index12, breakReward.skillItem);
      int start_ary_index14 = ResultUtility.SetDropData(drop_ary, start_ary_index13, breakReward.accessoryItem);
      int start_ary_index15 = ResultUtility.SetDropData(drop_ary, start_ary_index14, breakPartsReward.item, REWARD_CATEGORY.BREAK);
      int start_ary_index16 = ResultUtility.SetDropData(drop_ary, start_ary_index15, breakPartsReward.equipItem, REWARD_CATEGORY.BREAK);
      int start_ary_index17 = ResultUtility.SetDropData(drop_ary, start_ary_index16, breakPartsReward.skillItem, REWARD_CATEGORY.BREAK);
      ResultUtility.SetDropData(drop_ary, start_ary_index17, breakPartsReward.accessoryItem, REWARD_CATEGORY.BREAK);
      drop_ary.Sort((Comparison<SortCompareData>) ((l, r) => r.GetSortValueQuestResult() - l.GetSortValueQuestResult()));
      this.dropItemIconData = drop_ary.ToArray();
      this.dropItemNum = this.dropItemIconData.Length;
      this.dropLineNum = (this.dropItemNum - 1) / 5 + 1;
    }
    else
      this.dropItemIconData = new SortCompareData[0];
    if (this.dropItemNum != 0)
      return;
    this.animationEnd = true;
  }

  protected override void OnClose()
  {
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null) && MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID))
      MonoBehaviourSingleton<UIManager>.I.mainChat.ShowOpenButton();
    base.OnClose();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (!MonoBehaviourSingleton<FilterManager>.IsValid())
      return;
    MonoBehaviourSingleton<FilterManager>.I.StopBlur();
  }

  protected virtual void Update()
  {
    if (!this.isInitialized)
      return;
    this.ResultItemAnimation();
  }

  public override void UpdateUI()
  {
    this.SetFullScreenButton((Enum) QuestResultTop.UI.BTN_SKIP_FULL_SCREEN);
    this.SetHeight((Enum) QuestResultTop.UI.BTN_SKIP_IN_SCROLL, this.dropLineNum * 100);
    this.SetHeight((Enum) QuestResultTop.UI.BTN_SKIP_IN_SCROLL_2, this.dropLineNum * 100);
    this.SetActive((Enum) QuestResultTop.UI.BTN_NEXT, false);
    this.SetFontStyle((Enum) QuestResultTop.UI.STR_TITLE_EXP, (FontStyle) 2);
    this.SetFontStyle((Enum) QuestResultTop.UI.STR_TITLE_MISSION, (FontStyle) 2);
    this.SetFontStyle((Enum) QuestResultTop.UI.STR_TITLE_REWARD, (FontStyle) 2);
    if (MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag != null)
      this.missionNewClear = MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag.ToArray();
    int my_user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    int before_level = MonoBehaviourSingleton<InGameRecorder>.I.players.Find((Predicate<InGameRecorder.PlayerRecord>) (data => data.charaInfo.userId == my_user_id)).beforeLevel;
    this.SetLabelText((Enum) QuestResultTop.UI.LBL_PLAYER_LV, before_level.ToString());
    this.InitDeactive((Enum) QuestResultTop.UI.LBL_PLAYER_LVUP);
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
    {
      UIVirtualScreen componentInChildren = ((Component) this).GetComponentInChildren<UIVirtualScreen>();
      UIWidget component = ((Component) this.GetCtrl((Enum) QuestResultTop.UI.SHADOW)).GetComponent<UIWidget>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null) && Object.op_Inequality((Object) component, (Object) null))
      {
        component.width = (int) componentInChildren.ScreenWidthFull;
        component.height = (int) componentInChildren.ScreenHeightFull;
      }
    }
    this.SetProgressValue((Enum) QuestResultTop.UI.PBR_EXP, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ExpProgress01);
    this.SetLabelText((Enum) QuestResultTop.UI.LBL_QUEST_NAME, Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).questText);
    QuestResultTop.UI[] uiArray1 = new QuestResultTop.UI[3]
    {
      QuestResultTop.UI.OBJ_MISSION_01,
      QuestResultTop.UI.OBJ_MISSION_02,
      QuestResultTop.UI.OBJ_MISSION_03
    };
    QuestResultTop.UI[] uiArray2 = new QuestResultTop.UI[3]
    {
      QuestResultTop.UI.LBL_MISSION_NAME_01,
      QuestResultTop.UI.LBL_MISSION_NAME_02,
      QuestResultTop.UI.LBL_MISSION_NAME_03
    };
    QuestResultTop.UI[] uiArray3 = new QuestResultTop.UI[3]
    {
      QuestResultTop.UI.SPR_CROWN_01,
      QuestResultTop.UI.SPR_CROWN_02,
      QuestResultTop.UI.SPR_CROWN_03
    };
    QuestResultTop.UI[] uiArray4 = new QuestResultTop.UI[3]
    {
      QuestResultTop.UI.SPR_CLEARED_CROWN_01,
      QuestResultTop.UI.SPR_CLEARED_CROWN_02,
      QuestResultTop.UI.SPR_CLEARED_CROWN_03
    };
    QuestResultTop.UI[] uiArray5 = new QuestResultTop.UI[3]
    {
      QuestResultTop.UI.TEX_MISSION_COIN_01,
      QuestResultTop.UI.TEX_MISSION_COIN_02,
      QuestResultTop.UI.TEX_MISSION_COIN_03
    };
    QuestResultTop.UI[] uiArray6 = new QuestResultTop.UI[3]
    {
      QuestResultTop.UI.SPR_CROWN01_OFF,
      QuestResultTop.UI.SPR_CROWN02_OFF,
      QuestResultTop.UI.SPR_CROWN03_OFF
    };
    QuestInfoData.Mission[] missionData = QuestInfoData.CreateMissionData(Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID));
    if (missionData != null)
    {
      this.isValidMission = true;
      for (int index = 0; index < 3; ++index)
      {
        bool is_visible = missionData[index] != null;
        this.SetActive((Enum) uiArray1[index], is_visible);
        if (is_visible)
        {
          this.SetLabelText((Enum) uiArray2[index], missionData[index].tableData.missionText);
          bool flag1 = this.missionNewClear != null && this.missionNewClear[index] > 0;
          bool flag2 = missionData[index].state >= CLEAR_STATUS.CLEAR | flag1;
          if (flag1)
            this.isValidMissionNewClearAnim = true;
          if (this.missionPointData != null)
          {
            this.SetActive((Enum) uiArray5[index], true);
            this.SetActive((Enum) uiArray6[index], false);
            this.SetActive((Enum) uiArray3[index], false);
            this.SetActive((Enum) uiArray4[index], false);
            UITexture component = ((Component) this.GetCtrl((Enum) uiArray5[index])).GetComponent<UITexture>();
            if (flag1)
              ResourceLoad.LoadPointIconImageTexture(component, (uint) this.missionPointData.pointShopId);
            else
              ResourceLoad.LoadGrayPointIconImageTexture(component, (uint) this.missionPointData.pointShopId);
          }
          else
          {
            this.SetActive((Enum) uiArray5[index], false);
            this.SetActive((Enum) uiArray6[index], true);
            if (flag1)
            {
              this.SetActive((Enum) uiArray3[index], true);
              this.SetActive((Enum) uiArray4[index], false);
            }
            else if (flag2)
            {
              this.SetActive((Enum) uiArray3[index], false);
              this.SetActive((Enum) uiArray4[index], true);
            }
            else
            {
              this.SetActive((Enum) uiArray3[index], false);
              this.SetActive((Enum) uiArray4[index], false);
            }
          }
        }
      }
    }
    else
    {
      for (int index = 0; index < 3; ++index)
        this.SetActive((Enum) uiArray1[index], false);
    }
    int num1 = 0;
    int exp = 0;
    if (this.isVictory)
    {
      QuestCompleteRewardList reward = MonoBehaviourSingleton<QuestManager>.I.compData.reward;
      QuestCompleteReward breakReward = reward.breakReward;
      QuestCompleteReward order = reward.order;
      num1 = this.dropReward.money + breakReward.money + order.money;
      exp = this.dropReward.exp + breakReward.exp + order.exp;
    }
    this.SetLabelText((Enum) QuestResultTop.UI.LBL_EXP, 0.ToString());
    this.SetLabelText((Enum) QuestResultTop.UI.LBL_REWARD_GOLD, num1.ToString("N0"));
    bool is_mission_visible = true;
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
    if (questData != null)
      is_mission_visible = questData.questType == QUEST_TYPE.HAPPEN || questData.questType == QUEST_TYPE.EVENT;
    this.dropItemGRD = is_mission_visible ? QuestResultTop.UI.GRD_DROP_ITEM : QuestResultTop.UI.GRD_DROP_ITEM_2;
    this.dropItemSCR = is_mission_visible ? QuestResultTop.UI.OBJ_SCROLL_VIEW : QuestResultTop.UI.OBJ_SCROLL_VIEW_2;
    this.animScrollValue = is_mission_visible ? -1.38f : -1.5f;
    this.SetGrid((Enum) this.dropItemGRD, (string) null, this.dropItemIconData.Length, true, (Action<int, Transform, bool>) ((i, o, is_recycle) =>
    {
      ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
      RARITY_TYPE? rarity = new RARITY_TYPE?();
      ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
      EQUIPMENT_TYPE? magi_enable_icon_type = new EQUIPMENT_TYPE?();
      int icon_id = -1;
      int num2 = -1;
      if (i < this.dropItemIconData.Length && this.dropItemIconData[i] != null)
      {
        itemIconType = this.dropItemIconData[i].GetIconType();
        icon_id = this.dropItemIconData[i].GetIconID();
        rarity = new RARITY_TYPE?(this.dropItemIconData[i].GetRarity());
        element = this.dropItemIconData[i].GetIconElement();
        magi_enable_icon_type = this.dropItemIconData[i].GetIconMagiEnableType();
        num2 = this.dropItemIconData[i].GetNum();
        if (num2 == 1)
          num2 = -1;
      }
      bool is_new = false;
      switch (itemIconType)
      {
        case ITEM_ICON_TYPE.NONE:
          int enemy_icon_id = 0;
          int enemy_icon_id2 = 0;
          if (itemIconType == ITEM_ICON_TYPE.ITEM)
          {
            ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(this.dropItemIconData[i].GetTableID());
            enemy_icon_id = itemData.enemyIconID;
            enemy_icon_id2 = itemData.enemyIconID2;
          }
          ItemIcon _icon;
          if (this.dropItemIconData[i].GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
            _icon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam()
            {
              icon_type = this.dropItemIconData[i].GetIconType(),
              icon_id = this.dropItemIconData[i].GetIconID(),
              rarity = new RARITY_TYPE?(this.dropItemIconData[i].GetRarity()),
              parent = o,
              element = this.dropItemIconData[i].GetIconElement(),
              magi_enable_equip_type = this.dropItemIconData[i].GetIconMagiEnableType(),
              num = this.dropItemIconData[i].GetNum(),
              enemy_icon_id = enemy_icon_id,
              enemy_icon_id2 = enemy_icon_id2,
              questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
            });
          else
            _icon = ItemIcon.Create(itemIconType, icon_id, rarity, o, element, magi_enable_icon_type, num2, "DROP", i, is_new, enemy_icon_id: enemy_icon_id, enemy_icon_id2: enemy_icon_id2, getType: this.dropItemIconData[i].GetGetType());
          _icon.SetRewardBG(true);
          _icon.SetRewardCategoryInfo(this.dropItemIconData[i].GetCategory());
          this.SetMaterialInfo(_icon.transform, this.dropItemIconData[i].GetMaterialType(), this.dropItemIconData[i].GetTableID(), this.GetCtrl((Enum) (QuestResultTop.UI) (is_mission_visible ? 44 : 45)));
          Transform transform = this.SetPrefab(o, "QuestResultDropIconOpener");
          QuestResultDropIconOpener.Info info1 = new QuestResultDropIconOpener.Info()
          {
            IsRare = ResultUtility.IsRare(this.dropItemIconData[i]),
            IsBroken = ResultUtility.IsBreakReward(this.dropItemIconData[i])
          };
          ((Component) transform).GetComponent<QuestResultDropIconOpener>().Initialized(_icon, info1, (Action<Transform, QuestResultDropIconOpener.Info, bool>) ((t, info, is_skip) =>
          {
            string ui_effect_name = "ef_ui_dropitem_silver_01";
            if (info.IsBroken)
              ui_effect_name = "ef_ui_dropitem_red_01";
            else if (info.IsRare)
              ui_effect_name = "ef_ui_dropitem_gold_01";
            this.SetVisibleWidgetOneShotEffect(this.GetCtrl((Enum) this.dropItemSCR), t, ui_effect_name);
          }));
          break;
        case ITEM_ICON_TYPE.ITEM:
        case ITEM_ICON_TYPE.QUEST_ITEM:
          if (this.dropItemIconData[i].GetUniqID() != 0UL)
          {
            is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(itemIconType, this.dropItemIconData[i].GetUniqID());
            goto case ITEM_ICON_TYPE.NONE;
          }
          goto case ITEM_ICON_TYPE.NONE;
        default:
          is_new = true;
          goto case ITEM_ICON_TYPE.NONE;
      }
    }));
    if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level > before_level)
    {
      if (string.IsNullOrEmpty(this.lvupTextFormat))
      {
        UILabel component = this.GetComponent<UILabel>((Enum) QuestResultTop.UI.LBL_LVUP_NUM);
        if (Object.op_Inequality((Object) component, (Object) null))
          this.lvupTextFormat = component.text;
      }
      if (GameSaveData.instance.lvupMessageFlag != 1)
      {
        GameSaveData.instance.lvupMessageFlag = 1;
        GameSaveData.Save();
      }
    }
    this.expGauge = this.GetComponent<ResultExpGaugeCtrl>((Enum) QuestResultTop.UI.OBJ_RESULT_EXP_GAUGE_CTRL);
    this.expGauge.InitDirection((Action<ResultExpGaugeCtrl>) (gauge =>
    {
      gauge.getExp = (float) exp;
      gauge.startExp = (float) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.Exp - gauge.getExp;
      gauge.nowLevel = before_level;
      gauge.remainLevelUpCnt = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level - before_level;
      gauge.OnUpdate = (Action<bool, int, ResultExpGaugeCtrl>) ((is_lvup, now_gauge_exp, _gauge) =>
      {
        this.SetLabelText((Enum) QuestResultTop.UI.LBL_EXP, now_gauge_exp.ToString("N0"));
        this.PlayAudio(QuestResultTop.AUDIO.COUNTUP);
        if (!is_lvup)
          return;
        this.SetLabelText((Enum) QuestResultTop.UI.LBL_PLAYER_LV, _gauge.nowLevel.ToString());
      });
      gauge.callBack = (System.Action) (() =>
      {
        int level = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
        if (this.animState == QuestResultTop.RESULT_ANIM_STATE.EXP_GAUGE && (level > before_level || MonoBehaviourSingleton<UIManager>.I.levelUp.IsLevelUp()))
        {
          this.animState = QuestResultTop.RESULT_ANIM_STATE.LVUP;
          MonoBehaviourSingleton<UIManager>.I.levelUp.PlayLevelUpForce((System.Action) (() =>
          {
            if (is_mission_visible)
              this.ResultAnim2();
            else
              this.TreasureStart();
          }));
        }
        else if (is_mission_visible)
          this.ResultAnim2();
        else
          this.TreasureStart();
      });
    }));
    this.SetActive((Enum) QuestResultTop.UI.STR_EMPTY_MISSION, !this.isValidMission);
    this.SetActive((Enum) QuestResultTop.UI.OBJ_MISSION_ROOT, is_mission_visible);
    this.SetActive((Enum) QuestResultTop.UI.GET_ITEM, is_mission_visible);
    this.SetActive((Enum) QuestResultTop.UI.GET_ITEM_2, !is_mission_visible);
    int pointNumber = this.pointShopResultData.Count;
    List<string> pointShopObjNames = new List<string>();
    if (this.exploreResultData != null)
    {
      ++pointNumber;
      pointShopObjNames.Add("QuestResultExplorePoint");
    }
    if (this.guildPoint > 0)
    {
      ++pointNumber;
      pointShopObjNames.Add("QuestResultGuildRequestPoint");
    }
    pointNumber = Mathf.Min(pointNumber, 4);
    bool is_visible1 = pointNumber > 0;
    this.SetActive((Enum) QuestResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT, is_visible1);
    if (is_visible1)
      this.SetGrid((Enum) QuestResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT, "", pointNumber, true, (Func<int, Transform, Transform>) ((i, parent) =>
      {
        if (pointShopObjNames.Count == 0)
          return this.Realizes("QuestResultPointShop", parent);
        int num3 = -(i - pointNumber) - 1;
        return num3 >= 0 && num3 < pointShopObjNames.Count ? this.Realizes(pointShopObjNames[-(i - pointNumber) - 1], parent) : this.Realizes("QuestResultPointShop", parent);
      }), (Action<int, Transform, bool>) ((i, t, b) =>
      {
        this.ResetTween(t);
        int index = -(i - pointNumber) - 1;
        if (pointShopObjNames.Count > 0 && index >= 0 && index < pointShopObjNames.Count)
        {
          switch (pointShopObjNames[index])
          {
            case "QuestResultExplorePoint":
              int getPoint = this.exploreResultData.pointRankingData.getPoint;
              int num4 = getPoint + this.exploreResultData.pointRankingData.userPoint;
              this.SetLabelText(t, (Enum) QuestResultTop.UI.LBL_EXPLORE_GET_POINT, string.Format("+" + StringTable.Get(STRING_CATEGORY.EXPLORE, 0U), (object) getPoint));
              this.SetLabelText(t, (Enum) QuestResultTop.UI.LBL_EXPLORE_TOTAL_POINT, string.Format(StringTable.Get(STRING_CATEGORY.EXPLORE, 0U), (object) num4));
              break;
            case "QuestResultGuildRequestPoint":
              this.SetLabelText(t, (Enum) QuestResultTop.UI.LBL_GUILD_REQUEST_GET_POINT, this.guildPoint.ToString());
              break;
          }
        }
        else
        {
          PointShopResultData pointShopResultData = this.pointShopResultData[i];
          this.SetActive(t, (Enum) QuestResultTop.UI.OBJ_NORMAL_POINT_SHOP_ROOT, !pointShopResultData.isEvent);
          if (!pointShopResultData.isEvent)
          {
            this.SetLabelText(t, (Enum) QuestResultTop.UI.LBL_NORMAL_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.getPoint));
            this.SetLabelText(t, (Enum) QuestResultTop.UI.LBL_NORMAL_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.totalPoint));
            ResourceLoad.LoadPointIconImageTexture(((Component) this.FindCtrl(t, (Enum) QuestResultTop.UI.TEX_NORMAL_POINT_SHOP_ICON)).GetComponent<UITexture>(), (uint) pointShopResultData.pointShopId);
          }
          this.SetActive(t, (Enum) QuestResultTop.UI.OBJ_EVENT_POINT_SHOP_ROOT, pointShopResultData.isEvent);
          if (!pointShopResultData.isEvent)
            return;
          this.SetLabelText(t, (Enum) QuestResultTop.UI.LBL_EVENT_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.getPoint));
          this.SetLabelText(t, (Enum) QuestResultTop.UI.LBL_EVENT_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.totalPoint));
          ResourceLoad.LoadPointIconImageTexture(((Component) this.FindCtrl(t, (Enum) QuestResultTop.UI.TEX_EVENT_POINT_SHOP_ICON)).GetComponent<UITexture>(), (uint) pointShopResultData.pointShopId);
        }
      }));
    if (!is_mission_visible)
      this.GetCtrl((Enum) QuestResultTop.UI.OBJ_TREASURE_ROOT).localPosition = this.GetCtrl((Enum) QuestResultTop.UI.OBJ_TREASURE_ROOT_NON_MISSION).localPosition;
    this.StartCoroutine(this.ResultAnim1(exp != 0));
  }

  private IEnumerator ResultAnim1(bool is_wait)
  {
    this.PlayAudio(QuestResultTop.AUDIO.ADVENT);
    this.animState = QuestResultTop.RESULT_ANIM_STATE.EXP_GAUGE;
    bool wait = true;
    this.PlayTween((Enum) QuestResultTop.UI.OBJ_GET_EXP_ROOT, callback: (EventDelegate.Callback) (() => wait = false), is_input_block: false);
    while (wait)
      yield return (object) null;
    if (is_wait)
      yield return (object) new WaitForSeconds(0.1f);
    this.expGauge.StartAnim();
  }

  private void ResultAnim2()
  {
    this.PlayAudio(QuestResultTop.AUDIO.MISSION);
    this.animState = QuestResultTop.RESULT_ANIM_STATE.MISSION;
    this.MissionClearStart((System.Action) (() => this.OpenMissionClearRewardDialog((System.Action) (() => this.TreasureStart()))));
  }

  protected void MissionClearStart(System.Action callback)
  {
    if (!this.isValidMissionNewClearAnim)
    {
      this.PlayTween((Enum) QuestResultTop.UI.OBJ_MISSION_ROOT, callback: (EventDelegate.Callback) (() =>
      {
        if (callback == null)
          return;
        callback();
      }));
    }
    else
    {
      this.PlayTween((Enum) QuestResultTop.UI.OBJ_MISSION_ROOT, is_input_block: false);
      this.PlayTween((Enum) QuestResultTop.UI.OBJ_MISSION_NEW_CLEAR_ROOT, callback: (EventDelegate.Callback) (() =>
      {
        if (callback == null)
          return;
        callback();
      }), is_input_block: false);
    }
  }

  protected virtual void TreasureStart()
  {
    this.animState = QuestResultTop.RESULT_ANIM_STATE.TREASURE;
    this.PlayAudio(QuestResultTop.AUDIO.MONEY);
    this.PlayAudio(QuestResultTop.AUDIO.MONEY_WH);
    this.PlayAudio(QuestResultTop.AUDIO.ACHIEVEMENT);
    if (this.pointShopResultData.Count > 0)
    {
      foreach (Transform t in ((Component) this.GetCtrl((Enum) QuestResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT)).transform)
        this.PlayTween(t);
    }
    this.PlayTween((Enum) QuestResultTop.UI.OBJ_TREASURE_ROOT, callback: (EventDelegate.Callback) (() => this.startDropDirection = true), is_input_block: false);
  }

  protected virtual void ResultItemAnimation()
  {
    if (!this.startDropDirection)
      return;
    if (this.animationEnd && this.animState < QuestResultTop.RESULT_ANIM_STATE.END)
    {
      this.AnimationEnd();
    }
    else
    {
      if (this.animationEnd || this.IsOpenGetRareItem())
        return;
      this.animTimer += Time.deltaTime;
      if ((double) this.animTimer <= 0.40000000596046448)
        return;
      ++this.animIndex;
      this.animTimer = 0.0f;
      this.VisibleItemIcon(this.animIndex);
      int num = this.animIndex + 1;
      if (num >= this.dropItemNum)
      {
        this.AnimationEnd();
      }
      else
      {
        bool flag = false;
        if (MonoBehaviourSingleton<QuestManager>.IsValid())
          flag = MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
        if (flag || num < 5 || num % 5 != 0)
          return;
        this.SetScroll((Enum) this.dropItemSCR, this.animScrollValue);
      }
    }
  }

  protected void OpenRareItemDialog(SortCompareData icon_base)
  {
    if (this.IsOpenGetRareItem() || icon_base == null)
      return;
    this.is_open_get_rare_item = true;
    this.DispatchEvent("RARE", (object) new object[1]
    {
      (object) icon_base.GetName()
    });
  }

  protected bool IsOpenGetRareItem() => this.is_open_get_rare_item;

  public void OnCloseDialog_QuestResultGetRareItem() => this.is_open_get_rare_item = false;

  protected void VisibleItemIcon(int index, bool is_skip = false)
  {
    if (index >= this.dropItemIconData.Length || this.dropItemIconData[index] == null || this.dropItemIconData[index].GetTableID() == 0U)
      return;
    Transform child = this.GetChild((Enum) this.dropItemGRD, index);
    if (Object.op_Equality((Object) child, (Object) null))
      return;
    QuestResultDropIconOpener componentInChildren = ((Component) child).gameObject.GetComponentInChildren<QuestResultDropIconOpener>();
    if (Object.op_Equality((Object) componentInChildren, (Object) null))
      return;
    this.PlayAudio(QuestResultTop.AUDIO.DROPITEM);
    componentInChildren.StartEffect(is_skip);
  }

  protected void OpenedIconEndEff(int index)
  {
    Transform child = this.GetChild((Enum) this.dropItemGRD, index);
    if (Object.op_Equality((Object) child, (Object) null))
      return;
    QuestResultDropIconOpener componentInChildren = ((Component) child).gameObject.GetComponentInChildren<QuestResultDropIconOpener>();
    if (Object.op_Equality((Object) componentInChildren, (Object) null))
      return;
    componentInChildren.StartEffect(true);
  }

  protected virtual void AnimationEnd()
  {
    this.animState = QuestResultTop.RESULT_ANIM_STATE.END;
    this.animationEnd = true;
    this.OpenMissionClearRewardDialog((System.Action) (() => this.OpenFirstClearRewardDialog((System.Action) (() => this.OpenMutualFollowBonusDialog((System.Action) (() => this.OpenAllEventRewardDialog((System.Action) (() => this.OpenDropSell((System.Action) (() => this.OpenPointEvent((System.Action) (() => this.VisibleEndButton()))))))))))));
  }

  protected virtual void VisibleEndButton()
  {
    if (MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
      {
        this.SetActive((Enum) QuestResultTop.UI.BTN_END_HUNT_LEFT, this.animationEnd);
        this.SetActive((Enum) QuestResultTop.UI.BTN_REPEAT_HUNT, this.animationEnd);
        this.StartCoroutine("WaitForRepeatHunt");
      }
      else
      {
        this.SetActive((Enum) QuestResultTop.UI.BTN_END_HUNT_CENTER, this.animationEnd);
        this.StartCoroutine("WaitForHost");
      }
    }
    else
      this.SetActive((Enum) QuestResultTop.UI.BTN_NEXT, this.animationEnd);
    this.SetActive((Enum) QuestResultTop.UI.BTN_SKIP_FULL_SCREEN, false);
    this.SetActive((Enum) QuestResultTop.UI.BTN_SKIP_IN_SCROLL, false);
    this.SetActive((Enum) QuestResultTop.UI.BTN_SKIP_IN_SCROLL_2, false);
    this.DispatchEvent("RESULT_TUTORIAL");
  }

  protected void AnimSkip()
  {
    if (this.animationEnd)
      return;
    if (this.animIndex < 0)
      this.animIndex = 0;
    for (int index = 0; index < this.dropItemIconData.Length; ++index)
    {
      if (index < this.animIndex)
        this.OpenedIconEndEff(index);
      else
        this.VisibleItemIcon(index, true);
    }
    if (this.dropLineNum > 1)
    {
      bool flag = false;
      if (MonoBehaviourSingleton<QuestManager>.IsValid())
        flag = MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
      if (!flag)
        this.SetScroll((Enum) this.dropItemSCR, -100f);
    }
    this.animIndex = this.dropItemIconData.Length - 1;
    this.AnimationEnd();
    this.animTimer = 0.0f;
  }

  private void OnQuery_SKIP()
  {
    if (this.animState == QuestResultTop.RESULT_ANIM_STATE.END)
      return;
    switch (this.animState)
    {
      case QuestResultTop.RESULT_ANIM_STATE.EXP_GAUGE:
        this.SkipTween((Enum) QuestResultTop.UI.OBJ_GET_EXP_ROOT);
        this.expGauge.Skip();
        break;
      case QuestResultTop.RESULT_ANIM_STATE.LVUP:
        MonoBehaviourSingleton<UIManager>.I.levelUp.SkipAnim();
        break;
      case QuestResultTop.RESULT_ANIM_STATE.MISSION:
        this.animState = QuestResultTop.RESULT_ANIM_STATE.MISSION_REWARD;
        this.SkipTween((Enum) QuestResultTop.UI.OBJ_MISSION_ROOT);
        this.SkipTween((Enum) QuestResultTop.UI.OBJ_MISSION_NEW_CLEAR_ROOT);
        break;
      case QuestResultTop.RESULT_ANIM_STATE.TREASURE:
        this.SkipTween((Enum) QuestResultTop.UI.OBJ_TREASURE_ROOT);
        this.SkipTween((Enum) QuestResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT);
        this.AnimSkip();
        break;
    }
    GameSection.StopEvent();
  }

  private void OnQuery_FRIEND()
  {
    if (this.animState >= QuestResultTop.RESULT_ANIM_STATE.END)
      return;
    this.OnQuery_SKIP();
  }

  protected REWARD_TYPE GetRewardType(ITEM_ICON_TYPE icon_type)
  {
    switch (icon_type)
    {
      case ITEM_ICON_TYPE.SKILL_ATTACK:
      case ITEM_ICON_TYPE.SKILL_SUPPORT:
      case ITEM_ICON_TYPE.SKILL_HEAL:
      case ITEM_ICON_TYPE.SKILL_PASSIVE:
      case ITEM_ICON_TYPE.SKILL_GROW:
        return REWARD_TYPE.SKILL_ITEM;
      case ITEM_ICON_TYPE.ITEM:
      case ITEM_ICON_TYPE.ABILITY_ITEM:
        return REWARD_TYPE.ITEM;
      case ITEM_ICON_TYPE.COMMON:
        return REWARD_TYPE.COMMON;
      case ITEM_ICON_TYPE.STAMP:
        return REWARD_TYPE.STAMP;
      case ITEM_ICON_TYPE.ACCESSORY:
        return REWARD_TYPE.ACCESSORY;
      default:
        return REWARD_TYPE.EQUIP_ITEM;
    }
  }

  public static bool isNeedOpenDropSellDialog
  {
    get
    {
      return MonoBehaviourSingleton<QuestManager>.I.compData != null && MonoBehaviourSingleton<QuestManager>.I.compData.reward != null && MonoBehaviourSingleton<QuestManager>.I.compData.reward.sell.Count > 0;
    }
  }

  protected void OpenDropSell(System.Action callback)
  {
    if (!QuestResultTop.isNeedOpenDropSellDialog)
    {
      if (callback == null)
        return;
      callback();
    }
    else
    {
      this.dropSellCallback = callback;
      int total_sell = 0;
      List<SortCompareData> list = new List<SortCompareData>();
      MonoBehaviourSingleton<QuestManager>.I.compData.reward.sell.ForEach((Action<QuestCompleteReward.SellItem>) (data =>
      {
        ItemSortData itemSortData = new ItemSortData();
        ItemInfo itemInfo = new ItemInfo()
        {
          tableID = (uint) data.itemId
        };
        itemInfo.tableData = Singleton<ItemTable>.I.GetItemData(itemInfo.tableID);
        itemInfo.num = data.num;
        itemSortData.SetItem((object) itemInfo);
        list.Add((SortCompareData) itemSortData);
        total_sell += data.price;
      }));
      if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
        this.StartCoroutine(this.ExecEndDialogEvent(this.GetSceneName(), (System.Action) (() => this.DispatchEvent("DROP_SELL", (object) new object[2]
        {
          (object) list,
          (object) total_sell
        }))));
      else
        this.DispatchEvent("DROP_SELL", (object) new object[2]
        {
          (object) list,
          (object) total_sell
        });
    }
  }

  protected void OnCloseDialog_QuestResultDropSellConfirm()
  {
    if (this.dropSellCallback == null)
      return;
    this.StartCoroutine(this.OnCloseDropSellClearCoroutine());
  }

  protected IEnumerator OnCloseDropSellClearCoroutine()
  {
    if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
      yield return (object) null;
    if (this.dropSellCallback != null)
      this.dropSellCallback();
  }

  public static bool isNeedPointResult
  {
    get
    {
      return MonoBehaviourSingleton<QuestManager>.I.compData != null && MonoBehaviourSingleton<QuestManager>.I.compData.pointEvent != null && MonoBehaviourSingleton<QuestManager>.I.compData.pointEvent.Count != 0;
    }
  }

  protected void OpenPointEvent(System.Action callback)
  {
    if (!QuestResultTop.isNeedPointResult)
    {
      if (callback == null)
        return;
      callback();
    }
    else
    {
      this.pointEventCallback = callback;
      if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
        this.StartCoroutine(this.ExecEndDialogEvent(this.GetSceneName(), (System.Action) (() => this.DispatchEvent("CARNIVAL_POINT", (object) MonoBehaviourSingleton<QuestManager>.I.compData.pointEvent[0]))));
      else
        this.DispatchEvent("CARNIVAL_POINT", (object) MonoBehaviourSingleton<QuestManager>.I.compData.pointEvent[0]);
    }
  }

  protected void OnCloseDialog_QuestResultPointEvent()
  {
    if (this.pointEventCallback == null)
      return;
    this.StartCoroutine(this.OnCloseQuestResultPointEventClearCoroutine());
  }

  protected void OnCloseDialog_CarnivalResultPoint()
  {
    if (this.pointEventCallback == null)
      return;
    this.StartCoroutine(this.OnCloseQuestResultPointEventClearCoroutine());
  }

  protected IEnumerator OnCloseQuestResultPointEventClearCoroutine()
  {
    if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
      yield return (object) null;
    if (this.pointEventCallback != null)
      this.pointEventCallback();
  }

  protected bool isNeedOpenMissionClearDialog
  {
    get
    {
      if (MonoBehaviourSingleton<QuestManager>.I.compData == null || MonoBehaviourSingleton<QuestManager>.I.compData.reward == null)
        return false;
      bool missionClearDialog = false;
      if (this.missionNewClear != null && this.missionNewClear.Length != 0)
        missionClearDialog = Array.FindIndex<int>(this.missionNewClear, (Predicate<int>) (flag => flag > 0)) != -1;
      if (this.missionPointData != null)
        missionClearDialog = true;
      return missionClearDialog;
    }
  }

  private void OpenMissionClearRewardDialog(System.Action end_callback)
  {
    bool flag = this.animState == QuestResultTop.RESULT_ANIM_STATE.MISSION || this.animState == QuestResultTop.RESULT_ANIM_STATE.END;
    if (!this.isNeedOpenMissionClearDialog || !flag || this.isOpenedMissionClearDialog)
    {
      if (end_callback == null)
        return;
      end_callback();
    }
    else
    {
      QuestCompleteReward questCompleteReward = (QuestCompleteReward) null;
      PointShopResultData missionPoint = (PointShopResultData) null;
      bool isCompleteReward = false;
      if (this.missionPointData != null || this.missionReward != null)
      {
        if (this.missionReward != null)
        {
          questCompleteReward = this.missionReward;
          this.missionReward = (QuestCompleteReward) null;
        }
        if (this.missionPointData != null)
        {
          missionPoint = this.missionPointData;
          this.missionPointData = (PointShopResultData) null;
        }
      }
      else if (this.missionCompleteReward != null)
      {
        questCompleteReward = this.missionCompleteReward;
        this.missionCompleteReward = (QuestCompleteReward) null;
        isCompleteReward = true;
        if (this.animState < QuestResultTop.RESULT_ANIM_STATE.END)
          this.animState = QuestResultTop.RESULT_ANIM_STATE.MISSION_REWARD;
      }
      else
      {
        this.isOpenedMissionClearDialog = true;
        if (this.animState < QuestResultTop.RESULT_ANIM_STATE.END)
          this.animState = QuestResultTop.RESULT_ANIM_STATE.MISSION_REWARD;
        if (end_callback == null)
          return;
        end_callback();
        return;
      }
      List<SortCompareData> tmp = new List<SortCompareData>();
      int start_ary_index1 = 0;
      int gold = questCompleteReward != null ? questCompleteReward.money : 0;
      int crystal = questCompleteReward != null ? questCompleteReward.crystal : 0;
      if (questCompleteReward != null)
      {
        int start_ary_index2 = ResultUtility.SetDropData(tmp, start_ary_index1, questCompleteReward.item);
        int start_ary_index3 = ResultUtility.SetDropData(tmp, start_ary_index2, questCompleteReward.equipItem);
        int start_ary_index4 = ResultUtility.SetDropData(tmp, start_ary_index3, questCompleteReward.skillItem);
        start_ary_index1 = ResultUtility.SetDropData(tmp, start_ary_index4, questCompleteReward.accessoryItem);
      }
      if (start_ary_index1 == 0 && crystal == 0 && missionPoint == null)
      {
        if (end_callback == null)
          return;
        end_callback();
      }
      else
      {
        this.missionClearRewardCallback = end_callback;
        if (missionPoint == null)
          missionPoint = new PointShopResultData();
        if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
          this.StartCoroutine(this.ExecEndDialogEvent(this.GetSceneName(), (System.Action) (() => this.DispatchEvent("MISSION_CLEAR_REWARD", (object) new object[5]
          {
            (object) tmp,
            (object) gold,
            (object) crystal,
            (object) isCompleteReward,
            (object) missionPoint
          }))));
        else
          this.DispatchEvent("MISSION_CLEAR_REWARD", (object) new object[5]
          {
            (object) tmp,
            (object) gold,
            (object) crystal,
            (object) isCompleteReward,
            (object) missionPoint
          });
      }
    }
  }

  private void OnCloseDialog_QuestResultMissionClearRewardDialog()
  {
    if (this.missionClearRewardCallback == null)
      return;
    this.StartCoroutine(this.OnCloseMissionClearCoroutine());
  }

  private IEnumerator OnCloseMissionClearCoroutine()
  {
    if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
      yield return (object) null;
    this.OpenMissionClearRewardDialog(this.missionClearRewardCallback);
  }

  public static bool isNeedOpenFollowBonusDialog
  {
    get
    {
      if (MonoBehaviourSingleton<QuestManager>.I.compData == null || MonoBehaviourSingleton<QuestManager>.I.compData.reward == null)
        return false;
      bool followBonusDialog = false;
      List<int> userIdList = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserIdList(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
      for (int index = 0; index < userIdList.Count; ++index)
      {
        if (userIdList[index] != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
        {
          QuestResultUserCollection.ResultUserInfo userInfo = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserInfo(userIdList[index]);
          if (!userInfo.CanSendFollow & userInfo.IsFollower)
            followBonusDialog = true;
        }
      }
      return followBonusDialog;
    }
  }

  private void OpenMutualFollowBonusDialog(System.Action end_callback)
  {
    bool flag = this.animState == QuestResultTop.RESULT_ANIM_STATE.FOLLOW_BONUS || this.animState == QuestResultTop.RESULT_ANIM_STATE.END;
    if (!QuestResultTop.isNeedOpenFollowBonusDialog || !flag)
    {
      if (end_callback == null)
        return;
      end_callback();
    }
    else if (this.followReward != null)
    {
      QuestCompleteReward followReward = this.followReward;
      this.followReward = (QuestCompleteReward) null;
      List<SortCompareData> tmp = new List<SortCompareData>();
      int start_ary_index1 = 0;
      int gold = followReward.money;
      int crystal = followReward.crystal;
      int exp = followReward.exp;
      int start_ary_index2 = ResultUtility.SetDropData(tmp, start_ary_index1, followReward.item);
      int start_ary_index3 = ResultUtility.SetDropData(tmp, start_ary_index2, followReward.equipItem);
      int start_ary_index4 = ResultUtility.SetDropData(tmp, start_ary_index3, followReward.skillItem);
      int start_ary_index5 = ResultUtility.SetDropData(tmp, start_ary_index4, followReward.questItem);
      if (ResultUtility.SetDropData(tmp, start_ary_index5, followReward.accessoryItem) == 0 && crystal == 0 && gold == 0 && exp == 0)
      {
        if (end_callback == null)
          return;
        end_callback();
      }
      else
      {
        this.followBonusCallback = end_callback;
        if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
          this.StartCoroutine(this.ExecEndDialogEvent(this.GetSceneName(), (System.Action) (() => this.DispatchEvent("MUTUAL_FOLLOW_BONUS", (object) new object[3]
          {
            (object) tmp,
            (object) gold,
            (object) crystal
          }))));
        else
          this.DispatchEvent("MUTUAL_FOLLOW_BONUS", (object) new object[3]
          {
            (object) tmp,
            (object) gold,
            (object) crystal
          });
      }
    }
    else
    {
      if (this.animState < QuestResultTop.RESULT_ANIM_STATE.END)
        this.animState = QuestResultTop.RESULT_ANIM_STATE.FOLLOW_BONUS;
      if (end_callback == null)
        return;
      end_callback();
    }
  }

  protected void OnCloseDialog_QuestResultMutualFollowBonusDialog()
  {
    if (this.followBonusCallback == null)
      return;
    this.StartCoroutine(this.OnCloseFollowBonusCoroutine());
  }

  protected IEnumerator OnCloseFollowBonusCoroutine()
  {
    if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
      yield return (object) null;
    this.OpenMutualFollowBonusDialog(this.followBonusCallback);
  }

  protected void OpenAllEventRewardDialog(System.Action endCallback)
  {
    this.eventRewardIndex = 0;
    this.eventRewardList = new List<QuestCompleteReward>();
    for (int index = 0; index < this.eventRewardTitles.Count; ++index)
      this.eventRewardList.Add(new QuestCompleteReward());
    for (int index1 = 0; index1 < this.eventReward.eventPrice.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.eventRewardTitles.Count; ++index2)
      {
        if (this.eventRewardTitles[index2] == this.eventReward.eventPrice[index1].rewardTitle)
          this.eventRewardList[index2].eventPrice.Add(this.eventReward.eventPrice[index1]);
      }
    }
    for (int index3 = 0; index3 < this.eventReward.item.Count; ++index3)
    {
      for (int index4 = 0; index4 < this.eventRewardTitles.Count; ++index4)
      {
        if (this.eventRewardTitles[index4] == this.eventReward.item[index3].rewardTitle)
          this.eventRewardList[index4].item.Add(this.eventReward.item[index3]);
      }
    }
    for (int index5 = 0; index5 < this.eventReward.skillItem.Count; ++index5)
    {
      for (int index6 = 0; index6 < this.eventRewardTitles.Count; ++index6)
      {
        if (this.eventRewardTitles[index6] == this.eventReward.skillItem[index5].rewardTitle)
          this.eventRewardList[index6].skillItem.Add(this.eventReward.skillItem[index5]);
      }
    }
    for (int index7 = 0; index7 < this.eventReward.equipItem.Count; ++index7)
    {
      for (int index8 = 0; index8 < this.eventRewardTitles.Count; ++index8)
      {
        if (this.eventRewardTitles[index8] == this.eventReward.equipItem[index7].rewardTitle)
          this.eventRewardList[index8].equipItem.Add(this.eventReward.equipItem[index7]);
      }
    }
    for (int index9 = 0; index9 < this.eventReward.questItem.Count; ++index9)
    {
      for (int index10 = 0; index10 < this.eventRewardTitles.Count; ++index10)
      {
        if (this.eventRewardTitles[index10] == this.eventReward.questItem[index9].rewardTitle)
          this.eventRewardList[index10].questItem.Add(this.eventReward.questItem[index9]);
      }
    }
    for (int index11 = 0; index11 < this.eventReward.accessoryItem.Count; ++index11)
    {
      for (int index12 = 0; index12 < this.eventRewardTitles.Count; ++index12)
      {
        if (this.eventRewardTitles[index12] == this.eventReward.accessoryItem[index11].rewardTitle)
          this.eventRewardList[index12].accessoryItem.Add(this.eventReward.accessoryItem[index11]);
      }
    }
    if (this.eventRewardList.Count == 0)
    {
      endCallback();
    }
    else
    {
      this.OpenEventRewardDialog(this.eventRewardList[this.eventRewardIndex], this.eventRewardTitles[this.eventRewardIndex], endCallback);
      ++this.eventRewardIndex;
    }
  }

  protected void OpenEventRewardDialog(
    QuestCompleteReward reward,
    string title,
    System.Action end_callback)
  {
    List<SortCompareData> tmp = new List<SortCompareData>();
    int start_ary_index1 = 0;
    int gold = 0;
    int crystal = 0;
    for (int index = 0; index < reward.eventPrice.Count; ++index)
    {
      gold += reward.eventPrice[index].gold;
      crystal += reward.eventPrice[index].crystal;
    }
    int start_ary_index2 = ResultUtility.SetDropData(tmp, start_ary_index1, reward.item);
    int start_ary_index3 = ResultUtility.SetDropData(tmp, start_ary_index2, reward.equipItem);
    int start_ary_index4 = ResultUtility.SetDropData(tmp, start_ary_index3, reward.skillItem);
    int start_ary_index5 = ResultUtility.SetDropData(tmp, start_ary_index4, reward.questItem);
    if (ResultUtility.SetDropData(tmp, start_ary_index5, reward.accessoryItem) == 0 && gold == 0 && crystal == 0)
    {
      if (end_callback == null)
        return;
      end_callback();
    }
    else
    {
      this.eventRewardCallback = end_callback;
      if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
        this.StartCoroutine(this.ExecEndDialogEvent(this.GetSceneName(), (System.Action) (() => this.DispatchEvent("EVENT_REWARD", (object) new object[4]
        {
          (object) tmp,
          (object) gold,
          (object) crystal,
          (object) title
        }))));
      else
        this.DispatchEvent("EVENT_REWARD", (object) new object[4]
        {
          (object) tmp,
          (object) gold,
          (object) crystal,
          (object) title
        });
    }
  }

  protected void OnCloseDialog_QuestResultEventRewardDialog()
  {
    if (this.eventRewardCallback == null)
      return;
    this.StartCoroutine(this.OnCloseEventRewardCoroutine());
  }

  protected IEnumerator OnCloseEventRewardCoroutine()
  {
    if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
      yield return (object) null;
    if (this.eventRewardIndex > this.eventRewardList.Count - 1)
    {
      this.eventRewardCallback();
    }
    else
    {
      this.OpenEventRewardDialog(this.eventRewardList[this.eventRewardIndex], this.eventRewardTitles[this.eventRewardIndex], this.eventRewardCallback);
      ++this.eventRewardIndex;
    }
  }

  private QuestCompleteReward CreateExploreReward()
  {
    if (this.exploreResultData == null)
      return (QuestCompleteReward) null;
    List<PointEventCurrentData.Reward> rewardList = new List<PointEventCurrentData.Reward>();
    List<PointEventCurrentData.PointRewardData> getReward = this.exploreResultData.pointRankingData.getReward;
    for (int index = 0; index < getReward.Count; ++index)
    {
      List<PointEventCurrentData.Reward> reward = getReward[index].reward;
      rewardList.AddRange((IEnumerable<PointEventCurrentData.Reward>) reward);
    }
    string rewardTitle = this.exploreResultData.rewardTitle;
    QuestCompleteReward exploreReward = new QuestCompleteReward();
    for (int index = 0; index < rewardList.Count; ++index)
    {
      switch ((REWARD_TYPE) rewardList[index].type)
      {
        case REWARD_TYPE.CRYSTAL:
          exploreReward.crystal += rewardList[index].num;
          if (!string.IsNullOrEmpty(rewardTitle))
          {
            QuestCompleteReward.EventPrice eventPrice = new QuestCompleteReward.EventPrice();
            eventPrice.rewardTitle = rewardTitle;
            eventPrice.crystal = rewardList[index].num;
            exploreReward.eventPrice.Add(eventPrice);
            break;
          }
          break;
        case REWARD_TYPE.MONEY:
          exploreReward.money += rewardList[index].num;
          if (!string.IsNullOrEmpty(rewardTitle))
          {
            QuestCompleteReward.EventPrice eventPrice = new QuestCompleteReward.EventPrice();
            eventPrice.rewardTitle = rewardTitle;
            eventPrice.gold = rewardList[index].num;
            exploreReward.eventPrice.Add(eventPrice);
            break;
          }
          break;
        case REWARD_TYPE.ITEM:
          QuestCompleteReward.Item obj = new QuestCompleteReward.Item();
          obj.rewardTitle = rewardTitle;
          obj.itemId = rewardList[index].itemId;
          obj.num = rewardList[index].num;
          exploreReward.item.Add(obj);
          break;
        case REWARD_TYPE.EQUIP_ITEM:
          QuestCompleteReward.EquipItem equipItem = new QuestCompleteReward.EquipItem();
          equipItem.rewardTitle = rewardTitle;
          equipItem.equipItemId = rewardList[index].itemId;
          equipItem.num = rewardList[index].num;
          exploreReward.equipItem.Add(equipItem);
          break;
        case REWARD_TYPE.SKILL_ITEM:
          QuestCompleteReward.SkillItem skillItem = new QuestCompleteReward.SkillItem();
          skillItem.rewardTitle = rewardTitle;
          skillItem.skillItemId = rewardList[index].itemId;
          skillItem.num = rewardList[index].num;
          exploreReward.skillItem.Add(skillItem);
          break;
        case REWARD_TYPE.QUEST_ITEM:
          QuestCompleteReward.QuestItem questItem = new QuestCompleteReward.QuestItem();
          questItem.rewardTitle = rewardTitle;
          questItem.questId = rewardList[index].itemId;
          questItem.num = rewardList[index].num;
          exploreReward.questItem.Add(questItem);
          break;
        case REWARD_TYPE.ACCESSORY:
          QuestCompleteReward.AccessoryItem accessoryItem = new QuestCompleteReward.AccessoryItem();
          accessoryItem.rewardTitle = rewardTitle;
          accessoryItem.accessoryId = rewardList[index].itemId;
          accessoryItem.num = rewardList[index].num;
          exploreReward.accessoryItem.Add(accessoryItem);
          break;
      }
    }
    return exploreReward;
  }

  protected bool isNeedOpenFirstClearDialog
  {
    get
    {
      if (MonoBehaviourSingleton<QuestManager>.I.compData == null || MonoBehaviourSingleton<QuestManager>.I.compData.reward == null)
        return false;
      bool firstClearDialog = false;
      QuestCompleteReward first = MonoBehaviourSingleton<QuestManager>.I.compData.reward.first;
      if (first.money > 0 || first.crystal > 0 || first.exp > 0 || first.item.Count > 0 || first.equipItem.Count > 0 || first.skillItem.Count > 0 || first.accessoryItem.Count > 0)
        firstClearDialog = true;
      return firstClearDialog;
    }
  }

  protected void OpenFirstClearRewardDialog(System.Action callback)
  {
    if (!this.isNeedOpenFirstClearDialog)
    {
      if (callback == null)
        return;
      callback();
    }
    else
    {
      this.animState = QuestResultTop.RESULT_ANIM_STATE.FIRST_CLEAR_REWARD;
      this.firstClearRewardCallback = callback;
      QuestCompleteReward first = MonoBehaviourSingleton<QuestManager>.I.compData.reward.first;
      List<SortCompareData> tmp = new List<SortCompareData>();
      int start_ary_index1 = 0;
      int gold = first.money;
      int crystal = first.crystal;
      int start_ary_index2 = ResultUtility.SetDropData(tmp, start_ary_index1, first.item);
      int start_ary_index3 = ResultUtility.SetDropData(tmp, start_ary_index2, first.equipItem);
      int start_ary_index4 = ResultUtility.SetDropData(tmp, start_ary_index3, first.skillItem);
      ResultUtility.SetDropData(tmp, start_ary_index4, first.accessoryItem);
      if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
        this.StartCoroutine(this.ExecEndDialogEvent(this.GetSceneName(), (System.Action) (() => this.DispatchEvent("FIRST_CLEAR_REWARD", (object) new object[3]
        {
          (object) tmp,
          (object) gold,
          (object) crystal
        }))));
      else
        this.DispatchEvent("FIRST_CLEAR_REWARD", (object) new object[3]
        {
          (object) tmp,
          (object) gold,
          (object) crystal
        });
    }
  }

  protected void OnCloseDialog_QuestResultFirstClearRewardDialog()
  {
    if (this.firstClearRewardCallback == null)
      return;
    this.StartCoroutine(this.OnCloseFirstClearCoroutine());
  }

  protected IEnumerator OnCloseFirstClearCoroutine()
  {
    if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
      yield return (object) null;
    if (this.firstClearRewardCallback != null)
      this.firstClearRewardCallback();
  }

  public static bool IsExecuteNowSceneEvent(string section_name)
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == section_name && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible();
  }

  protected IEnumerator ExecEndDialogEvent(string section_name, System.Action callback)
  {
    if (!QuestResultTop.IsExecuteNowSceneEvent(section_name))
      yield return (object) null;
    callback();
  }

  private void PlayAudio(QuestResultTop.AUDIO type) => SoundManager.PlayOneShotUISE((int) type);

  protected virtual string GetSceneName() => nameof (QuestResultTop);

  protected void OnQuery_LEAVE_HUNT()
  {
    this.StopCoroutine("AutoJoinParty");
    this.StopCoroutine("WaitForHost");
    this.DispatchEvent("FRIEND");
  }

  protected void OnQuery_END_HUNT()
  {
    this.StopCoroutine("WaitForRepeatHunt");
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendRepeat(false, (Action<bool>) (is_success =>
    {
      GameSection.ChangeStayEvent("FRIEND");
      GameSection.ResumeEvent(is_success);
    }));
  }

  protected void OnQuery_REPEAT_HUNT()
  {
    this.StopCoroutine("WaitForRepeatHunt");
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
    bool is_free_join = true;
    if (questData.questType == QUEST_TYPE.EVENT)
      is_free_join = !MonoBehaviourSingleton<PartyManager>.I.IsPayingQuest();
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID, is_free_join);
    MonoBehaviourSingleton<InGameProgress>.I.QuestRepeat();
  }

  private IEnumerator WaitForHost()
  {
    this.canEnterParty = false;
    for (int wait = 10; wait > 0; --wait)
    {
      this.SetLabelText((Enum) QuestResultTop.UI.LBL_WAIT_FOR_HOST, $"Wait For Host({wait}s)");
      yield return (object) new WaitForSeconds(1f);
    }
    yield return (object) null;
    this.SetLabelText((Enum) QuestResultTop.UI.LBL_WAIT_FOR_HOST, "Wait For Host...");
    this.canEnterParty = true;
  }

  private IEnumerator AutoJoinParty()
  {
    bool wait = true;
    bool waitGetData = false;
    while (MonoBehaviourSingleton<PartyManager>.I.repeatPartyStatus == 0)
    {
      if (!waitGetData)
      {
        waitGetData = true;
        MonoBehaviourSingleton<PartyManager>.I.SendGetNextParty((Action<bool>) (is_success => waitGetData = false));
      }
      else
        yield return (object) new WaitForSeconds(2f);
    }
    if (MonoBehaviourSingleton<PartyManager>.I.repeatPartyStatus >= 0)
    {
      MonoBehaviourSingleton<PartyManager>.I.SendEntry(MonoBehaviourSingleton<PartyManager>.I.partyData.id, false, (Action<bool>) (is_success => wait = false));
      while (wait)
        yield return (object) null;
      wait = true;
      MonoBehaviourSingleton<PartyManager>.I.SendReady(true, (Action<bool>) (is_success => wait = false));
      while (wait)
        yield return (object) null;
      wait = true;
      waitGetData = false;
      while (wait)
      {
        if (!waitGetData)
        {
          waitGetData = true;
          MonoBehaviourSingleton<PartyManager>.I.SendInfo((Action<bool>) (is_success =>
          {
            waitGetData = false;
            if (PartyManager.IsValidInParty())
            {
              if (MonoBehaviourSingleton<PartyManager>.I.partyData.status == 100)
              {
                wait = false;
              }
              else
              {
                if (MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest)
                  return;
                wait = false;
              }
            }
            else
              wait = false;
          }));
          yield return (object) null;
        }
        else
          yield return (object) new WaitForSeconds(2f);
      }
      while (!this.canEnterParty)
      {
        if (!PartyManager.IsValidInParty())
          this.canEnterParty = true;
        else if (MonoBehaviourSingleton<PartyManager>.I.partyData.status != 100 && !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest)
          this.canEnterParty = true;
        yield return (object) null;
      }
      if (PartyManager.IsValidInParty())
      {
        if (MonoBehaviourSingleton<PartyManager>.I.partyData.status == 100)
          this.EnterPartyQuest();
        else if (!MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest)
          this.DispatchEvent("HOST_LEFT");
      }
      else
        this.DispatchEvent("HOST_LEFT");
    }
  }

  private IEnumerator WaitForRepeatHunt()
  {
    for (int wait = 5; wait > 0; --wait)
    {
      this.SetLabelText((Enum) QuestResultTop.UI.LBL_BTN_REPEAT_HUNT, $"Repeat({wait}s)");
      yield return (object) new WaitForSeconds(1f);
    }
    this.SetLabelText((Enum) QuestResultTop.UI.LBL_BTN_REPEAT_HUNT, "Repeat(0s)");
    yield return (object) null;
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
    bool is_free_join = true;
    if (questData.questType == QUEST_TYPE.EVENT)
      is_free_join = !MonoBehaviourSingleton<PartyManager>.I.IsPayingQuest();
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID, is_free_join);
    MonoBehaviourSingleton<InGameProgress>.I.QuestRepeat();
  }

  private void EnterPartyQuest()
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
    bool is_free_join = true;
    if (questData.questType == QUEST_TYPE.EVENT)
      is_free_join = !MonoBehaviourSingleton<PartyManager>.I.IsPayingQuest();
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID, is_free_join);
    MonoBehaviourSingleton<InGameProgress>.I.QuestRepeat();
  }

  private enum UI
  {
    LBL_QUEST_NAME,
    LBL_PLAYER_LV,
    LBL_PLAYER_LVUP,
    SPR_LEVELUP,
    LBL_LVUP_NUM,
    OBJ_GET_EXP_ROOT,
    OBJ_MISSION_ROOT,
    OBJ_MISSION_NEW_CLEAR_ROOT,
    OBJ_TREASURE_ROOT,
    STR_TITLE_EXP,
    STR_TITLE_MISSION,
    STR_TITLE_REWARD,
    OBJ_EXP_REWARD_FRAME,
    LBL_EXP,
    SPR_GAUGE_UPPER,
    PBR_EXP,
    OBJ_RESULT_EXP_GAUGE_CTRL,
    OBJ_MISSION_INFO_FRAME,
    OBJ_MISSION_01,
    OBJ_MISSION_02,
    OBJ_MISSION_03,
    LBL_MISSION_NAME_01,
    LBL_MISSION_NAME_02,
    LBL_MISSION_NAME_03,
    SPR_CROWN_01,
    SPR_CROWN_02,
    SPR_CROWN_03,
    SPR_CLEARED_CROWN_01,
    SPR_CLEARED_CROWN_02,
    SPR_CLEARED_CROWN_03,
    STR_EMPTY_MISSION,
    GET_ITEM,
    GET_ITEM_2,
    OBJ_QUEST_REWARD_FRAME,
    LBL_REWARD_GOLD,
    TBL_ITEM,
    OBJ_SCROLL_VIEW,
    OBJ_SCROLL_VIEW_2,
    GRD_DROP_ITEM,
    GRD_DROP_ITEM_2,
    BTN_NEXT,
    BTN_SKIP_FULL_SCREEN,
    BTN_SKIP_IN_SCROLL,
    BTN_SKIP_IN_SCROLL_2,
    PNL_MATERIAL_INFO,
    PNL_MATERIAL_INFO_2,
    OBJ_TREASURE_ROOT_NON_MISSION,
    OBJ_POINT_SHOP_RESULT_ROOT,
    OBJ_NORMAL_POINT_SHOP_ROOT,
    OBJ_EVENT_POINT_SHOP_ROOT,
    LBL_NORMAL_GET_POINT_SHOP,
    LBL_NORMAL_TOTAL_POINT_SHOP,
    TEX_NORMAL_POINT_SHOP_ICON,
    LBL_EVENT_GET_POINT_SHOP,
    LBL_EVENT_TOTAL_POINT_SHOP,
    TEX_EVENT_POINT_SHOP_ICON,
    LBL_GUILD_REQUEST_GET_POINT,
    OBJ_TITLE,
    OBJ_WAVE,
    LBL_WAVE,
    OBJ_TIME,
    LBL_TIME,
    OBJ_MONEY,
    OBJ_COIN,
    OBJ_ARRIVAL_EFFECT_ROOT,
    OBJ_ARRIVAL_EFFECT,
    OBJ_ARRIVAL_BONUS,
    GRD_ARRIVAL_ITEM_ICON,
    STR_REWARD_TITLE,
    SPR_WAVE_01,
    SPR_WAVE_10,
    SPR_WAVE_100,
    TBL_DROP_ITEM,
    LBL_DROP_ITEM_WAVE,
    STR_TITLE_WAVE,
    STR_TITLE_TIME,
    LBL_EXPLORE_GET_POINT,
    LBL_EXPLORE_TOTAL_POINT,
    SPR_TITLE,
    OBJ_EXP,
    OBJ_REMAIN_TIME,
    OBJ_CLEAR_TIME,
    STR_CLEAR_TIME_NAME,
    LBL_CLEAR_TIME,
    OBJ_BEFORE_TIME,
    SPR_BEFORE_TIME_NAME,
    LBL_BEFORE_TIME,
    SPR_BESTSCORE,
    OBJ_CLEAR_EFFECT_ROOT,
    OBJ_CLEAR_EFFECT,
    OBJ_RANK_UP_ROOT,
    OBJ_RANK_UP,
    TEX_RANK_PRE,
    TEX_RANK_NEW,
    OBJ_PARTICLE,
    OBJ_CONGRATULATIONS_ROOT,
    OBJ_CONGRATULATIONS,
    OBJ_CONGRATULATIONS_PARTICLE,
    SHADOW,
    LBL_BOSS_NAME,
    TBL_GUILD_REQUEST_RESULT,
    OBJ_BONUS_POINT_SHOP,
    TXT_BONUS_POINT_ICON,
    LBL_BONUS_POINT_NUM,
    TEX_MISSION_COIN_01,
    TEX_MISSION_COIN_02,
    TEX_MISSION_COIN_03,
    SPR_CROWN01_OFF,
    SPR_CROWN02_OFF,
    SPR_CROWN03_OFF,
    BTN_NEXT_ALL,
    BTN_END_HUNT_CENTER,
    BTN_END_HUNT_LEFT,
    BTN_REPEAT_HUNT,
    LBL_BTN_REPEAT_HUNT,
    LBL_WAIT_FOR_HOST,
  }

  private enum AUDIO
  {
    COUNTUP = 40000012, // 0x02625A0C
    MISSION = 40000013, // 0x02625A0D
    DROPITEM_BREAK = 40000014, // 0x02625A0E
    DROPITEM = 40000015, // 0x02625A0F
    MONEY = 40000016, // 0x02625A10
    LEVELUP = 40000017, // 0x02625A11
    ADVENT = 40000026, // 0x02625A1A
    MONEY_WH = 40000027, // 0x02625A1B
    ACHIEVEMENT = 40000028, // 0x02625A1C
    RESULT = 40000049, // 0x02625A31
    GET_REWARD = 40000155, // 0x02625A9B
    TITLE_LOGO = 40000227, // 0x02625AE3
    CATEGORY = 40000228, // 0x02625AE4
    POINTUP = 40000229, // 0x02625AE5
    POINTREWARD = 40000230, // 0x02625AE6
    START_BONUS_TIME = 40000268, // 0x02625B0C
  }

  protected enum RESULT_ANIM_STATE
  {
    WAIT,
    EXP_GAUGE,
    LVUP,
    MISSION,
    MISSION_REWARD,
    FOLLOW_BONUS,
    TREASURE,
    END,
    FIRST_CLEAR_REWARD,
  }
}
