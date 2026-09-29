// Decompiled with JetBrains decompiler
// Type: FishingRecordList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FishingRecordList : GameSection
{
  private int eventId;
  private GatherItemUserRecordModel.Param records;
  private bool isResetUI;

  public override void Initialize()
  {
    this.eventId = (int) GameSection.GetEventData();
    this.StartCoroutine(this.DoInitialize());
  }

  protected IEnumerator DoInitialize()
  {
    bool isReceived = false;
    MonoBehaviourSingleton<UserInfoManager>.I.SendGatherItemRecord(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, this.eventId, (Action<bool, GatherItemUserRecordModel>) ((b, r) =>
    {
      if (r != null)
        this.records = r.result;
      isReceived = true;
    }));
    while (!isReceived)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (this.records == null)
      return;
    this.SetupSrollBarCollider();
    this.SetActive((Enum) FishingRecordList.UI.STR_NON_LIST, this.records.gatherItems.IsNullOrEmpty<GatherItemRecord>());
    this.SetLabelText((Enum) FishingRecordList.UI.LBL_SUM_VALUE, string.Format("{0:#,0}", (object) this.records.totalNum.ToString()));
    if (!this.records.gatherItems.IsNullOrEmpty<GatherItemRecord>())
      this.SetDynamicList((Enum) FishingRecordList.UI.GRD_LIST, "FishingRecordItem", this.records.gatherItems.Count, this.isResetUI, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetupItem(t, this.records.gatherItems[i]);
        this.SetActive(t, true);
      }));
    this.isResetUI = false;
  }

  private void SetupSrollBarCollider()
  {
    Transform ctrl = this.GetCtrl((Enum) FishingRecordList.UI.OBJ_SCROLL_BAR);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    UIWidget component1 = ((Component) ctrl).GetComponent<UIWidget>();
    if (Object.op_Equality((Object) component1, (Object) null))
      return;
    BoxCollider component2 = ((Component) ctrl).GetComponent<BoxCollider>();
    if (Object.op_Equality((Object) component2, (Object) null))
      return;
    component2.size = Vector2.op_Implicit(new Vector2(component2.size.x, component1.localSize.y));
  }

  private void SetupItem(Transform t, GatherItemRecord info)
  {
    FishingRecordItem fishingRecordItem = ((Component) t).GetComponent<FishingRecordItem>();
    if (Object.op_Equality((Object) fishingRecordItem, (Object) null))
      fishingRecordItem = ((Component) t).gameObject.AddComponent<FishingRecordItem>();
    fishingRecordItem.InitUI();
    fishingRecordItem.Setup(t, info);
  }

  private void OnQuery_SECTION_BACK() => this.Save();

  private void Save()
  {
    Transform ctrl = this.GetCtrl((Enum) FishingRecordList.UI.GRD_LIST);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    FishingRecordItem[] componentsInChildren = ((Component) ctrl).GetComponentsInChildren<FishingRecordItem>(true);
    if (((IList<FishingRecordItem>) componentsInChildren).IsNullOrEmpty<FishingRecordItem>())
      return;
    bool flag = false;
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (componentsInChildren[index].SaveState())
        flag = true;
    }
    if (!flag)
      return;
    PlayerPrefs.Save();
  }

  protected enum UI
  {
    STR_NON_LIST,
    GRD_LIST,
    OBJ_SCROLL_BAR,
    LBL_SUM_VALUE,
  }
}
