// Decompiled with JetBrains decompiler
// Type: CarnivalResultPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CarnivalResultPoint : GameSection
{
  private CarnivalResultPoint.RESULT_TYPE resultType;
  private CarnivalResultPoint.State pointResultState;
  private bool stateInitialized;
  private PointEventCurrentData currentData;
  private const string BOOST_BONUS_NAME = "ブーストポイント";
  private int boostPoint;
  private float boostRate;
  private const string TITLE_LOGO_NAME_FORMAT = "ef_ui_pointresult";
  private const float COUNT_ANIM_SPEED = 4f;
  private bool skipRequest;
  private Transform bannerCtrl;
  private MeshRenderer[] bannerMeshRenderers;
  private const float FADE_SPEED = 5f;
  private readonly string[] SPR_RANKING_NUMBER = new string[10]
  {
    "RankingNumber_0",
    "RankingNumber_1",
    "RankingNumber_2",
    "RankingNumber_3",
    "RankingNumber_4",
    "RankingNumber_5",
    "RankingNumber_6",
    "RankingNumber_7",
    "RankingNumber_8",
    "RankingNumber_9"
  };
  private readonly string[] SPR_PASS_NUMBER = new string[10]
  {
    "RankingNumber2_0",
    "RankingNumber2_1",
    "RankingNumber2_2",
    "RankingNumber2_3",
    "RankingNumber2_4",
    "RankingNumber2_5",
    "RankingNumber2_6",
    "RankingNumber2_7",
    "RankingNumber2_8",
    "RankingNumber2_9"
  };
  private List<GameObject> rankingNumbers;
  private List<GameObject> passPositionNumbers;
  private int beforeDigits;

  public override void Initialize()
  {
    this.SetPointEventData();
    this.SetActive((Enum) CarnivalResultPoint.UI.OBJ_REWARD_ROOT, false);
    this.SetActive((Enum) CarnivalResultPoint.UI.OBJ_RANKING_ROOT, false);
    this.SetActive((Enum) CarnivalResultPoint.UI.OBJ_BONUS_ROOT, false);
    this.bannerCtrl = this.GetCtrl((Enum) CarnivalResultPoint.UI.TXT_BANNER);
    this.StartCoroutine(this.DoInitalize());
  }

  private void SetPointEventData()
  {
    PointEventCurrentData allPointEvents = new PointEventCurrentData();
    allPointEvents.pointRankingData = new PointEventCurrentData.PointResultData();
    if (MonoBehaviourSingleton<InGameManager>.I.rushPointEvents != null)
    {
      this.resultType = CarnivalResultPoint.RESULT_TYPE.RUSH;
      int index = 0;
      for (int count = MonoBehaviourSingleton<InGameManager>.I.rushPointEvents.Count; index < count; ++index)
      {
        PointEventCurrentData rushPointEvent = MonoBehaviourSingleton<InGameManager>.I.rushPointEvents[index];
        if (index == 0)
        {
          allPointEvents.eventId = rushPointEvent.eventId;
          allPointEvents.pointRankingData.userPoint = rushPointEvent.pointRankingData.userPoint;
          allPointEvents.pointRankingData.beforeRank = rushPointEvent.pointRankingData.beforeRank;
        }
        if (index == count - 1)
        {
          allPointEvents.pointRankingData.afterRank = rushPointEvent.pointRankingData.afterRank;
          allPointEvents.pointRankingData.isStartedBoost = rushPointEvent.pointRankingData.isStartedBoost;
        }
        this.AddPointEventData(rushPointEvent, allPointEvents);
      }
      this.currentData = allPointEvents;
    }
    else if (MonoBehaviourSingleton<QuestManager>.I.compData != null && MonoBehaviourSingleton<QuestManager>.I.compData.waveMatchPoint != null)
    {
      this.resultType = CarnivalResultPoint.RESULT_TYPE.WAVE;
      this.currentData = MonoBehaviourSingleton<QuestManager>.I.compData.waveMatchPoint;
    }
    else if (MonoBehaviourSingleton<QuestManager>.I.retireData != null && MonoBehaviourSingleton<QuestManager>.I.retireData.waveMatchPoint != null)
    {
      this.resultType = CarnivalResultPoint.RESULT_TYPE.WAVE;
      this.currentData = MonoBehaviourSingleton<QuestManager>.I.retireData.waveMatchPoint;
    }
    else
    {
      this.currentData = GameSection.GetEventData() as PointEventCurrentData;
      if (this.currentData != null)
      {
        this.resultType = CarnivalResultPoint.RESULT_TYPE.NORMAL;
      }
      else
      {
        if (this.currentData != null)
          return;
        Debug.LogError((object) "CarnivalResultDataが存在しません!!!");
        this.currentData = new PointEventCurrentData();
        this.currentData.pointRankingData = new PointEventCurrentData.PointResultData();
      }
    }
  }

  private void AddPointEventData(
    PointEventCurrentData addData,
    PointEventCurrentData allPointEvents)
  {
    if (addData == null)
      return;
    allPointEvents.pointRankingData.getPoint += addData.pointRankingData.getPoint;
    foreach (PointEventCurrentData.BonusPointData bonusPointData1 in addData.pointRankingData.bonusPoint)
    {
      PointEventCurrentData.BonusPointData add_bonus = bonusPointData1;
      PointEventCurrentData.BonusPointData bonusPointData2 = allPointEvents.pointRankingData.bonusPoint.Find((Predicate<PointEventCurrentData.BonusPointData>) (bp => bp.name == add_bonus.name));
      if (bonusPointData2 == null)
        allPointEvents.pointRankingData.bonusPoint.Add(add_bonus);
      else
        bonusPointData2.point += add_bonus.point;
    }
    allPointEvents.pointRankingData.nextReward = addData.pointRankingData.nextReward;
    allPointEvents.pointRankingData.getReward.AddRange((IEnumerable<PointEventCurrentData.PointRewardData>) addData.pointRankingData.getReward);
  }

  private IEnumerator DoInitalize()
  {
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
    yield return (object) new WaitForEndOfFrame();
    LoadingQueue loadingQueue1 = new LoadingQueue((MonoBehaviour) this);
    loadingQueue1.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_pointresult");
    LoadingQueue loadingQueue2 = new LoadingQueue((MonoBehaviour) this);
    foreach (int se_id in (int[]) Enum.GetValues(typeof (CarnivalResultPoint.AUDIO)))
      loadingQueue2.CacheSE(se_id);
    int event_id = -1;
    if (this.currentData != null)
    {
      event_id = this.currentData.eventId;
      string eventBannerResult = ResourceName.GetQuestEventBannerResult(event_id);
      if (Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.event_manifest, (Object) null))
      {
        event_id = 10017100;
      }
      else
      {
        Hash128 assetBundleHash = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BANNER_RESULT.ToAssetBundleName(eventBannerResult));
        if (!((Hash128) ref assetBundleHash).isValid)
          event_id = 10017100;
      }
    }
    ResourceLoad.LoadEventBannerResultTexture(((Component) this.GetCtrl((Enum) CarnivalResultPoint.UI.SPR_LOGO)).GetComponent<UITexture>(), (uint) event_id);
    if (loadingQueue1.IsLoading())
      yield return (object) loadingQueue1.Wait();
    GC.Collect();
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
    this.SetVisibleWidgetEffect((Enum) CarnivalResultPoint.UI.TXT_BANNER, "ef_ui_pointresult");
    ((Component) this.bannerCtrl).GetComponent<UIVisibleWidgetEffect>().SetRendererQueue(4000);
    this.PlayAudio(CarnivalResultPoint.AUDIO.TITLE_LOGO);
    base.Initialize();
  }

  private void Update()
  {
    switch (this.pointResultState)
    {
      case CarnivalResultPoint.State.START:
        Animation componentInChildren = ((Component) this.bannerCtrl).GetComponentInChildren<Animation>(true);
        if (Object.op_Equality((Object) componentInChildren, (Object) null) || componentInChildren.isPlaying)
          break;
        this.pointResultState = CarnivalResultPoint.State.TO_REWARD;
        break;
      case CarnivalResultPoint.State.TO_REWARD:
        if (!this.stateInitialized)
        {
          this.bannerMeshRenderers = ((Component) this.bannerCtrl).GetComponentsInChildren<MeshRenderer>();
          this.stateInitialized = true;
        }
        int num1 = 0;
        for (int index = 0; index < this.bannerMeshRenderers.Length; ++index)
        {
          Color color = ((Renderer) this.bannerMeshRenderers[index]).material.color;
          color.a = Mathf.Max(0.0f, color.a - 5f * Time.deltaTime);
          ((Renderer) this.bannerMeshRenderers[index]).material.color = color;
          if ((double) ((Renderer) this.bannerMeshRenderers[index]).material.color.a <= 0.0)
            ++num1;
          if (index == this.bannerMeshRenderers.Length - 1)
          {
            if (num1 == this.bannerMeshRenderers.Length)
            {
              this.bannerCtrl.position = this.GetCtrl((Enum) CarnivalResultPoint.UI.OBJ_REWARD_POS).position;
              this.bannerCtrl.localScale = this.GetCtrl((Enum) CarnivalResultPoint.UI.OBJ_REWARD_POS).localScale;
              this.StartCoroutine(this.WaitTiming(2f));
              this.pointResultState = CarnivalResultPoint.State.REWARD;
              this.stateInitialized = false;
            }
            else
              num1 = 0;
          }
        }
        break;
      case CarnivalResultPoint.State.REWARD:
        if (this.stateInitialized)
          break;
        int num2 = 0;
        for (int index = 0; index < this.bannerMeshRenderers.Length; ++index)
        {
          Color color = ((Renderer) this.bannerMeshRenderers[index]).material.color;
          color.a = Mathf.Min(1f, color.a + 5f * Time.deltaTime);
          ((Renderer) this.bannerMeshRenderers[index]).material.color = color;
          if ((double) ((Renderer) this.bannerMeshRenderers[index]).material.color.a >= 1.0)
            ++num2;
          if (index == this.bannerMeshRenderers.Length - 1)
          {
            if (num2 == this.bannerMeshRenderers.Length)
            {
              this.SetActive((Enum) CarnivalResultPoint.UI.OBJ_REWARD_ROOT, true);
              this.stateInitialized = true;
              this.SetRewardUI();
            }
            else
              num2 = 0;
          }
        }
        break;
      case CarnivalResultPoint.State.BONUS:
        if (this.stateInitialized)
          break;
        this.SetActive((Enum) CarnivalResultPoint.UI.OBJ_REWARD_ROOT, false);
        this.SetActive((Enum) CarnivalResultPoint.UI.OBJ_BONUS_ROOT, true);
        this.SetActive((Enum) CarnivalResultPoint.UI.TXT_BANNER, false);
        this.SetBonusUI();
        this.stateInitialized = true;
        break;
      case CarnivalResultPoint.State.RANKING:
        if (this.stateInitialized)
          break;
        this.SetActive((Enum) CarnivalResultPoint.UI.OBJ_REWARD_ROOT, false);
        this.SetActive((Enum) CarnivalResultPoint.UI.OBJ_BONUS_ROOT, false);
        this.SetActive((Enum) CarnivalResultPoint.UI.OBJ_RANKING_ROOT, true);
        this.SetActive((Enum) CarnivalResultPoint.UI.TXT_BANNER, false);
        this.SetRankingUI();
        this.stateInitialized = true;
        break;
    }
  }

  private void SetRewardUI()
  {
    this.SetFullScreenButton((Enum) CarnivalResultPoint.UI.BTN_SKIP_FULL_SCREEN);
    this.SetActive((Enum) CarnivalResultPoint.UI.BTN_OK, false);
    this.InitTween((Enum) CarnivalResultPoint.UI.OBJ_GET_REWARD_ROOT);
    PointEventCurrentData.PointResultData data = this.currentData.pointRankingData;
    this.SetGrid((Enum) CarnivalResultPoint.UI.GRD_POINT_DETAIL, "CarnivalResultPointDetailItem", data.bonusPoint.Count, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      UILabel component1 = ((Component) this.FindCtrl(t, (Enum) CarnivalResultPoint.UI.LBL_POINT)).GetComponent<UILabel>();
      component1.alpha = 1f;
      component1.text = data.bonusPoint[i].point.ToString("N0");
      component1.fontStyle = (FontStyle) 2;
      UILabel component2 = ((Component) this.FindCtrl(t, (Enum) CarnivalResultPoint.UI.LBL_POINT_NAME)).GetComponent<UILabel>();
      component2.alpha = 1f;
      component2.text = data.bonusPoint[i].name;
      component2.fontStyle = (FontStyle) 2;
    }));
    MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti = false;
    this.SetLabelText((Enum) CarnivalResultPoint.UI.LBL_GET_POINT, data.getPoint.ToString("N0"));
    this.SetLabelText((Enum) CarnivalResultPoint.UI.LBL_TOTAL_POINT, (data.userPoint + data.getPoint).ToString("N0"));
    if (data.nextReward != null)
    {
      this.SetAllRewardItem(CarnivalResultPoint.UI.GRD_NEXT_ITEM_ROOT, data.nextReward.reward);
      this.SetPoint(CarnivalResultPoint.UI.OBJ_NEXT_REWARD, data.nextReward.point - (data.userPoint + data.getPoint));
    }
    else
    {
      this.SetActive((Enum) CarnivalResultPoint.UI.STR_POINT_NEXT, false);
      this.SetFontStyle(this.GetCtrl((Enum) CarnivalResultPoint.UI.OBJ_NEXT_REWARD), (Enum) CarnivalResultPoint.UI.LBL_POINT, (FontStyle) 2);
      this.SetLabelText(this.GetCtrl((Enum) CarnivalResultPoint.UI.OBJ_NEXT_REWARD), (Enum) CarnivalResultPoint.UI.LBL_POINT, "None");
    }
    List<PointEventCurrentData.Reward> rewardList = new List<PointEventCurrentData.Reward>();
    foreach (PointEventCurrentData.PointRewardData pointRewardData in data.getReward)
      rewardList.AddRange((IEnumerable<PointEventCurrentData.Reward>) pointRewardData.reward);
    this.SetAllRewardItem(CarnivalResultPoint.UI.GRD_ITEM_ROOT, rewardList);
    this.StartCoroutine(this.GetPointAnimation());
  }

  private IEnumerator GetPointAnimation()
  {
    int getPoint = this.currentData.pointRankingData.getPoint;
    int userPoint = this.currentData.pointRankingData.userPoint;
    int totalPoint = userPoint + getPoint;
    bool wait = true;
    this.PlayAudio(CarnivalResultPoint.AUDIO.CATEGORY);
    wait = true;
    this.SetLabelText((Enum) CarnivalResultPoint.UI.LBL_GET_POINT, "0");
    this.PlayTween((Enum) CarnivalResultPoint.UI.OBJ_CARNIVAL_POINT, callback: (EventDelegate.Callback) (() => wait = false));
    while (wait)
    {
      if (this.skipRequest)
      {
        this.SkipTween((Enum) CarnivalResultPoint.UI.OBJ_CARNIVAL_POINT);
        wait = false;
      }
      yield return (object) 0;
    }
    yield return (object) this.StartCoroutine(this.CountUpAnimation(0.0f, getPoint - this.boostPoint, CarnivalResultPoint.UI.LBL_GET_POINT));
    yield return (object) this.StartCoroutine(this.CountUpAnimation((float) userPoint, totalPoint, CarnivalResultPoint.UI.LBL_TOTAL_POINT));
    if (this.currentData.pointRankingData.getReward.Count > 0)
    {
      this.PlayAudio(CarnivalResultPoint.AUDIO.POINTREWARD);
      wait = true;
      this.PlayTween((Enum) CarnivalResultPoint.UI.OBJ_GET_REWARD_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
      while (wait)
      {
        if (this.skipRequest)
        {
          this.SkipTween((Enum) CarnivalResultPoint.UI.OBJ_GET_REWARD_ROOT);
          wait = false;
        }
        yield return (object) 0;
      }
    }
    this.SetActive((Enum) CarnivalResultPoint.UI.BTN_OK, true);
    this.SetActive((Enum) CarnivalResultPoint.UI.BTN_SKIP_FULL_SCREEN, false);
  }

  private void SetNextItemIcon(List<PointEventCurrentData.Reward> reward)
  {
    this.SetDynamicList((Enum) CarnivalResultPoint.UI.OBJ_NEXT_REWARD_ITEM_ICON_ROOT, "ItemIcon", reward.Count, true, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      PointEventCurrentData.Reward reward1 = reward[i];
      ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward1.type, (uint) reward1.itemId, t, reward1.num);
    }));
  }

  private IEnumerator CountUpAnimation(
    float currentPoint,
    int targetPoint,
    CarnivalResultPoint.UI targetUI)
  {
    while ((double) currentPoint < (double) targetPoint)
    {
      yield return (object) 0;
      if (this.skipRequest)
        currentPoint = (float) targetPoint;
      int num1 = Mathf.FloorToInt(currentPoint);
      currentPoint += Mathf.Max(((float) targetPoint - currentPoint) * CarnivalResultPoint.CountDownCube(Time.deltaTime * 4f), 1f);
      currentPoint = Mathf.Min(currentPoint, (float) targetPoint);
      int num2 = Mathf.FloorToInt(currentPoint);
      if (num1 < num2)
        this.PlayAudio(CarnivalResultPoint.AUDIO.POINTUP);
      this.SetLabelText((Enum) targetUI, Mathf.FloorToInt(currentPoint).ToString("N0"));
    }
  }

  private IEnumerator CountDownAnimation(
    float currentPoint,
    int targetPoint,
    CarnivalResultPoint.UI targetUI)
  {
    while ((double) currentPoint > (double) targetPoint)
    {
      yield return (object) 0;
      if (this.skipRequest)
        currentPoint = (float) targetPoint;
      int num1 = Mathf.FloorToInt(currentPoint);
      currentPoint += Mathf.Min(((float) targetPoint - currentPoint) * CarnivalResultPoint.CountDownCube(Time.deltaTime * 4f), -1f);
      currentPoint = Mathf.Max(currentPoint, (float) targetPoint);
      int num2 = Mathf.FloorToInt(currentPoint);
      if (num1 > num2)
        this.PlayAudio(CarnivalResultPoint.AUDIO.POINTUP);
      this.SetLabelText((Enum) targetUI, Mathf.CeilToInt(currentPoint).ToString("N0"));
    }
  }

  private void SetAllRewardItem(
    CarnivalResultPoint.UI targetGrid,
    List<PointEventCurrentData.Reward> rewardList)
  {
    this.SetGrid((Enum) targetGrid, "ItemIconReward", rewardList.Count, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      PointEventCurrentData.Reward reward = rewardList[i];
      ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward.type, (uint) reward.itemId, t, reward.num);
      if (Object.op_Inequality((Object) rewardItemIcon, (Object) null))
        rewardItemIcon.SetEnableCollider(false);
      ((Component) t.Find("itemNum")).GetComponent<UILabel>().text = "×" + (object) rewardList[i].num;
      if (targetGrid != CarnivalResultPoint.UI.GRD_NEXT_ITEM_ROOT)
        return;
      t.localScale = new Vector3(0.7f, 0.7f, 1f);
      if (i <= 2)
        return;
      rewardItemIcon.VisibleIcon(false);
    }));
  }

  private void SetPoint(CarnivalResultPoint.UI parent, int point)
  {
    this.SetFontStyle(this.GetCtrl((Enum) parent), (Enum) CarnivalResultPoint.UI.LBL_POINT, (FontStyle) 2);
    this.SetLabelText(this.GetCtrl((Enum) parent), (Enum) CarnivalResultPoint.UI.LBL_POINT, point.ToString("N0") + "pt");
  }

  private void SetBonusUI()
  {
    this.InitTween((Enum) CarnivalResultPoint.UI.OBJ_BONUS_ANIM_ROOT);
    this.StartCoroutine(this.StartBonusAnimation());
  }

  private IEnumerator StartBonusAnimation()
  {
    SoundManager.PlayOneshotJingle(40000268);
    this.PlayTween((Enum) CarnivalResultPoint.UI.OBJ_BONUS_ANIM_ROOT);
    yield return (object) this.StartCoroutine(this.WaitTiming(2.8f));
    this.ChangeToRankingState();
  }

  private void SetRankingUI()
  {
    this.SetActive((Enum) CarnivalResultPoint.UI.BTN_SKIP_FULL_SCREEN, true);
    PointEventCurrentData.PointResultData pointRankingData = this.currentData.pointRankingData;
    this.SetLabelText((Enum) CarnivalResultPoint.UI.LBL_RANKING_TOTAL_POINT, (pointRankingData.userPoint + pointRankingData.getPoint).ToString("N0"));
    this.SetLabelText((Enum) CarnivalResultPoint.UI.LBL_PASS_NUM, Mathf.Max(0, pointRankingData.beforeRank - pointRankingData.afterRank).ToString("N0") + "人抜き");
    int num1 = Mathf.Min(999999, pointRankingData.beforeRank);
    int num2 = Mathf.Min(999999, pointRankingData.afterRank);
    int num3 = num1 <= num2 ? num2.ToString().Length : num1.ToString().Length;
    this.rankingNumbers = new List<GameObject>(6);
    Transform ctrl = this.GetCtrl((Enum) CarnivalResultPoint.UI.GRD_COUNT_NUMBERS);
    for (int index = 0; index < 6; ++index)
    {
      Transform transform = Utility.Find(ctrl, "Number" + (object) index);
      if (!Object.op_Equality((Object) transform, (Object) null))
      {
        if (index >= num3)
        {
          transform.parent = (Transform) null;
          Object.Destroy((Object) ((Component) transform).gameObject);
        }
        else
          this.rankingNumbers.Add(((Component) transform).gameObject);
      }
    }
    ((Component) ctrl).GetComponent<UIGrid>().Reposition();
    if (num1 > num2)
      this.SetSpriteNumber(num1);
    else
      this.SetSpriteNumber(num2);
    this.InitTween((Enum) CarnivalResultPoint.UI.OBJ_RANKING_ANIM_ROOT);
    this.StartCoroutine(this.StartRankingAnimation());
  }

  private IEnumerator StartRankingAnimation()
  {
    this.PlayAudio(CarnivalResultPoint.AUDIO.CATEGORY);
    bool wait = true;
    this.PlayTween((Enum) CarnivalResultPoint.UI.OBJ_RANKING_ANIM_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
    while (wait)
    {
      if (this.skipRequest)
      {
        this.SkipTween((Enum) CarnivalResultPoint.UI.OBJ_RANKING_ROOT);
        wait = false;
      }
      yield return (object) 0;
    }
    if (!this.skipRequest)
      yield return (object) this.StartCoroutine(this.WaitTiming(1f));
    int currentRank = this.currentData.pointRankingData.beforeRank;
    int targetRank = this.currentData.pointRankingData.afterRank;
    if (currentRank > targetRank)
    {
      yield return (object) this.StartCoroutine(this.CountSpriteAnimation((float) currentRank, targetRank));
      int num = currentRank - targetRank;
      Transform ctrl1 = this.GetCtrl((Enum) CarnivalResultPoint.UI.SPR_POSITION_PASS);
      this.passPositionNumbers = new List<GameObject>();
      this.passPositionNumbers.Add(((Component) ctrl1).gameObject);
      for (int index = 0; index < num.ToString().Length - 1; ++index)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(((Component) ctrl1).gameObject);
        gameObject.transform.parent = ctrl1.parent;
        gameObject.transform.localPosition = ctrl1.localPosition;
        gameObject.transform.localScale = ctrl1.localScale;
        this.passPositionNumbers.Add(gameObject);
      }
      this.SetSpritePassNumber(num, this.passPositionNumbers);
      ((Component) this.GetCtrl((Enum) CarnivalResultPoint.UI.GRD_POSITION_PASS)).GetComponent<UIGrid>().Reposition();
      Transform ctrl2 = this.GetCtrl((Enum) CarnivalResultPoint.UI.SPR_PASS_TEXT);
      Vector3 localPosition = this.passPositionNumbers[this.passPositionNumbers.Count - 1].transform.localPosition;
      ((Component) ctrl2).GetComponent<TweenPosition>().to.x = localPosition.x + 41f;
      this.InitTween((Enum) CarnivalResultPoint.UI.OBJ_PASS_ANIM_ROOT);
      if (!this.skipRequest)
        yield return (object) this.StartCoroutine(this.WaitTiming(0.6f));
      this.PlayAudio(CarnivalResultPoint.AUDIO.RESULT);
      wait = true;
      this.PlayTween((Enum) CarnivalResultPoint.UI.OBJ_PASS_ANIM_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
      while (wait)
      {
        if (this.skipRequest)
        {
          this.SkipTween((Enum) CarnivalResultPoint.UI.OBJ_PASS_ANIM_ROOT);
          wait = false;
        }
        yield return (object) 0;
      }
    }
    this.SetActive((Enum) CarnivalResultPoint.UI.BTN_SKIP_FULL_SCREEN, false);
    this.SetActive((Enum) CarnivalResultPoint.UI.BTN_END_OK, true);
  }

  private IEnumerator CountSpriteAnimation(float currentRank, int targetRank)
  {
    while ((double) currentRank > (double) targetRank)
    {
      yield return (object) 0;
      if (this.skipRequest)
        currentRank = (float) targetRank;
      int num1 = Mathf.FloorToInt(currentRank);
      currentRank += Mathf.Min(((float) targetRank - currentRank) * CarnivalResultPoint.CountDownCube(Time.deltaTime * 4f), -1f);
      currentRank = Mathf.Max(currentRank, (float) targetRank);
      int num2 = Mathf.FloorToInt(currentRank);
      if (num1 > num2)
        this.PlayAudio(CarnivalResultPoint.AUDIO.POINTUP);
      this.SetSpriteNumber(Mathf.Max((int) currentRank, 1));
    }
  }

  private void SetSpriteNumber(int value)
  {
    string str = value.ToString();
    if (this.beforeDigits == 0)
      this.beforeDigits = str.Length;
    if (this.beforeDigits > str.Length)
    {
      Object.Destroy((Object) this.rankingNumbers[0].gameObject);
      this.rankingNumbers.RemoveAt(0);
    }
    for (int index1 = 0; index1 < str.Length; ++index1)
    {
      int index2 = int.Parse(str[index1].ToString());
      if (!Object.op_Equality((Object) this.rankingNumbers[index1], (Object) null))
        this.SetSprite(this.rankingNumbers[index1].transform, this.SPR_RANKING_NUMBER[index2]);
    }
    this.beforeDigits = str.Length;
  }

  private void SetSpritePassNumber(int value, List<GameObject> numbers)
  {
    string str = value.ToString();
    for (int index1 = 0; index1 < str.Length; ++index1)
    {
      int index2 = int.Parse(str[index1].ToString());
      this.SetSprite(numbers[index1].transform, this.SPR_PASS_NUMBER[index2]);
    }
  }

  private IEnumerator WaitTiming(float waitTime)
  {
    yield return (object) new WaitForSeconds(waitTime);
  }

  private static float CountDownCube(float currentValue) => currentValue * (2f - currentValue);

  private void PlayAudio(CarnivalResultPoint.AUDIO type)
  {
    SoundManager.PlayOneShotUISE((int) type);
  }

  private void ChangeToRankingState()
  {
    if (this.currentData.pointRankingData.beforeRank < 0 || this.currentData.pointRankingData.afterRank < 0)
    {
      this.NextSection();
    }
    else
    {
      this.stateInitialized = false;
      this.skipRequest = false;
      this.pointResultState = CarnivalResultPoint.State.RANKING;
    }
  }

  private void OnQuery_SKIP()
  {
    this.skipRequest = true;
    GameSection.StopEvent();
  }

  private void OnQuery_OK_REWARD()
  {
    if (this.currentData.pointRankingData.isStartedBoost)
    {
      this.stateInitialized = false;
      this.skipRequest = false;
      this.pointResultState = CarnivalResultPoint.State.BONUS;
    }
    else
      this.ChangeToRankingState();
  }

  private void OnQuery_OK_END() => this.NextSection();

  private void NextSection()
  {
    switch (this.resultType)
    {
      case CarnivalResultPoint.RESULT_TYPE.NORMAL:
        GameSection.BackSection();
        break;
      case CarnivalResultPoint.RESULT_TYPE.RUSH:
        if (MonoBehaviourSingleton<InGameManager>.IsValid())
          MonoBehaviourSingleton<InGameManager>.I.SetResultedRush();
        if (MonoBehaviourSingleton<QuestManager>.I.compData == null)
        {
          GameSection.ChangeEvent("FAILED");
          break;
        }
        GameSection.ChangeEvent("NEXT");
        break;
      case CarnivalResultPoint.RESULT_TYPE.WAVE:
        if (MonoBehaviourSingleton<QuestManager>.I.compData == null)
        {
          GameSection.ChangeEvent("FAILED");
          break;
        }
        GameSection.ChangeEvent("NEXT");
        break;
      default:
        GameSection.BackSection();
        break;
    }
  }

  public enum UI
  {
    TXT_BANNER,
    OBJ_GET_REWARD_ROOT,
    LBL_GET_POINT,
    LBL_TOTAL_POINT,
    OBJ_NEXT_REWARD_ITEM_ICON_ROOT,
    BTN_SKIP_FULL_SCREEN,
    BTN_OK,
    SPR_LOGO,
    OBJ_REWARD_POS,
    OBJ_REWARD_ROOT,
    OBJ_RANKING_ROOT,
    OBJ_RANKING_ANIM_ROOT,
    OBJ_PASS_ANIM_ROOT,
    LBL_RANKING_TOTAL_POINT,
    LBL_PASS_NUM,
    BTN_END_OK,
    GRD_POSITION_PASS,
    SPR_POSITION_PASS,
    SPR_PASS_TEXT,
    GRD_COUNT_NUMBERS,
    OBJ_BONUS_ROOT,
    OBJ_BONUS_ANIM_ROOT,
    OBJ_CARNIVAL_POINT,
    GRD_POINT_DETAIL,
    LBL_POINT,
    LBL_POINT_NAME,
    GRD_NEXT_ITEM_ROOT,
    OBJ_NEXT_REWARD,
    STR_POINT_NEXT,
    GRD_ITEM_ROOT,
  }

  private enum State
  {
    START,
    TO_REWARD,
    REWARD,
    BONUS,
    RANKING,
  }

  private enum AUDIO
  {
    RESULT = 40000049, // 0x02625A31
    TITLE_LOGO = 40000227, // 0x02625AE3
    CATEGORY = 40000228, // 0x02625AE4
    POINTUP = 40000229, // 0x02625AE5
    POINTREWARD = 40000230, // 0x02625AE6
    START_BONUS_TIME = 40000268, // 0x02625B0C
  }

  private enum RESULT_TYPE
  {
    NORMAL,
    RUSH,
    WAVE,
  }
}
