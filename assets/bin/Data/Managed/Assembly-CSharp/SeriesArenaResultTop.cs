// Decompiled with JetBrains decompiler
// Type: SeriesArenaResultTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SeriesArenaResultTop : QuestResultTop
{
  private const float COUNT_ANIM_SPEED = 4f;
  private bool isSkip;
  private bool isNext;
  private ResultReward[] resultRewards;
  private PointEventCurrentData allPointEvents;
  private ARENA_RANK preRank;
  private ARENA_RANK newRank;
  private SeriesArenaResultTop.RESULT_ANIM_STATE animState;

  public override void Initialize()
  {
    base.Initialize();
    if (MonoBehaviourSingleton<QuestManager>.I.compData != null)
    {
      this.preRank = (ARENA_RANK) MonoBehaviourSingleton<QuestManager>.I.compData.seriesArena.beforeRank;
      this.newRank = (ARENA_RANK) MonoBehaviourSingleton<QuestManager>.I.compData.seriesArena.afterRank;
      if (this.preRank != this.newRank)
      {
        ResourceLoad.LoadWithSetUITexture(((Component) this.GetCtrl((Enum) SeriesArenaResultTop.UI.TEX_RANK_PRE)).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(this.preRank));
        ResourceLoad.LoadWithSetUITexture(((Component) this.GetCtrl((Enum) SeriesArenaResultTop.UI.TEX_RANK_NEW)).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(this.newRank));
      }
    }
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
    MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
  }

  protected override void InitReward()
  {
    base.InitReward();
    if (this.eventRewardTitles == null)
      this.eventRewardTitles = new List<string>();
    if (this.eventReward == null)
      this.eventReward = new QuestCompleteReward();
    if (this.pointShopResultData != null)
      return;
    this.pointShopResultData = new List<PointShopResultData>();
  }

  public override void UpdateUI()
  {
    this.allPointEvents = new PointEventCurrentData();
    this.allPointEvents.pointRankingData = new PointEventCurrentData.PointResultData();
    this.SetFullScreenButton((Enum) SeriesArenaResultTop.UI.BTN_SKIP_FULL_SCREEN);
    this.SetActive((Enum) SeriesArenaResultTop.UI.BTN_NEXT_ONLY, false);
    this.SetActive((Enum) SeriesArenaResultTop.UI.BTN_NEXT_ALL, false);
    this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_TIME, false);
    this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_CLEAR_EFFECT_ROOT, false);
    this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_CLEAR_EFFECT, false);
    this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_RANK_UP_ROOT, false);
    this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_CONGRATULATIONS_ROOT, false);
    if (!this.isVictory)
    {
      Transform ctrl = this.GetCtrl((Enum) SeriesArenaResultTop.UI.OBJ_MONEY);
      ctrl.localPosition = new Vector3(ctrl.localPosition.x, 0.0f, ctrl.localPosition.z);
    }
    this.SetLabelText((Enum) SeriesArenaResultTop.UI.LBL_QUEST_NAME, Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).questText);
    int num1 = 0;
    int num2 = 0;
    if (this.isVictory)
    {
      QuestCompleteRewardList reward = MonoBehaviourSingleton<QuestManager>.I.compData.reward;
      QuestCompleteReward breakReward = reward.breakReward;
      QuestCompleteReward order = reward.order;
      num2 = this.dropReward.money + breakReward.money + order.money;
      num1 = this.dropReward.exp + breakReward.exp + order.exp;
    }
    this.SetLabelText((Enum) SeriesArenaResultTop.UI.LBL_EXP, num1.ToString("N0"));
    this.SetLabelText((Enum) SeriesArenaResultTop.UI.LBL_REWARD_GOLD, num2.ToString("N0"));
    this.SetLabelText((Enum) SeriesArenaResultTop.UI.LBL_TIME, MonoBehaviourSingleton<InGameRecorder>.I.arenaRemainTimeToString);
    this.SetGrid((Enum) SeriesArenaResultTop.UI.GRD_DROP_ITEM, (string) null, this.dropItemIconData.Length, true, (Action<int, Transform, bool>) ((i, o, is_recycle) =>
    {
      ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
      RARITY_TYPE? rarity = new RARITY_TYPE?();
      ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
      EQUIPMENT_TYPE? magi_enable_icon_type = new EQUIPMENT_TYPE?();
      int icon_id = -1;
      int num3 = -1;
      if (i < this.dropItemIconData.Length && this.dropItemIconData[i] != null)
      {
        itemIconType = this.dropItemIconData[i].GetIconType();
        icon_id = this.dropItemIconData[i].GetIconID();
        rarity = new RARITY_TYPE?(this.dropItemIconData[i].GetRarity());
        element = this.dropItemIconData[i].GetIconElement();
        magi_enable_icon_type = this.dropItemIconData[i].GetIconMagiEnableType();
        num3 = this.dropItemIconData[i].GetNum();
        if (num3 == 1)
          num3 = -1;
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
            _icon = ItemIcon.Create(itemIconType, icon_id, rarity, o, element, magi_enable_icon_type, num3, "DROP", i, is_new, enemy_icon_id: enemy_icon_id, enemy_icon_id2: enemy_icon_id2, getType: this.dropItemIconData[i].GetGetType());
          _icon.SetRewardBG(true);
          _icon.SetRewardCategoryInfo(this.dropItemIconData[i].GetCategory());
          this.SetMaterialInfo(_icon.transform, this.dropItemIconData[i].GetMaterialType(), this.dropItemIconData[i].GetTableID(), this.GetCtrl((Enum) SeriesArenaResultTop.UI.PNL_MATERIAL_INFO));
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
            this.SetVisibleWidgetOneShotEffect(this.GetCtrl((Enum) SeriesArenaResultTop.UI.OBJ_SCROLL_VIEW), t, ui_effect_name);
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
    this.SetLabelText((Enum) SeriesArenaResultTop.UI.LBL_CLEAR_TIME, InGameProgress.GetSeriesArenaTimeWithMilliSecToString(0.0f));
    this.SetActive((Enum) SeriesArenaResultTop.UI.SPR_BESTSCORE, false);
    if (this.isVictory)
      this.SetLabelText((Enum) SeriesArenaResultTop.UI.LBL_BEFORE_TIME, InGameProgress.GetSeriesArenaTimeWithMilliSecToString((float) MonoBehaviourSingleton<QuestManager>.I.compData.seriesArena.prevClearTime * (1f / 1000f)));
    bool is_visible1 = this.pointShopResultData.Count > 0;
    this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT, is_visible1);
    if (is_visible1)
      this.SetGrid((Enum) SeriesArenaResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT, "QuestResultPointShop", this.pointShopResultData.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
      {
        this.ResetTween(t);
        PointShopResultData pointShopResultData = this.pointShopResultData[i];
        this.SetActive(t, (Enum) SeriesArenaResultTop.UI.OBJ_NORMAL_POINT_SHOP_ROOT, !pointShopResultData.isEvent);
        if (!pointShopResultData.isEvent)
        {
          this.SetLabelText(t, (Enum) SeriesArenaResultTop.UI.LBL_NORMAL_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.getPoint));
          this.SetLabelText(t, (Enum) SeriesArenaResultTop.UI.LBL_NORMAL_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.totalPoint));
          ResourceLoad.LoadPointIconImageTexture(((Component) this.FindCtrl(t, (Enum) SeriesArenaResultTop.UI.TEX_NORMAL_POINT_SHOP_ICON)).GetComponent<UITexture>(), (uint) pointShopResultData.pointShopId);
        }
        this.SetActive(t, (Enum) SeriesArenaResultTop.UI.OBJ_EVENT_POINT_SHOP_ROOT, pointShopResultData.isEvent);
        if (!pointShopResultData.isEvent)
          return;
        this.SetLabelText(t, (Enum) SeriesArenaResultTop.UI.LBL_EVENT_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.getPoint));
        this.SetLabelText(t, (Enum) SeriesArenaResultTop.UI.LBL_EVENT_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.totalPoint));
        ResourceLoad.LoadPointIconImageTexture(((Component) this.FindCtrl(t, (Enum) SeriesArenaResultTop.UI.TEX_EVENT_POINT_SHOP_ICON)).GetComponent<UITexture>(), (uint) pointShopResultData.pointShopId);
      }));
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
    {
      UIVirtualScreen componentInChildren = ((Component) this).GetComponentInChildren<UIVirtualScreen>();
      UIWidget component = ((Component) this.GetCtrl((Enum) SeriesArenaResultTop.UI.SHADOW)).GetComponent<UIWidget>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null) && Object.op_Inequality((Object) component, (Object) null))
      {
        component.width = (int) componentInChildren.ScreenWidthFull;
        component.height = (int) componentInChildren.ScreenHeightFull;
      }
    }
    if (MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag != null)
      this.missionNewClear = MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag.ToArray();
    SeriesArenaResultTop.UI[] uiArray1 = new SeriesArenaResultTop.UI[3]
    {
      SeriesArenaResultTop.UI.OBJ_MISSION_01,
      SeriesArenaResultTop.UI.OBJ_MISSION_02,
      SeriesArenaResultTop.UI.OBJ_MISSION_03
    };
    SeriesArenaResultTop.UI[] uiArray2 = new SeriesArenaResultTop.UI[3]
    {
      SeriesArenaResultTop.UI.LBL_MISSION_NAME_01,
      SeriesArenaResultTop.UI.LBL_MISSION_NAME_02,
      SeriesArenaResultTop.UI.LBL_MISSION_NAME_03
    };
    SeriesArenaResultTop.UI[] uiArray3 = new SeriesArenaResultTop.UI[3]
    {
      SeriesArenaResultTop.UI.SPR_CROWN_01,
      SeriesArenaResultTop.UI.SPR_CROWN_02,
      SeriesArenaResultTop.UI.SPR_CROWN_03
    };
    SeriesArenaResultTop.UI[] uiArray4 = new SeriesArenaResultTop.UI[3]
    {
      SeriesArenaResultTop.UI.SPR_CLEARED_CROWN_01,
      SeriesArenaResultTop.UI.SPR_CLEARED_CROWN_02,
      SeriesArenaResultTop.UI.SPR_CLEARED_CROWN_03
    };
    SeriesArenaResultTop.UI[] uiArray5 = new SeriesArenaResultTop.UI[3]
    {
      SeriesArenaResultTop.UI.TEX_MISSION_COIN_01,
      SeriesArenaResultTop.UI.TEX_MISSION_COIN_02,
      SeriesArenaResultTop.UI.TEX_MISSION_COIN_03
    };
    SeriesArenaResultTop.UI[] uiArray6 = new SeriesArenaResultTop.UI[3]
    {
      SeriesArenaResultTop.UI.SPR_CROWN01_OFF,
      SeriesArenaResultTop.UI.SPR_CROWN02_OFF,
      SeriesArenaResultTop.UI.SPR_CROWN03_OFF
    };
    QuestInfoData.Mission[] missionData = QuestInfoData.CreateMissionData(Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID));
    for (int index = 0; index < 3; ++index)
    {
      bool is_visible2 = missionData[index] != null;
      this.SetActive((Enum) uiArray1[index], is_visible2);
      if (is_visible2)
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
    this.StartCoroutine(this.PlayAnimation());
  }

  private void PlayAudio(SeriesArenaResultTop.AUDIO type)
  {
    int se_id = (int) type;
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    SoundManager.PlayOneShotUISE(se_id);
  }

  private IEnumerator PlayAnimation()
  {
    this.isSkip = false;
    this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.TITLE;
    this.PlayAudio(SeriesArenaResultTop.AUDIO.ADVENT);
    bool isTitleEnd = false;
    this.PlayTween((Enum) SeriesArenaResultTop.UI.OBJ_TITLE, callback: (EventDelegate.Callback) (() => isTitleEnd = true), is_input_block: false);
    yield return (object) new WaitWhile((Func<bool>) (() => !isTitleEnd && !this.isSkip));
    this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.DROP;
    this.PlayAudio(SeriesArenaResultTop.AUDIO.ACHIEVEMENT);
    if (this.pointShopResultData.Count > 0)
    {
      foreach (Transform t in ((Component) this.GetCtrl((Enum) SeriesArenaResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT)).transform)
        this.PlayTween(t);
    }
    this.PlayTween((Enum) SeriesArenaResultTop.UI.OBJ_EXP);
    if (this.isVictory)
    {
      this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.MISSION;
      bool isMissionEnd = false;
      if (!this.isValidMissionNewClearAnim)
      {
        this.PlayTween((Enum) SeriesArenaResultTop.UI.OBJ_MISSION_ROOT, callback: (EventDelegate.Callback) (() => this.OpenMissionClearRewardDialog((System.Action) (() => isMissionEnd = true))));
      }
      else
      {
        this.PlayTween((Enum) SeriesArenaResultTop.UI.OBJ_MISSION_ROOT, is_input_block: false);
        this.PlayTween((Enum) SeriesArenaResultTop.UI.OBJ_MISSION_NEW_CLEAR_ROOT, callback: (EventDelegate.Callback) (() => this.OpenMissionClearRewardDialog((System.Action) (() => isMissionEnd = true))), is_input_block: false);
      }
      yield return (object) new WaitUntil((Func<bool>) (() => isMissionEnd));
    }
    bool isMoneyEnd = false;
    this.PlayTween((Enum) SeriesArenaResultTop.UI.OBJ_MONEY, callback: (EventDelegate.Callback) (() => isMoneyEnd = true), is_input_block: false);
    yield return (object) new WaitWhile((Func<bool>) (() => !isMoneyEnd && !this.isSkip));
    if (this.isVictory)
    {
      this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.TREASURE;
      this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_TREASURE_ROOT, true);
      bool isTreasureEnd = false;
      this.PlayTween((Enum) SeriesArenaResultTop.UI.OBJ_TREASURE_ROOT, callback: (EventDelegate.Callback) (() => isTreasureEnd = true), is_input_block: false);
      yield return (object) new WaitWhile((Func<bool>) (() => !isTreasureEnd && !this.isSkip));
      int dropIndex = 0;
      float dropAnimTime = 0.0f;
      yield return (object) new WaitWhile((Func<bool>) (() =>
      {
        dropAnimTime += Time.deltaTime;
        if ((double) dropAnimTime > 0.40000000596046448 || this.isSkip)
        {
          dropAnimTime = 0.0f;
          this.VisibleItemIcon(dropIndex, this.isSkip);
          if (dropIndex >= 5 && dropIndex % 5 == 0)
            this.SetScroll((Enum) SeriesArenaResultTop.UI.OBJ_SCROLL_VIEW, this.animScrollValue);
          ++dropIndex;
          if (dropIndex >= this.dropItemNum)
            return false;
        }
        return true;
      }));
      this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.TREASURE_END;
      this.isNext = false;
      this.VisibleEndButton();
      yield return (object) new WaitUntil((Func<bool>) (() => this.isNext));
      this.isSkip = false;
      this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_TREASURE_ROOT, false);
      this.InvisibleEndButton();
      if (this.preRank != this.newRank)
      {
        this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_RANK_UP_ROOT, true);
        this.ResetTween((Enum) SeriesArenaResultTop.UI.OBJ_RANK_UP);
        this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.CLEAR_EFFECT;
        ((Renderer) ((Component) ((Component) this.GetCtrl((Enum) SeriesArenaResultTop.UI.OBJ_PARTICLE)).GetComponent<ParticleSystem>()).GetComponent<ParticleSystemRenderer>()).sharedMaterial.renderQueue = 4000;
        yield return (object) null;
        this.PlayAudio(SeriesArenaResultTop.AUDIO.ARRIVAL);
        bool isRankUpEnd = false;
        this.PlayTween((Enum) SeriesArenaResultTop.UI.OBJ_RANK_UP, callback: (EventDelegate.Callback) (() => isRankUpEnd = true));
        yield return (object) new WaitWhile((Func<bool>) (() => !isRankUpEnd && !this.isSkip));
        for (float waitTime = 2.5f; (double) waitTime > 0.0 && !this.isSkip; waitTime -= Time.deltaTime)
          yield return (object) null;
        this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_RANK_UP_ROOT, false);
      }
      this.SetActive((Enum) SeriesArenaResultTop.UI.OBJ_TIME, true);
      this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.CLEAR_TIME_COUNT_UP;
      bool isTimeEnd = false;
      this.StartCoroutine(this.PlayCountUpClearTimeAnim(MonoBehaviourSingleton<InGameRecorder>.I.arenaElapsedTime, (System.Action) (() => isTimeEnd = true)));
      yield return (object) new WaitWhile((Func<bool>) (() => !isTimeEnd && !this.isSkip));
      if (this.IsBreakRecord())
      {
        this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.BEST_SCORE;
        this.PlayAudio(SeriesArenaResultTop.AUDIO.ARRIVAL);
        this.SetActive((Enum) SeriesArenaResultTop.UI.SPR_BESTSCORE, true);
        bool isBestScoreEnd = false;
        this.PlayTween((Enum) SeriesArenaResultTop.UI.SPR_BESTSCORE, callback: (EventDelegate.Callback) (() => isBestScoreEnd = true));
        yield return (object) new WaitWhile((Func<bool>) (() => !isBestScoreEnd && !this.isSkip));
      }
    }
    this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.EVENT;
    this.OpenAllEventRewardDialog((System.Action) (() => this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.IDLE));
    yield return (object) new WaitWhile((Func<bool>) (() => this.animState != SeriesArenaResultTop.RESULT_ANIM_STATE.IDLE && !this.isSkip));
    this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.END;
    this.VisibleEndButton();
  }

  private IEnumerator PlayCountUpClearTimeAnim(float targetTime, System.Action callBack)
  {
    float currentShowTime = 0.0f;
    while ((double) currentShowTime < (double) targetTime)
    {
      yield return (object) null;
      if (this.isSkip)
        currentShowTime = targetTime;
      int num1 = Mathf.FloorToInt(currentShowTime);
      currentShowTime += Mathf.Max((targetTime - currentShowTime) * this.CountDownCube(Time.deltaTime * 4f), 1f);
      currentShowTime = Mathf.Min(currentShowTime, targetTime);
      int num2 = Mathf.FloorToInt(currentShowTime);
      if (num1 < num2)
        SoundManager.PlayOneShotUISE(40000012);
      this.SetLabelText((Enum) SeriesArenaResultTop.UI.LBL_CLEAR_TIME, InGameProgress.GetSeriesArenaTimeWithMilliSecToString((float) Math.Round((double) currentShowTime, 2, MidpointRounding.AwayFromZero)));
    }
    if (callBack != null)
      callBack();
  }

  private float CountDownCube(float currentValue) => currentValue * (2f - currentValue);

  private void InvisibleEndButton()
  {
    this.SetActive((Enum) SeriesArenaResultTop.UI.BTN_NEXT_ONLY, false);
    this.SetActive((Enum) SeriesArenaResultTop.UI.BTN_NEXT_ALL, false);
    this.SetActive((Enum) SeriesArenaResultTop.UI.BTN_SKIP_FULL_SCREEN, true);
  }

  protected override void VisibleEndButton()
  {
    bool is_visible1 = this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.TREASURE_END;
    bool is_visible2 = this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.END;
    this.SetActive((Enum) SeriesArenaResultTop.UI.BTN_NEXT_ONLY, is_visible1);
    this.SetActive((Enum) SeriesArenaResultTop.UI.BTN_NEXT_ALL, is_visible2);
    this.SetActive((Enum) SeriesArenaResultTop.UI.BTN_SKIP_FULL_SCREEN, !is_visible1 && !is_visible2);
  }

  private bool IsBreakRecord()
  {
    return MonoBehaviourSingleton<InGameRecorder>.IsValid() && MonoBehaviourSingleton<QuestManager>.IsValid() && Mathf.FloorToInt(MonoBehaviourSingleton<InGameRecorder>.I.arenaElapsedTime * 1000f) < MonoBehaviourSingleton<QuestManager>.I.compData.seriesArena.prevClearTime;
  }

  private void DevideRewardDropAndEvent(ResultReward resultReward, QuestCompleteReward reward)
  {
    resultReward.dropReward = new QuestCompleteReward();
    resultReward.eventReward = new QuestCompleteReward();
    List<string> stringList = new List<string>();
    resultReward.dropReward.exp = reward.exp;
    int num1 = 0;
    int num2 = 0;
    for (int index = 0; index < reward.eventPrice.Count; ++index)
    {
      num1 += reward.eventPrice[index].gold;
      num2 += reward.eventPrice[index].gold;
      resultReward.eventReward.eventPrice.Add(reward.eventPrice[index]);
      stringList.Add(reward.eventPrice[index].rewardTitle);
    }
    resultReward.dropReward.money = Mathf.Max(0, reward.money - num1);
    resultReward.dropReward.crystal = Mathf.Max(0, reward.crystal - num2);
    for (int index = 0; index < reward.item.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.item[index].rewardTitle))
      {
        resultReward.dropReward.item.Add(reward.item[index]);
      }
      else
      {
        resultReward.eventReward.item.Add(reward.item[index]);
        stringList.Add(reward.item[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.skillItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.skillItem[index].rewardTitle))
      {
        resultReward.dropReward.skillItem.Add(reward.skillItem[index]);
      }
      else
      {
        resultReward.eventReward.skillItem.Add(reward.skillItem[index]);
        stringList.Add(reward.skillItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.equipItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.equipItem[index].rewardTitle))
      {
        resultReward.dropReward.equipItem.Add(reward.equipItem[index]);
      }
      else
      {
        resultReward.eventReward.equipItem.Add(reward.equipItem[index]);
        stringList.Add(reward.equipItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.questItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.questItem[index].rewardTitle))
      {
        resultReward.dropReward.questItem.Add(reward.questItem[index]);
      }
      else
      {
        resultReward.eventReward.questItem.Add(reward.questItem[index]);
        stringList.Add(reward.questItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.accessoryItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.accessoryItem[index].rewardTitle))
      {
        resultReward.dropReward.accessoryItem.Add(reward.accessoryItem[index]);
      }
      else
      {
        resultReward.eventReward.accessoryItem.Add(reward.accessoryItem[index]);
        stringList.Add(reward.accessoryItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < stringList.Count; ++index)
    {
      if (!this.eventRewardTitles.Contains(stringList[index]))
        this.eventRewardTitles.Add(stringList[index]);
    }
  }

  private void OpenMissionClearRewardDialog(System.Action end_callback)
  {
    bool flag = this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.MISSION || this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.END;
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
        if (this.animState < SeriesArenaResultTop.RESULT_ANIM_STATE.END)
          this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.MISSION_REWARD;
      }
      else
      {
        this.isOpenedMissionClearDialog = true;
        if (this.animState < SeriesArenaResultTop.RESULT_ANIM_STATE.END)
          this.animState = SeriesArenaResultTop.RESULT_ANIM_STATE.MISSION_REWARD;
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
    Debug.LogFormat(nameof (OnCloseDialog_QuestResultMissionClearRewardDialog), Array.Empty<object>());
    if (this.missionClearRewardCallback == null)
      return;
    this.StartCoroutine(this.OnCloseMissionClearCoroutine());
  }

  private IEnumerator OnCloseMissionClearCoroutine()
  {
    Debug.LogFormat(nameof (OnCloseMissionClearCoroutine), Array.Empty<object>());
    if (!QuestResultTop.IsExecuteNowSceneEvent(this.GetSceneName()))
      yield return (object) null;
    this.OpenMissionClearRewardDialog(this.missionClearRewardCallback);
  }

  private void OnQuery_SKIP()
  {
    switch (this.animState)
    {
      case SeriesArenaResultTop.RESULT_ANIM_STATE.TITLE:
      case SeriesArenaResultTop.RESULT_ANIM_STATE.DROP:
      case SeriesArenaResultTop.RESULT_ANIM_STATE.MISSION:
      case SeriesArenaResultTop.RESULT_ANIM_STATE.TREASURE:
        this.SkipTween((Enum) SeriesArenaResultTop.UI.OBJ_TITLE);
        this.SkipTween((Enum) SeriesArenaResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT);
        this.SkipTween((Enum) SeriesArenaResultTop.UI.OBJ_EXP);
        this.SkipTween((Enum) SeriesArenaResultTop.UI.OBJ_MONEY);
        if (this.isVictory)
        {
          this.SkipTween((Enum) SeriesArenaResultTop.UI.OBJ_MISSION_ROOT);
          this.SkipTween((Enum) SeriesArenaResultTop.UI.OBJ_MISSION_NEW_CLEAR_ROOT);
          this.SkipTween((Enum) SeriesArenaResultTop.UI.OBJ_TREASURE_ROOT);
          break;
        }
        break;
      case SeriesArenaResultTop.RESULT_ANIM_STATE.REMAIN_TIME:
        this.SkipTween((Enum) SeriesArenaResultTop.UI.OBJ_REMAIN_TIME);
        break;
      case SeriesArenaResultTop.RESULT_ANIM_STATE.CLEAR_EFFECT:
        this.SkipTween((Enum) SeriesArenaResultTop.UI.OBJ_RANK_UP);
        break;
      case SeriesArenaResultTop.RESULT_ANIM_STATE.BEST_SCORE:
        this.SkipTween((Enum) SeriesArenaResultTop.UI.SPR_BESTSCORE);
        break;
    }
    this.isSkip = true;
    GameSection.StopEvent();
  }

  private void OnQuery_NEXT()
  {
    if (this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.END)
      this.ToSeriesArena();
    else if (this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.TREASURE_END)
    {
      this.isNext = true;
    }
    else
    {
      if (this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.IDLE)
        return;
      this.OnQuery_SKIP();
    }
  }

  private void OnQuery_RETRY()
  {
    if (this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.END)
    {
      if (MonoBehaviourSingleton<CoopManager>.IsValid())
        MonoBehaviourSingleton<CoopManager>.I.Clear();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.isRetry = true;
      MonoBehaviourSingleton<GameSceneManager>.I.ReloadScene();
    }
    else
    {
      if (this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.IDLE || this.animState == SeriesArenaResultTop.RESULT_ANIM_STATE.TREASURE_END)
        return;
      this.OnQuery_SKIP();
    }
  }

  protected override string GetSceneName() => nameof (SeriesArenaResultTop);

  private new enum UI
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
    BTN_NEXT_ONLY,
    BTN_NEXT_ALL,
    BTN_NEXT,
    BTN_RETRY,
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
    SHADOW,
  }

  private new enum AUDIO
  {
    COUNTUP = 40000012, // 0x02625A0C
    ADVENT = 40000026, // 0x02625A1A
    ACHIEVEMENT = 40000028, // 0x02625A1C
    CATEGORY = 40000228, // 0x02625AE4
    POINTREWARD = 40000230, // 0x02625AE6
    ARRIVAL = 40000269, // 0x02625B0D
  }

  private new enum RESULT_ANIM_STATE
  {
    IDLE,
    TITLE,
    DROP,
    MISSION,
    MISSION_REWARD,
    TREASURE,
    TREASURE_END,
    REMAIN_TIME,
    CLEAR_TIME_COUNT_UP,
    CLEAR_EFFECT,
    BEST_SCORE,
    EVENT,
    END,
  }
}
