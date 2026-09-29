// Decompiled with JetBrains decompiler
// Type: HomeMiniBingo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Linq;
using UnityEngine;

#nullable disable
public class HomeMiniBingo : HomeBingo
{
  private uint defaultEventId;
  protected new int ColmunNum = 3;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    if (this.isLocalInitialized)
      base.Initialize();
    else
      this.InitializeOnce((System.Action) (() => base.Initialize()));
  }

  protected override void InitializeOnce(System.Action callback)
  {
    this.isLocalInitialized = true;
    this.itemNum = this.ColmunNum * this.ColmunNum;
    switch (GameSection.GetEventData())
    {
      case bool flag:
        this.isComeFromAutoEvent = flag;
        break;
      case object[] objArray:
        this.defaultEventId = (uint) objArray[0];
        this.isComeFromAutoEvent = (bool) objArray[1];
        break;
    }
    this.isFirstUpdate = true;
    this.CacheAudio(new LoadingQueue((MonoBehaviour) this));
    this.StartCoroutine(this.DoInitialize(callback));
  }

  protected override IEnumerator DoInitialize(System.Action callback)
  {
    this.eventDataList = MonoBehaviourSingleton<QuestManager>.I.GetValidBingoDataListInSection();
    if (this.eventDataList == null || this.eventDataList.Count <= 0)
    {
      this.RequestNotExistBingo();
      this.HideAll();
      base.Initialize();
    }
    else
    {
      this.InitCardDataList(this.eventDataList);
      this.SetCurrentIndex(this.eventDataList.Select((e, j) => new
      {
        Content = e,
        Index = j
      }).Where(ano => (long) ano.Content.eventId == (long) this.defaultEventId).Select(ano => ano.Index).FirstOrDefault<int>());
      yield return (object) this.StartCoroutine(this.LoadBanner(this.GetEventDataFromList(this.GetCurrentIndex()), this.GetCurrentIndex()));
      callback();
    }
  }

  public override void StartSection() => base.StartSection();

  protected override void InitCard(int cardIndex)
  {
    this.cardDataList[cardIndex].cardTransform = this.GetCtrl((Enum) HomeMiniBingo.UI.OBJ_CARD);
  }

  public override void UpdateUI()
  {
    if (this.cardDataList == null || this.cardDataList.Count <= 0 || this.cardDataList[this.currentCardIndex].gridDataList.Count <= 0)
      return;
    int count = this.cardDataList.Count;
    if (this.isFirstUpdate)
    {
      this.isFirstUpdate = false;
      this.SetActive((Enum) HomeMiniBingo.UI.OBJ_COMPLETE, false);
      bool isCompleted = this.GetCurrentCard().allBingoData.isCompleted;
      this.SetActive((Enum) HomeMiniBingo.UI.OBJ_COMPLETE_STAY, isCompleted);
      if (isCompleted)
      {
        UITweenCtrl component = ((Component) this.GetCtrl((Enum) HomeMiniBingo.UI.OBJ_COMPLETE_STAY)).GetComponent<UITweenCtrl>();
        component.Reset();
        component.Play();
      }
    }
    this.SetActive((Enum) HomeMiniBingo.UI.OBJ_BINGO_ANIMATION, false);
    this.UpdateLeftRightButton();
    this.UpdateCard(this.cardDataList[this.currentCardIndex], this.currentCardIndex);
    this.SetEndDateLabel();
  }

  private void HideLeftRightButton()
  {
    this.SetActive((Enum) HomeMiniBingo.UI.BTN_RIGHT, false);
    this.SetActive((Enum) HomeMiniBingo.UI.BTN_LEFT, false);
  }

  private void DispLeftRightButton()
  {
    this.SetActive((Enum) HomeMiniBingo.UI.BTN_RIGHT, true);
    this.SetActive((Enum) HomeMiniBingo.UI.BTN_LEFT, true);
  }

  private void UpdateLeftRightButton()
  {
    if (this.cardDataList.Count <= 1)
      this.HideLeftRightButton();
    else
      this.DispLeftRightButton();
  }

  protected override void UpdateCard(HomeBingo.CardData cardData, int cardIndex)
  {
    Transform cardTransform = cardData.cardTransform;
    this.UpdateBingoName(cardTransform, cardData.eventData);
    this.UpdateEndData(cardTransform, cardData.eventData);
    for (int index = 0; index < this.ColmunNum * this.ColmunNum; ++index)
      this.SetUpGridItem(cardIndex, index, this.GetCtrl((Enum) HomeMiniBingo.UI.GRD_BINGO_LIST).Find("SPR_ICON_" + (object) (index + 1)), false);
    this.UpdateReachs(cardData);
    ((Component) this.GetCtrl((Enum) HomeMiniBingo.UI.GRD_BINGO_LIST)).GetComponent<UIGrid>().Reposition();
  }

  protected override void SetUpGridItem(int cardIndex, int index, Transform t, bool recycle)
  {
    int num = index + 1;
    HomeBingo.GridData gridData = this.cardDataList[cardIndex].gridDataList[index];
    gridData.SetEntity(t, "");
    BoxCollider component = ((Component) t).GetComponent<BoxCollider>();
    if (Object.op_Inequality((Object) component, (Object) null))
      ((Collider) component).enabled = true;
    this.SetActive(t, (Enum) HomeMiniBingo.UI.SPR_GRID_ITEM, !gridData.isCompleted);
    this.SetReachVisual(gridData, false);
    gridData.tweenCtrl.Reset();
    this.SetLabelText(t, (Enum) HomeMiniBingo.UI.LBL_GRID_ITEM, num.ToString());
    this.SetEvent(t, "SELECT_GRID_ITEM", (object) new object[2]
    {
      (object) cardIndex,
      (object) index
    });
  }

  protected override void UpdateRewardNumber(
    HomeBingo.CardData cardData,
    HomeBingo.MissionData missionData)
  {
    if (missionData is HomeBingo.GridData)
    {
      HomeBingo.GridData gridData = missionData as HomeBingo.GridData;
      this.SetActive(cardData.cardTransform, (Enum) HomeMiniBingo.UI.SPR_REWARD_GRID_ITEM, true);
      int num = cardData.gridDataList.IndexOf(gridData);
      HomeMiniBingo.UI ui = HomeMiniBingo.UI.SPR_REWARD_1;
      for (int index = 0; index < this.ColmunNum * this.ColmunNum; ++index)
        this.SetActive((Enum) (ui + index), num == index);
    }
    else
      this.SetActive(cardData.cardTransform, (Enum) HomeMiniBingo.UI.SPR_REWARD_GRID_ITEM, false);
  }

  private void OnQuery_LEFT()
  {
    ++this.currentCardIndex;
    this.currentCardIndex %= this.cardDataList.Count;
    this.isComeFromLeftRightEvent = true;
    GameSection.StayEvent();
    this.ChangeCard(this.currentCardIndex, (System.Action) (() =>
    {
      GameSection.ResumeEvent(true);
      this.StartCoroutine(this.WaitAndStartAutoComplete());
    }));
  }

  private void OnQuery_RIGHT()
  {
    --this.currentCardIndex;
    if (this.currentCardIndex < 0)
      this.currentCardIndex = this.cardDataList.Count - 1;
    this.isComeFromLeftRightEvent = true;
    GameSection.StayEvent();
    this.ChangeCard(this.currentCardIndex, (System.Action) (() =>
    {
      GameSection.ResumeEvent(true);
      this.StartCoroutine(this.WaitAndStartAutoComplete());
    }));
  }

  protected override void ChangeCard(int cardIndex, System.Action callback)
  {
    this.SetActive((Enum) HomeMiniBingo.UI.OBJ_COMPLETE, false);
    bool isCompleteAll = this.GetCurrentCard().allBingoData.isCompleted;
    this.SetActive((Enum) HomeMiniBingo.UI.OBJ_COMPLETE_STAY, false);
    if (isCompleteAll)
      ((Component) this.GetCtrl((Enum) HomeMiniBingo.UI.OBJ_COMPLETE_STAY)).GetComponent<UITweenCtrl>().Reset();
    this.StartCoroutine(this.LoadBanner(this.GetEventDataFromList(cardIndex), cardIndex, (Action<bool>) (isSuccess =>
    {
      this.HideReward(this.cardDataList[cardIndex]);
      this.RefreshUI();
      if (isCompleteAll)
      {
        Transform ctrl = this.GetCtrl((Enum) HomeMiniBingo.UI.OBJ_COMPLETE_STAY);
        this.SetActive((Enum) HomeMiniBingo.UI.OBJ_COMPLETE_STAY, true);
        UITweenCtrl component = ((Component) ctrl).GetComponent<UITweenCtrl>();
        component.Reset();
        component.Play();
      }
      callback();
    })));
  }

  private HomeBingo.CardData GetSelectedCardData()
  {
    return this.currentCardIndex >= this.cardDataList.Count ? (HomeBingo.CardData) null : this.cardDataList[this.currentCardIndex];
  }

  private new enum UI
  {
    GRD_BINGO_LIST,
    OBJ_ROOT_CARDS,
    BTN_CLOSE,
    OBJ_COMPLETE,
    OBJ_BINGO_EFFECT,
    OBJ_PARTICLE_1,
    OBJ_PARTICLE_2,
    OBJ_COMPLETE_STAY,
    OBJ_BINGO_ANIMATION,
    OBJ_CARD,
    OBJ_CARD_TWEEN_CTRL,
    OBJ_TWEEN_CARD,
    LBL_REWARD,
    GRD_REWARD,
    LBL_PERIOD,
    LBL_BINGO_NAME,
    OBJ_BTN_ROOT,
    TEX_EVENT_BG,
    OBJ_BANNER_ROOT,
    SPR_REWARD_GRID_ITEM,
    OBJ_COMPLETE_MARK,
    LBL_REWRAD_COUNT,
    OBJ_COMPLETE_TWEEN_ROOT,
    LBL_GRID_ITEM,
    SPR_GRID_COMPLETED,
    SPR_GRID_COMPLETE,
    SPR_GRID_ITEM,
    SPR_GRID_REACH,
    BTN_LEFT,
    BTN_RIGHT,
    SPR_REWARD_1,
    SPR_REWARD_2,
    SPR_REWARD_3,
    SPR_REWARD_4,
    SPR_REWARD_5,
    SPR_REWARD_6,
    SPR_REWARD_7,
    SPR_REWARD_8,
    SPR_REWARD_9,
    LBL_EVENT_END,
  }

  private new enum AUDIO
  {
    ONE = 40000390, // 0x02625B86
    BINGO = 40000391, // 0x02625B87
    ALL_BINGO = 40000392, // 0x02625B88
  }
}
