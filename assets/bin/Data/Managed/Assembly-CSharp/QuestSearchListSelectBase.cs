// Decompiled with JetBrains decompiler
// Type: QuestSearchListSelectBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public abstract class QuestSearchListSelectBase : GameSection
{
  protected QuestSearchListSelectBase.UI[] ui = new QuestSearchListSelectBase.UI[3]
  {
    QuestSearchListSelectBase.UI.TGL_MEMBER_1,
    QuestSearchListSelectBase.UI.TGL_MEMBER_2,
    QuestSearchListSelectBase.UI.TGL_MEMBER_3
  };
  protected bool recommentUpdate;

  protected abstract void SendSearchRequest(System.Action onFinish, Action<bool> cb);

  protected abstract void ResetSearchRequest();

  protected abstract void SetQuestData(QuestTable.QuestTableData questData, Transform t);

  protected void CloseSearchRoomCondition() => this.recommentUpdate = true;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    this.ResetSearchRequest();
    bool is_recv = false;
    this.SendGetChallengeInfo((System.Action) (() => is_recv = true), (Action<bool>) null);
    while (!is_recv)
      yield return (object) null;
    yield return (object) this.StartCoroutine(this.Reload());
    base.Initialize();
  }

  private IEnumerator Reload(Action<bool> cb = null)
  {
    bool is_recv = false;
    this.SendSearchRequest((System.Action) (() => is_recv = true), cb);
    while (!is_recv)
      yield return (object) null;
    this.SetDirty((Enum) QuestSearchListSelectBase.UI.GRD_QUEST);
    this.RefreshUI();
  }

  protected void SetNpcMessage()
  {
    string messageBySectionData = Singleton<NPCMessageTable>.I.GetNPCMessageBySectionData(this.sectionData);
    this.SetRenderNPCModel((Enum) QuestSearchListSelectBase.UI.TEX_NPCMODEL, 2, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCFOV);
    this.SetLabelText((Enum) QuestSearchListSelectBase.UI.LBL_NPC_MESSAGE, messageBySectionData);
  }

  protected void SetPartyData(PartyModel.Party party, Transform t)
  {
    int member_num = 0;
    party.slotInfos.ForEach((Action<PartyModel.SlotInfo>) (data =>
    {
      if (data == null || data.userInfo == null)
        return;
      if (data.userInfo.userId == party.ownerUserId)
      {
        this.SetLabelText(t, (Enum) QuestSearchListSelectBase.UI.LBL_HOST_NAME, data.userInfo.name);
        this.SetLabelText(t, (Enum) QuestSearchListSelectBase.UI.LBL_HOST_LV, data.userInfo.level.ToString());
      }
      else
        ++member_num;
    }));
    for (int index = 0; index < 3; ++index)
      this.SetToggle(t, (Enum) this.ui[index], index < member_num);
    this.SetLabelText(t, (Enum) QuestSearchListSelectBase.UI.LBL_LV, this.sectionData.GetText("LV"));
  }

  protected void SetStatusIconInfo(PartyModel.Party _partyParam, Transform _targetObject)
  {
    if (Object.op_Equality((Object) _targetObject, (Object) null) || _partyParam == null)
      return;
    QuestUserStatusIconController componentInChildren = ((Component) _targetObject).GetComponentInChildren<QuestUserStatusIconController>();
    if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
      return;
    componentInChildren.Initialize(new QuestUserStatusIconController.InitParam()
    {
      StatusBit = (uint) _partyParam.iconBit
    });
  }

  public virtual void OnQuery_SELECT_ROOM()
  {
    int eventData = (int) GameSection.GetEventData();
    if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint) MonoBehaviourSingleton<PartyManager>.I.partys[eventData].quest.questId))
    {
      GameSection.StopEvent();
    }
    else
    {
      GameSection.SetEventData((object) new object[1]
      {
        (object) false
      });
      GameSection.StayEvent();
      MonoBehaviourSingleton<PartyManager>.I.SendEntry(MonoBehaviourSingleton<PartyManager>.I.partys[eventData].id, false, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    }
  }

  public virtual void OnQuery_RELOAD()
  {
    GameSection.StayEvent();
    this.StartCoroutine(this.Reload((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }

  private void Update()
  {
    if (!this.recommentUpdate)
      return;
    this.recommentUpdate = false;
    this.RefreshUI();
  }

  public void OnCloseDialog_QuestAcceptRoomInvalid()
  {
    this.StartCoroutine(this.Reload((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }

  protected void SendGetChallengeInfo(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<PartyManager>.I.SendGetChallengeInfo((Action<bool, Error>) ((is_success, err) =>
    {
      if (onFinish != null)
        onFinish();
      if (cb == null)
        return;
      cb(is_success);
    }));
  }

  protected void SetMemberIcon(Transform t, QuestTable.QuestTableData table)
  {
    if (table == null)
      return;
    this.SetActive(t, (Enum) QuestSearchListSelectBase.UI.TGL_MEMBER_3, true);
    this.SetActive(t, (Enum) QuestSearchListSelectBase.UI.TGL_MEMBER_2, true);
    this.SetActive(t, (Enum) QuestSearchListSelectBase.UI.TGL_MEMBER_1, true);
    if (table.userNumLimit < 4)
      this.SetActive(t, (Enum) QuestSearchListSelectBase.UI.TGL_MEMBER_3, false);
    if (table.userNumLimit < 3)
      this.SetActive(t, (Enum) QuestSearchListSelectBase.UI.TGL_MEMBER_2, false);
    if (table.userNumLimit >= 2)
      return;
    this.SetActive(t, (Enum) QuestSearchListSelectBase.UI.TGL_MEMBER_1, false);
  }

  protected enum UI
  {
    GRD_QUEST,
    LBL_HOST_NAME,
    LBL_HOST_LV,
    TGL_MEMBER_1,
    TGL_MEMBER_2,
    TGL_MEMBER_3,
    LBL_LV,
    TEX_NPCMODEL,
    LBL_NPC_MESSAGE,
  }
}
