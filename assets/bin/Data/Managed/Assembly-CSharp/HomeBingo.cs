// Decompiled with JetBrains decompiler
// Type: HomeBingo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

#nullable disable
public class HomeBingo : GameSection
{
  protected List<Network.EventData> eventDataList;
  protected bool isLocalInitialized;
  protected readonly string GridItemName = "HomeBingoGridItem";
  protected int ColmunNum = 5;
  protected readonly float StandByX = 700f;
  protected readonly float GridAnimationTime = 1f;
  protected readonly float CompleteAnimationTime = 2.5f;
  protected readonly float BingoAnimationTime = 3f;
  protected int itemNum;
  protected int centerIndex;
  protected int currentCardIndex;
  protected bool isComeFromAutoEvent;
  protected bool isComeFromLeftRightEvent;
  protected bool isFirstUpdate;
  protected List<HomeBingo.CardData> cardDataList = new List<HomeBingo.CardData>(3);
  protected HomeBingo.GridData centerGrid;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    if (this.isLocalInitialized)
      base.Initialize();
    else
      this.InitializeOnce((System.Action) (() => base.Initialize()));
  }

  protected virtual void InitializeOnce(System.Action callback)
  {
    this.isLocalInitialized = true;
    this.itemNum = this.ColmunNum * this.ColmunNum;
    this.centerIndex = this.itemNum / 2;
    if (GameSection.GetEventData() is bool eventData)
      this.isComeFromAutoEvent = eventData;
    this.isFirstUpdate = true;
    this.CacheAudio(new LoadingQueue((MonoBehaviour) this));
    this.StartCoroutine(this.DoInitialize(callback));
  }

  protected virtual IEnumerator DoInitialize(System.Action callback)
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
      this.SetCurrentIndex(0);
      this.InitCardDataList(this.eventDataList);
      int currentIndex = this.GetCurrentIndex();
      yield return (object) this.StartCoroutine(this.LoadBanner(this.GetEventDataFromList(currentIndex), currentIndex));
      callback();
    }
  }

  protected IEnumerator LoadBanner(Network.EventData eventData, int index, Action<bool> callback = null)
  {
    if (eventData == null)
    {
      if (callback != null)
        callback(false);
    }
    else
    {
      string eventBg = ResourceName.GetEventBG(eventData.bannerId);
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo_bg = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_BG, eventBg);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      Texture2D loadedObject = lo_bg.loadedObject as Texture2D;
      this.SetTexture(this.cardDataList[index].cardTransform, (Enum) HomeBingo.UI.TEX_EVENT_BG, (Texture) loadedObject);
      bool flag = true;
      if (callback != null)
        callback(flag);
    }
  }

  protected void HideAll()
  {
    this.SetActive((Enum) HomeBingo.UI.OBJ_BTN_ROOT, false);
    this.SetActive((Enum) HomeBingo.UI.OBJ_ROOT_CARDS, false);
    this.SetActive((Enum) HomeBingo.UI.BTN_CLOSE, false);
  }

  protected void RequestNotExistBingo() => this.RequestEvent("NOT_EXIST_BINGO");

  protected void InitCardDataList(List<Network.EventData> eventDataList)
  {
    this.cardDataList.Clear();
    int index = 0;
    for (int count = eventDataList.Count; index < count; ++index)
      this.InitCardData(index, eventDataList[index]);
  }

  protected HomeBingo.CardData GetCurrentCard() => this.cardDataList[this.GetCurrentIndex()];

  protected Network.EventData GetEventDataFromList(int index)
  {
    if (this.eventDataList.IsNullOrEmpty<Network.EventData>())
      return (Network.EventData) null;
    return index >= this.eventDataList.Count ? (Network.EventData) null : this.eventDataList[index];
  }

  protected int GetCurrentIndex() => this.currentCardIndex;

  protected virtual void SetCurrentIndex(int index) => this.currentCardIndex = index;

  protected void InitCardData(int index, Network.EventData eventData)
  {
    this.cardDataList.Add(new HomeBingo.CardData()
    {
      eventData = eventData
    });
    this.InitCard(index);
    this.RefreshMissionData(index);
  }

  protected bool IsPlayableVersion(out Network.EventData notPlayableEventData)
  {
    notPlayableEventData = (Network.EventData) null;
    if (this.cardDataList == null || this.cardDataList.Count <= 0)
      return true;
    Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
    int index = 0;
    for (int count = this.cardDataList.Count; index < count; ++index)
    {
      Network.EventData eventData = this.cardDataList[index].eventData;
      if (eventData != null && !eventData.IsPlayableWith(nativeVersionFromName))
      {
        notPlayableEventData = eventData;
        return false;
      }
    }
    return true;
  }

  protected virtual void InitCard(int cardIndex)
  {
    HomeBingo.CardData cardData = this.cardDataList[cardIndex];
    Transform root;
    if (cardIndex == 0)
    {
      root = this.GetCtrl((Enum) HomeBingo.UI.OBJ_CARD);
    }
    else
    {
      root = Object.Instantiate<Transform>(this.GetCtrl((Enum) HomeBingo.UI.OBJ_CARD));
      root.SetParent(this.GetCtrl((Enum) HomeBingo.UI.OBJ_ROOT_CARDS), false);
    }
    cardData.cardTransform = root;
    cardData.cardTweenCtrl = ((Component) this.FindCtrl(root, (Enum) HomeBingo.UI.OBJ_CARD_TWEEN_CTRL)).GetComponent<UITweenCtrl>();
    Transform ctrl1 = this.FindCtrl(root, (Enum) HomeBingo.UI.OBJ_TWEEN_CARD);
    cardData.cardTween = ((Component) ctrl1).GetComponent<TweenPosition>();
    Transform ctrl2 = this.FindCtrl(this.GetCurrentCard().cardTransform, (Enum) HomeBingo.UI.OBJ_COMPLETE_TWEEN_ROOT);
    if (Object.op_Inequality((Object) ctrl2, (Object) null))
    {
      cardData.completeRewardTweenlRoot = ctrl2;
      UITweenCtrl component = ((Component) ctrl2).GetComponent<UITweenCtrl>();
      if (Object.op_Inequality((Object) component, (Object) null))
        cardData.completeRewardTweenlCtrl = component;
    }
    if (cardIndex == this.currentCardIndex)
      return;
    Vector3 localPosition = ctrl1.localPosition;
    localPosition.x = this.StandByX;
    ctrl1.localPosition = localPosition;
  }

  protected void RefreshMissionData(int eventIndex)
  {
    HomeBingo.CardData cardData = this.cardDataList[eventIndex];
    cardData.gridDataList.Clear();
    cardData.bingoDataList.Clear();
    this.AddDataNotCompleted(cardData);
    this.AddDataCompleted(cardData);
    this.SetBingosChildrenData(cardData);
    if (cardData.gridDataList == null || cardData.gridDataList.Count <= 0)
    {
      this.RequestNotExistBingo();
      this.HideAll();
    }
    else
      cardData.gridDataList = cardData.gridDataList.OrderBy<HomeBingo.GridData, uint>((Func<HomeBingo.GridData, uint>) (g => g.deliveryData.id)).ToList<HomeBingo.GridData>();
  }

  protected void AddDataNotCompleted(HomeBingo.CardData cardData)
  {
    Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
    int index = 0;
    for (int length = deliveryList.Length; index < length; ++index)
    {
      Delivery info = deliveryList[index];
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) info.dId);
      if (deliveryTableData == null)
        Log.Warning("DeliveryTable Not Found : dId " + (object) info.dId);
      else if (deliveryTableData.IsEvent() && deliveryTableData.eventID == cardData.eventData.eventId)
        this.AddMissionData(deliveryTableData, info, cardData, false);
    }
  }

  protected void AddDataCompleted(HomeBingo.CardData cardData)
  {
    List<ClearStatusDelivery> clearStatusDelivery1 = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
    int index = 0;
    for (int count = clearStatusDelivery1.Count; index < count; ++index)
    {
      ClearStatusDelivery clearStatusDelivery2 = clearStatusDelivery1[index];
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) clearStatusDelivery2.deliveryId);
      if (deliveryTableData == null)
        Log.Warning("DeliveryTable Not Found : dId " + (object) clearStatusDelivery2.deliveryId);
      else if (deliveryTableData.IsEvent() && deliveryTableData.eventID == cardData.eventData.eventId && clearStatusDelivery2.deliveryStatus == 3)
        this.AddMissionData(deliveryTableData, (Delivery) null, cardData, true);
    }
  }

  protected void AddMissionData(
    DeliveryTable.DeliveryData data,
    Delivery info,
    HomeBingo.CardData cardData,
    bool isCompleted)
  {
    switch (data.subType)
    {
      case DELIVERY_SUB_TYPE.ROW_BINGO:
        cardData.bingoDataList.Add(new HomeBingo.BingoData(data, info, isCompleted));
        break;
      case DELIVERY_SUB_TYPE.ALL_BINGO:
        cardData.allBingoData = new HomeBingo.AllBingoData(data, info, isCompleted);
        break;
      default:
        cardData.gridDataList.Add(new HomeBingo.GridData(data, info, isCompleted));
        break;
    }
  }

  protected void SetBingosChildrenData(HomeBingo.CardData cardData)
  {
    List<HomeBingo.BingoData> bingoDataList = cardData.bingoDataList;
    int index = 0;
    for (int count = bingoDataList.Count; index < count; ++index)
    {
      HomeBingo.BingoData bingoData = bingoDataList[index];
      this.SetABingoChildrenData(cardData, bingoData);
    }
  }

  private void SetABingoChildrenData(HomeBingo.CardData cardData, HomeBingo.BingoData bingoData)
  {
    DeliveryTable.DeliveryData.NeedData[] needs = bingoData.deliveryData.needs;
    int index = 0;
    for (int length = needs.Length; index < length; ++index)
    {
      DeliveryTable.DeliveryData.NeedData need = needs[index];
      if ((uint) need.needId > 0U)
      {
        HomeBingo.GridData gridData = cardData.gridDataList.FirstOrDefault<HomeBingo.GridData>((Func<HomeBingo.GridData, bool>) (grid => (int) grid.deliveryData.id == (int) (uint) need.needId));
        if (gridData != null && gridData.deliveryData != null)
          bingoData.childrenGridList.Add(gridData);
      }
    }
  }

  private bool IsGridDelivery(DeliveryTable.DeliveryData data)
  {
    switch (data.subType)
    {
      case DELIVERY_SUB_TYPE.ROW_BINGO:
        return false;
      case DELIVERY_SUB_TYPE.ALL_BINGO:
        return false;
      default:
        return true;
    }
  }

  public override void StartSection()
  {
    Network.EventData notPlayableEventData;
    if (!this.IsPlayableVersion(out notPlayableEventData))
    {
      this.RequestEvent("SELECT_VERSION", (object) string.Format(this.sectionData.GetText("REQUIRE_HIGHER_VERSION"), (object) notPlayableEventData.minVersion));
    }
    else
    {
      if (this.isComeFromAutoEvent)
        return;
      this.AutoCompleteAchievableDelivery();
    }
  }

  protected void OnCloseDialog() => this.AutoCompleteAchievableDelivery();

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    base.OnNotify(flags);
    if ((flags & GameSection.NOTIFY_FLAG.TRANSITION_END) == (GameSection.NOTIFY_FLAG) 0)
      return;
    Network.EventData notPlayableEventData;
    if (!this.IsPlayableVersion(out notPlayableEventData))
      this.StartCoroutine(this.WaitAndStartNotPlayable(notPlayableEventData));
    else
      this.StartCoroutine(this.WaitAndStartAutoComplete());
  }

  protected IEnumerator WaitAndStartAutoComplete()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      yield return (object) null;
    this.AutoCompleteAchievableDelivery();
  }

  private IEnumerator WaitAndStartNotPlayable(Network.EventData eventData)
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      yield return (object) null;
    this.DispatchEvent("SELECT_VERSION", (object) string.Format(this.sectionData.GetText("REQUIRE_HIGHER_VERSION"), (object) eventData.minVersion));
  }

  protected void AutoCompleteAchievableDelivery()
  {
    HomeBingo.CardData selectedCardData = this.GetSelectedCardData();
    if (selectedCardData == null)
      return;
    HomeBingo.BingoData missionCompleted1 = this.FindBingoMissionCompleted(selectedCardData);
    if (missionCompleted1 != null)
    {
      this.DoCompleteBingo(missionCompleted1);
    }
    else
    {
      HomeBingo.GridData missionCompleted2 = this.FindGridMissionCompleted(selectedCardData);
      if (missionCompleted2 != null)
      {
        this.DoCompleteGrid(missionCompleted2);
      }
      else
      {
        HomeBingo.AllBingoData missionCompleted3 = this.FindAllBingoMissionCompleted(selectedCardData);
        if (missionCompleted3 == null)
          return;
        this.DoCompleteAllBingo(missionCompleted3);
      }
    }
  }

  private void DoCompleteGrid(HomeBingo.GridData completedGridData)
  {
    string event_name = "COMPLETE_GRID";
    if (this.isComeFromAutoEvent || this.isComeFromLeftRightEvent)
    {
      this.DispatchEvent(event_name, (object) completedGridData);
      this.isComeFromAutoEvent = false;
      this.isComeFromLeftRightEvent = false;
    }
    else
      this.RequestEvent(event_name, (object) completedGridData);
  }

  private void DoCompleteBingo(HomeBingo.BingoData completedData)
  {
    string event_name = "COMPLETE_BINGO";
    if (this.isComeFromAutoEvent || this.isComeFromLeftRightEvent)
    {
      this.DispatchEvent(event_name, (object) completedData);
      this.isComeFromAutoEvent = false;
      this.isComeFromLeftRightEvent = false;
    }
    else
      this.RequestEvent(event_name, (object) completedData);
  }

  private void DoCompleteAllBingo(HomeBingo.AllBingoData completedData)
  {
    string event_name = "COMPLETE_ALL_BINGO";
    if (this.isComeFromAutoEvent || this.isComeFromLeftRightEvent)
    {
      this.DispatchEvent(event_name, (object) completedData);
      this.isComeFromAutoEvent = false;
      this.isComeFromLeftRightEvent = false;
    }
    else
      this.RequestEvent(event_name, (object) completedData);
  }

  private HomeBingo.GridData FindGridMissionCompleted(HomeBingo.CardData cardData)
  {
    List<HomeBingo.GridData> gridDataList = cardData.gridDataList;
    int index = 0;
    for (int count = gridDataList.Count; index < count; ++index)
    {
      HomeBingo.GridData missionCompleted = gridDataList[index];
      if (!missionCompleted.isCompleted && MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) missionCompleted.deliveryData.id))
        return missionCompleted;
    }
    return (HomeBingo.GridData) null;
  }

  private HomeBingo.BingoData FindBingoMissionCompleted(HomeBingo.CardData cardData)
  {
    List<HomeBingo.BingoData> bingoDataList = cardData.bingoDataList;
    int index = 0;
    for (int count = bingoDataList.Count; index < count; ++index)
    {
      HomeBingo.BingoData missionCompleted = bingoDataList[index];
      if (!missionCompleted.isCompleted && MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) missionCompleted.deliveryData.id))
        return missionCompleted;
    }
    return (HomeBingo.BingoData) null;
  }

  private HomeBingo.AllBingoData FindAllBingoMissionCompleted(HomeBingo.CardData cardData)
  {
    HomeBingo.AllBingoData allBingoData = cardData.allBingoData;
    if (allBingoData == null)
      return (HomeBingo.AllBingoData) null;
    if (allBingoData.isCompleted)
      return (HomeBingo.AllBingoData) null;
    return !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) allBingoData.deliveryData.id) ? (HomeBingo.AllBingoData) null : allBingoData;
  }

  public override void UpdateUI()
  {
    if (this.cardDataList == null || this.cardDataList.Count <= 0)
      return;
    if (this.currentCardIndex >= this.cardDataList.Count)
      this.currentCardIndex = 0;
    if (this.cardDataList[this.currentCardIndex].gridDataList.Count <= 0)
      return;
    int count = this.cardDataList.Count;
    this.SetActive((Enum) HomeBingo.UI.OBJ_BTN_ROOT, false);
    if (this.isFirstUpdate)
    {
      this.isFirstUpdate = false;
      this.SetActive((Enum) HomeBingo.UI.OBJ_COMPLETE, false);
      bool isCompleted = this.GetCurrentCard().allBingoData.isCompleted;
      this.SetActive((Enum) HomeBingo.UI.OBJ_COMPLETE_STAY, isCompleted);
      if (isCompleted)
      {
        UITweenCtrl component = ((Component) this.GetCtrl((Enum) HomeBingo.UI.OBJ_COMPLETE_STAY)).GetComponent<UITweenCtrl>();
        component.Reset();
        component.Play();
      }
    }
    this.SetActive((Enum) HomeBingo.UI.OBJ_BINGO_ANIMATION, false);
    for (int index = 0; index < count; ++index)
      this.UpdateCard(this.cardDataList[index], index);
    this.SetEndDateLabel();
  }

  protected virtual void UpdateCard(HomeBingo.CardData cardData, int cardIndex)
  {
    Transform cardTransform = cardData.cardTransform;
    this.UpdateBingoName(cardTransform, cardData.eventData);
    this.UpdateEndData(cardTransform, cardData.eventData);
    this.SetGrid(cardTransform, (Enum) HomeBingo.UI.GRD_BINGO_LIST, this.GridItemName, cardData.gridDataList.Count + 1, false, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      if (i == this.centerIndex)
        this.SetUpCenterItem(cardIndex, i, t, b);
      else
        this.SetUpGridItem(cardIndex, i, t, b);
    }));
    this.UpdateReachs(cardData);
  }

  protected void UpdateBingoName(Transform t, Network.EventData eventData)
  {
  }

  protected void UpdateEndData(Transform t, Network.EventData eventData)
  {
  }

  protected void UpdateReachs(HomeBingo.CardData cardData)
  {
    List<HomeBingo.BingoData> bingoDataList = cardData.bingoDataList;
    int index = 0;
    for (int count = bingoDataList.Count; index < count; ++index)
      this.UpdateAReach(bingoDataList[index]);
  }

  protected void UpdateAReach(HomeBingo.BingoData bingoData)
  {
    List<HomeBingo.GridData> childrenGridList = bingoData.childrenGridList;
    HomeBingo.GridData gridData = (HomeBingo.GridData) null;
    int num = 0;
    int index = 0;
    for (int count = childrenGridList.Count; index < count; ++index)
    {
      if (!childrenGridList[index].isCompleted)
      {
        gridData = childrenGridList[index];
        ++num;
      }
      if (num >= 2)
        return;
    }
    if (gridData == null)
      return;
    this.SetReachVisual(gridData, true);
  }

  protected void SetReachVisual(HomeBingo.GridData gridData, bool isActive)
  {
    this.SetActive(gridData.transform, (Enum) HomeBingo.UI.SPR_GRID_REACH, isActive);
  }

  protected virtual void SetUpGridItem(int cardIndex, int index, Transform t, bool recycle)
  {
    if (index > this.centerIndex)
      --index;
    int num = index + 1;
    HomeBingo.GridData gridData = this.cardDataList[cardIndex].gridDataList[index];
    gridData.SetEntity(t, this.GetGridSpriteName(index));
    BoxCollider component = ((Component) t).GetComponent<BoxCollider>();
    if (Object.op_Inequality((Object) component, (Object) null))
      ((Collider) component).enabled = true;
    this.SetActive(t, (Enum) HomeBingo.UI.SPR_GRID_ITEM, !gridData.isCompleted);
    this.SetSprite(t, (Enum) HomeBingo.UI.SPR_GRID_ITEM, gridData.spriteName);
    this.SetLabelText(t, (Enum) HomeBingo.UI.LBL_GRID_ITEM, num.ToString());
    this.SetEvent(t, "SELECT_GRID_ITEM", (object) new object[2]
    {
      (object) cardIndex,
      (object) index
    });
  }

  protected virtual string GetGridSpriteName(int index)
  {
    if (index >= this.centerIndex)
      ++index;
    return $"{index + 1:d2}";
  }

  private string GetCenterSpriteName() => "13";

  private void SetUpCenterItem(int cardIndex, int index, Transform t, bool recycle)
  {
    this.centerGrid = new HomeBingo.GridData((DeliveryTable.DeliveryData) null, (Delivery) null, true);
    this.centerGrid.SetEntity(t, this.GetCenterSpriteName());
    this.SetLabelText(t, (Enum) HomeBingo.UI.LBL_GRID_ITEM, "C");
    this.SetActive(t, (Enum) HomeBingo.UI.SPR_GRID_ITEM, false);
    this.SetSprite(t, (Enum) HomeBingo.UI.SPR_GRID_ITEM, this.GetCenterSpriteName());
    this.SetEvent(t, "SELECT_CENTER", (object) new object[2]
    {
      (object) cardIndex,
      (object) index
    });
  }

  private void UpdateRewardIcon(Transform cardTransform, HomeBingo.MissionData gridData)
  {
    DeliveryRewardTable.DeliveryRewardData[] rewards = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData(gridData.deliveryData.id);
    int exp = 0;
    if (rewards == null || rewards.Length == 0)
      return;
    this.SetGrid(cardTransform, (Enum) HomeBingo.UI.GRD_REWARD, "", rewards.Length, false, (Action<int, Transform, bool>) ((index, t, is_recycle) =>
    {
      DeliveryRewardTable.DeliveryRewardData.Reward reward = rewards[index].reward;
      bool is_visible = false;
      if (reward.type == REWARD_TYPE.EXP)
      {
        exp += reward.num;
      }
      else
      {
        is_visible = true;
        ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(reward.type, reward.item_id, t, reward.num, questIconSizeType: ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_DETAIL);
        this.SetMaterialInfo(rewardItemIcon.transform, reward.type, reward.item_id);
        rewardItemIcon.SetRewardBG(true);
      }
      this.SetActive(t, is_visible);
    }));
  }

  private void OnQuery_SELECT_GRID_ITEM()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    int index1 = (int) eventData[0];
    int index2 = (int) eventData[1];
    HomeBingo.CardData cardData = this.cardDataList[index1];
    HomeBingo.GridData gridData = cardData.gridDataList[index2];
    this.UpdateReward(cardData, (HomeBingo.MissionData) gridData);
  }

  private void UpdateReward(
    HomeBingo.CardData cardData,
    HomeBingo.MissionData missionData,
    bool isComplete = false)
  {
    this.UpdateRewardIcon(cardData.cardTransform, missionData);
    this.UpdateRewardNumber(cardData, missionData);
    this.SetBannerActvie(cardData, false);
    this.SetActive(cardData.completeRewardTweenlRoot, false);
    this.SetActive(cardData.cardTransform, (Enum) HomeBingo.UI.OBJ_COMPLETE_MARK, missionData.isCompleted);
    DeliveryRewardTable.DeliveryRewardData[] deliveryRewardTableData = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData(missionData.deliveryData.id);
    if (deliveryRewardTableData == null || deliveryRewardTableData.Length == 0)
      return;
    DeliveryRewardTable.DeliveryRewardData deliveryRewardData = deliveryRewardTableData[0];
    this.SetLabelText(cardData.cardTransform, (Enum) HomeBingo.UI.LBL_REWARD, missionData.deliveryData.npcComment);
    int have;
    int need;
    MonoBehaviourSingleton<DeliveryManager>.I.GetAllProgressDelivery((int) missionData.deliveryData.id, out have, out need);
    if (isComplete || missionData.isCompleted)
      have = need;
    this.SetActive(cardData.cardTransform, (Enum) HomeBingo.UI.LBL_REWRAD_COUNT, true);
    this.SetLabelText(cardData.cardTransform, (Enum) HomeBingo.UI.LBL_REWRAD_COUNT, $"{(object) have}/{(object) need}");
  }

  protected virtual void UpdateRewardNumber(
    HomeBingo.CardData cardData,
    HomeBingo.MissionData missionData)
  {
    if (missionData is HomeBingo.GridData)
    {
      HomeBingo.GridData gridData = missionData as HomeBingo.GridData;
      this.SetActive(cardData.cardTransform, (Enum) HomeBingo.UI.SPR_REWARD_GRID_ITEM, true);
      this.SetSprite(cardData.cardTransform, (Enum) HomeBingo.UI.SPR_REWARD_GRID_ITEM, gridData.spriteName);
    }
    else
      this.SetActive(cardData.cardTransform, (Enum) HomeBingo.UI.SPR_REWARD_GRID_ITEM, false);
  }

  protected void HideReward(HomeBingo.CardData cardData)
  {
    this.SetActive(cardData.cardTransform, (Enum) HomeBingo.UI.SPR_REWARD_GRID_ITEM, false);
    this.SetBannerActvie(cardData, true);
    this.SetActive(cardData.completeRewardTweenlRoot, true);
    this.SetActive(cardData.cardTransform, (Enum) HomeBingo.UI.OBJ_COMPLETE_MARK, false);
  }

  private void SetBannerActvie(HomeBingo.CardData cardData, bool isActive)
  {
    this.SetActive(cardData.cardTransform, (Enum) HomeBingo.UI.OBJ_BANNER_ROOT, isActive);
  }

  private void OnQuery_GET_REWARD()
  {
  }

  private void OnQuery_COMPLETE_GRID()
  {
    HomeBingo.GridData gridData = GameSection.GetEventData() as HomeBingo.GridData;
    GameSection.StayEvent();
    Delivery deliveryInfo = gridData.deliveryInfo;
    HomeBingo.CardData cardData = this.GetCurrentCard();
    MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
    MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo.uId, false, (Action<bool, DeliveryRewardList>) ((is_success, recv_reward) =>
    {
      if (is_success)
        this.StartCoroutine(this.WaitAndDo((EventDelegate.Callback) (() => this.PlayGridCompleteAnimation(gridData, true, (EventDelegate.Callback) (() =>
        {
          GameSection.ChangeStayEvent("GET_REWARD", (object) new object[2]
          {
            (object) gridData.deliveryData,
            (object) cardData.eventData
          });
          GameSection.ResumeEvent(true);
          this.RefreshMissionData(this.currentCardIndex);
          this.RefreshUI();
        }))), 0.2f));
      else
        GameSection.ResumeEvent(false);
    }));
  }

  private void OnQuery_COMPLETE_BINGO()
  {
    HomeBingo.BingoData bingoData = GameSection.GetEventData() as HomeBingo.BingoData;
    GameSection.StayEvent();
    Delivery deliveryInfo = bingoData.deliveryInfo;
    HomeBingo.CardData cardData = this.GetCurrentCard();
    DeliveryTable.DeliveryData.NeedData[] needs = bingoData.deliveryData.needs;
    MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
    MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo.uId, false, (Action<bool, DeliveryRewardList>) ((is_success, recv_reward) => this.StartCoroutine(this.WaitAndDo((EventDelegate.Callback) (() => this.OnEndSendBingoMission(is_success, cardData, bingoData, bingoData.childrenGridList)), 0.2f))));
  }

  private void OnQuery_COMPLETE_ALL_BINGO()
  {
    HomeBingo.AllBingoData allBingoData = GameSection.GetEventData() as HomeBingo.AllBingoData;
    GameSection.StayEvent();
    Delivery deliveryInfo = allBingoData.deliveryInfo;
    HomeBingo.CardData cardData = this.GetCurrentCard();
    List<HomeBingo.GridData> gridDatas = cardData.gridDataList;
    MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
    MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo.uId, false, (Action<bool, DeliveryRewardList>) ((is_success, recv_reward) => this.StartCoroutine(this.WaitAndDo((EventDelegate.Callback) (() => this.OnEndSendAllMission(is_success, cardData, allBingoData, gridDatas)), 0.2f))));
  }

  private void OnEndSendBingoMission(
    bool is_success,
    HomeBingo.CardData cardData,
    HomeBingo.BingoData bingoData,
    List<HomeBingo.GridData> gridDatas)
  {
    if (is_success)
    {
      if (gridDatas == null || gridDatas.Count <= 0)
      {
        this.OnSuccessSend(cardData, bingoData.deliveryData);
      }
      else
      {
        int index = 0;
        for (int count = gridDatas.Count; index < count; ++index)
          this.PlayGridCompleteAnimation(gridDatas[index], false);
        this.UpdateReward(this.GetCurrentCard(), (HomeBingo.MissionData) bingoData, true);
        this.PlayTweenComplete();
        if (gridDatas.Count <= 4)
          this.PlayCenterAnimation();
        this.StartCoroutine(this.WaitAndDo((EventDelegate.Callback) (() => this.PlayBingoAnimation((EventDelegate.Callback) (() => this.OnSuccessSend(cardData, bingoData.deliveryData)))), this.GridAnimationTime));
      }
    }
    else
      GameSection.ResumeEvent(false);
  }

  private void OnEndSendAllMission(
    bool is_success,
    HomeBingo.CardData cardData,
    HomeBingo.AllBingoData allBingoData,
    List<HomeBingo.GridData> gridDatas)
  {
    if (is_success)
    {
      if (gridDatas == null || gridDatas.Count <= 0)
      {
        this.PlayAllCompleteAnimation((EventDelegate.Callback) (() => this.OnSuccessSend(cardData, allBingoData.deliveryData)));
      }
      else
      {
        int index = 0;
        for (int count = gridDatas.Count; index < count; ++index)
          this.PlayGridCompleteAnimation(gridDatas[index], false);
        this.PlayCenterAnimation();
        this.UpdateReward(this.GetCurrentCard(), (HomeBingo.MissionData) allBingoData, true);
        this.PlayTweenComplete();
        this.StartCoroutine(this.WaitAndDo((EventDelegate.Callback) (() => this.PlayAllCompleteAnimation((EventDelegate.Callback) (() => this.OnSuccessSend(cardData, allBingoData.deliveryData)))), this.GridAnimationTime));
      }
    }
    else
      GameSection.ResumeEvent(false);
  }

  private void OnSuccessSend(HomeBingo.CardData cardData, DeliveryTable.DeliveryData deliveryData)
  {
    GameSection.ChangeStayEvent("GET_REWARD", (object) new object[2]
    {
      (object) deliveryData,
      (object) cardData.eventData
    });
    GameSection.ResumeEvent(true);
    this.RefreshMissionData(this.currentCardIndex);
    this.RefreshUI();
  }

  private void PlayGridCompleteAnimation(
    HomeBingo.GridData gridData,
    bool isPlayCompleteMarkAnimation,
    EventDelegate.Callback onFinished = null)
  {
    if (gridData == null)
      return;
    UITweenCtrl tweenCtrl = gridData.tweenCtrl;
    if (Object.op_Equality((Object) tweenCtrl, (Object) null) || tweenCtrl.tweens == null || tweenCtrl.tweens.Length == 0)
      return;
    this.SetActive(gridData.transform, (Enum) HomeBingo.UI.SPR_GRID_ITEM, true);
    BoxCollider collider = ((Component) gridData.transform).GetComponent<BoxCollider>();
    if (Object.op_Inequality((Object) collider, (Object) null))
      ((Collider) collider).enabled = false;
    if (isPlayCompleteMarkAnimation)
    {
      this.UpdateReward(this.GetCurrentCard(), (HomeBingo.MissionData) gridData, true);
      this.PlayTweenComplete();
      SoundManager.PlayOneShotUISE(40000390);
    }
    if (onFinished != null)
      this.StartCoroutine(this.WaitAndDo(onFinished, this.GridAnimationTime));
    gridData.tweenCtrl.Reset();
    gridData.tweenCtrl.Play(onFinished: (EventDelegate.Callback) (() =>
    {
      this.SetActive(gridData.transform, (Enum) HomeBingo.UI.SPR_GRID_REACH, false);
      if (Object.op_Inequality((Object) collider, (Object) null))
        ((Collider) collider).enabled = true;
      this.SetActive(gridData.transform, (Enum) HomeBingo.UI.SPR_GRID_ITEM, false);
    }));
  }

  private void PlayTweenComplete()
  {
    HomeBingo.CardData currentCard = this.GetCurrentCard();
    UITweenCtrl rewardTweenlCtrl = currentCard.completeRewardTweenlCtrl;
    if (Object.op_Equality((Object) rewardTweenlCtrl, (Object) null))
      return;
    this.SetActive(currentCard.completeRewardTweenlRoot, true);
    rewardTweenlCtrl.Reset();
    rewardTweenlCtrl.Play();
  }

  private void PlayCenterAnimation()
  {
    if (this.centerGrid == null || Object.op_Equality((Object) this.centerGrid.transform, (Object) null))
      return;
    this.SetActive(this.centerGrid.transform, (Enum) HomeBingo.UI.SPR_GRID_ITEM, true);
    BoxCollider collider = ((Component) this.centerGrid.transform).GetComponent<BoxCollider>();
    if (Object.op_Inequality((Object) collider, (Object) null))
      ((Collider) collider).enabled = false;
    this.centerGrid.tweenCtrl.Reset();
    this.centerGrid.tweenCtrl.Play(onFinished: (EventDelegate.Callback) (() =>
    {
      this.SetActive(this.centerGrid.transform, (Enum) HomeBingo.UI.SPR_GRID_REACH, false);
      if (Object.op_Inequality((Object) collider, (Object) null))
        ((Collider) collider).enabled = true;
      this.SetActive(this.centerGrid.transform, (Enum) HomeBingo.UI.SPR_GRID_ITEM, false);
    }));
  }

  private void PlayBingoAnimation(EventDelegate.Callback onFinished = null)
  {
    SoundManager.PlayOneshotJingle(40000391);
    this.StartCoroutine(this.WaitAndDo(onFinished, this.BingoAnimationTime));
    this.SetActive((Enum) HomeBingo.UI.OBJ_BINGO_ANIMATION, true);
  }

  private void PlayAllCompleteAnimation(EventDelegate.Callback onFinished = null)
  {
    Transform ctrl = this.GetCtrl((Enum) HomeBingo.UI.OBJ_COMPLETE);
    if (Object.op_Equality((Object) ctrl, (Object) null))
    {
      if (onFinished == null)
        return;
      onFinished();
    }
    else
    {
      this.SetActive(ctrl, true);
      UITweenCtrl component = ((Component) ctrl).GetComponent<UITweenCtrl>();
      if (Object.op_Equality((Object) component, (Object) null))
      {
        if (onFinished == null)
          return;
        onFinished();
      }
      else
      {
        SoundManager.PlayOneshotJingle(40000392);
        ((Renderer) ((Component) ((Component) this.GetCtrl((Enum) HomeBingo.UI.OBJ_PARTICLE_2)).GetComponent<ParticleSystem>()).GetComponent<ParticleSystemRenderer>()).sharedMaterial.renderQueue = 4000;
        this.StartCoroutine(this.WaitAndDo(onFinished, this.CompleteAnimationTime));
        component.Reset();
        component.Play();
      }
    }
  }

  private IEnumerator WaitAndDo(EventDelegate.Callback onFinished, float time)
  {
    if (onFinished != null)
    {
      yield return (object) new WaitForSeconds(time);
      onFinished();
    }
  }

  private void OnQuery_SELECT_CENTER()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    int index = (int) eventData[0];
    int num = (int) eventData[1];
    this.SetActive(this.cardDataList[index].cardTransform, (Enum) HomeBingo.UI.OBJ_COMPLETE_MARK, false);
    this.SetBannerActvie(this.cardDataList[index], true);
  }

  private void OnQuery_CLOSE()
  {
    MonoBehaviourSingleton<DeliveryManager>.I.DeleteCleardDeliveryId();
  }

  private void OnQuery_INFO_RULE()
  {
    string str = "";
    if (this.GetCurrentCard() != null && this.GetCurrentCard().eventData != null)
      str = "?eid=" + (object) this.GetCurrentCard().eventData.eventId;
    GameSection.SetEventData((object) (WebViewManager.BingoRule + str));
  }

  private void OnQuery_INFO_REWARD()
  {
    string str = "";
    if (this.GetCurrentCard() != null && this.GetCurrentCard().eventData != null)
      str = "?eid=" + (object) this.GetCurrentCard().eventData.eventId;
    GameSection.SetEventData((object) (WebViewManager.BingoReward + str));
  }

  private void OnQuery_LEFT()
  {
    if (this.currentCardIndex >= this.cardDataList.Count - 1)
      return;
    GameSection.StayEvent();
    this.MoveToSide(false, this.currentCardIndex);
    ++this.currentCardIndex;
    this.MoveToCenter(false, this.currentCardIndex, (EventDelegate.Callback) (() => GameSection.ResumeEvent(true)));
  }

  private void OnQuery_RIGHT()
  {
    if (this.currentCardIndex <= 0)
      return;
    GameSection.StayEvent();
    this.MoveToSide(true, this.currentCardIndex);
    --this.currentCardIndex;
    this.MoveToCenter(true, this.currentCardIndex, (EventDelegate.Callback) (() => GameSection.ResumeEvent(true)));
  }

  private void MoveToSide(bool toRight, int cardIndex, EventDelegate.Callback OnEndEvent = null)
  {
    HomeBingo.CardData cardData = this.cardDataList[cardIndex];
    Vector3 localPosition = cardData.cardTransform.localPosition;
    TweenPosition cardTween = cardData.cardTween;
    UITweenCtrl cardTweenCtrl = cardData.cardTweenCtrl;
    localPosition.x = 0.0f;
    cardTween.from = localPosition;
    localPosition.x = toRight ? this.StandByX : -this.StandByX;
    cardTween.to = localPosition;
    cardTweenCtrl.Reset();
    cardTweenCtrl.Play(onFinished: OnEndEvent);
  }

  protected virtual void ChangeCard(int cardIndex, System.Action callback) => callback();

  private void MoveToCenter(bool fromLeft, int cardIndex, EventDelegate.Callback OnEndEvent = null)
  {
    HomeBingo.CardData cardData = this.cardDataList[cardIndex];
    Vector3 localPosition = cardData.cardTransform.localPosition;
    TweenPosition cardTween = cardData.cardTween;
    UITweenCtrl cardTweenCtrl = cardData.cardTweenCtrl;
    localPosition.x = fromLeft ? -this.StandByX : this.StandByX;
    cardTween.from = localPosition;
    localPosition.x = 0.0f;
    cardTween.to = localPosition;
    cardTweenCtrl.Reset();
    cardTweenCtrl.Play(onFinished: OnEndEvent);
  }

  private HomeBingo.CardData GetSelectedCardData()
  {
    return this.currentCardIndex >= this.cardDataList.Count ? (HomeBingo.CardData) null : this.cardDataList[this.currentCardIndex];
  }

  protected void SetEndDateLabel()
  {
    this.SetActive((Enum) HomeBingo.UI.LBL_EVENT_END, false);
    Network.EventData eventDataFromList = this.GetEventDataFromList(this.GetCurrentIndex());
    if (eventDataFromList == null || !eventDataFromList.HasEndDate())
      return;
    DateTime dateTime = eventDataFromList.endDate.ConvToDateTime();
    dateTime = dateTime.AddMinutes(-1.0);
    this.SetLabelText((Enum) HomeBingo.UI.LBL_EVENT_END, dateTime.ToString("M/d(ddd)H:mmまで", (IFormatProvider) new CultureInfo("ja-JP")));
    this.SetActive((Enum) HomeBingo.UI.LBL_EVENT_END, false);
  }

  private enum UI
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

  private enum AUDIO
  {
    ONE = 40000390, // 0x02625B86
    BINGO = 40000391, // 0x02625B87
    ALL_BINGO = 40000392, // 0x02625B88
  }

  protected class CardData
  {
    public Network.EventData eventData;
    public Transform cardTransform;
    public UITweenCtrl cardTweenCtrl;
    public TweenPosition cardTween;
    public UITweenCtrl completeRewardTweenlCtrl;
    public Transform completeRewardTweenlRoot;
    public List<HomeBingo.GridData> gridDataList = new List<HomeBingo.GridData>();
    public List<HomeBingo.BingoData> bingoDataList = new List<HomeBingo.BingoData>();
    public HomeBingo.AllBingoData allBingoData;
  }

  public class MissionData
  {
    public DeliveryTable.DeliveryData deliveryData;
    public Delivery deliveryInfo;
    public bool isCompleted;

    public MissionData(
      DeliveryTable.DeliveryData deliveryData,
      Delivery deliveryInfo,
      bool isCompleted)
    {
      this.deliveryData = deliveryData;
      this.deliveryInfo = deliveryInfo;
      this.isCompleted = isCompleted;
    }
  }

  public class GridData(
    DeliveryTable.DeliveryData deliveryData,
    Delivery deliveryInfo,
    bool isCompleted) : HomeBingo.MissionData(deliveryData, deliveryInfo, isCompleted)
  {
    public UITweenCtrl tweenCtrl;
    public Transform transform;
    public string spriteName;

    public void SetEntity(Transform transform, string spriteName)
    {
      this.transform = transform;
      this.tweenCtrl = ((Component) transform).GetComponent<UITweenCtrl>();
      this.spriteName = spriteName;
    }
  }

  protected class BingoData(
    DeliveryTable.DeliveryData deliveryData,
    Delivery deliveryInfo,
    bool isCompleted) : HomeBingo.MissionData(deliveryData, deliveryInfo, isCompleted)
  {
    public List<HomeBingo.GridData> childrenGridList = new List<HomeBingo.GridData>();

    public void SetChildren(List<HomeBingo.GridData> childrenGridList)
    {
      this.childrenGridList = childrenGridList;
    }
  }

  protected class AllBingoData(
    DeliveryTable.DeliveryData deliveryData,
    Delivery deliveryInfo,
    bool isCompleted) : HomeBingo.MissionData(deliveryData, deliveryInfo, isCompleted)
  {
  }
}
