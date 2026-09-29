// Decompiled with JetBrains decompiler
// Type: QuestRoomUserAbilityData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestRoomUserAbilityData : EquipSetDetailAbilityData
{
  private QuestRoomObserver observer;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.observer = ((Component) this).gameObject.AddComponent<QuestRoomObserver>().Initialize((bool) eventData[1], (bool) eventData[2], (Action<string>) (dispatch_event_name => this.DispatchEvent(dispatch_event_name)), (Action<string>) (change_event_name => GameSection.ChangeEvent(change_event_name)), (System.Action) (() => GameSection.StayEvent()), (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    GameSection.SetEventData(eventData[0]);
    base.Initialize();
  }

  protected void OnQuery_QuestRoomInvalid_UserDetailItem_OK()
  {
    this.observer.SetupBackSectionEvent();
  }
}
