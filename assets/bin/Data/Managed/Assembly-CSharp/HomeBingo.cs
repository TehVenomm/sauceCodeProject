using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class HomeBingo : GameSection
{
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
		LBL_EVENT_END
	}

	private enum AUDIO
	{
		ONE = 40000390,
		BINGO,
		ALL_BINGO
	}

	protected class CardData
	{
		public Network.EventData eventData;

		public Transform cardTransform;

		public UITweenCtrl cardTweenCtrl;

		public TweenPosition cardTween;

		public UITweenCtrl completeRewardTweenlCtrl;

		public Transform completeRewardTweenlRoot;

		public List<GridData> gridDataList = new List<GridData>();

		public List<BingoData> bingoDataList = new List<BingoData>();

		public AllBingoData allBingoData;
	}

	public class MissionData
	{
		public DeliveryTable.DeliveryData deliveryData;

		public Delivery deliveryInfo;

		public bool isCompleted;

		public MissionData(DeliveryTable.DeliveryData deliveryData, Delivery deliveryInfo, bool isCompleted)
		{
			this.deliveryData = deliveryData;
			this.deliveryInfo = deliveryInfo;
			this.isCompleted = isCompleted;
		}
	}

	public class GridData : MissionData
	{
		public UITweenCtrl tweenCtrl;

		public Transform transform;

		public string spriteName;

		public GridData(DeliveryTable.DeliveryData deliveryData, Delivery deliveryInfo, bool isCompleted)
			: base(deliveryData, deliveryInfo, isCompleted)
		{
		}

		public void SetEntity(Transform transform, string spriteName)
		{
			this.transform = transform;
			tweenCtrl = transform.GetComponent<UITweenCtrl>();
			this.spriteName = spriteName;
		}
	}

	protected class BingoData : MissionData
	{
		public List<GridData> childrenGridList = new List<GridData>();

		public BingoData(DeliveryTable.DeliveryData deliveryData, Delivery deliveryInfo, bool isCompleted)
			: base(deliveryData, deliveryInfo, isCompleted)
		{
		}

		public void SetChildren(List<GridData> childrenGridList)
		{
			this.childrenGridList = childrenGridList;
		}
	}

	protected class AllBingoData : MissionData
	{
		public AllBingoData(DeliveryTable.DeliveryData deliveryData, Delivery deliveryInfo, bool isCompleted)
			: base(deliveryData, deliveryInfo, isCompleted)
		{
		}
	}

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

	protected List<CardData> cardDataList = new List<CardData>(3);

	protected GridData centerGrid;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		if (isLocalInitialized)
		{
			base.Initialize();
		}
		else
		{
			InitializeOnce(delegate
			{
				base.Initialize();
			});
		}
	}

	protected virtual void InitializeOnce(Action callback)
	{
		isLocalInitialized = true;
		itemNum = ColmunNum * ColmunNum;
		centerIndex = itemNum / 2;
		object eventData = GameSection.GetEventData();
		if (eventData is bool)
		{
			isComeFromAutoEvent = (bool)eventData;
		}
		isFirstUpdate = true;
		LoadingQueue load_queue = new LoadingQueue(this);
		CacheAudio(load_queue);
		StartCoroutine(DoInitialize(callback));
	}

	protected virtual IEnumerator DoInitialize(Action callback)
	{
		eventDataList = MonoBehaviourSingleton<QuestManager>.I.GetValidBingoDataListInSection();
		if (eventDataList == null || eventDataList.Count <= 0)
		{
			RequestNotExistBingo();
			HideAll();
			base.Initialize();
		}
		else
		{
			SetCurrentIndex(0);
			InitCardDataList(eventDataList);
			int index = GetCurrentIndex();
			yield return (object)StartCoroutine(LoadBanner(GetEventDataFromList(index), index, null));
			callback();
		}
	}

	protected IEnumerator LoadBanner(Network.EventData eventData, int index, Action<bool> callback = null)
	{
		if (eventData == null)
		{
			callback?.Invoke(false);
		}
		else
		{
			bool isSuccess = false;
			string resourceName = ResourceName.GetEventBG(eventData.bannerId);
			Hash128 hash = default(Hash128);
			if ((UnityEngine.Object)MonoBehaviourSingleton<ResourceManager>.I.manifest != (UnityEngine.Object)null)
			{
				hash = MonoBehaviourSingleton<ResourceManager>.I.manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BG.ToAssetBundleName(resourceName));
			}
			if ((UnityEngine.Object)MonoBehaviourSingleton<ResourceManager>.I.manifest == (UnityEngine.Object)null || hash.isValid)
			{
				LoadingQueue load_queue = new LoadingQueue(this);
				LoadObject lo_bg = load_queue.Load(RESOURCE_CATEGORY.EVENT_BG, resourceName, false);
				if (load_queue.IsLoading())
				{
					yield return (object)load_queue.Wait();
				}
				SetTexture(texture: lo_bg.loadedObject as Texture2D, root: cardDataList[index].cardTransform, texture_enum: UI.TEX_EVENT_BG);
				isSuccess = true;
			}
			callback?.Invoke(isSuccess);
		}
	}

	protected void HideAll()
	{
		SetActive(UI.OBJ_BTN_ROOT, false);
		SetActive(UI.OBJ_ROOT_CARDS, false);
		SetActive(UI.BTN_CLOSE, false);
	}

	protected void RequestNotExistBingo()
	{
		RequestEvent("NOT_EXIST_BINGO", null);
	}

	protected void InitCardDataList(List<Network.EventData> eventDataList)
	{
		cardDataList.Clear();
		int i = 0;
		for (int count = eventDataList.Count; i < count; i++)
		{
			InitCardData(i, eventDataList[i]);
		}
	}

	protected CardData GetCurrentCard()
	{
		return cardDataList[GetCurrentIndex()];
	}

	protected Network.EventData GetEventDataFromList(int index)
	{
		if (eventDataList.IsNullOrEmpty())
		{
			return null;
		}
		if (index >= eventDataList.Count)
		{
			return null;
		}
		return eventDataList[index];
	}

	protected int GetCurrentIndex()
	{
		return currentCardIndex;
	}

	protected virtual void SetCurrentIndex(int index)
	{
		currentCardIndex = index;
	}

	protected void InitCardData(int index, Network.EventData eventData)
	{
		CardData cardData = new CardData();
		cardData.eventData = eventData;
		cardDataList.Add(cardData);
		InitCard(index);
		RefreshMissionData(index);
	}

	protected bool IsPlayableVersion(out Network.EventData notPlayableEventData)
	{
		notPlayableEventData = null;
		if (cardDataList == null || cardDataList.Count <= 0)
		{
			return true;
		}
		Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
		int i = 0;
		for (int count = cardDataList.Count; i < count; i++)
		{
			Network.EventData eventData = cardDataList[i].eventData;
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
		CardData cardData = cardDataList[cardIndex];
		Transform transform;
		if (cardIndex == 0)
		{
			transform = GetCtrl(UI.OBJ_CARD);
		}
		else
		{
			transform = UnityEngine.Object.Instantiate(GetCtrl(UI.OBJ_CARD));
			transform.SetParent(GetCtrl(UI.OBJ_ROOT_CARDS), false);
		}
		cardData.cardTransform = transform;
		cardData.cardTweenCtrl = FindCtrl(transform, UI.OBJ_CARD_TWEEN_CTRL).GetComponent<UITweenCtrl>();
		Transform transform2 = FindCtrl(transform, UI.OBJ_TWEEN_CARD);
		cardData.cardTween = transform2.GetComponent<TweenPosition>();
		Transform transform3 = FindCtrl(GetCurrentCard().cardTransform, UI.OBJ_COMPLETE_TWEEN_ROOT);
		if ((UnityEngine.Object)transform3 != (UnityEngine.Object)null)
		{
			cardData.completeRewardTweenlRoot = transform3;
			UITweenCtrl component = transform3.GetComponent<UITweenCtrl>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				cardData.completeRewardTweenlCtrl = component;
			}
		}
		if (cardIndex != currentCardIndex)
		{
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = StandByX;
			transform2.localPosition = localPosition;
		}
	}

	protected void RefreshMissionData(int eventIndex)
	{
		CardData cardData = cardDataList[eventIndex];
		cardData.gridDataList.Clear();
		cardData.bingoDataList.Clear();
		AddDataNotCompleted(cardData);
		AddDataCompleted(cardData);
		SetBingosChildrenData(cardData);
		if (cardData.gridDataList == null || cardData.gridDataList.Count <= 0)
		{
			RequestNotExistBingo();
			HideAll();
		}
		else
		{
			cardData.gridDataList = (from g in cardData.gridDataList
			orderby g.deliveryData.id
			select g).ToList();
		}
	}

	protected void AddDataNotCompleted(CardData cardData)
	{
		Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
		int i = 0;
		for (int num = deliveryList.Length; i < num; i++)
		{
			Delivery delivery = deliveryList[i];
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery.dId);
			if (deliveryTableData == null)
			{
				Log.Warning("DeliveryTable Not Found : dId " + delivery.dId);
			}
			else if (deliveryTableData.IsEvent() && deliveryTableData.eventID == cardData.eventData.eventId)
			{
				AddMissionData(deliveryTableData, delivery, cardData, false);
			}
		}
	}

	protected void AddDataCompleted(CardData cardData)
	{
		List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
		int i = 0;
		for (int count = clearStatusDelivery.Count; i < count; i++)
		{
			ClearStatusDelivery clearStatusDelivery2 = clearStatusDelivery[i];
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)clearStatusDelivery2.deliveryId);
			if (deliveryTableData == null)
			{
				Log.Warning("DeliveryTable Not Found : dId " + clearStatusDelivery2.deliveryId);
			}
			else if (deliveryTableData.IsEvent() && deliveryTableData.eventID == cardData.eventData.eventId && clearStatusDelivery2.deliveryStatus == 3)
			{
				AddMissionData(deliveryTableData, null, cardData, true);
			}
		}
	}

	protected void AddMissionData(DeliveryTable.DeliveryData data, Delivery info, CardData cardData, bool isCompleted)
	{
		switch (data.subType)
		{
		case DELIVERY_SUB_TYPE.ALL_BINGO:
			cardData.allBingoData = new AllBingoData(data, info, isCompleted);
			break;
		case DELIVERY_SUB_TYPE.ROW_BINGO:
			cardData.bingoDataList.Add(new BingoData(data, info, isCompleted));
			break;
		default:
			cardData.gridDataList.Add(new GridData(data, info, isCompleted));
			break;
		}
	}

	protected void SetBingosChildrenData(CardData cardData)
	{
		List<BingoData> bingoDataList = cardData.bingoDataList;
		int i = 0;
		for (int count = bingoDataList.Count; i < count; i++)
		{
			BingoData bingoData = bingoDataList[i];
			SetABingoChildrenData(cardData, bingoData);
		}
	}

	private void SetABingoChildrenData(CardData cardData, BingoData bingoData)
	{
		DeliveryTable.DeliveryData.NeedData[] needs = bingoData.deliveryData.needs;
		int i = 0;
		for (int num = needs.Length; i < num; i++)
		{
			DeliveryTable.DeliveryData.NeedData need = needs[i];
			if ((uint)need.needId != 0)
			{
				GridData gridData = cardData.gridDataList.FirstOrDefault((GridData grid) => grid.deliveryData.id == (uint)need.needId);
				if (gridData != null && gridData.deliveryData != null)
				{
					bingoData.childrenGridList.Add(gridData);
				}
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
		if (!IsPlayableVersion(out Network.EventData notPlayableEventData))
		{
			string event_data = string.Format(base.sectionData.GetText("REQUIRE_HIGHER_VERSION"), notPlayableEventData.minVersion);
			RequestEvent("SELECT_VERSION", event_data);
		}
		else if (!isComeFromAutoEvent)
		{
			AutoCompleteAchievableDelivery();
		}
	}

	protected void OnCloseDialog()
	{
		AutoCompleteAchievableDelivery();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((flags & NOTIFY_FLAG.TRANSITION_END) != (NOTIFY_FLAG)0L)
		{
			if (!IsPlayableVersion(out Network.EventData notPlayableEventData))
			{
				StartCoroutine(WaitAndStartNotPlayable(notPlayableEventData));
			}
			else
			{
				StartCoroutine(WaitAndStartAutoComplete());
			}
		}
	}

	protected IEnumerator WaitAndStartAutoComplete()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			yield return (object)null;
		}
		AutoCompleteAchievableDelivery();
	}

	private IEnumerator WaitAndStartNotPlayable(Network.EventData eventData)
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			yield return (object)null;
		}
		string message = string.Format(base.sectionData.GetText("REQUIRE_HIGHER_VERSION"), eventData.minVersion);
		DispatchEvent("SELECT_VERSION", message);
	}

	protected void AutoCompleteAchievableDelivery()
	{
		CardData selectedCardData = GetSelectedCardData();
		if (selectedCardData != null)
		{
			BingoData bingoData = FindBingoMissionCompleted(selectedCardData);
			if (bingoData != null)
			{
				DoCompleteBingo(bingoData);
			}
			else
			{
				GridData gridData = FindGridMissionCompleted(selectedCardData);
				if (gridData != null)
				{
					DoCompleteGrid(gridData);
				}
				else
				{
					AllBingoData allBingoData = FindAllBingoMissionCompleted(selectedCardData);
					if (allBingoData != null)
					{
						DoCompleteAllBingo(allBingoData);
					}
				}
			}
		}
	}

	private void DoCompleteGrid(GridData completedGridData)
	{
		string event_name = "COMPLETE_GRID";
		if (isComeFromAutoEvent || isComeFromLeftRightEvent)
		{
			DispatchEvent(event_name, completedGridData);
			isComeFromAutoEvent = false;
			isComeFromLeftRightEvent = false;
		}
		else
		{
			RequestEvent(event_name, completedGridData);
		}
	}

	private void DoCompleteBingo(BingoData completedData)
	{
		string event_name = "COMPLETE_BINGO";
		if (isComeFromAutoEvent || isComeFromLeftRightEvent)
		{
			DispatchEvent(event_name, completedData);
			isComeFromAutoEvent = false;
			isComeFromLeftRightEvent = false;
		}
		else
		{
			RequestEvent(event_name, completedData);
		}
	}

	private void DoCompleteAllBingo(AllBingoData completedData)
	{
		string event_name = "COMPLETE_ALL_BINGO";
		if (isComeFromAutoEvent || isComeFromLeftRightEvent)
		{
			DispatchEvent(event_name, completedData);
			isComeFromAutoEvent = false;
			isComeFromLeftRightEvent = false;
		}
		else
		{
			RequestEvent(event_name, completedData);
		}
	}

	private GridData FindGridMissionCompleted(CardData cardData)
	{
		List<GridData> gridDataList = cardData.gridDataList;
		int i = 0;
		for (int count = gridDataList.Count; i < count; i++)
		{
			GridData gridData = gridDataList[i];
			if (!gridData.isCompleted && MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)gridData.deliveryData.id))
			{
				return gridData;
			}
		}
		return null;
	}

	private BingoData FindBingoMissionCompleted(CardData cardData)
	{
		List<BingoData> bingoDataList = cardData.bingoDataList;
		int i = 0;
		for (int count = bingoDataList.Count; i < count; i++)
		{
			BingoData bingoData = bingoDataList[i];
			if (!bingoData.isCompleted && MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)bingoData.deliveryData.id))
			{
				return bingoData;
			}
		}
		return null;
	}

	private AllBingoData FindAllBingoMissionCompleted(CardData cardData)
	{
		AllBingoData allBingoData = cardData.allBingoData;
		if (allBingoData == null)
		{
			return null;
		}
		if (allBingoData.isCompleted)
		{
			return null;
		}
		if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)allBingoData.deliveryData.id))
		{
			return null;
		}
		return allBingoData;
	}

	public override void UpdateUI()
	{
		if (cardDataList != null && cardDataList.Count > 0)
		{
			if (currentCardIndex >= cardDataList.Count)
			{
				currentCardIndex = 0;
			}
			if (cardDataList[currentCardIndex].gridDataList.Count > 0)
			{
				int count = cardDataList.Count;
				SetActive(UI.OBJ_BTN_ROOT, false);
				if (isFirstUpdate)
				{
					isFirstUpdate = false;
					SetActive(UI.OBJ_COMPLETE, false);
					bool isCompleted = GetCurrentCard().allBingoData.isCompleted;
					SetActive(UI.OBJ_COMPLETE_STAY, isCompleted);
					if (isCompleted)
					{
						Transform ctrl = GetCtrl(UI.OBJ_COMPLETE_STAY);
						UITweenCtrl component = ctrl.GetComponent<UITweenCtrl>();
						component.Reset();
						component.Play(true, null);
					}
				}
				SetActive(UI.OBJ_BINGO_ANIMATION, false);
				for (int i = 0; i < count; i++)
				{
					UpdateCard(cardDataList[i], i);
				}
				SetEndDateLabel();
			}
		}
	}

	protected virtual void UpdateCard(CardData cardData, int cardIndex)
	{
		Transform cardTransform = cardData.cardTransform;
		UpdateBingoName(cardTransform, cardData.eventData);
		UpdateEndData(cardTransform, cardData.eventData);
		SetGrid(cardTransform, UI.GRD_BINGO_LIST, GridItemName, cardData.gridDataList.Count + 1, false, delegate(int i, Transform t, bool b)
		{
			if (i == centerIndex)
			{
				SetUpCenterItem(cardIndex, i, t, b);
			}
			else
			{
				SetUpGridItem(cardIndex, i, t, b);
			}
		});
		UpdateReachs(cardData);
	}

	protected void UpdateBingoName(Transform t, Network.EventData eventData)
	{
	}

	protected void UpdateEndData(Transform t, Network.EventData eventData)
	{
	}

	protected void UpdateReachs(CardData cardData)
	{
		List<BingoData> bingoDataList = cardData.bingoDataList;
		int i = 0;
		for (int count = bingoDataList.Count; i < count; i++)
		{
			UpdateAReach(bingoDataList[i]);
		}
	}

	protected void UpdateAReach(BingoData bingoData)
	{
		List<GridData> childrenGridList = bingoData.childrenGridList;
		GridData gridData = null;
		int num = 0;
		int i = 0;
		for (int count = childrenGridList.Count; i < count; i++)
		{
			if (!childrenGridList[i].isCompleted)
			{
				gridData = childrenGridList[i];
				num++;
			}
			if (num >= 2)
			{
				return;
			}
		}
		if (gridData != null)
		{
			SetReachVisual(gridData, true);
		}
	}

	protected void SetReachVisual(GridData gridData, bool isActive)
	{
		SetActive(gridData.transform, UI.SPR_GRID_REACH, isActive);
	}

	protected virtual void SetUpGridItem(int cardIndex, int index, Transform t, bool recycle)
	{
		if (index > centerIndex)
		{
			index--;
		}
		int num = index + 1;
		GridData gridData = cardDataList[cardIndex].gridDataList[index];
		gridData.SetEntity(t, GetGridSpriteName(index));
		BoxCollider component = t.GetComponent<BoxCollider>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.enabled = true;
		}
		SetActive(t, UI.SPR_GRID_ITEM, !gridData.isCompleted);
		SetSprite(t, UI.SPR_GRID_ITEM, gridData.spriteName);
		SetLabelText(t, UI.LBL_GRID_ITEM, num.ToString());
		SetEvent(t, "SELECT_GRID_ITEM", new object[2]
		{
			cardIndex,
			index
		});
	}

	protected virtual string GetGridSpriteName(int index)
	{
		if (index >= centerIndex)
		{
			index++;
		}
		return $"{index + 1:d2}";
	}

	private string GetCenterSpriteName()
	{
		return "13";
	}

	private void SetUpCenterItem(int cardIndex, int index, Transform t, bool recycle)
	{
		centerGrid = new GridData(null, null, true);
		centerGrid.SetEntity(t, GetCenterSpriteName());
		SetLabelText(t, UI.LBL_GRID_ITEM, "C");
		SetActive(t, UI.SPR_GRID_ITEM, false);
		SetSprite(t, UI.SPR_GRID_ITEM, GetCenterSpriteName());
		SetEvent(t, "SELECT_CENTER", new object[2]
		{
			cardIndex,
			index
		});
	}

	private void UpdateRewardIcon(Transform cardTransform, MissionData gridData)
	{
		DeliveryRewardTable.DeliveryRewardData[] rewards = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData(gridData.deliveryData.id);
		int exp = 0;
		if (rewards != null && rewards.Length > 0)
		{
			SetGrid(cardTransform, UI.GRD_REWARD, string.Empty, rewards.Length, false, delegate(int index, Transform t, bool is_recycle)
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
					ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(reward.type, reward.item_id, t, reward.num, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_DETAIL);
					SetMaterialInfo(itemIcon.transform, reward.type, reward.item_id, null);
					itemIcon.SetRewardBG(true);
				}
				SetActive(t, is_visible);
			});
		}
	}

	private void OnQuery_SELECT_GRID_ITEM()
	{
		object[] array = GameSection.GetEventData() as object[];
		int index = (int)array[0];
		int index2 = (int)array[1];
		CardData cardData = cardDataList[index];
		GridData missionData = cardData.gridDataList[index2];
		UpdateReward(cardData, missionData, false);
	}

	private void UpdateReward(CardData cardData, MissionData missionData, bool isComplete = false)
	{
		UpdateRewardIcon(cardData.cardTransform, missionData);
		UpdateRewardNumber(cardData, missionData);
		SetBannerActvie(cardData, false);
		SetActive(cardData.completeRewardTweenlRoot, false);
		SetActive(cardData.cardTransform, UI.OBJ_COMPLETE_MARK, missionData.isCompleted);
		DeliveryRewardTable.DeliveryRewardData[] deliveryRewardTableData = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData(missionData.deliveryData.id);
		if (deliveryRewardTableData != null && deliveryRewardTableData.Length > 0)
		{
			DeliveryRewardTable.DeliveryRewardData deliveryRewardData = deliveryRewardTableData[0];
			SetLabelText(cardData.cardTransform, UI.LBL_REWARD, missionData.deliveryData.npcComment);
			MonoBehaviourSingleton<DeliveryManager>.I.GetAllProgressDelivery((int)missionData.deliveryData.id, out int have, out int need);
			if (isComplete || missionData.isCompleted)
			{
				have = need;
			}
			SetActive(cardData.cardTransform, UI.LBL_REWRAD_COUNT, true);
			SetLabelText(cardData.cardTransform, UI.LBL_REWRAD_COUNT, have + "/" + need);
		}
	}

	protected virtual void UpdateRewardNumber(CardData cardData, MissionData missionData)
	{
		if (missionData is GridData)
		{
			GridData gridData = missionData as GridData;
			SetActive(cardData.cardTransform, UI.SPR_REWARD_GRID_ITEM, true);
			SetSprite(cardData.cardTransform, UI.SPR_REWARD_GRID_ITEM, gridData.spriteName);
		}
		else
		{
			SetActive(cardData.cardTransform, UI.SPR_REWARD_GRID_ITEM, false);
		}
	}

	protected void HideReward(CardData cardData)
	{
		SetActive(cardData.cardTransform, UI.SPR_REWARD_GRID_ITEM, false);
		SetBannerActvie(cardData, true);
		SetActive(cardData.completeRewardTweenlRoot, true);
		SetActive(cardData.cardTransform, UI.OBJ_COMPLETE_MARK, false);
	}

	private void SetBannerActvie(CardData cardData, bool isActive)
	{
		SetActive(cardData.cardTransform, UI.OBJ_BANNER_ROOT, isActive);
	}

	private void OnQuery_GET_REWARD()
	{
	}

	private void OnQuery_COMPLETE_GRID()
	{
		GridData gridData = GameSection.GetEventData() as GridData;
		GameSection.StayEvent();
		Delivery deliveryInfo = gridData.deliveryInfo;
		CardData cardData = GetCurrentCard();
		MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
		MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo.uId, false, delegate(bool is_success, DeliveryRewardList recv_reward)
		{
			if (is_success)
			{
				StartCoroutine(WaitAndDo(delegate
				{
					PlayGridCompleteAnimation(gridData, true, delegate
					{
						GameSection.ChangeStayEvent("GET_REWARD", new object[2]
						{
							gridData.deliveryData,
							cardData.eventData
						});
						GameSection.ResumeEvent(true, null);
						RefreshMissionData(currentCardIndex);
						RefreshUI();
					});
				}, 0.2f));
			}
			else
			{
				GameSection.ResumeEvent(false, null);
			}
		});
	}

	private void OnQuery_COMPLETE_BINGO()
	{
		BingoData bingoData = GameSection.GetEventData() as BingoData;
		GameSection.StayEvent();
		Delivery deliveryInfo = bingoData.deliveryInfo;
		CardData cardData = GetCurrentCard();
		DeliveryTable.DeliveryData.NeedData[] needs = bingoData.deliveryData.needs;
		MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
		MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo.uId, false, delegate(bool is_success, DeliveryRewardList recv_reward)
		{
			StartCoroutine(WaitAndDo(delegate
			{
				OnEndSendBingoMission(is_success, cardData, bingoData, bingoData.childrenGridList);
			}, 0.2f));
		});
	}

	private void OnQuery_COMPLETE_ALL_BINGO()
	{
		AllBingoData allBingoData = GameSection.GetEventData() as AllBingoData;
		GameSection.StayEvent();
		Delivery deliveryInfo = allBingoData.deliveryInfo;
		CardData cardData = GetCurrentCard();
		List<GridData> gridDatas = cardData.gridDataList;
		MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
		MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo.uId, false, delegate(bool is_success, DeliveryRewardList recv_reward)
		{
			StartCoroutine(WaitAndDo(delegate
			{
				OnEndSendAllMission(is_success, cardData, allBingoData, gridDatas);
			}, 0.2f));
		});
	}

	private void OnEndSendBingoMission(bool is_success, CardData cardData, BingoData bingoData, List<GridData> gridDatas)
	{
		if (is_success)
		{
			if (gridDatas == null || gridDatas.Count <= 0)
			{
				OnSuccessSend(cardData, bingoData.deliveryData);
			}
			else
			{
				int i = 0;
				for (int count = gridDatas.Count; i < count; i++)
				{
					PlayGridCompleteAnimation(gridDatas[i], false, null);
				}
				UpdateReward(GetCurrentCard(), bingoData, true);
				PlayTweenComplete();
				if (gridDatas.Count <= 4)
				{
					PlayCenterAnimation();
				}
				StartCoroutine(WaitAndDo(delegate
				{
					PlayBingoAnimation(delegate
					{
						OnSuccessSend(cardData, bingoData.deliveryData);
					});
				}, GridAnimationTime));
			}
		}
		else
		{
			GameSection.ResumeEvent(false, null);
		}
	}

	private void OnEndSendAllMission(bool is_success, CardData cardData, AllBingoData allBingoData, List<GridData> gridDatas)
	{
		if (is_success)
		{
			if (gridDatas == null || gridDatas.Count <= 0)
			{
				PlayAllCompleteAnimation(delegate
				{
					OnSuccessSend(cardData, allBingoData.deliveryData);
				});
			}
			else
			{
				int i = 0;
				for (int count = gridDatas.Count; i < count; i++)
				{
					PlayGridCompleteAnimation(gridDatas[i], false, null);
				}
				PlayCenterAnimation();
				UpdateReward(GetCurrentCard(), allBingoData, true);
				PlayTweenComplete();
				StartCoroutine(WaitAndDo(delegate
				{
					PlayAllCompleteAnimation(delegate
					{
						OnSuccessSend(cardData, allBingoData.deliveryData);
					});
				}, GridAnimationTime));
			}
		}
		else
		{
			GameSection.ResumeEvent(false, null);
		}
	}

	private void OnSuccessSend(CardData cardData, DeliveryTable.DeliveryData deliveryData)
	{
		GameSection.ChangeStayEvent("GET_REWARD", new object[2]
		{
			deliveryData,
			cardData.eventData
		});
		GameSection.ResumeEvent(true, null);
		RefreshMissionData(currentCardIndex);
		RefreshUI();
	}

	private void PlayGridCompleteAnimation(GridData gridData, bool isPlayCompleteMarkAnimation, EventDelegate.Callback onFinished = null)
	{
		if (gridData != null)
		{
			UITweenCtrl tweenCtrl = gridData.tweenCtrl;
			if (!((UnityEngine.Object)tweenCtrl == (UnityEngine.Object)null) && tweenCtrl.tweens != null && tweenCtrl.tweens.Length > 0)
			{
				SetActive(gridData.transform, UI.SPR_GRID_ITEM, true);
				BoxCollider collider = gridData.transform.GetComponent<BoxCollider>();
				if ((UnityEngine.Object)collider != (UnityEngine.Object)null)
				{
					collider.enabled = false;
				}
				if (isPlayCompleteMarkAnimation)
				{
					UpdateReward(GetCurrentCard(), gridData, true);
					PlayTweenComplete();
					SoundManager.PlayOneShotUISE(40000390);
				}
				if (onFinished != null)
				{
					StartCoroutine(WaitAndDo(onFinished, GridAnimationTime));
				}
				gridData.tweenCtrl.Reset();
				gridData.tweenCtrl.Play(true, delegate
				{
					SetActive(gridData.transform, UI.SPR_GRID_REACH, false);
					if ((UnityEngine.Object)collider != (UnityEngine.Object)null)
					{
						collider.enabled = true;
					}
					SetActive(gridData.transform, UI.SPR_GRID_ITEM, false);
				});
			}
		}
	}

	private void PlayTweenComplete()
	{
		CardData currentCard = GetCurrentCard();
		UITweenCtrl completeRewardTweenlCtrl = currentCard.completeRewardTweenlCtrl;
		if (!((UnityEngine.Object)completeRewardTweenlCtrl == (UnityEngine.Object)null))
		{
			SetActive(currentCard.completeRewardTweenlRoot, true);
			completeRewardTweenlCtrl.Reset();
			completeRewardTweenlCtrl.Play(true, null);
		}
	}

	private void PlayCenterAnimation()
	{
		if (centerGrid != null && !((UnityEngine.Object)centerGrid.transform == (UnityEngine.Object)null))
		{
			SetActive(centerGrid.transform, UI.SPR_GRID_ITEM, true);
			BoxCollider collider = centerGrid.transform.GetComponent<BoxCollider>();
			if ((UnityEngine.Object)collider != (UnityEngine.Object)null)
			{
				collider.enabled = false;
			}
			centerGrid.tweenCtrl.Reset();
			centerGrid.tweenCtrl.Play(true, delegate
			{
				SetActive(centerGrid.transform, UI.SPR_GRID_REACH, false);
				if ((UnityEngine.Object)collider != (UnityEngine.Object)null)
				{
					collider.enabled = true;
				}
				SetActive(centerGrid.transform, UI.SPR_GRID_ITEM, false);
			});
		}
	}

	private void PlayBingoAnimation(EventDelegate.Callback onFinished = null)
	{
		SoundManager.PlayOneshotJingle(40000391, null, null);
		StartCoroutine(WaitAndDo(onFinished, BingoAnimationTime));
		SetActive(UI.OBJ_BINGO_ANIMATION, true);
	}

	private void PlayAllCompleteAnimation(EventDelegate.Callback onFinished = null)
	{
		Transform ctrl = GetCtrl(UI.OBJ_COMPLETE);
		if ((UnityEngine.Object)ctrl == (UnityEngine.Object)null)
		{
			onFinished?.Invoke();
		}
		else
		{
			SetActive(ctrl, true);
			UITweenCtrl component = ctrl.GetComponent<UITweenCtrl>();
			if ((UnityEngine.Object)component == (UnityEngine.Object)null)
			{
				onFinished?.Invoke();
			}
			else
			{
				SoundManager.PlayOneshotJingle(40000392, null, null);
				ParticleSystem component2 = GetCtrl(UI.OBJ_PARTICLE_2).GetComponent<ParticleSystem>();
				component2.GetComponent<ParticleSystemRenderer>().sharedMaterial.renderQueue = 4000;
				StartCoroutine(WaitAndDo(onFinished, CompleteAnimationTime));
				component.Reset();
				component.Play(true, null);
			}
		}
	}

	private IEnumerator WaitAndDo(EventDelegate.Callback onFinished, float time)
	{
		if (onFinished != null)
		{
			yield return (object)new WaitForSeconds(time);
			onFinished();
		}
	}

	private void OnQuery_SELECT_CENTER()
	{
		object[] array = GameSection.GetEventData() as object[];
		int index = (int)array[0];
		int num = (int)array[1];
		SetActive(cardDataList[index].cardTransform, UI.OBJ_COMPLETE_MARK, false);
		SetBannerActvie(cardDataList[index], true);
	}

	private void OnQuery_CLOSE()
	{
		MonoBehaviourSingleton<DeliveryManager>.I.DeleteCleardDeliveryId();
	}

	private void OnQuery_INFO_RULE()
	{
		string str = string.Empty;
		if (GetCurrentCard() != null && GetCurrentCard().eventData != null)
		{
			str = "?eid=" + GetCurrentCard().eventData.eventId;
		}
		GameSection.SetEventData(WebViewManager.BingoRule + str);
	}

	private void OnQuery_INFO_REWARD()
	{
		string str = string.Empty;
		if (GetCurrentCard() != null && GetCurrentCard().eventData != null)
		{
			str = "?eid=" + GetCurrentCard().eventData.eventId;
		}
		GameSection.SetEventData(WebViewManager.BingoReward + str);
	}

	private void OnQuery_LEFT()
	{
		if (currentCardIndex < cardDataList.Count - 1)
		{
			GameSection.StayEvent();
			MoveToSide(false, currentCardIndex, null);
			currentCardIndex++;
			MoveToCenter(false, currentCardIndex, delegate
			{
				GameSection.ResumeEvent(true, null);
			});
		}
	}

	private void OnQuery_RIGHT()
	{
		if (currentCardIndex > 0)
		{
			GameSection.StayEvent();
			MoveToSide(true, currentCardIndex, null);
			currentCardIndex--;
			MoveToCenter(true, currentCardIndex, delegate
			{
				GameSection.ResumeEvent(true, null);
			});
		}
	}

	private void MoveToSide(bool toRight, int cardIndex, EventDelegate.Callback OnEndEvent = null)
	{
		CardData cardData = cardDataList[cardIndex];
		Transform cardTransform = cardData.cardTransform;
		Vector3 localPosition = cardTransform.localPosition;
		TweenPosition cardTween = cardData.cardTween;
		UITweenCtrl cardTweenCtrl = cardData.cardTweenCtrl;
		localPosition.x = 0f;
		cardTween.from = localPosition;
		localPosition.x = ((!toRight) ? (0f - StandByX) : StandByX);
		cardTween.to = localPosition;
		cardTweenCtrl.Reset();
		cardTweenCtrl.Play(true, OnEndEvent);
	}

	protected virtual void ChangeCard(int cardIndex, Action callback)
	{
		callback();
	}

	private void MoveToCenter(bool fromLeft, int cardIndex, EventDelegate.Callback OnEndEvent = null)
	{
		CardData cardData = cardDataList[cardIndex];
		Transform cardTransform = cardData.cardTransform;
		Vector3 localPosition = cardTransform.localPosition;
		TweenPosition cardTween = cardData.cardTween;
		UITweenCtrl cardTweenCtrl = cardData.cardTweenCtrl;
		localPosition.x = ((!fromLeft) ? StandByX : (0f - StandByX));
		cardTween.from = localPosition;
		localPosition.x = 0f;
		cardTween.to = localPosition;
		cardTweenCtrl.Reset();
		cardTweenCtrl.Play(true, OnEndEvent);
	}

	private CardData GetSelectedCardData()
	{
		if (currentCardIndex >= cardDataList.Count)
		{
			return null;
		}
		return cardDataList[currentCardIndex];
	}

	protected void SetEndDateLabel()
	{
		SetActive(UI.LBL_EVENT_END, false);
		Network.EventData eventDataFromList = GetEventDataFromList(GetCurrentIndex());
		if (eventDataFromList != null && eventDataFromList.HasEndDate())
		{
			DateTime dateTime = eventDataFromList.endDate.ConvToDateTime();
			dateTime = dateTime.AddMinutes(-1.0);
			SetLabelText(UI.LBL_EVENT_END, dateTime.ToString("M/d(ddd)H:mmまで", new CultureInfo("ja-JP")));
			SetActive(UI.LBL_EVENT_END, true);
		}
	}
}
