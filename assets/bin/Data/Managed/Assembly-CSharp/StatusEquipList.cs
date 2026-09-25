// Decompiled with JetBrains decompiler
// Type: StatusEquipList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StatusEquipList : GameSection
{
  private int obtainedNum;
  private int currentPageIndex;
  private static readonly int ONE_PAGE_EQUIP_NUM = 25;

  public override void Initialize()
  {
    this.currentPageIndex = 0;
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    Singleton<EquipItemTable>.I.CreateTableForEquipList();
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.obtainedNum = MonoBehaviourSingleton<AchievementManager>.I.GetEquipItemCollectionNum();
    this.SetLabelText((Enum) StatusEquipList.UI.LBL_CURRENT_NUM, this.obtainedNum.ToString());
    int equipListCount = Singleton<EquipItemTable>.I.GetEquipListCount();
    this.SetLabelText((Enum) StatusEquipList.UI.LBL_MAX_NUM, equipListCount.ToString());
    this.SetPageNumText((Enum) StatusEquipList.UI.LBL_PAGE_MAX, equipListCount);
    this.SetActive((Enum) StatusEquipList.UI.BTN_ENEMY_COLLECTION_LIST, true);
    this.InitializeCaption();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.UpdateUI(0);
    base.UpdateUI();
  }

  public int GetMaxPageNum()
  {
    return Singleton<EquipItemTable>.I.GetEquipListCount() / StatusEquipList.ONE_PAGE_EQUIP_NUM + (Singleton<EquipItemTable>.I.GetEquipListCount() % StatusEquipList.ONE_PAGE_EQUIP_NUM != 0 ? 1 : 0);
  }

  public bool UpdateUI(int pageIndex)
  {
    int maxPageNum = this.GetMaxPageNum();
    if (maxPageNum <= pageIndex)
      return false;
    this.currentPageIndex = pageIndex;
    this.SkipTween((Enum) StatusEquipList.UI.SPR_SELECT_WEAPON);
    this.SkipTween((Enum) StatusEquipList.UI.SPR_SELECT_DEF);
    this.SetPageNumText((Enum) StatusEquipList.UI.LBL_PAGE_MAX, maxPageNum);
    this.UpdateInventory();
    this.UpdateAnchors();
    return true;
  }

  protected void OnQuery_EQUIP_LIST_L()
  {
    --this.currentPageIndex;
    if (this.currentPageIndex < 0)
      this.currentPageIndex = this.GetMaxPageNum() - 1;
    this.UpdateInventory();
  }

  protected void OnQuery_EQUIP_LIST_R()
  {
    ++this.currentPageIndex;
    if (this.currentPageIndex > this.GetMaxPageNum() - 1)
      this.currentPageIndex = 0;
    this.UpdateInventory();
  }

  private void UpdateInventory()
  {
    EquipItemTable.EquipItemData[] items = (EquipItemTable.EquipItemData[]) null;
    int start = this.currentPageIndex * StatusEquipList.ONE_PAGE_EQUIP_NUM;
    int last = start + StatusEquipList.ONE_PAGE_EQUIP_NUM;
    items = this.GetEquips(start, last);
    if (items == null)
      return;
    this.SetPageNumText((Enum) StatusEquipList.UI.LBL_PAGE_NOW, this.currentPageIndex + 1);
    this.SetDynamicList((Enum) StatusEquipList.UI.GRD_INVENTORY, "", items.Length, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) =>
    {
      this.SetActive(t, true);
      EquipItemTable.EquipItemData equipItem = items[i];
      EquipItemSortData sortData = new EquipItemSortData();
      EquipItemInfo equipItemInfo = new EquipItemInfo();
      equipItemInfo.tableData = equipItem;
      equipItemInfo.SetDefaultData();
      sortData.SetItem((object) equipItemInfo);
      ITEM_ICON_TYPE iconType = ItemIcon.GetItemIconType(equipItem.type);
      int num = !MonoBehaviourSingleton<AchievementManager>.I.CheckEquipItemCollection(equipItem) ? 1 : 0;
      if (num != 0)
        iconType = ITEM_ICON_TYPE.UNKNOWN;
      bool isNew = false;
      GET_TYPE getType = GET_TYPE.PAY;
      if (equipItem != null)
        getType = equipItem.getType;
      ItemIcon smallListItemIcon = ItemIconDetailSmall.CreateSmallListItemIcon(iconType, sortData, t, isNew, start + i + 1, getType);
      if (num == 0)
      {
        ((Behaviour) smallListItemIcon.button).enabled = true;
        this.SetEvent(smallListItemIcon._transform, "DETAIL", (object) new object[2]
        {
          (object) ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST,
          (object) equipItem
        });
      }
      else
      {
        ((Behaviour) smallListItemIcon.button).enabled = false;
        this.SetEvent(smallListItemIcon._transform, string.Empty, 0);
      }
    }));
  }

  private EquipItemTable.EquipItemData[] GetEquips(int start, int last)
  {
    if (last == 0)
      return new EquipItemTable.EquipItemData[0];
    int equipListCount = Singleton<EquipItemTable>.I.GetEquipListCount();
    if (equipListCount <= last)
      last = equipListCount;
    int num = last - start;
    List<EquipItemTable.EquipItemData> equipItemDataList = new List<EquipItemTable.EquipItemData>();
    for (int index = 0; index < num; ++index)
      equipItemDataList.Add(Singleton<EquipItemTable>.I.GetEquipListData(start + index));
    return equipItemDataList.ToArray();
  }

  private void InitializeCaption()
  {
    Transform ctrl = this.GetCtrl((Enum) StatusEquipList.UI.OBJ_CAPTION_2);
    string text = this.sectionData.GetText("CAPTION");
    this.SetLabelText(ctrl, (Enum) StatusEquipList.UI.LBL_CAPTION, text);
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  protected enum UI
  {
    BTN_SORT,
    BTN_EQUIP_LIST_L,
    BTN_EQUIP_LIST_R,
    LBL_CURRENT_NUM,
    LBL_MAX_NUM,
    LBL_PAGE_NOW,
    LBL_PAGE_MAX,
    GRD_INVENTORY,
    SPR_SELECT_WEAPON,
    SPR_SELECT_DEF,
    OBJ_CAPTION_2,
    LBL_CAPTION,
    BTN_ENEMY_COLLECTION_LIST,
  }
}
