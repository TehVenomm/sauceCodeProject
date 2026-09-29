// Decompiled with JetBrains decompiler
// Type: ItemStorageSellConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
public class ItemStorageSellConfirm : ItemSellConfirm
{
  private ItemStorageTop.TAB_MODE tab;
  private List<string> uniqs = new List<string>();
  private List<int> nums = new List<int>();
  private ItemStorageSellConfirm.GO_BACK goBackTo;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "EquipItemExceedTable";
      foreach (string str in base.requireDataTable)
        yield return str;
    }
  }

  protected override bool isShowIconBG() => false;

  public override string overrideBackKeyEvent => "NO";

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.tab = (ItemStorageTop.TAB_MODE) eventData[0];
    this.sellData = eventData[1] as List<SortCompareData>;
    if (eventData.Length > 2)
      this.goBackTo = (ItemStorageSellConfirm.GO_BACK) eventData[2];
    this.isRareConfirm = false;
    this.isEquipConfirm = false;
    this.isExceedConfirm = false;
    this.isExceedEquipmentConfirm = false;
    int index = 0;
    for (int count = this.sellData.Count; index < count; ++index)
    {
      SortCompareData sortCompareData = this.sellData[index];
      if (!this.isRareConfirm || !this.isEquipConfirm || !this.isExceedConfirm && !this.isExceedEquipmentConfirm)
      {
        if (!this.isRareConfirm && GameDefine.IsRequiredAlertByRarity(sortCompareData.GetRarity()))
          this.isRareConfirm = true;
        if (!this.isEquipConfirm && sortCompareData.IsEquipping())
          this.isEquipConfirm = true;
        if (!this.isExceedConfirm && !this.isExceedEquipmentConfirm && sortCompareData.IsExceeded())
        {
          if (sortCompareData.GetMaterialType() == REWARD_TYPE.EQUIP_ITEM)
            this.isExceedEquipmentConfirm = true;
          else
            this.isExceedConfirm = true;
        }
      }
    }
    base.Initialize();
  }

  protected override void DrawIcon()
  {
    base.DrawIcon();
    NeedMaterial[] reward_ary = this.CreateNeedMaterialAry();
    this.SetGrid((Enum) ItemStorageSellConfirm.UI.GRD_REWARD_ICON, (string) null, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (i < reward_ary.Length)
      {
        NeedMaterial needMaterial = reward_ary[i];
        ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.ITEM, needMaterial.itemID, t, needMaterial.num, "NONE");
        rewardItemIcon.SetRewardBG(true);
        Transform ctrl = this.GetCtrl((Enum) ItemStorageSellConfirm.UI.GRD_REWARD_ICON);
        this.SetMaterialInfo(rewardItemIcon.transform, REWARD_TYPE.ITEM, needMaterial.itemID, ctrl);
      }
      else
        this.SetActive(t, false);
    }));
    this.SetActive((Enum) ItemStorageSellConfirm.UI.STR_NON_REWARD, reward_ary.Length == 0);
  }

  protected virtual NeedMaterial[] CreateNeedMaterialAry()
  {
    SortCompareData[] array = this.sellData.ToArray();
    List<NeedMaterial> reward = new List<NeedMaterial>();
    Action<SortCompareData> action = (Action<SortCompareData>) (_data =>
    {
      if (!(_data.GetItemData() is EquipItemInfo itemData2))
        return;
      uint lapis_id = 0;
      EquipItemExceedTable.EquipItemExceedData equipItemExceedData = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedData(itemData2.tableData.rarity, itemData2.tableData.getType, itemData2.tableData.eventId);
      if (equipItemExceedData != null)
        lapis_id = equipItemExceedData.exchangeItemId;
      if (lapis_id == 0U || Singleton<ItemTable>.I.GetItemData(lapis_id) == null)
        return;
      NeedMaterial needMaterial = reward.Find((Predicate<NeedMaterial>) (regist_lapis => (int) regist_lapis.itemID == (int) lapis_id));
      if (needMaterial == null)
        reward.Add(new NeedMaterial(lapis_id, 1));
      else
        ++needMaterial.num;
    });
    Array.ForEach<SortCompareData>(array, action);
    return reward.ToArray();
  }

  private void OnQuery_NO()
  {
    this.ChangeEventForGoBack();
    GameSection.SetEventData((object) this.sellData);
  }

  private void OnQuery_YES()
  {
    GameSection.SetEventData((object) null);
    this.uniqs.Clear();
    this.sellData.ForEach((Action<SortCompareData>) (sort_data =>
    {
      this.uniqs.Add(sort_data.GetUniqID().ToString());
      this.nums.Add(1);
    }));
    if (this.isRareConfirm || this.isEquipConfirm || this.isExceedConfirm || this.isExceedEquipmentConfirm)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(this.sectionData.GetText("TEXT_CONFIRM"));
      if (this.isRareConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_RARE"));
      if (this.isEquipConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_EQUIP"));
      if (this.isExceedConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_EXCEED"));
      if (this.isExceedEquipmentConfirm)
        stringBuilder.AppendLine(this.sectionData.GetText("TEXT_INCLUDE_EXCEED_EQUIP"));
      stringBuilder.AppendLine("");
      stringBuilder.Append(this.sectionData.GetText("TEXT_GROW"));
      GameSection.ChangeEvent("INCLUDE_RARE_CONFIRM", (object) stringBuilder.ToString());
    }
    else
    {
      this.ChangeEventForGoBack();
      GameSection.SetEventData((object) null);
      this.SendSell();
    }
  }

  private void SendSell()
  {
    if (this.tab == ItemStorageTop.TAB_MODE.EQUIP)
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellEquipItem(this.uniqs, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    }
    else if (this.tab == ItemStorageTop.TAB_MODE.MATERIAL)
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellItem(this.uniqs, this.nums, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    }
    else
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellSkillItem(this.uniqs, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    }
  }

  public void OnQuery_ItemStorageSellIncludeRareConfirm_YES()
  {
    this.ChangeEventForGoBack();
    GameSection.SetEventData((object) null);
    this.SendSell();
  }

  public void OnQuery_ItemStorageSellIncludeRareConfirm_NO() => this.ChangeEventForGoBack();

  protected virtual void ChangeEventForGoBack()
  {
    GameSection.ChangeEvent($"{GameSceneEvent.current.eventName}_{(object) this.goBackTo}");
  }

  public new enum UI
  {
    STR_INCLUDE_RARE,
    STR_MAIN_TEXT,
    STR_TITLE_R,
    GRD_ICON,
    LBL_TOTAL,
    OBJ_GOLD,
    BTN_0,
    BTN_1,
    BTN_CENTER,
    SCR_ICON,
    GRD_REWARD_ICON,
    STR_NON_REWARD,
  }

  public enum GO_BACK
  {
    TOP,
    SELL,
  }
}
