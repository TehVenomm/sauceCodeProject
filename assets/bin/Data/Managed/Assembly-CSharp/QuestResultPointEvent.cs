// Decompiled with JetBrains decompiler
// Type: QuestResultPointEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestResultPointEvent : GameSection
{
  private QuestResultPointEvent.State pointResultState;
  private bool stateInitialized;
  private PointEventCurrentData currentData;
  private const string BOOST_BONUS_NAME = "ブーストポイント";
  private int boostPoint;
  private float boostRate;
  private QuestResultPointEvent.UI[] bonusNames = new QuestResultPointEvent.UI[3]
  {
    QuestResultPointEvent.UI.LBL_BONUS1_NAME,
    QuestResultPointEvent.UI.LBL_BONUS2_NAME,
    QuestResultPointEvent.UI.LBL_BONUS3_NAME
  };
  private QuestResultPointEvent.UI[] bonusPoints = new QuestResultPointEvent.UI[3]
  {
    QuestResultPointEvent.UI.LBL_BONUS1_POINT,
    QuestResultPointEvent.UI.LBL_BONUS2_POINT,
    QuestResultPointEvent.UI.LBL_BONUS3_POINT
  };
  private QuestResultPointEvent.UI[] bonusRoots = new QuestResultPointEvent.UI[3]
  {
    QuestResultPointEvent.UI.OBJ_BONUS1_ROOT,
    QuestResultPointEvent.UI.OBJ_BONUS2_ROOT,
    QuestResultPointEvent.UI.OBJ_BONUS3_ROOT
  };
  private QuestResultPointEvent.UI[] bonusLines = new QuestResultPointEvent.UI[3]
  {
    QuestResultPointEvent.UI.OBJ_LINE2,
    QuestResultPointEvent.UI.OBJ_LINE3,
    QuestResultPointEvent.UI.OBJ_LINE4
  };
  private const string TITLE_LOGO_NAME_FORMAT = "ef_ui_pointresult";
  private const float COUNT_ANIM_SPEED = 4f;
  private List<PointEventCurrentData.Reward> rewardList = new List<PointEventCurrentData.Reward>();
  private List<GameObject> rewardObjects = new List<GameObject>();
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
    this.currentData = GameSection.GetEventData() as PointEventCurrentData;
    this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_REWARD_ROOT, false);
    this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_RANKING_ROOT, false);
    this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_BONUS_ROOT, false);
    this.bannerCtrl = this.GetCtrl((Enum) QuestResultPointEvent.UI.TXT_BANNER);
    this.StartCoroutine(this.DoInitalize());
  }

  private IEnumerator DoInitalize()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_pointresult");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.SetVisibleWidgetEffect((Enum) QuestResultPointEvent.UI.TXT_BANNER, "ef_ui_pointresult");
    ((Component) this.bannerCtrl).GetComponent<UIVisibleWidgetEffect>().SetRendererQueue(4000);
    this.PlayAudio(QuestResultPointEvent.AUDIO.TITLE_LOGO);
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
    ResourceLoad.LoadEventBannerResultTexture(((Component) this.GetCtrl((Enum) QuestResultPointEvent.UI.SPR_LOGO)).GetComponent<UITexture>(), (uint) event_id);
    ResourceLoad.LoadEventBannerResultBGTexture(((Component) this.GetCtrl((Enum) QuestResultPointEvent.UI.SPR_LOGO_BG)).GetComponent<UITexture>(), (uint) event_id);
    TweenAlpha component = ((Component) this.GetCtrl((Enum) QuestResultPointEvent.UI.SPR_LOGO_BG)).GetComponent<TweenAlpha>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      component.ResetToBeginning();
      component.PlayForward();
    }
    base.Initialize();
  }

  private void Update()
  {
    switch (this.pointResultState)
    {
      case QuestResultPointEvent.State.START:
        Animation componentInChildren = ((Component) this.bannerCtrl).GetComponentInChildren<Animation>(true);
        if (Object.op_Equality((Object) componentInChildren, (Object) null) || componentInChildren.isPlaying)
          break;
        this.pointResultState = QuestResultPointEvent.State.TO_REWARD;
        break;
      case QuestResultPointEvent.State.TO_REWARD:
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
              this.bannerCtrl.position = this.GetCtrl((Enum) QuestResultPointEvent.UI.OBJ_REWARD_POS).position;
              this.bannerCtrl.localScale = this.GetCtrl((Enum) QuestResultPointEvent.UI.OBJ_REWARD_POS).localScale;
              this.SetActive((Enum) QuestResultPointEvent.UI.SPR_LOGO_BG, false);
              this.StartCoroutine(this.WaitTiming(2f));
              this.pointResultState = QuestResultPointEvent.State.REWARD;
              this.stateInitialized = false;
            }
            else
              num1 = 0;
          }
        }
        break;
      case QuestResultPointEvent.State.REWARD:
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
              this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_REWARD_ROOT, true);
              this.stateInitialized = true;
              this.SetRewardUI();
            }
            else
              num2 = 0;
          }
        }
        break;
      case QuestResultPointEvent.State.BONUS:
        if (this.stateInitialized)
          break;
        this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_REWARD_ROOT, false);
        this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_BONUS_ROOT, true);
        this.SetActive((Enum) QuestResultPointEvent.UI.TXT_BANNER, false);
        this.SetBonusUI();
        this.stateInitialized = true;
        break;
      case QuestResultPointEvent.State.RANKING:
        if (this.stateInitialized)
          break;
        this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_REWARD_ROOT, false);
        this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_BONUS_ROOT, false);
        this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_RANKING_ROOT, true);
        this.SetActive((Enum) QuestResultPointEvent.UI.TXT_BANNER, false);
        this.SetRankingUI();
        this.stateInitialized = true;
        break;
    }
  }

  private void SetRewardUI()
  {
    this.SetFullScreenButton((Enum) QuestResultPointEvent.UI.BTN_SKIP_FULL_SCREEN);
    this.SetActive((Enum) QuestResultPointEvent.UI.BTN_OK, false);
    this.InitTween((Enum) QuestResultPointEvent.UI.OBJ_GET_EXP_ROOT);
    this.InitTween((Enum) QuestResultPointEvent.UI.OBJ_TOTAL_EXP_ROOT);
    this.InitTween((Enum) QuestResultPointEvent.UI.OBJ_NEXT_REWARD_ROOT);
    this.InitTween((Enum) QuestResultPointEvent.UI.OBJ_GET_REWARD_ROOT);
    this.InitTween((Enum) QuestResultPointEvent.UI.OBJ_BONUS_TIME_ROOT);
    PointEventCurrentData.PointResultData pointRankingData = this.currentData.pointRankingData;
    if (MonoBehaviourSingleton<GuildRequestManager>.IsValid() && MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti)
    {
      foreach (Component component in this.GetCtrl((Enum) QuestResultPointEvent.UI.OBJ_POINT_DETAIL))
        component.gameObject.SetActive(false);
    }
    else
    {
      this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_QUEST_NAME, MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestName());
      ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, Singleton<EnemyTable>.I.GetEnemyData((uint) MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID(0)).iconId, new RARITY_TYPE?(), this.GetCtrl((Enum) QuestResultPointEvent.UI.OBJ_ENEMY));
    }
    int num1 = 0;
    int bonusIndex = 0;
    for (int index = 0; index < pointRankingData.bonusPoint.Count; ++index)
    {
      num1 += pointRankingData.bonusPoint[index].point;
      if (!MonoBehaviourSingleton<GuildRequestManager>.IsValid() || !MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti)
      {
        if (pointRankingData.bonusPoint[index].name != "ブーストポイント" && pointRankingData.bonusPoint[index].point > 0)
        {
          this.SetBonusPoint(bonusIndex, pointRankingData.bonusPoint[index].name, pointRankingData.bonusPoint[index].point);
          ++bonusIndex;
        }
        else
        {
          this.boostPoint = pointRankingData.bonusPoint[index].point;
          this.boostRate = pointRankingData.bonusPoint[index].boostRate;
        }
      }
    }
    MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti = false;
    // ISSUE: variable of a boxed type
    __Boxed<QuestResultPointEvent.UI> label_enum1 = (Enum) QuestResultPointEvent.UI.LBL_MONSTER_POINT;
    int num2 = pointRankingData.getPoint - num1;
    string text1 = "+" + num2.ToString("N0");
    this.SetLabelText((Enum) label_enum1, text1);
    this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_GET_POINT, pointRankingData.getPoint.ToString("N0"));
    // ISSUE: variable of a boxed type
    __Boxed<QuestResultPointEvent.UI> label_enum2 = (Enum) QuestResultPointEvent.UI.LBL_TOTAL_POINT;
    num2 = pointRankingData.userPoint + pointRankingData.getPoint;
    string text2 = num2.ToString("N0");
    this.SetLabelText((Enum) label_enum2, text2);
    if (pointRankingData.nextReward != null)
    {
      this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_NEXT_POINT, pointRankingData.nextReward.point.ToString());
      this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_NEXT_ITEM_NUM, pointRankingData.nextReward.reward[0].num.ToString("N0"));
    }
    this.StartCoroutine(this.GetPointAnimation());
  }

  private IEnumerator GetPointAnimation()
  {
    int getPoint = this.currentData.pointRankingData.getPoint;
    int userPoint = this.currentData.pointRankingData.userPoint;
    int totalPoint = userPoint + getPoint;
    bool wait = true;
    this.PlayAudio(QuestResultPointEvent.AUDIO.CATEGORY);
    wait = true;
    this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_GET_POINT, "0");
    this.PlayTween((Enum) QuestResultPointEvent.UI.OBJ_GET_EXP_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
    while (wait)
    {
      if (this.skipRequest)
      {
        this.SkipTween((Enum) QuestResultPointEvent.UI.OBJ_GET_EXP_ROOT);
        wait = false;
      }
      yield return (object) 0;
    }
    yield return (object) this.StartCoroutine(this.CountUpAnimation(0.0f, getPoint - this.boostPoint, QuestResultPointEvent.UI.LBL_GET_POINT));
    if (this.boostPoint > 0)
    {
      this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_BONUS_RATE, "x" + this.boostRate.ToString());
      wait = true;
      this.PlayTween((Enum) QuestResultPointEvent.UI.OBJ_BONUS_TIME_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
      while (wait)
      {
        if (this.skipRequest)
        {
          this.SkipTween((Enum) QuestResultPointEvent.UI.OBJ_BONUS_TIME_ROOT);
          wait = false;
        }
        yield return (object) 0;
      }
      this.PlayAudio(QuestResultPointEvent.AUDIO.RESULT);
      if (!this.skipRequest)
        yield return (object) this.StartCoroutine(this.WaitTiming(1.2f));
      yield return (object) this.StartCoroutine(this.CountUpAnimation((float) (getPoint - this.boostPoint), getPoint, QuestResultPointEvent.UI.LBL_GET_POINT));
    }
    else
      this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_BONUS_TIME_ROOT, false);
    this.PlayAudio(QuestResultPointEvent.AUDIO.CATEGORY);
    wait = true;
    this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_TOTAL_POINT, userPoint.ToString("N0"));
    this.PlayTween((Enum) QuestResultPointEvent.UI.OBJ_TOTAL_EXP_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
    while (wait)
    {
      if (this.skipRequest)
      {
        this.SkipTween((Enum) QuestResultPointEvent.UI.OBJ_TOTAL_EXP_ROOT);
        wait = false;
      }
      yield return (object) 0;
    }
    yield return (object) this.StartCoroutine(this.CountUpAnimation((float) userPoint, totalPoint, QuestResultPointEvent.UI.LBL_TOTAL_POINT));
    int num = 0;
    int i;
    if (this.currentData.pointRankingData.getReward.Count > 0)
    {
      PointEventCurrentData.PointRewardData currentNextData;
      for (int index1 = 0; index1 < this.currentData.pointRankingData.getReward.Count; ++index1)
      {
        currentNextData = this.currentData.pointRankingData.getReward[index1];
        for (int index2 = 0; index2 < currentNextData.reward.Count; ++index2)
          this.rewardList.Add(currentNextData.reward[index2]);
      }
      if (this.rewardList.Count > 0)
      {
        this.SetAllRewardItem(this.rewardList);
        for (int index = 0; index < this.rewardObjects.Count; ++index)
          this.rewardObjects[index].SetActive(false);
      }
      for (i = 0; i < this.currentData.pointRankingData.getReward.Count; ++i)
      {
        currentNextData = this.currentData.pointRankingData.getReward[i];
        this.SetNextItemIcon(currentNextData.reward);
        wait = true;
        this.ResetTween((Enum) QuestResultPointEvent.UI.OBJ_NEXT_REWARD_ROOT);
        int currentPoint = currentNextData.point - num;
        this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_NEXT_POINT, currentPoint.ToString("N0"));
        this.PlayTween((Enum) QuestResultPointEvent.UI.OBJ_NEXT_REWARD_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
        if (!this.skipRequest)
          this.PlayAudio(QuestResultPointEvent.AUDIO.CATEGORY);
        while (wait)
        {
          if (this.skipRequest)
          {
            this.SkipTween((Enum) QuestResultPointEvent.UI.OBJ_NEXT_REWARD_ROOT);
            wait = false;
          }
          yield return (object) 0;
        }
        yield return (object) this.StartCoroutine(this.CountDownAnimation((float) currentPoint, 0, QuestResultPointEvent.UI.LBL_NEXT_POINT));
        if (!this.skipRequest)
          yield return (object) this.StartCoroutine(this.WaitTiming(0.5f));
        num = currentNextData.point;
      }
      currentNextData = (PointEventCurrentData.PointRewardData) null;
    }
    if (this.currentData.pointRankingData.nextReward != null)
    {
      this.SetNextItemIcon(this.currentData.pointRankingData.nextReward.reward);
      wait = true;
      this.ResetTween((Enum) QuestResultPointEvent.UI.OBJ_NEXT_REWARD_ROOT);
      i = this.currentData.pointRankingData.nextReward.point - num;
      this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_NEXT_POINT, i.ToString("N0"));
      this.PlayTween((Enum) QuestResultPointEvent.UI.OBJ_NEXT_REWARD_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
      this.PlayAudio(QuestResultPointEvent.AUDIO.CATEGORY);
      while (wait)
      {
        if (this.skipRequest)
        {
          this.SkipTween((Enum) QuestResultPointEvent.UI.OBJ_NEXT_REWARD_ROOT);
          wait = false;
        }
        yield return (object) 0;
      }
      int targetPoint = this.currentData.pointRankingData.nextReward.point - totalPoint;
      yield return (object) this.StartCoroutine(this.CountDownAnimation((float) i, targetPoint, QuestResultPointEvent.UI.LBL_NEXT_POINT));
    }
    else
      this.SetActive((Enum) QuestResultPointEvent.UI.OBJ_NEXT_REWARD_ROOT, false);
    if (this.rewardObjects.Count > 0)
    {
      wait = true;
      this.ResetTween((Enum) QuestResultPointEvent.UI.OBJ_GET_REWARD_ROOT);
      this.PlayTween((Enum) QuestResultPointEvent.UI.OBJ_GET_REWARD_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
      this.PlayAudio(QuestResultPointEvent.AUDIO.POINTREWARD);
      while (wait)
      {
        if (this.skipRequest)
        {
          this.SkipTween((Enum) QuestResultPointEvent.UI.OBJ_GET_REWARD_ROOT);
          wait = false;
        }
        yield return (object) 0;
      }
      for (int index = 0; index < this.rewardObjects.Count; ++index)
      {
        this.rewardObjects[index].SetActive(true);
        TweenAlpha component = this.rewardObjects[index].GetComponent<TweenAlpha>();
        if (!Object.op_Equality((Object) component, (Object) null))
        {
          component.ResetToBeginning();
          component.PlayForward();
        }
      }
    }
    this.SetActive((Enum) QuestResultPointEvent.UI.BTN_OK, true);
    this.SetActive((Enum) QuestResultPointEvent.UI.BTN_SKIP_FULL_SCREEN, false);
  }

  private void SetNextItemIcon(List<PointEventCurrentData.Reward> reward)
  {
    this.SetDynamicList((Enum) QuestResultPointEvent.UI.OBJ_NEXT_REWARD_ITEM_ICON_ROOT, "ItemIcon", reward.Count, true, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      PointEventCurrentData.Reward reward1 = reward[i];
      ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward1.type, (uint) reward1.itemId, t, reward1.num);
    }));
  }

  private void SetAllRewardItem(List<PointEventCurrentData.Reward> rewardList)
  {
    Transform ctrl = this.GetCtrl((Enum) QuestResultPointEvent.UI.OBJ_ITEM_ROOT);
    for (int index = 0; index < rewardList.Count; ++index)
    {
      if (index == 0)
      {
        ((Component) Utility.FindChild(ctrl, "itemNum")).GetComponent<UILabel>().text = "×" + (object) rewardList[index].num;
        this.rewardObjects.Add(((Component) ctrl).gameObject);
      }
      else
      {
        GameObject gameObject = Object.Instantiate<GameObject>(((Component) ctrl).gameObject);
        gameObject.transform.parent = ctrl.parent;
        gameObject.transform.localPosition = ctrl.localPosition;
        gameObject.transform.localScale = ctrl.localScale;
        ((Component) Utility.FindChild(gameObject.transform, "itemNum")).GetComponent<UILabel>().text = "×" + (object) rewardList[index].num;
        this.rewardObjects.Add(gameObject);
      }
    }
    this.SetDynamicList((Enum) QuestResultPointEvent.UI.GRD_ANIM_ITEM_ROOT, "ItemIcon", rewardList.Count, true, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      PointEventCurrentData.Reward reward = rewardList[i];
      ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward.type, (uint) reward.itemId, t.parent, reward.num);
      if (!Object.op_Inequality((Object) rewardItemIcon, (Object) null))
        return;
      rewardItemIcon.SetEnableCollider(false);
    }));
  }

  private void SetBonusPoint(int bonusIndex, string bonusName, int point)
  {
    if (bonusIndex > 2)
      return;
    this.SetLabelText((Enum) this.bonusNames[bonusIndex], bonusName);
    this.SetLabelText((Enum) this.bonusPoints[bonusIndex], "+" + point.ToString("N0"));
    this.SetActive((Enum) this.bonusRoots[bonusIndex], true);
    this.SetActive((Enum) this.bonusLines[bonusIndex], true);
  }

  private IEnumerator CountUpAnimation(
    float currentPoint,
    int targetPoint,
    QuestResultPointEvent.UI targetUI)
  {
    while ((double) currentPoint < (double) targetPoint)
    {
      yield return (object) 0;
      if (this.skipRequest)
        currentPoint = (float) targetPoint;
      int num1 = Mathf.FloorToInt(currentPoint);
      currentPoint += Mathf.Max(((float) targetPoint - currentPoint) * QuestResultPointEvent.CountDownCube(Time.deltaTime * 4f), 1f);
      currentPoint = Mathf.Min(currentPoint, (float) targetPoint);
      int num2 = Mathf.FloorToInt(currentPoint);
      if (num1 < num2)
        this.PlayAudio(QuestResultPointEvent.AUDIO.POINTUP);
      this.SetLabelText((Enum) targetUI, Mathf.FloorToInt(currentPoint).ToString("N0"));
    }
  }

  private IEnumerator CountDownAnimation(
    float currentPoint,
    int targetPoint,
    QuestResultPointEvent.UI targetUI)
  {
    while ((double) currentPoint > (double) targetPoint)
    {
      yield return (object) 0;
      if (this.skipRequest)
        currentPoint = (float) targetPoint;
      int num1 = Mathf.FloorToInt(currentPoint);
      currentPoint += Mathf.Min(((float) targetPoint - currentPoint) * QuestResultPointEvent.CountDownCube(Time.deltaTime * 4f), -1f);
      currentPoint = Mathf.Max(currentPoint, (float) targetPoint);
      int num2 = Mathf.FloorToInt(currentPoint);
      if (num1 > num2)
        this.PlayAudio(QuestResultPointEvent.AUDIO.POINTUP);
      this.SetLabelText((Enum) targetUI, Mathf.CeilToInt(currentPoint).ToString("N0"));
    }
  }

  private void SetBonusUI()
  {
    this.InitTween((Enum) QuestResultPointEvent.UI.OBJ_BONUS_ANIM_ROOT);
    this.StartCoroutine(this.StartBonusAnimation());
  }

  private IEnumerator StartBonusAnimation()
  {
    SoundManager.PlayOneshotJingle(40000268);
    this.PlayTween((Enum) QuestResultPointEvent.UI.OBJ_BONUS_ANIM_ROOT);
    yield return (object) this.StartCoroutine(this.WaitTiming(2.8f));
    this.ChangeToRankingState();
  }

  private void SetRankingUI()
  {
    this.SetActive((Enum) QuestResultPointEvent.UI.BTN_SKIP_FULL_SCREEN, true);
    PointEventCurrentData.PointResultData pointRankingData = this.currentData.pointRankingData;
    this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_RANKING_TOTAL_POINT, (pointRankingData.userPoint + pointRankingData.getPoint).ToString("N0"));
    this.SetLabelText((Enum) QuestResultPointEvent.UI.LBL_PASS_NUM, Mathf.Max(0, pointRankingData.beforeRank - pointRankingData.afterRank).ToString("N0") + "人抜き");
    int num1 = Mathf.Min(999999, pointRankingData.beforeRank);
    int num2 = Mathf.Min(999999, pointRankingData.afterRank);
    int num3 = num1 <= num2 ? num2.ToString().Length : num1.ToString().Length;
    this.rankingNumbers = new List<GameObject>(6);
    Transform ctrl = this.GetCtrl((Enum) QuestResultPointEvent.UI.GRD_COUNT_NUMBERS);
    for (int index = 0; index < 6; ++index)
    {
      Transform child = Utility.FindChild(ctrl, "Number" + (object) index);
      if (!Object.op_Equality((Object) child, (Object) null))
      {
        if (index >= num3)
        {
          child.parent = (Transform) null;
          Object.Destroy((Object) ((Component) child).gameObject);
        }
        else
          this.rankingNumbers.Add(((Component) child).gameObject);
      }
    }
    ((Component) ctrl).GetComponent<UIGrid>().Reposition();
    if (num1 > num2)
      this.SetSpriteNumber(num1);
    else
      this.SetSpriteNumber(num2);
    this.InitTween((Enum) QuestResultPointEvent.UI.OBJ_RANKING_ANIM_ROOT);
    this.StartCoroutine(this.StartRankingAnimation());
  }

  private IEnumerator StartRankingAnimation()
  {
    this.PlayAudio(QuestResultPointEvent.AUDIO.CATEGORY);
    bool wait = true;
    this.PlayTween((Enum) QuestResultPointEvent.UI.OBJ_RANKING_ANIM_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
    while (wait)
    {
      if (this.skipRequest)
      {
        this.SkipTween((Enum) QuestResultPointEvent.UI.OBJ_RANKING_ROOT);
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
      Transform ctrl1 = this.GetCtrl((Enum) QuestResultPointEvent.UI.SPR_POSITION_PASS);
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
      ((Component) this.GetCtrl((Enum) QuestResultPointEvent.UI.GRD_POSITION_PASS)).GetComponent<UIGrid>().Reposition();
      Transform ctrl2 = this.GetCtrl((Enum) QuestResultPointEvent.UI.SPR_PASS_TEXT);
      Vector3 localPosition = this.passPositionNumbers[this.passPositionNumbers.Count - 1].transform.localPosition;
      ((Component) ctrl2).GetComponent<TweenPosition>().to.x = localPosition.x + 41f;
      this.InitTween((Enum) QuestResultPointEvent.UI.OBJ_PASS_ANIM_ROOT);
      if (!this.skipRequest)
        yield return (object) this.StartCoroutine(this.WaitTiming(0.6f));
      this.PlayAudio(QuestResultPointEvent.AUDIO.RESULT);
      wait = true;
      this.PlayTween((Enum) QuestResultPointEvent.UI.OBJ_PASS_ANIM_ROOT, callback: (EventDelegate.Callback) (() => wait = false));
      while (wait)
      {
        if (this.skipRequest)
        {
          this.SkipTween((Enum) QuestResultPointEvent.UI.OBJ_PASS_ANIM_ROOT);
          wait = false;
        }
        yield return (object) 0;
      }
    }
    this.SetActive((Enum) QuestResultPointEvent.UI.BTN_SKIP_FULL_SCREEN, false);
    this.SetActive((Enum) QuestResultPointEvent.UI.BTN_END_OK, true);
  }

  private IEnumerator CountSpriteAnimation(float currentRank, int targetRank)
  {
    while ((double) currentRank > (double) targetRank)
    {
      yield return (object) 0;
      if (this.skipRequest)
        currentRank = (float) targetRank;
      int num1 = Mathf.FloorToInt(currentRank);
      currentRank += Mathf.Min(((float) targetRank - currentRank) * QuestResultPointEvent.CountDownCube(Time.deltaTime * 4f), -1f);
      currentRank = Mathf.Max(currentRank, (float) targetRank);
      int num2 = Mathf.FloorToInt(currentRank);
      if (num1 > num2)
        this.PlayAudio(QuestResultPointEvent.AUDIO.POINTUP);
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

  private void PlayAudio(QuestResultPointEvent.AUDIO type)
  {
    SoundManager.PlayOneShotUISE((int) type);
  }

  private void ChangeToRankingState()
  {
    if (this.currentData.pointRankingData.beforeRank < 0 || this.currentData.pointRankingData.afterRank < 0)
    {
      GameSection.BackSection();
    }
    else
    {
      this.stateInitialized = false;
      this.skipRequest = false;
      this.pointResultState = QuestResultPointEvent.State.RANKING;
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
      this.pointResultState = QuestResultPointEvent.State.BONUS;
    }
    else
      this.ChangeToRankingState();
  }

  private void OnQuery_OK_END() => GameSection.BackSection();

  public enum UI
  {
    TXT_BANNER,
    OBJ_GET_EXP_ROOT,
    OBJ_TOTAL_EXP_ROOT,
    OBJ_NEXT_REWARD_ROOT,
    OBJ_GET_REWARD_ROOT,
    LBL_GET_POINT,
    LBL_TOTAL_POINT,
    LBL_NEXT_POINT,
    OBJ_NEXT_REWARD_ITEM_ICON_ROOT,
    GRD_ANIM_ITEM_ROOT,
    LBL_NEXT_ITEM_NUM,
    BTN_SKIP_FULL_SCREEN,
    BTN_OK,
    SPR_LOGO,
    SPR_LOGO_BG,
    OBJ_REWARD_POS,
    OBJ_REWARD_ROOT,
    OBJ_RANKING_ROOT,
    OBJ_ENEMY,
    LBL_QUEST_NAME,
    LBL_MONSTER_POINT,
    OBJ_BONUS1_ROOT,
    LBL_BONUS1_NAME,
    LBL_BONUS1_POINT,
    OBJ_BONUS2_ROOT,
    LBL_BONUS2_NAME,
    LBL_BONUS2_POINT,
    OBJ_BONUS3_ROOT,
    LBL_BONUS3_NAME,
    LBL_BONUS3_POINT,
    OBJ_LINE1,
    OBJ_LINE2,
    OBJ_LINE3,
    OBJ_LINE4,
    OBJ_ITEM_ROOT,
    OBJ_RANKING_ANIM_ROOT,
    OBJ_PASS_ANIM_ROOT,
    LBL_RANKING_TOTAL_POINT,
    LBL_PASS_NUM,
    BTN_END_OK,
    GRD_POSITION_PASS,
    SPR_POSITION_PASS,
    SPR_PASS_TEXT,
    GRD_COUNT_NUMBERS,
    OBJ_BONUS_TIME_ROOT,
    LBL_BONUS_RATE,
    OBJ_BONUS_ROOT,
    OBJ_BONUS_ANIM_ROOT,
    OBJ_POINT_DETAIL,
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
}
