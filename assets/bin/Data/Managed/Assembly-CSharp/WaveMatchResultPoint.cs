// Decompiled with JetBrains decompiler
// Type: WaveMatchResultPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class WaveMatchResultPoint : GameSection
{
  private const float COUNT_ANIM_SPEED = 4f;
  private bool is_skip;
  private PointEventCurrentData pointEvents;
  private WaveMatchResultPoint.RESULT_ANIM_STATE animState;

  public override void Initialize() => this.StartCoroutine(this.LoadSE());

  private IEnumerator LoadSE()
  {
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
    yield return (object) new WaitForEndOfFrame();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    foreach (int se_id in (int[]) Enum.GetValues(typeof (WaveMatchResultPoint.AUDIO)))
      loadingQueue.CacheSE(se_id);
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    GC.Collect();
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.pointEvents = MonoBehaviourSingleton<QuestManager>.I.compData == null ? MonoBehaviourSingleton<QuestManager>.I.retireData.waveMatchPoint : MonoBehaviourSingleton<QuestManager>.I.compData.waveMatchPoint;
    PointEventCurrentData.PointResultData d = this.pointEvents.pointRankingData;
    this.SetLabelText((Enum) WaveMatchResultPoint.UI.LBL_QUEST_NAME, Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).questText);
    this.SetFontStyle((Enum) WaveMatchResultPoint.UI.LBL_GET_POINT, (FontStyle) 2);
    this.SetLabelText((Enum) WaveMatchResultPoint.UI.LBL_GET_POINT, "0pt");
    this.SetFontStyle((Enum) WaveMatchResultPoint.UI.LBL_TOTAL_POINT, (FontStyle) 2);
    this.SetLabelText((Enum) WaveMatchResultPoint.UI.LBL_TOTAL_POINT, d.userPoint.ToString("N0") + "pt");
    this.SetGrid((Enum) WaveMatchResultPoint.UI.GRD_POINT_DETAIL, "WaveMatchResultPointDetailItem", d.bonusPoint.Count, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      UILabel component1 = ((Component) this.FindCtrl(t, (Enum) WaveMatchResultPoint.UI.LBL_POINT)).GetComponent<UILabel>();
      component1.alpha = 1f;
      component1.text = d.bonusPoint[i].point.ToString("N0");
      component1.fontStyle = (FontStyle) 2;
      UILabel component2 = ((Component) this.FindCtrl(t, (Enum) WaveMatchResultPoint.UI.LBL_POINT_NAME)).GetComponent<UILabel>();
      component2.alpha = 1f;
      component2.text = d.bonusPoint[i].name;
      component2.fontStyle = (FontStyle) 2;
    }));
    if (d.nextReward != null)
    {
      this.SetAllRewardItem(WaveMatchResultPoint.UI.GRD_NEXT_ITEM_ROOT, d.nextReward.reward);
      this.SetPoint(WaveMatchResultPoint.UI.OBJ_NEXT_REWARD, d.nextReward.point - (d.userPoint + d.getPoint));
    }
    else
    {
      this.SetActive((Enum) WaveMatchResultPoint.UI.STR_POINT_NEXT, false);
      this.SetFontStyle(this.GetCtrl((Enum) WaveMatchResultPoint.UI.OBJ_NEXT_REWARD), (Enum) WaveMatchResultPoint.UI.LBL_POINT, (FontStyle) 2);
      this.SetLabelText(this.GetCtrl((Enum) WaveMatchResultPoint.UI.OBJ_NEXT_REWARD), (Enum) WaveMatchResultPoint.UI.LBL_POINT, "None");
    }
    List<PointEventCurrentData.Reward> rewardList = new List<PointEventCurrentData.Reward>();
    foreach (PointEventCurrentData.PointRewardData pointRewardData in d.getReward)
      rewardList.AddRange((IEnumerable<PointEventCurrentData.Reward>) pointRewardData.reward);
    this.SetAllRewardItem(WaveMatchResultPoint.UI.GRD_ITEM_ROOT, rewardList);
    this.StartCoroutine(this.PlayAnimation());
  }

  private void SetAllRewardItem(
    WaveMatchResultPoint.UI targetGrid,
    List<PointEventCurrentData.Reward> rewardList)
  {
    this.SetGrid((Enum) targetGrid, "ItemIconReward", rewardList.Count, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      PointEventCurrentData.Reward reward = rewardList[i];
      ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward.type, (uint) reward.itemId, t, reward.num);
      if (Object.op_Inequality((Object) rewardItemIcon, (Object) null))
        rewardItemIcon.SetEnableCollider(false);
      ((Component) t.Find("itemNum")).GetComponent<UILabel>().text = "×" + (object) rewardList[i].num;
      if (targetGrid != WaveMatchResultPoint.UI.GRD_NEXT_ITEM_ROOT)
        return;
      t.localScale = new Vector3(0.7f, 0.7f, 1f);
      if (i <= 2)
        return;
      rewardItemIcon.VisibleIcon(false);
    }));
  }

  private void SetPoint(WaveMatchResultPoint.UI parent, int point)
  {
    this.SetFontStyle(this.GetCtrl((Enum) parent), (Enum) WaveMatchResultPoint.UI.LBL_POINT, (FontStyle) 2);
    this.SetLabelText(this.GetCtrl((Enum) parent), (Enum) WaveMatchResultPoint.UI.LBL_POINT, point.ToString("N0") + "pt");
  }

  private void AddPointEventData(PointEventCurrentData add_data)
  {
    if (add_data == null || Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).eventId != add_data.eventId)
      return;
    this.pointEvents.pointRankingData.getPoint += add_data.pointRankingData.getPoint;
    foreach (PointEventCurrentData.BonusPointData bonusPointData1 in add_data.pointRankingData.bonusPoint)
    {
      PointEventCurrentData.BonusPointData add_bonus = bonusPointData1;
      PointEventCurrentData.BonusPointData bonusPointData2 = this.pointEvents.pointRankingData.bonusPoint.Find((Predicate<PointEventCurrentData.BonusPointData>) (bp => bp.name == add_bonus.name));
      if (bonusPointData2 == null)
        this.pointEvents.pointRankingData.bonusPoint.Add(add_bonus);
      else
        bonusPointData2.point += add_bonus.point;
    }
    this.pointEvents.pointRankingData.nextReward = add_data.pointRankingData.nextReward;
    this.pointEvents.pointRankingData.getReward.AddRange((IEnumerable<PointEventCurrentData.PointRewardData>) add_data.pointRankingData.getReward);
  }

  private IEnumerator PlayAnimation()
  {
    this.is_skip = false;
    this.PlayTween((Enum) WaveMatchResultPoint.UI.OBJ_TITLE);
    this.SkipTween((Enum) WaveMatchResultPoint.UI.OBJ_TITLE);
    this.animState = WaveMatchResultPoint.RESULT_ANIM_STATE.POINT;
    this.PlayTween((Enum) WaveMatchResultPoint.UI.OBJ_WAVEMATCH_POINT, callback: (EventDelegate.Callback) (() =>
    {
      SoundManager.PlayOneShotUISE(40000228);
      this.animState = WaveMatchResultPoint.RESULT_ANIM_STATE.IDLE;
    }), is_input_block: false);
    while (this.animState != WaveMatchResultPoint.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.animState = WaveMatchResultPoint.RESULT_ANIM_STATE.COUNT_UP;
    this.StartCoroutine(this.GetPointAnimation((System.Action) (() => this.animState = WaveMatchResultPoint.RESULT_ANIM_STATE.IDLE)));
    while (this.animState != WaveMatchResultPoint.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.animState = WaveMatchResultPoint.RESULT_ANIM_STATE.NEXT_REWARD;
    this.PlayTween((Enum) WaveMatchResultPoint.UI.OBJ_NEXT_REWARD, callback: (EventDelegate.Callback) (() => this.animState = WaveMatchResultPoint.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    SoundManager.PlayOneShotUISE(40000228);
    if (this.pointEvents.pointRankingData.getReward.Count > 0)
    {
      this.animState = WaveMatchResultPoint.RESULT_ANIM_STATE.REWARD;
      this.PlayTween((Enum) WaveMatchResultPoint.UI.OBJ_GET_REWARD_ROOT, callback: (EventDelegate.Callback) (() => this.animState = WaveMatchResultPoint.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    }
    this.animState = WaveMatchResultPoint.RESULT_ANIM_STATE.END;
    this.VisibleEndButton();
  }

  private IEnumerator GetPointAnimation(System.Action callback)
  {
    int getPoint = this.pointEvents.pointRankingData.getPoint;
    int userPoint = this.pointEvents.pointRankingData.userPoint;
    int totalPoint = userPoint + getPoint;
    this.SetFontStyle((Enum) WaveMatchResultPoint.UI.LBL_GET_POINT, (FontStyle) 2);
    yield return (object) this.StartCoroutine(this.CountUpAnimation(0.0f, getPoint, WaveMatchResultPoint.UI.LBL_GET_POINT));
    this.SetFontStyle((Enum) WaveMatchResultPoint.UI.LBL_TOTAL_POINT, (FontStyle) 2);
    yield return (object) this.StartCoroutine(this.CountUpAnimation((float) userPoint, totalPoint, WaveMatchResultPoint.UI.LBL_TOTAL_POINT));
    callback();
  }

  private IEnumerator CountUpAnimation(
    float currentPoint,
    int targetPoint,
    WaveMatchResultPoint.UI targetUI)
  {
    while ((double) currentPoint < (double) targetPoint)
    {
      yield return (object) 0;
      if (this.is_skip)
        currentPoint = (float) targetPoint;
      int num1 = Mathf.FloorToInt(currentPoint);
      currentPoint += Mathf.Max(((float) targetPoint - currentPoint) * WaveMatchResultPoint.CountDownCube(Time.deltaTime * 4f), 1f);
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
    this.SetActive((Enum) WaveMatchResultPoint.UI.BTN_NEXT, this.animState == WaveMatchResultPoint.RESULT_ANIM_STATE.END);
    this.SetActive((Enum) WaveMatchResultPoint.UI.BTN_SKIP_FULL_SCREEN, this.animState != WaveMatchResultPoint.RESULT_ANIM_STATE.END);
    this.SetActive((Enum) WaveMatchResultPoint.UI.BTN_SKIP_IN_SCROLL, this.animState != WaveMatchResultPoint.RESULT_ANIM_STATE.END);
  }

  private void OnQuery_SKIP()
  {
    this.is_skip = true;
    switch (this.animState)
    {
      case WaveMatchResultPoint.RESULT_ANIM_STATE.POINT:
      case WaveMatchResultPoint.RESULT_ANIM_STATE.COUNT_UP:
      case WaveMatchResultPoint.RESULT_ANIM_STATE.NEXT_REWARD:
        this.SkipTween((Enum) WaveMatchResultPoint.UI.OBJ_WAVEMATCH_POINT);
        this.SkipTween((Enum) WaveMatchResultPoint.UI.OBJ_NEXT_REWARD);
        break;
      case WaveMatchResultPoint.RESULT_ANIM_STATE.REWARD:
        this.SkipTween((Enum) WaveMatchResultPoint.UI.OBJ_GET_REWARD_ROOT);
        break;
    }
  }

  private void OnQuery_NEXT()
  {
    if (MonoBehaviourSingleton<QuestManager>.I.compData != null)
      return;
    GameSection.ChangeEvent("FAILED");
  }

  private enum UI
  {
    OBJ_TITLE,
    LBL_QUEST_NAME,
    SPR_TITLE,
    OBJ_WAVEMATCH_POINT,
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
