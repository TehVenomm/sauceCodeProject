// Decompiled with JetBrains decompiler
// Type: RushResultTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class RushResultTop : QuestResultTop
{
  private ResultReward[] resultRewards;
  private PointEventCurrentData.Reward[] firstRewards;
  private bool is_skip;
  private const string ARRIVAL_EFFECT = "RushArrival";
  private const string ARRIVAL_EFFECT_NAME_BASE = "RushArrival_Wave_Txt_";
  private const float DROP_ICON_HEIGHT = 100f;
  private const float WAVE_LABEL_HEIGHT = 36f;
  private Transform material_info_t;
  private RushResultTop.RESULT_ANIM_STATE animState;
  private Vector3 preScrollViewPosition;

  public override void Initialize()
  {
    this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT_ROOT, false);
    this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_BONUS, false);
    this.material_info_t = this.CreateMaterialInfo((Enum) RushResultTop.UI.PNL_MATERIAL_INFO);
    base.Initialize();
  }

  protected override void InitReward()
  {
    List<ResultReward> resultRewardList = new List<ResultReward>();
    this.dropItemNum = 0;
    this.dropLineNum = 0;
    this.eventRewardTitles = new List<string>();
    this.followReward = new QuestCompleteReward();
    List<PointEventCurrentData.Reward> rewardList = new List<PointEventCurrentData.Reward>();
    if (MonoBehaviourSingleton<InGameManager>.I.rushRewards.Count > 0)
    {
      this.isVictory = true;
      foreach (QuestCompleteRewardList rushReward in MonoBehaviourSingleton<InGameManager>.I.rushRewards)
      {
        ResultReward resultReward = new ResultReward();
        this.DevideRewardDropAndEvent(resultReward, rushReward.drop);
        QuestCompleteReward breakPartsReward = rushReward.breakPartsReward;
        QuestCompleteReward breakReward = rushReward.breakReward;
        QuestCompleteReward order = rushReward.order;
        this.followReward.Add(rushReward.followReward);
        foreach (QuestCompleteReward.Item obj in rushReward.first.item)
          rewardList.Add(new PointEventCurrentData.Reward()
          {
            type = 3,
            itemId = obj.itemId,
            num = obj.num
          });
        foreach (QuestCompleteReward.QuestItem questItem in rushReward.first.questItem)
          rewardList.Add(new PointEventCurrentData.Reward()
          {
            type = 6,
            itemId = questItem.questId,
            num = questItem.num
          });
        foreach (QuestCompleteReward.EquipItem equipItem in rushReward.first.equipItem)
          rewardList.Add(new PointEventCurrentData.Reward()
          {
            type = 4,
            itemId = equipItem.equipItemId,
            num = equipItem.num
          });
        foreach (QuestCompleteReward.AccessoryItem accessoryItem in rushReward.first.accessoryItem)
          rewardList.Add(new PointEventCurrentData.Reward()
          {
            type = 14,
            itemId = accessoryItem.accessoryId,
            num = accessoryItem.num
          });
        int money = rushReward.first.money;
        if (money > 0)
          rewardList.Add(new PointEventCurrentData.Reward()
          {
            type = 2,
            num = money
          });
        int crystal = rushReward.first.crystal;
        if (crystal > 0)
          rewardList.Add(new PointEventCurrentData.Reward()
          {
            type = 1,
            num = crystal
          });
        List<SortCompareData> drop_ary = new List<SortCompareData>();
        int start_ary_index1 = 0;
        int start_ary_index2 = ResultUtility.SetDropData(drop_ary, start_ary_index1, order.item);
        int start_ary_index3 = ResultUtility.SetDropData(drop_ary, start_ary_index2, order.equipItem);
        int start_ary_index4 = ResultUtility.SetDropData(drop_ary, start_ary_index3, order.skillItem);
        int start_ary_index5 = ResultUtility.SetDropData(drop_ary, start_ary_index4, order.accessoryItem);
        int start_ary_index6 = ResultUtility.SetDropData(drop_ary, start_ary_index5, resultReward.dropReward.item);
        int start_ary_index7 = ResultUtility.SetDropData(drop_ary, start_ary_index6, resultReward.dropReward.equipItem);
        int start_ary_index8 = ResultUtility.SetDropData(drop_ary, start_ary_index7, resultReward.dropReward.skillItem);
        int start_ary_index9 = ResultUtility.SetDropData(drop_ary, start_ary_index8, resultReward.dropReward.questItem);
        int start_ary_index10 = ResultUtility.SetDropData(drop_ary, start_ary_index9, resultReward.dropReward.accessoryItem);
        int start_ary_index11 = ResultUtility.SetDropData(drop_ary, start_ary_index10, breakReward.item);
        int start_ary_index12 = ResultUtility.SetDropData(drop_ary, start_ary_index11, breakReward.equipItem);
        int start_ary_index13 = ResultUtility.SetDropData(drop_ary, start_ary_index12, breakReward.skillItem);
        int start_ary_index14 = ResultUtility.SetDropData(drop_ary, start_ary_index13, breakReward.accessoryItem);
        int start_ary_index15 = ResultUtility.SetDropData(drop_ary, start_ary_index14, breakPartsReward.item, REWARD_CATEGORY.BREAK);
        int start_ary_index16 = ResultUtility.SetDropData(drop_ary, start_ary_index15, breakPartsReward.equipItem, REWARD_CATEGORY.BREAK);
        int start_ary_index17 = ResultUtility.SetDropData(drop_ary, start_ary_index16, breakPartsReward.skillItem, REWARD_CATEGORY.BREAK);
        ResultUtility.SetDropData(drop_ary, start_ary_index17, breakPartsReward.accessoryItem, REWARD_CATEGORY.BREAK);
        drop_ary.Sort((Comparison<SortCompareData>) ((l, r) => r.GetSortValueQuestResult() - l.GetSortValueQuestResult()));
        resultReward.dropItemIconData = drop_ary.ToArray();
        this.dropItemNum += resultReward.dropItemIconData.Length;
        resultRewardList.Add(resultReward);
      }
    }
    this.pointShopResultData = MonoBehaviourSingleton<InGameManager>.I.rushPointShops ?? new List<PointShopResultData>();
    this.resultRewards = resultRewardList.ToArray();
    this.firstRewards = rewardList.ToArray();
    this.dropLineNum = (this.dropItemNum - 1) / 5 + 1;
  }

  public override void UpdateUI()
  {
    this.SetFullScreenButton((Enum) RushResultTop.UI.BTN_SKIP_FULL_SCREEN);
    this.SetHeight((Enum) RushResultTop.UI.BTN_SKIP_IN_SCROLL, this.dropLineNum * 100);
    this.SetActive((Enum) RushResultTop.UI.BTN_NEXT, false);
    this.SetLabelText((Enum) RushResultTop.UI.LBL_QUEST_NAME, Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).questText);
    this.SetLabelText((Enum) RushResultTop.UI.LBL_WAVE, string.Format(StringTable.Get(STRING_CATEGORY.RUSH_WAVE, 10004400U), (object) MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum()));
    this.SetLabelText((Enum) RushResultTop.UI.LBL_TIME, MonoBehaviourSingleton<InGameRecorder>.I.rushRemainTimeToString);
    this.SetActive((Enum) RushResultTop.UI.GET_ITEM, true);
    int num = 0;
    if (this.isVictory)
    {
      List<QuestCompleteRewardList> rushRewards = MonoBehaviourSingleton<InGameManager>.I.rushRewards;
      this.SetTable(this.GetCtrl((Enum) RushResultTop.UI.OBJ_TREASURE_ROOT), (Enum) RushResultTop.UI.TBL_DROP_ITEM, "RushWaveDropItem", this.resultRewards.Length, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        ((Object) t).name = "wave" + (object) MonoBehaviourSingleton<InGameManager>.I.GetWaveNum(i);
        this.SetDropItemIcon(this.resultRewards[i].dropItemIconData, t, MonoBehaviourSingleton<InGameManager>.I.GetWaveNum(i));
      }));
      for (int index = 0; index < rushRewards.Count; ++index)
      {
        QuestCompleteRewardList completeRewardList = rushRewards[index];
        QuestCompleteReward dropReward = this.resultRewards[index].dropReward;
        QuestCompleteReward breakReward = completeRewardList.breakReward;
        QuestCompleteReward order = completeRewardList.order;
        num = num + dropReward.money + breakReward.money + order.money;
      }
      if (this.firstRewards.Length != 0)
      {
        this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT_ROOT, true);
        int waveNum = MonoBehaviourSingleton<InGameManager>.I.GetWaveNum(MonoBehaviourSingleton<InGameManager>.I.GetRushIndex() - (MonoBehaviourSingleton<InGameRecorder>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY ? 0 : 1));
        UISprite component1 = ((Component) this.GetCtrl((Enum) RushResultTop.UI.SPR_WAVE_01)).GetComponent<UISprite>();
        UISprite component2 = ((Component) this.GetCtrl((Enum) RushResultTop.UI.SPR_WAVE_10)).GetComponent<UISprite>();
        UISprite component3 = ((Component) this.GetCtrl((Enum) RushResultTop.UI.SPR_WAVE_100)).GetComponent<UISprite>();
        UISprite component4 = ((Component) this.GetCtrl((Enum) RushResultTop.UI.SPR_WAVE_1000)).GetComponent<UISprite>();
        string str1 = waveNum.ToString("D4");
        string str2 = "RushArrival_Wave_Txt_" + str1[3].ToString();
        component1.spriteName = str2;
        component2.spriteName = "RushArrival_Wave_Txt_" + str1[2].ToString();
        UISprite uiSprite1 = component3;
        char ch;
        string str3;
        if (waveNum < 100)
        {
          str3 = "";
        }
        else
        {
          ch = str1[1];
          str3 = "RushArrival_Wave_Txt_" + ch.ToString();
        }
        uiSprite1.spriteName = str3;
        UISprite uiSprite2 = component4;
        string str4;
        if (waveNum < 1000)
        {
          str4 = "";
        }
        else
        {
          ch = str1[0];
          str4 = "RushArrival_Wave_Txt_" + ch.ToString();
        }
        uiSprite2.spriteName = str4;
        this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT_ROOT, false);
        this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_BONUS, true);
        this.SetGrid((Enum) RushResultTop.UI.GRD_ARRIVAL_ITEM_ICON, "ItemIconReward", this.firstRewards.Length, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
        {
          PointEventCurrentData.Reward firstReward = this.firstRewards[i];
          ItemIcon.CreateRewardItemIcon((REWARD_TYPE) firstReward.type, (uint) firstReward.itemId, t, firstReward.num);
          ((Component) t.Find("itemNum")).GetComponent<UILabel>().text = "×" + (object) this.firstRewards[i].num;
        }));
        this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_BONUS, false);
      }
    }
    this.SetLabelText((Enum) RushResultTop.UI.LBL_REWARD_GOLD, num.ToString("N0"));
    bool is_visible = this.pointShopResultData.Count > 0;
    this.SetActive((Enum) RushResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT, is_visible);
    if (is_visible)
      this.SetGrid((Enum) RushResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT, "QuestResultPointShop", this.pointShopResultData.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
      {
        this.ResetTween(t);
        PointShopResultData pointShopResultData = this.pointShopResultData[i];
        this.SetActive(t, (Enum) RushResultTop.UI.OBJ_NORMAL_POINT_SHOP_ROOT, !pointShopResultData.isEvent);
        if (!pointShopResultData.isEvent)
        {
          this.SetLabelText(t, (Enum) RushResultTop.UI.LBL_NORMAL_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.getPoint));
          this.SetLabelText(t, (Enum) RushResultTop.UI.LBL_NORMAL_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.totalPoint));
          ResourceLoad.LoadPointIconImageTexture(((Component) this.FindCtrl(t, (Enum) RushResultTop.UI.TEX_NORMAL_POINT_SHOP_ICON)).GetComponent<UITexture>(), (uint) pointShopResultData.pointShopId);
        }
        this.SetActive(t, (Enum) RushResultTop.UI.OBJ_EVENT_POINT_SHOP_ROOT, pointShopResultData.isEvent);
        if (!pointShopResultData.isEvent)
          return;
        this.SetLabelText(t, (Enum) RushResultTop.UI.LBL_EVENT_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.getPoint));
        this.SetLabelText(t, (Enum) RushResultTop.UI.LBL_EVENT_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.totalPoint));
        ResourceLoad.LoadPointIconImageTexture(((Component) this.FindCtrl(t, (Enum) RushResultTop.UI.TEX_EVENT_POINT_SHOP_ICON)).GetComponent<UITexture>(), (uint) pointShopResultData.pointShopId);
      }));
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
    {
      UIVirtualScreen componentInChildren = ((Component) this).GetComponentInChildren<UIVirtualScreen>();
      UIWidget component = ((Component) this.GetCtrl((Enum) RushResultTop.UI.SHADOW)).GetComponent<UIWidget>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null) && Object.op_Inequality((Object) component, (Object) null))
      {
        component.width = (int) componentInChildren.ScreenWidthFull;
        component.height = (int) componentInChildren.ScreenHeightFull;
      }
    }
    this.StartCoroutine(this.PlayAnimation());
  }

  private ItemIcon CreateItemIcon(SortCompareData dropItem, Transform o, string event_name, int i)
  {
    ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
    RARITY_TYPE? rarity = new RARITY_TYPE?();
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
    EQUIPMENT_TYPE? magi_enable_icon_type = new EQUIPMENT_TYPE?();
    int icon_id = -1;
    int num = -1;
    if (dropItem != null)
    {
      itemIconType = dropItem.GetIconType();
      icon_id = dropItem.GetIconID();
      rarity = new RARITY_TYPE?(dropItem.GetRarity());
      element = dropItem.GetIconElement();
      magi_enable_icon_type = dropItem.GetIconMagiEnableType();
      num = dropItem.GetNum();
      if (num == 1)
        num = -1;
    }
    bool is_new = false;
    switch (itemIconType)
    {
      case ITEM_ICON_TYPE.NONE:
        int enemy_icon_id = 0;
        int enemy_icon_id2 = 0;
        if (itemIconType == ITEM_ICON_TYPE.ITEM)
        {
          ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(dropItem.GetTableID());
          enemy_icon_id = itemData.enemyIconID;
          enemy_icon_id2 = itemData.enemyIconID2;
        }
        ItemIcon itemIcon;
        if (dropItem.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
          itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam()
          {
            icon_type = dropItem.GetIconType(),
            icon_id = dropItem.GetIconID(),
            rarity = new RARITY_TYPE?(dropItem.GetRarity()),
            parent = o,
            element = dropItem.GetIconElement(),
            magi_enable_equip_type = dropItem.GetIconMagiEnableType(),
            num = dropItem.GetNum(),
            enemy_icon_id = enemy_icon_id,
            enemy_icon_id2 = enemy_icon_id2,
            questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
          });
        else
          itemIcon = ItemIcon.Create(itemIconType, icon_id, rarity, o, element, magi_enable_icon_type, num, event_name, i, is_new, enemy_icon_id: enemy_icon_id, enemy_icon_id2: enemy_icon_id2, getType: dropItem.GetGetType());
        itemIcon.SetRewardBG(true);
        itemIcon.SetRewardCategoryInfo(dropItem.GetCategory());
        Transform ctrl = this.GetCtrl((Enum) RushResultTop.UI.PNL_MATERIAL_INFO);
        MaterialInfoButton.Set(itemIcon.transform, this.material_info_t, dropItem.GetMaterialType(), dropItem.GetTableID(), this.sectionData.sectionName, ctrl);
        return itemIcon;
      case ITEM_ICON_TYPE.ITEM:
      case ITEM_ICON_TYPE.QUEST_ITEM:
        if (dropItem.GetUniqID() != 0UL)
        {
          is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(itemIconType, dropItem.GetUniqID());
          goto case ITEM_ICON_TYPE.NONE;
        }
        goto case ITEM_ICON_TYPE.NONE;
      default:
        is_new = true;
        goto case ITEM_ICON_TYPE.NONE;
    }
  }

  private void SetDropItemIcon(SortCompareData[] dropItemList, Transform t_grid, int wave)
  {
    if (dropItemList == null)
      return;
    string text1 = string.Format(StringTable.Get(STRING_CATEGORY.RUSH_WAVE, 10004400U), (object) wave);
    this.SetLabelText(t_grid, (Enum) RushResultTop.UI.LBL_DROP_ITEM_WAVE, text1);
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) MonoBehaviourSingleton<InGameManager>.I.GetRushQuestId(wave));
    string text2 = $"Lv{questData.GetMainEnemyLv().ToString()}{Singleton<EnemyTable>.I.GetEnemyName((uint) questData.GetMainEnemyID())}";
    this.SetLabelText(t_grid, (Enum) RushResultTop.UI.LBL_BOSS_NAME, text2);
    this.SetGrid(t_grid, (Enum) RushResultTop.UI.GRD_DROP_ITEM, (string) null, dropItemList.Length, true, (Action<int, Transform, bool>) ((i, o, is_recycle) =>
    {
      ItemIcon _icon = (ItemIcon) null;
      if (i < dropItemList.Length)
        _icon = this.CreateItemIcon(dropItemList[i], o, "DROP", i);
      Transform transform = this.SetPrefab(o, "QuestResultDropIconOpener");
      QuestResultDropIconOpener.Info info1 = new QuestResultDropIconOpener.Info()
      {
        IsRare = ResultUtility.IsRare(dropItemList[i]),
        IsBroken = ResultUtility.IsBreakReward(dropItemList[i])
      };
      ((Component) transform).GetComponent<QuestResultDropIconOpener>().Initialized(_icon, info1, (Action<Transform, QuestResultDropIconOpener.Info, bool>) ((t, info, is_skip) =>
      {
        string ui_effect_name = "ef_ui_dropitem_silver_01";
        if (info.IsBroken)
          ui_effect_name = "ef_ui_dropitem_red_01";
        else if (info.IsRare)
          ui_effect_name = "ef_ui_dropitem_gold_01";
        this.SetVisibleWidgetOneShotEffect(this.GetCtrl((Enum) RushResultTop.UI.OBJ_SCROLL_VIEW), t, ui_effect_name);
      }));
    }));
  }

  protected override void Update()
  {
  }

  private IEnumerator PlayAnimation()
  {
    this.is_skip = false;
    this.animState = RushResultTop.RESULT_ANIM_STATE.TITLE;
    this.PlayAudio(RushResultTop.AUDIO.ADVENT);
    this.PlayTween((Enum) RushResultTop.UI.OBJ_TITLE, callback: (EventDelegate.Callback) (() => this.animState = RushResultTop.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    while (this.animState != RushResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.animState = RushResultTop.RESULT_ANIM_STATE.WAVE;
    this.PlayAudio(RushResultTop.AUDIO.ACHIEVEMENT);
    this.PlayTween((Enum) RushResultTop.UI.OBJ_WAVE, callback: (EventDelegate.Callback) (() => this.animState = RushResultTop.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    while (this.animState != RushResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.animState = RushResultTop.RESULT_ANIM_STATE.TIME;
    this.PlayAudio(RushResultTop.AUDIO.ACHIEVEMENT);
    this.PlayTween((Enum) RushResultTop.UI.OBJ_TIME, callback: (EventDelegate.Callback) (() => this.animState = RushResultTop.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    while (this.animState != RushResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    if (this.firstRewards.Length != 0)
    {
      this.animState = RushResultTop.RESULT_ANIM_STATE.ARRIVAL;
      this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT_ROOT, true);
      this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_BONUS, true);
      this.PlayAudio(RushResultTop.AUDIO.ARRIVAL);
      this.PlayTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT, is_input_block: false, tween_ctrl_id: 1);
      this.PlayTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT, callback: (EventDelegate.Callback) (() => this.PlayAudio(RushResultTop.AUDIO.ARRIVAL_WAVE)), is_input_block: false, tween_ctrl_id: 2);
      this.PlayTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT, callback: (EventDelegate.Callback) (() => this.PlayAudio(RushResultTop.AUDIO.ARRIVAL_WAVE)), is_input_block: false, tween_ctrl_id: 3);
      this.PlayTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_BONUS, is_input_block: false);
      this.SetActive((Enum) RushResultTop.UI.BTN_NEXT, true);
      this.is_skip = false;
      while (!this.is_skip)
        yield return (object) null;
      this.animState = RushResultTop.RESULT_ANIM_STATE.ARRIVAL_NEXT;
      this.PlayTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT, callback: (EventDelegate.Callback) (() => this.animState = RushResultTop.RESULT_ANIM_STATE.IDLE), is_input_block: false, tween_ctrl_id: 4);
      while (this.animState != RushResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
        yield return (object) null;
      this.is_skip = false;
      this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT_ROOT, false);
      this.SetActive((Enum) RushResultTop.UI.OBJ_ARRIVAL_BONUS, false);
    }
    this.animState = RushResultTop.RESULT_ANIM_STATE.TREASURE;
    this.PlayAudio(RushResultTop.AUDIO.ACHIEVEMENT);
    if (this.pointShopResultData.Count > 0)
    {
      foreach (Transform t in ((Component) this.GetCtrl((Enum) RushResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT)).transform)
        this.PlayTween(t);
    }
    this.PlayTween((Enum) RushResultTop.UI.OBJ_MONEY);
    this.PlayTween((Enum) RushResultTop.UI.OBJ_TREASURE_ROOT, callback: (EventDelegate.Callback) (() => this.animState = RushResultTop.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    while (this.animState != RushResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.is_skip = false;
    this.animState = RushResultTop.RESULT_ANIM_STATE.ITEM_ICON;
    this.StartCoroutine(this.PlayItemAnimation((System.Action) (() => this.animState = RushResultTop.RESULT_ANIM_STATE.IDLE)));
    while (this.animState != RushResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.animState = RushResultTop.RESULT_ANIM_STATE.EVENT;
    this.OpenAllEventRewardDialog((System.Action) (() => this.OpenMutualFollowBonusDialog((System.Action) (() => this.animState = RushResultTop.RESULT_ANIM_STATE.IDLE))));
    while (this.animState != RushResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.is_skip = true;
    this.animState = RushResultTop.RESULT_ANIM_STATE.END;
    this.VisibleEndButton();
  }

  protected override void VisibleEndButton()
  {
    this.SetActive((Enum) RushResultTop.UI.BTN_NEXT, true);
    this.SetActive((Enum) RushResultTop.UI.BTN_SKIP_FULL_SCREEN, false);
    this.SetActive((Enum) RushResultTop.UI.BTN_SKIP_IN_SCROLL, false);
  }

  private IEnumerator PlayItemAnimation(System.Action callback)
  {
    for (int wave = 0; wave < this.resultRewards.Length; ++wave)
    {
      if (wave == 0)
      {
        Transform ctrl = this.GetCtrl((Enum) RushResultTop.UI.OBJ_SCROLL_VIEW);
        ((Component) ctrl).GetComponent<UIScrollView>().ResetPosition();
        this.preScrollViewPosition = ctrl.localPosition;
      }
      else
      {
        this.animTimer = 0.0f;
        while ((double) this.animTimer < 0.40000000596046448 && !this.is_skip)
        {
          this.animTimer += Time.deltaTime;
          yield return (object) null;
        }
        this.Scroll((float) (36.0 + 100.0 * (double) Mathf.CeilToInt((float) this.resultRewards[wave - 1].dropItemIconData.Length / 5f)));
      }
      for (int i = 0; i < this.resultRewards[wave].dropItemIconData.Length; ++i)
      {
        this.animTimer = 0.0f;
        while ((double) this.animTimer < 0.40000000596046448 && !this.is_skip)
        {
          this.animTimer += Time.deltaTime;
          yield return (object) null;
        }
        this.VisibleItemIcon(wave, i, this.is_skip);
      }
    }
    callback();
  }

  private void Scroll(float delta)
  {
    Vector3 pos = Vector3.op_Addition(this.preScrollViewPosition, Vector3.op_Multiply(Vector3.up, delta));
    SpringPanel.Begin(((Component) this.GetCtrl((Enum) RushResultTop.UI.OBJ_SCROLL_VIEW)).gameObject, pos, 8f);
    this.preScrollViewPosition = pos;
  }

  private void VisibleItemIcon(int wave, int index, bool is_skip = false)
  {
    QuestResultDropIconOpener componentInChildren = ((Component) this.GetChild(this.GetChild((Enum) RushResultTop.UI.TBL_DROP_ITEM, wave), (Enum) RushResultTop.UI.GRD_DROP_ITEM, index)).gameObject.GetComponentInChildren<QuestResultDropIconOpener>();
    if (Object.op_Equality((Object) componentInChildren, (Object) null))
      return;
    this.PlayAudio(RushResultTop.AUDIO.DROPITEM);
    componentInChildren.StartEffect(is_skip);
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

  private void OnQuery_SKIP()
  {
    switch (this.animState)
    {
      case RushResultTop.RESULT_ANIM_STATE.TITLE:
      case RushResultTop.RESULT_ANIM_STATE.WAVE:
      case RushResultTop.RESULT_ANIM_STATE.TIME:
      case RushResultTop.RESULT_ANIM_STATE.ARRIVAL:
        this.SkipTween((Enum) RushResultTop.UI.OBJ_TITLE);
        this.SkipTween((Enum) RushResultTop.UI.OBJ_WAVE);
        this.SkipTween((Enum) RushResultTop.UI.OBJ_TIME);
        this.SkipTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT, tween_ctrl_id: 1);
        this.SkipTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT, tween_ctrl_id: 2);
        this.SkipTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT, tween_ctrl_id: 3);
        break;
      case RushResultTop.RESULT_ANIM_STATE.ARRIVAL_NEXT:
        this.SkipTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_EFFECT, tween_ctrl_id: 4);
        this.SkipTween((Enum) RushResultTop.UI.OBJ_ARRIVAL_BONUS);
        break;
      case RushResultTop.RESULT_ANIM_STATE.TREASURE:
        this.SkipTween((Enum) RushResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT);
        this.SkipTween((Enum) RushResultTop.UI.OBJ_MONEY);
        this.SkipTween((Enum) RushResultTop.UI.OBJ_TREASURE_ROOT);
        break;
    }
    this.is_skip = true;
    GameSection.StopEvent();
  }

  private void OnQuery_NEXT()
  {
    if (this.animState == RushResultTop.RESULT_ANIM_STATE.IDLE || this.animState == RushResultTop.RESULT_ANIM_STATE.END)
      return;
    this.OnQuery_SKIP();
  }

  private void PlayAudio(RushResultTop.AUDIO type) => SoundManager.PlayOneShotUISE((int) type);

  private new void OpenAllEventRewardDialog(System.Action endCallback)
  {
    this.eventRewardIndex = 0;
    this.eventRewardList = new List<QuestCompleteReward>();
    for (int index = 0; index < this.eventRewardTitles.Count; ++index)
      this.eventRewardList.Add(new QuestCompleteReward());
    foreach (ResultReward resultReward in this.resultRewards)
    {
      QuestCompleteReward eventReward = resultReward.eventReward;
      for (int index1 = 0; index1 < eventReward.eventPrice.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.eventRewardTitles.Count; ++index2)
        {
          if (this.eventRewardTitles[index2] == eventReward.eventPrice[index1].rewardTitle)
            this.eventRewardList[index2].eventPrice.Add(eventReward.eventPrice[index1]);
        }
      }
      for (int index3 = 0; index3 < eventReward.item.Count; ++index3)
      {
        for (int index4 = 0; index4 < this.eventRewardTitles.Count; ++index4)
        {
          if (this.eventRewardTitles[index4] == eventReward.item[index3].rewardTitle)
            this.eventRewardList[index4].item.Add(eventReward.item[index3]);
        }
      }
      for (int index5 = 0; index5 < eventReward.skillItem.Count; ++index5)
      {
        for (int index6 = 0; index6 < this.eventRewardTitles.Count; ++index6)
        {
          if (this.eventRewardTitles[index6] == eventReward.skillItem[index5].rewardTitle)
            this.eventRewardList[index6].skillItem.Add(eventReward.skillItem[index5]);
        }
      }
      for (int index7 = 0; index7 < eventReward.equipItem.Count; ++index7)
      {
        for (int index8 = 0; index8 < this.eventRewardTitles.Count; ++index8)
        {
          if (this.eventRewardTitles[index8] == eventReward.equipItem[index7].rewardTitle)
            this.eventRewardList[index8].equipItem.Add(eventReward.equipItem[index7]);
        }
      }
      for (int index9 = 0; index9 < eventReward.questItem.Count; ++index9)
      {
        for (int index10 = 0; index10 < this.eventRewardTitles.Count; ++index10)
        {
          if (this.eventRewardTitles[index10] == eventReward.questItem[index9].rewardTitle)
            this.eventRewardList[index10].questItem.Add(eventReward.questItem[index9]);
        }
      }
      for (int index11 = 0; index11 < eventReward.accessoryItem.Count; ++index11)
      {
        for (int index12 = 0; index12 < this.eventRewardTitles.Count; ++index12)
        {
          if (this.eventRewardTitles[index12] == eventReward.accessoryItem[index11].rewardTitle)
            this.eventRewardList[index12].accessoryItem.Add(eventReward.accessoryItem[index11]);
        }
      }
    }
    if (this.eventRewardList.Count == 0)
    {
      if (endCallback == null)
        return;
      endCallback();
    }
    else
    {
      this.OpenEventRewardDialog(this.eventRewardList[this.eventRewardIndex], this.eventRewardTitles[this.eventRewardIndex], endCallback);
      ++this.eventRewardIndex;
    }
  }

  private void OpenMutualFollowBonusDialog(System.Action end_callback)
  {
    if (this.followReward != null)
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
      if (end_callback == null)
        return;
      end_callback();
    }
  }

  protected override string GetSceneName() => nameof (RushResultTop);

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
    SPR_WAVE_1000,
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
    BTN_NEXT_ALL,
    BTN_END_HUNT_CENTER,
    BTN_END_HUNT_LEFT,
    BTN_REPEAT_HUNT,
    LBL_BTN_REPEAT_HUNT,
    LBL_WAIT_FOR_HOST,
  }

  private new enum AUDIO
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
    ARRIVAL = 40000269, // 0x02625B0D
    ARRIVAL_WAVE = 40000270, // 0x02625B0E
  }

  private new enum RESULT_ANIM_STATE
  {
    IDLE,
    TITLE,
    WAVE,
    TIME,
    ARRIVAL,
    ARRIVAL_NEXT,
    TREASURE,
    ITEM_ICON,
    EVENT,
    END,
  }
}
