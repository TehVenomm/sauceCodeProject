// Decompiled with JetBrains decompiler
// Type: InGameItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class InGameItem : GameSection
{
  private bool isInActiveRotate;
  private ItemStorageTop.SHOW_INVENTORY_MODE showInventoryMode;
  private InGameItem.InGameUseItemInventory inventory;

  public override void Initialize()
  {
    base.Initialize();
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
    {
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
      this.isInActiveRotate = true;
    }
    this.PlayTween((Enum) InGameItem.UI.OBJ_CAPTION_3, is_input_block: false);
  }

  public override void Exit()
  {
    base.Exit();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  public override void UpdateUI()
  {
    this.UpdateAnchors();
    base.UpdateUI();
    this.SetToggle((Enum) InGameItem.UI.TGL_CHANGE_INVENTORY, true);
    this.inventory = new InGameItem.InGameUseItemInventory();
    this.SetDynamicList((Enum) this.SelectListTarget(this.showInventoryMode), (string) null, this.inventory.datas.Length, false, (Func<int, bool>) (i =>
    {
      SortCompareData data = this.inventory.datas[i];
      return data != null && data.IsPriority(this.inventory.sortSettings.orderTypeAsc);
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycre) =>
    {
      ItemIcon icon = this.inventory.CreateIcon(new object[4]
      {
        (object) this.inventory.datas[i],
        (object) t,
        (object) i,
        (object) this.showInventoryMode
      });
      if (!Object.op_Inequality((Object) icon, (Object) null))
        return;
      icon.toggleSelectFrame.onChange.Clear();
      icon.toggleSelectFrame.onChange.Add(new EventDelegate((MonoBehaviour) this, "IconToggleChange"));
      this.SetEvent(icon.transform, "DETAIL", i);
      this.SetLongTouch(icon.transform, "DETAIL", (object) i);
    }));
    ((Component) this.GetCtrl((Enum) InGameItem.UI.SCR_INVENTORY)).GetComponent<UIPanel>().Refresh();
    if (this.isInActiveRotate && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      this.Reposition(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    this.isInActiveRotate = false;
  }

  private void Reposition(bool isPortrait)
  {
    foreach (UIScreenRotationHandler component in ((Component) this.GetCtrl((Enum) InGameItem.UI.BG)).GetComponents<UIScreenRotationHandler>())
      component.InvokeRotate();
    ((Component) this.GetCtrl((Enum) InGameItem.UI.OBJ_BACK)).GetComponent<UIScreenRotationHandler>().InvokeRotate();
    ((Component) this.GetCtrl((Enum) InGameItem.UI.BTN_ITEM_SHOP)).GetComponent<UIScreenRotationHandler>().InvokeRotate();
    ((Component) this.GetCtrl((Enum) InGameItem.UI.BG)).GetComponent<UIRect>().UpdateAnchors();
    this.UpdateAnchors();
    ((Component) this.GetCtrl((Enum) InGameItem.UI.SCR_INVENTORY)).GetComponent<UIScrollView>().ResetPosition();
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.RefreshUI());
  }

  private void OnScreenRotate(bool isPortrait)
  {
    this.isInActiveRotate = !Object.op_Inequality((Object) this.transferUI, (Object) null) ? !((Component) this.collectUI).gameObject.activeInHierarchy : !((Component) this.transferUI).gameObject.activeInHierarchy;
    if (this.isInActiveRotate)
      return;
    this.Reposition(isPortrait);
  }

  private InGameItem.UI SelectListTarget(
    ItemStorageTop.SHOW_INVENTORY_MODE show_detail_icon)
  {
    this.SetActive((Enum) InGameItem.UI.GRD_INVENTORY, true);
    this.SetActive((Enum) InGameItem.UI.GRD_INVENTORY_SMALL, false);
    return InGameItem.UI.GRD_INVENTORY;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.SetDirty((Enum) this.SelectListTarget(this.showInventoryMode));
      this.RefreshUI();
    }
    base.OnNotify(flags);
  }

  private void OnQuery_CHANGE_INVENTORY()
  {
    this.showInventoryMode = this.showInventoryMode + 1 != ItemStorageTop.SHOW_INVENTORY_MODE.MAX ? this.showInventoryMode + 1 : ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS;
    this.SetDirty((Enum) InGameItem.UI.GRD_INVENTORY);
    this.SetDirty((Enum) InGameItem.UI.GRD_INVENTORY_SMALL);
    this.RefreshUI();
  }

  private void OnQuery_DETAIL()
  {
    GameSection.ChangeEvent("USE_ITEM_SELECT", (object) this.inventory.datas[(int) GameSection.GetEventData()]);
  }

  private void OnQuery_ITEM_SHOP()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ShopManager>.I.SendGetShop((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private enum UI
  {
    BG,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    OBJ_BACK,
    OBJ_CAPTION_3,
    TGL_CHANGE_INVENTORY,
    BTN_ITEM_SHOP,
  }

  public class InGameUseItemInventory : ItemStorageTop.UseItemInventory
  {
    protected override bool IsUseItemType(ITEM_TYPE item_type) => item_type == ITEM_TYPE.USE_ITEM;

    public override ItemIcon CreateIcon(object[] data)
    {
      SortCompareData sortCompareData = data[0] as SortCompareData;
      Transform parent = data[1] as Transform;
      int event_data = (int) data[2];
      bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ITEM, sortCompareData.GetUniqID());
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(sortCompareData.GetTableID());
      ItemStorageTop.SHOW_INVENTORY_MODE showInventoryMode = (ItemStorageTop.SHOW_INVENTORY_MODE) data[3];
      return ItemIconDetail.CreateMaterialIcon(sortCompareData.GetIconType(), sortCompareData.GetIconID(), new RARITY_TYPE?(sortCompareData.GetRarity()), itemData, showInventoryMode == ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS, parent, sortCompareData.GetNum(), sortCompareData.GetName(), "SELECT", event_data, is_new: is_new);
    }
  }
}
