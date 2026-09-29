// Decompiled with JetBrains decompiler
// Type: StatusManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StatusManager : MonoBehaviourSingleton<StatusManager>
{
  private EquipSetInfo[] equipSet;
  private EquipSetCalculator[] equipSetCalc;
  private List<EquipSetCalculator> otherEquipSetCalc = new List<EquipSetCalculator>(14);
  private EquipItemInfo equipData;
  private EquipItemInfo selectEquipData;
  public bool recommended_update_icon;
  private EquipSetInfo[] localEquipSet;
  private StatusManager.LocalVisual localVisual;
  private int localEquipSetNo = -1;
  private EQUIPMENT_TYPE[] _enum;
  private int _weaponStartIndex = -1;
  private int _weaponEndIndex = -1;
  private bool firstSetUserStatus = true;
  public bool isEquipSetCalcUpdate = true;
  public const int OFFSET_FRIENDLIST_EQUIPSET = 4;
  public int otherEquipSetSaveIndex;
  public int selectUniqueEquipSetNo;
  private bool is_unique;
  private EquipSetInfo[] uniqueEquipSet;
  private EquipSetCalculator[] uniqueEquipSetCalc;

  public bool initialized { private set; get; }

  public List<BoostStatus> boostStatus { private set; get; }

  public List<TimeSlotEvent> timeSlotEvents { private set; get; }

  public int ENABLE_EQUIP_TYPE_MAX { private set; get; }

  public StatusManager()
  {
    this.ENABLE_EQUIP_TYPE_MAX = 1;
    EQUIPMENT_TYPE[] values = (EQUIPMENT_TYPE[]) Enum.GetValues(typeof (EQUIPMENT_TYPE));
    int index = 0;
    for (int length = values.Length; index < length; ++index)
    {
      if (this.IsWeapon(values[index]))
        ++this.ENABLE_EQUIP_TYPE_MAX;
    }
  }

  private bool IsWeapon(EQUIPMENT_TYPE type)
  {
    return type >= EQUIPMENT_TYPE.ONE_HAND_SWORD && type <= EQUIPMENT_TYPE.ARROW;
  }

  private bool IsArmor(EQUIPMENT_TYPE type)
  {
    return type >= EQUIPMENT_TYPE.ARMOR && type <= EQUIPMENT_TYPE.LEG;
  }

  public void _LoadTable() => this.StartCoroutine(this._LoadTableData());

  private IEnumerator _LoadTableData()
  {
    if (!this.initialized)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo_equip_model_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "EquipModelTable");
      LoadObject lo_equip_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "EquipItemTable");
      LoadObject lo_equip_exceed_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "EquipItemExceedTable");
      LoadObject lo_skill_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "SkillItemTable");
      LoadObject lo_ability_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "AbilityTable");
      LoadObject lo_ability_data_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "AbilityDataTable");
      LoadObject lo_ability_item_lot_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "AbilityItemLotTable");
      LoadObject lo_assigned_equipment_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "AssignedEquipmentTable");
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      if (!Singleton<EquipModelTable>.IsValid())
        Singleton<EquipModelTable>.Create();
      Singleton<EquipModelTable>.I.CreateTable(((TextAsset) lo_equip_model_table.loadedObject).text);
      if (!Singleton<EquipItemTable>.IsValid())
        Singleton<EquipItemTable>.Create();
      Singleton<EquipItemTable>.I.CreateTable(((TextAsset) lo_equip_table.loadedObject).text);
      if (!Singleton<EquipItemExceedTable>.IsValid())
        Singleton<EquipItemExceedTable>.Create();
      Singleton<EquipItemExceedTable>.I.CreateTable(((TextAsset) lo_equip_exceed_table.loadedObject).text);
      if (!Singleton<SkillItemTable>.IsValid())
        Singleton<SkillItemTable>.Create();
      Singleton<SkillItemTable>.I.CreateTable(((TextAsset) lo_skill_table.loadedObject).text);
      if (!Singleton<AbilityTable>.IsValid())
        Singleton<AbilityTable>.Create();
      Singleton<AbilityTable>.I.CreateTable(((TextAsset) lo_ability_table.loadedObject).text);
      if (!Singleton<AbilityDataTable>.IsValid())
        Singleton<AbilityDataTable>.Create();
      Singleton<AbilityDataTable>.I.CreateTable(((TextAsset) lo_ability_data_table.loadedObject).text);
      if (!Singleton<AbilityItemLotTable>.IsValid())
        Singleton<AbilityItemLotTable>.Create();
      Singleton<AbilityItemLotTable>.I.CreateTable(((TextAsset) lo_ability_item_lot_table.loadedObject).text);
      if (!Singleton<AssignedEquipmentTable>.IsValid())
        Singleton<AssignedEquipmentTable>.Create();
      Singleton<AssignedEquipmentTable>.I.CreateTable(((TextAsset) lo_assigned_equipment_table.loadedObject).text);
      this.initialized = true;
    }
  }

  public CharaInfo assignedCharaInfo { get; private set; }

  public AssignedEquipmentTable.AssignedEquipmentData assignedEquipmentData { get; private set; }

  public void SetAssignedEquipmentData(
    AssignedEquipmentTable.AssignedEquipmentData tableData,
    CharaInfo info)
  {
    this.assignedEquipmentData = tableData;
    this.assignedCharaInfo = info;
  }

  public void ClearTrial()
  {
    this.assignedEquipmentData = (AssignedEquipmentTable.AssignedEquipmentData) null;
    this.assignedCharaInfo = (CharaInfo) null;
  }

  public AssignedEquipmentTable.AssignedEquipmentData EventEquipSet { get; private set; }

  public void SetupEventEquipSet(uint questid)
  {
    this.EventEquipSet = Singleton<AssignedEquipmentTable>.I.GetAssignedEquipmentDataFromQuestId(questid);
  }

  public void ClearEventEquipSet()
  {
    this.EventEquipSet = (AssignedEquipmentTable.AssignedEquipmentData) null;
  }

  public bool HasEventEquipSet() => this.EventEquipSet != null;

  public EquipItemInfo GetEquippingItem() => this.equipData;

  public EquipItemInfo GetSelectEquipItem() => this.selectEquipData;

  public void SetEquippingItem(EquipItemInfo select_item) => this.equipData = select_item;

  public void SetSelectEquipItem(EquipItemInfo select_item) => this.selectEquipData = select_item;

  public void InitStatusEquipData()
  {
    this.equipData = this.selectEquipData = (EquipItemInfo) null;
    this.localEquipSet = (EquipSetInfo[]) null;
    this.localVisual = (StatusManager.LocalVisual) null;
    this.localEquipSetNo = -1;
  }

  public void SetLocalEquipSetNo(int no) => this.localEquipSetNo = no;

  public void SetTimeSlotEvents(List<TimeSlotEvent> statuses) => this.timeSlotEvents = statuses;

  public void CreateLocalEquipSetData()
  {
    this.localEquipSet = new EquipSetInfo[this.equipSet.Length];
    this.localEquipSetNo = MonoBehaviourSingleton<UserInfoManager>.IsValid() ? MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo : 0;
    this.is_unique = false;
    int set_no = 0;
    for (int length = this.localEquipSet.Length; set_no < length; ++set_no)
    {
      EquipSetInfo equipSet = this.GetEquipSet(set_no);
      this.localEquipSet[set_no] = new EquipSetInfo(new EquipItemInfo[7]
      {
        equipSet.item[0],
        equipSet.item[1],
        equipSet.item[2],
        equipSet.item[3],
        equipSet.item[4],
        equipSet.item[5],
        equipSet.item[6]
      }, equipSet.name, equipSet.showHelm, equipSet.acc);
    }
    int showHelm;
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || this.localEquipSet[this.localEquipSetNo].showHelm == (showHelm = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm))
      return;
    int index = 0;
    for (int length = this.localEquipSet.Length; index < length; ++index)
      this.localEquipSet[index].showHelm = showHelm;
  }

  public EquipSetInfo[] GetLocalEquipSet() => this.localEquipSet;

  public EquipSetInfo GetCurrentLocalEquipSet() => this.localEquipSet[this.GetCurrentEquipSetNo()];

  private int GetLocalEquipSetNo() => this.localEquipSetNo;

  public int GetCurrentEquipSetNo()
  {
    if (this.GetLocalEquipSetNo() != -1)
      return this.GetLocalEquipSetNo();
    return !MonoBehaviourSingleton<UserInfoManager>.IsValid() ? 0 : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
  }

  public void UpdateLocalEquipSet(int equip_no)
  {
    EquipSetInfo equipSet = this.GetEquipSet(equip_no);
    this.localEquipSet[equip_no] = new EquipSetInfo(new EquipItemInfo[7]
    {
      equipSet.item[0],
      equipSet.item[1],
      equipSet.item[2],
      equipSet.item[3],
      equipSet.item[4],
      equipSet.item[5],
      equipSet.item[6]
    }, equipSet.name, equipSet.showHelm, equipSet.acc);
    if (this.localEquipSet[equip_no].showHelm == 2)
      this.localEquipSet[equip_no].showHelm = MonoBehaviourSingleton<UserInfoManager>.IsValid() ? MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm : 1;
    this.ReplaceEquipSet(this.localEquipSet[equip_no], equip_no);
  }

  public void UpdateLocalInventory(EquipItemInfo item)
  {
    if (item == null)
      return;
    if (this.localEquipSet != null)
    {
      int index1 = 0;
      for (int length = this.localEquipSet.Length; index1 < length; ++index1)
      {
        int index2 = 0;
        for (int index3 = 7; index2 < index3; ++index2)
        {
          if (this.localEquipSet[index1].item[index2] != null && (long) this.localEquipSet[index1].item[index2].uniqueID == (long) item.uniqueID)
            this.localEquipSet[index1].item[index2] = item;
        }
      }
    }
    if (this.localVisual == null)
      return;
    int index = 0;
    for (int length = this.localVisual.visualItem.Length; index < length; ++index)
    {
      if (this.localVisual.visualItem[index] != null && (long) this.localVisual.visualItem[index].uniqueID == (long) item.uniqueID)
        this.localVisual.visualItem[index] = item;
    }
  }

  public bool IsEquippingLocal(EquipItemInfo item)
  {
    if (this.localEquipSet == null || item == null)
      return false;
    int index1 = 0;
    for (int length = this.localEquipSet.Length; index1 < length; ++index1)
    {
      int index2 = 0;
      for (int index3 = 7; index2 < index3; ++index2)
      {
        if (this.localEquipSet[index1].item[index2] != null && (long) this.localEquipSet[index1].item[index2].uniqueID == (long) item.uniqueID)
          return true;
      }
    }
    return false;
  }

  public void CheckChangeEquipSet(int set_no, Action<bool> callback)
  {
    List<StatusManager.SendSetEquipData> change_data = new List<StatusManager.SendSetEquipData>();
    int index1 = 0;
    for (int length = this.localEquipSet.Length; index1 < length; ++index1)
    {
      EquipSetInfo localEquip = this.localEquipSet[index1];
      ulong[] change_equip_items = new ulong[7];
      for (int index2 = 0; index2 < 7; ++index2)
        change_equip_items[index2] = localEquip.item[index2] != null ? localEquip.item[index2].uniqueID : 0UL;
      if (MonoBehaviourSingleton<StatusManager>.I.IsChangeEquipSetInfo(index1, change_equip_items, localEquip.showHelm, localEquip.acc))
        change_data.Add(new StatusManager.SendSetEquipData(index1, change_equip_items, localEquip.showHelm, localEquip.acc));
    }
    if (change_data.Count > 0 && !PartyManager.IsValidInParty())
    {
      MonoBehaviourSingleton<StatusManager>.I.SendEquipSet(set_no, change_data, (Action<Error>) (err =>
      {
        if (err == Error.None)
        {
          if (callback == null)
            return;
          callback(true);
        }
        else
        {
          if (callback == null)
            return;
          callback(false);
        }
      }));
      MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm = this.localEquipSet[this.localEquipSetNo].showHelm;
    }
    else if (set_no != MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo)
    {
      MonoBehaviourSingleton<StatusManager>.I.SendEquipSetNo(set_no, (Action<Error>) (err =>
      {
        if (err == Error.None)
        {
          if (callback == null)
            return;
          callback(true);
        }
        else
        {
          if (callback == null)
            return;
          callback(false);
        }
      }));
      MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm = this.localEquipSet[this.localEquipSetNo].showHelm;
    }
    else
    {
      MonoBehaviourSingleton<PartyManager>.I.SendIsEquip(false, (Action<bool>) (is_success => { }));
      if (callback == null)
        return;
      callback(true);
    }
  }

  public void CreateLocalVisualEquipData()
  {
    this.localVisual = new StatusManager.LocalVisual();
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    this.localVisual.visualItem[0] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.armorUniqId));
    this.localVisual.visualItem[1] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.helmUniqId));
    this.localVisual.visualItem[2] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.armUniqId));
    this.localVisual.visualItem[3] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.legUniqId));
    this.localVisual.isVisibleHelm = this.localEquipSet[this.localEquipSetNo].showHelm == 1;
  }

  public StatusManager.LocalVisual GetLocalVisualEquip() => this.localVisual;

  public bool IsEquippingLocalVisual(EquipItemInfo item)
  {
    if (this.localVisual == null || this.localVisual.visualItem == null || item == null)
      return false;
    int index = 0;
    for (int length = this.localVisual.visualItem.Length; index < length; ++index)
    {
      if (this.localVisual.visualItem[index] != null && (long) this.localVisual.visualItem[index].uniqueID == (long) item.uniqueID)
        return true;
    }
    return false;
  }

  public void CheckChangeVisualEquip(Action<bool> callback)
  {
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    ulong[] numArray = new ulong[4]
    {
      ulong.Parse(userStatus.armorUniqId),
      ulong.Parse(userStatus.helmUniqId),
      ulong.Parse(userStatus.armUniqId),
      ulong.Parse(userStatus.legUniqId)
    };
    bool flag = false;
    int index = 0;
    for (int length = this.localVisual.visualItem.Length; index < length; ++index)
    {
      if ((this.localVisual.visualItem[index] != null ? (long) this.localVisual.visualItem[index].uniqueID : 0L) != (long) numArray[index])
      {
        flag = true;
        break;
      }
    }
    if (flag)
    {
      MonoBehaviourSingleton<StatusManager>.I.SendVisualEquip(this.localVisual.VisialID(0), this.localVisual.VisialID(1), this.localVisual.VisialID(2), this.localVisual.VisialID(3), this.localVisual.isVisibleHelm, (Action<bool>) (is_success =>
      {
        if (callback == null)
          return;
        callback(is_success);
      }));
    }
    else
    {
      if (callback == null)
        return;
      callback(true);
    }
  }

  public void CheckChangeEquip(int equip_set_no, Action<bool> callback)
  {
    this.StartCoroutine(this._CheckChangeEquipCoroutine(equip_set_no, callback));
  }

  private IEnumerator _CheckChangeEquipCoroutine(int equip_set_no, Action<bool> callback)
  {
    bool recv_break = false;
    bool wait_visual_equip = true;
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeVisualEquip((Action<bool>) (is_success =>
    {
      if (!is_success)
      {
        if (callback != null)
          callback(false);
        recv_break = true;
      }
      else
        wait_visual_equip = false;
    }));
    bool wait_equip = true;
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquipSet(equip_set_no, (Action<bool>) (is_success =>
    {
      if (!is_success)
      {
        if (callback != null)
          callback(false);
        recv_break = true;
      }
      wait_equip = false;
    }));
    while (wait_equip | wait_visual_equip)
    {
      if (recv_break)
      {
        if (callback == null)
          yield break;
        callback(false);
        yield break;
      }
      yield return (object) null;
    }
    if (callback != null)
      callback(true);
  }

  public int EquipSetNum() => this.equipSet == null ? 0 : this.equipSet.Length;

  public EquipSetInfo GetEquipSet(int set_no)
  {
    return set_no >= this.equipSet.Length ? (EquipSetInfo) null : this.equipSet[set_no];
  }

  public EquipSetInfo[] GetEquipSets()
  {
    EquipSetInfo[] equipSets = new EquipSetInfo[this.equipSet.Length];
    int set_no = 0;
    for (int length = equipSets.Length; set_no < length; ++set_no)
    {
      EquipSetInfo equipSet = this.GetEquipSet(set_no);
      equipSets[set_no] = new EquipSetInfo(new EquipItemInfo[7]
      {
        equipSet.item[0],
        equipSet.item[1],
        equipSet.item[2],
        equipSet.item[3],
        equipSet.item[4],
        equipSet.item[5],
        equipSet.item[6]
      }, equipSet.name, equipSet.showHelm, equipSet.acc);
    }
    return equipSets;
  }

  public bool IsEquipping(EquipItemInfo item, int set_no = -1)
  {
    if (item == null || item.uniqueID == 0UL)
      return false;
    if (set_no != -1)
      return this.IsEquipping(set_no, item);
    int set_no1 = 0;
    for (int length = this.equipSet.Length; set_no1 < length; ++set_no1)
    {
      if (this.IsEquipping(set_no1, item))
        return true;
    }
    return false;
  }

  public void UpdateEquip(EquipItemInfo equip)
  {
    int set_no1 = 0;
    for (int length = this.equipSet.Length; set_no1 < length; ++set_no1)
      this.IsEquipping(set_no1, equip, (Action<int, int>) ((set_no, index) =>
      {
        EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(set_no);
        equipSet.item[index] = equip;
        this.ReplaceEquipItem(equipSet, set_no, index);
      }));
  }

  private bool IsEquipping(int set_no, EquipItemInfo item, Action<int, int> callback = null)
  {
    if (item == null || item.uniqueID == 0UL)
      return false;
    EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(set_no);
    if (equipSet != null)
    {
      int index = 0;
      for (int length = equipSet.item.Length; index < length; ++index)
      {
        if (equipSet.item[index] != null && equipSet.item[index].uniqueID != 0UL && (long) equipSet.item[index].uniqueID == (long) item.uniqueID)
        {
          if (callback != null)
            callback(set_no, index);
          return true;
        }
      }
    }
    return false;
  }

  public bool IsChangeEquipSetInfo(
    int set_no,
    ulong[] change_equip_items,
    int show_helm,
    AccessoryPlaceInfo acc)
  {
    if (set_no >= this.equipSet.Length)
      return false;
    EquipSetInfo equip = this.equipSet[set_no];
    if (change_equip_items != null && change_equip_items.Length != equip.item.Length)
      return false;
    if (equip.showHelm != show_helm)
      return true;
    int index = 0;
    for (int length = equip.item.Length; index < length; ++index)
    {
      ulong uniqueId = equip.item[index] != null ? equip.item[index].uniqueID : 0UL;
      if ((long) change_equip_items[index] != (long) uniqueId)
        return true;
    }
    return !equip.acc.IsEqual(acc);
  }

  public StageObjectManager.CreatePlayerInfo GetCreatePlayerInfo()
  {
    StageObjectManager.CreatePlayerInfo createPlayerInfo = new StageObjectManager.CreatePlayerInfo();
    createPlayerInfo.charaInfo = new CharaInfo();
    createPlayerInfo.extentionInfo = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
    if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
    {
      createPlayerInfo.charaInfo.name = PlayerPrefs.HasKey("Tut_Name") ? PlayerPrefs.GetString("Tut_Name") : "???";
      createPlayerInfo.charaInfo.comment = "";
      createPlayerInfo.charaInfo.hp = (XorInt) 200;
      createPlayerInfo.charaInfo.atk = (XorInt) 100;
      createPlayerInfo.charaInfo.def = (XorInt) 100;
      createPlayerInfo.charaInfo.level = (XorInt) 1;
      createPlayerInfo.charaInfo.aId = PlayerPrefs.GetInt("Tut_Armor");
      createPlayerInfo.charaInfo.hId = PlayerPrefs.GetInt("Tut_Head");
      createPlayerInfo.charaInfo.rId = PlayerPrefs.GetInt("Tut_Arm");
      createPlayerInfo.charaInfo.lId = PlayerPrefs.GetInt("Tut_Leg");
      createPlayerInfo.charaInfo.sex = PlayerPrefs.GetInt("Tut_Sex");
      createPlayerInfo.charaInfo.showHelm = 1;
    }
    else if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
    {
      UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
      createPlayerInfo.charaInfo.userId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
      createPlayerInfo.charaInfo.name = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
      createPlayerInfo.charaInfo.comment = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment;
      createPlayerInfo.charaInfo.code = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code;
      createPlayerInfo.charaInfo.hp = userStatus.hp;
      createPlayerInfo.charaInfo.atk = userStatus.atk;
      createPlayerInfo.charaInfo.def = userStatus.def;
      createPlayerInfo.charaInfo.level = userStatus.level;
      createPlayerInfo.charaInfo.sex = userStatus.sex;
      createPlayerInfo.charaInfo.faceId = userStatus.faceId;
      createPlayerInfo.charaInfo.hairId = userStatus.hairId;
      createPlayerInfo.charaInfo.hairColorId = userStatus.hairColorId;
      createPlayerInfo.charaInfo.skinId = userStatus.skinId;
      createPlayerInfo.charaInfo.voiceId = userStatus.voiceId;
      createPlayerInfo.charaInfo.aId = (int) MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armorUniqId);
      createPlayerInfo.charaInfo.hId = (int) MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.helmUniqId);
      createPlayerInfo.charaInfo.rId = (int) MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armUniqId);
      createPlayerInfo.charaInfo.lId = (int) MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.legUniqId);
      createPlayerInfo.charaInfo.showHelm = userStatus.showHelm;
      if (MonoBehaviourSingleton<PartyManager>.IsValid())
      {
        PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
        if (slotInfoByUserId != null && slotInfoByUserId.userInfo != null)
          createPlayerInfo.charaInfo.userClanData = slotInfoByUserId.userInfo.userClanData;
      }
      if (MonoBehaviourSingleton<GuildManager>.I.guildData != null)
      {
        createPlayerInfo.charaInfo.clanInfo = new CharaInfo.ClanInfo();
        createPlayerInfo.charaInfo.clanInfo.clanId = MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
        createPlayerInfo.charaInfo.clanInfo.tag = MonoBehaviourSingleton<GuildManager>.I.guildData.tag;
        createPlayerInfo.charaInfo.clanInfo.emblem = MonoBehaviourSingleton<GuildManager>.I.guildData.emblem;
      }
    }
    for (int equip_slot = 0; equip_slot < 7; ++equip_slot)
    {
      CharaInfo.EquipItem equipItem = (CharaInfo.EquipItem) null;
      EquipItemInfo equippingItemInfo = this.GetEquippingItemInfo(equip_slot);
      if (equippingItemInfo != null)
      {
        equipItem = new CharaInfo.EquipItem();
        equipItem.eId = (int) equippingItemInfo.tableID;
        equipItem.lv = equippingItemInfo.level;
        equipItem.exceed = equippingItemInfo.exceed;
        int index1 = 0;
        for (int maxSlot = equippingItemInfo.GetMaxSlot(); index1 < maxSlot; ++index1)
        {
          SkillItemInfo skillItem = equippingItemInfo.GetSkillItem(index1, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo);
          if (skillItem != null)
          {
            equipItem.sIds.Add((int) skillItem.tableID);
            equipItem.sLvs.Add(skillItem.level);
            equipItem.sExs.Add(skillItem.exceedCnt);
          }
        }
        EquipItemAbility[] lotteryAbility = equippingItemInfo.GetLotteryAbility();
        int index2 = 0;
        for (int length = lotteryAbility.Length; index2 < length; ++index2)
        {
          if (lotteryAbility[index2].id != 0U)
          {
            equipItem.aIds.Add((int) lotteryAbility[index2].id);
            equipItem.aPts.Add(lotteryAbility[index2].ap);
          }
        }
        AbilityItemInfo abilityItem = equippingItemInfo.GetAbilityItem();
        if (abilityItem != null && abilityItem.tableID != 0U)
          equipItem.ai = abilityItem.originalData;
      }
      if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField && equip_slot == 0)
      {
        equipItem = new CharaInfo.EquipItem();
        equipItem.eId = PlayerPrefs.GetInt("Tut_Weapon");
        equipItem.lv = 1;
        equipItem.sIds.Add(105200200);
        equipItem.sLvs.Add(1);
        equipItem.sExs.Add(0);
      }
      if (equipItem != null)
        createPlayerInfo.charaInfo.equipSet.Add(equipItem);
      if (equip_slot >= 0 && equip_slot < 3)
      {
        int num = -1;
        if (equipItem != null)
          num = createPlayerInfo.charaInfo.equipSet.Count - 1;
        createPlayerInfo.extentionInfo.weaponIndexList.Add(num);
      }
    }
    AccessoryPlaceInfo equippingAccessoryInfo = this.GetEquippingAccessoryInfo();
    if (equippingAccessoryInfo != null)
      createPlayerInfo.charaInfo.accessory = equippingAccessoryInfo.ConvertAccessory();
    return createPlayerInfo;
  }

  public StageObjectManager.CreatePlayerInfo GetAssignedCreatePlayerInfo()
  {
    StageObjectManager.CreatePlayerInfo createPlayerInfo = new StageObjectManager.CreatePlayerInfo();
    if (this.assignedCharaInfo != null)
      createPlayerInfo.charaInfo = this.assignedCharaInfo;
    return createPlayerInfo;
  }

  public int GetEquippingShowHelm(int set_no = -1)
  {
    if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
      set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
    if (this.equipSet == null || set_no < 0 || set_no >= this.equipSet.Length || this.equipSet[set_no] == null)
      return 1;
    if (this.equipSet[set_no].showHelm != 2)
      return this.equipSet[set_no].showHelm;
    return !MonoBehaviourSingleton<UserInfoManager>.IsValid() ? 1 : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm;
  }

  public EquipItemInfo GetEquippingItemInfo(int equip_slot, int set_no = -1)
  {
    if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
      set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
    if (this.equipSet == null)
      return (EquipItemInfo) null;
    if (set_no < 0)
      return (EquipItemInfo) null;
    if (set_no >= this.equipSet.Length)
      return (EquipItemInfo) null;
    if (equip_slot >= 7)
      return (EquipItemInfo) null;
    if (this.equipSet[set_no] == null)
      return (EquipItemInfo) null;
    return this.equipSet[set_no].item == null ? (EquipItemInfo) null : this.equipSet[set_no].item[equip_slot];
  }

  public AccessoryPlaceInfo GetEquippingAccessoryInfo(int set_no = -1)
  {
    if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
      set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
    if (this.equipSet == null)
      return (AccessoryPlaceInfo) null;
    if (set_no < 0)
      return (AccessoryPlaceInfo) null;
    if (set_no >= this.equipSet.Length)
      return (AccessoryPlaceInfo) null;
    return this.equipSet[set_no] == null ? (AccessoryPlaceInfo) null : this.equipSet[set_no].acc;
  }

  public uint GetEquippingItemTableID(int equip_slot, int set_no = -1)
  {
    EquipItemInfo equippingItemInfo = this.GetEquippingItemInfo(equip_slot, set_no);
    return equippingItemInfo == null ? 0U : equippingItemInfo.tableID;
  }

  public EquipItemInfo GetEquipmentWeaponInfo(int equip_slot = 0, int set_no = -1)
  {
    return equip_slot >= 3 ? (EquipItemInfo) null : this.GetEquippingItemInfo(equip_slot, set_no);
  }

  public string GetEquipItemGroupString(EQUIPMENT_TYPE type)
  {
    return Singleton<StringTable>.IsValid() ? StringTable.Get(STRING_CATEGORY.EQUIP, (uint) type) : (string) null;
  }

  public string GetSkillItemGroupString(SKILL_SLOT_TYPE type)
  {
    return Singleton<StringTable>.IsValid() ? StringTable.Get(STRING_CATEGORY.SKILL, (uint) type) : (string) null;
  }

  public EquipItemStatus GetEquipSetAllSkillParam(
    int equip_set_no,
    bool is_local = false,
    params int[] exclusion_index)
  {
    EquipItemStatus setAllSkillParam = new EquipItemStatus();
    EquipSetInfo[] equipSetInfoArray = is_local ? this.localEquipSet : this.equipSet;
    if (equipSetInfoArray == null)
      return (EquipItemStatus) null;
    for (int index1 = 0; index1 < 7; ++index1)
    {
      if (exclusion_index != null)
      {
        bool flag = false;
        if (exclusion_index != null)
        {
          int index2 = 0;
          for (int length = exclusion_index.Length; index2 < length; ++index2)
          {
            if (exclusion_index[index2] == index1)
            {
              flag = true;
              break;
            }
          }
        }
        if (flag)
          continue;
      }
      EquipItemInfo equipItemInfo = equipSetInfoArray[equip_set_no].item[index1];
      if (equipItemInfo != null)
        setAllSkillParam.Add(equipItemInfo.GetEquipSkillParam());
    }
    return setAllSkillParam;
  }

  public int GetEquipmentTypeIndex(EQUIPMENT_TYPE type)
  {
    int num = -1;
    if (this._enum == null)
    {
      this._enum = (EQUIPMENT_TYPE[]) Enum.GetValues(typeof (EQUIPMENT_TYPE));
      int index = 0;
      for (int length = this._enum.Length; index < length; ++index)
      {
        if (this._enum[index] == EQUIPMENT_TYPE.ONE_HAND_SWORD)
          this._weaponStartIndex = index;
        else if (this._enum[index] == EQUIPMENT_TYPE.ARROW)
          this._weaponEndIndex = index;
        if (this._enum[index] == type)
          num = index;
        if (this._weaponStartIndex >= 0 && this._weaponEndIndex >= 0 && num >= 0)
          break;
      }
    }
    else if (this.IsWeapon(type))
    {
      int index = 0;
      for (int length = this._enum.Length; index < length; ++index)
      {
        if (this._enum[index] == type)
        {
          num = index;
          break;
        }
      }
    }
    return !this.IsArmor(type) ? (num <= this._weaponEndIndex ? num - this._weaponStartIndex : -1) : this._weaponEndIndex + 1 - this._weaponStartIndex;
  }

  public void CalcSelfStatusParam(EquipSetInfo set_info, out int _atk, out int _def, out int _hp)
  {
    this.CalcSelfStatusParam(set_info, out _atk, out _def, out _hp, out int _, out int _);
  }

  public void CalcSelfStatusParam(
    EquipSetInfo set_info,
    out int _atk,
    out int _def,
    out int _hp,
    out int _elem_type_atk,
    out int _elem_type_def)
  {
    CharaInfo.EquipItem[] set_item = new CharaInfo.EquipItem[set_info.item.Length];
    int index = 0;
    for (int length = set_item.Length; index < length; ++index)
    {
      if (set_info.item[index] == null)
      {
        set_item[index] = (CharaInfo.EquipItem) null;
      }
      else
      {
        set_item[index] = new CharaInfo.EquipItem();
        set_item[index].eId = (int) set_info.item[index].tableID;
        set_item[index].lv = set_info.item[index].level;
        set_item[index].exceed = set_info.item[index].exceed;
        int slotNo = 0;
        for (int maxSlot = set_info.item[index].GetMaxSlot(); slotNo < maxSlot; ++slotNo)
        {
          SkillItemInfo orHomeEquipSkill = MonoBehaviourSingleton<StatusManager>.I.GetUniqueOrHomeEquipSkill(set_info.item[index], slotNo, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo);
          if (orHomeEquipSkill != null)
          {
            set_item[index].sIds.Add((int) orHomeEquipSkill.tableID);
            set_item[index].sLvs.Add(orHomeEquipSkill.level);
            set_item[index].sExs.Add(orHomeEquipSkill.exceedCnt);
          }
        }
      }
    }
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    this._CalcUserStatusParam(set_item, (int) userStatus.atk, (int) userStatus.def, (int) userStatus.hp, out _atk, out _def, out _hp, out _elem_type_atk, out _elem_type_def);
  }

  public void CalcUserStatusParam(CharaInfo user_info, out int _atk, out int _def, out int _hp)
  {
    this._CalcUserStatusParam(user_info.equipSet.ToArray(), (int) user_info.atk, (int) user_info.def, (int) user_info.hp, out _atk, out _def, out _hp, out int _, out int _);
  }

  public void CalcUserStatusParam(
    List<CharaInfo.EquipItem> equip_set,
    int base_atk,
    int base_def,
    int base_hp,
    out int _atk,
    out int _def,
    out int _hp)
  {
    this._CalcUserStatusParam(equip_set.ToArray(), base_atk, base_def, base_hp, out _atk, out _def, out _hp, out int _, out int _);
  }

  private void _CalcUserStatusParam(
    CharaInfo.EquipItem[] set_item,
    int status_atk,
    int status_def,
    int status_hp,
    out int _atk,
    out int _def,
    out int _hp,
    out int _elem_type_atk,
    out int _elem_type_def)
  {
    _atk = 0;
    _def = 0;
    _hp = 0;
    _elem_type_atk = 0;
    _elem_type_def = 0;
    int length1 = set_item.Length;
    EquipItemStatus skill = new EquipItemStatus();
    EquipItemStatus[] equip = new EquipItemStatus[length1];
    bool[] equip_is_weapon = new bool[length1];
    int[] equip_atk_elem_type = new int[length1];
    int[] equip_def_elem_type = new int[length1];
    int index = 0;
    int main_weapon_index = -1;
    EquipItemTable.EquipItemData mainWeaponItemData = (EquipItemTable.EquipItemData) null;
    Array.ForEach<CharaInfo.EquipItem>(set_item, (Action<CharaInfo.EquipItem>) (data =>
    {
      if (data == null)
      {
        equip[index] = (EquipItemStatus) null;
        ++index;
      }
      else
      {
        equip[index] = new EquipItemStatus();
        equip_atk_elem_type[index] = 6;
        equip_def_elem_type[index] = 6;
        EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) data.eId);
        if (equipItemData == null)
          return;
        bool flag = equipItemData.type < EQUIPMENT_TYPE.ARMOR;
        equip_is_weapon[index] = flag;
        if (main_weapon_index < 0 & flag)
        {
          main_weapon_index = index;
          mainWeaponItemData = equipItemData;
        }
        if (data.lv > 1)
        {
          GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(equipItemData.growID, (uint) data.lv);
          if (equipItemData != null && growEquipItemData != null)
          {
            equip[index].atk = growEquipItemData.GetGrowParamAtk((int) equipItemData.baseAtk);
            equip[index].def = growEquipItemData.GetGrowParamDef((int) equipItemData.baseDef);
            equip[index].hp = growEquipItemData.GetGrowParamHp((int) equipItemData.baseHp);
            int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(equipItemData.atkElement);
            int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(equipItemData.defElement);
            int index1 = 0;
            for (int length2 = growParamElemAtk.Length; index1 < length2; ++index1)
            {
              equip[index].elemAtk[index1] = growParamElemAtk[index1];
              equip[index].elemDef[index1] = growParamElemDef[index1];
            }
          }
        }
        else
        {
          equip[index].atk = (int) equipItemData.baseAtk;
          equip[index].def = (int) equipItemData.baseDef;
          equip[index].hp = (int) equipItemData.baseHp;
          int[] atkElement = equipItemData.atkElement;
          int[] defElement = equipItemData.defElement;
          int index2 = 0;
          for (int length3 = atkElement.Length; index2 < length3; ++index2)
          {
            equip[index].elemAtk[index2] = atkElement[index2];
            equip[index].elemDef[index2] = defElement[index2];
          }
        }
        int[] exceed_elem1 = (int[]) null;
        int[] exceed_elem2 = (int[]) null;
        EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = equipItemData.GetExceedParam((uint) data.exceed);
        if (exceedParam != null)
        {
          exceed_elem1 = exceedParam.atkElement;
          exceed_elem2 = exceedParam.defElement;
          int index3 = 0;
          for (int length4 = equip[index].elemAtk.Length; index3 < length4; ++index3)
          {
            equip[index].elemAtk[index3] += exceedParam.atkElement[index3];
            equip[index].elemDef[index3] += exceedParam.defElement[index3];
          }
          equip[index].atk += (int) exceedParam.atk;
          equip[index].def += (int) exceedParam.def;
          equip[index].hp += (int) exceedParam.hp;
        }
        equip_atk_elem_type[index] = equipItemData.GetElemAtkType(exceed_elem1);
        equip_def_elem_type[index] = equipItemData.GetElemDefType(exceed_elem2);
        int count = data.sIds.Count;
        if (count > 0 && count == data.sLvs.Count)
        {
          int[] array1 = data.sIds.ToArray();
          int[] array2 = data.sLvs.ToArray();
          for (int idx = 0; idx < count; ++idx)
          {
            SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) array1[idx]);
            if (skillItemData != null)
            {
              GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, array2[idx], data.GetSkillExceed(idx));
              if (growSkillItemData != null)
              {
                skill.atk += growSkillItemData.GetGrowParamAtk((int) skillItemData.baseAtk);
                int[] growParamElemAtk = growSkillItemData.GetGrowParamElemAtk(skillItemData.atkElement);
                int index4 = 0;
                for (int length5 = growParamElemAtk.Length; index4 < length5; ++index4)
                  skill.elemAtk[index4] += growParamElemAtk[index4];
                skill.def += growSkillItemData.GetGrowParamDef((int) skillItemData.baseDef);
                skill.hp += growSkillItemData.GetGrowParamHp((int) skillItemData.baseHp);
                int[] growParamElemDef = growSkillItemData.GetGrowParamElemDef(skillItemData.defElement);
                int index5 = 0;
                for (int length6 = growParamElemDef.Length; index5 < length6; ++index5)
                  skill.elemDef[index5] += growParamElemDef[index5];
              }
              else
              {
                skill.atk += (int) skillItemData.baseAtk;
                int[] atkElement = skillItemData.atkElement;
                int index6 = 0;
                for (int length7 = atkElement.Length; index6 < length7; ++index6)
                  skill.elemAtk[index6] += atkElement[index6];
                skill.def += (int) skillItemData.baseDef;
                skill.hp += (int) skillItemData.baseHp;
                int[] defElement = skillItemData.defElement;
                int index7 = 0;
                for (int length8 = defElement.Length; index7 < length8; ++index7)
                  skill.elemDef[index7] += defElement[index7];
              }
              if (mainWeaponItemData != null && skillItemData.IsMatchSupportEquipType(mainWeaponItemData.type))
              {
                for (int index8 = 0; index8 < skillItemData.supportValue.Length; ++index8)
                {
                  if (growSkillItemData != null)
                    skill.atk += growSkillItemData.GetGrowParamSupprtValue(skillItemData.supportValue, index8);
                  else
                    skill.atk += skillItemData.supportValue[index8];
                }
              }
            }
          }
        }
        ++index;
      }
    }));
    if (main_weapon_index < 0)
    {
      _atk = 0;
      _def = 0;
      _hp = 0;
      _elem_type_atk = 6;
      _elem_type_def = 6;
    }
    else
    {
      _elem_type_atk = equip_atk_elem_type[main_weapon_index];
      _atk = status_atk + equip[main_weapon_index].atk + equip[main_weapon_index].GetElemAtk(_elem_type_atk);
      _elem_type_def = 6;
      _def = status_def;
      _hp = status_hp;
      int index9 = 0;
      for (int length9 = equip.Length; index9 < length9; ++index9)
      {
        if (equip[index9] != null)
        {
          if (!equip_is_weapon[index9])
          {
            int num = 0;
            int index10 = equip_atk_elem_type[index9];
            if (index10 != 6 && index10 != 6)
              num = index10 != -1 ? equip[index9].elemAtk[index10] : equip[index9].elemAtk[0];
            _atk += equip[index9].atk + num;
          }
          if (!equip_is_weapon[index9] || index9 == main_weapon_index)
          {
            _def += equip[index9].def;
            _hp += equip[index9].hp;
          }
          if (equip_def_elem_type[index9] != 6)
            _elem_type_def = _elem_type_def != 6 ? -1 : equip_def_elem_type[index9];
        }
      }
      int num1 = skill.atk + skill.GetAllAtkElem();
      _atk += num1;
      _def += skill.def;
      _hp += skill.hp;
    }
  }

  public EquipItemAbilityCollection[] GetLocalEquipSetAbility(
    int set_no,
    EquipItemAbilityCollection.SwapData swap_data = null)
  {
    return this.GetEquipSetAbility(MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[set_no], swap_data);
  }

  public EquipItemAbilityCollection[] GetEquipSetAbility(
    EquipSetInfo set_info,
    EquipItemAbilityCollection.SwapData swap_data = null)
  {
    List<EquipItemAbilityCollection> abilityCollectionList = new List<EquipItemAbilityCollection>();
    int num = swap_data != null ? swap_data.index : -1;
    EquipSetInfo equipSetInfo = set_info.SwapArmorAndHelm();
    switch (num)
    {
      case 3:
        num = 4;
        break;
      case 4:
        num = 3;
        break;
    }
    int equip_index = 0;
    for (int length1 = equipSetInfo.item.Length; equip_index < length1; ++equip_index)
    {
      EquipItemInfo equipItemInfo = equipSetInfo.item[equip_index];
      EquipItemAbilityCollection.COLLECTION_TYPE type = EquipItemAbilityCollection.COLLECTION_TYPE.NORMAL;
      if (num == equip_index)
      {
        if (swap_data.item != null)
        {
          int index = 0;
          for (int length2 = swap_data.item.ability.Length; index < length2; ++index)
          {
            EquipItemAbility swap_a = swap_data.item.ability[index];
            EquipItemAbilityCollection abilityCollection = abilityCollectionList.Find((Predicate<EquipItemAbilityCollection>) (data => (int) data.ability.id == (int) swap_a.id));
            if (abilityCollection == null)
              abilityCollectionList.Add(new EquipItemAbilityCollection(swap_a, equip_index, EquipItemAbilityCollection.COLLECTION_TYPE.SWAP_IN));
            else
              abilityCollection.Add(swap_a.ap, equip_index, EquipItemAbilityCollection.COLLECTION_TYPE.SWAP_IN);
          }
        }
        type = EquipItemAbilityCollection.COLLECTION_TYPE.SWAP_OUT;
      }
      if (equipItemInfo != null && equipItemInfo.ability != null && equipItemInfo.ability.Length != 0)
      {
        int index = 0;
        for (int length3 = equipItemInfo.ability.Length; index < length3; ++index)
        {
          EquipItemAbility a = equipItemInfo.ability[index];
          if (a.id != 0U && !a.IsNeedUpdate() && a.IsActiveAbility())
          {
            if (num == equip_index)
              a = a.Inverse();
            EquipItemAbilityCollection abilityCollection = abilityCollectionList.Find((Predicate<EquipItemAbilityCollection>) (data => (int) data.ability.id == (int) a.id));
            if (abilityCollection == null)
              abilityCollectionList.Add(new EquipItemAbilityCollection(a, equip_index, type));
            else
              abilityCollection.Add(a.ap, equip_index, type);
          }
        }
      }
    }
    return abilityCollectionList.ToArray();
  }

  public EquipSetInfo CreateEquipSetData(List<CharaInfo.EquipItem> list)
  {
    int num = 0;
    EquipItemInfo[] equip_item_info_ary = new EquipItemInfo[7];
    for (int index1 = 0; index1 < 7; ++index1)
    {
      if (index1 < list.Count)
      {
        EquipItemInfo equipItemInfo = new EquipItemInfo(list[index1]);
        int index2;
        switch (equipItemInfo.tableData.type)
        {
          case EQUIPMENT_TYPE.ARMOR:
            index2 = 3;
            break;
          case EQUIPMENT_TYPE.HELM:
            index2 = 4;
            break;
          case EQUIPMENT_TYPE.ARM:
            index2 = 5;
            break;
          case EQUIPMENT_TYPE.LEG:
            index2 = 6;
            break;
          default:
            index2 = num++;
            break;
        }
        equip_item_info_ary[index2] = equipItemInfo;
      }
    }
    return new EquipSetInfo(equip_item_info_ary, "装備セット", 1, new AccessoryPlaceInfo());
  }

  public BoostStatus GetBoostStatus(USE_ITEM_EFFECT_TYPE type)
  {
    if (this.boostStatus == null)
      return (BoostStatus) null;
    DateTime now = TimeManager.GetNow();
    int index = 0;
    for (int count = this.boostStatus.Count; index < count; ++index)
    {
      if (this.boostStatus[index].Type == type && this.boostStatus[index].value != 0 && (this.boostStatus[index].endDate == null || DateTime.Parse(this.boostStatus[index].endDate.date).CompareTo(now) > 0))
        return this.boostStatus[index];
    }
    return (BoostStatus) null;
  }

  public int GetBoostStatusEndTimestamp(USE_ITEM_EFFECT_TYPE type)
  {
    BoostStatus boostStatus = this.GetBoostStatus(type);
    return boostStatus == null ? 0 : boostStatus.endTimestamp;
  }

  public bool IsEffectedItem(ItemInfo item)
  {
    int index = 0;
    for (int length = item.tableData.useEffectTypes.Length; index < length; ++index)
    {
      if (this.GetBoostStatus(item.tableData.useEffectTypes[index]) != null)
        return true;
    }
    return false;
  }

  public void SetUserStatus()
  {
    if (!this.firstSetUserStatus)
      return;
    this.firstSetUserStatus = false;
    OnceStatusInfoModel.Param statusinfo = MonoBehaviourSingleton<OnceManager>.I.result.statusinfo;
    MonoBehaviourSingleton<UserInfoManager>.I.SetUserInfoAndUserStatus(statusinfo.user, statusinfo.userStatus, statusinfo.userClan, statusinfo.unlockStamps, statusinfo.selectedDegrees, statusinfo.unlockDegrees);
    this.equipSet = new EquipSetInfo[statusinfo.equipSets.Count];
    statusinfo.equipSets.ForEach((Action<EquipSetSimple>) (o => this.equipSet[o.setNo] = new EquipSetInfo(o)));
    this.boostStatus = statusinfo.boost;
    MonoBehaviourSingleton<PresentManager>.I.SetPresentNum(statusinfo.userStatus.present);
    MonoBehaviourSingleton<FriendManager>.I.SetFollowNum(statusinfo.followNum);
    MonoBehaviourSingleton<FriendManager>.I.SetFollowerNum(statusinfo.followerNum);
    MonoBehaviourSingleton<GlobalSettingsManager>.I.SetHasVisuals(statusinfo.hasVisuals);
    int index = 0;
    for (int count = statusinfo.accessorySets.Count; index < count; ++index)
    {
      AccessorySet accessorySet = statusinfo.accessorySets[index];
      if (accessorySet.attachPlace == "-1")
        this.equipSet[accessorySet.setNo].acc.Clear();
      else
        this.equipSet[accessorySet.setNo].acc.Add(accessorySet.uniqId, accessorySet.attachPlace);
    }
    int length = this.equipSet.Length;
    this.equipSetCalc = new EquipSetCalculator[length];
    for (int setNo = 0; setNo < length; ++setNo)
    {
      this.equipSetCalc[setNo] = new EquipSetCalculator();
      this.equipSetCalc[setNo].SetEquipSet(this.equipSet[setNo], setNo);
    }
    this.SetUserUniqueEquipStatus();
  }

  public void ResetEquipSetInfo()
  {
    this.CreateLocalEquipSetData();
    int length = this.equipSet.Length;
    this.equipSetCalc = new EquipSetCalculator[length];
    for (int setNo = 0; setNo < length; ++setNo)
    {
      this.equipSetCalc[setNo] = new EquipSetCalculator();
      this.equipSetCalc[setNo].SetEquipSet(this.equipSet[setNo], setNo);
    }
  }

  public void AccessoryOn(string _uuid, ACCESSORY_PART _part)
  {
    if (this.localEquipSet == null || this.localEquipSetNo == -1)
    {
      Debug.LogWarning((object) "AccessoryOn() : invalid");
    }
    else
    {
      this.localEquipSet[this.localEquipSetNo].acc.Clear();
      this.localEquipSet[this.localEquipSetNo].acc.Add(_uuid, ((int) _part).ToString());
    }
  }

  public void AccessoryOff(string _uuid, ACCESSORY_PART _part)
  {
    if (this.localEquipSet == null || this.localEquipSetNo == -1)
      Debug.LogWarning((object) "AccessoryOff() : invalid");
    else
      this.localEquipSet[this.localEquipSetNo].acc.Clear();
  }

  public void SendEquipSet(
    int equip_set_no,
    List<StatusManager.SendSetEquipData> change_data,
    Action<Error> call_back)
  {
    StatusEquipModel.RequestSendForm send_form = new StatusEquipModel.RequestSendForm();
    send_form.select = equip_set_no;
    change_data.ForEach((Action<StatusManager.SendSetEquipData>) (data =>
    {
      send_form.nos.Add(data.set_no);
      send_form.wuids0.Add(data.item[0].ToString());
      send_form.wuids1.Add(data.item[1].ToString());
      send_form.wuids2.Add(data.item[2].ToString());
      send_form.auids.Add(data.item[3].ToString());
      send_form.huids.Add(data.item[4].ToString());
      send_form.ruids.Add(data.item[5].ToString());
      send_form.luids.Add(data.item[6].ToString());
      send_form.shows.Add(data.show_helm);
      send_form.accs.Add(data.accs);
    }));
    Protocol.Send<StatusEquipModel.RequestSendForm, StatusEquipModel>(StatusEquipModel.URL, send_form, (Action<StatusEquipModel>) (ret =>
    {
      if (ret.Error == Error.None)
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
      call_back(ret.Error);
    }));
  }

  public void SendEquipSetNo(int set_no, Action<Error> call_back)
  {
    StatusEquipSetModel.RequestSendForm postData = new StatusEquipSetModel.RequestSendForm();
    postData.no = set_no;
    if (PartyManager.IsValidInParty())
      postData.partyId = MonoBehaviourSingleton<PartyManager>.I.GetPartyId();
    Protocol.Send<StatusEquipSetModel.RequestSendForm, StatusEquipSetModel>(StatusEquipSetModel.URL, postData, (Action<StatusEquipSetModel>) (ret =>
    {
      if (ret.Error == Error.None)
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
      call_back(ret.Error);
    }));
  }

  public void SendMainSetSkill(
    ulong equip_uniq_id,
    ulong skill_uniq_id,
    int slot_index,
    int setNo,
    Action<bool> call_back)
  {
    StatusEquipSkillModel.RequestSendForm postData = new StatusEquipSkillModel.RequestSendForm();
    postData.euid = equip_uniq_id.ToString();
    postData.suid = skill_uniq_id.ToString();
    postData.slot = slot_index;
    postData.no = setNo;
    if (MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equip_uniq_id) == null)
    {
      call_back(false);
    }
    else
    {
      ulong now_equip_skill_uniq_id = 0;
      for (LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      {
        EquipSetSkillData equipSetSkillData = linkedListNode.Value.equipSetSkill.Find((Predicate<EquipSetSkillData>) (x => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo()));
        if (equipSetSkillData != null && (long) equipSetSkillData.equipItemUniqId == (long) equip_uniq_id && equipSetSkillData.equipSlotNo == slot_index)
        {
          now_equip_skill_uniq_id = linkedListNode.Value.uniqueID;
          break;
        }
      }
      Protocol.Send<StatusEquipSkillModel.RequestSendForm, StatusEquipSkillModel>(StatusEquipSkillModel.URL, postData, (Action<StatusEquipSkillModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skill_uniq_id);
          SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(now_equip_skill_uniq_id);
          if (skillItemInfo != null)
          {
            EquipSetSkillData equipSetSkillData = skillItemInfo.equipSetSkill.Find((Predicate<EquipSetSkillData>) (x => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo()));
            skillItemInfo.equipSetSkill.Remove(equipSetSkillData);
          }
          MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
        }
        call_back(flag);
      }));
    }
  }

  public void SendDetachMainSkill(
    ulong equip_uniq_id,
    int slot,
    int setNo,
    Action<bool> call_back)
  {
    Protocol.Send<StatusDetachSkillModel.RequestSendForm, StatusDetachSkillModel>(StatusDetachSkillModel.URL, new StatusDetachSkillModel.RequestSendForm()
    {
      euid = equip_uniq_id.ToString(),
      slot = slot,
      no = setNo
    }, (Action<StatusDetachSkillModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
      }
      call_back(flag);
    }));
  }

  public void SendDetachAllMainSkill(ulong equip_uniq_id, int _setNo, Action<bool> _callback)
  {
    Protocol.Send<StatusDetachAllSkillModel.RequestSendForm, StatusDetachAllSkillModel>(StatusDetachAllSkillModel.URL, new StatusDetachAllSkillModel.RequestSendForm()
    {
      euid = equip_uniq_id.ToString(),
      no = _setNo
    }, (Action<StatusDetachAllSkillModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
      }
      if (_callback == null)
        return;
      _callback(flag);
    }));
  }

  public void SendDetachMainAllSkillFromEvery(int _setNo, Action<bool> _callback)
  {
    Protocol.Send<StatusDetachAllSkillFromEveryModel.RequestSendForm, StatusDetachAllSkillFromEveryModel>(StatusDetachAllSkillFromEveryModel.URL, new StatusDetachAllSkillFromEveryModel.RequestSendForm()
    {
      no = _setNo
    }, (Action<StatusDetachAllSkillFromEveryModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
      }
      if (_callback == null)
        return;
      _callback(flag);
    }));
  }

  public void SendInventoryEquipLock(ulong equip_uniq_id, Action<bool, EquipItemInfo> call_back)
  {
    Protocol.Send<InventoryEquipLockModel.RequestSendForm, InventoryEquipLockModel>(InventoryEquipLockModel.URL, new InventoryEquipLockModel.RequestSendForm()
    {
      euid = equip_uniq_id.ToString()
    }, (Action<InventoryEquipLockModel>) (ret =>
    {
      bool flag = false;
      EquipItemInfo equipItemInfo = (EquipItemInfo) null;
      if (ret.Error == Error.None)
      {
        flag = true;
        equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equip_uniq_id);
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE);
      }
      call_back(flag, equipItemInfo);
    }));
  }

  public void SendInventorySkillLock(ulong skill_uniq_id, Action<bool, SkillItemInfo> call_back)
  {
    Protocol.Send<InventorySkillLockModel.RequestSendForm, InventorySkillLockModel>(InventorySkillLockModel.URL, new InventorySkillLockModel.RequestSendForm()
    {
      suid = skill_uniq_id.ToString()
    }, (Action<InventorySkillLockModel>) (ret =>
    {
      bool flag = false;
      SkillItemInfo skillItemInfo = (SkillItemInfo) null;
      if (ret.Error == Error.None)
      {
        flag = true;
        skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(skill_uniq_id);
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE);
      }
      call_back(flag, skillItemInfo);
    }));
  }

  public void SendVisualEquip(
    ulong armor_id,
    ulong helm_id,
    ulong arm_id,
    ulong leg_id,
    bool is_visible_helm,
    Action<bool> call_back)
  {
    Protocol.Send<StatusVisualEquipModel.RequestSendForm, StatusVisualEquipModel>(StatusVisualEquipModel.URL, new StatusVisualEquipModel.RequestSendForm()
    {
      auid = armor_id.ToString(),
      huid = helm_id.ToString(),
      ruid = arm_id.ToString(),
      luid = leg_id.ToString()
    }, (Action<StatusVisualEquipModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void OnDiff(BaseModelDiff.DiffEquipSet diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      diff.add.ForEach((Action<EquipSetSimple>) (o =>
      {
        if (o.setNo > this.equipSet.Length)
          return;
        Array.Resize<EquipSetInfo>(ref this.equipSet, o.setNo + 1);
        this.equipSet[o.setNo] = new EquipSetInfo(o);
      }));
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<EquipSetSimple>) (o =>
      {
        if (o.setNo >= this.equipSet.Length)
          return;
        AccessoryPlaceInfo acc = this.equipSet[o.setNo].acc;
        this.equipSet[o.setNo] = new EquipSetInfo(o);
        this.equipSet[o.setNo].acc = acc;
      }));
      flag = true;
    }
    if (!flag)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
  }

  public void OnDiff(BaseModelDiff.DiffBoost diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      this.boostStatus.AddRange((IEnumerable<BoostStatus>) diff.add);
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<BoostStatus>) (boost =>
      {
        BoostStatus boostStatus = this.boostStatus.Find((Predicate<BoostStatus>) (b => b.type == boost.type));
        boostStatus.value = boost.value;
        boostStatus.endDate = boost.endDate;
        boostStatus.endTimestamp = boost.endTimestamp;
      }));
      flag = true;
    }
    if (!flag)
      return;
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.SetUpBoostAnimator();
    if (!MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnduranceStatus>.I.SetUpBoostAnimator();
  }

  public void OnDiff(BaseModelDiff.DiffAccessorySet diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        AccessorySet accessorySet = diff.add[index];
        if (accessorySet.setNo < this.equipSet.Length)
        {
          this.equipSet[accessorySet.setNo].acc.Clear();
          if (accessorySet.attachPlace != "-1")
            this.equipSet[accessorySet.setNo].acc.Add(accessorySet.uniqId, accessorySet.attachPlace);
        }
      }
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        AccessorySet accessorySet = diff.update[index];
        if (accessorySet.setNo < this.equipSet.Length)
        {
          this.equipSet[accessorySet.setNo].acc.Clear();
          if (accessorySet.attachPlace != "-1")
            this.equipSet[accessorySet.setNo].acc.Add(accessorySet.uniqId, accessorySet.attachPlace);
        }
      }
      flag = true;
    }
    if (!flag)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
  }

  public void SendEquipSetName(string name, int setNo, Action<bool> callback)
  {
    Protocol.Send<StatusEquipSetNameChangeModel.RequestSendForm, BaseModel>(StatusEquipSetNameChangeModel.URL, new StatusEquipSetNameChangeModel.RequestSendForm()
    {
      name = name,
      setNo = setNo
    }, (Action<BaseModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
      callback(flag);
    }));
  }

  public EquipSetCalculator GetEquipSetCalculator(int setNo)
  {
    return this.equipSetCalc == null || setNo >= this.equipSetCalc.Length ? (EquipSetCalculator) null : this.equipSetCalc[setNo];
  }

  public void ReplaceEquipItem(EquipSetInfo info, int setNo, int index)
  {
    if (this.equipSetCalc == null || setNo >= this.equipSetCalc.Length)
      return;
    CharaInfo.EquipItem equipItem = info.ConvertSelfEquipSetItem(index, setNo);
    this.equipSetCalc[setNo].SetEquipItem(equipItem, index);
  }

  public void ReplaceEquipSet(EquipSetInfo info, int setNo)
  {
    if (this.equipSetCalc == null || setNo >= this.equipSetCalc.Length)
      return;
    this.equipSetCalc[setNo].SetEquipSet(info, setNo);
  }

  public void ReplaceEquipSets(EquipSetInfo[] info)
  {
    int setNo = 0;
    for (int length = info.Length; setNo < length; ++setNo)
      this.equipSetCalc[setNo].SetEquipSet(info[setNo], setNo);
    this.isEquipSetCalcUpdate = false;
  }

  public void SwapWeapon(int swapIndex, int nowIndex)
  {
    this.equipSetCalc[this.localEquipSetNo].SwapWeapon(swapIndex, nowIndex);
  }

  public EquipSetCalculator GetOtherEquipSetCalculator(int index)
  {
    if (index >= this.otherEquipSetCalc.Count)
    {
      int num = 0;
      for (int index1 = index + 1 - this.otherEquipSetCalc.Count; num < index1; ++num)
        this.otherEquipSetCalc.Add(new EquipSetCalculator());
    }
    return this.otherEquipSetCalc[index];
  }

  public static bool IsUnique()
  {
    return MonoBehaviourSingleton<StatusManager>.IsValid() && MonoBehaviourSingleton<StatusManager>.I.is_unique;
  }

  public void InitUniqueEquip() => this.is_unique = false;

  public void SetSelectUniqueEquipSetNo(int setNo)
  {
    this.selectUniqueEquipSetNo = setNo;
    if (this.selectUniqueEquipSetNo != -1)
      return;
    this.selectUniqueEquipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
  }

  public void SetUserUniqueEquipStatus()
  {
    OnceStatusInfoModel.Param statusinfo = MonoBehaviourSingleton<OnceManager>.I.result.statusinfo;
    this.uniqueEquipSet = new EquipSetInfo[statusinfo.uniqueEquipSets.Count];
    statusinfo.uniqueEquipSets.ForEach((Action<EquipSetSimple>) (o => this.uniqueEquipSet[o.setNo] = new EquipSetInfo(o)));
    int index = 0;
    for (int count = statusinfo.uniqueAccessorySets.Count; index < count; ++index)
    {
      AccessorySet uniqueAccessorySet = statusinfo.uniqueAccessorySets[index];
      if (uniqueAccessorySet.attachPlace == "-1")
        this.uniqueEquipSet[uniqueAccessorySet.setNo].acc.Clear();
      else
        this.uniqueEquipSet[uniqueAccessorySet.setNo].acc.Add(uniqueAccessorySet.uniqId, uniqueAccessorySet.attachPlace);
    }
    int length = this.uniqueEquipSet.Length;
    this.uniqueEquipSetCalc = new EquipSetCalculator[length];
    for (int setNo = 0; setNo < length; ++setNo)
    {
      this.uniqueEquipSetCalc[setNo] = new EquipSetCalculator();
      this.uniqueEquipSetCalc[setNo].SetEquipSet(this.uniqueEquipSet[setNo], setNo, true);
    }
  }

  public void CreateLocalUniqueEquipSetData()
  {
    this.localEquipSet = new EquipSetInfo[this.uniqueEquipSet.Length];
    this.localEquipSetNo = MonoBehaviourSingleton<UserInfoManager>.IsValid() ? MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo : 0;
    this.is_unique = true;
    int set_no = 0;
    for (int length = this.localEquipSet.Length; set_no < length; ++set_no)
    {
      EquipSetInfo uniqueEquipSet = this.GetUniqueEquipSet(set_no);
      this.localEquipSet[set_no] = new EquipSetInfo(new EquipItemInfo[7]
      {
        uniqueEquipSet.item[0],
        uniqueEquipSet.item[1],
        uniqueEquipSet.item[2],
        uniqueEquipSet.item[3],
        uniqueEquipSet.item[4],
        uniqueEquipSet.item[5],
        uniqueEquipSet.item[6]
      }, uniqueEquipSet.name, uniqueEquipSet.showHelm, uniqueEquipSet.order, uniqueEquipSet.acc);
    }
  }

  public bool checkEquipMagi(int set_no)
  {
    EquipSetInfo localEquip = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[set_no];
    for (int index1 = 0; index1 < localEquip.item.Length; ++index1)
    {
      if (localEquip.item[index1] != null)
      {
        for (int index2 = 0; index2 < localEquip.item[index1].GetMaxSlot(); ++index2)
        {
          if (localEquip.item[index1].GetUniqueSkillItem(index2) != null)
            return true;
        }
      }
    }
    return false;
  }

  public EquipSetInfo GetUniqueEquipSet(int set_no)
  {
    return set_no >= this.uniqueEquipSet.Length ? (EquipSetInfo) null : this.uniqueEquipSet[set_no];
  }

  public EquipSetInfo GetOrderUniqueEquipSet(int order)
  {
    for (int index = 0; index < this.uniqueEquipSet.Length; ++index)
    {
      if (this.uniqueEquipSet[index].order == order)
        return this.uniqueEquipSet[index];
    }
    return (EquipSetInfo) null;
  }

  public int GetOrderUniqueEquipSetNo(int order)
  {
    for (int uniqueEquipSetNo = 0; uniqueEquipSetNo < this.uniqueEquipSet.Length; ++uniqueEquipSetNo)
    {
      if (this.uniqueEquipSet[uniqueEquipSetNo].order == order)
        return uniqueEquipSetNo;
    }
    return -1;
  }

  public EquipSetCalculator GetLocalEquipSetCalculator(int setNo)
  {
    return this.is_unique ? this.GetUniqueEquipSetCalculator(setNo) : this.GetEquipSetCalculator(setNo);
  }

  public SkillItemInfo GetUniqueOrHomeEquipSkill(EquipItemInfo equip, int slotNo, int setNo)
  {
    return this.is_unique ? equip.GetUniqueSkillItem(slotNo) : equip.GetSkillItem(slotNo, setNo);
  }

  public void SendUniqueEquipSet(
    List<StatusManager.SendSetEquipData> change_data,
    Action<Error> call_back)
  {
    UniqueStatusEquipModel.RequestSendForm send_form = new UniqueStatusEquipModel.RequestSendForm();
    change_data.ForEach((Action<StatusManager.SendSetEquipData>) (data =>
    {
      send_form.nos.Add(data.set_no);
      send_form.wuids0.Add(data.item[0].ToString());
      send_form.wuids1.Add(data.item[1].ToString());
      send_form.wuids2.Add(data.item[2].ToString());
      send_form.auids.Add(data.item[3].ToString());
      send_form.huids.Add(data.item[4].ToString());
      send_form.ruids.Add(data.item[5].ToString());
      send_form.luids.Add(data.item[6].ToString());
      send_form.shows.Add(data.show_helm);
      send_form.accs.Add(data.accs);
    }));
    Protocol.Send<UniqueStatusEquipModel.RequestSendForm, UniqueStatusEquipModel>(UniqueStatusEquipModel.URL, send_form, (Action<UniqueStatusEquipModel>) (ret =>
    {
      if (ret.Error == Error.None)
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
      call_back(ret.Error);
    }));
  }

  public void OnDiff(BaseModelDiff.DiffUniqueEquipSet diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<EquipSetSimple>) (o =>
      {
        if (o.setNo >= this.uniqueEquipSet.Length)
          return;
        AccessoryPlaceInfo acc = this.uniqueEquipSet[o.setNo].acc;
        this.uniqueEquipSet[o.setNo] = new EquipSetInfo(o);
        this.uniqueEquipSet[o.setNo].acc = acc;
        this.uniqueEquipSet[o.setNo].order = o.order;
      }));
      flag = true;
    }
    if (!flag)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
  }

  public void SendSetSkill(
    ulong equip_uniq_id,
    ulong skill_uniq_id,
    int slot_index,
    int setNo,
    Action<bool> call_back)
  {
    if (StatusManager.IsUnique())
      this.SendUniqueSetSkill(equip_uniq_id, skill_uniq_id, slot_index, (Action<bool>) (is_success => call_back(is_success)));
    else
      this.SendMainSetSkill(equip_uniq_id, skill_uniq_id, slot_index, setNo, (Action<bool>) (is_success => call_back(is_success)));
  }

  public void SendDetachSkill(ulong equip_uniq_id, int slot, int setNo, Action<bool> call_back)
  {
    if (StatusManager.IsUnique())
      this.SendUniqueDetachSkill(equip_uniq_id, slot, (Action<bool>) (is_success => call_back(is_success)));
    else
      this.SendDetachMainSkill(equip_uniq_id, slot, setNo, (Action<bool>) (is_success => call_back(is_success)));
  }

  public void SendUniqueSetSkillMultiple(
    List<ulong> equip_uniq_ids,
    List<ulong> skill_uniq_ids,
    List<int> slots_index,
    Action<bool> call_back)
  {
    UniqueEquipSkillMultiple.RequestSendForm postData = new UniqueEquipSkillMultiple.RequestSendForm();
    postData.euids = equip_uniq_ids.ConvertAll<string>((Converter<ulong, string>) (x => x.ToString()));
    postData.suids = skill_uniq_ids.ConvertAll<string>((Converter<ulong, string>) (x => x.ToString()));
    postData.slots = slots_index.ConvertAll<string>((Converter<int, string>) (x => x.ToString()));
    List<ulong> remove_equip_skill = new List<ulong>();
    LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetFirstNode();
    for (int index = 0; index < equip_uniq_ids.Count; ++index)
    {
      ulong equipUniqId = equip_uniq_ids[index];
      int num = slots_index[index];
      ulong skillUniqId = skill_uniq_ids[index];
      for (; linkedListNode != null; linkedListNode = linkedListNode.Next)
      {
        EquipSetSkillData uniqueEquipSetSkill = linkedListNode.Value.uniqueEquipSetSkill;
        if (uniqueEquipSetSkill != null && (long) uniqueEquipSetSkill.equipItemUniqId == (long) equipUniqId && uniqueEquipSetSkill.equipSlotNo == num && (long) linkedListNode.Value.uniqueID != (long) skillUniqId)
        {
          remove_equip_skill.Add(linkedListNode.Value.uniqueID);
          break;
        }
      }
    }
    Protocol.Send<UniqueEquipSkillMultiple.RequestSendForm, UniqueEquipSkillMultiple>(UniqueEquipSkillMultiple.URL, postData, (Action<UniqueEquipSkillMultiple>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        for (int index = 0; index < skill_uniq_ids.Count; ++index)
          GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skill_uniq_ids[index]);
        for (int index = 0; index < remove_equip_skill.Count; ++index)
        {
          SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(remove_equip_skill[index]);
          if (skillItemInfo != null)
          {
            skillItemInfo.uniqueEquipSetSkill.equipItemUniqId = 0UL;
            skillItemInfo.uniqueEquipSetSkill.equipSlotNo = 0;
          }
        }
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
      }
      call_back(flag);
    }));
  }

  public void SendUniqueSetSkill(
    ulong equip_uniq_id,
    ulong skill_uniq_id,
    int slot_index,
    Action<bool> call_back)
  {
    UniqueStatusEquipSkillModel.RequestSendForm postData = new UniqueStatusEquipSkillModel.RequestSendForm();
    postData.euid = equip_uniq_id.ToString();
    postData.suid = skill_uniq_id.ToString();
    postData.slot = slot_index;
    if (MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equip_uniq_id) == null)
    {
      call_back(false);
    }
    else
    {
      ulong now_equip_skill_uniq_id = 0;
      for (LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      {
        EquipSetSkillData uniqueEquipSetSkill = linkedListNode.Value.uniqueEquipSetSkill;
        if (uniqueEquipSetSkill != null && (long) uniqueEquipSetSkill.equipItemUniqId == (long) equip_uniq_id && uniqueEquipSetSkill.equipSlotNo == slot_index)
        {
          now_equip_skill_uniq_id = linkedListNode.Value.uniqueID;
          break;
        }
      }
      Protocol.Send<UniqueStatusEquipSkillModel.RequestSendForm, UniqueStatusEquipSkillModel>(UniqueStatusEquipSkillModel.URL, postData, (Action<UniqueStatusEquipSkillModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skill_uniq_id);
          SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(now_equip_skill_uniq_id);
          if (skillItemInfo != null)
          {
            skillItemInfo.uniqueEquipSetSkill.equipItemUniqId = 0UL;
            skillItemInfo.uniqueEquipSetSkill.equipSlotNo = 0;
          }
          MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
        }
        call_back(flag);
      }));
    }
  }

  public void SendUniqueDetachSkill(ulong equip_uniq_id, int slot, Action<bool> call_back)
  {
    Protocol.Send<UniqueStatusDetachSkillModel.RequestSendForm, UniqueStatusDetachSkillModel>(UniqueStatusDetachSkillModel.URL, new UniqueStatusDetachSkillModel.RequestSendForm()
    {
      euid = equip_uniq_id.ToString(),
      slot = slot
    }, (Action<UniqueStatusDetachSkillModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
      }
      call_back(flag);
    }));
  }

  public int GetCurrentUniqueEquipSetNo()
  {
    if (this.GetLocalEquipSetNo() != -1)
      return this.GetLocalEquipSetNo();
    return !MonoBehaviourSingleton<UserInfoManager>.IsValid() ? 0 : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
  }

  public void SendDetachAllSkill(ulong equip_uniq_id, int _setNo, Action<bool> _callback)
  {
    if (StatusManager.IsUnique())
      this.SendDetachUniqueAllSkill(equip_uniq_id, _setNo, (Action<bool>) (isSucces => _callback(isSucces)));
    else
      this.SendDetachAllMainSkill(equip_uniq_id, _setNo, (Action<bool>) (isSucces => _callback(isSucces)));
  }

  public void SendDetachUniqueAllSkill(ulong equip_uniq_id, int _setNo, Action<bool> _callback)
  {
    Protocol.Send<UniqueStatusDetachAllSkillModel.RequestSendForm, UniqueStatusDetachAllSkillModel>(UniqueStatusDetachAllSkillModel.URL, new UniqueStatusDetachAllSkillModel.RequestSendForm()
    {
      euid = equip_uniq_id.ToString()
    }, (Action<UniqueStatusDetachAllSkillModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
      }
      if (_callback == null)
        return;
      _callback(flag);
    }));
  }

  public void SendDetachAllSkillFromEvery(int _setNo, Action<bool> _callback)
  {
    if (StatusManager.IsUnique())
      this.SendDetachUniqueAllSkillFromEvery(_setNo, (Action<bool>) (is_success => _callback(is_success)));
    else
      this.SendDetachMainAllSkillFromEvery(_setNo, (Action<bool>) (is_success => _callback(is_success)));
  }

  public void SendDetachUniqueAllSkillFromEvery(int _setNo, Action<bool> _callback)
  {
    Protocol.Send<UniqueStatusDetachSkillAllEquipSet.RequestSendForm, UniqueStatusDetachSkillAllEquipSet>(UniqueStatusDetachSkillAllEquipSet.URL, new UniqueStatusDetachSkillAllEquipSet.RequestSendForm()
    {
      setNo = _setNo
    }, (Action<UniqueStatusDetachSkillAllEquipSet>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
      }
      if (_callback == null)
        return;
      _callback(flag);
    }));
  }

  public void SendUniqueEquipSetName(string name, int setNo, Action<bool> callback)
  {
    Protocol.Send<UniqueEquipSetNameChangeModel.RequestSendForm, UniqueEquipSetNameChangeModel>(UniqueEquipSetNameChangeModel.URL, new UniqueEquipSetNameChangeModel.RequestSendForm()
    {
      name = name,
      setNo = setNo
    }, (Action<UniqueEquipSetNameChangeModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
      callback(flag);
    }));
  }

  public bool IsUniqueEquipping(EquipItemInfo item, int set_no = -1)
  {
    if (item == null || item.uniqueID == 0UL)
      return false;
    if (set_no != -1)
      return this.IsUniqueEquipping(set_no, item);
    int set_no1 = 0;
    for (int length = this.uniqueEquipSet.Length; set_no1 < length; ++set_no1)
    {
      if (this.IsUniqueEquipping(set_no1, item))
        return true;
    }
    return false;
  }

  private bool IsUniqueEquipping(int set_no, EquipItemInfo item, Action<int, int> callback = null)
  {
    if (item == null || item.uniqueID == 0UL)
      return false;
    EquipSetInfo uniqueEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSet(set_no);
    if (uniqueEquipSet != null)
    {
      int index = 0;
      for (int length = uniqueEquipSet.item.Length; index < length; ++index)
      {
        if (uniqueEquipSet.item[index] != null && uniqueEquipSet.item[index].uniqueID != 0UL && (long) uniqueEquipSet.item[index].uniqueID == (long) item.uniqueID)
        {
          if (callback != null)
            callback(set_no, index);
          return true;
        }
      }
    }
    return false;
  }

  public bool IsUniqueLocalEquipping(EquipItemInfo item, int set_no = -1)
  {
    if (item == null || item.uniqueID == 0UL)
      return false;
    int set_no1 = 0;
    for (int length = this.localEquipSet.Length; set_no1 < length; ++set_no1)
    {
      if (set_no != set_no1 && this.IsUniqueLocalEquipping(set_no1, item))
        return true;
    }
    return false;
  }

  private bool IsUniqueLocalEquipping(int set_no, EquipItemInfo item, Action<int, int> callback = null)
  {
    if (item == null || item.uniqueID == 0UL)
      return false;
    EquipSetInfo localEquip = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[set_no];
    if (localEquip != null)
    {
      int index = 0;
      for (int length = localEquip.item.Length; index < length; ++index)
      {
        if (localEquip.item[index] != null && localEquip.item[index].uniqueID != 0UL && (long) localEquip.item[index].uniqueID == (long) item.uniqueID)
        {
          if (callback != null)
            callback(set_no, index);
          return true;
        }
      }
    }
    return false;
  }

  public void CheckChangeUniqueEquip(Action<bool> callback)
  {
    this.StartCoroutine(this._CheckChangeUniqueEquipCoroutine(callback));
  }

  private IEnumerator _CheckChangeUniqueEquipCoroutine(Action<bool> callback)
  {
    bool recv_break = false;
    bool wait_equip = true;
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet((Action<bool>) (is_success =>
    {
      if (!is_success)
      {
        if (callback != null)
          callback(false);
        recv_break = true;
      }
      wait_equip = false;
    }));
    while (wait_equip)
    {
      if (recv_break)
      {
        if (callback == null)
          yield break;
        callback(false);
        yield break;
      }
      yield return (object) null;
    }
    if (callback != null)
      callback(true);
  }

  public void CheckChangeUniqueEquipSet(Action<bool> callback)
  {
    List<StatusManager.SendSetEquipData> change_data = new List<StatusManager.SendSetEquipData>();
    int index1 = 0;
    for (int length = this.localEquipSet.Length; index1 < length; ++index1)
    {
      EquipSetInfo localEquip = this.localEquipSet[index1];
      ulong[] change_equip_items = new ulong[7];
      for (int index2 = 0; index2 < 7; ++index2)
        change_equip_items[index2] = localEquip.item[index2] != null ? localEquip.item[index2].uniqueID : 0UL;
      if (MonoBehaviourSingleton<StatusManager>.I.IsChangeUniqueEquipSetInfo(index1, change_equip_items, localEquip.showHelm, localEquip.acc))
        change_data.Add(new StatusManager.SendSetEquipData(index1, change_equip_items, localEquip.showHelm, localEquip.acc));
    }
    if (change_data.Count > 0 && !PartyManager.IsValidInParty())
    {
      MonoBehaviourSingleton<StatusManager>.I.SendUniqueEquipSet(change_data, (Action<Error>) (err =>
      {
        if (err == Error.None)
        {
          if (callback == null)
            return;
          callback(true);
        }
        else
        {
          if (callback == null)
            return;
          callback(false);
        }
      }));
    }
    else
    {
      if (callback == null)
        return;
      callback(true);
    }
  }

  public bool IsChangeUniqueEquipSetInfo(
    int set_no,
    ulong[] change_equip_items,
    int show_helm,
    AccessoryPlaceInfo acc)
  {
    if (set_no >= this.uniqueEquipSet.Length)
      return false;
    EquipSetInfo uniqueEquip = this.uniqueEquipSet[set_no];
    if (change_equip_items != null && change_equip_items.Length != uniqueEquip.item.Length)
      return false;
    if (uniqueEquip.showHelm != show_helm)
      return true;
    int index = 0;
    for (int length = uniqueEquip.item.Length; index < length; ++index)
    {
      ulong uniqueId = uniqueEquip.item[index] != null ? uniqueEquip.item[index].uniqueID : 0UL;
      if ((long) change_equip_items[index] != (long) uniqueId)
        return true;
    }
    return !uniqueEquip.acc.IsEqual(acc);
  }

  public int UniqueEquipSetNum() => this.uniqueEquipSet == null ? 0 : this.uniqueEquipSet.Length;

  public EquipItemInfo GetUniqueEquippingItemInfo(int equip_slot, int set_no = -1)
  {
    if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
      set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
    if (this.uniqueEquipSet == null)
      return (EquipItemInfo) null;
    if (set_no < 0)
      return (EquipItemInfo) null;
    if (set_no >= this.uniqueEquipSet.Length)
      return (EquipItemInfo) null;
    if (equip_slot >= 7)
      return (EquipItemInfo) null;
    if (this.uniqueEquipSet[set_no] == null)
      return (EquipItemInfo) null;
    return this.uniqueEquipSet[set_no].item == null ? (EquipItemInfo) null : this.uniqueEquipSet[set_no].item[equip_slot];
  }

  public StageObjectManager.CreatePlayerInfo GetCreateUniquePlayerInfo(int order)
  {
    StageObjectManager.CreatePlayerInfo uniquePlayerInfo = new StageObjectManager.CreatePlayerInfo()
    {
      charaInfo = new CharaInfo(),
      extentionInfo = new StageObjectManager.CreatePlayerInfo.ExtentionInfo()
    };
    uniquePlayerInfo.extentionInfo.uniqueEquipmentIndex = order;
    EquipSetInfo orderUniqueEquipSet = this.GetOrderUniqueEquipSet(order);
    if (orderUniqueEquipSet == null)
    {
      uniquePlayerInfo.charaInfo = (CharaInfo) null;
      return uniquePlayerInfo;
    }
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
    {
      UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
      uniquePlayerInfo.charaInfo.userId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
      uniquePlayerInfo.charaInfo.name = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
      uniquePlayerInfo.charaInfo.comment = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment;
      uniquePlayerInfo.charaInfo.code = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code;
      uniquePlayerInfo.charaInfo.hp = userStatus.hp;
      uniquePlayerInfo.charaInfo.atk = userStatus.atk;
      uniquePlayerInfo.charaInfo.def = userStatus.def;
      uniquePlayerInfo.charaInfo.level = userStatus.level;
      uniquePlayerInfo.charaInfo.sex = userStatus.sex;
      uniquePlayerInfo.charaInfo.faceId = userStatus.faceId;
      uniquePlayerInfo.charaInfo.hairId = userStatus.hairId;
      uniquePlayerInfo.charaInfo.hairColorId = userStatus.hairColorId;
      uniquePlayerInfo.charaInfo.skinId = userStatus.skinId;
      uniquePlayerInfo.charaInfo.voiceId = userStatus.voiceId;
      EquipItemInfo equipItemInfo1 = orderUniqueEquipSet.item[3];
      EquipItemInfo equipItemInfo2 = orderUniqueEquipSet.item[4];
      EquipItemInfo equipItemInfo3 = orderUniqueEquipSet.item[5];
      EquipItemInfo equipItemInfo4 = orderUniqueEquipSet.item[6];
      string str_uniq_id1 = equipItemInfo1 != null ? equipItemInfo1.uniqueID.ToString() : "0";
      string str_uniq_id2 = equipItemInfo2 != null ? equipItemInfo2.uniqueID.ToString() : "0";
      string str_uniq_id3 = equipItemInfo3 != null ? equipItemInfo3.uniqueID.ToString() : "0";
      string str_uniq_id4 = equipItemInfo4 != null ? equipItemInfo4.uniqueID.ToString() : "0";
      uniquePlayerInfo.charaInfo.aId = (int) MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(str_uniq_id1);
      uniquePlayerInfo.charaInfo.hId = (int) MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(str_uniq_id2);
      uniquePlayerInfo.charaInfo.rId = (int) MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(str_uniq_id3);
      uniquePlayerInfo.charaInfo.lId = (int) MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(str_uniq_id4);
      uniquePlayerInfo.charaInfo.showHelm = orderUniqueEquipSet.showHelm;
      if (MonoBehaviourSingleton<PartyManager>.IsValid())
      {
        PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
        if (slotInfoByUserId != null && slotInfoByUserId.userInfo != null)
          uniquePlayerInfo.charaInfo.userClanData = slotInfoByUserId.userInfo.userClanData;
      }
      uniquePlayerInfo.charaInfo.equipSetName = orderUniqueEquipSet.name;
    }
    for (int index1 = 0; index1 < 7; ++index1)
    {
      CharaInfo.EquipItem equipItem = (CharaInfo.EquipItem) null;
      EquipItemInfo equipItemInfo = orderUniqueEquipSet.item[index1];
      if (equipItemInfo != null)
      {
        equipItem = new CharaInfo.EquipItem();
        equipItem.eId = (int) equipItemInfo.tableID;
        equipItem.lv = equipItemInfo.level;
        equipItem.exceed = equipItemInfo.exceed;
        int index2 = 0;
        for (int maxSlot = equipItemInfo.GetMaxSlot(); index2 < maxSlot; ++index2)
        {
          SkillItemInfo uniqueSkillItem = equipItemInfo.GetUniqueSkillItem(index2);
          if (uniqueSkillItem != null)
          {
            equipItem.sIds.Add((int) uniqueSkillItem.tableID);
            equipItem.sLvs.Add(uniqueSkillItem.level);
            equipItem.sExs.Add(uniqueSkillItem.exceedCnt);
          }
        }
        EquipItemAbility[] lotteryAbility = equipItemInfo.GetLotteryAbility();
        int index3 = 0;
        for (int length = lotteryAbility.Length; index3 < length; ++index3)
        {
          if (lotteryAbility[index3].id != 0U)
          {
            equipItem.aIds.Add((int) lotteryAbility[index3].id);
            equipItem.aPts.Add(lotteryAbility[index3].ap);
          }
        }
        AbilityItemInfo abilityItem = equipItemInfo.GetAbilityItem();
        if (abilityItem != null && abilityItem.tableID != 0U)
          equipItem.ai = abilityItem.originalData;
        if (equipItem != null)
          uniquePlayerInfo.charaInfo.equipSet.Add(equipItem);
      }
      if (index1 >= 0 && index1 < 3)
      {
        int num = -1;
        if (equipItem != null)
          num = uniquePlayerInfo.charaInfo.equipSet.Count - 1;
        uniquePlayerInfo.extentionInfo.weaponIndexList.Add(num);
      }
    }
    AccessoryPlaceInfo acc = orderUniqueEquipSet.acc;
    if (acc != null)
      uniquePlayerInfo.charaInfo.accessory = acc.ConvertAccessory();
    return uniquePlayerInfo;
  }

  public void OnDiff(BaseModelDiff.DiffUniqueAccessorySet diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      int index = 0;
      for (int count = diff.add.Count; index < count; ++index)
      {
        AccessorySet accessorySet = diff.add[index];
        if (accessorySet.setNo < this.uniqueEquipSet.Length)
        {
          this.uniqueEquipSet[accessorySet.setNo].acc.Clear();
          if (accessorySet.attachPlace != "-1")
            this.uniqueEquipSet[accessorySet.setNo].acc.Add(accessorySet.uniqId, accessorySet.attachPlace);
        }
      }
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      int index = 0;
      for (int count = diff.update.Count; index < count; ++index)
      {
        AccessorySet accessorySet = diff.update[index];
        if (accessorySet.setNo < this.uniqueEquipSet.Length)
        {
          this.uniqueEquipSet[accessorySet.setNo].acc.Clear();
          if (accessorySet.attachPlace != "-1")
            this.uniqueEquipSet[accessorySet.setNo].acc.Add(accessorySet.uniqId, accessorySet.attachPlace);
        }
      }
      flag = true;
    }
    if (!flag)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
  }

  public AccessoryPlaceInfo GetUniqueEquippingAccessoryInfo(int set_no = -1)
  {
    if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
      set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
    if (this.uniqueEquipSet == null)
      return (AccessoryPlaceInfo) null;
    if (set_no < 0)
      return (AccessoryPlaceInfo) null;
    if (set_no >= this.uniqueEquipSet.Length)
      return (AccessoryPlaceInfo) null;
    return this.uniqueEquipSet[set_no] == null ? (AccessoryPlaceInfo) null : this.uniqueEquipSet[set_no].acc;
  }

  public void ReplaceUniqueEquipSet(EquipSetInfo info, int setNo)
  {
    if (this.uniqueEquipSetCalc == null || setNo >= this.uniqueEquipSetCalc.Length)
      return;
    this.uniqueEquipSetCalc[setNo].SetEquipSet(info, setNo, true);
  }

  public void ReplaceUniqueEquipSets(EquipSetInfo[] info)
  {
    int setNo = 0;
    for (int length = info.Length; setNo < length; ++setNo)
      this.uniqueEquipSetCalc[setNo].SetEquipSet(info[setNo], setNo, true);
    this.isEquipSetCalcUpdate = false;
  }

  public bool IsEquipSkillSlotCheck(SkillItemInfo skillInfo, EquipItemInfo equipInfo, int slotNo)
  {
    if (skillInfo == null || equipInfo == null || skillInfo.uniqueEquipSetSkill == null)
      return false;
    ulong equipItemUniqId = skillInfo.uniqueEquipSetSkill.equipItemUniqId;
    int equipSlotNo = skillInfo.uniqueEquipSetSkill.equipSlotNo;
    if (!skillInfo.isUniqueAttached)
      return false;
    return (long) equipItemUniqId != (long) equipInfo.uniqueID || equipSlotNo != slotNo;
  }

  public bool CopyEquipSetCheck(int setNo)
  {
    EquipSetInfo equipSet = this.GetEquipSet(setNo);
    for (int index1 = 0; index1 < equipSet.item.Length; ++index1)
    {
      if (this.IsUniqueLocalEquipping(equipSet.item[index1], this.localEquipSetNo))
        return false;
      int maxSlot = equipSet.item[index1] != null ? equipSet.item[index1].GetMaxSlot() : 0;
      for (int index2 = 0; index2 < maxSlot; ++index2)
      {
        int num = index2;
        if (equipSet.item[index1].IsExceedSkillSlot(num))
          num = equipSet.item[index1].GetExceedSkillSlotNo(num);
        if (this.IsEquipSkillSlotCheck(equipSet.item[index1].GetSkillItem(num, setNo), equipSet.item[index1], num))
          return false;
      }
    }
    return true;
  }

  public void CopyEquipSet(int setNo, Action<bool> call_back)
  {
    EquipSetInfo equipSet = this.GetEquipSets()[setNo];
    EquipSetInfo localEquip = this.localEquipSet[this.localEquipSetNo];
    List<ulong> skill_uniq_ids = new List<ulong>();
    List<ulong> equip_uniq_ids = new List<ulong>();
    List<int> slots_index = new List<int>();
    List<ulong> ulongList = new List<ulong>();
    for (int index1 = 0; index1 < localEquip.item.Length; ++index1)
    {
      int maxSlot = localEquip.item[index1] != null ? localEquip.item[index1].GetMaxSlot() : 0;
      for (int index2 = 0; index2 < maxSlot; ++index2)
      {
        SkillItemInfo uniqueSkillItem = localEquip.item[index1].GetUniqueSkillItem(index2);
        if (uniqueSkillItem != null)
          ulongList.Add(uniqueSkillItem.uniqueID);
      }
    }
    for (int index = 0; index < equipSet.item.Length; ++index)
      localEquip.item[index] = this.IsUniqueLocalEquipping(equipSet.item[index], this.localEquipSetNo) ? (EquipItemInfo) null : equipSet.item[index];
    for (int index3 = 0; index3 < localEquip.item.Length; ++index3)
    {
      EquipItemInfo equipInfo = localEquip.item[index3];
      int maxSlot = equipInfo != null ? equipInfo.GetMaxSlot() : 0;
      for (int index4 = 0; index4 < maxSlot; ++index4)
      {
        SkillItemInfo skillItem = equipInfo.GetSkillItem(index4, setNo);
        ulong uniqueId = skillItem != null ? skillItem.uniqueID : 0UL;
        int num = index4;
        if (equipInfo.IsExceedSkillSlot(num))
          num = equipInfo.GetExceedSkillSlotNo(num);
        if (!this.IsEquipSkillSlotCheck(skillItem, equipInfo, num) || ulongList.Contains(uniqueId))
        {
          skill_uniq_ids.Add(uniqueId);
          equip_uniq_ids.Add(equipInfo.uniqueID);
          slots_index.Add(num);
        }
        else
        {
          skill_uniq_ids.Add(0UL);
          equip_uniq_ids.Add(equipInfo.uniqueID);
          slots_index.Add(num);
        }
      }
    }
    if (equip_uniq_ids.Count > 0)
      this.SendUniqueSetSkillMultiple(equip_uniq_ids, skill_uniq_ids, slots_index, call_back);
    else
      call_back(true);
    this.ReplaceUniqueEquipSet(localEquip, this.localEquipSetNo);
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO);
  }

  public void ReplaceUniqueEquipItem(EquipSetInfo info, int setNo, int index)
  {
    if (this.uniqueEquipSetCalc == null || setNo >= this.uniqueEquipSetCalc.Length)
      return;
    CharaInfo.EquipItem equipItem = info.ConvertSelfUniqueEquipSetItem(index, setNo);
    this.uniqueEquipSetCalc[setNo].SetEquipItem(equipItem, index);
  }

  public void UpdateUniqueEquip(EquipItemInfo equip)
  {
    if (!this.IsUniqueEquipping(equip))
      return;
    int set_no1 = 0;
    for (int length = this.uniqueEquipSet.Length; set_no1 < length; ++set_no1)
      this.IsUniqueEquipping(set_no1, equip, (Action<int, int>) ((set_no, index) =>
      {
        EquipSetInfo uniqueEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSet(set_no);
        uniqueEquipSet.item[index] = equip;
        this.ReplaceUniqueEquipItem(uniqueEquipSet, set_no, index);
      }));
  }

  public EquipSetCalculator GetUniqueEquipSetCalculator(int setNo)
  {
    return this.uniqueEquipSetCalc == null || setNo >= this.uniqueEquipSetCalc.Length || setNo < 0 ? (EquipSetCalculator) null : this.uniqueEquipSetCalc[setNo];
  }

  public void SwapUniqueWeapon(int swapIndex, int nowIndex)
  {
    this.uniqueEquipSetCalc[this.localEquipSetNo].SwapWeapon(swapIndex, nowIndex);
  }

  public void SwapClosetUniqueWeapon(int swapSetNo, int nowSetNo)
  {
  }

  public void ChangeOrderNo(int order, int setNo, Action<bool> call_back)
  {
    EquipSetInfo orderUniqueEquipSet = this.GetOrderUniqueEquipSet(order);
    if (orderUniqueEquipSet != null)
      orderUniqueEquipSet.order = 0;
    this.uniqueEquipSet[setNo].order = order;
    this.SendUniqueEquipSetOrder(call_back);
  }

  public void RemoveOrderNo(int setNo, Action<bool> call_back)
  {
    this.uniqueEquipSet[setNo].order = 0;
    this.SendUniqueEquipSetOrder(call_back);
    this.localEquipSet[setNo].order = 0;
  }

  public void SendUniqueEquipSetOrder(Action<bool> call_back)
  {
    UniqueEquipSetOrderModel.RequestSendForm postData = new UniqueEquipSetOrderModel.RequestSendForm();
    postData.selects = new List<int>();
    postData.nos = new List<int>();
    for (int index = 0; index < this.uniqueEquipSet.Length; ++index)
    {
      postData.selects.Add(this.uniqueEquipSet[index].order);
      postData.nos.Add(index);
    }
    Protocol.Send<UniqueEquipSetOrderModel.RequestSendForm, UniqueEquipSetOrderModel>(UniqueEquipSetOrderModel.URL, postData, (Action<UniqueEquipSetOrderModel>) (ret =>
    {
      if (ret.Error == Error.None)
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
      call_back(ret.Error == Error.None);
    }));
  }

  public uint GetUniqueEquippingItemTableID(int equip_slot, int set_no = -1)
  {
    EquipItemInfo equippingItemInfo = this.GetUniqueEquippingItemInfo(equip_slot, set_no);
    return equippingItemInfo == null ? 0U : equippingItemInfo.tableID;
  }

  public int GetUniqueEquippingShowHelm(int set_no = -1)
  {
    if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
      set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
    return set_no < 0 || set_no >= this.uniqueEquipSet.Length || this.uniqueEquipSet[set_no] == null ? 1 : this.uniqueEquipSet[set_no].showHelm;
  }

  public class LocalVisual
  {
    public bool isVisibleHelm;
    public EquipItemInfo[] visualItem = new EquipItemInfo[4];

    public ulong VisialID(int index)
    {
      return this.visualItem == null || this.visualItem.Length <= index || this.visualItem[index] == null ? 0UL : this.visualItem[index].uniqueID;
    }
  }

  public class SendSetEquipData
  {
    public int set_no;
    public ulong[] item = new ulong[7];
    public int show_helm;
    public AccessoryPlaceInfo accs = new AccessoryPlaceInfo();

    public SendSetEquipData(int _set_no, ulong[] _item, int _show_helm, AccessoryPlaceInfo _acc)
    {
      this.set_no = _set_no;
      this.item = _item;
      this.show_helm = _show_helm;
      this.accs.Copy(_acc);
    }
  }
}
