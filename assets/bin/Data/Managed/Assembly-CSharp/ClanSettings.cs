// Decompiled with JetBrains decompiler
// Type: ClanSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanSettings : GameSection
{
  protected ClanSettings.CreateRequestParam createRequest = new ClanSettings.CreateRequestParam();
  private List<string> lockNames;
  protected string[] labels;
  private Transform lockPopup;
  private Transform labelPopup;
  protected int lockIndex;
  protected int labelIndex;
  private List<int> stampIdListCanUse;
  private GameObject stampListPrefab;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  protected IEnumerator DoInitialize()
  {
    this.SetActive((Enum) ClanSettings.UI.OBJ_CHANGE, MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered());
    this.SetActive((Enum) ClanSettings.UI.OBJ_CREATE, MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsNotRegistered());
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
    {
      bool isWait = true;
      MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail("0", (Action<ClanDetailModel.Param>) (result => isWait = false));
      while (isWait)
        yield return (object) null;
      this.GetCurrentClanSettings();
    }
    this.lockNames = new List<string>();
    this.lockNames.Add(StringTable.Get(STRING_CATEGORY.JOIN_TYPE, 0U));
    this.lockNames.Add(StringTable.Get(STRING_CATEGORY.JOIN_TYPE, 1U));
    this.lockIndex = this.createRequest.isLock ? 1 : 0;
    string[] allInCategory = StringTable.GetAllInCategory(STRING_CATEGORY.CLAN_LABEL);
    if (allInCategory != null && allInCategory.Length > 1)
    {
      this.labels = new string[allInCategory.Length - 1];
      for (int index = 1; index < allInCategory.Length; ++index)
        this.labels[index - 1] = allInCategory[index];
    }
    this.labelIndex = (int) (this.createRequest.label - 1);
    if (this.labelIndex < 0)
      this.labelIndex = 0;
    if (this.labels != null && this.labelIndex >= this.labels.Length)
      this.labelIndex = this.labels.Length - 1;
    if (string.IsNullOrEmpty(this.createRequest.clanName))
      this.createRequest.SetClanName(this.sectionData.GetText("DEFAULT_CLAN_NAME"));
    this.SetInput(this.GetCtrl((Enum) ClanSettings.UI.IPT_NAME), this.createRequest.clanName, 9, "", new EventDelegate.Callback(this.OnChangeClanName));
    if (string.IsNullOrEmpty(this.createRequest.comment))
      this.createRequest.SetComment(this.sectionData.GetText("DEFAULT_CLAN_COMMENT"));
    this.SetInput(this.GetCtrl((Enum) ClanSettings.UI.IPT_COMMENT), this.createRequest.comment, 48 /*0x30*/, "", new EventDelegate.Callback(this.OnChangeComment));
    if (string.IsNullOrEmpty(this.createRequest.clanTag))
      this.createRequest.SetClanTag(this.sectionData.GetText("DEFAULT_CLAN_TAG"));
    this.SetInput(this.GetCtrl((Enum) ClanSettings.UI.IPT_TAG), this.createRequest.clanTag, 4, "", new EventDelegate.Callback(this.OnChangeClanTag));
    this.SetTouchAndRelease((Enum) ClanSettings.UI.BTN_INFO, "TAG_INFO_SHOW", "TAG_INFO_HIDE");
    this.SetActive((Enum) ClanSettings.UI.SPR_TAG, false);
    this.InitializeBase();
  }

  protected void InitializeBase() => base.Initialize();

  private void GetCurrentClanSettings()
  {
    ClanData clanData = MonoBehaviourSingleton<ClanMatchingManager>.I.clanData;
    bool isLock = clanData.jt == 1;
    this.createRequest = new ClanSettings.CreateRequestParam((CLAN_LABEL) clanData.lbl, isLock, clanData.name, clanData.cmt, clanData.tag);
  }

  public override void UpdateUI()
  {
    this.UpdateLock();
    this.UpdateLabel();
  }

  private void UpdateLock()
  {
    this.SetLabelText((Enum) ClanSettings.UI.LBL_TARGET_LOCK, this.lockNames[this.lockIndex]);
  }

  protected void UpdateLabel()
  {
    if (this.labels == null)
      Debug.LogError((object) "[UpdateLabel] labels are null");
    else
      this.SetLabelText((Enum) ClanSettings.UI.LBL_TARGET_LABEL, this.labels[this.labelIndex]);
  }

  protected virtual void OnChangeClanName()
  {
    string name = this.GetInputValue((Enum) ClanSettings.UI.IPT_NAME).Replace(" ", "").Replace("　", "").Replace("\n", "");
    this.createRequest.SetClanName(name);
    this.SetActive((Enum) ClanSettings.UI.LBL_NAME_DEFAULT, string.IsNullOrEmpty(name));
    this.SetActive((Enum) ClanSettings.UI.BTN_NEXT, !string.IsNullOrEmpty(name) && this.CheckValidClanTag());
    this.SetActive((Enum) ClanSettings.UI.BTN_NEXT_OFF, string.IsNullOrEmpty(name) || !this.CheckValidClanTag());
    this.SetActive((Enum) ClanSettings.UI.BTN_CHANGE, !string.IsNullOrEmpty(name) && this.CheckValidClanTag());
    this.SetActive((Enum) ClanSettings.UI.BTN_CHANGE_OFF, string.IsNullOrEmpty(name) || !this.CheckValidClanTag());
  }

  protected virtual void OnChangeComment()
  {
    this.createRequest.SetComment(this.GetInputValue((Enum) ClanSettings.UI.IPT_COMMENT).Replace("\n", ""));
  }

  protected virtual void OnChangeClanTag()
  {
    string upper = this.GetInputValue((Enum) ClanSettings.UI.IPT_TAG).Replace(" ", "").Replace("　", "").Replace("\n", "").ToUpper();
    this.SetLabelText((Enum) ClanSettings.UI.LBL_INPUT_TAG, upper);
    this.createRequest.SetClanTag(upper);
    this.SetActive((Enum) ClanSettings.UI.LBL_TAG_DEFAULT, string.IsNullOrEmpty(upper));
    this.SetActive((Enum) ClanSettings.UI.BTN_NEXT, !string.IsNullOrEmpty(upper) && !string.IsNullOrEmpty(this.createRequest.clanName));
    this.SetActive((Enum) ClanSettings.UI.BTN_NEXT_OFF, string.IsNullOrEmpty(upper) || string.IsNullOrEmpty(this.createRequest.clanName));
    this.SetActive((Enum) ClanSettings.UI.BTN_CHANGE, !string.IsNullOrEmpty(upper) && !string.IsNullOrEmpty(this.createRequest.clanName));
    this.SetActive((Enum) ClanSettings.UI.BTN_CHANGE_OFF, string.IsNullOrEmpty(upper) || string.IsNullOrEmpty(this.createRequest.clanName));
  }

  private void OnQuery_TARGET_LOCK()
  {
    if (Object.op_Equality((Object) this.lockPopup, (Object) null))
      this.lockPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) ClanSettings.UI.POP_TARGET_LOCK), false);
    if (Object.op_Equality((Object) this.lockPopup, (Object) null))
      return;
    bool[] button_enable = new bool[this.lockNames.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = true;
    int lockIndex = this.lockIndex;
    UIScrollablePopupList.CreatePopup(this.lockPopup, this.GetCtrl((Enum) ClanSettings.UI.POP_TARGET_LOCK), 2, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.lockNames.ToArray(), button_enable, lockIndex, (Action<int>) (index =>
    {
      this.lockIndex = index;
      this.createRequest.SetLockSetting(this.lockIndex == 1);
      this.RefreshUI();
    }));
  }

  private void OnQuery_TARGET_LABEL()
  {
    if (Object.op_Equality((Object) this.labelPopup, (Object) null))
      this.labelPopup = this.Realizes("ScrollablePopupList", this.GetCtrl((Enum) ClanSettings.UI.POP_TARGET_LABEL), false);
    if (Object.op_Equality((Object) this.labelPopup, (Object) null))
      return;
    if (this.labels == null)
    {
      Debug.LogError((object) "labels are null");
    }
    else
    {
      bool[] button_enable = new bool[this.labels.Length];
      for (int index = 0; index < button_enable.Length; ++index)
        button_enable[index] = true;
      int labelIndex = this.labelIndex;
      UIScrollablePopupList.CreatePopup(this.labelPopup, this.GetCtrl((Enum) ClanSettings.UI.POP_TARGET_LABEL), 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, this.labels, button_enable, labelIndex, (Action<int>) (index =>
      {
        this.labelIndex = index;
        this.SetParamLabel((CLAN_LABEL) (index + 1));
        this.RefreshUI();
      }));
    }
  }

  protected virtual void SetParamLabel(CLAN_LABEL label) => this.createRequest.SetLabel(label);

  private void OnQuery_TAG_INFO_SHOW() => this.SetActive((Enum) ClanSettings.UI.SPR_TAG, true);

  private void OnQuery_TAG_INFO_HIDE() => this.SetActive((Enum) ClanSettings.UI.SPR_TAG, false);

  protected bool CheckValidClanTag()
  {
    string clanTag = this.createRequest.clanTag;
    return !string.IsNullOrEmpty(clanTag) && !clanTag.Contains(" ") && !clanTag.Contains("　") && !clanTag.Contains("\n") && clanTag.Length <= 4;
  }

  public enum UI
  {
    LBL_NAME_DEFAULT,
    LBL_TAG_DEFAULT,
    POP_TARGET_LOCK,
    LBL_TARGET_LOCK,
    POP_TARGET_LABEL,
    LBL_TARGET_LABEL,
    IPT_NAME,
    IPT_COMMENT,
    OBJ_CREATE,
    BTN_NEXT,
    BTN_NEXT_OFF,
    OBJ_CHANGE,
    BTN_CHANGE,
    BTN_CHANGE_OFF,
    BTN_INFO,
    SPR_TAG,
    IPT_TAG,
    LBL_INPUT_TAG,
  }

  public class CreateRequestParam
  {
    public int stampId { get; private set; }

    public CLAN_LABEL label { get; private set; }

    public bool isLock { get; private set; }

    public string clanName { get; private set; }

    public string comment { get; private set; }

    public string clanTag { get; private set; }

    public CreateRequestParam()
    {
      this.stampId = 1;
      this.isLock = false;
      this.label = CLAN_LABEL.NONE;
      this.clanName = "";
      this.clanTag = "";
    }

    public CreateRequestParam(
      CLAN_LABEL label,
      bool isLock,
      string name,
      string comment,
      string tag)
    {
      this.isLock = isLock;
      this.label = label;
      this.clanName = name;
      this.comment = comment;
      this.clanTag = tag;
    }

    public void SetStampId(int id) => this.stampId = id;

    public void SetClanName(string name) => this.clanName = name;

    public void SetLabel(CLAN_LABEL label) => this.label = label;

    public void SetLockSetting(bool isLock) => this.isLock = isLock;

    public void SetComment(string comment) => this.comment = comment;

    public void SetClanTag(string tag) => this.clanTag = tag;
  }
}
