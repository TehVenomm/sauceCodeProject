// Decompiled with JetBrains decompiler
// Type: ArenaResultTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ArenaResultTop : QuestResultTop
{
  private const float COUNT_ANIM_SPEED = 4f;
  private bool is_skip;
  private ResultReward[] resultRewards;
  private PointEventCurrentData allPointEvents;
  private ARENA_RANK m_rank;
  private ARENA_RANK m_nextRank;
  private ARENA_GROUP m_group;
  private bool m_isTimeAttack;
  private ArenaResultTop.RESULT_ANIM_STATE animState;

  public override void Initialize()
  {
    this.m_rank = MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaRank();
    this.m_group = MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaGroup();
    this.m_isTimeAttack = MonoBehaviourSingleton<InGameManager>.I.IsArenaTimeAttack();
    base.Initialize();
    this.m_nextRank = this.m_rank + 1;
    if (this.m_nextRank > ARENA_RANK.SSS)
      this.m_nextRank = ARENA_RANK.NONE;
    ResourceLoad.LoadWithSetUITexture(((Component) this.GetCtrl((Enum) ArenaResultTop.UI.TEX_RANK_PRE)).GetComponent<UITexture>(), RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(this.m_rank));
    ResourceLoad.LoadWithSetUITexture(((Component) this.GetCtrl((Enum) ArenaResultTop.UI.TEX_RANK_NEW)).GetComponent<UITexture>(), RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(this.m_rank + 1));
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
    MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
  }

  protected override void InitReward()
  {
    List<ResultReward> resultRewardList = new List<ResultReward>();
    this.dropItemNum = 0;
    this.dropLineNum = 0;
    this.eventRewardTitles = new List<string>();
    if (MonoBehaviourSingleton<InGameManager>.I.arenaRewards.Count > 0)
    {
      foreach (QuestCompleteRewardList arenaReward in MonoBehaviourSingleton<InGameManager>.I.arenaRewards)
      {
        ResultReward resultReward = new ResultReward();
        this.DevideRewardDropAndEvent(resultReward, arenaReward.drop);
        List<SortCompareData> drop_ary = new List<SortCompareData>();
        int start_ary_index1 = 0;
        int start_ary_index2 = ResultUtility.SetDropData(drop_ary, start_ary_index1, resultReward.dropReward.item);
        int start_ary_index3 = ResultUtility.SetDropData(drop_ary, start_ary_index2, resultReward.dropReward.equipItem);
        int start_ary_index4 = ResultUtility.SetDropData(drop_ary, start_ary_index3, resultReward.dropReward.skillItem);
        int start_ary_index5 = ResultUtility.SetDropData(drop_ary, start_ary_index4, resultReward.dropReward.questItem);
        ResultUtility.SetDropData(drop_ary, start_ary_index5, resultReward.dropReward.accessoryItem);
        drop_ary.Sort((Comparison<SortCompareData>) ((l, r) => r.GetSortValueQuestResult() - l.GetSortValueQuestResult()));
        resultReward.dropItemIconData = drop_ary.ToArray();
        this.dropItemNum += resultReward.dropItemIconData.Length;
        resultRewardList.Add(resultReward);
      }
    }
    this.pointShopResultData = MonoBehaviourSingleton<InGameManager>.I.arenaPointShops ?? new List<PointShopResultData>();
    this.resultRewards = resultRewardList.ToArray();
  }

  protected override void OnClose()
  {
    try
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && !MonoBehaviourSingleton<InGameManager>.I.isRetry)
        MonoBehaviourSingleton<InGameManager>.I.ClearArenaInfo();
      base.OnClose();
    }
    catch (Exception ex)
    {
      Log.Warning(LOG.UI, "ArenaResultTop OnClose\n{0}\n{1}", (object) ex.Message, (object) ex.StackTrace);
    }
  }

  public override void UpdateUI()
  {
    this.allPointEvents = new PointEventCurrentData();
    this.allPointEvents.pointRankingData = new PointEventCurrentData.PointResultData();
    this.isVictory = MonoBehaviourSingleton<QuestManager>.I.arenaCompData != null;
    this.SetFullScreenButton((Enum) ArenaResultTop.UI.BTN_SKIP_FULL_SCREEN);
    this.SetActive((Enum) ArenaResultTop.UI.BTN_NEXT, false);
    this.SetActive((Enum) ArenaResultTop.UI.BTN_RETRY, false);
    this.SetActive((Enum) ArenaResultTop.UI.OBJ_TIME, false);
    this.SetActive((Enum) ArenaResultTop.UI.OBJ_CLEAR_EFFECT_ROOT, false);
    this.SetActive((Enum) ArenaResultTop.UI.OBJ_CLEAR_EFFECT, false);
    this.SetActive((Enum) ArenaResultTop.UI.OBJ_RANK_UP_ROOT, false);
    this.SetActive((Enum) ArenaResultTop.UI.OBJ_CONGRATULATIONS_ROOT, false);
    if (this.m_isTimeAttack)
    {
      this.SetActive((Enum) ArenaResultTop.UI.OBJ_REMAIN_TIME, false);
      if (this.isVictory)
        this.SetActive((Enum) ArenaResultTop.UI.OBJ_TIME, true);
    }
    this.SetLabelText((Enum) ArenaResultTop.UI.LBL_QUEST_NAME, $"{string.Format(StringTable.Get(STRING_CATEGORY.ARENA, 1U), (object) this.m_rank.ToString())} {string.Format(StringTable.Get(STRING_CATEGORY.ARENA, 0U), (object) this.m_group.ToString())}");
    List<QuestCompleteRewardList> arenaRewards = MonoBehaviourSingleton<InGameManager>.I.arenaRewards;
    int num1 = 0;
    int num2 = 0;
    for (int index = 0; index < arenaRewards.Count; ++index)
    {
      QuestCompleteRewardList completeRewardList = arenaRewards[index];
      QuestCompleteReward drop = completeRewardList.drop;
      QuestCompleteReward breakReward = completeRewardList.breakReward;
      QuestCompleteReward order = completeRewardList.order;
      num1 += drop.exp + breakReward.exp + order.exp;
      num2 += drop.money + breakReward.money + order.money;
    }
    int my_user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    if (MonoBehaviourSingleton<InGameRecorder>.I.players.Find((Predicate<InGameRecorder.PlayerRecord>) (data => data.charaInfo.userId == my_user_id)).beforeLevel >= Singleton<UserLevelTable>.I.GetMaxLevel())
      num1 = 0;
    this.SetLabelText((Enum) ArenaResultTop.UI.LBL_EXP, num1.ToString("N0"));
    this.SetLabelText((Enum) ArenaResultTop.UI.LBL_REWARD_GOLD, num2.ToString("N0"));
    this.SetLabelText((Enum) ArenaResultTop.UI.LBL_TIME, MonoBehaviourSingleton<InGameRecorder>.I.arenaRemainTimeToString);
    this.SetLabelText((Enum) ArenaResultTop.UI.LBL_CLEAR_TIME, InGameProgress.GetTimeWithMilliSecToString(0.0f));
    this.SetActive((Enum) ArenaResultTop.UI.SPR_BESTSCORE, false);
    if (this.isVictory)
      this.SetLabelText((Enum) ArenaResultTop.UI.LBL_BEFORE_TIME, InGameProgress.GetTimeWithMilliSecToString((float) (int) MonoBehaviourSingleton<QuestManager>.I.arenaCompData.previousClearMilliSec * (1f / 1000f)));
    bool is_visible = this.pointShopResultData.Count > 0;
    this.SetActive((Enum) ArenaResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT, is_visible);
    if (is_visible)
      this.SetGrid((Enum) ArenaResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT, "QuestResultPointShop", this.pointShopResultData.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
      {
        this.ResetTween(t);
        PointShopResultData pointShopResultData = this.pointShopResultData[i];
        this.SetActive(t, (Enum) ArenaResultTop.UI.OBJ_NORMAL_POINT_SHOP_ROOT, !pointShopResultData.isEvent);
        if (!pointShopResultData.isEvent)
        {
          this.SetLabelText(t, (Enum) ArenaResultTop.UI.LBL_NORMAL_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.getPoint));
          this.SetLabelText(t, (Enum) ArenaResultTop.UI.LBL_NORMAL_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.totalPoint));
          ResourceLoad.LoadPointIconImageTexture(((Component) this.FindCtrl(t, (Enum) ArenaResultTop.UI.TEX_NORMAL_POINT_SHOP_ICON)).GetComponent<UITexture>(), (uint) pointShopResultData.pointShopId);
        }
        this.SetActive(t, (Enum) ArenaResultTop.UI.OBJ_EVENT_POINT_SHOP_ROOT, pointShopResultData.isEvent);
        if (!pointShopResultData.isEvent)
          return;
        this.SetLabelText(t, (Enum) ArenaResultTop.UI.LBL_EVENT_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.getPoint));
        this.SetLabelText(t, (Enum) ArenaResultTop.UI.LBL_EVENT_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) pointShopResultData.totalPoint));
        ResourceLoad.LoadPointIconImageTexture(((Component) this.FindCtrl(t, (Enum) ArenaResultTop.UI.TEX_EVENT_POINT_SHOP_ICON)).GetComponent<UITexture>(), (uint) pointShopResultData.pointShopId);
      }));
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
    {
      UIVirtualScreen componentInChildren = ((Component) this).GetComponentInChildren<UIVirtualScreen>();
      UIWidget component = ((Component) this.GetCtrl((Enum) ArenaResultTop.UI.SHADOW)).GetComponent<UIWidget>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null) && Object.op_Inequality((Object) component, (Object) null))
      {
        component.width = (int) componentInChildren.ScreenWidthFull;
        component.height = (int) componentInChildren.ScreenHeightFull;
      }
    }
    this.StartCoroutine(this.PlayAnimation());
  }

  private void PlayAudio(ArenaResultTop.AUDIO type)
  {
    int se_id = (int) type;
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    SoundManager.PlayOneShotUISE(se_id);
  }

  private IEnumerator PlayAnimation()
  {
    this.is_skip = false;
    this.animState = ArenaResultTop.RESULT_ANIM_STATE.TITLE;
    this.PlayAudio(ArenaResultTop.AUDIO.ADVENT);
    this.PlayTween((Enum) ArenaResultTop.UI.OBJ_TITLE, callback: (EventDelegate.Callback) (() => this.animState = ArenaResultTop.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    while (this.animState != ArenaResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.animState = ArenaResultTop.RESULT_ANIM_STATE.DROP;
    this.PlayAudio(ArenaResultTop.AUDIO.ACHIEVEMENT);
    if (this.pointShopResultData.Count > 0)
    {
      foreach (Transform t in ((Component) this.GetCtrl((Enum) ArenaResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT)).transform)
        this.PlayTween(t);
    }
    this.PlayTween((Enum) ArenaResultTop.UI.OBJ_EXP);
    this.PlayTween((Enum) ArenaResultTop.UI.OBJ_MONEY, callback: (EventDelegate.Callback) (() => this.animState = ArenaResultTop.RESULT_ANIM_STATE.IDLE), is_input_block: false);
    while (this.animState != ArenaResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    if (!this.m_isTimeAttack)
    {
      this.animState = ArenaResultTop.RESULT_ANIM_STATE.REMAIN_TIME;
      this.PlayTween((Enum) ArenaResultTop.UI.OBJ_REMAIN_TIME, callback: (EventDelegate.Callback) (() =>
      {
        SoundManager.PlayOneShotUISE(40000228);
        this.animState = ArenaResultTop.RESULT_ANIM_STATE.IDLE;
      }), is_input_block: false);
      while (this.animState != ArenaResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
        yield return (object) null;
      if (this.isVictory)
      {
        if (this.m_nextRank == ARENA_RANK.NONE)
        {
          this.SetActive((Enum) ArenaResultTop.UI.OBJ_CONGRATULATIONS_ROOT, true);
          this.ResetTween((Enum) ArenaResultTop.UI.OBJ_CONGRATULATIONS);
          this.animState = ArenaResultTop.RESULT_ANIM_STATE.CLEAR_EFFECT;
          ((Renderer) ((Component) ((Component) this.GetCtrl((Enum) ArenaResultTop.UI.OBJ_CONGRATULATIONS_PARTICLE)).GetComponent<ParticleSystem>()).GetComponent<ParticleSystemRenderer>()).sharedMaterial.renderQueue = 4000;
          yield return (object) null;
          this.PlayAudio(ArenaResultTop.AUDIO.ARRIVAL);
          this.PlayTween((Enum) ArenaResultTop.UI.OBJ_CONGRATULATIONS, callback: (EventDelegate.Callback) (() => this.animState = ArenaResultTop.RESULT_ANIM_STATE.IDLE));
          while (this.animState != ArenaResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
            yield return (object) null;
        }
        else
        {
          this.SetActive((Enum) ArenaResultTop.UI.OBJ_RANK_UP_ROOT, true);
          this.ResetTween((Enum) ArenaResultTop.UI.OBJ_RANK_UP);
          this.animState = ArenaResultTop.RESULT_ANIM_STATE.CLEAR_EFFECT;
          ((Renderer) ((Component) ((Component) this.GetCtrl((Enum) ArenaResultTop.UI.OBJ_PARTICLE)).GetComponent<ParticleSystem>()).GetComponent<ParticleSystemRenderer>()).sharedMaterial.renderQueue = 4000;
          yield return (object) null;
          this.PlayAudio(ArenaResultTop.AUDIO.ARRIVAL);
          this.PlayTween((Enum) ArenaResultTop.UI.OBJ_RANK_UP, callback: (EventDelegate.Callback) (() => this.animState = ArenaResultTop.RESULT_ANIM_STATE.IDLE));
          while (this.animState != ArenaResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
            yield return (object) null;
        }
      }
    }
    if (this.m_isTimeAttack)
    {
      this.animState = ArenaResultTop.RESULT_ANIM_STATE.CLEAR_TIME_COUNT_UP;
      this.StartCoroutine(this.PlayCountUpClearTimeAnim(MonoBehaviourSingleton<InGameRecorder>.I.arenaElapsedTime, (System.Action) (() => this.animState = ArenaResultTop.RESULT_ANIM_STATE.IDLE)));
      while (this.animState != ArenaResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
        yield return (object) null;
      if (this.IsBreakRecord())
      {
        this.animState = ArenaResultTop.RESULT_ANIM_STATE.BEST_SCORE;
        this.PlayAudio(ArenaResultTop.AUDIO.ARRIVAL);
        this.SetActive((Enum) ArenaResultTop.UI.SPR_BESTSCORE, true);
        this.PlayTween((Enum) ArenaResultTop.UI.SPR_BESTSCORE, callback: (EventDelegate.Callback) (() => this.animState = ArenaResultTop.RESULT_ANIM_STATE.IDLE));
        while (this.animState != ArenaResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
          yield return (object) null;
      }
    }
    this.animState = ArenaResultTop.RESULT_ANIM_STATE.EVENT;
    this.OpenAllEventRewardDialog((System.Action) (() => this.animState = ArenaResultTop.RESULT_ANIM_STATE.IDLE));
    while (this.animState != ArenaResultTop.RESULT_ANIM_STATE.IDLE && !this.is_skip)
      yield return (object) null;
    this.animState = ArenaResultTop.RESULT_ANIM_STATE.END;
    this.VisibleEndButton();
  }

  private IEnumerator GetPointAnimation(System.Action callback)
  {
    int getPoint = this.allPointEvents.pointRankingData.getPoint;
    int userPoint = this.allPointEvents.pointRankingData.userPoint;
    yield return (object) null;
    callback();
  }

  private IEnumerator PlayCountUpClearTimeAnim(float targetTime, System.Action callBack)
  {
    float currentShowTime = 0.0f;
    while ((double) currentShowTime < (double) targetTime)
    {
      yield return (object) null;
      if (this.is_skip)
        currentShowTime = targetTime;
      int num1 = Mathf.FloorToInt(currentShowTime);
      currentShowTime += Mathf.Max((targetTime - currentShowTime) * this.CountDownCube(Time.deltaTime * 4f), 1f);
      currentShowTime = Mathf.Min(currentShowTime, targetTime);
      int num2 = Mathf.FloorToInt(currentShowTime);
      if (num1 < num2)
        SoundManager.PlayOneShotUISE(40000012);
      this.SetLabelText((Enum) ArenaResultTop.UI.LBL_CLEAR_TIME, InGameProgress.GetTimeWithMilliSecToString(currentShowTime));
    }
    if (callBack != null)
      callBack();
  }

  private float CountDownCube(float currentValue) => currentValue * (2f - currentValue);

  protected override void VisibleEndButton()
  {
    this.SetActive((Enum) ArenaResultTop.UI.BTN_NEXT, this.animState == ArenaResultTop.RESULT_ANIM_STATE.END);
    this.SetActive((Enum) ArenaResultTop.UI.BTN_SKIP_FULL_SCREEN, this.animState != ArenaResultTop.RESULT_ANIM_STATE.END);
    if (this.m_isTimeAttack || MonoBehaviourSingleton<InGameRecorder>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE)
    {
      this.SetActive((Enum) ArenaResultTop.UI.BTN_RETRY, this.animState == ArenaResultTop.RESULT_ANIM_STATE.END);
    }
    else
    {
      this.SetActive((Enum) ArenaResultTop.UI.BTN_RETRY, false);
      Vector3 localPosition = this.GetCtrl((Enum) ArenaResultTop.UI.BTN_NEXT).localPosition;
      this.GetCtrl((Enum) ArenaResultTop.UI.BTN_NEXT).localPosition = new Vector3(0.0f, localPosition.y, localPosition.x);
    }
  }

  private bool IsBreakRecord()
  {
    return MonoBehaviourSingleton<InGameRecorder>.IsValid() && MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.arenaCompData != null && Mathf.FloorToInt(MonoBehaviourSingleton<InGameRecorder>.I.arenaElapsedTime * 1000f) < (int) MonoBehaviourSingleton<QuestManager>.I.arenaCompData.previousClearMilliSec;
  }

  private void DevideRewardDropAndEvent(ResultReward resultReward, QuestCompleteReward reward)
  {
    resultReward.dropReward = new QuestCompleteReward();
    resultReward.eventReward = new QuestCompleteReward();
    List<string> stringList = new List<string>();
    resultReward.dropReward.exp = reward.exp;
    int num1 = 0;
    int num2 = 0;
    for (int index = 0; index < reward.eventPrice.Count; ++index)
    {
      num1 += reward.eventPrice[index].gold;
      num2 += reward.eventPrice[index].gold;
      resultReward.eventReward.eventPrice.Add(reward.eventPrice[index]);
      stringList.Add(reward.eventPrice[index].rewardTitle);
    }
    resultReward.dropReward.money = Mathf.Max(0, reward.money - num1);
    resultReward.dropReward.crystal = Mathf.Max(0, reward.crystal - num2);
    for (int index = 0; index < reward.item.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.item[index].rewardTitle))
      {
        resultReward.dropReward.item.Add(reward.item[index]);
      }
      else
      {
        resultReward.eventReward.item.Add(reward.item[index]);
        stringList.Add(reward.item[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.skillItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.skillItem[index].rewardTitle))
      {
        resultReward.dropReward.skillItem.Add(reward.skillItem[index]);
      }
      else
      {
        resultReward.eventReward.skillItem.Add(reward.skillItem[index]);
        stringList.Add(reward.skillItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.equipItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.equipItem[index].rewardTitle))
      {
        resultReward.dropReward.equipItem.Add(reward.equipItem[index]);
      }
      else
      {
        resultReward.eventReward.equipItem.Add(reward.equipItem[index]);
        stringList.Add(reward.equipItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.questItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.questItem[index].rewardTitle))
      {
        resultReward.dropReward.questItem.Add(reward.questItem[index]);
      }
      else
      {
        resultReward.eventReward.questItem.Add(reward.questItem[index]);
        stringList.Add(reward.questItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < reward.accessoryItem.Count; ++index)
    {
      if (string.IsNullOrEmpty(reward.accessoryItem[index].rewardTitle))
      {
        resultReward.dropReward.accessoryItem.Add(reward.accessoryItem[index]);
      }
      else
      {
        resultReward.eventReward.accessoryItem.Add(reward.accessoryItem[index]);
        stringList.Add(reward.accessoryItem[index].rewardTitle);
      }
    }
    for (int index = 0; index < stringList.Count; ++index)
    {
      if (!this.eventRewardTitles.Contains(stringList[index]))
        this.eventRewardTitles.Add(stringList[index]);
    }
  }

  private new void OpenAllEventRewardDialog(System.Action endCallback)
  {
    this.eventRewardIndex = 0;
    this.eventRewardList = new List<QuestCompleteReward>();
    for (int index = 0; index < this.eventRewardTitles.Count; ++index)
      this.eventRewardList.Add(new QuestCompleteReward());
    foreach (ResultReward resultReward in this.resultRewards)
    {
      QuestCompleteReward eventReward = resultReward.eventReward;
      for (int index1 = 0; index1 < eventReward.eventPrice.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.eventRewardTitles.Count; ++index2)
        {
          if (this.eventRewardTitles[index2] == eventReward.eventPrice[index1].rewardTitle)
            this.eventRewardList[index2].eventPrice.Add(eventReward.eventPrice[index1]);
        }
      }
      for (int index3 = 0; index3 < eventReward.item.Count; ++index3)
      {
        for (int index4 = 0; index4 < this.eventRewardTitles.Count; ++index4)
        {
          if (this.eventRewardTitles[index4] == eventReward.item[index3].rewardTitle)
            this.eventRewardList[index4].item.Add(eventReward.item[index3]);
        }
      }
      for (int index5 = 0; index5 < eventReward.skillItem.Count; ++index5)
      {
        for (int index6 = 0; index6 < this.eventRewardTitles.Count; ++index6)
        {
          if (this.eventRewardTitles[index6] == eventReward.skillItem[index5].rewardTitle)
            this.eventRewardList[index6].skillItem.Add(eventReward.skillItem[index5]);
        }
      }
      for (int index7 = 0; index7 < eventReward.equipItem.Count; ++index7)
      {
        for (int index8 = 0; index8 < this.eventRewardTitles.Count; ++index8)
        {
          if (this.eventRewardTitles[index8] == eventReward.equipItem[index7].rewardTitle)
            this.eventRewardList[index8].equipItem.Add(eventReward.equipItem[index7]);
        }
      }
      for (int index9 = 0; index9 < eventReward.questItem.Count; ++index9)
      {
        for (int index10 = 0; index10 < this.eventRewardTitles.Count; ++index10)
        {
          if (this.eventRewardTitles[index10] == eventReward.questItem[index9].rewardTitle)
            this.eventRewardList[index10].questItem.Add(eventReward.questItem[index9]);
        }
      }
      for (int index11 = 0; index11 < eventReward.accessoryItem.Count; ++index11)
      {
        for (int index12 = 0; index12 < this.eventRewardTitles.Count; ++index12)
        {
          if (this.eventRewardTitles[index12] == eventReward.accessoryItem[index11].rewardTitle)
            this.eventRewardList[index12].accessoryItem.Add(eventReward.accessoryItem[index11]);
        }
      }
    }
    if (this.eventRewardList.Count == 0)
    {
      if (endCallback == null)
        return;
      endCallback();
    }
    else
    {
      this.OpenEventRewardDialog(this.eventRewardList[this.eventRewardIndex], this.eventRewardTitles[this.eventRewardIndex], endCallback);
      ++this.eventRewardIndex;
    }
  }

  private void OnQuery_SKIP()
  {
    switch (this.animState)
    {
      case ArenaResultTop.RESULT_ANIM_STATE.TITLE:
      case ArenaResultTop.RESULT_ANIM_STATE.DROP:
      case ArenaResultTop.RESULT_ANIM_STATE.REMAIN_TIME:
        this.SkipTween((Enum) ArenaResultTop.UI.OBJ_TITLE);
        this.SkipTween((Enum) ArenaResultTop.UI.OBJ_POINT_SHOP_RESULT_ROOT);
        this.SkipTween((Enum) ArenaResultTop.UI.OBJ_EXP);
        this.SkipTween((Enum) ArenaResultTop.UI.OBJ_MONEY);
        this.SkipTween((Enum) ArenaResultTop.UI.OBJ_REMAIN_TIME);
        break;
      case ArenaResultTop.RESULT_ANIM_STATE.CLEAR_EFFECT:
        this.SkipTween((Enum) ArenaResultTop.UI.OBJ_RANK_UP);
        break;
      case ArenaResultTop.RESULT_ANIM_STATE.BEST_SCORE:
        this.SkipTween((Enum) ArenaResultTop.UI.SPR_BESTSCORE);
        break;
    }
    this.is_skip = true;
    GameSection.StopEvent();
  }

  private void OnQuery_NEXT()
  {
    if (this.animState == ArenaResultTop.RESULT_ANIM_STATE.IDLE)
      this.GoArenaList();
    else if (this.animState != ArenaResultTop.RESULT_ANIM_STATE.END)
      this.OnQuery_SKIP();
    else
      this.GoArenaList();
  }

  private void GoArenaList()
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.ClearArenaInfo();
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData(GameSection.GetGoingHomeEvent()),
      new EventData("ARENA_LIST")
    });
  }

  private void OnQuery_RETRY()
  {
    if (this.animState == ArenaResultTop.RESULT_ANIM_STATE.IDLE)
      this.ReloadScene();
    else if (this.animState != ArenaResultTop.RESULT_ANIM_STATE.END)
      this.OnQuery_SKIP();
    else
      this.ReloadScene();
  }

  private void ReloadScene()
  {
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.Clear();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.isRetry = true;
    MonoBehaviourSingleton<GameSceneManager>.I.ReloadScene();
  }

  protected override string GetSceneName() => nameof (ArenaResultTop);

  private new enum UI
  {
    LBL_QUEST_NAME,
    LBL_PLAYER_LV,
    LBL_PLAYER_LVUP,
    SPR_LEVELUP,
    LBL_LVUP_NUM,
    OBJ_GET_EXP_ROOT,
    OBJ_MISSION_ROOT,
    OBJ_MISSION_NEW_CLEAR_ROOT,
    OBJ_TREASURE_ROOT,
    STR_TITLE_EXP,
    STR_TITLE_MISSION,
    STR_TITLE_REWARD,
    OBJ_EXP_REWARD_FRAME,
    LBL_EXP,
    SPR_GAUGE_UPPER,
    PBR_EXP,
    OBJ_RESULT_EXP_GAUGE_CTRL,
    OBJ_MISSION_INFO_FRAME,
    OBJ_MISSION_01,
    OBJ_MISSION_02,
    OBJ_MISSION_03,
    LBL_MISSION_NAME_01,
    LBL_MISSION_NAME_02,
    LBL_MISSION_NAME_03,
    SPR_CROWN_01,
    SPR_CROWN_02,
    SPR_CROWN_03,
    SPR_CLEARED_CROWN_01,
    SPR_CLEARED_CROWN_02,
    SPR_CLEARED_CROWN_03,
    STR_EMPTY_MISSION,
    GET_ITEM,
    GET_ITEM_2,
    OBJ_QUEST_REWARD_FRAME,
    LBL_REWARD_GOLD,
    TBL_ITEM,
    OBJ_SCROLL_VIEW,
    OBJ_SCROLL_VIEW_2,
    GRD_DROP_ITEM,
    GRD_DROP_ITEM_2,
    BTN_NEXT,
    BTN_RETRY,
    BTN_SKIP_FULL_SCREEN,
    BTN_SKIP_IN_SCROLL,
    BTN_SKIP_IN_SCROLL_2,
    PNL_MATERIAL_INFO,
    PNL_MATERIAL_INFO_2,
    OBJ_TREASURE_ROOT_NON_MISSION,
    OBJ_POINT_SHOP_RESULT_ROOT,
    OBJ_NORMAL_POINT_SHOP_ROOT,
    OBJ_EVENT_POINT_SHOP_ROOT,
    LBL_NORMAL_GET_POINT_SHOP,
    LBL_NORMAL_TOTAL_POINT_SHOP,
    TEX_NORMAL_POINT_SHOP_ICON,
    LBL_EVENT_GET_POINT_SHOP,
    LBL_EVENT_TOTAL_POINT_SHOP,
    TEX_EVENT_POINT_SHOP_ICON,
    LBL_GUILD_REQUEST_GET_POINT,
    OBJ_TITLE,
    OBJ_WAVE,
    LBL_WAVE,
    OBJ_TIME,
    LBL_TIME,
    OBJ_MONEY,
    OBJ_COIN,
    OBJ_ARRIVAL_EFFECT_ROOT,
    OBJ_ARRIVAL_EFFECT,
    OBJ_ARRIVAL_BONUS,
    GRD_ARRIVAL_ITEM_ICON,
    STR_REWARD_TITLE,
    SPR_WAVE_01,
    SPR_WAVE_10,
    SPR_WAVE_100,
    TBL_DROP_ITEM,
    LBL_DROP_ITEM_WAVE,
    STR_TITLE_WAVE,
    STR_TITLE_TIME,
    LBL_EXPLORE_GET_POINT,
    LBL_EXPLORE_TOTAL_POINT,
    SPR_TITLE,
    OBJ_EXP,
    OBJ_REMAIN_TIME,
    OBJ_CLEAR_TIME,
    STR_CLEAR_TIME_NAME,
    LBL_CLEAR_TIME,
    OBJ_BEFORE_TIME,
    SPR_BEFORE_TIME_NAME,
    LBL_BEFORE_TIME,
    SPR_BESTSCORE,
    OBJ_CLEAR_EFFECT_ROOT,
    OBJ_CLEAR_EFFECT,
    OBJ_RANK_UP_ROOT,
    OBJ_RANK_UP,
    TEX_RANK_PRE,
    TEX_RANK_NEW,
    OBJ_PARTICLE,
    OBJ_CONGRATULATIONS_ROOT,
    OBJ_CONGRATULATIONS,
    OBJ_CONGRATULATIONS_PARTICLE,
    LBL_BOSS_NAME,
    TBL_GUILD_REQUEST_RESULT,
    OBJ_BONUS_POINT_SHOP,
    TXT_BONUS_POINT_ICON,
    LBL_BONUS_POINT_NUM,
    TEX_MISSION_COIN_01,
    TEX_MISSION_COIN_02,
    TEX_MISSION_COIN_03,
    SPR_CROWN01_OFF,
    SPR_CROWN02_OFF,
    SPR_CROWN03_OFF,
    SHADOW,
    BTN_NEXT_ALL,
    BTN_END_HUNT_CENTER,
    BTN_END_HUNT_LEFT,
    BTN_REPEAT_HUNT,
    LBL_BTN_REPEAT_HUNT,
    LBL_WAIT_FOR_HOST,
  }

  private new enum AUDIO
  {
    COUNTUP = 40000012, // 0x02625A0C
    ADVENT = 40000026, // 0x02625A1A
    ACHIEVEMENT = 40000028, // 0x02625A1C
    CATEGORY = 40000228, // 0x02625AE4
    POINTREWARD = 40000230, // 0x02625AE6
    ARRIVAL = 40000269, // 0x02625B0D
  }

  private new enum RESULT_ANIM_STATE
  {
    IDLE,
    TITLE,
    DROP,
    REMAIN_TIME,
    CLEAR_TIME_COUNT_UP,
    CLEAR_EFFECT,
    BEST_SCORE,
    EVENT,
    END,
  }
}
