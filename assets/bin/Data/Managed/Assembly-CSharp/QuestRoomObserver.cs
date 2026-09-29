// Decompiled with JetBrains decompiler
// Type: QuestRoomObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestRoomObserver : MonoBehaviour
{
  public bool fromSearchSection;
  public bool isEntryPass;
  private Action<string> dispatchCallBack;
  private Action<string> changeEventCallBack;
  private System.Action stayEventCallBack;
  private Action<bool> resumeEventCallBack;
  private bool queryInvalidRoom;
  private bool checkInviteListDone;
  private bool isSendingInviteList;
  private bool isStayEvent;
  private static bool isObserve;
  private GameSection section;
  private SpanTimer sendInfoSpan = new SpanTimer(5f);

  public static void OffObserve() => QuestRoomObserver.isObserve = false;

  public QuestRoomObserver Initialize(
    bool from_search_section,
    bool is_entry_pass,
    Action<string> _dispatch_callback,
    Action<string> _change_event_callback,
    System.Action _stay_event_callback,
    Action<bool> _resume_event_callback,
    bool? is_update_observe = null)
  {
    this.fromSearchSection = from_search_section;
    this.isEntryPass = is_entry_pass;
    this.dispatchCallBack = _dispatch_callback;
    this.changeEventCallBack = _change_event_callback;
    this.stayEventCallBack = _stay_event_callback;
    this.resumeEventCallBack = _resume_event_callback;
    QuestRoomObserver.isObserve = ((int) is_update_observe ?? (QuestRoomObserver.isObserve ? 1 : 0)) != 0;
    this.section = ((Component) this).gameObject.GetComponent<GameSection>();
    return this;
  }

  public bool IsValidParty() => PartyManager.IsValidInParty();

  public bool IsConnect() => true;

  public bool IsQueryInvalidRoomEvent() => this.queryInvalidRoom;

  private void Update()
  {
    if (!QuestRoomObserver.isObserve)
      return;
    if ((!this.IsValidParty() || !this.IsConnect()) && !this.queryInvalidRoom)
    {
      if (!this.checkInviteListDone)
      {
        if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
        {
          if (this.isSendingInviteList)
            return;
          if (!GameSceneEvent.IsStay())
          {
            GameSceneEvent.Stay();
            this.isStayEvent = true;
          }
          MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty((Action<bool>) (b =>
          {
            if (this.isStayEvent)
              GameSceneEvent.Resume();
            this.checkInviteListDone = true;
          }));
          this.isSendingInviteList = true;
        }
        else
          this.checkInviteListDone = true;
      }
      else
      {
        string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
        if (!Object.op_Inequality((Object) this.section, (Object) null) || !(this.section.sectionData != (GameSceneTables.SectionData) null) || !(this.section.sectionData.sectionName == currentSectionName) || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
          return;
        this.queryInvalidRoom = true;
        if (this.dispatchCallBack == null)
          return;
        this.dispatchCallBack("INVALID_ROOM");
        QuestRoomObserver.OffObserve();
      }
    }
    else
    {
      if (!this.sendInfoSpan.IsReady())
        return;
      Protocol.Try((System.Action) (() => MonoBehaviourSingleton<PartyManager>.I.SendInfo((Action<bool>) (is_success => { }))));
    }
  }

  public void SetupBackSectionEvent()
  {
    if (this.changeEventCallBack == null || this.stayEventCallBack == null || this.resumeEventCallBack == null || !this.fromSearchSection)
      return;
    this.changeEventCallBack(this.isEntryPass ? "BACK_INPUT_PASS" : "BACK_ROOM_SEARCH");
    if (this.isEntryPass)
      return;
    this.stayEventCallBack();
    this.resumeEventCallBack(true);
  }
}
