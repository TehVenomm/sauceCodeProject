using Network;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class HomeMiniBingo : HomeBingo
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

	private uint defaultEventId;

	protected new int ColmunNum = 3;

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

	protected override void InitializeOnce(Action callback)
	{
		isLocalInitialized = true;
		itemNum = ColmunNum * ColmunNum;
		object eventData = GameSection.GetEventData();
		if (eventData is bool)
		{
			isComeFromAutoEvent = (bool)eventData;
		}
		else
		{
			object[] array = eventData as object[];
			if (array != null)
			{
				defaultEventId = (uint)array[0];
				isComeFromAutoEvent = (bool)array[1];
			}
		}
		isFirstUpdate = true;
		LoadingQueue load_queue = new LoadingQueue(this);
		CacheAudio(load_queue);
		StartCoroutine(DoInitialize(callback));
	}

	protected override IEnumerator DoInitialize(Action callback)
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
			InitCardDataList(eventDataList);
			int defaultIndex = (from ano in eventDataList.Select((Network.EventData e, int j) => new
			{
				Content = e,
				Index = j
			})
			where ano.Content.eventId == ((_003CDoInitialize_003Ec__Iterator92)/*Error near IL_00c6: stateMachine*/)._003C_003Ef__this.defaultEventId
			select ano.Index).FirstOrDefault();
			SetCurrentIndex(defaultIndex);
			yield return (object)StartCoroutine(LoadBanner(GetEventDataFromList(GetCurrentIndex()), GetCurrentIndex(), null));
			callback();
		}
	}

	public override void StartSection()
	{
		base.StartSection();
	}

	protected override void InitCard(int cardIndex)
	{
		CardData cardData = cardDataList[cardIndex];
		Transform transform = cardData.cardTransform = GetCtrl(UI.OBJ_CARD);
	}

	public override void UpdateUI()
	{
		if (cardDataList != null && cardDataList.Count > 0 && cardDataList[currentCardIndex].gridDataList.Count > 0)
		{
			int count = cardDataList.Count;
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
			UpdateLeftRightButton();
			UpdateCard(cardDataList[currentCardIndex], currentCardIndex);
			SetEndDateLabel();
		}
	}

	private void HideLeftRightButton()
	{
		SetActive(UI.BTN_RIGHT, false);
		SetActive(UI.BTN_LEFT, false);
	}

	private void DispLeftRightButton()
	{
		SetActive(UI.BTN_RIGHT, true);
		SetActive(UI.BTN_LEFT, true);
	}

	private void UpdateLeftRightButton()
	{
		if (cardDataList.Count <= 1)
		{
			HideLeftRightButton();
		}
		else
		{
			DispLeftRightButton();
		}
	}

	protected override void UpdateCard(CardData cardData, int cardIndex)
	{
		Transform cardTransform = cardData.cardTransform;
		UpdateBingoName(cardTransform, cardData.eventData);
		UpdateEndData(cardTransform, cardData.eventData);
		for (int i = 0; i < ColmunNum * ColmunNum; i++)
		{
			SetUpGridItem(cardIndex, i, GetCtrl(UI.GRD_BINGO_LIST).FindChild("SPR_ICON_" + (i + 1)), false);
		}
		UpdateReachs(cardData);
		GetCtrl(UI.GRD_BINGO_LIST).GetComponent<UIGrid>().Reposition();
	}

	protected override void SetUpGridItem(int cardIndex, int index, Transform t, bool recycle)
	{
		int num = index + 1;
		GridData gridData = cardDataList[cardIndex].gridDataList[index];
		gridData.SetEntity(t, string.Empty);
		BoxCollider component = t.GetComponent<BoxCollider>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.enabled = true;
		}
		SetActive(t, UI.SPR_GRID_ITEM, !gridData.isCompleted);
		SetReachVisual(gridData, false);
		gridData.tweenCtrl.Reset();
		SetLabelText(t, UI.LBL_GRID_ITEM, num.ToString());
		SetEvent(t, "SELECT_GRID_ITEM", new object[2]
		{
			cardIndex,
			index
		});
	}

	protected override void UpdateRewardNumber(CardData cardData, MissionData missionData)
	{
		if (missionData is GridData)
		{
			GridData item = missionData as GridData;
			SetActive(cardData.cardTransform, UI.SPR_REWARD_GRID_ITEM, true);
			int num = cardData.gridDataList.IndexOf(item);
			UI uI = UI.SPR_REWARD_1;
			for (int i = 0; i < ColmunNum * ColmunNum; i++)
			{
				UI uI2 = uI + i;
				SetActive(uI2, num == i);
			}
		}
		else
		{
			SetActive(cardData.cardTransform, UI.SPR_REWARD_GRID_ITEM, false);
		}
	}

	private void OnQuery_LEFT()
	{
		currentCardIndex++;
		currentCardIndex %= cardDataList.Count;
		isComeFromLeftRightEvent = true;
		GameSection.StayEvent();
		ChangeCard(currentCardIndex, delegate
		{
			GameSection.ResumeEvent(true, null);
			StartCoroutine(WaitAndStartAutoComplete());
		});
	}

	private void OnQuery_RIGHT()
	{
		currentCardIndex--;
		if (currentCardIndex < 0)
		{
			currentCardIndex = cardDataList.Count - 1;
		}
		isComeFromLeftRightEvent = true;
		GameSection.StayEvent();
		ChangeCard(currentCardIndex, delegate
		{
			GameSection.ResumeEvent(true, null);
			StartCoroutine(WaitAndStartAutoComplete());
		});
	}

	protected override void ChangeCard(int cardIndex, Action callback)
	{
		SetActive(UI.OBJ_COMPLETE, false);
		bool isCompleteAll = GetCurrentCard().allBingoData.isCompleted;
		SetActive(UI.OBJ_COMPLETE_STAY, false);
		if (isCompleteAll)
		{
			Transform ctrl = GetCtrl(UI.OBJ_COMPLETE_STAY);
			UITweenCtrl component = ctrl.GetComponent<UITweenCtrl>();
			component.Reset();
		}
		StartCoroutine(LoadBanner(GetEventDataFromList(cardIndex), cardIndex, delegate
		{
			HideReward(cardDataList[cardIndex]);
			RefreshUI();
			if (isCompleteAll)
			{
				Transform ctrl2 = GetCtrl(UI.OBJ_COMPLETE_STAY);
				SetActive(UI.OBJ_COMPLETE_STAY, true);
				UITweenCtrl component2 = ctrl2.GetComponent<UITweenCtrl>();
				component2.Reset();
				component2.Play(true, null);
			}
			callback();
		}));
	}

	private CardData GetSelectedCardData()
	{
		if (currentCardIndex >= cardDataList.Count)
		{
			return null;
		}
		return cardDataList[currentCardIndex];
	}
}
