// Decompiled with JetBrains decompiler
// Type: ItemDetailTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class ItemDetailTop : GameSection
{
  private SortCompareData data;
  private Transform detailBase;
  private ItemDetailTop.UI[] difficult = new ItemDetailTop.UI[10]
  {
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_1,
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_2,
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_3,
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_4,
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_5,
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_6,
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_7,
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_8,
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_9,
    ItemDetailTop.UI.OBJ_DIFFICULT_STAR_10
  };
  private ItemDetailTop.UI[] mission = new ItemDetailTop.UI[6]
  {
    ItemDetailTop.UI.OBJ_TOP_CROWN1,
    ItemDetailTop.UI.SPR_CROWN_1,
    ItemDetailTop.UI.OBJ_TOP_CROWN2,
    ItemDetailTop.UI.SPR_CROWN_2,
    ItemDetailTop.UI.OBJ_TOP_CROWN3,
    ItemDetailTop.UI.SPR_CROWN_3
  };
  private const int RECOMMEND_ITEM_MAX = 3;
  private List<ItemDetailTop.ItemDestination> m_ItemDestinations = new List<ItemDetailTop.ItemDestination>();
  private int searchEnemySpecies;
  private QuestTable.QuestTableData jumpQuest;
  private FieldMapTable.FieldMapTableData jumpField;
  private EventData[] jump_event_list_datas;
  private List<PointShop> pointShopList = new List<PointShop>();
  private ItemDetailTop.PointShopData selectedPointShopdata;
  private ItemDetailTop.ItemDestination selectedPointShopDestination;
  private int itemId = -1;
  private bool backSection;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "ItemToQuestTable";
      yield return "QuestToFieldTable";
      yield return "ItemToFieldTable";
      yield return "EquipItemExceedTable";
      yield return "FieldMapTable";
      yield return "QuestTable";
    }
  }

  public override void Initialize()
  {
    this.data = GameSection.GetEventData() as SortCompareData;
    Protocol.Force(new System.Action(this.GetItemData));
  }

  private void GetItemData()
  {
    ItemToQuestTableModel.RequestForm postData = new ItemToQuestTableModel.RequestForm();
    this.itemId = (int) this.data.GetTableID();
    postData.itemId = this.itemId.ToString();
    Protocol.Send<ItemToQuestTableModel.RequestForm, ItemToQuestTableModel>("ajax/datatable/itemtoquest", postData, (Action<ItemToQuestTableModel>) (res =>
    {
      if (res.Error != Error.None)
        return;
      Singleton<ItemToQuestTable>.I.AddTableFromAPI(this.data.GetTableID(), res.result.questIds);
      if (this.data is ItemSortData)
        GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.ITEM, this.data.GetUniqID());
      else if (this.data is AbilityItemSortData)
        GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.ABILITY_ITEM, this.data.GetUniqID());
      else if (this.data is AccessorySortData)
        GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.ACCESSORY, this.data.GetUniqID());
      this.StartCoroutine(this.SendGetInfos());
    }));
  }

  private IEnumerator SendGetInfos()
  {
    bool isFinishChallengeInfo = false;
    MonoBehaviourSingleton<PartyManager>.I.SendGetChallengeInfo((Action<bool, Error>) ((is_success, err) => isFinishChallengeInfo = true));
    if (!isFinishChallengeInfo)
      yield return (object) null;
    bool isFinishSendPointShop = false;
    MonoBehaviourSingleton<UserInfoManager>.I.PointShopManager.SendGetPointShops((Action<bool, List<PointShop>>) ((isSuccess, resultList) =>
    {
      if (!isSuccess)
        return;
      this.pointShopList = resultList;
      isFinishSendPointShop = true;
    }));
    if (!isFinishSendPointShop)
      yield return (object) null;
    bool isRecvQuest = false;
    MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeList(new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam()
    {
      enemyLevel = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_ITEM_LEVEL_MAX
    }, (Action<bool, Error>) ((is_success, err) => isRecvQuest = true), false);
    while (!isRecvQuest)
      yield return (object) null;
    this.CreateDestinationList();
    base.Initialize();
  }

  private void CreateDestinationList()
  {
    if (this.data == null)
      return;
    uint tableId = this.data.GetTableID();
    this.m_ItemDestinations.Clear();
    ItemToQuestTable.RecommendQuestData recommendQuest = Singleton<ItemToQuestTable>.I.GetRecommendQuest(tableId);
    if (recommendQuest != null)
    {
      foreach (QuestTable.QuestTableData questTableData in recommendQuest.recommendData)
      {
        if (questTableData != null)
          this.m_ItemDestinations.Add(new ItemDetailTop.ItemDestination()
          {
            type = ItemDetailTop.ItemDestination.TYPE.AdmissionQuest,
            quest = questTableData
          });
      }
    }
    int num1 = 3;
    if (num1 > 0)
    {
      QuestTable.QuestTableData[] questTableFromItemId = Singleton<ItemToQuestTable>.I.GetHappenQuestTableFromItemID(tableId);
      if (questTableFromItemId != null)
      {
        List<ItemDetailTop.ItemDestination> collection = new List<ItemDetailTop.ItemDestination>();
        List<ItemDetailTop.ItemDestination> itemDestinationList = new List<ItemDetailTop.ItemDestination>();
        foreach (QuestTable.QuestTableData questTableData in questTableFromItemId)
        {
          FieldMapTable.FieldMapTableData[] idWithClosedField = Singleton<QuestToFieldTable>.I.GetFieldMapTableFromQuestIdWithClosedField(questTableData.questID);
          if (idWithClosedField != null)
          {
            foreach (FieldMapTable.FieldMapTableData field_table in idWithClosedField)
            {
              if (Singleton<ItemToFieldTable>.I.IsOpenMap(field_table))
                itemDestinationList.Add(new ItemDetailTop.ItemDestination()
                {
                  type = ItemDetailTop.ItemDestination.TYPE.HappenField,
                  quest = questTableData,
                  field = field_table
                });
              else
                collection.Add(new ItemDetailTop.ItemDestination()
                {
                  type = ItemDetailTop.ItemDestination.TYPE.HappenFieldUnknown,
                  quest = questTableData,
                  field = field_table
                });
            }
          }
        }
        itemDestinationList.AddRange((IEnumerable<ItemDetailTop.ItemDestination>) collection);
        int num2 = Mathf.Min(itemDestinationList.Count, num1);
        for (int index = 0; index < num2; ++index)
          this.m_ItemDestinations.Add(itemDestinationList[index]);
      }
    }
    if (this.m_ItemDestinations.Count < 1)
    {
      int num3 = 3;
      QuestTable.QuestTableData[] distinctQuestFromItemId = Singleton<ItemToQuestTable>.I.GetDistinctQuestFromItemID(tableId, new QUEST_TYPE?(QUEST_TYPE.ORDER));
      if (distinctQuestFromItemId != null)
      {
        int num4 = Mathf.Min(num3, distinctQuestFromItemId.Length);
        for (int index = 0; index < num4; ++index)
        {
          QuestTable.QuestTableData questTableData = distinctQuestFromItemId[index];
          if (questTableData != null)
            this.m_ItemDestinations.Add(new ItemDetailTop.ItemDestination()
            {
              type = ItemDetailTop.ItemDestination.TYPE.DeniedQuest,
              quest = questTableData
            });
        }
      }
    }
    int max_num = 3;
    int num5 = 0;
    if (max_num > 0)
    {
      ItemToFieldTable.RecommendFieldData recommendField = Singleton<ItemToFieldTable>.I.GetRecommendField(tableId, max_num, true);
      if (recommendField != null && recommendField.dropFieldData != null)
      {
        foreach (ItemToFieldTable.ItemDetailToFieldData detailToFieldData in recommendField.dropFieldData)
        {
          this.m_ItemDestinations.Add(new ItemDetailTop.ItemDestination()
          {
            type = ItemDetailTop.ItemDestination.TYPE.DropField,
            recommend_field = detailToFieldData,
            field = detailToFieldData.mapData
          });
          ++num5;
        }
      }
    }
    int trim_count = num5 <= 0 ? 3 : 0;
    if (trim_count > 0)
    {
      ItemToFieldTable.CandidateField[] candidateField1 = Singleton<ItemToFieldTable>.I.GetCandidateField(tableId, trim_count, true);
      if (candidateField1 != null)
      {
        foreach (ItemToFieldTable.CandidateField candidateField2 in candidateField1)
          this.m_ItemDestinations.Add(new ItemDetailTop.ItemDestination()
          {
            type = ItemDetailTop.ItemDestination.TYPE.DropFieldUnknown,
            enemy_id = candidateField2.enemyId,
            field = candidateField2.mapData
          });
      }
    }
    this.AddPointShopIfNeed();
    if (this.IsItemBossMaterial(tableId))
    {
      int index = 0;
      for (int count = this.m_ItemDestinations.Count; index < count; ++index)
      {
        ItemDetailTop.ItemDestination itemDestination = this.m_ItemDestinations[index];
        if (itemDestination != null)
        {
          EnemyTable.EnemyData enemyData = this.SearchEnemyData(itemDestination.quest);
          if (enemyData != null)
          {
            this.AddChallengeQuestIfNeed(enemyData);
            this.m_ItemDestinations.Insert(0, new ItemDetailTop.ItemDestination()
            {
              type = ItemDetailTop.ItemDestination.TYPE.GachaQuest,
              enemy_species = enemyData.enemySpecies
            });
            break;
          }
        }
      }
    }
    if (!TradingPostManager.IsItemValid(this.data.GetTableID()))
      return;
    this.m_ItemDestinations.Insert(0, new ItemDetailTop.ItemDestination()
    {
      type = ItemDetailTop.ItemDestination.TYPE.TradingPostQuest
    });
  }

  private int GetQuestNum(QuestTable.QuestTableData q)
  {
    if (q.questType != QUEST_TYPE.ORDER)
      return -1;
    QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(q.questID);
    int num1 = questItem != null ? questItem.infoData.questData.num : 0;
    int num2 = 0;
    if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
      num2 = MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Where<GuildRequestItem>((Func<GuildRequestItem, bool>) (g => g.questId == (int) q.questID)).Count<GuildRequestItem>();
    int num3 = num2;
    return Mathf.Max(num1 - num3, 0);
  }

  private void AddChallengeQuestIfNeed(EnemyTable.EnemyData enemyData)
  {
    List<QuestData> challengeList = MonoBehaviourSingleton<QuestManager>.I.challengeList;
    int index = 0;
    for (int count = challengeList.Count; index < count; ++index)
    {
      if (this.SearchEnemyData(Singleton<QuestTable>.I.GetQuestData((uint) challengeList[index].questId)).enemySpecies == enemyData.enemySpecies)
      {
        this.m_ItemDestinations.Insert(0, new ItemDetailTop.ItemDestination()
        {
          type = ItemDetailTop.ItemDestination.TYPE.ChallengeQuest,
          enemy_species = enemyData.enemySpecies
        });
        break;
      }
    }
  }

  private void AddPointShopIfNeed()
  {
    int skillItemIdIfNeed = ItemTable.ChangeItemIdToSkillItemIdIfNeed(this.itemId);
    int index1 = 0;
    for (int count1 = this.pointShopList.Count; index1 < count1; ++index1)
    {
      PointShop pointShop = this.pointShopList[index1];
      List<PointShopItem> items = pointShop.items;
      int index2 = 0;
      for (int count2 = items.Count; index2 < count2; ++index2)
      {
        PointShopItem pointShopItem = items[index2];
        if (pointShopItem.itemId == skillItemIdIfNeed && pointShopItem.isBuyable)
          this.m_ItemDestinations.Insert(0, new ItemDetailTop.ItemDestination()
          {
            type = ItemDetailTop.ItemDestination.TYPE.PointShop,
            pointShopData = new ItemDetailTop.PointShopData(pointShop, pointShopItem)
          });
      }
    }
  }

  private bool IsItemBossMaterial(uint itemId)
  {
    if (!Singleton<ItemTable>.IsValid())
      return false;
    ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(itemId);
    return itemData != null && itemData.enemyIconID != 0;
  }

  private EnemyTable.EnemyData SearchEnemyData(QuestTable.QuestTableData questTableData)
  {
    if (questTableData == null)
      return (EnemyTable.EnemyData) null;
    int index = 0;
    for (int length = questTableData.enemyID.Length; index < length; ++index)
    {
      EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questTableData.enemyID[index]);
      if (enemyData != null)
        return enemyData;
    }
    return (EnemyTable.EnemyData) null;
  }

  public override void UpdateUI()
  {
    string key = "TEXT_BTN_SELL";
    this.SetLabelText((Enum) ItemDetailTop.UI.STR_BTN_SELL, this.sectionData.GetText(key));
    this.SetLabelText((Enum) ItemDetailTop.UI.STR_BTN_SELL_D, this.sectionData.GetText(key));
    this.detailBase = this.SetPrefab(this.GetCtrl((Enum) ItemDetailTop.UI.OBJ_DETAIL_ROOT), "ItemDetailBase");
    this.SetActive((Enum) ItemDetailTop.UI.OBJ_SCROLL_BAR, true);
    if (Object.op_Inequality((Object) this.detailBase, (Object) null))
    {
      this.SetFontStyle(this.detailBase, (Enum) ItemDetailTop.UI.STR_TITLE, (FontStyle) 2);
      this.SetFontStyle(this.detailBase, (Enum) ItemDetailTop.UI.STR_SELL, (FontStyle) 2);
      this.SetFontStyle(this.detailBase, (Enum) ItemDetailTop.UI.STR_NEED, (FontStyle) 2);
      this.SetFontStyle(this.detailBase, (Enum) ItemDetailTop.UI.STR_HAVE, (FontStyle) 2);
      this.SetActive(this.detailBase, (Enum) ItemDetailTop.UI.STR_SELL, this.data.CanSale());
      this.SetDepth(this.detailBase, (Enum) ItemDetailTop.UI.SCR_HOWTO, this.baseDepth + 1);
      this.SetLabelText(this.detailBase, (Enum) ItemDetailTop.UI.LBL_NAME, this.data.GetName());
      Transform detailBase1 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<ItemDetailTop.UI> label_enum1 = (Enum) ItemDetailTop.UI.LBL_HAVE_NUM;
      int num1 = this.data.GetNum();
      string text1 = num1.ToString();
      this.SetLabelText(detailBase1, (Enum) label_enum1, text1);
      Transform detailBase2 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<ItemDetailTop.UI> label_enum2 = (Enum) ItemDetailTop.UI.LBL_SELL;
      num1 = this.data.GetSalePrice();
      string text2 = num1.ToString();
      this.SetLabelText(detailBase2, (Enum) label_enum2, text2);
      ItemTable.ItemData itemData = (ItemTable.ItemData) null;
      if (this.data is ItemSortData)
        itemData = (this.data.GetItemData() as ItemInfo).tableData;
      else if (this.data is AbilityItemSortData)
        itemData = (this.data.GetItemData() as AbilityItemInfo).GetItemTableData();
      int enemyIconId = itemData.enemyIconID;
      int enemyIconId2 = itemData.enemyIconID2;
      ItemIcon.Create(this.data.GetIconType(), this.data.GetIconID(), new RARITY_TYPE?(this.data.GetRarity()), this.FindCtrl(this.detailBase, (Enum) ItemDetailTop.UI.OBJ_ICON_ROOT), this.data.GetIconElement(), this.data.GetIconMagiEnableType(), enemy_icon_id: enemyIconId, enemy_icon_id2: enemyIconId2, getType: this.data.GetGetType()).SetEnableCollider(false);
      int count = this.m_ItemDestinations.Count;
      Transform ctrl1 = this.GetCtrl((Enum) ItemDetailTop.UI.GRD_HOWTO);
      if (Object.op_Implicit((Object) ctrl1))
      {
        int num2 = 0;
        for (int childCount = ctrl1.childCount; num2 < childCount; ++num2)
        {
          Transform child = ctrl1.GetChild(0);
          child.parent = (Transform) null;
          Object.Destroy((Object) ((Component) child).gameObject);
        }
      }
      this.SetTable(this.detailBase, (Enum) ItemDetailTop.UI.GRD_HOWTO, "QuestListItem", count, true, new Func<int, Transform, Transform>(this.CreateListItem), new Action<int, Transform, bool>(this.UpdateListItem));
      this.SetActive(this.detailBase, (Enum) ItemDetailTop.UI.STR_NOT_HOWTO, this.m_ItemDestinations.Count < 1);
      string empty = string.Empty;
      int id = 2;
      RARITY_TYPE rarity = RARITY_TYPE.A;
      if (itemData.type == ITEM_TYPE.LAPIS)
      {
        rarity = itemData.rarity;
        id = Singleton<EquipItemExceedTable>.I.IsFreeLapis(rarity, itemData.id, itemData.eventId) ? 0 : 1;
        if (Singleton<LimitedEquipItemExceedTable>.I.IsLimitedLapis(itemData.id))
          id = 8;
      }
      this.SetLabelText(this.detailBase, (Enum) ItemDetailTop.UI.STR_NOT_HOWTO, string.Format(StringTable.Get(STRING_CATEGORY.ITEM_DETAIL, (uint) id), (object) rarity.ToString()));
      this.SetActive(this.detailBase, (Enum) ItemDetailTop.UI.SPR_NEED, this.GetNeedNum().HasValue);
      int? needNum = this.GetNeedNum();
      if (needNum.HasValue)
      {
        Transform ctrl2 = this.FindCtrl(this.detailBase, (Enum) ItemDetailTop.UI.LBL_HAVE_NUM);
        Transform ctrl3 = this.FindCtrl(this.detailBase, (Enum) ItemDetailTop.UI.LBL_NEED_NUM);
        int num3 = this.data.GetNum();
        needNum = this.GetNeedNum();
        int need_num = needNum.Value;
        UIBehaviour.SetMaterialNumText(ctrl2, ctrl3, num3, need_num);
      }
    }
    this.SetActive((Enum) ItemDetailTop.UI.BTN_DETAIL_SELL, this.CanSell() && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
    this.SetActive((Enum) ItemDetailTop.UI.BTN_SEARCH_TP, false);
  }

  private Transform CreateListItem(int index, Transform t)
  {
    if (index >= this.m_ItemDestinations.Count)
      return (Transform) null;
    if (this.m_ItemDestinations[index].type == ItemDetailTop.ItemDestination.TYPE.AdmissionQuest && this.m_ItemDestinations[index].quest.questType == QUEST_TYPE.ORDER)
      return this.Realizes("QuestListOrderItem", t);
    if (this.m_ItemDestinations[index].type == ItemDetailTop.ItemDestination.TYPE.AdmissionQuest && this.m_ItemDestinations[index].quest.questType == QUEST_TYPE.SERIES_ARENA)
      return this.Realizes("QuestListSeriesArena", t);
    if (this.m_ItemDestinations[index].type == ItemDetailTop.ItemDestination.TYPE.AdmissionQuest && this.m_ItemDestinations[index].quest.questType == QUEST_TYPE.WAVE && this.m_ItemDestinations[index].quest.questType == QUEST_TYPE.WAVE_STRATEGY && this.m_ItemDestinations[index].quest.questType == QUEST_TYPE.SERIES && this.m_ItemDestinations[index].quest.questType == QUEST_TYPE.EVENT)
      return this.Realizes("QuestListFieldItem", t);
    if (this.m_ItemDestinations[index].type == ItemDetailTop.ItemDestination.TYPE.DeniedQuest)
      return this.Realizes("QuestListOrderItem", t);
    if (this.m_ItemDestinations[index].type == ItemDetailTop.ItemDestination.TYPE.GachaQuest)
      return this.Realizes("QuestListGachaQuestItem", t);
    if (this.m_ItemDestinations[index].type == ItemDetailTop.ItemDestination.TYPE.ChallengeQuest)
      return this.Realizes("QuestListChallengeGotoItem", t);
    if (this.m_ItemDestinations[index].type == ItemDetailTop.ItemDestination.TYPE.PointShop)
      return this.Realizes("ItemDetailPointShopItem", t);
    if (this.m_ItemDestinations[index].type == ItemDetailTop.ItemDestination.TYPE.GuildRequest)
      return this.Realizes("QuestListGuildRequestItem", t);
    return this.m_ItemDestinations[index].type == ItemDetailTop.ItemDestination.TYPE.TradingPostQuest ? this.Realizes("QuestListSearchTradingPostItem", t) : this.Realizes("QuestListFieldItem", t);
  }

  private void UpdateListItem(int i, Transform t, bool is_recycle)
  {
    this.SetActive(t, (Enum) ItemDetailTop.UI.TEX_FIELD_SUB, false);
    if (i >= this.m_ItemDestinations.Count)
      return;
    ItemDetailTop.ItemDestination itemDestination = this.m_ItemDestinations[i];
    switch (itemDestination.type)
    {
      case ItemDetailTop.ItemDestination.TYPE.AdmissionQuest:
        if (itemDestination.quest.questType == QUEST_TYPE.ORDER)
        {
          this.UpdateJumpQuestButton(i, t, itemDestination);
          break;
        }
        if (itemDestination.quest.questType == QUEST_TYPE.SERIES_ARENA)
        {
          this.UpdateJumpSeriesArenaButton(i, t, itemDestination);
          break;
        }
        this.UpdateJumpEventButton(i, t, itemDestination);
        break;
      case ItemDetailTop.ItemDestination.TYPE.DeniedQuest:
        QuestTable.QuestTableData quest = itemDestination.quest;
        if (quest == null)
        {
          this.SetActive(t, false);
          break;
        }
        this.SetActive(t, true);
        int num1 = 0;
        this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_ORDER_NUM, num1.ToString());
        this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ICON, false);
        this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_QUEST_TYPE, quest.questType.ToString());
        this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_QUEST_NUM, string.Empty);
        uint mainEnemyId1 = (uint) quest.GetMainEnemyID();
        EnemyTable.EnemyData enemyData1 = Singleton<EnemyTable>.I.GetEnemyData(mainEnemyId1);
        if (enemyData1 != null)
          this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_QUEST_NAME, enemyData1.name);
        else
          this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_QUEST_NAME, quest.questText);
        int num2 = 0;
        for (int index1 = 3; num2 < index1; ++num2)
        {
          int index2 = num2 * 2;
          this.SetActive(t, (Enum) this.mission[index2], false);
          this.SetActive(t, (Enum) this.mission[index2 + 1], false);
        }
        int index = 0;
        for (int length = this.difficult.Length; index < length; ++index)
          this.SetActive(t, (Enum) this.difficult[index], false);
        EnemyTable.EnemyData enemyData2 = Singleton<EnemyTable>.I.GetEnemyData((uint) quest.GetMainEnemyID());
        if (enemyData2 != null)
        {
          this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY, true);
          ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData2.iconId, quest.questType == QUEST_TYPE.ORDER ? new RARITY_TYPE?(quest.rarity) : new RARITY_TYPE?(), this.FindCtrl(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY), enemyData2.element).SetEnableCollider(false);
          this.SetActive(t, (Enum) ItemDetailTop.UI.SPR_ELEMENT_ROOT, enemyData2.element != ELEMENT_TYPE.MAX);
          this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_ELEMENT, (int) enemyData2.element);
          this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_WEAK_ELEMENT, (int) enemyData2.weakElement);
          this.SetActive(t, (Enum) ItemDetailTop.UI.STR_NON_WEAK_ELEMENT, enemyData2.weakElement == ELEMENT_TYPE.MAX);
        }
        else
        {
          this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY, false);
          this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_WEAK_ELEMENT, 6);
          this.SetActive(t, (Enum) ItemDetailTop.UI.STR_NON_WEAK_ELEMENT, true);
        }
        this.SetEvent(t, "ORDER_NOT_HAVE", i);
        break;
      case ItemDetailTop.ItemDestination.TYPE.HappenField:
        int mainEnemyId2 = itemDestination.quest.GetMainEnemyID();
        FieldMapTable.FieldMapTableData field1 = itemDestination.field;
        EnemyTable.EnemyData enemyData3 = Singleton<EnemyTable>.I.GetEnemyData((uint) mainEnemyId2);
        this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY, true);
        string text1 = $"{this.sectionData.GetText("HAPPEN_BOSS")}{enemyData3.name}";
        this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_FIELD_ENEMY_NAME, text1);
        this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_FIELD_NAME, field1.mapName);
        if (enemyData3 != null)
          ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData3.iconId, new RARITY_TYPE?(), this.FindCtrl(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY), enemyData3.element).SetEnableCollider(false);
        ResourceLoad.LoadFieldIconTexture(((Component) this.FindCtrl(t, (Enum) ItemDetailTop.UI.TEX_FIELD)).GetComponent<UITexture>(), field1);
        this.SetEvent(t, "JUMP_FIELD", i);
        break;
      case ItemDetailTop.ItemDestination.TYPE.HappenFieldUnknown:
        this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_FIELD_ICON, true);
        int mainEnemyId3 = itemDestination.quest.GetMainEnemyID();
        EnemyTable.EnemyData enemyData4 = Singleton<EnemyTable>.I.GetEnemyData((uint) mainEnemyId3);
        if (enemyData4 != null)
        {
          this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY, true);
          string text2 = $"{this.sectionData.GetText("HAPPEN_BOSS")}{enemyData4.name}";
          this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_FIELD_ENEMY_NAME, text2);
          ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData4.iconId, new RARITY_TYPE?(), this.FindCtrl(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY), enemyData4.element).SetEnableCollider(false);
          this.SetActive(t, (Enum) ItemDetailTop.UI.SPR_ELEMENT_ROOT, enemyData4.element != ELEMENT_TYPE.MAX);
          this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_ELEMENT, (int) enemyData4.element);
          this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_WEAK_ELEMENT, (int) enemyData4.weakElement);
          this.SetActive(t, (Enum) ItemDetailTop.UI.STR_NON_WEAK_ELEMENT, enemyData4.weakElement == ELEMENT_TYPE.MAX);
          FieldMapTable.FieldMapTableData field2 = itemDestination.field;
          this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_FIELD_NAME, field2.mapName);
          ResourceLoad.LoadFieldIconTexture(((Component) this.FindCtrl(t, (Enum) ItemDetailTop.UI.TEX_FIELD)).GetComponent<UITexture>(), field2);
        }
        this.SetEvent(t, "UNKNOWN_RECOMMEND", 0);
        break;
      case ItemDetailTop.ItemDestination.TYPE.DropField:
        ItemToFieldTable.ItemDetailToFieldData recommendField = itemDestination.recommend_field;
        ItemToFieldTable.ItemDetailToFieldEnemyData toFieldEnemyData = recommendField as ItemToFieldTable.ItemDetailToFieldEnemyData;
        ItemToFieldTable.ItemDetailToFieldPointData point_data = recommendField as ItemToFieldTable.ItemDetailToFieldPointData;
        if (toFieldEnemyData == null && point_data == null)
        {
          this.SetActive(t, false);
          break;
        }
        this.SetActive(t, true);
        QuestListFieldItem questListFieldItem = ((Component) t).GetComponent<QuestListFieldItem>();
        if (Object.op_Equality((Object) questListFieldItem, (Object) null))
          questListFieldItem = ((Component) t).gameObject.AddComponent<QuestListFieldItem>();
        questListFieldItem.InitUI();
        if (toFieldEnemyData != null)
        {
          EnemyTable.EnemyData enemyData5 = Singleton<EnemyTable>.I.GetEnemyData(toFieldEnemyData.enemyID[0]);
          questListFieldItem.SetUpFieldEnemy(enemyData5, recommendField);
        }
        else if (point_data != null)
        {
          string field_name = string.Format(this.sectionData.GetText("STR_GATHER_FIELD_NAME"), (object) recommendField.mapData.mapName);
          questListFieldItem.SetUpGather(field_name, point_data);
        }
        this.SetEvent(t, "JUMP_FIELD", i);
        break;
      case ItemDetailTop.ItemDestination.TYPE.DropFieldUnknown:
        this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_FIELD_ICON, true);
        uint enemyId = itemDestination.enemy_id;
        EnemyTable.EnemyData enemyData6 = Singleton<EnemyTable>.I.GetEnemyData(enemyId);
        if (enemyData6 != null)
        {
          this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY, true);
          this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_FIELD_ENEMY_NAME, enemyData6.name);
          ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData6.iconId, new RARITY_TYPE?(), this.FindCtrl(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY), enemyData6.element).SetEnableCollider(false);
          this.SetActive(t, (Enum) ItemDetailTop.UI.SPR_ELEMENT_ROOT, enemyData6.element != ELEMENT_TYPE.MAX);
          this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_ELEMENT, (int) enemyData6.element);
          this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_WEAK_ELEMENT, (int) enemyData6.weakElement);
          this.SetActive(t, (Enum) ItemDetailTop.UI.STR_NON_WEAK_ELEMENT, enemyData6.weakElement == ELEMENT_TYPE.MAX);
          if (itemDestination.field != null)
          {
            FieldMapTable.FieldMapTableData field3 = itemDestination.field;
            this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_FIELD_NAME, field3.mapName);
            ResourceLoad.LoadFieldIconTexture(((Component) this.FindCtrl(t, (Enum) ItemDetailTop.UI.TEX_FIELD)).GetComponent<UITexture>(), field3);
          }
        }
        this.SetEvent(t, "UNKNOWN_RECOMMEND", 0);
        break;
      case ItemDetailTop.ItemDestination.TYPE.GachaQuest:
        this.SetEvent(t, "JUMP_GACHA_QUEST", i);
        break;
      case ItemDetailTop.ItemDestination.TYPE.ChallengeQuest:
        this.UpdateGridListItemChallenge(i, t);
        break;
      case ItemDetailTop.ItemDestination.TYPE.PointShop:
        this.UpdateListItemPointShop(i, t, this.m_ItemDestinations[i].pointShopData);
        break;
      case ItemDetailTop.ItemDestination.TYPE.GuildRequest:
        if (MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem != null && MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem.Length >= 3)
        {
          this.SetSprite((Enum) ItemDetailTop.UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem[0]));
          this.SetSprite((Enum) ItemDetailTop.UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem[1]));
          this.SetSprite((Enum) ItemDetailTop.UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem[2]));
        }
        else
        {
          this.SetSprite((Enum) ItemDetailTop.UI.SPR_EMBLEM_LAYER_1, "");
          this.SetSprite((Enum) ItemDetailTop.UI.SPR_EMBLEM_LAYER_2, "");
          this.SetSprite((Enum) ItemDetailTop.UI.SPR_EMBLEM_LAYER_3, "");
        }
        this.SetEvent(t, "OPEN_SEND_DIALOG", (object) null);
        break;
      case ItemDetailTop.ItemDestination.TYPE.TradingPostQuest:
        this.SetEvent(t, "SEARCH_IN_TP", i);
        break;
    }
  }

  private void UpdateJumpQuestButton(int i, Transform t, ItemDetailTop.ItemDestination destination)
  {
    QuestTable.QuestTableData table = destination.quest;
    if (table == null)
    {
      this.SetActive(t, false);
    }
    else
    {
      this.SetActive(t, true);
      if (table.questType == QUEST_TYPE.ORDER)
      {
        int questNum = this.GetQuestNum(table);
        this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_ORDER_NUM, questNum.ToString());
      }
      this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ICON, false);
      this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_QUEST_TYPE, table.questType.ToString());
      this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_QUEST_NUM, string.Empty);
      this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_QUEST_NAME, table.questText);
      ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (_status => (long) _status.questId == (long) table.questID));
      if (clearStatusQuest == null || table.missionID == null || table.missionID.Length == 0)
      {
        int num = 0;
        for (int index1 = 3; num < index1; ++num)
        {
          int index2 = num * 2;
          this.SetActive(t, (Enum) this.mission[index2], false);
          this.SetActive(t, (Enum) this.mission[index2 + 1], false);
        }
      }
      else
      {
        int index3 = 0;
        for (int index4 = 3; index3 < index4; ++index3)
        {
          int index5 = index3 * 2;
          this.SetActive(t, (Enum) this.mission[index5], index3 < table.missionID.Length);
          this.SetActive(t, (Enum) this.mission[index5 + 1], clearStatusQuest.missionStatus[index3] >= 3);
        }
      }
      int index = 0;
      for (int length = this.difficult.Length; index < length; ++index)
        this.SetActive(t, (Enum) this.difficult[index], (DIFFICULTY_TYPE) index <= table.difficulty);
      EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) table.GetMainEnemyID());
      if (enemyData != null)
      {
        this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY, true);
        ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, table.questType == QUEST_TYPE.ORDER ? new RARITY_TYPE?(table.rarity) : new RARITY_TYPE?(), this.FindCtrl(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
        this.SetActive(t, (Enum) ItemDetailTop.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
        this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_ELEMENT, (int) enemyData.element);
        this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
        this.SetActive(t, (Enum) ItemDetailTop.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
      }
      else
      {
        this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY, false);
        this.SetElementSprite(t, (Enum) ItemDetailTop.UI.SPR_WEAK_ELEMENT, 6);
        this.SetActive(t, (Enum) ItemDetailTop.UI.STR_NON_WEAK_ELEMENT, true);
      }
      this.ResetTween(t, (Enum) ItemDetailTop.UI.TWN_DIFFICULT_STAR);
      this.PlayTween(t, (Enum) ItemDetailTop.UI.TWN_DIFFICULT_STAR, is_input_block: false);
      this.SetEvent(t, "JUMP_QUEST", i);
    }
  }

  private void UpdateJumpSeriesArenaButton(
    int i,
    Transform t,
    ItemDetailTop.ItemDestination destination)
  {
    ((Component) t).GetComponent<ItemDetailSeriesArena>().SetUpItem(t);
    this.SetEvent(t, "SERIES_ARENA", i);
  }

  private void UpdateJumpEventButton(int i, Transform t, ItemDetailTop.ItemDestination destination)
  {
    int mainEnemyId = destination.quest.GetMainEnemyID();
    DeliveryTable.DeliveryData deliveryData = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(destination.quest.questID);
    if (deliveryData == null)
    {
      this.SetActive(t, false);
    }
    else
    {
      Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I.eventList.Where<Network.EventData>((Func<Network.EventData, bool>) (e => e.eventId == deliveryData.eventID)).FirstOrDefault<Network.EventData>();
      if (eventData == null)
      {
        this.SetActive(t, false);
      }
      else
      {
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) mainEnemyId);
        this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY, true);
        this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_FIELD_ENEMY_NAME, enemyData.name);
        string text = $"{eventData.name} / {deliveryData.name}";
        this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_FIELD_NAME, text);
        if (enemyData != null)
          ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(), this.FindCtrl(t, (Enum) ItemDetailTop.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
        ((Component) this.FindCtrl(t, (Enum) ItemDetailTop.UI.TEX_FIELD)).GetComponent<UITexture>();
        this.SetEvent(t, "JUMP_EVENT", i);
      }
    }
  }

  private void UpdateGridListItemChallenge(int i, Transform t)
  {
    if (!MonoBehaviourSingleton<PartyManager>.IsValid() || MonoBehaviourSingleton<PartyManager>.I.challengeInfo == null)
    {
      this.SetActive(t, false);
    }
    else
    {
      this.SetActive(t, true);
      this.SetEvent(t, "JUMP_CHALLENGE_QUEST", i);
      this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_CHALLENGE_ON, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy());
      this.SetActive(t, (Enum) ItemDetailTop.UI.OBJ_CHALLENGE_OFF, !MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy());
      this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_CHALLENGE_ON_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
      this.SetLabelText(t, (Enum) ItemDetailTop.UI.LBL_CHALLENGE_OFF_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
      this.SetSupportEncoding((Enum) ItemDetailTop.UI.LBL_CHALLENGE_ON_MESSAGE, true);
      this.SetSupportEncoding((Enum) ItemDetailTop.UI.LBL_CHALLENGE_OFF_MESSAGE, true);
    }
  }

  private void UpdateListItemPointShop(
    int i,
    Transform t,
    ItemDetailTop.PointShopData pointShopData)
  {
    this.SetEvent(t, "SELLECT_POINT_SHOP", i);
    this.SetPointShopIcon(t);
    ((Component) t).GetComponent<ItemDetailPointShopItem>().SetUpItemDetailItem(pointShopData.item, pointShopData.shop, (uint) pointShopData.shop.pointShopId, pointShopData.item.needPoint <= pointShopData.shop.userPoint);
  }

  protected virtual void SetPointShopIcon(Transform t)
  {
    NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(1);
    this.SetNPCIcon(t, (Enum) ItemDetailTop.UI.TEX_NPC, npcData.npcModelID);
  }

  private void OnBuy(PointShopItem item, int num)
  {
    GameSection.SetEventData((object) PointShopManager.GetBoughtMessage(item, num));
    GameSection.StayEvent();
    MonoBehaviourSingleton<UserInfoManager>.I.PointShopManager.SendPointShopBuy(item, this.selectedPointShopdata.shop, num, (Action<bool>) (isSuccess =>
    {
      if (isSuccess && !this.selectedPointShopdata.item.isBuyable)
        this.m_ItemDestinations.Remove(this.selectedPointShopDestination);
      this.RefreshUI();
      GameSection.ResumeEvent(isSuccess);
    }));
  }

  private void OnQuery_SEARCH_IN_TP()
  {
    if (!TradingPostManager.IsTradingEnable())
      TradingPostManager.ShowUnavailableDialog();
    else
      MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostFindData((int) this.data.GetTableID());
  }

  protected void OnQuery_JUMP_QUEST()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.m_ItemDestinations == null || this.m_ItemDestinations.Count <= eventData)
      return;
    ItemDetailTop.ItemDestination itemDestination = this.m_ItemDestinations[eventData];
    if (itemDestination.type != ItemDetailTop.ItemDestination.TYPE.AdmissionQuest)
      return;
    QuestTable.QuestTableData quest = itemDestination.quest;
    if (quest == null)
      return;
    if (quest.questType == QUEST_TYPE.ORDER)
    {
      QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(quest.questID);
      if (questItem == null || questItem.infoData == null || questItem.infoData.questData.num == 0)
      {
        GameSection.ChangeEvent("ORDER_NOT_HAVE");
        return;
      }
    }
    this.jumpQuest = quest;
    GameSection.ChangeEvent("JUMP_QUEST_CONFIRM");
  }

  protected void OnQuery_ItemDetailJumpQuestConfirm_NO()
  {
    this.jumpQuest = (QuestTable.QuestTableData) null;
  }

  protected void OnCloseDialog_ItemDetailJumpQuestConfirm()
  {
    QuestTable.QuestTableData jumpQuest = this.jumpQuest;
    if (jumpQuest == null)
      return;
    if (jumpQuest.questType == QUEST_TYPE.ORDER)
    {
      EventData[] event_datas = new EventData[4]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("GACHA_QUEST_COUNTER", (object) null),
        new EventData("TO_GACHA_QUEST_COUNTER", (object) null),
        new EventData("SELECT_ORDER_FROM_ITEM_DETAIL", (object) jumpQuest.questID)
      };
      GameSaveData.instance.lastQusetID = (int) jumpQuest.questID;
      GameSaveData.Save();
      MonoBehaviourSingleton<QuestManager>.I.StartHowToGetAutoEvent(jumpQuest.questID);
      GameSection.StopEvent();
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
    }
    else
      GameSection.StopEvent();
  }

  protected void OnQuery_JUMP_GACHA_QUEST()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.m_ItemDestinations == null || this.m_ItemDestinations.Count <= eventData)
      return;
    ItemDetailTop.ItemDestination itemDestination = this.m_ItemDestinations[eventData];
    if (itemDestination == null || itemDestination.type != ItemDetailTop.ItemDestination.TYPE.GachaQuest)
      return;
    this.searchEnemySpecies = itemDestination.enemy_species;
    GameSection.ChangeEvent("JUMP_GACHA_QUEST_CONFIRM");
  }

  protected void OnQuery_ItemDetailJumpGachaQuestConfirm_NO() => this.searchEnemySpecies = 0;

  protected void OnCloseDialog_ItemDetailJumpGachaQuestConfirm()
  {
    if (this.searchEnemySpecies == 0)
      return;
    EventData[] event_datas = new EventData[4]
    {
      new EventData(GameSection.GetGoingHomeEvent(), (object) null),
      new EventData("GACHA_QUEST_COUNTER", (object) null),
      new EventData("CONDITION", (object) null),
      new EventData("SPECIES_SEARCH_REQUEST", (object) this.searchEnemySpecies)
    };
    GameSection.StopEvent();
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
  }

  protected void OnQuery_JUMP_EVENT()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData >= this.m_ItemDestinations.Count)
      return;
    ItemDetailTop.ItemDestination destination = this.m_ItemDestinations[eventData];
    new List<Network.EventData>((IEnumerable<Network.EventData>) MonoBehaviourSingleton<QuestManager>.I.eventList).Find((Predicate<Network.EventData>) (e => e.eventId == destination.quest.eventId));
    Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(destination.quest.questID);
    this.jump_event_list_datas = new EventData[3]
    {
      new EventData(GameSection.GetGoingHomeEvent(), (object) null),
      new EventData("EVENT_COUNTER", (object) null),
      new EventData("SELECT", (object) destination.quest.eventId)
    };
    GameSection.ChangeEvent("JUMP_EVENT_NORMAL_CONFIRM");
  }

  protected void OnQuery_SERIES_ARENA()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentSeriesArenaId(0);
    this.ToSeriesArena();
  }

  protected void OnQuery_JUMP_FIELD()
  {
    this.jump_event_list_datas = (EventData[]) null;
    int eventData1 = (int) GameSection.GetEventData();
    if (eventData1 >= this.m_ItemDestinations.Count)
      return;
    ItemDetailTop.ItemDestination itemDestination = this.m_ItemDestinations[eventData1];
    if (itemDestination.type == ItemDetailTop.ItemDestination.TYPE.DropField || itemDestination.type == ItemDetailTop.ItemDestination.TYPE.HappenField)
    {
      FieldMapTable.FieldMapTableData table = itemDestination.field;
      if (table == null)
        return;
      if (table.IsEventData)
      {
        Network.EventData eventData2 = new List<Network.EventData>((IEnumerable<Network.EventData>) MonoBehaviourSingleton<QuestManager>.I.eventList).Find((Predicate<Network.EventData>) (e => e.eventId == table.eventId));
        string goingHomeEvent = GameSection.GetGoingHomeEvent();
        if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(table.mapID) && eventData2.readPrologueStory)
        {
          if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(table.jumpPortalID, false))
          {
            GameSection.StopEvent();
            return;
          }
          this.jumpField = table;
          GameSection.ChangeEvent("JUMP_FIELD_CONFIRM");
          return;
        }
        this.jump_event_list_datas = new EventData[3]
        {
          new EventData(goingHomeEvent, (object) null),
          new EventData("EVENT_COUNTER", (object) null),
          new EventData("SELECT", (object) table.eventId)
        };
        GameSection.ChangeEvent("JUMP_EVENT_CONFIRM");
        return;
      }
      if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(table.mapID))
      {
        if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(table.jumpPortalID, false))
        {
          GameSection.StopEvent();
          return;
        }
        this.jumpField = table;
        GameSection.ChangeEvent("JUMP_FIELD_CONFIRM");
        return;
      }
    }
    this.jumpField = (FieldMapTable.FieldMapTableData) null;
    GameSection.ChangeEvent("CAN_NOT_JUMP_FIELD");
  }

  protected void OnQuery_ItemDetailJumpFieldConfirm_YES()
  {
    GameSection.StayEvent();
    CoopApp.EnterField(this.jumpField.jumpPortalID, 0U, (Action<bool, bool, bool>) ((is_matching, is_connect, is_regist) =>
    {
      if (!is_connect)
      {
        GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
        GameSection.ResumeEvent(true);
      }
      else
      {
        this.jumpField = (FieldMapTable.FieldMapTableData) null;
        GameSection.ChangeStayEvent("TO_FIELD");
        GameSection.ResumeEvent(is_regist);
      }
    }));
  }

  protected void OnQuery_ItemDetailJumpFieldConfirm_NO()
  {
    this.jumpField = (FieldMapTable.FieldMapTableData) null;
  }

  protected void OnQuery_ItemDetailJumpEventConfirm_YES()
  {
    if (this.jump_event_list_datas == null)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(this.jump_event_list_datas);
  }

  protected void OnQuery_ItemDetailJumpEventConfirm_NO()
  {
    this.jump_event_list_datas = (EventData[]) null;
  }

  protected void OnQuery_ItemDetailJumpEventNormalConfirm_YES()
  {
    if (this.jump_event_list_datas == null)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(this.jump_event_list_datas);
  }

  protected void OnQuery_ItemDetailJumpEventNormalConfirm_NO()
  {
    this.jump_event_list_datas = (EventData[]) null;
  }

  protected void OnQuery_JUMP_CHALLENGE_QUEST()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.m_ItemDestinations == null || this.m_ItemDestinations.Count <= eventData)
      return;
    ItemDetailTop.ItemDestination itemDestination = this.m_ItemDestinations[eventData];
    if (itemDestination == null || itemDestination.type != ItemDetailTop.ItemDestination.TYPE.ChallengeQuest || !MonoBehaviourSingleton<PartyManager>.IsValid())
      return;
    if (!MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy())
      GameSection.ChangeEvent("NO_SATISFY");
    else if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.num == 0)
    {
      GameSection.ChangeEvent("NUM_ZERO");
    }
    else
    {
      this.searchEnemySpecies = itemDestination.enemy_species;
      GameSection.ChangeEvent("JUMP_CHALLENGE_QUEST_CONFIRM");
    }
  }

  protected void OnQuery_ItemDetailJumpChallengeQuestConfirm_NO() => this.searchEnemySpecies = 0;

  protected void OnCloseDialog_ItemDetailJumpChallengeQuestConfirm()
  {
    if (this.searchEnemySpecies == 0)
      return;
    EventData[] event_datas = new EventData[4]
    {
      new EventData(GameSection.GetGoingHomeEvent(), (object) null),
      new EventData("CHALLENGE_COUNTER", (object) null),
      new EventData("CONDITION", (object) null),
      new EventData("SPECIES_SEARCH_REQUEST", (object) this.searchEnemySpecies)
    };
    GameSection.StopEvent();
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
  }

  private void OnQuery_SELLECT_POINT_SHOP()
  {
    this.selectedPointShopDestination = this.m_ItemDestinations[(int) GameSection.GetEventData()];
    this.selectedPointShopdata = this.selectedPointShopDestination.pointShopData;
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.selectedPointShopdata.item,
      (object) this.selectedPointShopdata.shop,
      (object) new Action<PointShopItem, int>(this.OnBuy)
    });
    if (this.selectedPointShopdata.shop.userPoint >= this.selectedPointShopdata.item.needPoint)
      return;
    GameSection.ChangeEvent("SHORTAGE_POINT");
  }

  private void OnQuery_SELL()
  {
    if (!this.CanSell())
      GameSection.ChangeEvent("NOT_SELL");
    GameSection.SetEventData((object) this.data);
  }

  private bool CanSell() => this.data != null && this.data.CanSale();

  protected virtual int? GetNeedNum() => new int?();

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      int haveingItemNum = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(this.data.GetTableID());
      if (this.data.GetNum() != haveingItemNum)
      {
        ItemInfo itemInfo = new ItemInfo();
        itemInfo.uniqueID = this.data.GetUniqID();
        itemInfo.tableID = this.data.GetTableID();
        itemInfo.tableData = Singleton<ItemTable>.I.GetItemData(itemInfo.tableID);
        itemInfo.num = haveingItemNum;
        this.data = (SortCompareData) new ItemSortData();
        this.data.SetItem((object) itemInfo);
      }
    }
    base.OnNotify(flags);
    this.SetSupportEncoding((Enum) ItemDetailTop.UI.LBL_CHALLENGE_ON_MESSAGE, true);
    this.SetSupportEncoding((Enum) ItemDetailTop.UI.LBL_CHALLENGE_OFF_MESSAGE, true);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY;
  }

  private void OnCloseDialog_GuildDonateSendDialog()
  {
    string eventData = GameSection.GetEventData() as string;
    try
    {
      int numRequest = int.Parse(eventData);
      if (numRequest <= 0)
        return;
      this.StartCoroutine(this.CRSendDonateRequest((int) this.data.GetTableID(), this.data.GetName(), "", numRequest));
    }
    catch
    {
    }
  }

  private IEnumerator CRSendDonateRequest(
    int itemID,
    string itemName,
    string request,
    int numRequest)
  {
    yield return (object) new WaitUntil((Func<bool>) (() => !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible()));
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendDonateRequest(itemID, itemName, request, numRequest, (Action<bool>) (success =>
    {
      GameSection.ResumeEvent(success);
      if (!success)
        return;
      this.backSection = true;
    }));
  }

  private void Update()
  {
    if (!this.backSection || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      return;
    this.backSection = false;
    GameSection.BackSection();
  }

  private enum UI
  {
    OBJ_DETAIL_ROOT,
    BTN_DETAIL_SELL,
    STR_BTN_SELL,
    STR_BTN_SELL_D,
    BTN_SEARCH_TP,
    OBJ_ICON_ROOT,
    LBL_NAME,
    LBL_SELL,
    LBL_HAVE_NUM,
    SPR_NEED,
    LBL_NEED_NUM,
    GRD_HOWTO,
    STR_NOT_HOWTO,
    SCR_HOWTO,
    STR_TITLE,
    STR_SELL,
    STR_NEED,
    STR_HAVE,
    OBJ_ICON,
    LBL_QUEST_TYPE,
    LBL_QUEST_NUM,
    LBL_QUEST_NAME,
    OBJ_MISSION_INFO_ROOT,
    OBJ_TOP_CROWN1,
    SPR_CROWN_1,
    OBJ_TOP_CROWN2,
    SPR_CROWN_2,
    OBJ_TOP_CROWN3,
    SPR_CROWN_3,
    OBJ_ENEMY,
    SPR_MONSTER_ICON,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    OBJ_FIELD_ICON,
    TEX_FIELD,
    TEX_FIELD_SUB,
    TWN_DIFFICULT_STAR,
    OBJ_DIFFICULT_STAR_1,
    OBJ_DIFFICULT_STAR_2,
    OBJ_DIFFICULT_STAR_3,
    OBJ_DIFFICULT_STAR_4,
    OBJ_DIFFICULT_STAR_5,
    OBJ_DIFFICULT_STAR_6,
    OBJ_DIFFICULT_STAR_7,
    OBJ_DIFFICULT_STAR_8,
    OBJ_DIFFICULT_STAR_9,
    OBJ_DIFFICULT_STAR_10,
    LBL_ORDER_NUM,
    LBL_FIELD_NAME,
    LBL_FIELD_ENEMY_NAME,
    OBJ_SCROLL_BAR,
    OBJ_CHALLENGE_ON,
    OBJ_CHALLENGE_OFF,
    LBL_CHALLENGE_ON_MESSAGE,
    LBL_CHALLENGE_OFF_MESSAGE,
    TEX_NPC,
    SPR_EMBLEM_LAYER_1,
    SPR_EMBLEM_LAYER_2,
    SPR_EMBLEM_LAYER_3,
  }

  private class CandidateEnemyInfo
  {
    public uint EnemyId;
    public bool IsHappenEnemy;
  }

  public class ItemDestination
  {
    public ItemDetailTop.ItemDestination.TYPE type;
    public QuestTable.QuestTableData quest;
    public FieldMapTable.FieldMapTableData field;
    public ItemToFieldTable.ItemDetailToFieldData recommend_field;
    public uint enemy_id;
    public int enemy_species;
    public ItemDetailTop.PointShopData pointShopData;

    public enum TYPE
    {
      Unknown,
      AdmissionQuest,
      DeniedQuest,
      HappenField,
      HappenFieldUnknown,
      DropField,
      DropFieldUnknown,
      GachaQuest,
      ChallengeQuest,
      PointShop,
      GuildRequest,
      TradingPostQuest,
    }
  }

  public class PointShopData
  {
    public PointShop shop;
    public PointShopItem item;

    public PointShopData(PointShop shop, PointShopItem item)
    {
      this.shop = shop;
      this.item = item;
    }
  }
}
