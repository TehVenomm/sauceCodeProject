using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChatStampFavoriteEdit
{
	public UIGrid favoriteGrid;

	public UIGrid unlockGrid;

	public UIScrollView unlockScroll;

	public UIButton yesButton;

	public UIButton noButton;

	public UITweenCtrl tweenCtrl;

	public GameObject topBlocker;

	public GameObject bottomBlocker;

	public GameObject selectedIconRoot;

	private GameObject mChatStampPrefab;

	private int selectFavoriteStampIndex;

	private int[] currentFavorite;

	private List<StampTable.Data> currentUnlockStamps;

	private List<Transform> favoriteIcons;

	private List<Transform> unlockIcons;

	private ChatStampListItem selectedIcon;

	private bool initialized;

	public ChatStampFavoriteEdit()
		: this()
	{
	}

	private IEnumerator DoOpen()
	{
		if (!initialized)
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItemFavorite", false);
			if (loadingQueue.IsLoading())
			{
				yield return (object)loadingQueue.Wait();
			}
			mChatStampPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
			yesButton.onClick.Add(new EventDelegate(OnYes));
			noButton.onClick.Add(new EventDelegate(OnNo));
			initialized = true;
			favoriteIcons = new List<Transform>();
			unlockIcons = new List<Transform>();
		}
		selectedIcon = CreateStampItem(selectedIconRoot.get_transform()).GetComponent<ChatStampListItem>();
		selectedIcon.SetActiveComponents(false);
		CreateFavoriteStampList();
		InitFavoriteStampList();
		CreateUnlockStampList();
		InitUnlockStampList();
	}

	public void Open()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(true);
		tweenCtrl.Play(true, null);
		currentFavorite = MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.ToArray();
		currentUnlockStamps = Singleton<StampTable>.I.GetUnlockStamps(MonoBehaviourSingleton<UserInfoManager>.I);
		SetBlocker(true);
		this.StartCoroutine(DoOpen());
	}

	public void Close(bool update)
	{
		tweenCtrl.Play(false, delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			this.get_gameObject().SetActive(false);
		});
		if (update)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.SetFavoriteStamp(currentFavorite.ToList());
			MonoBehaviourSingleton<UIManager>.I.mainChat.UpdateStampList();
		}
		else
		{
			for (int i = 0; i < currentFavorite.Length; i++)
			{
				if (currentFavorite[i] != MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[i])
				{
					favoriteIcons[i].GetComponent<ChatStampListItem>().Init(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[i]);
				}
			}
		}
	}

	private void CreateFavoriteStampList()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		for (int i = favoriteIcons.Count; i < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count; i++)
		{
			Transform item = CreateStampItem(favoriteGrid.get_transform());
			favoriteIcons.Add(item);
		}
		favoriteGrid.Reposition();
	}

	private void CreateUnlockStampList()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		for (int i = unlockIcons.Count; i < currentUnlockStamps.Count; i++)
		{
			Transform item = CreateStampItem(unlockGrid.get_transform());
			unlockIcons.Add(item);
		}
		unlockGrid.Reposition();
		UIUtility.SetGridItemsDraggableWidget(unlockScroll, unlockGrid, currentUnlockStamps.Count);
	}

	private unsafe void InitFavoriteStampList()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		for (int i = 0; i < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count; i++)
		{
			int index = i;
			Transform iTransform = favoriteIcons[i];
			_003CInitFavoriteStampList_003Ec__AnonStorey2D5 _003CInitFavoriteStampList_003Ec__AnonStorey2D;
			InitStampItem(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[i], iTransform, new Action((object)_003CInitFavoriteStampList_003Ec__AnonStorey2D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private unsafe void InitUnlockStampList()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		for (int j = 0; j < currentUnlockStamps.Count; j++)
		{
			int i = (int)currentUnlockStamps[j].id;
			Transform iTransform = unlockIcons[j];
			_003CInitUnlockStampList_003Ec__AnonStorey2D6 _003CInitUnlockStampList_003Ec__AnonStorey2D;
			InitStampItem((int)currentUnlockStamps[j].id, iTransform, new Action((object)_003CInitUnlockStampList_003Ec__AnonStorey2D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private Transform CreateStampItem(Transform parent)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(mChatStampPrefab, 5);
		val.set_parent(parent);
		val.set_localScale(Vector3.get_one());
		return val;
	}

	private void InitStampItem(int stampId, Transform iTransform, Action onClick)
	{
		ChatStampListItem component = iTransform.GetComponent<ChatStampListItem>();
		component.Init(stampId);
		component.onButton = onClick;
	}

	private void OnClickFavoriteStamp(int index)
	{
		selectFavoriteStampIndex = index;
		SetBlocker(false);
		SetSelectedFavoriteIcon(favoriteIcons[index], currentFavorite[index]);
	}

	private void OnClickUnlockStamp(int stampId)
	{
		int num = Array.IndexOf(currentFavorite, stampId);
		if (num >= 0)
		{
			if (num != selectFavoriteStampIndex)
			{
				int num2 = currentFavorite[selectFavoriteStampIndex];
				currentFavorite[selectFavoriteStampIndex] = stampId;
				ChatStampListItem component = favoriteIcons[selectFavoriteStampIndex].GetComponent<ChatStampListItem>();
				component.Init(stampId);
				component.GetComponent<UITweenCtrl>().Reset();
				component.GetComponent<UITweenCtrl>().Play(true, null);
				currentFavorite[num] = num2;
				component = favoriteIcons[num].GetComponent<ChatStampListItem>();
				component.Init(num2);
				component.GetComponent<UITweenCtrl>().Reset();
				component.GetComponent<UITweenCtrl>().Play(true, null);
			}
		}
		else
		{
			currentFavorite[selectFavoriteStampIndex] = stampId;
			ChatStampListItem component2 = favoriteIcons[selectFavoriteStampIndex].GetComponent<ChatStampListItem>();
			component2.Init(stampId);
			component2.GetComponent<UITweenCtrl>().Reset();
			component2.GetComponent<UITweenCtrl>().Play(true, null);
		}
		SetBlocker(true);
	}

	private void SetBlocker(bool isTopActive)
	{
		topBlocker.SetActive(!isTopActive);
		bottomBlocker.SetActive(isTopActive);
		if (selectedIcon != null)
		{
			selectedIcon.SetActiveComponents(!isTopActive);
		}
	}

	private void SetSelectedFavoriteIcon(Transform selectIcon, int stampId)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		selectedIcon.get_transform().set_localPosition(selectIcon.get_localPosition());
		selectedIcon.Init(stampId);
	}

	private void OnYes()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.OK, 1f);
		Close(true);
	}

	private void OnNo()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CANCEL, 1f);
		Close(false);
	}
}
