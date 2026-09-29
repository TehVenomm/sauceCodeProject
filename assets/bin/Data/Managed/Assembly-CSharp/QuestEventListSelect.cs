// Decompiled with JetBrains decompiler
// Type: QuestEventListSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class QuestEventListSelect : QuestListSelectBase
{
  private EventLocationData[] eventLocation;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    bool is_recv = false;
    is_recv = true;
    while (!is_recv)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) QuestListSelectBase.UI.BTN_SORT, false);
    if (this.eventLocation == null || this.eventLocation.Length == 0)
    {
      this.SetActive((Enum) QuestListSelectBase.UI.STR_NON_LIST, true);
    }
    else
    {
      this.SetActive((Enum) QuestListSelectBase.UI.STR_NON_LIST, false);
      this.SetGrid((Enum) QuestListSelectBase.UI.GRD_QUEST, "QuestEventListSelectItem", this.eventLocation.Length, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetEvent(t, "SELECT_EVENT", i);
        this.SetTexture(t, (Enum) QuestListSelectBase.UI.TEX_EVENT_BANNER, (Texture) null);
        this.SetLabelText(t, (Enum) QuestListSelectBase.UI.LBL_QUEST_NAME, string.Empty);
        this.SetLabelText(t, (Enum) QuestListSelectBase.UI.LBL_REMAIN_TIME, this.eventLocation[i].eventAppearRemain);
        if (this.eventLocation[i].isPayingLocation)
        {
          this.SetActive(t, (Enum) QuestListSelectBase.UI.SPR_CRYSTAL, true);
          this.SetActive(t, (Enum) QuestListSelectBase.UI.SPR_FREE_PLAY, this.eventLocation[i].isFreePlaying);
          this.SetLabelText(t, (Enum) QuestListSelectBase.UI.LBL_PAYING_REMAIN, this.eventLocation[i].eventFreePayingRemain);
        }
        else
          this.SetActive(t, (Enum) QuestListSelectBase.UI.SPR_CRYSTAL, false);
      }));
      base.UpdateUI();
    }
  }

  public void OnQuery_SELECT_EVENT()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) QUEST_TYPE.EVENT
    });
  }

  public override EventData CheckAutoEvent(string event_name, object event_data)
  {
    if (!(event_name == "SELECT_EVENT"))
      return base.CheckAutoEvent(event_name, event_data);
    int _data = -1;
    EventLocationData eventLocationData = (EventLocationData) null;
    if (eventLocationData != null)
      _data = Array.IndexOf<EventLocationData>(this.eventLocation, eventLocationData);
    if (_data == -1)
    {
      MonoBehaviourSingleton<QuestManager>.I.EndHowToGetAutoEvent();
      event_name = "AUTO_TARGET_NONE";
      _data = 0;
    }
    return new EventData(event_name, (object) _data);
  }
}
