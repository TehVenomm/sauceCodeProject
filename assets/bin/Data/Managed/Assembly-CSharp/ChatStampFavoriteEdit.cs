using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChatStampFavoriteEdit : MonoBehaviour
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

	private IEnumerator DoOpen()
	{
		if (!initialized)
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItemFavorite");
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			mChatStampPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
			yesButton.onClick.Add(new EventDelegate(OnYes));
			noButton.onClick.Add(new EventDelegate(OnNo));
			initialized = true;
			favoriteIcons = new List<Transform>();
			unlockIcons = new List<Transform>();
		}
		selectedIcon = CreateStampItem(selectedIconRoot.transform).GetComponent<ChatStampListItem>();
		selectedIcon.SetActiveComponents(isActive: false);
		CreateFavoriteStampList();
		InitFavoriteStampList();
		CreateUnlockStampList();
		InitUnlockStampList();
	}

	public void Open()
	{
		base.gameObject.SetActive(value: true);
		tweenCtrl.Play();
		currentFavorite = MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.ToArray();
		currentUnlockStamps = Singleton<StampTable>.I.GetUnlockStamps(MonoBehaviourSingleton<UserInfoManager>.I);
		SetBlocker(isTopActive: true);
		StartCoroutine(DoOpen());
	}

	public void Close(bool update)
	{
		tweenCtrl.Play(forward: false, delegate
		{
			base.gameObject.SetActive(value: false);
		});
		if (update)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.SetFavoriteStamp(currentFavorite.ToList());
			MonoBehaviourSingleton<UIManager>.I.mainChat.UpdateStampList();
			return;
		}
		for (int i = 0; i < currentFavorite.Length; i++)
		{
			if (currentFavorite[i] != MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[i])
			{
				favoriteIcons[i].GetComponent<ChatStampListItem>().Init(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[i]);
			}
		}
	}

	private void CreateFavoriteStampList()
	{
		for (int i = favoriteIcons.Count; i < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count; i++)
		{
			Transform item = CreateStampItem(favoriteGrid.transform);
			favoriteIcons.Add(item);
		}
		favoriteGrid.Reposition();
	}

	private void CreateUnlockStampList()
	{
		for (int i = unlockIcons.Count; i < currentUnlockStamps.Count; i++)
		{
			Transform item = CreateStampItem(unlockGrid.transform);
			unlockIcons.Add(item);
		}
		unlockGrid.Reposition();
		UIUtility.SetGridItemsDraggableWidget(unlockScroll, unlockGrid, currentUnlockStamps.Count);
	}

	private void InitFavoriteStampList()
	{
		for (int i = 0; i < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count; i++)
		{
			int index = i;
			Transform iTransform = favoriteIcons[i];
			InitStampItem(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[i], iTransform, delegate
			{
				OnClickFavoriteStamp(index);
			});
		}
	}

	private void InitUnlockStampList()
	{
		for (int j = 0; j < currentUnlockStamps.Count; j++)
		{
			int i = (int)currentUnlockStamps[j].id;
			Transform iTransform = unlockIcons[j];
			InitStampItem((int)currentUnlockStamps[j].id, iTransform, delegate
			{
				OnClickUnlockStamp(i);
			});
		}
	}

	private Transform CreateStampItem(Transform parent)
	{
		Transform transform = ResourceUtility.Realizes(mChatStampPrefab, 5);
		transform.parent = parent;
		transform.localScale = Vector3.one;
		return transform;
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
		SetBlocker(isTopActive: false);
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
				component.GetComponent<UITweenCtrl>().Play();
				currentFavorite[num] = num2;
				ChatStampListItem component2 = favoriteIcons[num].GetComponent<ChatStampListItem>();
				component2.Init(num2);
				component2.GetComponent<UITweenCtrl>().Reset();
				component2.GetComponent<UITweenCtrl>().Play();
			}
		}
		else
		{
			currentFavorite[selectFavoriteStampIndex] = stampId;
			ChatStampListItem component3 = favoriteIcons[selectFavoriteStampIndex].GetComponent<ChatStampListItem>();
			component3.Init(stampId);
			component3.GetComponent<UITweenCtrl>().Reset();
			component3.GetComponent<UITweenCtrl>().Play();
		}
		SetBlocker(isTopActive: true);
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
		selectedIcon.transform.localPosition = selectIcon.localPosition;
		selectedIcon.Init(stampId);
	}

	private void OnYes()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.OK);
		Close(update: true);
	}

	private void OnNo()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
		Close(update: false);
	}
}
