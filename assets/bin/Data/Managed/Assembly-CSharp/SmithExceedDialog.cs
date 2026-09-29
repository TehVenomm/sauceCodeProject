// Decompiled with JetBrains decompiler
// Type: SmithExceedDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithExceedDialog : GameSection
{
  protected int exceedCount;
  protected EquipItemTable.EquipItemData itemTable;
  private EquipItemExceedTable.EquipItemExceedData exceedData;
  private SmithManager.SmithGrowData smithData;
  private int selectIndex;
  private Color paramColor = Color.white;
  private int selectPageIndex;
  private int maxPageIndex;
  private const int MAX_SHOW_EXCEED_NUM = 3;
  private int need_select_index;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "EquipItemExceedTable";
      yield return "LimitedEquipItemExceedTable";
    }
  }

  protected virtual bool IsValidExceedSection() => true;

  protected virtual void SetupExceedData()
  {
    this.exceedCount = this.smithData.selectEquipData.exceed;
    this.itemTable = this.smithData.selectEquipData.tableData;
  }

  public override void Initialize()
  {
    this.smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    this.SetupExceedData();
    this.exceedData = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedDataIncludeLimited(this.itemTable);
    this.selectPageIndex = 0;
    int num = Mathf.CeilToInt((float) this.exceedData.exceed.Length / 3f);
    this.maxPageIndex = num - 1;
    this.SetLabelText((Enum) SmithExceedDialog.UI.LBL_SELECT_MAX, num.ToString());
    this.SetLabelText((Enum) SmithExceedDialog.UI.LBL_SELECT_NOW, (this.selectPageIndex + 1).ToString());
    if (num == 0)
      this.SetActive((Enum) SmithExceedDialog.UI.OBJ_SELECT, false);
    else if (num <= 1)
    {
      this.SetActive((Enum) SmithExceedDialog.UI.BTN_AIM_R, false);
      this.SetActive((Enum) SmithExceedDialog.UI.BTN_AIM_L, false);
    }
    else
    {
      this.SetActive((Enum) SmithExceedDialog.UI.BTN_AIM_R_INACTIVE, false);
      this.SetActive((Enum) SmithExceedDialog.UI.BTN_AIM_L_INACTIVE, false);
    }
    UILabel component = this.GetComponent<UILabel>((Enum) SmithExceedDialog.UI.LBL_EXCEED_0);
    if (Object.op_Inequality((Object) component, (Object) null))
      this.paramColor = component.color;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetActive((Enum) SmithExceedDialog.UI.SPR_COUNT_0_ON, this.exceedCount > 0);
    this.SetActive((Enum) SmithExceedDialog.UI.SPR_COUNT_1_ON, this.exceedCount > 1);
    this.SetActive((Enum) SmithExceedDialog.UI.SPR_COUNT_2_ON, this.exceedCount > 2);
    this.SetActive((Enum) SmithExceedDialog.UI.SPR_COUNT_3_ON, this.exceedCount > 3);
    this.SetLabelText((Enum) SmithExceedDialog.UI.LBL_SELECT_NOW, (this.selectPageIndex + 1).ToString());
    this.UpdateBonusDetail();
    bool is_only_lapis = true;
    this.SetGrid((Enum) SmithExceedDialog.UI.GRD_LAPIS, "SmithExceedItem", this.exceedData.exceed.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (this.exceedCount >= 4)
        this.SetActive(t, false);
      else if (i < this.selectPageIndex * 3 || i >= (this.selectPageIndex + 1) * 3)
      {
        this.SetActive(t, false);
      }
      else
      {
        EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem exceedNeedItem = this.exceedData.exceed[i];
        if (exceedNeedItem == null || exceedNeedItem.itemId == 0U || exceedNeedItem.num[this.exceedCount] == 0U)
        {
          this.SetActive(t, false);
        }
        else
        {
          ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(exceedNeedItem.itemId);
          if (itemData == null)
          {
            this.SetActive(t, false);
          }
          else
          {
            if (itemData.type != ITEM_TYPE.LAPIS)
              is_only_lapis = false;
            this.SetActive(t, true);
            int haveingItemNum = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(exceedNeedItem.itemId);
            int need_num = (int) exceedNeedItem.num[this.exceedCount];
            ITEM_ICON_TYPE icon_type;
            ItemIcon.GetIconShowData(REWARD_TYPE.ITEM, exceedNeedItem.itemId, out int _, out icon_type, out RARITY_TYPE? _, out ELEMENT_TYPE _, out ELEMENT_TYPE _, out EQUIPMENT_TYPE? _, out int _, out int _, out GET_TYPE _);
            Transform ctrl = this.FindCtrl(t, (Enum) SmithExceedDialog.UI.OBJ_MATERIAL_ICON_ROOT);
            bool flag = haveingItemNum >= need_num;
            ItemIcon materialIcon = ItemIconMaterial.CreateMaterialIcon(icon_type, itemData, ctrl, haveingItemNum, need_num, flag ? "NEXT" : "NEED", i);
            this.SetMaterialInfo(materialIcon._transform, REWARD_TYPE.ITEM, exceedNeedItem.itemId, this.GetCtrl((Enum) SmithExceedDialog.UI.SCR_LAPIS_ROOT));
            ItemIconMaterial itemIconMaterial = materialIcon as ItemIconMaterial;
            if (Object.op_Inequality((Object) itemIconMaterial, (Object) null))
              itemIconMaterial.SetVisibleBG(false);
            this.FindCtrl(ctrl, (Enum) SmithExceedDialog.UI.SPR_EXCEED_BTN_BG).parent = materialIcon._transform;
            this.SetActive(t, (Enum) SmithExceedDialog.UI.SPR_EXCEED_GRAYOUT, !flag);
            DateTime dateTime = new DateTime();
            if (itemData.endDate != dateTime)
            {
              string format = StringTable.Get(STRING_CATEGORY.SHOP, 15U);
              this.SetLabelText(t, (Enum) SmithExceedDialog.UI.LBL_LIMITED, string.Format(format, (object) itemData.endDate.ToString("yyyy/MM/dd HH:mm")));
            }
            else
              this.SetActive(t, (Enum) SmithExceedDialog.UI.OBJ_EXCEED_LIMITED, false);
            if (this.IsValidExceedSection())
              return;
            if (Object.op_Equality((Object) ((Component) materialIcon).GetComponent<UINoAuto>(), (Object) null))
              ((Component) materialIcon).gameObject.AddComponent<UINoAuto>();
            if (!Object.op_Equality((Object) ((Component) materialIcon).GetComponent<UIButtonScale>(), (Object) null))
              return;
            UIButtonScale uiButtonScale = ((Component) materialIcon).gameObject.AddComponent<UIButtonScale>();
            uiButtonScale.hover = Vector3.one;
            uiButtonScale.pressed = UIButtonEffect.buttonScale_pressed;
            uiButtonScale.duration = UIButtonEffect.buttonScale_duration;
          }
        }
      }
    }));
    bool is_visible = this.exceedCount < 4;
    this.SetActive((Enum) SmithExceedDialog.UI.OBJ_VALID_EXCEED_ROOT, is_visible);
    this.SetActive((Enum) SmithExceedDialog.UI.OBJ_INVALID_EXCEED_ROOT, !is_visible);
    this.SetLabelText((Enum) SmithExceedDialog.UI.LBL_USE_MATERIAL_NAME, StringTable.Get(STRING_CATEGORY.ITEM_DETAIL, is_only_lapis ? 5U : 6U));
  }

  protected void UpdateBonusDetail(bool changeNextColor = true)
  {
    this.SetActive((Enum) SmithExceedDialog.UI.SPR_EXCEED_0_ON, this.exceedCount > 0);
    this.SetActive((Enum) SmithExceedDialog.UI.SPR_EXCEED_1_ON, this.exceedCount > 1);
    this.SetActive((Enum) SmithExceedDialog.UI.SPR_EXCEED_2_ON, this.exceedCount > 2);
    this.SetActive((Enum) SmithExceedDialog.UI.SPR_EXCEED_3_ON, this.exceedCount > 3);
    EquipItemTable.EquipItemData itemTable = this.itemTable;
    Color color1 = (!changeNextColor ? 0 : (this.exceedCount == 0 ? 1 : 0)) != 0 ? Color.yellow : this.paramColor;
    this.SetLabelText((Enum) SmithExceedDialog.UI.LBL_EXCEED_0, itemTable.GetExceedParamName(1));
    this.SetColor((Enum) SmithExceedDialog.UI.LBL_EXCEED_0, color1);
    Color color2 = (!changeNextColor ? 0 : (this.exceedCount == 1 ? 1 : 0)) != 0 ? Color.yellow : this.paramColor;
    this.SetLabelText((Enum) SmithExceedDialog.UI.LBL_EXCEED_1, itemTable.GetExceedParamName(2));
    this.SetColor((Enum) SmithExceedDialog.UI.LBL_EXCEED_1, color2);
    Color color3 = (!changeNextColor ? 0 : (this.exceedCount == 2 ? 1 : 0)) != 0 ? Color.yellow : this.paramColor;
    this.SetLabelText((Enum) SmithExceedDialog.UI.LBL_EXCEED_2, itemTable.GetExceedParamName(3));
    this.SetColor((Enum) SmithExceedDialog.UI.LBL_EXCEED_2, color3);
    Color color4 = (!changeNextColor ? 0 : (this.exceedCount == 3 ? 1 : 0)) != 0 ? Color.yellow : this.paramColor;
    this.SetLabelText((Enum) SmithExceedDialog.UI.LBL_EXCEED_3, itemTable.GetExceedParamName(4));
    this.SetColor((Enum) SmithExceedDialog.UI.LBL_EXCEED_3, color4);
  }

  private void OnQuery_NEXT()
  {
    if (this.smithData == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      this.selectIndex = (int) GameSection.GetEventData();
      int needNum = (int) this.exceedData.exceed[this.selectIndex].getNeedNum(this.smithData.selectEquipData.exceed + 1);
      GameSection.SetEventData((object) new object[3]
      {
        (object) Singleton<ItemTable>.I.GetItemData(this.exceedData.exceed[this.selectIndex].itemId).name,
        (object) needNum,
        (object) this.smithData.selectEquipData.tableData.name
      });
    }
  }

  private void OnQuery_NEED()
  {
    if (this.smithData == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      this.need_select_index = (int) GameSection.GetEventData();
      int eventData = (int) GameSection.GetEventData();
      int exceed = this.smithData.selectEquipData.exceed;
      int needNum = (int) this.exceedData.exceed[eventData].getNeedNum(exceed + 1);
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(this.exceedData.exceed[eventData].itemId);
      uint id = 7;
      if (itemData.type == ITEM_TYPE.LAPIS)
      {
        id = Singleton<EquipItemExceedTable>.I.IsFreeLapis(itemData.rarity, itemData.id, itemData.eventId) ? 3U : 4U;
        if (Singleton<LimitedEquipItemExceedTable>.I.IsLimitedLapis(itemData.id))
          id = 8U;
      }
      string str = string.Format(StringTable.Get(STRING_CATEGORY.ITEM_DETAIL, id), (object) itemData.rarity.ToString());
      GameSection.SetEventData((object) new object[4]
      {
        (object) itemData.name,
        (object) needNum,
        (object) this.smithData.selectEquipData.tableData.name,
        (object) str
      });
    }
  }

  private void OnQuery_SmithExceedNeedMessage_YES()
  {
    MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostFindData((int) this.exceedData.exceed[this.need_select_index].itemId);
  }

  private void OnQuery_SmithExceedConfirm_YES()
  {
    if (this.smithData == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      EquipItemInfo selectEquipData = this.smithData.selectEquipData;
      if (selectEquipData == null)
      {
        GameSection.StopEvent();
      }
      else
      {
        SmithManager.ResultData result_data = new SmithManager.ResultData();
        result_data.beforeRarity = (int) selectEquipData.tableData.rarity;
        result_data.beforeLevel = selectEquipData.level;
        result_data.beforeMaxLevel = selectEquipData.tableData.maxLv;
        result_data.beforeExceedCnt = selectEquipData.exceed;
        result_data.beforeAtk = selectEquipData.atk;
        result_data.beforeDef = selectEquipData.def;
        result_data.beforeHp = selectEquipData.hp;
        result_data.beforeElemAtk = selectEquipData.elemAtk;
        result_data.beforeElemDef = selectEquipData.elemDef;
        result_data.isExceed = true;
        EquipItemExceedTable.EquipItemExceedData dataIncludeLimited = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedDataIncludeLimited(selectEquipData.tableData);
        if (dataIncludeLimited == null || dataIncludeLimited.exceed.Length - 1 < this.selectIndex)
        {
          GameSection.StopEvent();
        }
        else
        {
          uint itemId = dataIncludeLimited.exceed[this.selectIndex].itemId;
          GameSection.StayEvent();
          MonoBehaviourSingleton<SmithManager>.I.SendExceedEquipItem(selectEquipData.uniqueID, itemId, (Action<Error, EquipItemInfo>) ((err, exceed_equip_item) =>
          {
            int num = err == Error.None ? 1 : 0;
            GameSection.ResumeEvent(num != 0);
            if (num == 0)
              return;
            result_data.itemData = (object) exceed_equip_item;
            GameSection.SetEventData((object) result_data);
          }));
        }
      }
    }
  }

  private void OnQuery_SmithExceedConfirm_NO()
  {
  }

  private void OnQuery_AIM_R()
  {
    if (this.selectPageIndex >= this.maxPageIndex)
      this.selectPageIndex = 0;
    else
      ++this.selectPageIndex;
    this.RefreshUI();
  }

  private void OnQuery_AIM_L()
  {
    if (this.selectPageIndex <= 0)
      this.selectPageIndex = this.maxPageIndex;
    else
      --this.selectPageIndex;
    this.RefreshUI();
  }

  private enum UI
  {
    SPR_COUNT_0_ON,
    SPR_COUNT_1_ON,
    SPR_COUNT_2_ON,
    SPR_COUNT_3_ON,
    LBL_EXCEED_0,
    LBL_EXCEED_1,
    LBL_EXCEED_2,
    LBL_EXCEED_3,
    SPR_EXCEED_0_ON,
    SPR_EXCEED_1_ON,
    SPR_EXCEED_2_ON,
    SPR_EXCEED_3_ON,
    LBL_USE_MATERIAL_NAME,
    SCR_LAPIS_ROOT,
    GRD_LAPIS,
    OBJ_VALID_EXCEED_ROOT,
    OBJ_INVALID_EXCEED_ROOT,
    LBL_MAX_COUNT,
    OBJ_MATERIAL_ICON_ROOT,
    SPR_EXCEED_BTN_BG,
    SPR_EXCEED_GRAYOUT,
    OBJ_SELECT,
    BTN_AIM_R,
    BTN_AIM_L,
    BTN_AIM_R_INACTIVE,
    BTN_AIM_L_INACTIVE,
    LBL_SELECT_NOW,
    LBL_SELECT_MAX,
    OBJ_EXCEED_LIMITED,
    LBL_LIMITED,
  }
}
