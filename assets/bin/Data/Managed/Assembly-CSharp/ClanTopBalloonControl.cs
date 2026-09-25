// Decompiled with JetBrains decompiler
// Type: ClanTopBalloonControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanTopBalloonControl : UIBehaviour
{
  private const float RequestTimerInterval = 10f;
  private Transform m_clanQuestBalloon;
  private Transform m_clanDailyBalloon;
  private Vector3 m_clanQuestBalloonPos;
  private Vector3 m_clanDailyBalloonPos;
  private float m_requesTimer;
  private List<ClanDelivery> questList = new List<ClanDelivery>();

  private void InitBalloonObj()
  {
    this.m_clanDailyBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateQuestBalloon(UI_Common.BALLOON_TYPE.NEW_NORMAL_R, this.FindCtrl(((Component) this).transform, (Enum) ClanTopBalloonControl.UI.OBJ_BALOON_ROOT));
    this.m_clanQuestBalloon = MonoBehaviourSingleton<UIManager>.I.common.CreateLoungeQuestBalloon(this.FindCtrl(((Component) this).transform, (Enum) ClanTopBalloonControl.UI.OBJ_BALOON_ROOT));
    ((Component) this.m_clanDailyBalloon.parent).gameObject.SetActive(false);
    ((Component) this.m_clanQuestBalloon.parent).gameObject.SetActive(false);
    if (!Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null))
      return;
    Transform transform1 = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/QUESTBOARD_ICON_POS");
    if (Object.op_Inequality((Object) transform1, (Object) null))
      this.m_clanQuestBalloonPos = transform1.position;
    Transform transform2 = MonoBehaviourSingleton<StageManager>.I.stageObject.Find("Icons/DAILY_ICON_POS");
    if (!Object.op_Inequality((Object) transform2, (Object) null))
      return;
    this.m_clanDailyBalloonPos = transform2.position;
  }

  private void Start() => this.InitBalloonObj();

  private void LateUpdate()
  {
    this.m_requesTimer += Time.deltaTime;
    if ((double) this.m_requesTimer >= 10.0)
    {
      this.StartCoroutine(this.UpdateBalloon());
      this.m_requesTimer = 0.0f;
    }
    this.SetBalloonPosition(this.m_clanDailyBalloon, this.m_clanDailyBalloonPos);
    this.SetBalloonPosition(this.m_clanQuestBalloon, this.m_clanQuestBalloonPos);
  }

  private IEnumerator UpdateBalloon()
  {
    bool wait = true;
    Protocol.Try((System.Action) (() => MonoBehaviourSingleton<ClanMatchingManager>.I.SendRoomParty((Action<bool, List<PartyModel.Party>>) ((isSuccess, parties) => wait = false))));
    while (wait)
      yield return (object) null;
    if (MonoBehaviourSingleton<ClanMatchingManager>.I.clanRoomParties != null && MonoBehaviourSingleton<ClanMatchingManager>.I.clanRoomParties.Count != 0 && MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level >= 2)
    {
      if (!((Component) this.m_clanQuestBalloon.parent).gameObject.activeSelf)
      {
        ((Component) this.m_clanQuestBalloon.parent).gameObject.SetActive(true);
        this.ResetTween(this.m_clanQuestBalloon);
        this.PlayTween(this.m_clanQuestBalloon, is_input_block: false);
      }
    }
    else
      ((Component) this.m_clanQuestBalloon.parent).gameObject.SetActive(false);
    wait = true;
    Protocol.Try((System.Action) (() => this.RequestDeliveryQuest((Action<bool>) (is_success => wait = false))));
    while (wait)
      yield return (object) null;
    if (!this.IsDailyComplete())
    {
      if (!((Component) this.m_clanDailyBalloon.parent).gameObject.activeSelf)
      {
        ((Component) this.m_clanDailyBalloon.parent).gameObject.SetActive(true);
        this.ResetTween(this.m_clanDailyBalloon);
        this.PlayTween(this.m_clanDailyBalloon, is_input_block: false);
      }
    }
    else
      ((Component) this.m_clanDailyBalloon.parent).gameObject.SetActive(false);
    MonoBehaviourSingleton<ClanMatchingManager>.I.StartRequestClanData();
  }

  private bool IsDailyComplete()
  {
    if (this.questList == null || this.questList.Count == 0)
      return true;
    foreach (ClanDelivery quest in this.questList)
    {
      if (!quest.isComplete)
        return false;
    }
    return true;
  }

  private void SetBalloonPosition(Transform balloon, Vector3 iconPos)
  {
    if (Object.op_Equality((Object) balloon, (Object) null))
      return;
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(iconPos));
    worldPoint.z = (double) worldPoint.z >= 0.0 ? 0.0f : -100f;
    balloon.position = worldPoint;
  }

  public void RequestDeliveryQuest(Action<bool> call_back)
  {
    Protocol.Send<ClanDeliveryModel>(ClanDeliveryModel.URL, (Action<ClanDeliveryModel>) (ret =>
    {
      this.questList = ret.result.deliveryList;
      call_back(ret.Error == Error.None);
    }));
  }

  private enum UI
  {
    OBJ_BALOON_ROOT,
  }
}
