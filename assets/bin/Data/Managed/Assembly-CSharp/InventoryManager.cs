// Decompiled with JetBrains decompiler
// Type: InventoryManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
public class InventoryManager : MonoBehaviourSingleton<InventoryManager>
{
  private List<InventoryManager.InGameTempItem> inGameTempItemInventory;
  private Dictionary<uint, InventoryManager.EquipItemFilter> equipItemFilterList;
  private bool firstSetList = true;
  private List<ulong> removeNewFragAbilityItemIds = new List<ulong>();

  public InventoryList<EquipItemInfo, EquipItem> equipItemInventory { private set; get; }

  public InventoryList<SkillItemInfo, Network.SkillItem> skillItemInventory { private set; get; }

  public InventoryList<SkillItemInfo, Network.SkillItem> skillMaterialInventory { private set; get; }

  public InventoryList<ItemInfo, Network.Item> itemInventory { private set; get; }

  public InventoryList<AbilityItemInfo, AbilityItem> abilityItemInventory { private set; get; }

  public InventoryList<QuestItemInfo, QuestItem> questItemInventory { private set; get; }

  public InventoryList<AccessoryInfo, Accessory> accessoryInventory { private set; get; }

  public InventoryManager.INVENTORY_TYPE changeInventoryType { get; set; }

  public bool IsWeaponInventoryType(InventoryManager.INVENTORY_TYPE type)
  {
    bool flag = false;
    switch (type)
    {
      case InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD:
      case InventoryManager.INVENTORY_TYPE.TWO_HAND_SWORD:
      case InventoryManager.INVENTORY_TYPE.SPEAR:
      case InventoryManager.INVENTORY_TYPE.PAIR_SWORDS:
      case InventoryManager.INVENTORY_TYPE.ARROW:
      case InventoryManager.INVENTORY_TYPE.ALL_WEAPON:
      case InventoryManager.INVENTORY_TYPE.ALL_EQUIP:
      case InventoryManager.INVENTORY_TYPE.ALL_EVOLVE_EQUIP:
        flag = true;
        break;
    }
    return flag;
  }

  private InventoryManager()
  {
    this.equipItemInventory = new InventoryList<EquipItemInfo, EquipItem>();
    this.skillItemInventory = new InventoryList<SkillItemInfo, Network.SkillItem>();
    this.skillMaterialInventory = new InventoryList<SkillItemInfo, Network.SkillItem>();
    this.itemInventory = new InventoryList<ItemInfo, Network.Item>();
    this.abilityItemInventory = new InventoryList<AbilityItemInfo, AbilityItem>();
    this.inGameTempItemInventory = new List<InventoryManager.InGameTempItem>();
    this.questItemInventory = new InventoryList<QuestItemInfo, QuestItem>();
    this.accessoryInventory = new InventoryList<AccessoryInfo, Accessory>();
    this.equipItemFilterList = new Dictionary<uint, InventoryManager.EquipItemFilter>();
  }

  public List<EquipItemInfo> GetWeaponInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type < EQUIPMENT_TYPE.ARMOR));
  }

  public List<EquipItemInfo> GetOneHandSwordInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.ONE_HAND_SWORD));
  }

  public List<EquipItemInfo> GetTwoHandSwordInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.TWO_HAND_SWORD));
  }

  public List<EquipItemInfo> GetPairSwordInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.PAIR_SWORDS));
  }

  public List<EquipItemInfo> GetSpearInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.SPEAR));
  }

  public List<EquipItemInfo> GetArrowInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.ARROW));
  }

  public List<EquipItemInfo> GetArmorInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.ARMOR));
  }

  public List<EquipItemInfo> GetHelmInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.HELM));
  }

  public List<EquipItemInfo> GetArmInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.ARM));
  }

  public List<EquipItemInfo> GetLegInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.LEG));
  }

  public List<EquipItemInfo> GetEvolveItemInventory()
  {
    List<EquipItemInfo> list = new List<EquipItemInfo>();
    this.ForAllEquipItemInventory((Action<EquipItemInfo>) (data =>
    {
      if (data == null || !data.tableData.IsEvolve())
        return;
      list.Add(data);
    }));
    return list;
  }

  public int GetEquipItemNum(uint equip_id)
  {
    int equipItemNum = 0;
    for (LinkedListNode<EquipItemInfo> linkedListNode = this.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((int) linkedListNode.Value.tableID == (int) equip_id)
        ++equipItemNum;
    }
    return equipItemNum;
  }

  public int GetEquipItemNumWithShadow(uint equip_id)
  {
    int itemNumWithShadow = 0;
    for (LinkedListNode<EquipItemInfo> linkedListNode = this.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((int) linkedListNode.Value.tableID == (int) equip_id || (int) linkedListNode.Value.tableData.shadowEvolveEquipItemId == (int) equip_id)
        ++itemNumWithShadow;
    }
    return itemNumWithShadow;
  }

  public List<EquipItemInfo> GetVisualArmorInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.ARMOR || type == EQUIPMENT_TYPE.VISUAL_ARMOR));
  }

  public List<EquipItemInfo> GetVisualHelmInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.HELM || type == EQUIPMENT_TYPE.VISUAL_HELM));
  }

  public List<EquipItemInfo> GetVisualArmInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.ARM || type == EQUIPMENT_TYPE.VISUAL_ARM));
  }

  public List<EquipItemInfo> GetVisualLegInventory()
  {
    return this.GetItemInventory((InventoryManager.CompareInventoryType) (type => type == EQUIPMENT_TYPE.LEG || type == EQUIPMENT_TYPE.VISUAL_LEG));
  }

  private List<EquipItemInfo> GetItemInventory(InventoryManager.CompareInventoryType compare)
  {
    List<EquipItemInfo> list = new List<EquipItemInfo>();
    this.ForAllEquipItemInventory((Action<EquipItemInfo>) (data =>
    {
      if (data == null || !compare(data.tableData.type))
        return;
      list.Add(data);
    }));
    return list;
  }

  public void ForAllEquipItemInventory(Action<EquipItemInfo> callback)
  {
    for (LinkedListNode<EquipItemInfo> linkedListNode = this.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      callback(linkedListNode.Value);
  }

  public void ForAllSkillItemInventory(Action<SkillItemInfo> callback)
  {
    for (LinkedListNode<SkillItemInfo> linkedListNode = this.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      callback(linkedListNode.Value);
  }

  public EquipItemInfo GetEquipItem(ulong uniq_id)
  {
    for (LinkedListNode<EquipItemInfo> linkedListNode = this.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
        return linkedListNode.Value;
    }
    return (EquipItemInfo) null;
  }

  public SkillItemInfo GetSkillItem(ulong uniq_id)
  {
    for (LinkedListNode<SkillItemInfo> linkedListNode = this.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
        return linkedListNode.Value;
    }
    return (SkillItemInfo) null;
  }

  public AbilityItemInfo GetAbilityItem(ulong uniq_id)
  {
    for (LinkedListNode<AbilityItemInfo> linkedListNode = this.abilityItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
        return linkedListNode.Value;
    }
    return (AbilityItemInfo) null;
  }

  public AccessoryInfo GetAccessory(ulong uniq_id)
  {
    for (LinkedListNode<AccessoryInfo> linkedListNode = this.accessoryInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
        return linkedListNode.Value;
    }
    return (AccessoryInfo) null;
  }

  public EquipItemInfo[] GetEquipInventoryClone()
  {
    if (this.changeInventoryType == InventoryManager.INVENTORY_TYPE.NONE)
      return (EquipItemInfo[]) null;
    List<EquipItemInfo> equipItemInfoList = (List<EquipItemInfo>) null;
    switch (this.changeInventoryType)
    {
      case InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD:
        equipItemInfoList = this.GetOneHandSwordInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.TWO_HAND_SWORD:
        equipItemInfoList = this.GetTwoHandSwordInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.SPEAR:
        equipItemInfoList = this.GetSpearInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.PAIR_SWORDS:
        equipItemInfoList = this.GetPairSwordInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.ARROW:
        equipItemInfoList = this.GetArrowInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.ARMOR:
        equipItemInfoList = this.GetArmorInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.HELM:
        equipItemInfoList = this.GetHelmInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.ARM:
        equipItemInfoList = this.GetArmInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.LEG:
        equipItemInfoList = this.GetLegInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.ALL_WEAPON:
        equipItemInfoList = this.GetWeaponInventory();
        break;
      case InventoryManager.INVENTORY_TYPE.ALL_EQUIP:
        equipItemInfoList = this.GetWeaponInventory();
        equipItemInfoList.AddRange((IEnumerable<EquipItemInfo>) this.GetArmorInventory());
        equipItemInfoList.AddRange((IEnumerable<EquipItemInfo>) this.GetHelmInventory());
        equipItemInfoList.AddRange((IEnumerable<EquipItemInfo>) this.GetArmInventory());
        equipItemInfoList.AddRange((IEnumerable<EquipItemInfo>) this.GetLegInventory());
        break;
      case InventoryManager.INVENTORY_TYPE.ALL_EVOLVE_EQUIP:
        equipItemInfoList = this.GetEvolveItemInventory();
        break;
    }
    return equipItemInfoList.ToArray();
  }

  public SkillItemInfo[] GetSkillInventoryClone()
  {
    SkillItemInfo[] skillInventoryClone = new SkillItemInfo[this.skillItemInventory.GetCount()];
    int num = 0;
    for (LinkedListNode<SkillItemInfo> linkedListNode = this.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      skillInventoryClone[num++] = linkedListNode.Value;
    return skillInventoryClone;
  }

  public void ForAllQuestInvetory(Action<QuestItemInfo> callback)
  {
    if (callback == null)
      return;
    for (LinkedListNode<QuestItemInfo> linkedListNode = this.questItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      callback(linkedListNode.Value);
  }

  public bool IsHaveingMaterial(NeedMaterial[] needMaterial)
  {
    if (needMaterial == null)
      return false;
    List<NeedMaterial> needMaterialList = new List<NeedMaterial>((IEnumerable<NeedMaterial>) needMaterial);
    for (LinkedListNode<ItemInfo> node = this.itemInventory.GetFirstNode(); node != null; node = node.Next)
    {
      bool not_enough = false;
      NeedMaterial find_material = (NeedMaterial) null;
      needMaterialList.ForEach((Action<NeedMaterial>) (material =>
      {
        if (not_enough || (int) material.itemID != (int) node.Value.tableID)
          return;
        if (material.num > node.Value.num)
          not_enough = true;
        find_material = material;
      }));
      if (not_enough)
        return false;
      if (find_material != null)
        needMaterialList.Remove(find_material);
    }
    return needMaterialList.Count == 0;
  }

  public bool IsHaveingEquip(NeedEquip[] needEquip)
  {
    if (needEquip == null)
      return true;
    List<NeedEquip> needEquipList = new List<NeedEquip>((IEnumerable<NeedEquip>) NeedEquip.DivideNeedEquip(needEquip));
    for (LinkedListNode<EquipItemInfo> linkedListNode = this.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      NeedEquip needEquip1 = (NeedEquip) null;
      for (int index = 0; index < needEquipList.Count; ++index)
      {
        if ((int) needEquipList[index].equipItemID == (int) linkedListNode.Value.tableID && needEquipList[index].num == 1 && needEquipList[index].needLv <= linkedListNode.Value.level)
        {
          needEquip1 = needEquipList[index];
          break;
        }
      }
      if (needEquip1 != null)
        needEquipList.Remove(needEquip1);
      if (needEquipList.Count == 0)
        return true;
    }
    return needEquipList.Count == 0;
  }

  public bool IsSetEquipMaterial(ulong[] uniqIdList)
  {
    if (uniqIdList == null)
      return false;
    for (int index = 0; index < uniqIdList.Length; ++index)
    {
      LinkedListNode<EquipItemInfo> linkedListNode = this.equipItemInventory.GetFirstNode();
      bool flag = false;
      for (; linkedListNode != null; linkedListNode = linkedListNode.Next)
      {
        if ((long) uniqIdList[index] == (long) linkedListNode.Value.uniqueID)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return false;
    }
    return true;
  }

  public bool IsHaveingKeyMaterial(LOGICAL_ORDER_TYPE needKeyOrder, NeedMaterial[] needMaterial)
  {
    List<NeedMaterial> needMaterialList = new List<NeedMaterial>();
    int index = 0;
    for (int length = needMaterial.Length; index < length; ++index)
    {
      if (needMaterial[index].isKey)
        needMaterialList.Add(needMaterial[index]);
    }
    int count = needMaterialList.Count;
    if (count <= 0)
      return true;
    for (LinkedListNode<ItemInfo> node = this.itemInventory.GetFirstNode(); node != null; node = node.Next)
    {
      NeedMaterial find_material = (NeedMaterial) null;
      needMaterialList.ForEach((Action<NeedMaterial>) (material =>
      {
        if ((int) material.itemID != (int) node.Value.tableID)
          return;
        find_material = material;
      }));
      if (find_material != null)
        needMaterialList.Remove(find_material);
    }
    switch (needKeyOrder)
    {
      case LOGICAL_ORDER_TYPE.OR:
        if (needMaterialList.Count == count)
          return false;
        break;
      case LOGICAL_ORDER_TYPE.AND:
        if (needMaterialList.Count != 0)
          return false;
        break;
    }
    return true;
  }

  public int IsPay(EquipItemInfo equipData)
  {
    int num1 = equipData.tableData.getType == GET_TYPE.PAY ? 1 : 0;
    int num2 = 0;
    return num1 == 0 ? num2 | 2 : num2 | 1;
  }

  public int IsHaveingMaterialAndPayAndObtained(SmithCreateItemInfo createData)
  {
    bool flag1 = false;
    bool flag2 = true;
    int num1 = 0;
    bool flag3 = this.equipItemFilterList.ContainsKey(createData.smithCreateTableData.equipItemID);
    bool flag4;
    if (flag3)
    {
      flag2 = this.equipItemFilterList[createData.smithCreateTableData.equipItemID].isCreateble;
      flag1 = this.equipItemFilterList[createData.smithCreateTableData.equipItemID].isPay;
      flag4 = this.equipItemFilterList[createData.smithCreateTableData.equipItemID].isObtained;
    }
    else
    {
      NeedMaterial[] needMaterial = createData.smithCreateTableData.needMaterial;
      if (createData.equipTableData.getType == GET_TYPE.PAY)
        flag1 = true;
      flag4 = MonoBehaviourSingleton<AchievementManager>.I.CheckEquipItemCollection(createData.equipTableData);
      List<NeedMaterial> needMaterialList = new List<NeedMaterial>((IEnumerable<NeedMaterial>) needMaterial);
      if (needMaterialList.Count > 0)
      {
        for (LinkedListNode<ItemInfo> node = this.itemInventory.GetFirstNode(); node != null; node = node.Next)
        {
          NeedMaterial find_enough_material = (NeedMaterial) null;
          needMaterialList.ForEach((Action<NeedMaterial>) (material =>
          {
            if ((int) material.itemID != (int) node.Value.tableID || material.num > node.Value.num)
              return;
            find_enough_material = material;
          }));
          if (find_enough_material != null)
            needMaterialList.Remove(find_enough_material);
        }
        if (needMaterialList.Count != 0)
          flag2 = false;
      }
    }
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < (int) createData.smithCreateTableData.needMoney)
      flag2 = false;
    if (!flag3)
    {
      InventoryManager.EquipItemFilter equipItemFilter = new InventoryManager.EquipItemFilter()
      {
        itemId = createData.smithCreateTableData.equipItemID,
        isCreateble = flag2,
        isPay = flag1,
        isObtained = flag4
      };
      this.equipItemFilterList[equipItemFilter.itemId] = equipItemFilter;
    }
    int num2 = !flag2 ? num1 | 8 : num1 | 4;
    int num3 = !flag1 ? num2 | 2 : num2 | 1;
    return !flag4 ? num3 | 32 /*0x20*/ : num3 | 16 /*0x10*/;
  }

  public bool IsHaveingItem(uint item_id)
  {
    for (LinkedListNode<ItemInfo> linkedListNode = this.itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((int) linkedListNode.Value.tableID == (int) item_id && linkedListNode.Value.num > 0)
        return true;
    }
    return false;
  }

  public int GetHaveingItemNum(uint item_id)
  {
    int inGameTempItemNum = this.GetInGameTempItemNum(item_id);
    for (LinkedListNode<ItemInfo> linkedListNode = this.itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((int) linkedListNode.Value.tableID == (int) item_id && linkedListNode.Value.GetNum() > 0)
        return item_id == 1200000U ? MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => x.tableData.type == ITEM_TYPE.TICKET)) : linkedListNode.Value.GetNum() + inGameTempItemNum;
    }
    return inGameTempItemNum;
  }

  protected int GetInGameTempItemNum(uint item_id)
  {
    int index = 0;
    for (int count = this.inGameTempItemInventory.Count; index < count; ++index)
    {
      if ((int) this.inGameTempItemInventory[index].itemId == (int) item_id)
        return this.inGameTempItemInventory[index].num;
    }
    return 0;
  }

  public void AddInGameTempItem(uint item_id, int num)
  {
    int index = 0;
    for (int count = this.inGameTempItemInventory.Count; index < count; ++index)
    {
      if ((int) this.inGameTempItemInventory[index].itemId == (int) item_id)
      {
        this.inGameTempItemInventory[index].num += num;
        return;
      }
    }
    this.inGameTempItemInventory.Add(new InventoryManager.InGameTempItem()
    {
      itemId = item_id,
      num = num
    });
  }

  public void ForAllItemInventory(Action<ItemInfo> callback)
  {
    for (LinkedListNode<ItemInfo> linkedListNode = this.itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if (linkedListNode != null && linkedListNode.Value != null)
      {
        if (linkedListNode.Value.tableData != null)
          callback(linkedListNode.Value);
        else
          Log.Error("SVに存在するが、CLには存在しないアイテム tableID = " + (object) linkedListNode.Value.tableID);
      }
    }
  }

  public List<ItemInfo> GetItemList(Predicate<ItemInfo> match, int listNum = 0)
  {
    List<ItemInfo> itemList = new List<ItemInfo>();
    int num = 0;
    for (LinkedListNode<ItemInfo> linkedListNode = this.itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      ItemInfo itemInfo = linkedListNode.Value;
      if (match(itemInfo))
      {
        itemList.Add(itemInfo);
        if (listNum > 0)
        {
          ++num;
          if (listNum <= num)
            return itemList;
        }
      }
    }
    return itemList;
  }

  public ItemInfo GetItem(Predicate<ItemInfo> match)
  {
    for (LinkedListNode<ItemInfo> linkedListNode = this.itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      ItemInfo itemInfo = linkedListNode.Value;
      if (match(itemInfo))
        return itemInfo;
    }
    return (ItemInfo) null;
  }

  public int GetItemNum(Predicate<ItemInfo> match, int listNum = 0, bool isItemNum = false)
  {
    int itemNum = 0;
    foreach (ItemInfo itemInfo in this.GetItemList(match, listNum))
    {
      if (isItemNum)
        itemNum += itemInfo.num;
      else
        itemNum += itemInfo.GetNum();
    }
    return itemNum;
  }

  public QuestItemInfo GetQuestItem(uint quest_id)
  {
    if (quest_id == 0U)
      return (QuestItemInfo) null;
    QuestItemInfo questItem = (QuestItemInfo) null;
    for (LinkedListNode<QuestItemInfo> linkedListNode = this.questItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((int) linkedListNode.Value.infoData.questData.tableData.questID == (int) quest_id && linkedListNode.Value.infoData.questData.num > 0)
      {
        questItem = linkedListNode.Value;
        break;
      }
    }
    return questItem;
  }

  public bool IsNewItem(ITEM_ICON_TYPE type, ulong uniq_id)
  {
    switch (type)
    {
      case ITEM_ICON_TYPE.NONE:
        return false;
      case ITEM_ICON_TYPE.SKILL_ATTACK:
      case ITEM_ICON_TYPE.SKILL_SUPPORT:
      case ITEM_ICON_TYPE.SKILL_HEAL:
      case ITEM_ICON_TYPE.SKILL_PASSIVE:
      case ITEM_ICON_TYPE.SKILL_GROW:
        for (LinkedListNode<SkillItemInfo> linkedListNode = this.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
            return GameSaveData.instance.IsNewItem(type, uniq_id);
        }
        break;
      case ITEM_ICON_TYPE.ITEM:
        for (LinkedListNode<ItemInfo> linkedListNode = this.itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
          {
            if (linkedListNode.Value.num > 0)
              return GameSaveData.instance.IsNewItem(type, uniq_id);
            break;
          }
        }
        break;
      case ITEM_ICON_TYPE.QUEST_ITEM:
        for (LinkedListNode<QuestItemInfo> linkedListNode = this.questItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
          {
            if (linkedListNode.Value.infoData.questData.num > 0)
              return GameSaveData.instance.IsNewItem(type, uniq_id);
            break;
          }
        }
        break;
      case ITEM_ICON_TYPE.ABILITY_ITEM:
        for (LinkedListNode<AbilityItemInfo> linkedListNode = this.abilityItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
          {
            if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
              return GameSaveData.instance.IsNewItem(type, uniq_id);
            break;
          }
        }
        break;
      case ITEM_ICON_TYPE.ACCESSORY:
        for (LinkedListNode<AccessoryInfo> linkedListNode = this.accessoryInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
          {
            if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
              return GameSaveData.instance.IsNewItem(type, uniq_id);
            break;
          }
        }
        break;
      default:
        for (LinkedListNode<EquipItemInfo> linkedListNode = this.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
            return GameSaveData.instance.IsNewItem(type, uniq_id);
        }
        break;
    }
    return false;
  }

  public bool RemoveNewIcon(ITEM_ICON_TYPE type, string str_uniq_id, int update_num)
  {
    if (type == ITEM_ICON_TYPE.NONE)
      return false;
    ulong uniq_id = ulong.Parse(str_uniq_id);
    switch (type - 10)
    {
      case ITEM_ICON_TYPE.NONE:
      case ITEM_ICON_TYPE.ONE_HAND_SWORD:
      case ITEM_ICON_TYPE.TWO_HAND_SWORD:
      case ITEM_ICON_TYPE.SPEAR:
      case ITEM_ICON_TYPE.PAIR_SWORDS:
        for (LinkedListNode<SkillItemInfo> linkedListNode = this.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
            return GameSaveData.instance.RemoveNewIcon(type, uniq_id);
        }
        break;
      case ITEM_ICON_TYPE.ARROW:
        for (LinkedListNode<ItemInfo> linkedListNode = this.itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
          {
            if (linkedListNode.Value.num > update_num)
              return GameSaveData.instance.RemoveNewIcon(type, uniq_id);
            break;
          }
        }
        break;
      case ITEM_ICON_TYPE.HELM:
        for (LinkedListNode<QuestItemInfo> linkedListNode = this.questItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
          {
            if (linkedListNode.Value.infoData.questData.num > update_num)
              return GameSaveData.instance.RemoveNewIcon(type, uniq_id);
            break;
          }
        }
        break;
      case ITEM_ICON_TYPE.SKILL_PASSIVE:
        for (LinkedListNode<AbilityItemInfo> linkedListNode = this.abilityItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
            return GameSaveData.instance.RemoveNewIcon(type, uniq_id);
        }
        break;
      case ITEM_ICON_TYPE.ITEM:
        for (LinkedListNode<AccessoryInfo> linkedListNode = this.accessoryInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
            return GameSaveData.instance.RemoveNewIcon(type, uniq_id);
        }
        break;
      default:
        for (LinkedListNode<EquipItemInfo> linkedListNode = this.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
            return GameSaveData.instance.RemoveNewIcon(type, uniq_id);
        }
        break;
    }
    return false;
  }

  public void SetList()
  {
    if (!this.firstSetList)
      return;
    this.firstSetList = false;
    OnceInventoryModel.Param inventory = MonoBehaviourSingleton<OnceManager>.I.result.inventory;
    this.equipItemInventory = EquipItemInfo.CreateList(inventory.equipItem);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY);
    this.skillItemInventory = SkillItemInfo.CreateList(inventory.skillItem);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
    this.skillMaterialInventory = SkillItemInfo.CreateListFromItem(inventory.item);
    this.abilityItemInventory = AbilityItemInfo.CreateList(inventory.abilityItem);
    this.accessoryInventory = AccessoryInfo.CreateList(inventory.accessory);
    this.inGameTempItemInventory.Clear();
    this.equipItemFilterList.Clear();
    this.itemInventory = ItemInfo.CreateList(inventory.item);
    this.SetExpiredAtList(inventory.expiredItem);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY);
    this.questItemInventory = QuestItemInfo.CreateList(inventory.questItem);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY);
    MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList = true;
  }

  public void SetExpiredAtList(List<ExpiredItem> list)
  {
    foreach (ExpiredItem expiredItem1 in list)
    {
      ExpiredItem expiredItem = expiredItem1;
      foreach (ItemInfo itemInfo in this.GetItemList((Predicate<ItemInfo>) (x => (long) x.tableID == (long) expiredItem.itemId), 1))
      {
        if (itemInfo.expiredAtItem == null)
          itemInfo.expiredAtItem = new List<ExpiredItem>();
        itemInfo.expiredAtItem.Add(expiredItem);
      }
    }
  }

  public void SendInventoryUseItem(string uid, Action<bool> call_back)
  {
    Protocol.Send<InventoryUseItemModel.RequestSendForm, InventoryUseItemModel>(InventoryUseItemModel.URL, new InventoryUseItemModel.RequestSendForm()
    {
      uid = uid
    }, (Action<InventoryUseItemModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendInventoryEquipSetExt(string uid, Action<bool> call_back)
  {
    Protocol.Send<InventoryUseItemModel.RequestSendForm, InventoryEquipSetExtModel>(InventoryEquipSetExtModel.URL, new InventoryUseItemModel.RequestSendForm()
    {
      uid = uid
    }, (Action<InventoryEquipSetExtModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendInventoryAutoItem(string uid, Action<bool> call_back)
  {
    Protocol.Send<InventoryAutoItemModel.RequestSendForm, InventoryAutoItemModel>(InventoryAutoItemModel.URL, new InventoryAutoItemModel.RequestSendForm()
    {
      uid = uid
    }, (Action<InventoryAutoItemModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag && FieldManager.IsValidInGame() && MonoBehaviourSingleton<CoopNetworkManager>.IsValid() && MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
        MonoBehaviourSingleton<UIPlayerStatus>.I.autoBattleButton.OnUseItem(ret.result.timeLeft);
      call_back(flag);
    }));
  }

  public void SendInventoryEquipSetCopy(
    StatusEquipSetCopyModel.RequestSendForm send_form,
    Action<bool> call_back)
  {
    Protocol.Send<StatusEquipSetCopyModel.RequestSendForm, StatusEquipSetCopyModel>(StatusEquipSetCopyModel.URL, send_form, (Action<StatusEquipSetCopyModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void SendInventoryExtend(Action<bool> call_back)
  {
    Protocol.Send<InventoryExtendModel.RequestSendForm, InventoryExtendModel>(InventoryExtendModel.URL, new InventoryExtendModel.RequestSendForm()
    {
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<InventoryExtendModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_INVENTORY_CAPACITY);
      }
      call_back(flag);
    }));
  }

  public void OnDiff(BaseModelDiff.DiffItem diff)
  {
    bool flag1 = false;
    bool flag2 = true;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        Network.Item obj = diff.add[index];
        List<ExpiredItem> expiredItemList = (List<ExpiredItem>) null;
        ItemInfo itemInfo1 = this.itemInventory.Find(ulong.Parse(obj.uniqId));
        if (itemInfo1 != null)
          expiredItemList = itemInfo1.expiredAtItem;
        ItemInfo itemInfo2 = this.itemInventory.Set(obj.uniqId, obj);
        if (expiredItemList != null)
          itemInfo2.expiredAtItem = expiredItemList;
        if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.ITEM, obj.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        Network.Item obj = diff.update[index];
        if (this.RemoveNewIcon(ITEM_ICON_TYPE.ITEM, obj.uniqId, obj.num))
          flag2 = true;
        List<ExpiredItem> expiredItemList = (List<ExpiredItem>) null;
        ItemInfo itemInfo3 = this.itemInventory.Find(ulong.Parse(obj.uniqId));
        if (itemInfo3 != null)
          expiredItemList = itemInfo3.expiredAtItem;
        ItemInfo itemInfo4 = this.itemInventory.Set(obj.uniqId, obj);
        if (expiredItemList != null)
          itemInfo4.expiredAtItem = expiredItemList;
      }
      flag1 = true;
    }
    this.skillMaterialInventory = SkillItemInfo.CreateListFromItemInventory(this.itemInventory);
    this.inGameTempItemInventory.Clear();
    this.equipItemFilterList.Clear();
    if (flag2)
      GameSaveData.Save();
    if (!flag1)
      return;
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene" && MonoBehaviourSingleton<SmithManager>.IsValid())
      MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData(true);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY);
  }

  public void OnDiff(BaseModelDiff.DiffExpiredItem diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      List<ItemInfo> itemInfoList1 = (List<ItemInfo>) null;
      int num = -1;
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        ExpiredItem o = diff.add[index];
        List<ItemInfo> itemInfoList2 = itemInfoList1;
        if (num != o.itemId || itemInfoList1 == null)
        {
          itemInfoList2 = this.GetItemList((Predicate<ItemInfo>) (x => (long) x.tableID == (long) o.itemId), 1);
          num = o.itemId;
          itemInfoList1 = itemInfoList2;
        }
        foreach (ItemInfo itemInfo in itemInfoList2)
        {
          if (itemInfo.expiredAtItem == null)
            itemInfo.expiredAtItem = new List<ExpiredItem>();
          itemInfo.expiredAtItem.Add(o);
        }
      }
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      List<ItemInfo> itemInfoList3 = (List<ItemInfo>) null;
      int num = -1;
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        ExpiredItem o = diff.update[index];
        List<ItemInfo> itemInfoList4 = itemInfoList3;
        if (num != o.itemId || itemInfoList3 == null)
        {
          itemInfoList4 = this.GetItemList((Predicate<ItemInfo>) (x => (long) x.tableID == (long) o.itemId), 1);
          num = o.itemId;
          itemInfoList3 = itemInfoList4;
        }
        foreach (ItemInfo itemInfo in itemInfoList4)
        {
          ExpiredItem expiredItem = itemInfo.expiredAtItem.Find((Predicate<ExpiredItem>) (y => y.uniqId.Equals(o.uniqId)));
          expiredItem.expiredAt = o.expiredAt;
          expiredItem.used = o.used;
        }
      }
      flag = true;
    }
    if (!flag)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY);
  }

  public void OnDiff(BaseModelDiff.DiffEquipItem diff)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        EquipItem equipItem = diff.add[index];
        this.equipItemInventory.Set(equipItem.uniqId, equipItem);
        if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.ONE_HAND_SWORD, equipItem.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        EquipItem equipItem = diff.update[index];
        this.equipItemInventory.Set(equipItem.uniqId, equipItem);
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ONE_HAND_SWORD, equipItem.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.del))
    {
      int index = 0;
      for (int count = diff.del.Count; index < count; ++index)
      {
        string str = diff.del[index];
        this.equipItemInventory.Delete(ulong.Parse(str));
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ONE_HAND_SWORD, str))
          flag2 = true;
      }
      flag1 = true;
    }
    if (flag2)
      GameSaveData.Save();
    if (!flag1)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY);
  }

  public void OnDiff(BaseModelDiff.DiffAbilityItem diff)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        AbilityItem abilityItem = diff.add[index];
        this.abilityItemInventory.Set(abilityItem.uniqId, abilityItem);
        if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, abilityItem.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        AbilityItem abilityItem = diff.update[index];
        this.abilityItemInventory.Set(abilityItem.uniqId, abilityItem);
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ABILITY_ITEM, abilityItem.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.del))
    {
      int index = 0;
      for (int count = diff.del.Count; index < count; ++index)
      {
        string str = diff.del[index];
        this.abilityItemInventory.Delete(ulong.Parse(str));
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ABILITY_ITEM, str))
          flag2 = true;
      }
      flag1 = true;
    }
    if (flag2)
      GameSaveData.Save();
    if (!flag1)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY);
  }

  public void OnDiff(BaseModelDiff.DiffAccessory diff)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        Accessory accessory = diff.add[index];
        this.accessoryInventory.Set(accessory.uniqId, accessory);
        if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.ACCESSORY, accessory.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        Accessory accessory = diff.update[index];
        this.accessoryInventory.Set(accessory.uniqId.ToString(), accessory);
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ACCESSORY, accessory.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.del))
    {
      int index = 0;
      for (int count = diff.del.Count; index < count; ++index)
      {
        string str = diff.del[index];
        this.accessoryInventory.Delete(ulong.Parse(str));
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.ACCESSORY, str))
          flag2 = true;
      }
      flag1 = true;
    }
    if (flag2)
      GameSaveData.Save();
    if (!flag1)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW);
  }

  public void OnDiff(BaseModelDiff.DiffSkillItem diff)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        Network.SkillItem skillItem = diff.add[index];
        this.skillItemInventory.Set(skillItem.uniqId, skillItem);
        if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.SKILL_ATTACK, skillItem.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        Network.SkillItem skillItem = diff.update[index];
        SkillItemInfo skillItemInfo1 = this.skillItemInventory.Find(ulong.Parse(skillItem.uniqId));
        if (skillItemInfo1 == null)
        {
          Log.Error("Not Found SkillItem:{0}, Update Action!", (object) skillItem.uniqId);
          this.skillItemInventory.Set(skillItem.uniqId, skillItem);
        }
        else
        {
          List<EquipSetSkillData> equipSetSkill = skillItemInfo1.equipSetSkill;
          SkillItemInfo skillItemInfo2 = this.skillItemInventory.Set(skillItem.uniqId, skillItem);
          skillItemInfo2.UpdateEquipSetSkill(equipSetSkill);
          skillItemInfo2.UpdateUniqueEquipSetSkill(skillItemInfo1.uniqueEquipSetSkill);
        }
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.SKILL_ATTACK, skillItem.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.del))
    {
      int index = 0;
      for (int count = diff.del.Count; index < count; ++index)
      {
        string str = diff.del[index];
        this.skillItemInventory.Delete(ulong.Parse(str));
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.SKILL_ATTACK, str))
          flag2 = true;
      }
      flag1 = true;
    }
    if (flag2)
      GameSaveData.Save();
    if (!flag1)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
  }

  public void OnDiff(BaseModelDiff.DiffEquipSetSlot diff)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        Network.SkillItem.DiffEquipSetSlot setSkill = diff.add[index];
        this.skillItemInventory.Find(ulong.Parse(setSkill.uniqId))?.equipSetSkill.Add(new EquipSetSkillData((Network.SkillItem.EquipSetSlot) setSkill));
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        Network.SkillItem.DiffEquipSetSlot o = diff.update[index];
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.SKILL_ATTACK, o.uniqId))
          flag2 = true;
        SkillItemInfo skillItemInfo = this.skillItemInventory.Find(ulong.Parse(o.uniqId));
        if (skillItemInfo != null)
        {
          EquipSetSkillData equipSetSkillData = skillItemInfo.equipSetSkill.Find((Predicate<EquipSetSkillData>) (x => x.equipSetNo == o.setNo));
          ulong num = ulong.Parse(o.euid);
          if (num == 0UL)
          {
            if (equipSetSkillData != null)
              skillItemInfo.equipSetSkill.Remove(equipSetSkillData);
          }
          else if (equipSetSkillData == null)
          {
            skillItemInfo.equipSetSkill.Add(new EquipSetSkillData((Network.SkillItem.EquipSetSlot) o));
          }
          else
          {
            equipSetSkillData.equipItemUniqId = num;
            equipSetSkillData.equipSlotNo = o.slotNo;
          }
        }
      }
      flag1 = true;
    }
    if (flag2)
      GameSaveData.Save();
    if (!flag1)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
  }

  public void OnDiff(BaseModelDiff.DiffUniqueEquipSetSlot diff)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        Network.SkillItem.DiffEquipSetSlot setSkill = diff.add[index];
        SkillItemInfo skillItemInfo = this.skillItemInventory.Find(ulong.Parse(setSkill.uniqId));
        if (skillItemInfo != null)
          skillItemInfo.uniqueEquipSetSkill = new EquipSetSkillData((Network.SkillItem.EquipSetSlot) setSkill);
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        Network.SkillItem.DiffEquipSetSlot setSkill = diff.update[index];
        if (GameSaveData.instance.RemoveNewIcon(ITEM_ICON_TYPE.SKILL_ATTACK, setSkill.uniqId))
          flag2 = true;
        SkillItemInfo skillItemInfo = this.skillItemInventory.Find(ulong.Parse(setSkill.uniqId));
        if (skillItemInfo != null)
        {
          EquipSetSkillData uniqueEquipSetSkill = skillItemInfo.uniqueEquipSetSkill;
          ulong num = ulong.Parse(setSkill.euid);
          if (num == 0UL)
          {
            if (uniqueEquipSetSkill != null)
            {
              skillItemInfo.uniqueEquipSetSkill.equipItemUniqId = 0UL;
              skillItemInfo.uniqueEquipSetSkill.equipSlotNo = 0;
            }
          }
          else if (uniqueEquipSetSkill == null)
          {
            skillItemInfo.uniqueEquipSetSkill = new EquipSetSkillData((Network.SkillItem.EquipSetSlot) setSkill);
          }
          else
          {
            uniqueEquipSetSkill.equipItemUniqId = num;
            uniqueEquipSetSkill.equipSlotNo = setSkill.slotNo;
          }
        }
      }
      flag1 = true;
    }
    if (flag2)
      GameSaveData.Save();
    if (!flag1)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY);
  }

  public void OnDiff(BaseModelDiff.DiffQuestItem diff)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        QuestItem questItem = diff.add[index];
        this.questItemInventory.Set(questItem.uniqId, questItem);
        if (GameSaveData.instance.AddNewItem(ITEM_ICON_TYPE.QUEST_ITEM, questItem.uniqId))
          flag2 = true;
      }
      flag1 = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        QuestItem questItem = diff.update[index];
        if (this.RemoveNewIcon(ITEM_ICON_TYPE.QUEST_ITEM, questItem.uniqId, questItem.num))
          flag2 = true;
        if (questItem.num == 0)
          this.questItemInventory.Delete(ulong.Parse(questItem.uniqId));
        else if (questItem.num > 0)
          this.questItemInventory.Set(questItem.uniqId, questItem);
      }
      flag1 = true;
    }
    if (flag2)
      GameSaveData.Save();
    if (!flag1)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY);
    MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList = true;
  }

  public void AddShowFragsAbilityItem(ulong removeId)
  {
    if (!this.IsNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, removeId) || this.removeNewFragAbilityItemIds.Contains(removeId))
      return;
    this.removeNewFragAbilityItemIds.Add(removeId);
  }

  public void DoRemoveNewFragsAbilityItem()
  {
    foreach (ulong fragAbilityItemId in this.removeNewFragAbilityItemIds)
      this.RemoveNewIcon(ITEM_ICON_TYPE.ABILITY_ITEM, fragAbilityItemId.ToString(), 0);
    this.removeNewFragAbilityItemIds.Clear();
  }

  public enum INVENTORY_TYPE
  {
    NONE,
    ONE_HAND_SWORD,
    TWO_HAND_SWORD,
    SPEAR,
    PAIR_SWORDS,
    ARROW,
    ARMOR,
    HELM,
    ARM,
    LEG,
    ALL_WEAPON,
    ALL_ARMOR,
    ALL_EQUIP,
    ALL_EVOLVE_EQUIP,
    VISUAL_ARMOR,
    VISUAL_HELM,
    VISUAL_ARM,
    VISUAL_LEG,
  }

  private class InGameTempItem
  {
    public uint itemId;
    public int num;
  }

  private class EquipItemFilter
  {
    public uint itemId;
    public bool isCreateble;
    public bool isPay;
    public bool isObtained;
  }

  private delegate bool CompareInventoryType(EQUIPMENT_TYPE equipment_type);
}
