// Decompiled with JetBrains decompiler
// Type: ShopGachaEquipListTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ShopGachaEquipListTop : GameSection
{
  private List<Coroutine> coroutineList = new List<Coroutine>();

  private void StopLoadCoroutine()
  {
    this.coroutineList.ForEach((Action<Coroutine>) (c =>
    {
      if (c == null)
        return;
      this.StopCoroutine(c);
    }));
    this.coroutineList.Clear();
  }

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    base.UpdateUI();
    uint materialID = (uint) ((object[]) GameSection.GetEventData())[0];
    CreateEquipItemTable.CreateEquipItemData[] equipItems = Singleton<CreateEquipItemTable>.I.GetSortedCreateEquipItemsByPart(materialID);
    this.SetTable((Enum) ShopGachaEquipListTop.UI.TBL_LIST, "GachaEquipItem", equipItems.Length, false, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      CreateEquipItemTable.CreateEquipItemData createEquipItemData = equipItems[i];
      uint equipItemId = createEquipItemData.equipItemID;
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipItemId);
      this.SetLabelText(t, (Enum) ShopGachaEquipListTop.UI.LBL_NAME, equipItemData.name);
      this.SetEquipmentTypeIcon(t, (Enum) ShopGachaEquipListTop.UI.SPR_TYPE_ICON, (Enum) ShopGachaEquipListTop.UI.SPR_TYPE_ICON_BG, (Enum) ShopGachaEquipListTop.UI.SPR_TYPE_ICON_RARITY, equipItemData);
      NeedMaterial[] needMaterial1 = createEquipItemData.needMaterial;
      NeedMaterial needMaterial2 = needMaterial1.Length >= 1 ? needMaterial1[0] : (NeedMaterial) null;
      if (needMaterial2 != null)
        this.SetItemIcon(needMaterial2.itemID, t, ShopGachaEquipListTop.UI.ITEM_ICON_1);
      NeedMaterial needMaterial3 = needMaterial1.Length >= 2 ? needMaterial1[1] : (NeedMaterial) null;
      if (needMaterial3 != null)
        this.SetItemIcon(needMaterial3.itemID, t, ShopGachaEquipListTop.UI.ITEM_ICON_2);
      this.coroutineList.Add(this.StartCoroutine(this.LoadEquipModel(t, (Enum) ShopGachaEquipListTop.UI.TEX_EQUIP_MODEL, equipItemData.id)));
      this.SetEvent(this.FindCtrl(t, (Enum) ShopGachaEquipListTop.UI.BTN_EQUIP_MODEL), "DETAIL_MAX_PARAM", (object) new object[3]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.GACHA_EQUIP_PREVIEW,
        (object) equipItemData,
        (object) materialID
      });
    }));
  }

  private void SetItemIcon(uint itemID, Transform trans, ShopGachaEquipListTop.UI target)
  {
    ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(itemID);
    ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.ITEM, itemData.iconID, new RARITY_TYPE?(itemData.rarity), this.FindCtrl(trans, (Enum) target), enemy_icon_id: itemData.enemyIconID, enemy_icon_id2: itemData.enemyIconID2);
    if (!Object.op_Inequality((Object) itemIcon, (Object) null))
      return;
    this.SetMaterialInfo(itemIcon.transform, REWARD_TYPE.ITEM, itemData.id, this.GetCtrl((Enum) ShopGachaEquipListTop.UI.PNL_MATERIAL_INFO));
  }

  private IEnumerator LoadEquipModel(Transform t, Enum _enum, uint item_id)
  {
    yield return (object) null;
    this.SetRenderEquipModel(t, _enum, item_id);
  }

  public void OnQuery_GACHA_DETAIL_MAX_PARAM_FROM_NEWS()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    uint materialId = (uint) eventData[0];
    int index = (int) eventData[1];
    CreateEquipItemTable.CreateEquipItemData[] equipItemsByPart = Singleton<CreateEquipItemTable>.I.GetSortedCreateEquipItemsByPart(materialId);
    if (index >= equipItemsByPart.Length || index <= -1)
    {
      GameSection.StopEvent();
    }
    else
    {
      uint equipItemId = equipItemsByPart[index].equipItemID;
      GameSection.ChangeEvent("DETAIL_MAX_PARAM", (object) new object[3]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.GACHA_EQUIP_PREVIEW,
        (object) Singleton<EquipItemTable>.I.GetEquipItemData(equipItemId),
        (object) materialId
      });
    }
  }

  private enum UI
  {
    SCR_LIST,
    TBL_LIST,
    PNL_MATERIAL_INFO,
    LBL_NAME,
    TEX_EQUIP_MODEL,
    ITEM_ICON_1,
    ITEM_ICON_2,
    SPR_TYPE_ICON,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    BTN_EQUIP_MODEL,
  }
}
