// Decompiled with JetBrains decompiler
// Type: SmithManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithManager : MonoBehaviourSingleton<SmithManager>
{
  public object[] localInventoryEquipData;
  private object smithData;

  public bool initialized { get; private set; }

  public bool isEnableSmithBlur { get; private set; }

  public T CreateSmithData<T>() where T : SmithManager.SmithDataBase, new()
  {
    if (this.smithData is T smithData)
      smithData.ResetData();
    else
      this.smithData = (object) new T();
    return this.smithData as T;
  }

  public T GetSmithData<T>() where T : SmithManager.SmithDataBase => this.smithData as T;

  private void DeleteSmithData()
  {
    if (this.smithData is SmithManager.SmithDataBase smithData)
      smithData.ResetData();
    this.smithData = (object) null;
  }

  public EquipItemInfo GetSmithEquipItemInfo(SmithEquipBase.SmithType type)
  {
    EquipItemInfo smithEquipItemInfo = (EquipItemInfo) null;
    if (type != SmithEquipBase.SmithType.GENERATE)
    {
      SmithManager.SmithGrowData smithData = this.GetSmithData<SmithManager.SmithGrowData>();
      if (smithData != null)
        smithEquipItemInfo = smithData.selectEquipData;
    }
    return smithEquipItemInfo;
  }

  public EquipItemTable.EquipItemData GetSmithEquipItemTable(SmithEquipBase.SmithType type)
  {
    EquipItemTable.EquipItemData smithEquipItemTable = (EquipItemTable.EquipItemData) null;
    switch (type)
    {
      case SmithEquipBase.SmithType.GROW:
      case SmithEquipBase.SmithType.ABILITY_CHANGE:
        SmithManager.SmithGrowData smithData1 = this.GetSmithData<SmithManager.SmithGrowData>();
        if (smithData1 != null && smithData1.selectEquipData != null)
        {
          smithEquipItemTable = smithData1.selectEquipData.tableData;
          break;
        }
        break;
      case SmithEquipBase.SmithType.EVOLVE:
        SmithManager.SmithGrowData smithData2 = this.GetSmithData<SmithManager.SmithGrowData>();
        if (smithData2 != null && smithData2.evolveData != null)
        {
          smithEquipItemTable = smithData2.evolveData.GetEquipTable();
          break;
        }
        break;
      default:
        SmithManager.SmithCreateData smithData3 = this.GetSmithData<SmithManager.SmithCreateData>();
        if (smithData3 != null)
        {
          smithEquipItemTable = smithData3.generateTableData;
          break;
        }
        break;
    }
    return smithEquipItemTable;
  }

  public void CreateLocalInventory()
  {
    this.localInventoryEquipData = (object[]) MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone();
  }

  private void DeleteLocalInventory() => this.localInventoryEquipData = (object[]) null;

  public void UpdateLocalInventoryItem(EquipItemInfo item)
  {
    if (item == null || item.uniqueID == 0UL || item.tableID == 0U || this.localInventoryEquipData == null || this.localInventoryEquipData.Length == 0 || !(this.localInventoryEquipData[0] is EquipItemInfo))
      return;
    int index = 0;
    for (int length = this.localInventoryEquipData.Length; index < length; ++index)
    {
      if (this.localInventoryEquipData[index] is EquipItemInfo equipItemInfo && (long) equipItemInfo.uniqueID == (long) item.uniqueID)
      {
        this.localInventoryEquipData[index] = (object) item;
        break;
      }
    }
  }

  public void InitSmithData()
  {
    this.DeleteSmithData();
    this.DeleteLocalInventory();
  }

  public SmithManager.ERR_SMITH_SEND CheckCreateEquipItem(uint create_id)
  {
    CreateEquipItemTable.CreateEquipItemData equipItemTableData = Singleton<CreateEquipItemTable>.I.GetCreateEquipItemTableData(create_id);
    if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(equipItemTableData.needMaterial))
      return SmithManager.ERR_SMITH_SEND.NOT_ENOUGH_MATERIAL;
    return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < (int) equipItemTableData.needMoney ? SmithManager.ERR_SMITH_SEND.NOT_ENOUGH_MONEY : SmithManager.ERR_SMITH_SEND.NONE;
  }

  public SmithManager.ERR_SMITH_SEND CheckGrowEquipItem(EquipItemInfo item)
  {
    if (item.IsLevelMax())
      return SmithManager.ERR_SMITH_SEND.ALREADY_LV_MAX;
    GrowEquipItemTable.GrowEquipItemNeedItemData nextNeedTableData = item.nextNeedTableData;
    if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(nextNeedTableData.needMaterial))
      return SmithManager.ERR_SMITH_SEND.NOT_ENOUGH_MATERIAL;
    return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < nextNeedTableData.needMoney ? SmithManager.ERR_SMITH_SEND.NOT_ENOUGH_MONEY : SmithManager.ERR_SMITH_SEND.NONE;
  }

  public SmithManager.ERR_SMITH_SEND CheckEvolveEquipItem(
    EquipItemInfo item,
    uint evolve_id,
    ulong[] uniqIdList)
  {
    if (!item.IsLevelMax())
      return SmithManager.ERR_SMITH_SEND.NOT_LV_MAX;
    EvolveEquipItemTable.EvolveEquipItemData[] evolveTable = item.tableData.GetEvolveTable();
    EvolveEquipItemTable.EvolveEquipItemData evolveEquipItemData1 = (EvolveEquipItemTable.EvolveEquipItemData) null;
    foreach (EvolveEquipItemTable.EvolveEquipItemData evolveEquipItemData2 in evolveTable)
    {
      if ((int) evolveEquipItemData2.id == (int) evolve_id)
      {
        evolveEquipItemData1 = evolveEquipItemData2;
        break;
      }
    }
    if (evolveEquipItemData1 == null)
      return SmithManager.ERR_SMITH_SEND.NOT_FOUND_EVOLVE_DATA;
    if (evolveEquipItemData1.needEquip != null && !MonoBehaviourSingleton<InventoryManager>.I.IsSetEquipMaterial(uniqIdList))
      return SmithManager.ERR_SMITH_SEND.NOT_SET_EQUIP_MATERIAL;
    if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(evolveEquipItemData1.needMaterial))
      return SmithManager.ERR_SMITH_SEND.NOT_ENOUGH_MATERIAL;
    return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < (int) evolveEquipItemData1.needMoney ? SmithManager.ERR_SMITH_SEND.NOT_ENOUGH_MONEY : SmithManager.ERR_SMITH_SEND.NONE;
  }

  public SmithManager.ERR_SMITH_SEND CheckAbilityChange(EquipItemInfo item)
  {
    return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < this.GetAbilityChangeNeedMoney(item) ? SmithManager.ERR_SMITH_SEND.NOT_ENOUGH_MONEY : SmithManager.ERR_SMITH_SEND.NONE;
  }

  public int GetAbilityChangeNeedMoney(EquipItemInfo item)
  {
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    switch (item.tableData.rarity)
    {
      case RARITY_TYPE.D:
        return constDefine.ABILITY_CHANGE_COST_RARITY_D;
      case RARITY_TYPE.C:
        return constDefine.ABILITY_CHANGE_COST_RARITY_C;
      case RARITY_TYPE.B:
        return constDefine.ABILITY_CHANGE_COST_RARITY_B;
      case RARITY_TYPE.A:
        return constDefine.ABILITY_CHANGE_COST_RARITY_A;
      case RARITY_TYPE.S:
        return constDefine.ABILITY_CHANGE_COST_RARITY_S;
      case RARITY_TYPE.SS:
        return constDefine.ABILITY_CHANGE_COST_RARITY_SS;
      default:
        return constDefine.ABILITY_CHANGE_COST_RARITY_SSS;
    }
  }

  public int GetGrowResultValue(int base_value, GrowRate rate_data, bool is_element = false)
  {
    int add = (int) rate_data.add;
    return Mathf.FloorToInt((float) (base_value * (int) rate_data.rate) * 0.01f) + add;
  }

  public float GetGrowResultValue(float base_value, GrowRateFloat rate_data, bool is_element = false)
  {
    float add = (float) rate_data.add;
    return (float) ((double) base_value * (double) (int) rate_data.rate * 0.0099999997764825821) + add;
  }

  public SmithManager.SmithBadgeData smithBadgeData { get; private set; }

  public int GetBadgeTotalNum() => this.smithBadgeData == null ? 0 : this.smithBadgeData.totalNum;

  public void CreateBadgeData(bool is_force = false)
  {
    if (this.smithBadgeData != null && !is_force)
      return;
    this.smithBadgeData = new SmithManager.SmithBadgeData();
    EQUIPMENT_TYPE[] values = (EQUIPMENT_TYPE[]) Enum.GetValues(typeof (EQUIPMENT_TYPE));
    int index1 = 0;
    for (int length1 = values.Length; index1 < length1; ++index1)
    {
      EQUIPMENT_TYPE type = values[index1];
      SmithCreateItemInfo[] equipItemDataAry = Singleton<CreateEquipItemTable>.I.GetCreateEquipItemDataAry(type);
      if (equipItemDataAry != null && equipItemDataAry.Length != 0)
      {
        int index2 = 0;
        for (int length2 = equipItemDataAry.Length; index2 < length2; ++index2)
          this.CheckAndAddSmithBadge(equipItemDataAry[index2]);
      }
    }
    this.smithBadgeData.DebugShowCount();
    SmithCreateItemInfo[] pickupItemAry1 = Singleton<CreatePickupItemTable>.I.GetPickupItemAry(SortBase.TYPE.WEAPON_ALL);
    int index3 = 0;
    for (int length = pickupItemAry1.Length; index3 < length; ++index3)
      this.CheckAndAddSmithBadge(pickupItemAry1[index3], true);
    this.smithBadgeData.DebugShowCount();
    SmithCreateItemInfo[] pickupItemAry2 = Singleton<CreatePickupItemTable>.I.GetPickupItemAry(SortBase.TYPE.ARMOR_ALL);
    int index4 = 0;
    for (int length = pickupItemAry2.Length; index4 < length; ++index4)
      this.CheckAndAddSmithBadge(pickupItemAry2[index4], true);
    this.smithBadgeData.DebugShowCount();
  }

  public bool NeedSmithBadge(SmithCreateItemInfo create_info, bool is_pickup = false)
  {
    return this.smithBadgeData != null && create_info != null && create_info.equipTableData.id != 0U && create_info.equipTableData.listId != 0 && (int) create_info.smithCreateTableData.researchLv <= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.researchLv && MonoBehaviourSingleton<InventoryManager>.I.IsHaveingKeyMaterial(create_info.smithCreateTableData.needKeyOrder, create_info.smithCreateTableData.needMaterial) && !GameSaveData.instance.IsCheckedSmithCreateRecipe((int) create_info.equipTableData.id);
  }

  private void CheckAndAddSmithBadge(SmithCreateItemInfo create_info, bool is_pickup = false)
  {
    int id = (int) create_info.equipTableData.id;
    if (!this.NeedSmithBadge(create_info, is_pickup))
      return;
    if (create_info.equipTableData.IsWeapon())
    {
      int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(create_info.equipTableData.type);
      if (is_pickup)
      {
        if (this.smithBadgeData.pickupBadgeIds[0] == null)
          this.smithBadgeData.pickupBadgeIds[0] = new List<int>();
        this.smithBadgeData.pickupBadgeIds[0].Add(id);
        ++this.smithBadgeData.pickupBadgeNum[0];
      }
      else
      {
        if (this.smithBadgeData.weaponsBadgeIds[equipmentTypeIndex] == null)
          this.smithBadgeData.weaponsBadgeIds[equipmentTypeIndex] = new List<int>();
        this.smithBadgeData.weaponsBadgeIds[equipmentTypeIndex].Add(id);
        ++this.smithBadgeData.weaponsBadgeNum[equipmentTypeIndex];
      }
    }
    else if (create_info.equipTableData.IsVisual())
    {
      int index = UIBehaviour.GetEquipmentTypeIndex(create_info.equipTableData.type) - 5;
      if (is_pickup)
      {
        if (this.smithBadgeData.pickupBadgeIds[1] == null)
          this.smithBadgeData.pickupBadgeIds[1] = new List<int>();
        this.smithBadgeData.pickupBadgeIds[1].Add(id);
        ++this.smithBadgeData.pickupBadgeNum[1];
      }
      else
      {
        if (this.smithBadgeData.visualBadgeIds[index] == null)
          this.smithBadgeData.visualBadgeIds[index] = new List<int>();
        this.smithBadgeData.visualBadgeIds[index].Add(id);
        ++this.smithBadgeData.visualBadgeNum[index];
      }
    }
    else
    {
      int index = UIBehaviour.GetEquipmentTypeIndex(create_info.equipTableData.type) - 5;
      if (is_pickup)
      {
        if (this.smithBadgeData.pickupBadgeIds[1] == null)
          this.smithBadgeData.pickupBadgeIds[1] = new List<int>();
        this.smithBadgeData.pickupBadgeIds[1].Add(id);
        ++this.smithBadgeData.pickupBadgeNum[1];
      }
      else
      {
        if (this.smithBadgeData.defenseBadgeIds[index] == null)
          this.smithBadgeData.defenseBadgeIds[index] = new List<int>();
        this.smithBadgeData.defenseBadgeIds[index].Add(id);
        ++this.smithBadgeData.defenseBadgeNum[index];
      }
    }
  }

  public void RemoveSmithBadge(EQUIPMENT_TYPE type, bool is_pickup)
  {
    if (Singleton<EquipItemTable>.I.IsWeapon(type))
    {
      int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(type);
      if (is_pickup)
      {
        if (this.smithBadgeData.pickupBadgeIds[0] != null)
        {
          GameSaveData.instance.AddCheckedSmithCreateRecipe(this.smithBadgeData.pickupBadgeIds[0].ToArray());
          this.smithBadgeData.pickupBadgeIds[0].Clear();
        }
        this.smithBadgeData.pickupBadgeNum[0] = 0;
      }
      else
      {
        if (this.smithBadgeData.weaponsBadgeIds[equipmentTypeIndex] != null)
        {
          GameSaveData.instance.AddCheckedSmithCreateRecipe(this.smithBadgeData.weaponsBadgeIds[equipmentTypeIndex].ToArray());
          this.smithBadgeData.weaponsBadgeIds[equipmentTypeIndex].Clear();
        }
        this.smithBadgeData.weaponsBadgeNum[equipmentTypeIndex] = 0;
      }
    }
    else if (Singleton<EquipItemTable>.I.IsVisual(type))
    {
      int index = UIBehaviour.GetEquipmentTypeIndex(type) - 5;
      if (is_pickup)
      {
        if (this.smithBadgeData.pickupBadgeIds[1] != null)
        {
          GameSaveData.instance.AddCheckedSmithCreateRecipe(this.smithBadgeData.pickupBadgeIds[1].ToArray());
          this.smithBadgeData.pickupBadgeIds[1].Clear();
        }
        this.smithBadgeData.pickupBadgeNum[1] = 0;
      }
      else
      {
        if (this.smithBadgeData.visualBadgeIds[index] != null)
        {
          GameSaveData.instance.AddCheckedSmithCreateRecipe(this.smithBadgeData.visualBadgeIds[index].ToArray());
          this.smithBadgeData.visualBadgeIds[index].Clear();
        }
        this.smithBadgeData.visualBadgeNum[index] = 0;
      }
    }
    else
    {
      int index = UIBehaviour.GetEquipmentTypeIndex(type) - 5;
      if (is_pickup)
      {
        if (this.smithBadgeData.pickupBadgeIds[1] != null)
        {
          GameSaveData.instance.AddCheckedSmithCreateRecipe(this.smithBadgeData.pickupBadgeIds[1].ToArray());
          this.smithBadgeData.pickupBadgeIds[1].Clear();
        }
        this.smithBadgeData.pickupBadgeNum[1] = 0;
      }
      else
      {
        if (this.smithBadgeData.defenseBadgeIds[index] != null)
        {
          GameSaveData.instance.AddCheckedSmithCreateRecipe(this.smithBadgeData.defenseBadgeIds[index].ToArray());
          this.smithBadgeData.defenseBadgeIds[index].Clear();
        }
        this.smithBadgeData.defenseBadgeNum[index] = 0;
      }
    }
    GameSaveData.Save();
  }

  public void SendCreateEquipItem(uint create_table_id, Action<Error, EquipItemInfo> call_back)
  {
    Protocol.Send<SmithCreateModel.RequestSendForm, SmithCreateModel>(SmithCreateModel.URL, new SmithCreateModel.RequestSendForm()
    {
      cid = (int) create_table_id
    }, (Action<SmithCreateModel>) (ret =>
    {
      EquipItemInfo equipItemInfo = (EquipItemInfo) null;
      if (ret.Error == Error.None)
        equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(ret.result.equipUniqId));
      call_back(ret.Error, equipItemInfo);
    }));
  }

  public void SendGrowEquipItem(ulong uid, int target_lv, Action<Error, EquipItemInfo> call_back)
  {
    Protocol.Send<SmithGrowModel.RequestSendForm, SmithGrowModel>(SmithGrowModel.URL, new SmithGrowModel.RequestSendForm()
    {
      euid = uid.ToString(),
      lv = target_lv
    }, (Action<SmithGrowModel>) (ret =>
    {
      EquipItemInfo equip = (EquipItemInfo) null;
      if (ret.Error == Error.None)
      {
        equip = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uid);
        if (equip != null)
        {
          if (MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equip))
            MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equip);
          MonoBehaviourSingleton<StatusManager>.I.UpdateUniqueEquip(equip);
          if (ret.result.maxGrowCount == 1 && equip.IsLevelMax())
            MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("first_maxenhance", "GamePlay", new Dictionary<string, object>()
            {
              {
                "rarity",
                (object) equip.tableData.rarity
              }
            });
        }
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW);
      }
      call_back(ret.Error, equip);
    }));
  }

  public void SendEvolveEquipItem(
    ulong uid,
    uint evolve_id,
    ulong[] uniq_ids,
    Action<Error, EquipItemInfo> call_back)
  {
    SmithEvolveModel.RequestSendForm postData = new SmithEvolveModel.RequestSendForm();
    postData.euid = uid.ToString();
    postData.vid = (int) evolve_id;
    List<string> stringList = new List<string>();
    for (int index = 0; index < uniq_ids.Length; ++index)
      stringList.Add(uniq_ids[index].ToString());
    postData.meids = stringList;
    Protocol.Send<SmithEvolveModel.RequestSendForm, SmithEvolveModel>(SmithEvolveModel.URL, postData, (Action<SmithEvolveModel>) (ret =>
    {
      EquipItemInfo equip = (EquipItemInfo) null;
      if (ret.Error == Error.None)
      {
        equip = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uid);
        if (equip != null)
        {
          if (MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equip))
            MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equip);
          MonoBehaviourSingleton<StatusManager>.I.UpdateUniqueEquip(equip);
          if (GameSaveData.instance.AddNewItem(ItemIcon.GetItemIconType(equip.tableData.type), equip.uniqueID.ToString()))
            GameSaveData.Save();
          if (ret.result.evolveCount == 1)
            MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("first_evolve", "GamePlay", new Dictionary<string, object>()
            {
              {
                "rarity",
                (object) equip.tableData.rarity
              }
            });
        }
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE);
      }
      call_back(ret.Error, equip);
    }));
  }

  public void SendShadowEvolveEquipItem(
    ulong uid,
    uint itemId,
    Action<Error, EquipItemInfo> call_back)
  {
    Protocol.Send<SmithShadowEvolveModel.RequestSendForm, SmithShadowEvolveModel>(SmithShadowEvolveModel.URL, new SmithShadowEvolveModel.RequestSendForm()
    {
      euid = uid.ToString(),
      iid = (int) itemId
    }, (Action<SmithShadowEvolveModel>) (ret =>
    {
      EquipItemInfo equip = (EquipItemInfo) null;
      if (ret.Error == Error.None)
      {
        equip = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uid);
        if (equip != null)
        {
          if (MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equip))
            MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equip);
          MonoBehaviourSingleton<StatusManager>.I.UpdateUniqueEquip(equip);
          if (GameSaveData.instance.AddNewItem(ItemIcon.GetItemIconType(equip.tableData.type), equip.uniqueID.ToString()))
            GameSaveData.Save();
        }
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE);
      }
      call_back(ret.Error, equip);
    }));
  }

  public void SendExceedEquipItem(ulong uid, uint itemId, Action<Error, EquipItemInfo> call_back)
  {
    Protocol.Send<SmithExceedModel.RequestSendForm, SmithExceedModel>(SmithExceedModel.URL, new SmithExceedModel.RequestSendForm()
    {
      euid = uid.ToString(),
      iid = (int) itemId
    }, (Action<SmithExceedModel>) (ret =>
    {
      EquipItemInfo equip = (EquipItemInfo) null;
      if (ret.Error == Error.None)
      {
        equip = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uid);
        if (equip != null)
        {
          if (MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equip))
            MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equip);
          MonoBehaviourSingleton<StatusManager>.I.UpdateUniqueEquip(equip);
        }
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW);
      }
      call_back(ret.Error, equip);
    }));
  }

  public void SendAbilityChangeEquipItem(ulong euid, Action<Error, EquipItemInfo> call_back)
  {
    Protocol.Send<SmithAbilityChangeModel.RequestSendForm, SmithAbilityChangeModel>(SmithAbilityChangeModel.URL, new SmithAbilityChangeModel.RequestSendForm()
    {
      euid = euid.ToString()
    }, (Action<SmithAbilityChangeModel>) (ret =>
    {
      EquipItemInfo equip = (EquipItemInfo) null;
      if (ret.Error == Error.None)
      {
        equip = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(euid);
        if (equip != null)
        {
          if (MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equip))
            MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equip);
          MonoBehaviourSingleton<StatusManager>.I.UpdateUniqueEquip(equip);
        }
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_ABILITY);
      }
      call_back(ret.Error, equip);
    }));
  }

  public void SendUseAbilityItem(
    ulong equipUniqueId,
    ulong abilityItemId,
    Action<Error, EquipItemInfo> call_back)
  {
    Protocol.Send<SmithAbilityItemUseModel.RequestSendForm, SmithAbilityItemUseModel>(SmithAbilityItemUseModel.URL, new SmithAbilityItemUseModel.RequestSendForm()
    {
      euid = equipUniqueId.ToString(),
      auid = abilityItemId.ToString()
    }, (Action<SmithAbilityItemUseModel>) (ret =>
    {
      EquipItemInfo equip = (EquipItemInfo) null;
      if (ret.Error == Error.None)
      {
        equip = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equipUniqueId);
        if (equip != null)
        {
          if (MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equip))
            MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equip);
          MonoBehaviourSingleton<StatusManager>.I.UpdateUniqueEquip(equip);
        }
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_ABILITY);
      }
      call_back(ret.Error, equip);
    }));
  }

  public void SendGetAbilityList(
    ulong equipUniqueId,
    Action<Error, List<SmithGetAbilityList.Param>> call_back)
  {
    Protocol.Send<SmithGetAbilityList.RequestSendForm, SmithGetAbilityList>(SmithGetAbilityList.URL, new SmithGetAbilityList.RequestSendForm()
    {
      euid = equipUniqueId.ToString()
    }, (Action<SmithGetAbilityList>) (ret =>
    {
      List<SmithGetAbilityList.Param> objList = (List<SmithGetAbilityList.Param>) null;
      if (ret.Error == Error.None)
        objList = ret.result;
      call_back(ret.Error, objList);
    }));
  }

  public void SendGetAbilityListPreGenerate(
    uint createId,
    Action<Error, List<SmithGetAbilityListForCreateModel.Param>> call_back)
  {
    Protocol.Send<SmithGetAbilityListForCreateModel.RequestSendForm, SmithGetAbilityListForCreateModel>(SmithGetAbilityListForCreateModel.URL, new SmithGetAbilityListForCreateModel.RequestSendForm()
    {
      cid = createId.ToString()
    }, (Action<SmithGetAbilityListForCreateModel>) (ret =>
    {
      List<SmithGetAbilityListForCreateModel.Param> objList = (List<SmithGetAbilityListForCreateModel.Param>) null;
      if (ret.Error == Error.None)
        objList = ret.result;
      call_back(ret.Error, objList);
    }));
  }

  public void SendGrowSkill(
    SkillItemInfo base_skill,
    SkillItemInfo[] material,
    Action<SkillItemInfo, bool> call_back)
  {
    string str = base_skill.uniqueID.ToString();
    List<string> stringList = new List<string>();
    List<AlchemyGrowModel.RequestSendForm.MagiItem> magiItemList = new List<AlchemyGrowModel.RequestSendForm.MagiItem>();
    int index1 = 0;
    for (int length = material.Length; index1 < length; ++index1)
    {
      if (material[index1].tableID >= 401900001U && material[index1].tableID <= 401900005U)
      {
        bool flag = false;
        int index2 = 0;
        for (int count = magiItemList.Count; index2 < count; ++index2)
        {
          if (magiItemList[index2].uiuid == material[index1].uniqueID.ToString())
          {
            ++magiItemList[index2].num;
            flag = true;
            break;
          }
        }
        if (!flag)
          magiItemList.Add(new AlchemyGrowModel.RequestSendForm.MagiItem()
          {
            uiuid = material[index1].uniqueID.ToString(),
            num = 1
          });
      }
      else
        stringList.Add(material[index1].uniqueID.ToString());
    }
    Protocol.Send<AlchemyGrowModel.RequestSendForm, AlchemyGrowModel>(AlchemyGrowModel.URL, new AlchemyGrowModel.RequestSendForm()
    {
      suid = str,
      uuids = stringList,
      uiuids = magiItemList
    }, (Action<AlchemyGrowModel>) (ret =>
    {
      SkillItemInfo skillItemInfo = (SkillItemInfo) null;
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = ret.result.greatSuccess;
        skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(base_skill.uniqueID);
        if (skillItemInfo != null)
          GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skillItemInfo.uniqueID);
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_GROW);
      }
      call_back(skillItemInfo, flag);
    }));
  }

  public void SendExceedSkill(
    SkillItemInfo base_skill,
    SkillItemInfo[] material,
    Action<SkillItemInfo, bool> call_back)
  {
    string str = base_skill.uniqueID.ToString();
    List<string> stringList = new List<string>();
    int index = 0;
    for (int length = material.Length; index < length; ++index)
      stringList.Add(material[index].uniqueID.ToString());
    Protocol.Send<AlchemyExceedModel.RequestSendForm, AlchemyExceedModel>(AlchemyExceedModel.URL, new AlchemyExceedModel.RequestSendForm()
    {
      suid = str,
      uuids = stringList
    }, (Action<AlchemyExceedModel>) (ret =>
    {
      SkillItemInfo skillItemInfo = (SkillItemInfo) null;
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = ret.result.greatSuccess;
        skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(base_skill.uniqueID);
        if (skillItemInfo != null)
          GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skillItemInfo.uniqueID);
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_GROW);
      }
      call_back(skillItemInfo, flag);
    }));
  }

  public void SendRevertLithograph(ulong euid, Action<bool> call_back)
  {
    Protocol.Send<SmithRestoreModel.RequestSendForm, InventorySellEquipModel>(SmithRestoreModel.URL, new SmithRestoreModel.RequestSendForm()
    {
      euid = euid.ToString(),
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<InventorySellEquipModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void BackSection()
  {
    GameSceneEvent.Cancel();
    if (StatusManager.IsUnique())
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("UniqueStatus", "UniqueStatusToSmith");
    else
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Status", "StatusToSmith");
  }

  public void CheckSmithSectionBlur(
    string scene_name,
    string section_name,
    GameSceneTables.SectionData section_data)
  {
    if (section_data == (GameSceneTables.SectionData) null || section_data.type.IsDialog())
      return;
    if (section_name == "SmithGrowItemSelect" || section_name == "SmithGrow" || section_name == "SmithCreateItemSelect" || section_name == "SmithCreateItem")
    {
      if (!MonoBehaviourSingleton<FilterManager>.IsValid() || MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
        return;
      this.EnableSmithBlur();
    }
    else
    {
      if (!MonoBehaviourSingleton<FilterManager>.IsValid() || !MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur() || !(section_name != "StatusToSmith"))
        return;
      this.DisableSmithBlur(scene_name != "SmithScene");
    }
  }

  public void EnableSmithBlur() => this.isEnableSmithBlur = true;

  public void DisableSmithBlur(bool is_instans_end_anim)
  {
    if (!this.isEnableSmithBlur)
      return;
    this.isEnableSmithBlur = false;
  }

  public enum ERR_SMITH_SEND
  {
    NOT_SET_EQUIP_MATERIAL = -7, // 0xFFFFFFF9
    NOT_FOUND_EVOLVE_DATA = -6, // 0xFFFFFFFA
    ALREADY_INVENTORY_MAX = -5, // 0xFFFFFFFB
    NOT_ENOUGH_MONEY = -4, // 0xFFFFFFFC
    NOT_ENOUGH_MATERIAL = -3, // 0xFFFFFFFD
    NOT_LV_MAX = -2, // 0xFFFFFFFE
    ALREADY_LV_MAX = -1, // 0xFFFFFFFF
    NONE = 0,
  }

  public class SmithDataBase
  {
    public virtual void ResetData()
    {
    }
  }

  public class SmithCreateData : SmithManager.SmithDataBase
  {
    public SortBase.TYPE selectCreateEquipItemType;
    public EquipItemInfo selectEquipData;
    public EquipItemTable.EquipItemData generateTableData;
    public CreateEquipItemTable.CreateEquipItemData createEquipItemTable;

    public override void ResetData()
    {
      this.selectCreateEquipItemType = SortBase.TYPE.ONE_HAND_SWORD;
      this.selectEquipData = (EquipItemInfo) null;
      this.generateTableData = (EquipItemTable.EquipItemData) null;
      this.createEquipItemTable = (CreateEquipItemTable.CreateEquipItemData) null;
      base.ResetData();
    }
  }

  public class SmithGrowData : SmithManager.SmithDataBase
  {
    public EquipItemInfo selectEquipData;
    public SmithManager.SmithEvolveData evolveData;

    public override void ResetData()
    {
      this.selectEquipData = (EquipItemInfo) null;
      this.evolveData = (SmithManager.SmithEvolveData) null;
      base.ResetData();
    }
  }

  public class SmithEvolveData
  {
    public EquipItemInfo evolveBeforeEquipData;
    public EquipItemTable.EquipItemData[] evolveEquipDataTable;
    public EvolveEquipItemTable.EvolveEquipItemData[] evolveTable;
    public int selectIndex;

    public EquipItemTable.EquipItemData GetEquipTable()
    {
      return this.evolveEquipDataTable[this.selectIndex];
    }

    public EvolveEquipItemTable.EvolveEquipItemData GetEvolveTable()
    {
      return this.evolveTable[this.selectIndex];
    }
  }

  public class ResultData
  {
    public object itemData;
    public int beforeRarity;
    public int beforeLevel;
    public int beforeMaxLevel;
    public int beforeExceedCnt;
    public int beforeExp;
    public int beforeAtk;
    public int beforeDef;
    public int beforeHp;
    public int beforeElemAtk;
    public int beforeElemDef;
    public bool isExceed;
  }

  public class SmithBadgeData
  {
    public int[] weaponsBadgeNum;
    public int[] defenseBadgeNum;
    public int[] visualBadgeNum;
    public int[] pickupBadgeNum;
    public List<int>[] weaponsBadgeIds;
    public List<int>[] defenseBadgeIds;
    public List<int>[] visualBadgeIds;
    public List<int>[] pickupBadgeIds;

    public SmithBadgeData()
    {
      this.weaponsBadgeNum = new int[5];
      this.defenseBadgeNum = new int[4];
      this.visualBadgeNum = new int[4];
      this.pickupBadgeNum = new int[2];
      this.weaponsBadgeIds = new List<int>[5];
      this.defenseBadgeIds = new List<int>[4];
      this.visualBadgeIds = new List<int>[4];
      this.pickupBadgeIds = new List<int>[2];
    }

    public int GetBadgeNum(EQUIPMENT_TYPE type)
    {
      int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(type);
      return !Singleton<EquipItemTable>.I.IsWeapon(type) ? (!Singleton<EquipItemTable>.I.IsVisual(type) ? this.defenseBadgeNum[equipmentTypeIndex - 5] : this.visualBadgeNum[equipmentTypeIndex - 5]) : this.weaponsBadgeNum[equipmentTypeIndex];
    }

    public int GetPickupBadgeNum(bool is_weapon) => this.pickupBadgeNum[is_weapon ? 0 : 1];

    public int GetAllWeaponBadgeNum()
    {
      int num = 0;
      int index = 0;
      for (int length = this.weaponsBadgeNum.Length; index < length; ++index)
        num += this.weaponsBadgeNum[index];
      return num + this.pickupBadgeNum[0];
    }

    public int GetAllDefenseBadgeNum()
    {
      int num = 0;
      int index1 = 0;
      for (int length = this.defenseBadgeNum.Length; index1 < length; ++index1)
        num += this.defenseBadgeNum[index1];
      int index2 = 0;
      for (int length = this.visualBadgeNum.Length; index2 < length; ++index2)
        num += this.visualBadgeNum[index2];
      return num + this.pickupBadgeNum[1];
    }

    public int totalNum => this.GetAllWeaponBadgeNum() + this.GetAllDefenseBadgeNum();

    public void DebugShowCount()
    {
    }

    private void _DebugShowCount(int[] tmp, int[] now)
    {
      int index = 0;
      for (int length = tmp.Length; index < length; ++index)
      {
        Debug.LogWarning((object) $"[{(object) index}] = {(object) (now[index] - tmp[index])}");
        tmp[index] = now[index];
      }
    }
  }
}
