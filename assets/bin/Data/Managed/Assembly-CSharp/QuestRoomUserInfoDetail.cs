// Decompiled with JetBrains decompiler
// Type: QuestRoomUserInfoDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestRoomUserInfoDetail : QuestFriendDetailBase
{
  protected QuestRoomObserver observer;

  protected virtual bool IsRoomObserve() => true;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.observer = ((Component) this).gameObject.AddComponent<QuestRoomObserver>().Initialize((bool) eventData[1], (bool) eventData[2], (Action<string>) (dispatch_event_name => this.DispatchEvent(dispatch_event_name)), (Action<string>) (change_event_name => GameSection.ChangeEvent(change_event_name)), (System.Action) (() => GameSection.StayEvent()), (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)), new bool?(this.IsRoomObserve()));
    GameSection.SetEventData(eventData[0]);
    base.Initialize();
  }

  protected void OnQuery_QuestRoomInvalid_EquipChange_OK() => this.observer.SetupBackSectionEvent();

  protected override void OnQuery_SKILL_LIST()
  {
    this.OnQuerySkillListBase();
    GameSection.SetEventData((object) new object[3]
    {
      (object) (GameSection.GetEventData() as object[]),
      (object) this.observer.fromSearchSection,
      (object) this.observer.isEntryPass
    });
  }

  protected void OnQuerySkillListBase() => base.OnQuery_SKILL_LIST();

  protected override void OnQuery_ABILITY()
  {
    this.OnQueryAbilityBase();
    GameSection.SetEventData((object) new object[3]
    {
      (object) (GameSection.GetEventData() as object[]),
      (object) this.observer.fromSearchSection,
      (object) this.observer.isEntryPass
    });
  }

  protected void OnQueryAbilityBase() => base.OnQuery_ABILITY();

  protected override void OnQuery_STATUS()
  {
    this.OnQueryStatusBase();
    GameSection.SetEventData((object) new object[3]
    {
      (object) (GameSection.GetEventData() as object[]),
      (object) this.observer.fromSearchSection,
      (object) this.observer.isEntryPass
    });
  }

  protected void OnQueryStatusBase() => base.OnQuery_STATUS();

  protected override void OnQuery_DETAIL()
  {
    this.OnQueryDetailBase();
    if (this.isVisualMode)
      return;
    GameSection.SetEventData((object) new object[3]
    {
      (object) (GameSection.GetEventData() as object[]),
      (object) this.observer.fromSearchSection,
      (object) this.observer.isEntryPass
    });
  }

  protected void OnQueryDetailBase() => base.OnQuery_DETAIL();
}
