// Decompiled with JetBrains decompiler
// Type: ClanSearchSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanSearchSettings : GameSection
{
  private ClanSearchModel.RequestSendForm searchRequest = new ClanSearchModel.RequestSendForm();
  private ClanSearchSettings.PopuplistParam popupJoinType;
  private ClanSearchSettings.PopuplistParam popupLabels;
  private ClanSearchSettings.PopuplistParam popupJoinable;

  public override void Initialize()
  {
    MonoBehaviourSingleton<ClanMatchingManager>.I.LoadSearchRequestFromPrefs();
    this.searchRequest = new ClanSearchModel.RequestSendForm();
    MonoBehaviourSingleton<ClanMatchingManager>.I.searchRequest.Copy(ref this.searchRequest);
    this.SetActive((Enum) ClanSearchSettings.UI.LBL_DEFAULT, string.IsNullOrEmpty(this.searchRequest.name));
    this.SetInput((Enum) ClanSearchSettings.UI.IPT_NAME, this.searchRequest.name, 16 /*0x10*/, new EventDelegate.Callback(this.OnChangeName));
    this.popupJoinType = new ClanSearchSettings.PopuplistParam();
    this.popupJoinType.texts.Add("Not Specified");
    this.popupJoinType.texts.Add(StringTable.Get(STRING_CATEGORY.JOIN_TYPE, 0U));
    this.popupJoinType.texts.Add(StringTable.Get(STRING_CATEGORY.JOIN_TYPE, 1U));
    this.popupJoinType.select_index = this.searchRequest.jt + 1;
    this.popupJoinType.parent_ctrl = this.GetCtrl((Enum) ClanSearchSettings.UI.POP_TARGET_JOIN_TYPE);
    this.popupJoinType.callback = (Action<int>) (index =>
    {
      this.popupJoinType.select_index = index;
      this.searchRequest.jt = index - 1;
      this.RefreshUI();
    });
    this.popupLabels = new ClanSearchSettings.PopuplistParam();
    foreach (string str in StringTable.GetAllInCategory(STRING_CATEGORY.CLAN_LABEL))
      this.popupLabels.texts.Add(str);
    if (this.popupLabels.texts.Count > this.searchRequest.lbl)
      this.popupLabels.select_index = this.searchRequest.lbl;
    this.popupLabels.parent_ctrl = this.GetCtrl((Enum) ClanSearchSettings.UI.POP_TARGET_LABEL);
    this.popupLabels.callback = (Action<int>) (index =>
    {
      this.popupLabels.select_index = index;
      this.searchRequest.lbl = index;
      this.RefreshUI();
    });
    this.popupJoinable = new ClanSearchSettings.PopuplistParam();
    this.popupJoinable.texts.Add("Brigades you can join");
    this.popupJoinable.texts.Add("All Brigades");
    this.popupJoinable.select_index = this.searchRequest.isCF;
    this.popupJoinable.parent_ctrl = this.GetCtrl((Enum) ClanSearchSettings.UI.POP_TARGET_JOINABLE);
    this.popupJoinable.callback = (Action<int>) (index =>
    {
      this.popupJoinable.select_index = index;
      this.searchRequest.isCF = index;
      this.RefreshUI();
    });
    GameSection.SetEventData((object) false);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) ClanSearchSettings.UI.LBL_TARGET_JOIN_TYPE, this.popupJoinType.SelectedString);
    this.SetLabelText((Enum) ClanSearchSettings.UI.LBL_TARGET_LABEL, this.popupLabels.SelectedString);
    this.SetLabelText((Enum) ClanSearchSettings.UI.LBL_TARGET_JOINABLE, this.popupJoinable.SelectedString);
  }

  protected void OnChangeName()
  {
    string str = this.GetInputValue((Enum) ClanSearchSettings.UI.IPT_NAME).Replace(" ", "").Replace("　", "");
    this.SetActive((Enum) ClanSearchSettings.UI.LBL_DEFAULT, string.IsNullOrEmpty(str));
    this.searchRequest.name = str;
  }

  private void OnQuery_SEARCH()
  {
    MonoBehaviourSingleton<ClanMatchingManager>.I.SetSearchRequest(this.searchRequest);
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestSearch((Action<bool, Error>) ((is_success, err) => GameSection.ResumeEvent(is_success)), true);
  }

  private void OnQuery_TARGET_JOIN_TYPE() => this.popup(this.popupJoinType);

  private void OnQuery_TARGET_LABEL() => this.popup(this.popupLabels);

  private void OnQuery_TARGET_JOINABLE() => this.popup(this.popupJoinable);

  private void popup(ClanSearchSettings.PopuplistParam param)
  {
    if (Object.op_Equality((Object) param.popup_transform, (Object) null))
      param.popup_transform = this.Realizes("ScrollablePopupList", param.parent_ctrl, false);
    if (Object.op_Equality((Object) param.popup_transform, (Object) null))
      return;
    bool[] button_enable = new bool[param.texts.Count];
    for (int index = 0; index < button_enable.Length; ++index)
      button_enable[index] = true;
    if (param.select_index >= param.texts.Count)
      param.select_index = param.texts.Count - 1;
    if (param.select_index < 0)
      param.select_index = 0;
    ((Component) param.popup_transform).gameObject.SetActive(true);
    UIScrollablePopupList.CreatePopup(param.popup_transform, param.parent_ctrl, 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, param.texts.ToArray(), button_enable, param.select_index, param.callback);
  }

  public enum UI
  {
    IPT_NAME,
    POP_TARGET_JOIN_TYPE,
    LBL_TARGET_JOIN_TYPE,
    POP_TARGET_LABEL,
    LBL_TARGET_LABEL,
    POP_TARGET_JOINABLE,
    LBL_TARGET_JOINABLE,
    OBJ_SEARCH,
    LBL_DEFAULT,
  }

  private class PopuplistParam
  {
    public Transform popup_transform;
    public Transform parent_ctrl;
    public List<string> texts = new List<string>();
    public int select_index;
    public Action<int> callback;

    public string SelectedString
    {
      get
      {
        return this.texts != null && this.texts.Count > this.select_index ? this.texts[this.select_index] : "";
      }
    }
  }
}
