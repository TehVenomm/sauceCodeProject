// Decompiled with JetBrains decompiler
// Type: StatusAccessory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StatusAccessory : SkillInfoBase
{
  private StatusAccessory.UI[] partButton = new StatusAccessory.UI[10]
  {
    StatusAccessory.UI.BTN_ICON_HEAD,
    StatusAccessory.UI.BTN_ICON_FACE,
    StatusAccessory.UI.BTN_ICON_R_SHOULDER,
    StatusAccessory.UI.BTN_ICON_L_SHOULDER,
    StatusAccessory.UI.BTN_ICON_R_ARM,
    StatusAccessory.UI.BTN_ICON_L_ARM,
    StatusAccessory.UI.BTN_ICON_CHEST,
    StatusAccessory.UI.BTN_ICON_WAIST,
    StatusAccessory.UI.BTN_ICON_R_LEG,
    StatusAccessory.UI.BTN_ICON_L_LEG
  };
  private StatusAccessory.UI[] partParent = new StatusAccessory.UI[10]
  {
    StatusAccessory.UI.OBJ_ICON_HEAD,
    StatusAccessory.UI.OBJ_ICON_FACE,
    StatusAccessory.UI.OBJ_ICON_R_SHOULDER,
    StatusAccessory.UI.OBJ_ICON_L_SHOULDER,
    StatusAccessory.UI.OBJ_ICON_R_ARM,
    StatusAccessory.UI.OBJ_ICON_L_ARM,
    StatusAccessory.UI.OBJ_ICON_CHEST,
    StatusAccessory.UI.OBJ_ICON_WAIST,
    StatusAccessory.UI.OBJ_ICON_R_LEG,
    StatusAccessory.UI.OBJ_ICON_L_LEG
  };
  private int maxNum = 1;
  private SortCompareData[] localInventory;
  private SortSettings sortSettings;
  private StatusAccessory.eDispState dispState;
  private StatusAccessory.ePutMode putMode;
  private UITweenCtrl tweenCtrl;
  private bool isLoading;
  private PlayerLoader playerLoader;
  private int renderLayer = -1;
  private bool isRefresh;
  private Transform selectIconTrans;
  private Transform selectModelTrans;
  private AccessoryInfo selectItem;
  private string selectUUID = "";
  private uint putUID;
  private ACCESSORY_PART putPart = ACCESSORY_PART.NONE;
  private Transform originParent;
  private Vector3 originPos;
  private Vector3 originScale;
  private Quaternion originRot = Quaternion.identity;
  private Dictionary<ulong, Transform> iconDic = new Dictionary<ulong, Transform>();

  protected override void OnClose()
  {
    this.TryFinalize();
    base.OnClose();
  }

  public override void Initialize()
  {
    this.maxNum = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.MAX_ATTACH_ACCESSORY_NUM;
    this.playerLoader = MonoBehaviourSingleton<StatusStageManager>.I.GetPlayerLoader();
    this.renderLayer = MonoBehaviourSingleton<StatusStageManager>.I.GetPlayerLayer();
    this.InitSort();
    this.InitLocalInventory();
    Transform ctrl = this.GetCtrl((Enum) StatusAccessory.UI.ACCESSORY_ROOT);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
      this.tweenCtrl = ((Component) ctrl).GetComponent<UITweenCtrl>();
    base.Initialize();
  }

  private void TryFinalize()
  {
    if (Object.op_Implicit((Object) this.selectModelTrans))
    {
      if (Object.op_Inequality((Object) this.originParent, (Object) null))
      {
        this.selectModelTrans.SetParent(this.originParent);
        this.selectModelTrans.localPosition = this.originPos;
        this.selectModelTrans.localScale = this.originScale;
        this.selectModelTrans.localRotation = this.originRot;
      }
      else
        this.playerLoader.DeleteAccessoryModel(((Object) this.selectModelTrans).name);
    }
    this.selectModelTrans = (Transform) null;
    this.originParent = (Transform) null;
    if (Object.op_Implicit((Object) this.selectIconTrans))
    {
      this.selectIconTrans.SetParent(((Component) this).transform);
      ((Component) this.selectIconTrans).gameObject.SetActive(false);
    }
    this.selectIconTrans = (Transform) null;
    this.selectItem = (AccessoryInfo) null;
    this.selectUUID = "";
    this.putUID = 0U;
    this.putPart = ACCESSORY_PART.NONE;
  }

  protected virtual void Update() => this.ObserveItemList();

  private void InitSort()
  {
    this.sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ACCESSORY, SortSettings.SETTINGS_TYPE.STORAGE_ACCESSORY);
  }

  private void InitLocalInventory()
  {
    this.localInventory = (SortCompareData[]) this.sortSettings.CreateSortAry<AccessoryInfo, AccessorySortData>(MonoBehaviourSingleton<InventoryManager>.I.accessoryInventory.GetAll().ToArray());
  }

  public override void UpdateUI()
  {
    if (this.dispState == StatusAccessory.eDispState.Part)
      return;
    if (this.localInventory == null)
      this.InitLocalInventory();
    this.SetLabelText((Enum) StatusAccessory.UI.LBL_SORT, this.sortSettings.GetSortLabel());
    this.SetToggle((Enum) StatusAccessory.UI.TGL_ICON_ASC, this.sortSettings.orderTypeAsc);
    this.m_generatedIconList.Clear();
    this.ResetIcon();
    this.UpdateNewIconInfo();
    int length = this.localInventory.Length;
    EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
    bool isRemoveBtn = equipSet.acc.ids.Count >= this.maxNum;
    if (isRemoveBtn)
      ++length;
    this.SetActive((Enum) StatusAccessory.UI.LBL_NON_LIST, length <= 0);
    this.SetDynamicList((Enum) StatusAccessory.UI.GRD_INVENTORY, (string) null, length, false, (Func<int, bool>) (i =>
    {
      if (isRemoveBtn && i == 0)
        return true;
      SortCompareData sortCompareData = this.localInventory[isRemoveBtn ? i - 1 : i];
      return sortCompareData != null && sortCompareData.IsPriority(this.sortSettings.orderTypeAsc);
    }), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (isRemoveBtn && i == 0)
      {
        ItemIconDetail.CreateRemoveButton(t, "TRY_ON", -1, 100, name: StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 44U));
      }
      else
      {
        int _eventData = isRemoveBtn ? i - 1 : i;
        if (_eventData >= this.localInventory.Length)
          this.SetActive(t, false);
        else if (!(this.localInventory[_eventData] is AccessorySortData accessorySortData2) || accessorySortData2.GetTableID() == 0U)
        {
          this.SetActive(t, false);
        }
        else
        {
          this.SetActive(t, true);
          bool _isNew = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ACCESSORY, accessorySortData2.GetUniqID());
          bool _isEquipping = false;
          int index = 0;
          for (int count = equipSet.acc.ids.Count; index < count; ++index)
          {
            if (equipSet.acc.ids[index].Equals(accessorySortData2.GetUniqID().ToString()))
            {
              _isEquipping = true;
              break;
            }
          }
          ItemIcon accessoryIcon = ItemIconDetail.CreateAccessoryIcon(accessorySortData2.itemData.tableData, t, "TRY_ON", _eventData, _isNew, _isEquipping);
          accessoryIcon.SetInitData((SortCompareData) accessorySortData2);
          this.SetLongTouch(accessoryIcon.transform, "DETAIL", (object) accessorySortData2);
          if (this.m_generatedIconList.Contains(accessoryIcon))
            return;
          this.m_generatedIconList.Add(accessoryIcon);
        }
      }
    }));
    base.UpdateUI();
    this.SetActive((Enum) StatusAccessory.UI.LIST_ROOT, true);
    this.SetActive((Enum) StatusAccessory.UI.ACCESSORY_ROOT, false);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.InitLocalInventory();
      this.isRefresh = true;
    }
    base.OnNotify(flags);
  }

  private void ChangeDisp(StatusAccessory.eDispState state)
  {
    if (this.dispState == state)
      return;
    this.dispState = state;
    this.SetActive((Enum) StatusAccessory.UI.LIST_ROOT, this.dispState == StatusAccessory.eDispState.List);
    this.SetActive((Enum) StatusAccessory.UI.ACCESSORY_ROOT, this.dispState == StatusAccessory.eDispState.Part);
  }

  private void ChangeMode(StatusAccessory.ePutMode mode)
  {
    this.putMode = mode;
    this.SetActive((Enum) StatusAccessory.UI.LBL_ON, this.putMode == StatusAccessory.ePutMode.On);
    this.SetActive((Enum) StatusAccessory.UI.LBL_OFF, this.putMode == StatusAccessory.ePutMode.Off);
    this.SetActive((Enum) StatusAccessory.UI.LBL_LIMIT, this.putMode == StatusAccessory.ePutMode.Limit);
  }

  private Transform CreateIcon(ulong _uuid)
  {
    Transform icon = (Transform) null;
    if (!this.iconDic.ContainsKey(_uuid))
    {
      int index = 0;
      for (int length = this.localInventory.Length; index < length; ++index)
      {
        AccessorySortData accessorySortData = this.localInventory[index] as AccessorySortData;
        if ((long) accessorySortData.GetUniqID() == (long) _uuid)
        {
          icon = AccessoryIcon.Create(accessorySortData.itemData.tableData.accessoryId, accessorySortData.itemData.tableData.rarity, accessorySortData.itemData.tableData.getType);
          break;
        }
      }
      if (Object.op_Inequality((Object) icon, (Object) null))
        this.iconDic.Add(_uuid, icon);
    }
    else
      icon = this.iconDic[_uuid];
    return icon;
  }

  private void ResetIcon()
  {
    foreach (KeyValuePair<ulong, Transform> keyValuePair in this.iconDic)
    {
      if (!Object.op_Equality((Object) keyValuePair.Value, (Object) null))
      {
        keyValuePair.Value.SetParent(((Component) this).transform);
        ((Component) keyValuePair.Value).gameObject.SetActive(false);
      }
    }
    this.iconDic.Clear();
  }

  private void SetIconParent(int part, Transform iconTrans)
  {
    iconTrans.SetParent(this.GetCtrl((Enum) this.partParent[part]));
    iconTrans.localPosition = Vector3.zero;
    iconTrans.localRotation = Quaternion.identity;
    iconTrans.localScale = Vector3.one;
    ((Component) iconTrans).gameObject.SetActive(true);
  }

  private void RemoveIconParent(int part)
  {
    Transform ctrl = this.GetCtrl((Enum) this.partParent[part]);
    if (ctrl.childCount <= 0)
      return;
    Transform child = ctrl.GetChild(0);
    if (Object.op_Equality((Object) child, (Object) null))
      return;
    ((Component) child).gameObject.SetActive(false);
  }

  private void SetupIcon(int index)
  {
    if (Object.op_Inequality((Object) this.tweenCtrl, (Object) null))
    {
      this.tweenCtrl.Reset();
      this.tweenCtrl.Play();
    }
    if (index == -1)
      this._SetupIconOff();
    else
      this._SetupIconOn(index);
    this.SetActive((Enum) StatusAccessory.UI.BTN_DECIDE, false);
  }

  private void _SetupIconOff()
  {
    for (int index = 0; index <= 9; ++index)
      this.SetActive((Enum) this.partButton[index], false);
    EquipSetInfo currentLocalEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
    int index1 = 0;
    for (int count = currentLocalEquipSet.acc.ids.Count; index1 < count; ++index1)
    {
      int part = currentLocalEquipSet.acc.GetPart(index1);
      if (part >= 0)
      {
        this.SetActive((Enum) this.partButton[part], true);
        this.SetIconParent(part, this.CreateIcon((ulong) currentLocalEquipSet.acc.GetId(index1)));
      }
    }
    this.ChangeMode(StatusAccessory.ePutMode.Off);
  }

  private void _SetupIconOn(int index)
  {
    AccessorySortData accessorySortData = this.localInventory[index] as AccessorySortData;
    this.selectItem = accessorySortData.itemData;
    this.selectUUID = accessorySortData.itemData.uniqueID.ToString();
    this.selectIconTrans = this.CreateIcon(this.selectItem.uniqueID);
    ((Component) this.selectIconTrans).gameObject.SetActive(false);
    bool flag = false;
    EquipSetInfo currentLocalEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
    int index1 = 0;
    for (int count = currentLocalEquipSet.acc.ids.Count; index1 < count; ++index1)
    {
      if (currentLocalEquipSet.acc.ids[index1] == this.selectUUID)
      {
        AccessoryTable.AccessoryInfoData info = this.selectItem.GetInfo((ACCESSORY_PART) currentLocalEquipSet.acc.GetPart(index1));
        if (info != null)
        {
          this.selectModelTrans = this.playerLoader.GetAccessoryModel(ResourceName.GetPlayerAccessory(info.accessoryId));
          this.originParent = this.selectModelTrans.parent;
          this.originPos = this.selectModelTrans.localPosition;
          this.originScale = this.selectModelTrans.localScale;
          this.originRot = this.selectModelTrans.localRotation;
          this.SetIconParent(currentLocalEquipSet.acc.GetPart(index1), this.selectIconTrans);
          ((Component) this.selectIconTrans).gameObject.SetActive(true);
          flag = true;
          break;
        }
      }
    }
    if (!flag && currentLocalEquipSet.acc.ids.Count >= this.maxNum)
    {
      for (int index2 = 0; index2 <= 9; ++index2)
        this.SetActive((Enum) this.partButton[index2], false);
      int index3 = 0;
      for (int count = currentLocalEquipSet.acc.ids.Count; index3 < count; ++index3)
      {
        int part = currentLocalEquipSet.acc.GetPart(index3);
        if (part >= 0)
        {
          this.SetActive((Enum) this.partButton[part], true);
          this.SetIconParent(part, this.CreateIcon((ulong) currentLocalEquipSet.acc.GetId(index3)));
        }
      }
      this.ChangeMode(StatusAccessory.ePutMode.Limit);
    }
    else
    {
      for (int index4 = 0; index4 <= 9; ++index4)
        this.SetActive((Enum) this.partButton[index4], ((ulong) this.selectItem.tableData.attachPlaceBit & (ulong) (1 << index4)) > 0UL);
      this.ChangeMode(StatusAccessory.ePutMode.On);
    }
  }

  private void OnQuery_BACK()
  {
    if (this.dispState == StatusAccessory.eDispState.List)
      this._BackList();
    else
      this._BackPart();
  }

  private void _BackList() => MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();

  private void _BackPart()
  {
    this.TryFinalize();
    this.ChangeDisp(StatusAccessory.eDispState.List);
    if (!this.isRefresh)
      return;
    this.RefreshUI();
    this.isRefresh = false;
  }

  private void OnQuery_DETAIL()
  {
    GameSection.ChangeEvent("ACCESSORY_SELECT", (object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
      GameSection.GetEventData()
    });
  }

  private void OnQuery_TRY_ON()
  {
    this.SetupIcon((int) GameSection.GetEventData());
    this.ChangeDisp(StatusAccessory.eDispState.Part);
  }

  private void OnQuery_ACC_DECIDE()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
    {
      MonoBehaviourSingleton<StatusManager>.I.AccessoryOn(this.selectUUID, this.putPart);
      this.playerLoader.loadInfo.accUIDs.Clear();
      this.playerLoader.loadInfo.accUIDs.Add(this.putUID);
    }
    this.selectModelTrans = (Transform) null;
    this.selectIconTrans = (Transform) null;
    this.selectItem = (AccessoryInfo) null;
    this.selectUUID = "";
    this.putUID = 0U;
    this.putPart = ACCESSORY_PART.NONE;
    this.originParent = (Transform) null;
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
  }

  private void OnQuery_ACC_HEAD()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.HEAD);
    else
      this.PutOff(ACCESSORY_PART.HEAD);
  }

  private void OnQuery_ACC_FACE()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.FACE);
    else
      this.PutOff(ACCESSORY_PART.FACE);
  }

  private void OnQuery_ACC_R_SHOULDER()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.R_SHOULDER);
    else
      this.PutOff(ACCESSORY_PART.R_SHOULDER);
  }

  private void OnQuery_ACC_L_SHOULDER()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.L_SHOULDER);
    else
      this.PutOff(ACCESSORY_PART.L_SHOULDER);
  }

  private void OnQuery_ACC_R_ARM()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.R_ARM);
    else
      this.PutOff(ACCESSORY_PART.R_ARM);
  }

  private void OnQuery_ACC_L_ARM()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.L_ARM);
    else
      this.PutOff(ACCESSORY_PART.L_ARM);
  }

  private void OnQuery_ACC_CHEST()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.CHEST);
    else
      this.PutOff(ACCESSORY_PART.CHEST);
  }

  private void OnQuery_ACC_WAIST()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.WAIST);
    else
      this.PutOff(ACCESSORY_PART.WAIST);
  }

  private void OnQuery_ACC_R_LEG()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.R_LEG);
    else
      this.PutOff(ACCESSORY_PART.R_LEG);
  }

  private void OnQuery_ACC_L_LEG()
  {
    if (this.putMode == StatusAccessory.ePutMode.On)
      this.PutOn(ACCESSORY_PART.L_LEG);
    else
      this.PutOff(ACCESSORY_PART.L_LEG);
  }

  private void PutOn(ACCESSORY_PART part)
  {
    if (this.isLoading || this.selectItem == null)
      return;
    AccessoryTable.AccessoryInfoData info = this.selectItem.GetInfo(part);
    if (info == null)
      return;
    this.putUID = info.id;
    this.putPart = part;
    this.SetIconParent((int) part, this.selectIconTrans);
    if (Object.op_Inequality((Object) this.selectModelTrans, (Object) null))
      this.SetupModel(info);
    else
      this.StartCoroutine(this._Load(info.accessoryId, (Action<Transform>) (t =>
      {
        this.selectModelTrans = t;
        this.playerLoader.AddAccessoryModel(t);
        this.SetupModel(info);
      })));
  }

  private void SetupModel(AccessoryTable.AccessoryInfoData info)
  {
    if (Object.op_Equality((Object) this.selectModelTrans, (Object) null))
      return;
    PlayerLoader.SetLightProbes(this.selectModelTrans, false);
    if (this.renderLayer != -1)
      PlayerLoader.SetLayerWithChildren_SecondaryNoChange(this.selectModelTrans, this.renderLayer);
    this.selectModelTrans.SetParent(this.playerLoader.GetNodeTrans(info.node));
    this.selectModelTrans.localPosition = info.offset;
    this.selectModelTrans.localRotation = info.rotation;
    this.selectModelTrans.localScale = info.scale;
    this.SetActive((Enum) StatusAccessory.UI.BTN_DECIDE, true);
  }

  private IEnumerator _Load(uint id, Action<Transform> callback)
  {
    this.isLoading = true;
    LoadingQueue queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo = queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, ResourceName.GetPlayerAccessory(id));
    if (queue.IsLoading())
      yield return (object) queue.Wait();
    if (lo == null)
    {
      Debug.LogWarning((object) $"StatusAccessory::_Load() cant load [{ResourceName.GetPlayerAccessory(id)}]");
    }
    else
    {
      Transform trans = lo.Realizes();
      if (Object.op_Equality((Object) trans, (Object) null))
      {
        Debug.LogWarning((object) $"StatusAccessory::_Load() cant realizes [{ResourceName.GetPlayerAccessory(id)}]");
      }
      else
      {
        yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(queue, trans));
        this.isLoading = false;
        if (callback != null)
          callback(trans);
      }
    }
  }

  private void PutOff(ACCESSORY_PART _part)
  {
    uint num = 0;
    EquipSetInfo currentLocalEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
    int index1 = 0;
    for (int count = currentLocalEquipSet.acc.ids.Count; index1 < count; ++index1)
    {
      if ((ACCESSORY_PART) currentLocalEquipSet.acc.GetPart(index1) == _part)
      {
        num = currentLocalEquipSet.acc.GetId(index1);
        MonoBehaviourSingleton<StatusManager>.I.AccessoryOff(currentLocalEquipSet.acc.ids[index1], _part);
        break;
      }
    }
    int index2 = 0;
    for (int length = this.localInventory.Length; index2 < length; ++index2)
    {
      AccessorySortData accessorySortData = this.localInventory[index2] as AccessorySortData;
      if ((long) accessorySortData.itemData.uniqueID == (long) num)
      {
        this.playerLoader.DeleteAccessoryModel(ResourceName.GetPlayerAccessory(accessorySortData.GetTableID()));
        break;
      }
    }
    this.RemoveIconParent((int) _part);
    if (this.putMode == StatusAccessory.ePutMode.Off)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
    }
    else
    {
      this.isRefresh = true;
      for (int index3 = 0; index3 <= 9; ++index3)
        this.SetActive((Enum) this.partButton[index3], ((ulong) this.selectItem.tableData.attachPlaceBit & (ulong) (1 << index3)) > 0UL);
      this.ChangeMode(StatusAccessory.ePutMode.On);
    }
  }

  public enum UI
  {
    SCR_INVENTORY,
    GRD_INVENTORY,
    LBL_SORT,
    TGL_ICON_ASC,
    LIST_ROOT,
    ACCESSORY_ROOT,
    LBL_ON,
    LBL_OFF,
    LBL_LIMIT,
    BTN_ICON_HEAD,
    BTN_ICON_FACE,
    BTN_ICON_R_SHOULDER,
    BTN_ICON_L_SHOULDER,
    BTN_ICON_R_ARM,
    BTN_ICON_L_ARM,
    BTN_ICON_CHEST,
    BTN_ICON_WAIST,
    BTN_ICON_R_LEG,
    BTN_ICON_L_LEG,
    OBJ_ICON_HEAD,
    OBJ_ICON_FACE,
    OBJ_ICON_R_SHOULDER,
    OBJ_ICON_L_SHOULDER,
    OBJ_ICON_R_ARM,
    OBJ_ICON_L_ARM,
    OBJ_ICON_CHEST,
    OBJ_ICON_WAIST,
    OBJ_ICON_R_LEG,
    OBJ_ICON_L_LEG,
    BTN_DECIDE,
    LBL_NON_LIST,
  }

  private enum eDispState
  {
    List,
    Part,
  }

  private enum ePutMode
  {
    On,
    Off,
    Limit,
  }
}
