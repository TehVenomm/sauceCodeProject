// Decompiled with JetBrains decompiler
// Type: CrystalShopMaterialDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CrystalShopMaterialDetail : GameSection
{
  private const string NORMAL_DROP_EFF_NAME = "ef_ui_dropitem_silver_01";
  private const string RARE_DROP_EFF_NAME = "ef_ui_dropitem_gold_01";
  private const string BREAK_DROP_EFF_NAME = "ef_ui_dropitem_red_01";
  private Network.ProductData materialData;
  private string priceStr = string.Empty;
  private List<ItemSortData> datas;
  private int index;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.materialData = eventData[0] as Network.ProductData;
    this.priceStr = eventData[1] as string;
    this.index = (int) eventData[2];
    InventoryList<ItemInfo, Network.Item> list = ItemInfo.CreateList(this.materialData.items);
    this.datas = new List<ItemSortData>();
    for (LinkedListNode<ItemInfo> linkedListNode = list.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if (linkedListNode != null && linkedListNode.Value != null && linkedListNode.Value.tableData != null)
      {
        ItemSortData itemSortData = new ItemSortData();
        itemSortData.SetItem((object) linkedListNode.Value);
        this.datas.Add(itemSortData);
      }
    }
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    string resource_name = "BTN_SHOP_NORMAL1";
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_button = loadingQueue.Load(RESOURCE_CATEGORY.GACHA_BUTTON, resource_name);
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    GameObject gameObject = Object.Instantiate(lo_button.loadedObject) as GameObject;
    gameObject.transform.parent = this.FindCtrl(this._transform, (Enum) CrystalShopMaterialDetail.UI.OBJ_BUY);
    gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText(this._transform, (Enum) CrystalShopMaterialDetail.UI.LBL_PRICE, this.priceStr);
    this.SetActive((Enum) CrystalShopMaterialDetail.UI.SPR_SALE, this.materialData.offerType == 3);
    this.SetGrid((Enum) CrystalShopMaterialDetail.UI.GRD_DETAIL, (string) null, this.datas.Count, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      ItemSortData data = this.datas[i];
      this.SetItemIcon(t, data, i);
    }));
  }

  private bool IsRare(SortCompareData icon_base)
  {
    return icon_base != null && GameDefine.IsRare(icon_base.GetRarity());
  }

  private bool IsBreakReward(SortCompareData icon_base)
  {
    return icon_base != null && icon_base.GetCategory() == REWARD_CATEGORY.BREAK;
  }

  private void SetItemIcon(Transform holder, ItemSortData data, int event_data = 0)
  {
    ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
    RARITY_TYPE? rarity = new RARITY_TYPE?();
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
    EQUIPMENT_TYPE? magi_enable_icon_type = new EQUIPMENT_TYPE?();
    int icon_id = -1;
    int num = -1;
    if (data != null)
    {
      itemIconType = data.GetIconType();
      icon_id = data.GetIconID();
      rarity = new RARITY_TYPE?(data.GetRarity());
      element = data.GetIconElement();
      magi_enable_icon_type = data.GetIconMagiEnableType();
      num = data.GetNum();
      if (num == 1)
        num = -1;
    }
    bool is_new = false;
    switch (itemIconType)
    {
      case ITEM_ICON_TYPE.NONE:
        int enemy_icon_id = 0;
        if (itemIconType == ITEM_ICON_TYPE.ITEM)
          enemy_icon_id = Singleton<ItemTable>.I.GetItemData(data.GetTableID()).enemyIconID;
        ItemIcon itemIcon;
        if (data.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
          itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam()
          {
            icon_type = data.GetIconType(),
            icon_id = data.GetIconID(),
            rarity = new RARITY_TYPE?(data.GetRarity()),
            parent = holder,
            element = data.GetIconElement(),
            magi_enable_equip_type = data.GetIconMagiEnableType(),
            num = data.GetNum(),
            enemy_icon_id = enemy_icon_id,
            questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
          });
        else
          itemIcon = ItemIcon.Create(itemIconType, icon_id, rarity, holder, element, magi_enable_icon_type, num, "DROP", event_data, is_new, enemy_icon_id: enemy_icon_id);
        itemIcon.SetRewardBG(true);
        this.SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), this.GetCtrl((Enum) CrystalShopMaterialDetail.UI.PNL_MATERIAL_INFO));
        break;
      case ITEM_ICON_TYPE.ITEM:
      case ITEM_ICON_TYPE.QUEST_ITEM:
        if (data.GetUniqID() != 0UL)
        {
          is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(itemIconType, data.GetUniqID());
          goto case ITEM_ICON_TYPE.NONE;
        }
        goto case ITEM_ICON_TYPE.NONE;
      default:
        is_new = true;
        goto case ITEM_ICON_TYPE.NONE;
    }
  }

  private void OnQuery_BUY() => this.RequestEvent("BUY", (object) this.index);

  private enum UI
  {
    SPR_SALE,
    OBJ_BUY,
    SPR_BUY_NOW,
    LBL_PRICE,
    SCR_DETAIL,
    GRD_DETAIL,
    PNL_MATERIAL_INFO,
  }
}
