// Decompiled with JetBrains decompiler
// Type: StatusEnemyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
[Obsolete]
public class StatusEnemyList : GameSection
{
  private StatusEnemyList.UI[] rarityTable = new StatusEnemyList.UI[4]
  {
    StatusEnemyList.UI.OBJ_COMMON_FRAME,
    StatusEnemyList.UI.OBJ_RARE_FRAME,
    StatusEnemyList.UI.OBJ_BIGRARE_FRAME,
    StatusEnemyList.UI.OBJ_FIELD_FRAME
  };
  private List<EnemyCollectionTable.EnemyCollectionData> currentRegionCollectionItems;
  private List<EnemyCollectionTable.EnemyCollectionData> regionCollectionSortItems;
  private List<RegionTable.Data> unlockRegion;
  private List<AchievementCounter> achievementCounter;
  private int currentPageIndex;
  private static readonly int ONE_PAGE_EQUIP_NUM = 5;
  private Transform popup;
  private int popupIndex;
  private List<string> fields;

  public override void Initialize()
  {
    this.currentPageIndex = 0;
    this.popupIndex = 0;
    this.achievementCounter = MonoBehaviourSingleton<AchievementManager>.I.monsterCollectionList;
    this.SetText((Enum) StatusEnemyList.UI.STR_MONSTER, "NORMAL");
    this.SetText((Enum) StatusEnemyList.UI.STR_BIGMONSTER, "HAPPEN");
    this.SetText((Enum) StatusEnemyList.UI.STR_RAREMONSTER, "HAPPEN_RARE");
    this.SetText((Enum) StatusEnemyList.UI.STR_FIELDMONSTER, "FIELD_CHANGE");
    this.unlockRegion = ((IEnumerable<uint>) MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdList()).Select<uint, RegionTable.Data>((Func<uint, RegionTable.Data>) (x => Singleton<RegionTable>.I.GetData(x))).ToList<RegionTable.Data>();
    this.fields = this.unlockRegion.Select<RegionTable.Data, string>((Func<RegionTable.Data, string>) (x => x.regionName)).ToList<string>();
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
    return Singleton<EquipItemTable>.I.GetEquipListCount() / StatusEnemyList.ONE_PAGE_EQUIP_NUM + 1;
  }

  public bool UpdateUI(int pageIndex)
  {
    int maxPageNum = this.GetMaxPageNum();
    if (maxPageNum <= pageIndex)
      return false;
    this.currentPageIndex = pageIndex;
    this.SetPageNumText((Enum) StatusEnemyList.UI.LBL_PAGE_MAX, maxPageNum);
    this.SetLabelText((Enum) StatusEnemyList.UI.LBL_TARGET_FIELD, this.fields[this.popupIndex]);
    this.UpdateRegion();
    this.UpdateInventory();
    this.UpdateAnchors();
    return true;
  }

  protected void OnQuery_LIST_L()
  {
    --this.currentPageIndex;
    this.UpdateInventory();
  }

  protected void OnQuery_LIST_R()
  {
    ++this.currentPageIndex;
    this.UpdateInventory();
  }

  private void OnQuery_FIELD_LIST() => this.ShowLevelPopup();

  private void UpdateRegion()
  {
    uint regionId = this.unlockRegion.First<RegionTable.Data>((Func<RegionTable.Data, bool>) (x => x.regionName == this.fields[this.popupIndex])).regionId;
    this.currentRegionCollectionItems = Singleton<EnemyCollectionTable>.I.GetEnemyCollectionDataByRegion(regionId).OrderBy<EnemyCollectionTable.EnemyCollectionData, uint>((Func<EnemyCollectionTable.EnemyCollectionData, uint>) (data => data.id)).ToList<EnemyCollectionTable.EnemyCollectionData>();
    int num = 0;
    foreach (EnemyCollectionTable.EnemyCollectionData regionCollectionItem in this.currentRegionCollectionItems)
    {
      EnemyCollectionTable.EnemyCollectionData item = regionCollectionItem;
      if (this.achievementCounter.Find((Predicate<AchievementCounter>) (x => (long) x.subType == (long) item.id)) != null)
        ++num;
    }
    this.SetLabelText((Enum) StatusEnemyList.UI.LBL_CURRENT_NUM, $"{num}/{this.currentRegionCollectionItems.Count}");
  }

  private void UpdateInventory()
  {
    IEnumerable<EnemyCollectionTable.EnemyCollectionData> enemyCollectionDatas1 = this.currentRegionCollectionItems.Where<EnemyCollectionTable.EnemyCollectionData>((Func<EnemyCollectionTable.EnemyCollectionData, bool>) (x => x.collectionType == COLLECTION_TYPE.NORMAL));
    IEnumerable<EnemyCollectionTable.EnemyCollectionData> enemyCollectionDatas2 = this.currentRegionCollectionItems.Where<EnemyCollectionTable.EnemyCollectionData>((Func<EnemyCollectionTable.EnemyCollectionData, bool>) (x => x.collectionType == COLLECTION_TYPE.HAPPEN));
    IEnumerable<EnemyCollectionTable.EnemyCollectionData> enemyCollectionDatas3 = this.currentRegionCollectionItems.Where<EnemyCollectionTable.EnemyCollectionData>((Func<EnemyCollectionTable.EnemyCollectionData, bool>) (x => x.collectionType == COLLECTION_TYPE.HAPPEN_RARE));
    IEnumerable<EnemyCollectionTable.EnemyCollectionData> enemyCollectionDatas4 = this.currentRegionCollectionItems.Where<EnemyCollectionTable.EnemyCollectionData>((Func<EnemyCollectionTable.EnemyCollectionData, bool>) (x => x.collectionType == COLLECTION_TYPE.FIELD_CHANGE));
    this.regionCollectionSortItems = enemyCollectionDatas1.Concat<EnemyCollectionTable.EnemyCollectionData>(enemyCollectionDatas2).Concat<EnemyCollectionTable.EnemyCollectionData>(enemyCollectionDatas3).Concat<EnemyCollectionTable.EnemyCollectionData>(enemyCollectionDatas4).ToList<EnemyCollectionTable.EnemyCollectionData>();
    int page_num = Mathf.CeilToInt((float) Mathf.Max(Mathf.Max(Mathf.Max(enemyCollectionDatas1.Count<EnemyCollectionTable.EnemyCollectionData>(), enemyCollectionDatas2.Count<EnemyCollectionTable.EnemyCollectionData>()), enemyCollectionDatas3.Count<EnemyCollectionTable.EnemyCollectionData>()), enemyCollectionDatas4.Count<EnemyCollectionTable.EnemyCollectionData>()) / (float) StatusEnemyList.ONE_PAGE_EQUIP_NUM);
    this.SetPageNumText((Enum) StatusEnemyList.UI.LBL_PAGE_MAX, page_num);
    if (this.currentPageIndex < 0)
      this.currentPageIndex = page_num - 1;
    if (this.currentPageIndex >= page_num)
      this.currentPageIndex = 0;
    int start = this.currentPageIndex * StatusEnemyList.ONE_PAGE_EQUIP_NUM;
    if (this.currentRegionCollectionItems == null || this.currentRegionCollectionItems.Count == 0)
      return;
    this.SetPageNumText((Enum) StatusEnemyList.UI.LBL_PAGE_NOW, this.currentPageIndex + 1);
    this.CreateIcon(enemyCollectionDatas1, StatusEnemyList.UI.GRD_NORMAL_INVENTORY, start);
    this.CreateIcon(enemyCollectionDatas2, StatusEnemyList.UI.GRD_BIG_INVENTORY, start);
    this.CreateIcon(enemyCollectionDatas3, StatusEnemyList.UI.GRD_RARE_INVENTORY, start);
    this.CreateIcon(enemyCollectionDatas4, StatusEnemyList.UI.GRD_FIELD_INVENTORY, start);
  }

  private void CreateIcon(
    IEnumerable<EnemyCollectionTable.EnemyCollectionData> items,
    StatusEnemyList.UI targetType,
    int start)
  {
    if (items.Count<EnemyCollectionTable.EnemyCollectionData>() > start)
    {
      List<EnemyCollectionTable.EnemyCollectionData> indexItems = items.Skip<EnemyCollectionTable.EnemyCollectionData>(start).ToList<EnemyCollectionTable.EnemyCollectionData>();
      int item_num = Mathf.Min(indexItems.Count, StatusEnemyList.ONE_PAGE_EQUIP_NUM);
      if (item_num > 0)
      {
        this.SetActive((Enum) targetType, true);
        this.SetDynamicList((Enum) targetType, "EnemyCollectionIcon", item_num, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) =>
        {
          this.SetActive(t, true);
          bool is_visible = this.achievementCounter.Find((Predicate<AchievementCounter>) (x => (long) x.subType == (long) indexItems[i].id)) == null;
          this.SetActive(t, (Enum) StatusEnemyList.UI.OBJ_UNKNOWN, is_visible);
          this.SetActive(t, (Enum) StatusEnemyList.UI.TEX_ICON, !is_visible);
          this.SetFrame(t, (int) (targetType - 10));
          if (!is_visible)
          {
            EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(indexItems[i].id).FirstOrDefault<EnemyTable.EnemyData>();
            this.SetEnemyIcon(t, (Enum) StatusEnemyList.UI.TEX_ICON, enemyData.iconId);
          }
          object[] event_data = new object[2]
          {
            (object) indexItems[i].id,
            (object) this.regionCollectionSortItems
          };
          this.SetEvent(t, "DETAIL", (object) event_data);
        }));
      }
      else
        this.SetActive((Enum) targetType, false);
    }
    else
      this.SetActive((Enum) targetType, false);
  }

  private void SetFrame(Transform iconRoot, int rarity)
  {
    rarity = Mathf.Clamp(rarity, 0, this.rarityTable.Length - 1);
    for (int index = 0; index < this.rarityTable.Length; ++index)
      this.SetActive(iconRoot, (Enum) this.rarityTable[index], rarity == index);
  }

  private void ShowLevelPopup()
  {
    if (Object.op_Equality((Object) this.popup, (Object) null))
      this.popup = ((Component) ((Component) this.GetCtrl((Enum) StatusEnemyList.UI.POP_TARGET_FIELD)).GetComponentInChildren<UIScrollablePopupList>(true)).transform;
    if (Object.op_Equality((Object) this.popup, (Object) null))
      return;
    ((Component) this.popup).gameObject.SetActive(true);
    bool[] button_enable = new bool[this.fields.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = true;
    UIScrollablePopupList.CreatePopup(this.popup, this.GetCtrl((Enum) StatusEnemyList.UI.POP_TARGET_FIELD), 4, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.fields.ToArray(), button_enable, this.popupIndex, (Action<int>) (index =>
    {
      this.popupIndex = index;
      this.UpdateRegion();
      this.RefreshUI();
    }));
  }

  private void InitializeCaption()
  {
    Transform ctrl = this.GetCtrl((Enum) StatusEnemyList.UI.OBJ_CAPTION_2);
    string text = this.sectionData.GetText("CAPTION");
    this.SetLabelText(ctrl, (Enum) StatusEnemyList.UI.LBL_CAPTION, text);
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
    BTN_ENEMY_COLLECTION,
    BTN_EQUIP_LIST_L,
    BTN_EQUIP_LIST_R,
    LBL_CURRENT_NUM,
    LBL_MAX_NUM,
    LBL_TARGET_FIELD,
    LBL_PAGE_NOW,
    LBL_PAGE_MAX,
    POP_TARGET_FIELD,
    GRD_NORMAL_INVENTORY,
    GRD_BIG_INVENTORY,
    GRD_RARE_INVENTORY,
    GRD_FIELD_INVENTORY,
    OBJ_COMMON_FRAME,
    OBJ_RARE_FRAME,
    OBJ_BIGRARE_FRAME,
    OBJ_FIELD_FRAME,
    OBJ_UNKNOWN,
    STR_MONSTER,
    STR_BIGMONSTER,
    STR_RAREMONSTER,
    STR_FIELDMONSTER,
    TEX_ICON,
    OBJ_CAPTION_2,
    LBL_CAPTION,
  }
}
