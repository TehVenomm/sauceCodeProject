// Decompiled with JetBrains decompiler
// Type: GuildRequestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class GuildRequestResult : QuestResultTop
{
  private GuildRequestResult.UI dropItemSCR = GuildRequestResult.UI.OBJ_SCROLL_VIEW;
  private GuildRequestCompleteModel.Param guildRequestCompleteData;

  public override void Initialize()
  {
    this.guildRequestCompleteData = GameSection.GetEventData() as GuildRequestCompleteModel.Param;
    MonoBehaviourSingleton<QuestManager>.I.SetCompleteDataFromGuildRequest(MonoBehaviourSingleton<GuildRequestManager>.I.GetBeforeQuestId(), this.guildRequestCompleteData);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) GuildRequestResult.UI.BTN_NEXT, false);
    this.SetActive((Enum) GuildRequestResult.UI.OBJ_BONUS_POINT_SHOP, false);
    this.SetGrid((Enum) GuildRequestResult.UI.GRD_DROP_ITEM, (string) null, this.dropItemIconData.Length, true, (Action<int, Transform, bool>) ((i, o, is_recycle) =>
    {
      ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
      RARITY_TYPE? rarity = new RARITY_TYPE?();
      ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
      EQUIPMENT_TYPE? magi_enable_icon_type = new EQUIPMENT_TYPE?();
      int icon_id = -1;
      int num = -1;
      if (i < this.dropItemIconData.Length && this.dropItemIconData[i] != null)
      {
        itemIconType = this.dropItemIconData[i].GetIconType();
        icon_id = this.dropItemIconData[i].GetIconID();
        rarity = new RARITY_TYPE?(this.dropItemIconData[i].GetRarity());
        element = this.dropItemIconData[i].GetIconElement();
        magi_enable_icon_type = this.dropItemIconData[i].GetIconMagiEnableType();
        num = this.dropItemIconData[i].GetNum();
        if (num == 1)
          num = -1;
      }
      bool is_new = false;
      switch (itemIconType)
      {
        case ITEM_ICON_TYPE.NONE:
          int enemy_icon_id = 0;
          if (itemIconType == ITEM_ICON_TYPE.ITEM)
            enemy_icon_id = Singleton<ItemTable>.I.GetItemData(this.dropItemIconData[i].GetTableID()).enemyIconID;
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
              questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
            });
          else
            _icon = ItemIcon.Create(itemIconType, icon_id, rarity, o, element, magi_enable_icon_type, num, "DROP", i, is_new, enemy_icon_id: enemy_icon_id, getType: this.dropItemIconData[i].GetGetType());
          _icon.SetRewardBG(true);
          _icon.SetRewardCategoryInfo(this.dropItemIconData[i].GetCategory());
          this.SetMaterialInfo(_icon.transform, this.dropItemIconData[i].GetMaterialType(), this.dropItemIconData[i].GetTableID(), this.GetCtrl((Enum) GuildRequestResult.UI.PNL_MATERIAL_INFO));
          ((Component) _icon.transform.Find("MaterialInfo")).gameObject.SetActive(false);
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
    this.GetComponent<UITable>((Enum) GuildRequestResult.UI.TBL_GUILD_REQUEST_RESULT).Reposition();
    this.TreasureStart();
  }

  protected override void TreasureStart()
  {
    this.animState = QuestResultTop.RESULT_ANIM_STATE.TREASURE;
    this.startDropDirection = true;
  }

  protected override void AnimationEnd()
  {
    this.animState = QuestResultTop.RESULT_ANIM_STATE.END;
    this.animationEnd = true;
    if (this.guildRequestCompleteData.bonusPointShop != null && this.guildRequestCompleteData.bonusPointShop.Count > 0)
    {
      this.SetLabelText((Enum) GuildRequestResult.UI.LBL_BONUS_POINT_NUM, this.guildRequestCompleteData.bonusPointShop[0].getPoint.ToString());
      this.SetActive((Enum) GuildRequestResult.UI.OBJ_BONUS_POINT_SHOP, true);
      this.GetComponent<UITable>((Enum) GuildRequestResult.UI.TBL_GUILD_REQUEST_RESULT).Reposition();
      this.SetScroll((Enum) this.dropItemSCR, this.animScrollValue);
    }
    this.OpenPointEvent((System.Action) (() => this.VisibleEndButton()));
  }

  protected override void VisibleEndButton()
  {
    this.SetActive((Enum) GuildRequestResult.UI.BTN_NEXT, this.animationEnd);
    this.SetActive((Enum) GuildRequestResult.UI.BTN_SKIP_FULL_SCREEN, false);
    this.SetActive((Enum) GuildRequestResult.UI.BTN_SKIP_IN_SCROLL, false);
    this.SetActive((Enum) GuildRequestResult.UI.BTN_SKIP_IN_SCROLL_2, false);
  }

  protected override string GetSceneName() => nameof (GuildRequestResult);

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
    START_BONUS_TIME = 40000268, // 0x02625B0C
  }
}
