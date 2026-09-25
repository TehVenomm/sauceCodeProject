// Decompiled with JetBrains decompiler
// Type: QuestRoomSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class QuestRoomSettings : GameSection
{
  private object[] eventData;
  private QUEST_TYPE questType;
  protected PartyManager.PartySetting setting;

  public override void Initialize()
  {
    this.eventData = GameSection.GetEventData() as object[];
    this.questType = (QUEST_TYPE) this.eventData[0];
    if (MonoBehaviourSingleton<PartyManager>.I.partySetting != null)
    {
      PartyManager.PartySetting partySetting = MonoBehaviourSingleton<PartyManager>.I.partySetting;
      this.setting = new PartyManager.PartySetting(partySetting.isLock, partySetting.level, partySetting.total);
    }
    else
      this.setting = new PartyManager.PartySetting(true, 0, 0);
    MonoBehaviourSingleton<PartyManager>.I.SetPartySetting((PartyManager.PartySetting) null);
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
    {
      this.SetActive((Enum) QuestRoomSettings.UI.OBJ_LOCK_LOUNGE, true);
      this.SetActive((Enum) QuestRoomSettings.UI.OBJ_LOCK, false);
    }
    else
    {
      this.SetActive((Enum) QuestRoomSettings.UI.OBJ_LOCK_LOUNGE, false);
      this.SetActive((Enum) QuestRoomSettings.UI.OBJ_LOCK, true);
    }
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.UpdateRoomSettingsText();
    this.UpdateSelectFrame();
  }

  private void UpdateRoomSettingsText()
  {
    if (this.setting.level > 0)
    {
      this.SetActive((Enum) QuestRoomSettings.UI.STR_NON_CONDITION_TOTAL, false);
      this.SetActive((Enum) QuestRoomSettings.UI.LBL_LEVEL, true);
      this.SetLabelText((Enum) QuestRoomSettings.UI.LBL_LEVEL, this.setting.level.ToString());
    }
    else
    {
      this.SetActive((Enum) QuestRoomSettings.UI.LBL_LEVEL, false);
      this.SetActive((Enum) QuestRoomSettings.UI.STR_NON_CONDITION_TOTAL, true);
    }
  }

  private void UpdateSelectFrame()
  {
    QuestRoomSettingsOption component = this.GetComponent<QuestRoomSettingsOption>((Enum) QuestRoomSettings.UI.OBJ_OPTION);
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.SetShowOption(true);
  }

  private void OnQuery_ROOM() => this.ToRoom();

  private void ToRoom()
  {
    GameSection.SetEventData((object) this.eventData);
    if (this.setting.isLock)
    {
      this.setting.level = 0;
      this.setting.total = 0;
    }
    GameSection.StayEvent();
    if (!MonoBehaviourSingleton<PartyManager>.I.IsInParty())
    {
      int currentQuestId = (int) MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
      this.setting.ex = ((IEnumerable<int>) MonoBehaviourSingleton<QuestManager>.I.GetExploreEventIds()).Contains<int>(questData.eventId) ? 1 : 0;
      MonoBehaviourSingleton<PartyManager>.I.SendCreate(currentQuestId, this.setting, (Action<bool>) (is_success =>
      {
        if (is_success)
          MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(this.setting);
        GameSection.ResumeEvent(is_success);
      }));
    }
    else
      MonoBehaviourSingleton<PartyManager>.I.SendEdit(this.setting, (Action<bool>) (is_success =>
      {
        if (is_success)
          MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(this.setting);
        GameSection.ResumeEvent(is_success);
      }));
  }

  private void OnQuery_CoopServerInvalidConfirm_YES()
  {
    GameSection.StayEvent();
    CoopApp.EnterQuestOffline((Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) => GameSection.ResumeEvent(is_s)));
  }

  private void OnQuery_CLOSE()
  {
    if (MonoBehaviourSingleton<PartyManager>.I.partySetting != null)
      GameSection.ChangeEvent("ROOM");
    else if (this.questType == QUEST_TYPE.ORDER)
      GameSection.ChangeEvent("TO_ORDER");
    else
      GameSection.ChangeEvent("TO_SELECT");
  }

  private void OnQuery_LOCK()
  {
    this.setting.isLock = true;
    this.UpdateSelectFrame();
    this.ToRoom();
  }

  private void OnQuery_LOCK_LOUNGE()
  {
    this.setting.isLock = true;
    this.UpdateSelectFrame();
    this.ToRoom();
  }

  private void OnQuery_UNLOCK()
  {
    this.setting.isLock = false;
    this.UpdateSelectFrame();
    this.ToRoom();
  }

  private void OnQuery_LEVEL()
  {
    MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(this.setting);
    GameSection.SetEventData((object) this.eventData);
  }

  private enum UI
  {
    LBL_LEVEL,
    STR_NON_CONDITION_TOTAL,
    OBJ_OPTION,
    OBJ_LOCK,
    OBJ_LOCK_LOUNGE,
  }
}
