// Decompiled with JetBrains decompiler
// Type: RushResultPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class RushResultPoint : GameSection
{
  private const float COUNT_ANIM_SPEED = 4f;
  private bool is_skip;
  private PointEventCurrentData allPointEvents;
  private RushResultPoint.RESULT_ANIM_STATE animState;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
    yield return (object) new WaitForEndOfFrame();
    yield return (object) MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
    yield return (object) new WaitForEndOfFrame();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    foreach (int se_id in (int[]) Enum.GetValues(typeof (RushResultPoint.AUDIO)))
      loadingQueue.CacheSE(se_id);
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    GC.Collect();
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.allPointEvents = new PointEventCurrentData();
    this.allPointEvents.pointRankingData = new PointEventCurrentData.PointResultData();
    for (int index = 0; index < MonoBehaviourSingleton<InGameManager>.I.rushPointEvents.Count; ++index)
    {
      PointEventCurrentData rushPointEvent = MonoBehaviourSingleton<InGameManager>.I.rushPointEvents[index];
      if (index == 0)
        this.allPointEvents.pointRankingData.userPoint = rushPointEvent.pointRankingData.userPoint;
      this.AddPointEventData(rushPointEvent);
    }
    PointEventCurrentData.PointResultData d = this.allPointEvents.pointRankingData;
    this.SetLabelText((Enum) RushResultPoint.UI.LBL_QUEST_NAME, Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).questText);
    this.SetFontStyle((Enum) RushResultPoint.UI.LBL_GET_POINT, (FontStyle) 2);
    this.SetLabelText((Enum) RushResultPoint.UI.LBL_GET_POINT, "0pt");
    this.SetFontStyle((Enum) RushResultPoint.UI.LBL_TOTAL_POINT, (FontStyle) 2);
    this.SetLabelText((Enum) RushResultPoint.UI.LBL_TOTAL_POINT, d.userPoint.ToString("N0") + "pt");
    this.SetGrid((Enum) RushResultPoint.UI.GRD_POINT_DETAIL, "RushResultPointDetailItem", d.bonusPoint.Count, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      UILabel component1 = ((Component) this.FindCtrl(t, (Enum) RushResultPoint.UI.LBL_POINT)).GetComponent<UILabel>();
      component1.alpha = 1f;
      component1.text = d.bonusPoint[i].point.ToString("N0");
      component1.fontStyle = (FontStyle) 2;
      UILabel component2 = ((Component) this.FindCtrl(t, (Enum) RushResultPoint.UI.LBL_POINT_NAME)).GetComponent<UILabel>();
      component2.alpha = 1f;
      component2.text = d.bonusPoint[i].name;
      component2.fontStyle = (FontStyle) 2;
    }));
    if (d.nextReward != null)
    {
      this.SetAllRewardItem(RushResultPoint.UI.GRD_NEXT_ITEM_ROOT, d.nextReward.reward);
      this.SetPoint(RushResultPoint.UI.OBJ_NEXT_REWARD, d.nextReward.point - (d.userPoint + d.getPoint));
    }
    else
    {
      this.SetActive((Enum) RushResultPoint.UI.STR_POINT_NEXT, false);
      this.SetFontStyle(this.GetCtrl((Enum) RushResultPoint.UI.OBJ_NEXT_REWARD), (Enum) RushResultPoint.UI.LBL_POINT, (FontStyle) 2);
      this.SetLabelText(this.GetCtrl((Enum) RushResultPoint.UI.OBJ_NEXT_REWARD), (Enum) RushResultPoint.UI.LBL_POINT, "None");
    }
    List<PointEventCurrentData.Reward> rewardList = new List<PointEventCurrentData.Reward>();
    foreach (PointEventCurrentData.PointRewardData pointRewardData in d.getReward)
      rewardList.AddRange((IEnumerable<PointEventCurrentData.Reward>) pointRewardData.reward);
    this.SetAllRewardItem(RushResultPoint.UI.GRD_ITEM_ROOT, rewardList);
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
    {
      UIVirtualScreen componentInChildren = ((Component) this).GetComponentInChildren<UIVirtualScreen>();
      UIWidget component = ((Component) this.GetCtrl((Enum) RushResultPoint.UI.SHADOW)).GetComponent<UIWidget>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null) && Object.op_Inequality((Object) component, (Object) null))
      {
        component.width = (int) componentInChildren.ScreenWidthFull;
        component.height = (int) componentInChildren.ScreenHeightFull;
      }
    }
    this.StartCoroutine(this.PlayAnimation());
  }

  private void SetAllRewardItem(
    RushResultPoint.UI targetGrid,
    List<PointEventCurrentData.Reward> rewardList)
  {
    this.SetGrid((Enum) targetGrid, "ItemIconReward", rewardList.Count, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      PointEventCurrentData.Reward reward = rewardList[i];
      ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward.type, (uint) reward.itemId, t, reward.num);
      if (Object.op_Inequality((Object) rewardItemIcon, (Object) null))
        rewardItemIcon.SetEnableCollider(false);
      ((Component) t.Find("itemNum")).GetComponent<UILabel>().text = "×" + (object) rewardList[i].num;
      if (targetGrid != RushResultPoint.UI.GRD_NEXT_ITEM_ROOT)
        return;
      t.localScale = new Vector3(0.7f, 0.7f, 1f);
      if (i <= 2)
        return;
      rewardItemIcon.VisibleIcon(false);
    }));
  }

  private void SetPoint(RushResultPoint.UI parent, int point)
  {
    this.SetFontStyle(this.GetCtrl((Enum) parent), (Enum) RushResultPoint.UI.LBL_POINT, (FontStyle) 2);
    this.SetLabelText(this.GetCtrl((Enum) parent), (Enum) RushResultPoint.UI.LBL_POINT, point.ToString("N0") + "pt");
  }

  private void AddPointEventData(PointEventCurrentData add_data)
  {
    if (add_data == null || Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).eventId != add_data.eventId)
      return;
    this.allPointEvents.pointRankingData.getPoint += add_data.pointRankingData.getPoint;
    foreach (PointEventCurrentData.BonusPointData bonusPointData1 in add_data.pointRankingData.bonusPoint)
    {
      PointEventCurrentData.BonusPointData add_bonus = bonusPointData1;
      PointEventCurrentData.BonusPointData bonusPointData2 = this.allPointEvents.pointRankingData.bonusPoint.Find((Predicate<PointEventCurrentData.BonusPointData>) (bp => bp.name == add_bonus.name));
      if (bonusPointData2 == null)
        this.allPointEvents.pointRankingData.bonusPoint.Add(add_bonus);
      else
        bonusPointData2.point += add_bonus.point;
    }
    this.allPointEvents.pointRankingData.nextReward = add_data.pointRankingData.nextReward;
    this.allPointEvents.pointRankingData.getReward.AddRange((IEnumerable<PointEventCurrentData.PointRewardData>) add_data.pointRankingData.getReward);
  }

  private IEnumerator PlayAnimation()
  {
    this.is_skip = false;
    this.PlayTween((Enum) RushResultPoint.UI.OBJ_TITLE);
    this.SkipTween((Enum) RushResultPoint.UI.OBJ_TITLE);
    this.animState = RushResultPoint.RESULT_ANIM_STATE.POINT;
    this.PlayTween((Enum) RushResultPoint.UI.OBJ_RUSH_POINT, callback: (EventDelegate.Callback) (() =>
    {
      SoundManager.PlayOneShotUISE(40000228);
      this.animState = RushResultPoint.RESULT_ANIM_STATE.IDLE;
    }), is_input_block: false);
    while (this.animState != RushResultPoint.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.animState = RushResultPoint.RESULT_ANIM_STATE.COUNT_UP;
    this.StartCoroutine(this.GetPointAnimation((System.Action) (() => this.animState = RushResultPoint.RESULT_ANIM_STATE.IDLE)));
    while (this.animState != RushResultPoint.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.animState = RushResultPoint.RESULT_ANIM_STATE.NEXT_REWARD;
    this.PlayTween((Enum) RushResultPoint.UI.OBJ_NEXT_REWARD, callback: (EventDelegate.Callback) (() => this.animState = RushResultPoint.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    SoundManager.PlayOneShotUISE(40000228);
    if (this.allPointEvents.pointRankingData.getReward.Count > 0)
    {
      this.animState = RushResultPoint.RESULT_ANIM_STATE.REWARD;
      this.PlayTween((Enum) RushResultPoint.UI.OBJ_GET_REWARD_ROOT, callback: (EventDelegate.Callback) (() => this.animState = RushResultPoint.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    }
    this.animState = RushResultPoint.RESULT_ANIM_STATE.END;
    this.VisibleEndButton();
  }

  private IEnumerator GetPointAnimation(System.Action callback)
  {
    int getPoint = this.allPointEvents.pointRankingData.getPoint;
    int userPoint = this.allPointEvents.pointRankingData.userPoint;
    int totalPoint = userPoint + getPoint;
    this.SetFontStyle((Enum) RushResultPoint.UI.LBL_GET_POINT, (FontStyle) 2);
    yield return (object) this.StartCoroutine(this.CountUpAnimation(0.0f, getPoint, RushResultPoint.UI.LBL_GET_POINT));
    this.SetFontStyle((Enum) RushResultPoint.UI.LBL_TOTAL_POINT, (FontStyle) 2);
    yield return (object) this.StartCoroutine(this.CountUpAnimation((float) userPoint, totalPoint, RushResultPoint.UI.LBL_TOTAL_POINT));
    callback();
  }

  private IEnumerator CountUpAnimation(
    float currentPoint,
    int targetPoint,
    RushResultPoint.UI targetUI)
  {
    while ((double) currentPoint < (double) targetPoint)
    {
      yield return (object) 0;
      if (this.is_skip)
        currentPoint = (float) targetPoint;
      int num1 = Mathf.FloorToInt(currentPoint);
      currentPoint += Mathf.Max(((float) targetPoint - currentPoint) * RushResultPoint.CountDownCube(Time.deltaTime * 4f), 1f);
      currentPoint = Mathf.Min(currentPoint, (float) targetPoint);
      int num2 = Mathf.FloorToInt(currentPoint);
      if (num1 < num2)
        SoundManager.PlayOneShotUISE(40000012);
      this.SetLabelText((Enum) targetUI, Mathf.FloorToInt(currentPoint).ToString("N0") + "pt");
    }
  }

  private static float CountDownCube(float currentValue) => currentValue * (2f - currentValue);

  private void VisibleEndButton()
  {
    this.SetActive((Enum) RushResultPoint.UI.BTN_NEXT, this.animState == RushResultPoint.RESULT_ANIM_STATE.END);
    this.SetActive((Enum) RushResultPoint.UI.BTN_SKIP_FULL_SCREEN, this.animState != RushResultPoint.RESULT_ANIM_STATE.END);
    this.SetActive((Enum) RushResultPoint.UI.BTN_SKIP_IN_SCROLL, this.animState != RushResultPoint.RESULT_ANIM_STATE.END);
  }

  private void OnQuery_SKIP()
  {
    this.is_skip = true;
    switch (this.animState)
    {
      case RushResultPoint.RESULT_ANIM_STATE.POINT:
      case RushResultPoint.RESULT_ANIM_STATE.COUNT_UP:
      case RushResultPoint.RESULT_ANIM_STATE.NEXT_REWARD:
        this.SkipTween((Enum) RushResultPoint.UI.OBJ_RUSH_POINT);
        this.SkipTween((Enum) RushResultPoint.UI.OBJ_NEXT_REWARD);
        break;
      case RushResultPoint.RESULT_ANIM_STATE.REWARD:
        this.SkipTween((Enum) RushResultPoint.UI.OBJ_GET_REWARD_ROOT);
        break;
    }
  }

  private void OnQuery_NEXT()
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.SetResultedRush();
    if (MonoBehaviourSingleton<QuestManager>.I.compData != null)
      return;
    GameSection.ChangeEvent("FAILED");
  }

  private enum UI
  {
    OBJ_TITLE,
    LBL_QUEST_NAME,
    SPR_TITLE,
    OBJ_RUSH_POINT,
    OBJ_GET_POINT,
    LBL_GET_POINT,
    GRD_POINT_DETAIL,
    OBJ_POINT_DETAIL_ITEM,
    LBL_POINT,
    LBL_POINT_NAME,
    STR_POINT_NEXT,
    OBJ_TOTAL_POINT,
    LBL_TOTAL_POINT,
    OBJ_NEXT_REWARD,
    GRD_NEXT_ITEM_ROOT,
    OBJ_NEXT_ITEM_ROOT,
    BTN_NEXT,
    BTN_SKIP_FULL_SCREEN,
    BTN_SKIP_IN_SCROLL,
    OBJ_GET_REWARD_ROOT,
    GRD_ITEM_ROOT,
    OBJ_ITEM_ROOT,
    SHADOW,
  }

  private enum AUDIO
  {
    COUNTUP = 40000012, // 0x02625A0C
    CATEGORY = 40000228, // 0x02625AE4
    POINTREWARD = 40000230, // 0x02625AE6
  }

  private enum RESULT_ANIM_STATE
  {
    IDLE,
    POINT,
    COUNT_UP,
    NEXT_REWARD,
    REWARD,
    END,
  }
}
