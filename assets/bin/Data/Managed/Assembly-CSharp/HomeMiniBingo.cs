using Network;
using System;
using System.Collections;
using System.Collections.Generic;
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

	public unsafe override void Initialize()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		if (isLocalInitialized)
		{
			base.Initialize();
		}
		else
		{
			InitializeOnce(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	protected override void InitializeOnce(Action callback)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
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
		this.StartCoroutine(DoInitialize(callback));
	}

	protected unsafe override IEnumerator DoInitialize(Action callback)
	{
		base.eventDataList = MonoBehaviourSingleton<QuestManager>.I.GetValidBingoDataListInSection();
		if (base.eventDataList == null || base.eventDataList.Count <= 0)
		{
			RequestNotExistBingo();
			HideAll();
			base.Initialize();
		}
		else
		{
			InitCardDataList(base.eventDataList);
			List<Network.EventData> eventDataList = base.eventDataList;
			if (_003CDoInitialize_003Ec__Iterator90._003C_003Ef__am_0024cache6 == null)
			{
				_003CDoInitialize_003Ec__Iterator90._003C_003Ef__am_0024cache6 = new Func<Network.EventData, int, _003C_003E__AnonType0<Network.EventData, int>>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			var source = Enumerable.Select(eventDataList, _003CDoInitialize_003Ec__Iterator90._003C_003Ef__am_0024cache6).Where(new Func<_003C_003E__AnonType0<Network.EventData, int>, bool>((object)/*Error near IL_00c6: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			if (_003CDoInitialize_003Ec__Iterator90._003C_003Ef__am_0024cache7 == null)
			{
				_003CDoInitialize_003Ec__Iterator90._003C_003Ef__am_0024cache7 = new Func<_003C_003E__AnonType0<Network.EventData, int>, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			int defaultIndex = Enumerable.Select(source, _003CDoInitialize_003Ec__Iterator90._003C_003Ef__am_0024cache7).FirstOrDefault();
			SetCurrentIndex(defaultIndex);
			yield return (object)this.StartCoroutine(LoadBanner(GetEventDataFromList(GetCurrentIndex()), GetCurrentIndex(), null));
			callback.Invoke();
		}
	}

	public override void StartSection()
	{
		base.StartSection();
	}

	protected override void InitCard(int cardIndex)
	{
		CardData cardData = cardDataList[cardIndex];
		Transform val = cardData.cardTransform = GetCtrl(UI.OBJ_CARD);
	}

	public override void UpdateUI()
	{
		if (cardDataList != null && cardDataList.Count > 0 && cardDataList[currentCardIndex].gridDataList.Count > 0)
		{
			int count = cardDataList.Count;
			if (isFirstUpdate)
			{
				isFirstUpdate = false;
				SetActive((Enum)UI.OBJ_COMPLETE, false);
				bool isCompleted = GetCurrentCard().allBingoData.isCompleted;
				SetActive((Enum)UI.OBJ_COMPLETE_STAY, isCompleted);
				if (isCompleted)
				{
					Transform ctrl = GetCtrl(UI.OBJ_COMPLETE_STAY);
					UITweenCtrl component = ctrl.GetComponent<UITweenCtrl>();
					component.Reset();
					component.Play(true, null);
				}
			}
			SetActive((Enum)UI.OBJ_BINGO_ANIMATION, false);
			UpdateLeftRightButton();
			UpdateCard(cardDataList[currentCardIndex], currentCardIndex);
			SetEndDateLabel();
		}
	}

	private void HideLeftRightButton()
	{
		SetActive((Enum)UI.BTN_RIGHT, false);
		SetActive((Enum)UI.BTN_LEFT, false);
	}

	private void DispLeftRightButton()
	{
		SetActive((Enum)UI.BTN_RIGHT, true);
		SetActive((Enum)UI.BTN_LEFT, true);
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
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
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
		if (component != null)
		{
			component.set_enabled(true);
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
				SetActive((Enum)uI2, num == i);
			}
		}
		else
		{
			SetActive(cardData.cardTransform, UI.SPR_REWARD_GRID_ITEM, false);
		}
	}

	private unsafe void OnQuery_LEFT()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		currentCardIndex++;
		currentCardIndex %= cardDataList.Count;
		isComeFromLeftRightEvent = true;
		GameSection.StayEvent();
		ChangeCard(currentCardIndex, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private unsafe void OnQuery_RIGHT()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		currentCardIndex--;
		if (currentCardIndex < 0)
		{
			currentCardIndex = cardDataList.Count - 1;
		}
		isComeFromLeftRightEvent = true;
		GameSection.StayEvent();
		ChangeCard(currentCardIndex, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override void ChangeCard(int cardIndex, Action callback)
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		SetActive((Enum)UI.OBJ_COMPLETE, false);
		bool isCompleteAll = GetCurrentCard().allBingoData.isCompleted;
		SetActive((Enum)UI.OBJ_COMPLETE_STAY, false);
		if (isCompleteAll)
		{
			Transform ctrl = GetCtrl(UI.OBJ_COMPLETE_STAY);
			UITweenCtrl component = ctrl.GetComponent<UITweenCtrl>();
			component.Reset();
		}
		this.StartCoroutine(LoadBanner(GetEventDataFromList(cardIndex), cardIndex, delegate
		{
			HideReward(cardDataList[cardIndex]);
			RefreshUI();
			if (isCompleteAll)
			{
				Transform ctrl2 = GetCtrl(UI.OBJ_COMPLETE_STAY);
				SetActive((Enum)UI.OBJ_COMPLETE_STAY, true);
				UITweenCtrl component2 = ctrl2.GetComponent<UITweenCtrl>();
				component2.Reset();
				component2.Play(true, null);
			}
			callback.Invoke();
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
