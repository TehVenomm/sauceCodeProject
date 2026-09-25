// Decompiled with JetBrains decompiler
// Type: LoungeConditionSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class LoungeConditionSettings : GameSection
{
  private LoungeConditionSettings.CreateRequestParam createRequest = new LoungeConditionSettings.CreateRequestParam();
  private List<string> levelNames;
  private List<int> levelList;
  private List<string> capacityNames;
  private List<int> capacityList;
  protected string[] labels;
  private List<string> lockNames;
  private Transform minLevelPopup;
  private Transform maxLevelPopup;
  private Transform capacityPopup;
  private Transform labelPopup;
  private Transform lockPopup;
  protected int minLevelIndex;
  protected int maxLevelIndex;
  protected int capacityIndex;
  protected int labelIndex;
  protected int lockIndex;
  private List<int> stampIdListCanUse;
  private GameObject stampListPrefab;

  public override void Initialize()
  {
    this.SetActive((Enum) LoungeConditionSettings.UI.OBJ_CHANGE, MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge());
    this.SetActive((Enum) LoungeConditionSettings.UI.OBJ_CREATE, !MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge());
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
      this.GetCurrentLoungeSettings();
    else
      this.CopyLoungeCreateRequestParam();
    this.levelNames = new List<string>();
    this.levelList = new List<int>();
    this.CreateLevelPopText();
    for (int index = 0; index < this.levelList.Count; ++index)
    {
      if (this.levelList[index] == this.createRequest.minLevel)
        this.minLevelIndex = index;
      if (this.levelList[index] == this.createRequest.maxLevel)
        this.maxLevelIndex = index;
    }
    this.capacityNames = new List<string>();
    this.capacityList = new List<int>();
    this.CreateCapacityPopText();
    for (int index = 0; index < this.capacityList.Count; ++index)
    {
      if (this.capacityList[index] == this.createRequest.capacity)
      {
        this.capacityIndex = index;
        break;
      }
    }
    this.labels = StringTable.GetAllInCategory(STRING_CATEGORY.LOUNGE_LABEL);
    if ((LOUNGE_LABEL) this.labels.Length > this.createRequest.label)
      this.labelIndex = (int) this.createRequest.label;
    this.lockNames = new List<string>();
    this.lockNames.Add(this.sectionData.GetText("PUBLIC"));
    this.lockNames.Add(this.sectionData.GetText("LOCK"));
    this.lockIndex = this.createRequest.isLock ? 1 : 0;
    if (string.IsNullOrEmpty(this.createRequest.loungeName))
      this.createRequest.SetLoungeName(this.sectionData.GetText("DEFAULT_LOUNGE_NAME"));
    this.SetInput((Enum) LoungeConditionSettings.UI.IPT_NAME, this.createRequest.loungeName, 16 /*0x10*/, new EventDelegate.Callback(this.OnChangeLoungeName));
    this.StartCoroutine(this.LoadStampList());
  }

  protected void InitializeBase() => base.Initialize();

  private IEnumerator LoadStampList()
  {
    this.SetActive((Enum) LoungeConditionSettings.UI.SPR_STAMP_LIST, false);
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.stampListPrefab = lo_chat_stamp_listitem.loadedObject as GameObject;
    this.InitStamp();
    ((Behaviour) ((Component) this.GetCtrl((Enum) LoungeConditionSettings.UI.SCR_STAMP_LIST)).GetComponent<UIScrollView>()).enabled = true;
    this.InitializeBase();
  }

  private void InitStamp()
  {
    if (this.stampIdListCanUse == null)
      this.ResetStampIdList();
    this.SetGrid((Enum) LoungeConditionSettings.UI.GRD_STAMP_LIST, (string) null, this.stampIdListCanUse.Count, true, new Func<int, Transform, Transform>(this.CreateStampItem), new Action<int, Transform, bool>(this.InitStampItem));
  }

  private Transform CreateStampItem(int index, Transform parent)
  {
    Transform stampItem = ResourceUtility.Realizes((Object) this.stampListPrefab, 5);
    stampItem.parent = parent;
    stampItem.localScale = Vector3.one;
    return stampItem;
  }

  private void InitStampItem(int index, Transform iTransform, bool isRecycle)
  {
    if (this.stampIdListCanUse == null)
      return;
    int _stampId = this.stampIdListCanUse[index];
    ChatStampListItem item = ((Component) iTransform).GetComponent<ChatStampListItem>();
    item.Init(_stampId);
    if (isRecycle)
      return;
    if (_stampId == this.createRequest.stampId)
      this.SetStampTextre(item.StampId);
    item.onButton += (System.Action) (() => this.SelectStamp(item.StampId));
  }

  private void SetStampTextre(int id)
  {
    ((Component) this.GetCtrl((Enum) LoungeConditionSettings.UI.OBJ_STAMP)).GetComponent<ChatStampListItem>().Init(id);
  }

  private void SelectStamp(int id)
  {
    this.SetActive((Enum) LoungeConditionSettings.UI.SPR_STAMP_LIST, false);
    this.SetStampTextre(id);
    this.createRequest.SetStampId(id);
  }

  private void ResetStampIdList()
  {
    if (!Singleton<StampTable>.IsValid() || Singleton<StampTable>.I.table == null)
      return;
    if (this.stampIdListCanUse == null)
      this.stampIdListCanUse = new List<int>();
    this.stampIdListCanUse.Clear();
    Singleton<StampTable>.I.table.ForEach((Action<StampTable.Data>) (stamp_data =>
    {
      int id = (int) stamp_data.id;
      if (!MonoBehaviourSingleton<UIManager>.I.mainChat.CanIPostTheStamp(id))
        return;
      this.stampIdListCanUse.Add(id);
    }));
  }

  private void GetCurrentLoungeSettings()
  {
    LoungeModel.Lounge loungeData = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData;
    bool isLock = loungeData.isLock == 1;
    int capacity = loungeData.num + 1;
    this.createRequest = new LoungeConditionSettings.CreateRequestParam(loungeData.stampId, loungeData.minLv, loungeData.maxLv, capacity, (LOUNGE_LABEL) loungeData.label, isLock, loungeData.name);
  }

  private void CopyLoungeCreateRequestParam()
  {
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SetLoungeCreateRequestFromPrefs();
    LoungeConditionSettings.CreateRequestParam createRequest = MonoBehaviourSingleton<LoungeMatchingManager>.I.createRequest;
    this.createRequest = new LoungeConditionSettings.CreateRequestParam(createRequest.stampId, createRequest.minLevel, createRequest.maxLevel, createRequest.capacity, createRequest.label, createRequest.isLock, createRequest.loungeName);
  }

  private void CreateLevelPopText()
  {
    int maxLevel = Singleton<UserLevelTable>.I.GetMaxLevel();
    this.levelList = new List<int>();
    int num = maxLevel / 10 + 1;
    for (int index = 0; index < num; ++index)
    {
      if (index == 0)
        this.levelList.Add(15);
      else if (10 * index > 15)
        this.levelList.Add(10 * index);
    }
    for (int index = 0; index < this.levelList.Count; ++index)
      this.levelNames.Add(this.levelList[index].ToString());
  }

  private void CreateCapacityPopText()
  {
    int num = 8;
    for (int index = 2; index <= num; ++index)
      this.capacityList.Add(index);
    for (int index = 0; index < this.capacityList.Count; ++index)
      this.capacityNames.Add(this.capacityList[index].ToString());
  }

  public override void UpdateUI()
  {
    this.UpdateMinLevel();
    this.UpdateMaxLevel();
    this.UpdateCapacity();
    this.UpdateLabel();
    this.UpdateLock();
  }

  private void UpdateMinLevel()
  {
    this.SetLabelText((Enum) LoungeConditionSettings.UI.LBL_TARGET_MIN_LEVEL, this.levelNames[this.minLevelIndex]);
  }

  private void UpdateMaxLevel()
  {
    this.SetLabelText((Enum) LoungeConditionSettings.UI.LBL_TARGET_MAX_LEVEL, this.levelNames[this.maxLevelIndex]);
  }

  private void UpdateCapacity()
  {
    this.SetLabelText((Enum) LoungeConditionSettings.UI.LBL_TARGET_CAPACITY, this.capacityNames[this.capacityIndex]);
  }

  protected void UpdateLabel()
  {
    this.SetLabelText((Enum) LoungeConditionSettings.UI.LBL_TARGET_LABEL, this.labels[this.labelIndex]);
  }

  private void UpdateLock()
  {
    this.SetLabelText((Enum) LoungeConditionSettings.UI.LBL_TARGET_LOCK, this.lockNames[this.lockIndex]);
  }

  protected virtual void OnChangeLoungeName()
  {
    this.createRequest.SetLoungeName(this.GetInputValue((Enum) LoungeConditionSettings.UI.IPT_NAME).Replace(" ", "").Replace("　", ""));
  }

  private void OnQuery_STAMP()
  {
    this.SetActive((Enum) LoungeConditionSettings.UI.SPR_STAMP_LIST, true);
  }

  private void OnQuery_TARGET_MIN_LEVEL() => this.ShowMinLevelPopup();

  private void ShowMinLevelPopup()
  {
    if (Object.op_Equality((Object) this.minLevelPopup, (Object) null))
      this.minLevelPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_MIN_LEVEL), false);
    if (Object.op_Equality((Object) this.minLevelPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.levelNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = index <= this.maxLevelIndex;
    int minLevelIndex = this.minLevelIndex;
    UIScrollablePopupList.CreatePopup(this.minLevelPopup, this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_MIN_LEVEL), 7, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.levelNames.ToArray(), button_enable, minLevelIndex, (Action<int>) (index =>
    {
      this.minLevelIndex = index;
      this.createRequest.SetMinLevel(this.levelList[index]);
      this.RefreshUI();
    }));
  }

  private void OnQuery_TARGET_MAX_LEVEL() => this.ShowMaxLevelPopup();

  private void ShowMaxLevelPopup()
  {
    if (Object.op_Equality((Object) this.maxLevelPopup, (Object) null))
      this.maxLevelPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_MAX_LEVEL), false);
    if (Object.op_Equality((Object) this.maxLevelPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.levelNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = index >= this.minLevelIndex;
    int maxLevelIndex = this.maxLevelIndex;
    UIScrollablePopupList.CreatePopup(this.maxLevelPopup, this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_MAX_LEVEL), 8, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.levelNames.ToArray(), button_enable, maxLevelIndex, (Action<int>) (index =>
    {
      this.maxLevelIndex = index;
      this.createRequest.SetMaxLevel(this.levelList[index]);
      this.RefreshUI();
    }));
  }

  private void OnQuery_TARGET_CAPACITY()
  {
    if (Object.op_Equality((Object) this.capacityPopup, (Object) null))
      this.capacityPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_CAPACITY), false);
    if (Object.op_Equality((Object) this.capacityPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.capacityNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = !MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge() || index >= this.createRequest.capacity - 2;
    int capacityIndex = this.capacityIndex;
    UIScrollablePopupList.CreatePopup(this.capacityPopup, this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_CAPACITY), 6, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.capacityNames.ToArray(), button_enable, capacityIndex, (Action<int>) (index =>
    {
      this.capacityIndex = index;
      this.createRequest.SetCapacity(this.capacityList[index]);
      this.RefreshUI();
    }));
  }

  private void OnQuery_TARGET_LABEL()
  {
    if (Object.op_Equality((Object) this.labelPopup, (Object) null))
      this.labelPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_LABEL), false);
    if (Object.op_Equality((Object) this.labelPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.labels.Length];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = true;
    int labelIndex = this.labelIndex;
    UIScrollablePopupList.CreatePopup(this.labelPopup, this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_LABEL), 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.labels, button_enable, labelIndex, (Action<int>) (index =>
    {
      this.labelIndex = index;
      this.SetParamLabel((LOUNGE_LABEL) index);
      this.RefreshUI();
    }));
  }

  protected virtual void SetParamLabel(LOUNGE_LABEL label) => this.createRequest.SetLabel(label);

  private void OnQuery_TARGET_LOCK()
  {
    if (Object.op_Equality((Object) this.lockPopup, (Object) null))
      this.lockPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_LOCK), false);
    if (Object.op_Equality((Object) this.lockPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.lockNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = true;
    int lockIndex = this.lockIndex;
    UIScrollablePopupList.CreatePopup(this.lockPopup, this.GetCtrl((Enum) LoungeConditionSettings.UI.POP_TARGET_LOCK), 2, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.lockNames.ToArray(), button_enable, lockIndex, (Action<int>) (index =>
    {
      this.lockIndex = index;
      this.createRequest.SetLockSetting(this.lockIndex == 1);
      this.RefreshUI();
    }));
  }

  private void OnQuery_CREATE()
  {
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SetLoungeCreateRequest(this.createRequest);
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendCreate((Action<bool, Error>) ((is_success, err) =>
    {
      if (!is_success && err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
        GameSection.ChangeStayEvent("NOT_FOUND_QUEST");
      GameSection.ResumeEvent(true);
    }));
  }

  private void OnQuery_CHANGE()
  {
    LoungeModel.RequestEdit lounge_setting = new LoungeModel.RequestEdit();
    lounge_setting.stampId = this.createRequest.stampId;
    lounge_setting.num = this.createRequest.capacity;
    lounge_setting.label = (int) this.createRequest.label;
    lounge_setting.isLock = this.createRequest.isLock ? 1 : 0;
    lounge_setting.minLv = this.createRequest.minLevel;
    lounge_setting.maxLv = this.createRequest.maxLevel;
    lounge_setting.name = this.createRequest.loungeName;
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEdit(lounge_setting, (Action<bool>) (is_success => GameSection.ResumeEvent(true)));
  }

  public enum UI
  {
    POP_TARGET_MIN_LEVEL,
    POP_TARGET_MAX_LEVEL,
    LBL_TARGET_MIN_LEVEL,
    LBL_TARGET_MAX_LEVEL,
    POP_TARGET_CAPACITY,
    LBL_TARGET_CAPACITY,
    POP_TARGET_LABEL,
    LBL_TARGET_LABEL,
    POP_TARGET_LOCK,
    LBL_TARGET_LOCK,
    IPT_NAME,
    OBJ_CREATE,
    OBJ_CHANGE,
    SPR_STAMP_LIST,
    SCR_STAMP_LIST,
    GRD_STAMP_LIST,
    BTN_STAMP,
    TEX_STAMP,
    OBJ_STAMP,
  }

  public class CreateRequestParam
  {
    public int stampId { get; private set; }

    public int minLevel { get; private set; }

    public int maxLevel { get; private set; }

    public int capacity { get; private set; }

    public LOUNGE_LABEL label { get; private set; }

    public bool isLock { get; private set; }

    public string loungeName { get; private set; }

    public CreateRequestParam()
    {
      this.stampId = 1;
      this.minLevel = 15;
      this.maxLevel = Singleton<UserLevelTable>.I.GetMaxLevel();
      this.capacity = 8;
      this.label = LOUNGE_LABEL.NONE;
      this.isLock = false;
      this.loungeName = "";
    }

    public CreateRequestParam(
      int stampId,
      int minLevel,
      int maxLevel,
      int capacity,
      LOUNGE_LABEL label,
      bool isLock,
      string name)
    {
      this.stampId = stampId;
      this.minLevel = minLevel;
      this.maxLevel = maxLevel;
      this.capacity = capacity;
      this.label = label;
      this.isLock = isLock;
      this.loungeName = name;
    }

    public void SetStampId(int id) => this.stampId = id;

    public void SetMinLevel(int level) => this.minLevel = level;

    public void SetMaxLevel(int level) => this.maxLevel = level;

    public void SetCapacity(int capacity) => this.capacity = capacity;

    public void SetLabel(LOUNGE_LABEL label) => this.label = label;

    public void SetLoungeName(string name) => this.loungeName = name;

    public void SetLockSetting(bool isLock) => this.isLock = isLock;
  }
}
