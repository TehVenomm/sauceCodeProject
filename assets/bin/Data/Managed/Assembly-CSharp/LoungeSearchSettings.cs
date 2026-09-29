// Decompiled with JetBrains decompiler
// Type: LoungeSearchSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class LoungeSearchSettings : LoungeConditionSettings
{
  private LoungeSearchSettings.SearchRequestParam searchRequest = new LoungeSearchSettings.SearchRequestParam();

  public override void Initialize()
  {
    this.CopyLoungeSearchRequestParam();
    this.labels = StringTable.GetAllInCategory(STRING_CATEGORY.LOUNGE_LABEL);
    if ((LOUNGE_LABEL) this.labels.Length > this.searchRequest.label)
      this.labelIndex = (int) this.searchRequest.label;
    this.SetActive((Enum) LoungeSearchSettings.UI.LBL_DEFAULT, string.IsNullOrEmpty(this.searchRequest.loungeName));
    this.SetInput((Enum) LoungeSearchSettings.UI.IPT_NAME, this.searchRequest.loungeName, 16 /*0x10*/, new EventDelegate.Callback(((LoungeConditionSettings) this).OnChangeLoungeName));
    GameSection.SetEventData((object) false);
    this.InitializeBase();
  }

  private void CopyLoungeSearchRequestParam()
  {
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SetLoungeSearchRequestFromPrefs();
    LoungeSearchSettings.SearchRequestParam searchRequest = MonoBehaviourSingleton<LoungeMatchingManager>.I.searchRequest;
    this.searchRequest = new LoungeSearchSettings.SearchRequestParam(searchRequest.order, searchRequest.label, searchRequest.loungeName);
  }

  public override void UpdateUI() => this.UpdateLabel();

  protected override void SetParamLabel(LOUNGE_LABEL label) => this.searchRequest.SetLabel(label);

  protected override void OnChangeLoungeName()
  {
    string name = this.GetInputValue((Enum) LoungeSearchSettings.UI.IPT_NAME).Replace(" ", "").Replace("　", "");
    this.SetActive((Enum) LoungeSearchSettings.UI.LBL_DEFAULT, string.IsNullOrEmpty(name));
    this.searchRequest.SetLoungeName(name);
  }

  private void OnQuery_SEARCH()
  {
    this.searchRequest.order = 1;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SetSearchRequest(this.searchRequest);
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendSearch((Action<bool, Error>) ((is_success, err) => GameSection.ResumeEvent(is_success)), true);
  }

  private void OnQuery_MATCHING()
  {
    this.searchRequest.order = 1;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SetSearchRequest(this.searchRequest);
    this.RequestRandomMatching();
  }

  private void OnQuery_LoungeSearchMatchingFailed_YES() => this.RequestRandomMatching();

  private void RequestRandomMatching()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendSearchRandomMatching((Action<bool, Error>) ((is_success, err) =>
    {
      if (!is_success)
        GameSection.ChangeStayEvent("NOT_FOUND_MATCHING_LOUNGE");
      GameSection.ResumeEvent(true);
    }));
  }

  public new enum UI
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
    OBJ_SEARCH,
    LBL_DEFAULT,
  }

  public class SearchRequestParam
  {
    public int order;

    public LOUNGE_LABEL label { get; private set; }

    public string loungeName { get; private set; }

    public SearchRequestParam()
    {
      this.order = 0;
      this.label = LOUNGE_LABEL.NONE;
      this.loungeName = "";
    }

    public SearchRequestParam(int order, LOUNGE_LABEL label, string loungeName)
    {
      this.order = order;
      this.label = label;
      this.loungeName = loungeName;
    }

    public void SetLabel(LOUNGE_LABEL label) => this.label = label;

    public void SetLoungeName(string name) => this.loungeName = name;
  }
}
