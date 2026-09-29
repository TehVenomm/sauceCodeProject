// Decompiled with JetBrains decompiler
// Type: ClanRequestSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanRequestSelect : GameSection
{
  private List<ClanDelivery> questList = new List<ClanDelivery>();

  public override void Initialize()
  {
    this.RequestDeliveryQuest((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedClanRequestToQuestBoard)
    {
      DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
      Transform ctrl = this.GetCtrl((Enum) ClanRequestSelect.UI.QuestExploreRequestItemToSearch);
      if (Object.op_Inequality((Object) ctrl, (Object) null))
      {
        UIWidget component = ((Component) ctrl).GetComponent<UIWidget>();
        component.leftAnchor.absolute = specialDeviceInfo.ClanRequestToQuestBoardAnchor.left;
        component.rightAnchor.absolute = specialDeviceInfo.ClanRequestToQuestBoardAnchor.right;
        component.bottomAnchor.absolute = specialDeviceInfo.ClanRequestToQuestBoardAnchor.bottom;
        component.topAnchor.absolute = specialDeviceInfo.ClanRequestToQuestBoardAnchor.top;
        component.UpdateAnchors();
      }
    }
    this.StartCoroutine(this.DoInitialize());
  }

  protected virtual IEnumerator DoInitialize()
  {
    base.Initialize();
    yield return (object) null;
  }

  public override void UpdateUI()
  {
    this.SetQuestBoardAccess();
    this.SetDailyQuestList();
  }

  private void SetDailyQuestList()
  {
    if (this.questList == null)
      return;
    if (this.questList == null || this.questList.Count == 0)
    {
      this.SetActive((Enum) ClanRequestSelect.UI.GRD_QUEST, false);
      this.SetActive((Enum) ClanRequestSelect.UI.STR_QUEST_NON_LIST, true);
    }
    else
    {
      this.SetActive((Enum) ClanRequestSelect.UI.STR_QUEST_NON_LIST, false);
      this.SetActive((Enum) ClanRequestSelect.UI.GRD_DELIVERY, true);
      this.SetDynamicList((Enum) ClanRequestSelect.UI.GRD_DELIVERY, "ClanRequestItem", this.questList.Count, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetActive(t, true);
        if (!Object.op_Implicit((Object) ((Component) ((Component) t).transform).GetComponent<ClanRequestItem>()))
          ((Component) t).gameObject.AddComponent<ClanRequestItem>();
        ((Component) ((Component) t).transform).GetComponent<ClanRequestItem>().Setup(t, this.questList[i]);
      }));
    }
  }

  public void RequestDeliveryQuest(Action<bool> call_back)
  {
    Protocol.Send<ClanDeliveryModel>(ClanDeliveryModel.URL, (Action<ClanDeliveryModel>) (ret =>
    {
      this.questList = ret.result.deliveryList;
      call_back(ret.Error == Error.None);
    }));
  }

  private void SetQuestBoardAccess()
  {
    if (MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level >= 2)
    {
      this.SetActive((Enum) ClanRequestSelect.UI.SPR_QUESTBOARD_BANNER_ON, true);
      this.SetActive((Enum) ClanRequestSelect.UI.SPR_QUESTBOARD_BANNER_OFF, false);
    }
    else
    {
      this.SetActive((Enum) ClanRequestSelect.UI.SPR_QUESTBOARD_BANNER_ON, false);
      this.SetActive((Enum) ClanRequestSelect.UI.SPR_QUESTBOARD_BANNER_OFF, true);
    }
  }

  private void OnQuery_CLAN_QUEST()
  {
    if (MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level >= 2)
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
      {
        new EventData("CLAN_QUEST_COUNTER", (object) null)
      });
    else
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
      {
        new EventData("CLAN_QUEST_COUNTER_OFF", (object) null)
      });
  }

  protected enum UI
  {
    TEX_NPCMODEL,
    LBL_DELIVERY_NON_LIST,
    STR_QUEST_NON_LIST,
    GRD_DELIVERY,
    GRD_QUEST,
    LBL_QUEST_NAME,
    LBL_QUEST_TIME,
    SPR_QUESTBOARD_BANNER_ON,
    SPR_QUESTBOARD_BANNER_OFF,
    QuestExploreRequestItemToSearch,
  }
}
